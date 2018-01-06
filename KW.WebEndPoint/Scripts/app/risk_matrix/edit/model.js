define(function(require, exports, module) {
    'use strict';

    var Model = require('backbone.model');

    module.exports = Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/StageTahunRiskMatrix',
        defaults: function() {
            return {
              RiskMatrixProjectId : '',
              Tahun : '',
              StageId : '',
              CreateBy : '',
              CreateDate : '',
              UpdateBy : '',
              UpdateDate : '',
              IsUpdate : '',
              IsDelete : '',
              DeleteDate : '',
              StageValue: [
                {
                   StageId: '',
                   Values: ['', '']
                }
              ],
              RiskMatrixProject: {
                Id: '',
                ProjectId: '',
                RiskMatrixId: '',
                ScenarioId: '',
                CreateBy: '',
                CreateDate: '',
                UpdateBy: '',
                UpdateDate: '',
                IsDelete: '',
                DeleteDate: '',
                Project: {
                    Id: '',
                    NamaProject: '',
                    TahunAwalProject: '',
                    TahunAkhirProject: ''
                  }
                }
            }
        }
    });
});
