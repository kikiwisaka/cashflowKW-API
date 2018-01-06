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
          var emergencyId = this.model.get('Id');
          this.model = new Model();
          this.model.set(this.model.idAttribute, emergencyId);
          this.listenToOnce(this.model, 'sync', function(model) {
              this.render();
              var data = model.toJSON();
              this.listenTo(this.model, 'sync', function() {
                commonFunction.responseSuccessUpdateAddDelete('Tahapan Penjamin successfully updated.');
                self.$el.modal('hide');
                eventAggregator.trigger('master/tahapanproject/edit:fecth');
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
                NamaTahapan: {
                  validators:{
                    stringLength: {
                        message: 'Nama Tahapan Penjamin must be less than 50 characters',
                        max: 50
                    },
                    notEmpty:{
                      message: 'Nama Tahapan Penjamin is required'
                    },
                    regexp: {
                        regexp: /^[a-z\s]+$/i,
                        message: 'Nama Tahapan Penjamin can consist of alphabetical characters and spaces only'
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
          var data = this.$('[name="NamaTahapan"]').val();
          var action = "edit";
          var retVal = confirm("Are you sure want to " + action + " Tahapan Penjamin : "+ data +" ?");
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
