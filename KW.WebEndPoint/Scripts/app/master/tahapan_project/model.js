define(function(require, exports, module) {
    'use strict';

    var Model = require('backbone.model');

    module.exports = Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/Tahapan',
        defaults: function() {
            return {
              //Id : '',
              NamaTahapan : '',
              Keterangan : '',
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
