define(function(require, exports, module) {
    'use strict';
    var Backbone = require('backbone');
    var commonConfig = require('commonconfig');

    // module.exports = Backbone.Model.extend({
    //     idAttribute: 'Id',
    //     urlRoot:'api/Authentication',
    //     defaults: {
    //         username: '',
    //         password: ''
    //     }
    // });

    module.exports = Backbone.Model.extend({
        idAttribute: 'access_token',
        urlRoot:'token',
        defaults: {
            grant_type: 'password',
            username: '',
            password: ''
        }
    });
});
