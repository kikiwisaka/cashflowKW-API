define(function(require, exports, module) {
    'use strict';
    var View = require('modaldialogdefault');
    var template = require('text!./template.html');
    var commonFunction = require('commonfunction');
    var Model = require('./../table/model');
    var eventAggregator = require('eventaggregator');
    require('bootstrap-validator');
    require('jquerymask');

    module.exports = View.extend({
        template: _.template(template),
        initialize: function(options) {
          var self = this;
          this.model = new Model();
          this.model.urlRoot += `/${commonFunction.getLastSplitHash()}`;
          this.model.set('MRiskId', options.model.get('Id'));
          this.masterCodeRisk = options.model.get('KodeMRisk').charAt(0);

          this.listenTo(this.model, 'request', function() {});

          this.listenTo(this.model, 'sync error', function() {});

          this.listenTo(this.model, 'sync', function(model) {
            commonFunction.responseSuccessUpdateAddDelete('Risk Event successfully created.');
            self.$el.modal('hide');
            eventAggregator.trigger('master/risk/detail/add:fecth');
          });
        },
        afterRender: function() {
          this.setTemplate();
          this.renderValidation();
        },
        setTemplate: function() {
          this.$('[name="MasterRiskCode"]').val(this.masterCodeRisk);
        },
        renderValidation: function() {
          var self = this;
          this.$('form').bind("keypress", function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
          this.$('form').bootstrapValidator({
              fields: {
                RiskEvenClaim: {
                  validators:{
                    stringLength: {
                        message: 'Risk Even Claim must be less than 50 characters',
                        max: 50
                    },
                    notEmpty:{
                      message: 'Risk Even Claim is required'
                    }
                  }
                },
                DescriptionRiskEvenClaim: {
                  validators:{
                    stringLength: {
                        message: 'Description must be less than 50 characters',
                        max: 50
                    },
                    notEmpty:{
                      message: 'Description is required'
                    }
                  }
                },
                SugestionMigration: {
                  validators:{
                    stringLength: {
                        message: 'Sugestion Mitigation must be less than 50 characters',
                        max: 50
                    },
                    notEmpty:{
                      message: 'Sugestion Mitigation is required'
                    }
                  }
                },
                MRiskId: {
                  validators: {
                    notEmpty: {
                      message: 'Risk Id is required'
                    },
                    numeric: {
                      message: 'Risk Id format is not valid. It should be 123.45 or 12.34 or 12'
                    }
                  }
                },
                KodeRisk: {
                  validators: {
                    notEmpty: {
                      message: 'Kode Risk is required'
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
          var data = this.$('[name="SubRiskCode"]').val();
          var action = "add";
          var retVal = confirm("Are you sure want to " + action + " Risk Event : "+ data +" ?");
          if( retVal == true ){
             this.doSave();
          }
          else{
            this.$('[type="submit"]').attr('disabled', false);
          }
        },
        doSave: function() {
          var data = commonFunction.formDataToJson(this.$('form').serializeArray());
          var subRiskCode = this.$('[name="SubRiskCode"]').val();
          data.KodeRisk = this.masterCodeRisk + '-' +  subRiskCode;
          data.RiskRegistrasiId = this.model.get('MRiskId');
          this.model.save(data);
        }
    });
});
