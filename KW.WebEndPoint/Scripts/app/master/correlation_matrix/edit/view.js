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
        initialize : function(){
          var self = this;
          var correlationId = this.model.get('Id');
          this.model = new Model();
          this.model.set(this.model.idAttribute, correlationId);
          this.listenToOnce(this.model, 'sync', function(model) {
              this.render();
              var data = model.toJSON();
              this.listenTo(this.model, 'sync', function() {
                commonFunction.responseSuccessUpdateAddDelete('Correlation Type successfully updated.');
                self.$el.modal('hide');
                eventAggregator.trigger('master/correlationmatrix/edit:fecth');
              });
          }, this);

          this.once('afterRender', function() {
              this.model.fetch();
          });
        },
        afterRender : function(){
          this.renderValidation();
        },
        renderValidation: function() {
          var self = this;
          this.$('[ehs-form]').bootstrapValidator({
              fields: {
                NamaCorrelationMatrix: {
                  validators:{
                    stringLength: {
                        message: 'Nama Correalation Type must be less than 50 characters',
                        max: 50
                    },
                    notEmpty:{
                      message: 'Nama Correalation Type is required'
                    },
                    regexp: {
                        regexp: /^[a-z\s]+$/i,
                        message: 'Nama Correalation Type can consist of alphabetical characters and spaces only'
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
                }
              }
            })
            .on('success.form.bv', function(e) {
              e.preventDefault();
              self.getConfirmation();
            });
        },
        getConfirmation: function(){
          var data = this.$('[name="NamaCorrelationMatrix"]').val();
          var action = "edit";
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
