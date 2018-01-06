define(function(require, exports, module) {
    'use strict';
    var View = require('modaldialogdefault');
    var template = require('text!./template.html');
    var Model = require('./../model');
    var ModelStageTahunRiskMatrix = require('./../edit/model');
    var CollectionStage = require('./../../master/stage/collection');
    var commonFunction = require('commonfunction');
    var commonConfig = require('commonconfig');
    var eventAggregator = require('eventaggregator');
    var moment = require('moment');
    var TableRisk = require('./../tabel_risk/table/table');
    var CollectionRisk = require('./../tabel_risk/collection');
    require('bootstrap-validator');

    module.exports = View.extend({
        template: _.template(template),
        initialize : function(){
          var self = this;
          var correlationId = this.model.get('Id');
          this.riskMatrixProjectId = this.model.get('Id');
          this.model = new Model();
          this.modelStageTahunRiskMatrix = new ModelStageTahunRiskMatrix();
          this.modelStageTahunRiskMatrix.set(this.modelStageTahunRiskMatrix.idAttribute, this.riskMatrixProjectId);
          this.collectionStage = new CollectionStage();
          this.tableRisk = new TableRisk({
              collection: new CollectionRisk()
          });
          this.model.set(this.model.idAttribute, correlationId);
          this.stageValue = {};
          this.dataStage = [];
          this.listenToOnce(this.model, 'sync', function(model) {
              this.render();
              var data = model.toJSON();
              this.listenTo(this.modelStageTahunRiskMatrix, 'sync', function() {
                commonFunction.responseSuccessUpdateAddDelete('Stage Tahun Risk Matrix Berhasil Di Perbarui.');
                self.$el.modal('hide');
                eventAggregator.trigger('risk_matrix/update:fecth');
              });
          }, this);

          this.modelStageTahunRiskMatrix.fetch({
                reset: true,
                data: {
                    id: this.riskMatrixProjectId
                }
            });

          this.once('afterRender', function() {
              this.model.fetch();
          });

          this.collectionStage.fetch({
          success: function(collection, response) {
              _.each(collection.models, function(model) {
              })
            }
          });
        },
        afterRender : function(){
          var self = this;
          this.formStage();
          this.setTemplate();
          var stage = this.getIdStage();
          for (var e = 0; e < stage.length; e++) {
            var stageId = stage[e];
            this.$('[name="data-start-stage-'+ stageId +'"]').datetimepicker({
              defaultDate: null,
              format: commonConfig.datePickerYearFormat
            });
            this.$('[name="data-end-stage-'+ stageId +'"]').datetimepicker({
              defaultDate: null,
              format: commonConfig.datePickerYearFormat
            });
          }
          this.renderRisk();
        },
        events: {
          'click [btn-save]': 'getConfirmation'
        },
        setTemplate: function() {
            var self = this;
            var data = this.modelStageTahunRiskMatrix.attributes.StageValue;
            if(data.length > 0) {
                $.each(data, function (index, item) {
                    var stageId = item.StageId;
                    self.$('[name="data-start-stage-'+ stageId +'"]').val(item.Values[0]);
                    self.$('[name="data-end-stage-'+ stageId +'"]').val(item.Values[1]);                    
                });
            }
        },
        formStage: function(data) {
          var self = this;
          if(this.collectionStage){
            for (var i = 0; i < this.collectionStage.length; i++){
              var namaStage = this.collectionStage.models[i].attributes.NamaStage;
              var idStage = this.collectionStage.models[i].attributes.Id;
              var html = '<div class="form-group">'
              html += '<label class="col-md-4 control-label">'+ namaStage +'</label>'
              html += '<div class="col-md-4">'
              html += '<input type="text" class="form-control datepicker" data-start-stage value="" stage-name-start="'+namaStage+'" name="data-start-stage-'+ idStage +'">'
              html += '</div>'
              html += '<div class="col-md-4">'
              html += '<input type="text" class="form-control datepicker" data-end-stage value="" stage-name-end="'+namaStage+'" name="data-end-stage-'+ idStage +'">'
              html += '<label data-id-stage class="hidden">'+ idStage +'</label>'
              html += '</div>'
              html += '</div>'
              self.$('[tab-content]').append(html);
            }
          }
        },
        getIdStage: function(){
            var idStage = [];
            this.$('[data-id-stage]').each(function(){
              var val = this.innerText;
              idStage.push(val);
            });
            return idStage;
        },
        getConfirmation: function(){
          //this.stageValidation();
          var action = "update";
          var retVal = confirm("Are you sure want to " + action + " Stage Year for this Risk Matrix?");
          if( retVal == true ){
             this.saveStage();
          }
          else{
            this.$('[btn-save"]').attr('disabled', false);
          }
        },
        saveStage: function() {
            var self = this;
            var stage = this.getIdStage();
            var data = {};
            var stageValue = [];

            for (var e = 0; e < stage.length; e++) {
              var stageValueItem = {};
              var stageId = stage[e];
              stageValueItem.StageId = stageId;
              
              var value = [];
              var startStageValue = self.$('[name="data-start-stage-'+ stageId +'"]').val();
              value.push(startStageValue);
              var endStageValue = self.$('[name="data-end-stage-'+ stageId +'"]').val();
              value.push(endStageValue);

              stageValueItem.Values = value;
              stageValue.push(stageValueItem);
            }
            
            data.StageValue = stageValue;

            var startProject = self.$('[name="StartProject"]').val();
            var endProject = self.$('[name="EndProject"]').val();
            data.StartProject = startProject;
            data.EndProject = endProject;
            var riskMatrixProjectId = this.riskMatrixProjectId;
            data.RiskMatrixProjectId = riskMatrixProjectId;

            this.modelStageTahunRiskMatrix.save(data);
        },
        renderRisk: function(){
                this.$('[obo-table-risk]').append(this.tableRisk.el);
                this.tableRisk.render();
                this.tableRisk.collection.fetch({
                    reset: true,
                    data: {
                        PageSize: 100
                    }
                });
        },
        stageValidation: function() {
          var self = this;
          var start = this.model.get('Project.TahunAwalProject');
          var end = this.model.get('Project.TahunAkhirProject');
          var startProject = moment(start).format('YYYY');
          var endProject = moment(end).format('YYYY');
          var stage = this.modelStageTahunRiskMatrix.attributes.StageValue;
          var currentStageName = null;
          var endLatestStage = null;
          var stageRow = 0;
          if(stage.length > 0) {
            $.each(stage, function (index, item) {
              var stageId = item.StageId;
              var stageStart = self.$('[name="data-start-stage-'+ stageId +'"]').val();
              var stageEnd = self.$('[name="data-end-stage-'+ stageId +'"]').val();
              var stageName = self.$('[data-end-stage]').attr('stage-name-end');
              currentStageName = stageName;      
              debugger;
              if(stageStart < startProject) {
                commonFunction.responseWarningCannotExecute(''+ stageName +' start year less than year of project start.');
              } else if(stageEnd > endProject) {
                commonFunction.responseWarningCannotExecute(''+ stageName +' end year more than year of project end.');
              } else if (stageStart > stageEnd) {
                commonFunction.responseWarningCannotExecute(''+ stageName +' start year more than end year.');
              } else if (endLatestStage != null && currentStageName != null && stageRow != 0) {
                if(stageStart <= endLatestStage) {
                  commonFunction.responseWarningCannotExecute(''+ stageName +' start year less than end year of '+ currentStageName +'.');
                } else if (endLatestStage > stageEnd) {
                  var dataLength = stageEnd - endLatestStage;
                  if(dataLength > 1) {
                    commonFunction.responseWarningCannotExecute(''+ stageName +' start year more than one from end year of '+ currentStageName +'.');
                  }
                }             
              }
              stageRow += 1;
              endLatestStage = stageEnd;
              console.log(stageRow);
            });
          }
        },
    });
});
