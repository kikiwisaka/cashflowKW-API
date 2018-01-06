define(function(require, exports, module) {
    'use strict';
    var View = require('modaldialogdefault');
    var template = require('text!./template.html');
    var commonFunction = require('commonfunction');
    var Model = require('./../model');
    var eventAggregator = require('eventaggregator');
    require('bootstrap-validator');
    require('jquerymask');

    module.exports = View.extend({
        template: _.template(template),
        initialize: function() {
          var self = this;
          this.model = new Model();
          this.listenTo(this.model, 'request', function() {});

          this.listenTo(this.model, 'sync error', function() {});

          this.listenTo(this.model, 'sync', function(model) {
            commonFunction.responseSuccessUpdateAddDelete('PMN successfully created.');
            self.$el.modal('hide');
            eventAggregator.trigger('master/asset_data/pmn/add:fetch');
          });
        },
        afterRender: function() {
          this.renderValidation();
        },
        renderValidation: function() {
          var self = this;
          this.$('[ehs-form]').bind("keypress", function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
          this.$('form').bootstrapValidator({
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
          var action = "add";
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
          //debugger;
          this.model.save(data);
        }
    });
});
