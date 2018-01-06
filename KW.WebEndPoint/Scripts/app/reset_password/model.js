// login/app/reset_password
define(function(require, exports, module) {
    'use strict';
    var Model = require('backbone.model');

    module.exports = Model.extend({
        urlRoot: 'Account/PasswordReset'
    });
});
