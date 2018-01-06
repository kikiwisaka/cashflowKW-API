define(function(require, exports, module) {
    'use strict';
    var View = require('modaldialogdefault');
    var template = require('text!./template.html');
    var commonFunction = require('commonfunction');
    var Model = require('./../table/model');
    var eventAggregator = require('eventaggregator');
    require('bootstrap-validator');
    require('jquerymask');

    module.exports = View.extend({
        template: _.template(template),
        initialize: function(options) {
          var self = this;
          this.model = new Model();
          this.model.urlRoot += `/${commonFunction.getLastSplitHash()}`;
          this.model.set('ColorId', options.model.get('Id'));
          this.masterColor = options.model.get('Warna');

          this.listenTo(this.model, 'request', function() {});

          this.listenTo(this.model, 'sync error', function() {});
          
          commonFunction.setSelect2Matrix(this);

          this.listenTo(this.model, 'sync', function(model) {
            commonFunction.responseSuccessUpdateAddDelete('Comment successfully created.');
            self.$el.modal('hide');
            eventAggregator.trigger('master/comment/detail/add:fetch');
          });
        },
        afterRender: function() {
          this.setTemplate();
          this.renderValidation();
        },
        setTemplate: function() {
          this.$('[name="MasterWarna"]').val(this.masterColor);
        },
        renderValidation: function() {
          var self = this;
          this.$('form').bind("keypress", function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
          this.$('form').bootstrapValidator({
              fields: {
                Comment: {
                  validators:{
                    notEmpty:{
                      message: 'Comment is required'
                    }
                  }
                }
              }
            })
            .on('success.form.bv', function(e) {
              e.preventDefault();
              self.doSave();
            });
        },
        getConfirmation: function(){
          var data = this.$('[name="Comment"]').val();
          var action = "add";
          var retVal = confirm("Are you sure want to " + action + " Comment : "+ data +" ?");
          if( retVal == true ){
             this.doSave();
            }
          else{
            this.$('[type="submit"]').attr('disabled', false);
            }
          },
        doSave: function() {
          var data = commonFunction.formDataToJson(this.$('form').serializeArray());
          data.ColorCommentId = this.model.get('ColorId');
          data.MatrixId = this.$('[name="NamaMatrix"]').val();
          this.model.save(data);
        }
    });
});
