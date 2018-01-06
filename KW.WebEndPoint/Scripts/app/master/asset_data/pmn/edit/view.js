define(function(require, exports, module) {
    'use strict';
    var View = require('modaldialogdefault');
    var template = require('text!./template.html');
    var Model = require('./../model');
    var commonFunction = require('commonfunction');
    var commonConfig = require('commonconfig');
    var eventAggregator = require('eventaggregator');
    require('bootstrap-validator');
    require('jquerymask');

    module.exports = View.extend({
        template: _.template(template),
        initialize : function(){
          var self = this;
          var stageId = this.model.get('Id');
          this.model = new Model();
          this.model.set(this.model.idAttribute, stageId);
          this.listenToOnce(this.model, 'sync', function(model) {
              this.render();
              var data = model.toJSON();
              this.listenTo(this.model, 'sync', function() {
                commonFunction.responseSuccessUpdateAddDelete('PMN successfully updated.');
                self.$el.modal('hide');
                eventAggregator.trigger('master/asset_data/pmn/edit:fetch');
              });
          }, this);

          this.once('afterRender', function() {
              this.model.fetch();
          });
        },
        afterRender : function(){
          this.setTemplate();
          this.renderValidation();
        },
        setTemplate : function(){
          // this.$('[name="Relationship"]').val(this.model.get('Relationship'));
        },
        renderValidation: function() {
          var self = this;
          this.$('[ehs-form]').bootstrapValidator({
              fields: {
                PMNToModalDasarCap: {
                  validators: {
                    numeric: {
                      message: 'PMNToModalDasarCap format is not valid. It should be 123.45 or 12.34 or 12',
                      thousandsSeparator: '',
                      decimalSeparator: '.'
                    }
                  }
                },
                RecourseDelay: {
                  validators: {
                    numeric: {
                      message: 'RecourseDelay format is not valid. It should be 123.45 or 12.34 or 12',
                      thousandsSeparator: '',
                      decimalSeparator: '.'
                    }
                  }
                },
                DelayYears: {
                  validators: {
                    numeric: {
                      message: 'DelayYears format is not valid. It should be 123.45 or 12.34 or 12',
                      thousandsSeparator: '',
                      decimalSeparator: '.'
                    }
                  }
                },
                OpexGrowth: {
                  validators: {
                    numeric: {
                      message: 'OpexGrowth format is not valid. It should be 123.45 or 12.34 or 12',
                      thousandsSeparator: '',
                      decimalSeparator: '.'
                    }
                  }
                },
                Opex: {
                  validators: {
                    numeric: {
                      message: 'Opex format is not valid. It should be 123.45 or 12.34 or 12',
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
          var data = this.$('[name="PMNToModalDasarCap"]').val();
          var action = "edit";
          var retVal = confirm("Are you sure want to " + action + " PMN Tahun : "+ data +" ?");
          if( retVal == true ){
             this.doSave();
          }
          else{
            this.$('[type="submit"]').attr('disabled', false);
          }
        },
        doSave: function() {
          var data = commonFunction.formDataToJson(this.$('form').serializeArray());
          this.model.save(data);
        }
    });
});
