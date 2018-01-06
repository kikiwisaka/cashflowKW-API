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
    var TableRisk = require('./../tabel_risk/table/table');
    var CollectionRisk = require('./../tabel_risk/collection');
    require('bootstrap-validator');

    module.exports = View.extend({
        template: _.template(template),
        initialize : function(){
          var self = this;
          var correlationId = this.model.get('Id');
          this.riskMatrixProjectId = this.model.get('Id');
          console.log(this.riskMatrixProjectId);
          this.model = new Model();
          this.modelStageTahunRiskMatrix = new ModelStageTahunRiskMatrix();
          this.collectionStage = new CollectionStage();
          this.tableRisk = new TableRisk({
                collection: new CollectionRisk()
            });
          this.model.set(this.model.idAttribute, correlationId);
          this.stageValue = {};
          this.dataStage = [];
          this.listenToOnce(this.model, 'sync', function(model) {
            this.render();
          }, this);
          this.listenToOnce(this.modelStageTahunRiskMatrix, 'sync', function(model) {
            self.$el.modal('hide');
            Router.navigate("/risk_matrix/" + this.riskMatrixProjectId, { trigger: true });
          }, this);
          this.once('afterRender', function() {
              this.model.fetch();
          });

          this.collectionStage.fetch();
        },
        afterRender : function(){
          var self = this;
          this.formStage();
          var stage = this.getIdStage();
            for (var e = 0; e < stage.length; e++) {
                console.log('hai');
                var stageId = stage[e];
                this.$('[name="data-start-stage-'+ stageId +'"]').datetimepicker({defaultDate: null,format: commonConfig.datePickerYearFormat});
                this.$('[name="data-end-stage-'+ stageId +'"]').datetimepicker({defaultDate: null,format: commonConfig.datePickerYearFormat});
                this.$('[name="data-start-stage-'+ stageId +'"]').attr("idstart",[e]);
                this.$('[name="data-end-stage-'+ stageId +'"]').attr("idend",[e]);
            }
            this.renderRisk();
        },
        events: {
          'dp.change [data-start-stage]': 'validatorTahun',
          'click [btn-save]': 'saveStage',
          
        },
        validatorTahun: function() {
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
                html += '<input type="text" class="form-control datepicker" data-start-stage value="" name="data-start-stage-'+ idStage +'">'
                html += '</div>'
                html += '<div class="col-md-4">'
                html += '<input type="text" class="form-control datepicker" data-end-stage value="" name="data-end-stage-'+ idStage +'">'
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
        renderValidation: function() {
          var self = this;
          this.$('[ehs-form]').bootstrapValidator({
              fields: {
                
              }
            })
            .on('success.form.bv', function(e) {
              e.preventDefault();
              self.doSave();
            });
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
        doSave: function() {
          this.saveStage();
        }
    });
});
