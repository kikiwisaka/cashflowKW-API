// login/app/login
define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var commonConfig = require('commonconfig');
    var commonFunction = require('commonfunction');
    var Model = require('./model');
    var template = require('text!./template.html');
    var templateTitle = require('text!./title.html');
    var templateContent = require('text!./content.html');
    var Cookies = require('Cookies');

    module.exports = LayoutManager.extend({
        className: 'page-container',
        template: _.template(template),
        initialize: function() {
            this.model = new Model();
            this.listenTo(this.model, 'request', function() {
                this.doLoadingDisabledButtonLogin();
                this.doHideErrorMessage();
            });

            this.listenTo(this.model, 'sync error', function() {
                this.doEnabledButtonLogin();
            });

            this.listenTo(this.model, 'sync', function() {
                this.doHideErrorMessage();
                console.log(this.model.toJSON());
                document.cookie = "Authorization=" + 'bearer ' + this.model.get('access_token') + "" + ";domain="+commonConfig.requestDomain+";path=/";
                document.cookie = "roleName=" + this.model.get('role') + "" + ";domain="+commonConfig.requestDomain+";path=/";
                document.cookie = "userName=" + this.model.get('username') + "" + ";domain="+commonConfig.requestDomain+";path=/";
                document.cookie = "firstName=" + this.model.get('firstName') + "" + ";domain="+commonConfig.requestDomain+";path=/";
                window.Router.navigate('', {trigger: true});
            });

            this.listenTo(this.model, 'error', function(xhr, text) {
                console.log(text);
                this.doShowErrorMessage(text);

            });
        },
        serialize: function(){
            return {
                title: templateTitle,
                content: templateContent
            }
        },
        events: {
            'submit form': 'doSubmit',
            'click [obo-doReset]': 'doReset'
        },
        doSubmit: function(){
          console.log(this.$('[name="username"]').val());
            this.model.save({
                username: this.$('[name="username"]').val(),
                password: this.$('[name="password"]').val()
            });
            console.log('logged');
            return false;
        },
        doHideErrorMessage: function() {
            this.$('[obo-errormessage]').hide();
        },
        doShowErrorMessage: function(text) {
            this.$('[obo-name="errorMessage"]').html( ( text.responseJSON && text.responseJSON.error_description) || 'something went wrong on server');
            this.$('[obo-errormessage]').show();
        },
        doLoadingDisabledButtonLogin: function() {
            this.$('[obo-dologin]').attr('disabled', 'disabled').text('Loading...');
        },
        doEnabledButtonLogin: function() {
            this.$('[obo-dologin]').removeAttr('disabled').text('Login');
        }
    });
});
