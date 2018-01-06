define(function(require, exports, module) {
    'use strict';

    var Model = require('backbone.model');

    module.exports = Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/RiskMatrixProject',
        defaults: function() {
            return {
              ProjectId: '',
              NamaProject: '',
              RiskMatrixId: '',
              ScenarioId: '',
              NamaProject:'',
              CreateBy : '',
              CreateDate : '',
              UpdateBy : '',
              UpdateDate : '',
              IsDelete : '',
              DeleteDate : '',
              Project : {
                  Id: '',
                  NamaProject: '',
                  NamaSektor: '',
                  ProjectRiskRegistrasi: {
                    ProjectId: '',
                    RiskRegistrasi: {
                      Id: '',
                      KodeMRisk: '',
                      NamaCategoryRisk: '',
                      Definisi: '',
                    },
                  },
                  RiskRegistrasi: {
                    KodeMRisk: '',
                    NamaCategoryRisk: '',
                    Definisi: '',
                  },
                  ProjectRiskStatus: {
                    Id: '',
                    KodeMRisk: '',
                    NamaCategoryRisk: '',
                    Definisi: '',
                    IsProjectUsed: ''
                  }
              },
              RiskRegistrasi : {
                Id: '',
                KodeMRisk: '',
                NamaCategoryRisk: '',
                Definisi: '',
              },
              Scenario: {
                Id: '',
                NamaScenario: '',
                LikehoodId: '',
                NamaLikehood: '',
                IsDefault: '',
                Likehood: {
                  Id: '',
                  NamaLikehood: '',
                  Incres: '',
                  Status: '',
                  Status: '',
                  LikehoodDetail: {
                    Id: '',
                    DefinisiLikehood: '',
                    Lower: '',
                    Upper: '',
                    Average: ''
                  }
                }
              },
              StageTahunRiskMatrix: {
                Id: '',
                Tahun: '',
                StageId: '',
                IsUpdate: ''
              }
            }
          }
        });
});
