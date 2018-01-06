define(function(require, exports, module) {
    'use strict';

    var Model = require('backbone.model');

    module.exports = Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/CorrelatedSektor',
        defaults: function() {
            return {
              NamaSektor : '',
              CreateBy : '',
              CreateDate : '',
              UpdateBy : '',
              UpdateDate : '',
              IsDelete : '',
              DeleteDate : '',
              CorrelationMatrix: {
                Id: '',
                NamaCorrelationMatrix: '',
                Nilai: ''
              },
              RiskRegistrasi: {
                Id: '',
                KodeMRisk: '',
                NamaCategoryRisk: '',
                Definisi: ''
              }
            }
        }
    });
});
