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
            commonFunction.responseSuccessUpdateAddDelete('Kode Risk successfully created.');
            self.$el.modal('hide');
            eventAggregator.trigger('master/risk/mainrisk/add:fecth');
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
                KodeMRisk: {
                  validators:{
                    stringLength: {
                        message: 'Kode Master Risk must be less than 1 character',
                        max: 1
                    },
                    notEmpty:{
                      message: 'Kode Master Risk is required'
                    },
                    regexp: {
                        regexp: /^[a-z\s]+$/i,
                        message: 'Kode Master Risk can consist of alphabetical characters and spaces only'
                    }
                  }
                },
                NamaCategoryRisk: {
                  validators: {
                    notEmpty: {
                      message: 'Nama Kategori Risiko is required'
                    },
                    stringLength: {
                        message: 'Nama Kategori Risiko  must be less than 50 character',
                        max: 50
                    }
                  }
                },
                Definisi: {
                  validators:{
                    stringLength: {
                        message: 'Definisi must be less than 150 character',
                        max: 150
                    },
                    notEmpty:{
                      message: 'Definisi is required'
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
          var data = this.$('[name="KodeMRisk"]').val();
          var action = "add";
          var retVal = confirm("Are you sure want to " + action + " Kode Risk : "+ data +" ?");
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
