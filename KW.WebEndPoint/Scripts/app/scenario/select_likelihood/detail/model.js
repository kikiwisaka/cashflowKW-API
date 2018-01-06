define(function(require, exports, module) {
    'use strict';

    var Model = require('backbone.model');

    module.exports = Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/LikehoodDetail',
        defaults: function() {
            return {
              DefinisiLikehood : '',
              NamaLikehood:'',
              Lower : '',
              Upper : '',
              Average : '',
              Incres : '',
              Status : '',
              LikehoodId : '',
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
