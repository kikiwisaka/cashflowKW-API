// login/app/reset_password
define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var commonConfig = require('commonconfig');
    var commonFunction = require('commonfunction');
    var Model = require('./model');
    var template = require('text!./template.html');
    var passValue = 6;

    var success = "success_check";
    var fail = "fail_exclamation";
    require('sweetalert');

    module.exports = LayoutManager.extend({
        className: 'container center-middle',
        template: _.template(template),
        initialize: function() {
            this.model = new Model();

            this.listenTo(this.model, 'request', function() {
                this.doLoadingDisabledButtonConfirm();
            });

            this.listenTo(this.model, 'sync error', function() {
                this.doEnabledButtonConfirm();
            });

            this.listenTo(this.model, 'sync', function(){
              swal({
              title: "Reset Password",
                  text: "Reset Password succeeded",
                  type: "info",
                  showCancelButton: false,
                  closeOnConfirm: true,
                  showLoaderOnConfirm: false
              },
              function() {
                  window.location.hash = 'login';
              });
            });

            this.listenTo(this.model, 'error', function() {
                this.doShowErrorMessage();
            });
        },
        afterRender: function() {
            this.checkNewPassword();
        },
        events: {
            'keyup [name="NewPassword"]': 'checkNewPassword',
            'change [name="NewPassword"]': 'checkNewPassword',
            'keyup [name="ConfirmPassword"]': 'checkNewPassword',
            'change [name="ConfirmPassword"]': 'checkNewPassword',
            'click [obo-reset]': 'doReset',
            'click [obo-confirm]': 'doConfirm'
        },
        checkNewPassword: function() {
            var self = this;
            var NewPassword = this.$('[name="NewPassword"]').val();

            var selectors = [{
                name: 'minimum8chars',
                value: (NewPassword.length > 7)
            }, {
                name: 'atleast1lochar',
                value: new RegExp("^(?=.*[a-z])").test(NewPassword)
            }, {
                name: 'atleast1upchar',
                value: new RegExp("^(?=.*[A-Z])").test(NewPassword)
            }, {
                name: 'atleast1numchar',
                value: new RegExp("^(?=.*\\d)").test(NewPassword)
            }, {
                name: 'atleast1nonalphanumchar',
                value: new RegExp("^(?=.*\\W)").test(NewPassword)
            }]

            this.passes = 0;
            _.each(selectors, function(selector) {
                var selectorName = '[obo-' + selector.name + ']';
                var selectorNameText = selectorName + ' + *';
                var className = selector.value ? success : fail;
                if (selector.value){
                    self.passes++;
                }

                self.$(selectorName).attr('class', '').addClass(className);
                self.$(selectorNameText)[(selector.value ? 'add' : 'remove') + 'Class']('text-muted');
            });

            this.checkConfirmPassword();
        },
        doReset: function(){
            this.$('form').get(0).reset();
            this.checkNewPassword();
        },
        checkConfirmPassword: function() {
            var NewPassword = this.$('[name="NewPassword"]').val();
            var ConfirmPassword = this.$('[name="ConfirmPassword"]').val();
            var isMatchpassword = (NewPassword == ConfirmPassword) && NewPassword.length && ConfirmPassword.length;

            this.$('[obo-passmustmatch]').attr('class', '').addClass(isMatchpassword ? success : fail);
            self.$('[obo-passmustmatch] + *')[(isMatchpassword ? 'add' : 'remove') + 'Class']('text-muted');

            if (isMatchpassword){
                this.passes++;
            }

            this.setConfirmButton();
        },
        setConfirmButton: function() {
            var isAllSuccess = this.passes == passValue;
            if (isAllSuccess) {
                this.$('[obo-confirm]').removeClass('disabled').removeAttr('disabled');
            } else {
                this.$('[obo-confirm]').addClass('disabled').attr('disabled', 'disabled');
            }
        },
        doConfirm: function() {
            if (this.passes != passValue){
                return false;
            }

            var data = _.extend({}, {
                Id: commonFunction.getUrlParameter('id'),
                UserName: commonFunction.getUrlParameter('username')
            }, commonFunction.formDataToJson(this.$('form').serializeArray()));

            this.model.save(data);
        },
        doShowErrorMessage: function() {
            // need to improve error message
            console.error(a);
        },
        doLoadingDisabledButtonConfirm: function() {
            this.$('[obo-confirm]').attr('disabled', 'disabled').text('Loading...');
        },
        doEnabledButtonConfirm: function() {
            this.$('[obo-confirm]').removeAttr('disabled').text('Confirm');
        }
    });
});
