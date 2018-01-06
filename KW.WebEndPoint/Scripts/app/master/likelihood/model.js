define(function(require, exports, module) {
    'use strict';

    var Model = require('backbone.model');

    module.exports = Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/Likehood',
        defaults: function() {
            return {
              NamaLikehood : '',
              DefinisiLikehood : '',
              Lower : '',
              Upper : '',
              Incres : '',
              Average : '',
              Status : '',
              CreateBy : '',
              CreateDate : '',
              UpdateBy : '',
              UpdateDate : '',
              IsDelete : '',
              DeleteDate : '',
              LikehoodDetail : {
                Id : '',
                DefinisiLikehood : '',
                Lower : '',
                Upper : '',
                Incres : '',
                Average : '',
              }
            }
        }
    });
});
