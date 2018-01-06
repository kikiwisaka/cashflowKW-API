define(function(require, exports, module) {
    'use strict';

    var Model = require('backbone.model');

    module.exports = Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/Scenario',
        defaults: function() {
            return {
              NamaScenario : '',
              LikehoodId : '',
              NamaLikehood : '',
              IsDefault : '',
              Status : '',
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
