define(function(require, exports, module) {
    'use strict';

    var Model = require('backbone.model');

    module.exports = Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/FunctionalRisk',
        defaults: function() {
            return {
              NamaMatrix : '',
              NamaFormula : '',
              MatrixId : '',
              ColorCommentId : '',
              ScenarioId : '',
              Definisi : '',
              CreateBy : '',
              CreateDate : '',
              UpdateBy : '',
              UpdateDate : '',
              IsDelete : '',
              DeleteDate : '',
            }
        }
    });
});
