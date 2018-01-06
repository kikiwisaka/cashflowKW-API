define(function(require, exports, module) {
    'use strict';

    var Model = require('backbone.model');

    module.exports = Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/SubRiskRegistrasi',
        defaults: function() {
            return {
              MRiskId : '',
              KodeRisk : '',
              RiskEvenClaim : '',
              DescriptionRiskEvenClaim : '',
              SugestionMigration : '',
              CreateBy : '',
              CreateDate : '',
              UpdateBy : '',
              UpdateDate : '',
              IsDelete : '',
              DeleteDate : ''
            }
        }
    });
});
