define(function(require, exports, module) {
    'use strict';

    var commonConfig = require('commonconfig');
    var Cookies = require('Cookies');

    Backbone.ajax = function() {
        var headers = arguments[0].headers;
        var useThisUrl = arguments[0].useThisUrl;
        if (arguments[0]) {
            var authorization = Cookies.get(commonConfig.cookieFields.Authorization);

            if (authorization) {
                arguments[0].headers = _.extend({}, headers, {
                    'Authorization': authorization
                });
            }
            var url = arguments[0].url;
            if (url && url.match(/\bdummydata/gi) && url.match(/\bdummydata/gi).length) {
                arguments[0].url = '/Scripts/' + arguments[0].url;
                if (!/.json$/.test(arguments[0].url)) {
                    arguments[0].url += '.json';
                }
            } else if (useThisUrl) {
                arguments[0].url = useThisUrl;
            } else {
                arguments[0].url = commonConfig.requestServer + (arguments[0].url || '');
            }
        }
        return Backbone.$.ajax.apply(Backbone.$, arguments);
    };

    var execute = Backbone.Router.prototype.execute;

    Backbone.Router.prototype.execute = function(callback, args, name) {
        var self = this;
        var isAuthorized = function() {
            execute.call(self, callback, args, name);
        }
        requirejs(['commonfunction'], function(commonFunction) {
            commonFunction.checkCookieAuthorization().then(isAuthorized, function() {});
        })
    };

    var remove = Backbone.View.prototype.remove;

    Backbone.View.prototype.remove = function() {
        this.trigger('beforeRemove');
        remove.apply(this, arguments);
        this.isRemoved = true;
        this.trigger('afterRemove');
    }
    module.exports = Backbone;
});
