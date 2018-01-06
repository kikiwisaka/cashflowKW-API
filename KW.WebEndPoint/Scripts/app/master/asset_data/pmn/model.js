define(function(require, exports, module) {
    'use strict';

    var Model = require('backbone.model');

    module.exports = Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/PMN',
        defaults: function() {
            return {
              PMNToModalDasarCap : '',
              RecourseDelay : '',
              DelayYears : '',
              OpexGrowth : '',
              Opex: '',
              CreateBy : '',
              CreateDate : '',
              UpdateBy : '',
              UpdateDate : '',
              IsDelete : '',
              DeleteDate : '',
              Status : '',
              ValuePMNToModalDasarCap: ''
            }
        }
    });
});
