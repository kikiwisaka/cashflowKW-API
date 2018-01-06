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
          this.mRiskId = options.model.get('MRiskId');
          this.masterCodeRisk = options.model.get('KodeRisk').charAt(0);
          this.subCodeRisk = options.model.get('KodeRisk').substring(2);

          this.model = new Model();
          this.model.set(this.model.idAttribute, subRiskId);
          this.listenToOnce(this.model, 'sync', function(model) {
              this.render();
              var data = model.toJSON();
              this.listenTo(this.model, 'sync', function() {
                commonFunction.responseSuccessUpdateAddDelete('Risk Event successfully updated.');
                self.$el.modal('hide');
                eventAggregator.trigger('master/risk/detail/edit:fecth');
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
        setTemplate: function() {
          this.$('[name="MasterRiskCode"]').val(this.masterCodeRisk);
          this.$('[name="SubRiskCode"]').val(this.subCodeRisk);
        },
        renderValidation: function() {
          var self = this;
          this.$('[ehs-form]').bootstrapValidator({
              fields: {
                RiskEvenClaim: {
                  validators:{
                    stringLength: {
                        message: 'Risk Even Claim must be less than 50 characters',
                        max: 50
                    },
                    notEmpty:{
                      message: 'Risk Even Claim is required'
                    },
                    regexp: {
                        regexp: /^[a-z\s]+$/i,
                        message: 'Risk Even Claim can consist of alphabetical characters and spaces only'
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
                    },
                    regexp: {
                        regexp: /^[a-z\s]+$/i,
                        message: 'Description can consist of alphabetical characters and spaces only'
                    }
                  }
                },
                SugestionMigration: {
                  validators:{
                    stringLength: {
                        message: 'Sugestion Migration must be less than 50 characters',
                        max: 50
                    },
                    notEmpty:{
                      message: 'Sugestion Migration is required'
                    },
                    regexp: {
                        regexp: /^[a-z\s]+$/i,
                        message: 'Sugestion Migration can consist of alphabetical characters and spaces only'
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
          var action = "edit";
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
          data.MRiskId = this.mRiskId;
          this.model.save(data);
        }
    });
});
