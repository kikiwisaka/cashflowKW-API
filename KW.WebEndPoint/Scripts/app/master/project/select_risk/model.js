define(function(require, exports, module) {
    'use strict';

    var Model = require('backbone.model');

    module.exports = Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/RiskRegistrasi',
        defaults: function() {
            return {
              Id : '',
              KodeMRisk : '',
              NamaCategoryRisk : '',
              Definisi : ''
            }
        }
    });
});
