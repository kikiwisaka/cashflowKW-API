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
        initialize : function(options){
          var self = this;
          var subRiskId = this.model.get('Id');
          this.model = new Model();
          this.model.set(this.model.idAttribute, subRiskId);
          this.listenToOnce(this.model, 'sync', function(model) {
              this.render();
              var data = model.toJSON();
              this.listenTo(this.model, 'sync', function() {
                commonFunction.responseSuccessUpdateAddDelete('Likelihood Definition successfully updated.');
                self.$el.modal('hide');
                eventAggregator.trigger('master/likelihood/detail/edit:fecth');
              });
          }, this);

          this.once('afterRender', function() {
              this.model.fetch();
          });
        },
        events: {
          'keyup [name="Upper"], [name="Lower"]': 'countingAverage'
        },
        afterRender : function(){
          this.setTemplate();
          this.renderValidation();
        },
        setTemplate: function() {
          this.$('[name="Incres"]').val(this.model.get('Incres'));
          // this.$('[name="SubRiskCode"]').val(this.subCodeRisk);
        },
        countingAverage: function() {
          var self = this;
          var sum = 0;
          var lower = $('[name="Lower"]').val();
          var upper = $('[name="Upper"]').val();
          sum = +lower + +upper;
          $('[name="Average"]').val(sum / 2);
        },
        renderValidation: function() {
          var self = this;
          this.$('[ehs-form]').bootstrapValidator({
              fields: {
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
                }
              }
            })
            .on('success.form.bv', function(e) {
              e.preventDefault();
              self.doSave();
            });
        },
        doSave: function() {
          var data = commonFunction.formDataToJson(this.$('form').serializeArray());
          data.Average = $('[name="Average"]').val();
          console.log(data);
          this.model.save(data);
        }
    });
});
