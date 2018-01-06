define(function(require, exports, module) {
    'use strict';

    var Model = require('backbone.model');

    module.exports = Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/Project',
        defaults: function() {
            return {
              Id : '',
              NamaProject : '',
              TahunAwalProject : '',
              TahunAkhirProject : '',
              StatusProject : '',
              Minimum : '',
              Maximum : '',
              Keterangan : '',
              TahapanId : '',
              SektorId : '',
              CreateBy : '',
              CreateDate : '',
              UpdateBy : '',
              UpdateDate : '',
              IsDelete : '',
              DeleteDate : '',
              RiskRegistrasi : {
                Id : '',
                KodeMRisk : '',
                NamaCategoryRisk : '',
              }
            }
        }
    });
});
