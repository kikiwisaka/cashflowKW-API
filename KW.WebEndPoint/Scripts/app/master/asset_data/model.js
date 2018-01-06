define(function(require, exports, module) {
    'use strict';

    var Model = require('backbone.model');

    module.exports = Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/AssetData',
        defaults: function() {
            return {
              //Id : '',
              AssetClass : '',
              TermAwal : '',
              TermAkhir : '',
              AssumentReturn : '',
              OutstandingStartYears : '',
              OutstandingEndYears : '',
              AssetValue : '',
              Porpotion : '',
              AssumedReturnPercentage : '',
              AssumedReturn : '',
              CreateBy : '',
              CreateDate : '',
              UpdateBy : '',
              UpdateDate : '',
              IsDelete : '',
              DeleteDate : '',
              Status : ''

            }
        }
    });
});
