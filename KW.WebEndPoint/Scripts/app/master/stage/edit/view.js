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
                commonFunction.responseSuccessUpdateAddDelete('Stage successfully updated.');
                self.$el.modal('hide');
                eventAggregator.trigger('master/stage/edit:fecth');
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
                NamaStage: {
                  validators:{
                    stringLength: {
                        message: 'Nama Stage Project must be less than 50 characters',
                        max: 50
                    },
                    notEmpty:{
                      message: 'Nama Stage Project is required'
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
                }
              }
            })
            .on('success.form.bv', function(e) {
              e.preventDefault();
              self.getConfirmation();
            });
        },
        getConfirmation: function(){
          var data = this.$('[name="NamaStage"]').val();
          var action = "edit";
          var retVal = confirm("Are you sure want to " + action + " Stage Proyek : "+ data +" ?");
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
