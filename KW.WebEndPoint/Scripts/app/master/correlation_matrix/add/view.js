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
            commonFunction.responseSuccessUpdateAddDelete('Correlation Type successfully created.');
            self.$el.modal('hide');
            eventAggregator.trigger('master/correlationmatrix/add:fecth');
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
                NamaCorrelationMatrix: {
                  validators:{
                    stringLength: {
                        message: 'Nama Correlation Type must be less than 50 characters',
                        max: 50
                    },
                    notEmpty:{
                      message: 'Nama Correlation Type is required'
                    },
                    regexp: {
                        regexp: /^[a-z\s]+$/i,
                        message: 'Nama Correlation Type can consist of alphabetical characters and spaces only'
                    }
                  }
                },
                Nilai: {
                  validators: {
                    notEmpty: {
                      message: 'Besaran Korelasi is required'
                    },
                    numeric: {
                      message: 'Besaran Korelasi format is not valid. It should be 123.45 or 12.34 or 12',
                      thousandsSeparator: '',
                      decimalSeparator: '.'
                    }
                  }
                },
              }
            })
            .on('success.form.bv', function(e) {
              e.preventDefault();
              self.getConfirmation();
            });
        },
        getConfirmation: function(){
          var data = this.$('[name="NamaCorrelationMatrix"]').val();
          var action = "add";
          var retVal = confirm("Are you sure want to " + action + " Correlation Type : "+ data +" ?");
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
