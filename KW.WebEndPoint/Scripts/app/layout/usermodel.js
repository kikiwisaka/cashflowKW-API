define(function (require, exports, module) {
    'use strict';
    require('backbone');

    var Model = require('backbone.model');
    var commonConfig = require('commonconfig');
    var commonFunction = require('commonfunction');
    var eventAggregator = require('eventaggregator');
    var userModelData = {};

    eventAggregator.off('layout:usermodel');
    eventAggregator.on('layout:usermodel:getData', function (fn) {
        fn(userModelData);
    });

    var url = commonConfig.useFakeServer ? 'account/user.json' : 'account/userInfo';
    url = commonFunction.getDomain() + url;

    module.exports = Model.extend({
        url: url,
        initialize: function () {
            var self = this;
            this.on('sync', function() {
                userModelData = this.toJSON();
				eventAggregator.trigger('layout:usermodel:sync', userModelData);
            });
            eventAggregator.on('layout:usermodel:getThis', function (fn) {
                fn(self);
            });
        }
    });
});
