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

          this.listenTo(this.model, 'sync', function() {
            commonFunction.responseSuccessUpdateAddDelete('Asset Data deleted.');
            self.$el.modal('hide');
            eventAggregator.trigger('master/asset_data/delete:fetch');
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
          this.$('[ehs-form]').bootstrapValidator({
              fields: {
                AssetClass: {
                  validators: {
                    notEmpty: {
                      message: 'Asset Class is required'
                    }
                  }
                },
                TermAwal: {
                  validators: {
                    integer: {
                      message: 'Term Awal format is not valid. It should be integer,.'
                    }
                  }
                },
                TermAkhir: {
                  validators: {
                    integer: {
                      message: 'Term Akhir format is not valid. It should be integer,.'
                    }
                  }
                },
                AssumentReturn: {
                  validators: {
                    numeric: {
                      message: 'Assument Return format is not valid. It should be 123.45 or 12.34 or 12',
                      thousandsSeparator: '',
                      decimalSeparator: '.'
                    }
                  }
                },
                OutstandingStartYears: {
                  validators: {
                    integer: {
                      message: 'Outstanding Start Years format is not valid. It should be integer,.'
                    }
                  }
                },
                OutstandingEndYears: {
                  validators: {
                    integer: {
                      message: 'Outstanding End Years format is not valid. It should be integer,.'
                    }
                  }
                },
                AssetValue: {
                  validators: {
                    numeric: {
                      message: 'AssetValue format is not valid. It should be 123.45 or 12.34 or 12',
                      thousandsSeparator: '',
                      decimalSeparator: '.'
                    }
                  }
                },
                Porpotion: {
                  validators: {
                    numeric: {
                      message: 'Porpotion format is not valid. It should be 123.45 or 12.34 or 12',
                      thousandsSeparator: '',
                      decimalSeparator: '.'
                    }
                  }
                },
                AssumedReturnPercentage: {
                  validators: {
                    numeric: {
                      message: 'AssumedReturnPercentage format is not valid. It should be 123.45 or 12.34 or 12',
                      thousandsSeparator: '',
                      decimalSeparator: '.'
                    }
                  }
                },
                AssumedReturn: {
                  validators: {
                    numeric: {
                      message: 'AssumedReturn format is not valid. It should be 123.45 or 12.34 or 12',
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
        var data = this.$('[name="AssetClass"]').val();
        var action = "add";
        var retVal = confirm("Are you sure want to " + action + " Asset Data : "+ data +" ?");
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
