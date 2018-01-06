define(function(require, exports, module) {
    'use strict';

    var Model = require('backbone.model');

    module.exports = Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/CorrelatedProject',
        defaults: function() {
            return {
              Id : '',
              ProjectId : '',
              NamaProject : '',
              SektorId : '',
              NamaSektor : '',
              CreateBy : '',
              CreateDate : '',
              UpdateBy : '',
              UpdateDate : '',
              IsDelete : '',
              DeleteDate : '',
              Project: {
                Id: '',
                NamaProject: '',
                Sektor: {
                  Id : '',
                  NamaSektor : ''
                }
              }
            }
        }
    });
});
