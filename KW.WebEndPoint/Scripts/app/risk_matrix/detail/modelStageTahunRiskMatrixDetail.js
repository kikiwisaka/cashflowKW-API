define(function(require, exports, module) {
    'use strict';

    var Model = require('backbone.model');

    module.exports = Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/StageTahunRiskMatrixDetail',
        defaults: function() {
            return {
              
             
            }
          }
        });
});
