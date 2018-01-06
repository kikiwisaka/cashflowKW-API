define(function(require, exports, module) {
    'use strict';
    require('recaptcha');
    var LayoutManager = require('layoutmanager');
    var template = require('text!./../login/template.html');
    var templateTitle = require('text!./title.html');
    var templateContent = require('text!./content.html');
    var templateResetPasswordRequest = _.template(require('text!./templateresetpasswordrequest.html'));
    var templateEmailAddressNotRegistered = _.template(require('text!./templateemailaddressnotregistered.html'));
    require('bootstrap-validator');

    var commonConfig = require('commonconfig');
    var commonFunction = require('commonfunction');

    var widgetId = undefined;

    module.exports = LayoutManager.extend({
        className: 'container center-middle',
        template: _.template(template),
        initialize: function() {
            this.options = this.options || {};
            this.options.loadingRecaptcha = false;
            this.options.firstTimeRecaptcha = true;
        },
        serialize: function() {
            var templatingContent = _.template(templateContent);

            return {
                title: templateTitle,
                content: templatingContent({
                }),
            }
        },
        events: {
            'submit form': 'doSubmit',
            'click [obo-reload]': 'doReload'
        },
        afterRender: function() {
            var self = this;
            this.renderValidation();
            this.$('.page-content-holder .col-md-4').removeClass('col-md-offset-3').addClass('col-md-offset-4');
            this.$('.page-content-holder .form-signin').addClass('forgot-password');
        },
        renderValidation: function() {
          var self = this;

          this.$('[ehs-form]').bootstrapValidator()
            .on('success.form.bv', function(e) {
              e.preventDefault();
              self.doSubmit();
            });
        },
        doSubmit: function(e) {
            var self = this;
                var data = commonFunction.formDataToJson(this.$('form').serializeArray());

                $.ajax({
                    url: commonConfig.requestServer + 'api/Account/ResetPassword?userName=' + data.UserName,
                    method: 'POST',
                    success: function() {
                      console.log('sukses reset');
                      Backbone.history.navigate('login', true);
                        // self.$('[obo-content]').html(templateResetPasswordRequest({
                        //     UserName: data.UserName
                        // }));
                    },
                    error: function(xhr) {
                        self.$('[obo-content]').html(templateEmailAddressNotRegistered({
                            UserName: data.UserName,
                            message: xhr.responseText || 'something went wrong'
                        }));
                    }
                });
            return false;
        },
        doReload: function() {
            this.render();
        }
    });
});
