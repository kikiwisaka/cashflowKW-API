define(function(require, exports, module) {
    'use strict';
    var View = require('modaldialogdefault');
    var template = require('text!./template.html');
    var Model = require('./../model');
    var commonFunction = require('commonfunction');
    var commonConfig = require('commonconfig');
    var eventAggregator = require('eventaggregator');
    require('bootstrap-validator');

    module.exports = View.extend({
        template: _.template(template),
        events: {
          'click [name="setdefault"]':'getConfirmation'
        },
        initialize : function(){
          var self = this;
          this.projectIds = [];
          _.each(this.model.get('Project'), function(item){
            self.projectIds.push(item.Id);
          });
          var scenarioId = this.model.get('Id');
          this.model = this.model;
          this.model = new Model();
          this.model.set(this.model.idAttribute, scenarioId);
          this.listenToOnce(this.model, 'sync', function(model) {
              this.render();
              var data = model.toJSON();
              this.listenTo(this.model, 'sync', function() {
                commonFunction.responseSuccessUpdateAddDelete('Scenario successfully set as default.');
                self.$el.modal('hide');
                eventAggregator.trigger('scenario/default:fecth');
              });
          }, this);

          this.once('afterRender', function() {
              this.model.fetch();
          });
        },
        afterRender : function(){
          this.renderValidation();
        },
        renderValidation: function() {
          var self = this;
          this.$('[ehs-form]').bootstrapValidator({
              fields: {
                NamaLikehood: {
                  validators:{
                    stringLength: {
                        message: 'Nama Likelihood must be less than 50 characters',
                        max: 50
                    },
                    notEmpty:{
                      message: 'Nama Likelihood is required'
                    },
                    regexp: {
                        regexp: /^[a-z\s]+$/i,
                        message: 'Nama Likelihood can consist of alphabetical characters and spaces only'
                    }
                  }
                },
                DefinisiLikehood: {
                  validators:{
                    stringLength: {
                        message: 'Definisi must be less than 10 characters',
                        max: 10
                    },
                    notEmpty:{
                      message: 'Definisi is required'
                    }
                  }
                },
                Lower: {
                  validators: {
                    notEmpty: {
                      message: 'Lower is required'
                    },
                    numeric: {
                      message: 'Lower format is not valid. It should be 123.45 or 12.34 or 12',
                      thousandsSeparator: '',
                      decimalSeparator: '.'
                    }
                  }
                },
                Upper: {
                  validators: {
                    notEmpty: {
                      message: 'Upper is required'
                    },
                    numeric: {
                      message: 'Upper format is not valid. It should be 123.45 or 12.34 or 12',
                      thousandsSeparator: '',
                      decimalSeparator: '.'
                    }
                  }
                },
                Incres: {
                  validators: {
                    notEmpty: {
                      message: 'Incres is required'
                    },
                    numeric: {
                      message: 'Incres format is not valid. It should be 123.45 or 12.34 or 12',
                      thousandsSeparator: '',
                      decimalSeparator: '.'
                    }
                  }
                }
              }
            })
            .on('success.form.bv', function(e) {
              e.preventDefault();
              self.getConfirmation();
            });
        },
        getConfirmation: function(){
          var data = this.model.get('NamaScenario');
          var action = "set default";
          var retVal = confirm("Are you sure want to " + action + " Scenario : "+ data +" ?");
          if( retVal == true ){
             this.doDefault();
          }
          else{
            this.$('[type="submit"]').attr('disabled', false);
          }
        },
        doDefault: function() {
          var data = commonFunction.formDataToJson(this.$('form').serializeArray());
          data.IsDefault = true;
          data.NamaScenario = this.model.get('NamaScenario');
          data.LikehoodId = this.model.get('LikehoodId');
          data.ProjectId = this.projectIds;
          this.model.save(data);
        }
    });
});
