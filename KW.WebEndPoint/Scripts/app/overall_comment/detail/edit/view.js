define(function(require, exports, module) {
    'use strict';
    var View = require('modaldialogdefault');
    var template = require('text!./template.html');
    var Model = require('./../table/model');
    var commonFunction = require('commonfunction');
    var commonConfig = require('commonconfig');
    var eventAggregator = require('eventaggregator');
    require('bootstrap-validator');

    module.exports = View.extend({
        template: _.template(template),
        initialize : function(options){
          var self = this;
          var commentId = this.model.get('Id');
          var matrixId = this.model.get('MatrixId');
          var colorCommentId = this.model.get('ColorCommentId');
          this.masterColor = options.model.get('Warna');

          this.model = new Model();
          this.model.set(this.model.idAttribute, commentId);
          this.listenToOnce(this.model, 'sync', function(model) {
              this.render();
              var data = model.toJSON();
              this.listenTo(this.model, 'sync', function() {
                commonFunction.responseSuccessUpdateAddDelete('Action Point successfully updated.');
                self.$el.modal('hide');
                eventAggregator.trigger('overall_comment/detail/edit:fetch');
              });
              commonFunction.setSelect2Matrix(this).then(function(object){
                self.$(object.options.selector).val(matrixId).trigger('change');
              });
          }, this);
          var selectorMatrix = this.$('[name="NamaMatrix"]');
          this.on('beforeRender beforeRemove cleanup', function() {
            if (selectorMatrix.length && selectorMatrix.hasClass('select2-hidden-accessible')) {
                selectorMatrix.select2('destroy');
            }
          });
          this.once('afterRender', function() {
              this.model.fetch();
          });
        },
        afterRender : function(){
          this.setTemplate();
          this.renderValidation();
        },
        setTemplate: function() {
          this.$('[name="MasterWarna"]').val(this.masterColor);
        },
        renderValidation: function() {
          var self = this;
          this.$('[ehs-form]').bootstrapValidator({
              fields: {
                Comment: {
                  validators:{
                    notEmpty:{
                      message: 'Comment is required'
                    },
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
          var data = this.$('[name="Comment"]').val();
          var action = "add Action Point";
          var retVal = confirm("Are you sure want to " + action + " to : "+ data +" ?");
          if( retVal == true ){
             this.doSave();
          }
          else{
            this.$('[type="submit"]').attr('disabled', false);
          }
        },
        doSave: function() {
          var data = commonFunction.formDataToJson(this.$('form').serializeArray());
          data.MatrixId = this.$('[name="NamaMatrix"]').val();
          this.model.save(data);
        }
    });
});
