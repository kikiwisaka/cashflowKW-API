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
                commonFunction.responseSuccessUpdateAddDelete('Asset Data successfully updated.');
                self.$el.modal('hide');
                eventAggregator.trigger('master/asset_data/edit:fetch');
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
          var action = "edit";
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
          this.model.save(data);
        }
    });
});
