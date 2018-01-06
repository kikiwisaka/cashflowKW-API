define(function (require, exports, module) {
  'use strict';
  var View = require('modaldialogdefault');
  var template = require('text!./template.html');
  var commonFunction = require('commonfunction');
  var commonConfig = require('commonconfig');
  var Model = require('./../model');
  var eventAggregator = require('eventaggregator');
  require('bootstrap-validator');
  require('datetimepicker');
  require('select2');

  module.exports = View.extend({
    template: _.template(template),
    initialize: function () {
      var self = this;
      this.model = new Model();
      this.listenTo(this.model, 'request', function () {});

      this.listenTo(this.model, 'sync error', function () {});

      // this.listenTo(eventAggregator, 'master/project/select_risk:risk_selected', function (obj) {
      //   self.riskRegistrasiId = obj;
      // });

      commonFunction.setSelect2Tahapan(this);
      commonFunction.setSelect2Sektor(this);
      commonFunction.setSelect2Risk(this);
      this.listenTo(this.model, 'sync', function (model) {
        commonFunction.responseSuccessUpdateAddDelete('Project successfully created.');
        self.$el.modal('hide');
        eventAggregator.trigger('master/project/add:fecth');
      });
      this.listenTo(eventAggregator, 'master/project/add/select_risk:risk_selected', function(riskNames, riskIds) {
        self.setTemplate(riskNames);
        self.riskIds = riskIds;
      });
    },
    afterRender: function () {
      this.$('[name="TahunAwalProject"], [name="TahunAkhirProject"]').datetimepicker({
        defaultDate: new(Date),
        format: commonConfig.datePickerFormat
      });
      this.renderValidation();
    },
    events: {
      'click [name="SelectRisk"]': 'selectRisk',
      'click [name="EditSelectRisk"]': 'selectRisk',
    },
    setTemplate: function(obj) {
      var chosenName = obj.join(", ");
      var currentChosenRisk = this.$('[name="ChosenRisk"]').text(chosenName);
      if(this.$('[name="ChosenRisk"]').val()){
        this.$('[name="EditSelectRisk"]').removeClass('hidden');
        this.$('[name="SelectRisk"]').addClass('hidden');
      }
    },
    selectRisk: function () {
      var self = this;
      require(['./../select_risk/view'], (View) => {
        commonFunction.setDefaultModalDialogFunction(self, View, this.model);
      });
    },
    renderValidation: function () {
      var self = this;
      this.$('[ehs-form]').bind("keypress", function (e) {
        if (e.keyCode == 13) {
          return false;
        }
      });
      this.$('[ehs-form]').bootstrapValidator({
          fields: {
            NamaProject: {
              validators: {
                stringLength: {
                  message: 'Nama Project must be less than 50 characters',
                  max: 50
                },
                notEmpty: {
                  message: 'Nama Project is required'
                }
              }
            },
            Keterangan: {
              validators: {
                stringLength: {
                  message: 'Keterangan must be less than 150 characters',
                  max: 150
                },
                notEmpty: {
                  message: 'Keterangan is required'
                }
              }
            },
            TahunAwalProject: {
              validators: {
                notEmpty: {
                  message: 'Awal Project is required'
                }
              }
            },
            TahunAkhirProject: {
              validators: {
                notEmpty: {
                  message: 'Akhir Project is required'
                }
              }
            },
            Minimum: {
              validators: {
                notEmpty: {
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
              validators: {
                notEmpty: {
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
              validators: {
                notEmpty: {
                  message: 'Sektor is required'
                }
              }
            },
            NamaTahapan: {
              validators: {
                notEmpty: {
                  message: 'Tahapan is required'
                }
              }
            },
            NamaCategoryRisk: {
              validators: {
                notEmpty: {
                  message: 'Risk is required'
                }
              }
            }
          }
        })
        .on('success.form.bv', function (e) {
          e.preventDefault();
          self.getConfirmation();
        });
    },
    getConfirmation: function(){
      var data = this.$('[name="NamaProject"]').val();
      var action = "add";
      var retVal = confirm("Are you sure want to " + action + " Project : "+ data +" ?");
      if( retVal == true ){
         this.doSave();
      }
      else{
        this.$('[type="submit"]').attr('disabled', false);
      }
    },
    doSave: function () {
      var data = commonFunction.formDataToJson(this.$('form').serializeArray());
      data.TahapanId = this.$('[name="NamaTahapan"]').val();
      data.SektorId = this.$('[name="NamaSektor"]').val();
      data.RiskRegistrasiId = this.riskIds;
      this.model.save(data);
    }
  });
});