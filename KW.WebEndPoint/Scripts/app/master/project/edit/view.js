define(function(require, exports, module) {
    'use strict';
    var View = require('modaldialogdefault');
    var template = require('text!./template.html');
    var Model = require('./../model');
    var commonFunction = require('commonfunction');
    var commonConfig = require('commonconfig');
    var eventAggregator = require('eventaggregator');
    require('bootstrap-validator');
    require('datetimepicker');
    require('select2');

    module.exports = View.extend({
        template: _.template(template),
        initialize : function(){
          var self = this;
          var tahapanId = this.model.get('TahapanId');
          var sektorId = this.model.get('SektorId');
          var projectId = this.model.get('Id');
          this.project = this.model;
          this.riskIds = [];
          if(this.model.get('RiskRegistrasi').length){
            _.each(this.model.get('RiskRegistrasi'), function(item) {
              self.riskIds.push(item.Id);
            });
          }
          this.model = new Model();
          this.model.set(this.model.idAttribute, projectId);
          this.listenToOnce(this.model, 'sync', function(model) {
              this.render();
              var data = model.toJSON();
              this.listenTo(this.model, 'sync', function() {
                commonFunction.responseSuccessUpdateAddDelete('Project successfully updated.');
                self.$el.modal('hide');
                eventAggregator.trigger('master/project/edit:fecth');
              });
              commonFunction.setSelect2Tahapan(this).then(function(object){
                self.$(object.options.selector).val(tahapanId).trigger('change');
              });
              commonFunction.setSelect2Sektor(this).then(function(object){
                self.$(object.options.selector).val(sektorId).trigger('change');
              });
          }, this);
          var selectorTahapan = this.$('[name="NamaTahapan"]');
          this.on('beforeRender beforeRemove cleanup', function() {
            if (selectorTahapan.length && selectorTahapan.hasClass('select2-hidden-accessible')) {
                selectorTahapan.select2('destroy');
            }
          });
          var selectorSektor = this.$('[name="NamaSektor"]');
          this.on('beforeRender beforeRemove cleanup', function() {
            if (selectorSektor.length && selectorSektor.hasClass('select2-hidden-accessible')) {
                selectorSektor.select2('destroy');
            }
          });
          this.listenTo(eventAggregator, 'master/project/select_risk:risk_selected', function(obj) {
            self.riskRegistrasiId = obj;
          });
          this.listenTo(eventAggregator, 'master/project/add/select_risk:risk_selected', function(riskNames, riskIds) {
            self.renderRiskAfterEdit(riskNames);
            self.riskIds = riskIds;
          });
          this.once('afterRender', function() {
              this.model.fetch();
          });
        },
        afterRender : function(){
          this.$('[name="TahunAwalProject"], [name="TahunAkhirProject"]').datetimepicker({
            format : commonConfig.datePickerFormat
          });
          this.setTemplate(this.project);
          this.renderValidation();
        },
        events: {
          'click [name="SelectRisk"]': 'selectRisk'
        },
        setTemplate: function(obj) {
          var riskNames = '';
          var risk = obj.get('RiskRegistrasi');
          if(risk.length > 0) {
            for(var i = 0; i < risk.length; i++) {
              riskNames += risk[i].NamaCategoryRisk;
              riskNames += ', ';
            }
          }
          this.$('[name="ChosenRisk"]').text(riskNames);
        },
        renderRiskAfterEdit: function(obj) {
          var chosenName = obj.join(", ");
          this.$('[name="ChosenRisk"]').text(chosenName);
        },
        selectRisk: function() {
          var self = this;
          require(['./../select_risk/view'], (View) => {
            commonFunction.setDefaultModalDialogFunction(self, View, this.model);
          });
        },
        renderValidation: function() {
          var self = this;
          this.$('[ehs-form]').bootstrapValidator({
              fields: {
                NamaProject: {
                  validators:{
                    stringLength: {
                        message: 'Nama Project must be less than 50 characters',
                        max: 50
                    },
                    notEmpty:{
                      message: 'Nama Project is required'
                    }
                  }
                },
                Keterangan: {
                  validators:{
                    stringLength: {
                        message: 'Keterangan must be less than 150 characters',
                        max: 150
                    },
                    notEmpty:{
                      message: 'Keterangan is required'
                    }
                  }
                },
                TahunAwalProject: {
                  validators:{
                    notEmpty:{
                      message: 'Awal Project is required'
                    }
                  }
                },
                TahunAkhirProject: {
                  validators:{
                    notEmpty:{
                      message: 'Akhir Project is required'
                    }
                  }
                },
                Minimum: {
                  validators:{
                    notEmpty:{
                      message: 'Minimum is required'
                    },
                    numeric: {
                      message: 'Minimum format is not valid. It should be 123.45 or 12.34 or 12',
                      thousandsSeparator: '',
                      decimalSeparator: '.'
                    }
                  }
                },
                Maximum: {
                  validators:{
                    notEmpty:{
                      message: 'Maximum is required'
                    },
                    numeric: {
                      message: 'Maximum format is not valid. It should be 123.45 or 12.34 or 12',
                      thousandsSeparator: '',
                      decimalSeparator: '.'
                    }
                  }
                },
                NamaSektor: {
                  validators:{
                    notEmpty:{
                      message: 'Sektor is required'
                    }
                  }
                },
                NamaTahapan: {
                  validators:{
                    notEmpty:{
                      message: 'Tahapan is required'
                    }
                  }
                },
                NamaCategoryRisk: {
                  validators:{
                    notEmpty:{
                      message: 'Risk is required'
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
          var data = this.$('[name="NamaProject"]').val();
          var action = "edit";
          var retVal = confirm("Are you sure want to " + action + " Project : "+ data +" ?");
          if( retVal == true ){
             this.doSave();
          }
          else{
            this.$('[type="submit"]').attr('disabled', false);
          }
        },
        doSave: function() {
          var data = commonFunction.formDataToJson(this.$('form').serializeArray());
          data.TahapanId = this.$('[name="NamaTahapan"]').val();
          data.SektorId = this.$('[name="NamaSektor"]').val();
          data.RiskRegistrasiId = this.riskIds;
          this.model.save(data);
        }
    });
});
