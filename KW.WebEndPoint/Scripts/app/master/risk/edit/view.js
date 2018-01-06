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
                commonFunction.responseSuccessUpdateAddDelete('Risk Kategori successfully updated.');
                self.$el.modal('hide');
                eventAggregator.trigger('master/risk/mainrisk/edit:fecth');
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
          var action = "edit";
          var retVal = confirm("Are you sure want to " + action + " Risk Kategori : "+ data +" ?");
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
