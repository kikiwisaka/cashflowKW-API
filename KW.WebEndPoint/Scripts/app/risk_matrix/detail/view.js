define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var template = require('text!./../detail/template.html');
    var commonFunction = require('commonfunction');
    var eventAggregator = require('eventaggregator');
    var Table = require('./table/table');
    var Paging = require('paging');
    var Model = require('./../model');
    var TableLikelihood = require('./likelihood/detail/table/table');
    var CollectionLikelihood = require('./likelihood/detail/collection');
    var CollectionStage = require('./../../master/stage/collection');
    var ModelStageTahunRiskMatrix = require('./../edit/model');
    var ModelRisk = require('./../../master/risk/model');
    var CollectionRisk = require('./../../master/risk/collection');
    var ModelStageTahunRiskMatrixDetail = require('./model');

    module.exports = LayoutManager.extend({
        className: 'container-fluid main-content tbl bg-white',
        template: _.template(template),
        initialize: function(options) {
            var self = this;
            var parentId = commonFunction.getUrlHashSplit(2);
            this.parentId = parentId;
            this.modelStageTahunRiskMatrixDetail = new ModelStageTahunRiskMatrixDetail();
            //this.modelStageTahunRiskMatrixDetail.set(this.modelStageTahunRiskMatrixDetail.idAttribute, this.parentId);
            this.model = new Model();
            this.modelStageTahunRiskMatrix = new ModelStageTahunRiskMatrix();
            this.modelStageTahunRiskMatrix.set(this.modelStageTahunRiskMatrix.idAttribute, this.parentId);
            this.collectionStage = new CollectionStage();

            this.tableLikelihood = new TableLikelihood({
                collection: new CollectionLikelihood()
            });
            this.collectionRisk = new CollectionRisk({
                collection: new CollectionRisk()
            });
            this.listenTo(this.model, 'sync', () => {
                this.render();
            });

            this.listenToOnce(this.modelStageTahunRiskMatrixDetail, 'sync', function(model) {
                console.log('lto');
                this.listenTo(this.modelStageTahunRiskMatrixDetail, 'sync', function(model) {
                    commonFunction.responseSuccessUpdateAddDelete('Risk Matrix Berhasil Disimpan.');
                });
            }, this);

            this.modelStageTahunRiskMatrixDetail.fetch({
                reset: true,
                data: {
                    id: this.parentId
                }
            });

            this.modelStageTahunRiskMatrix.fetch({
                reset: true,
                data: {
                    id: this.parentId
                }
            });

            this.collectionStage.fetch({
                success: function(collection, response) {
                    _.each(collection.models, function(model) {
                        console.log(model.toJSON());
                    })
                }
            });

            this.once('afterRender', () => {
                this.model.set(this.model.idAttribute, this.parentId);
                this.model.fetch();

                this.table.collection.fetch({
                    reset: true,
                    data: {
                        ParentId: this.parentId
                    }
                });
            });


            this.table = new Table();
        },
        afterRender: function() {
            if (!this.model.id) {
                return;
            }
            this.formStage();
            this.renderLikelihood();
            var likelihoodId = this.model && this.model.attributes && this.model.attributes.Scenario.LikehoodId;
            this.renderRiskCategory();
            this.renderStage();
            // this.setTemplate();
            this.setTemplateStage();
        },
        events: {
            'click [btn-save]'  : 'getConfirmation'
        },
        renderStage: function() {
            var self = this;
            var project = this.model.attributes.Project.NamaProject;
            var startProject = this.model.attributes.Project.TahunAwalProject;
            var endProject = this.model.attributes.Project.TahunAkhirProject;
            this.$('[name="NamaProject"]').val(project);
            this.$('[name="TahunAwalProject"]').val(startProject);
            this.$('[name="TahunAkhirProject"]').val(endProject);
        },
       
        renderLikelihood: function(){
            var likelihoodId = this.model.attributes.Scenario.LikehoodId;
            if(likelihoodId) {
                this.$('[obo-table-likelihood]').append(this.tableLikelihood.el);
                this.tableLikelihood.render();
                this.tableLikelihood.collection.fetch({
                    reset: true,
                    data: {
                        ParentId: likelihoodId,
                        PageSize: 100
                    }
                });
            }
        },
        renderRiskCategory: function() {
            this.collectionRisk.fetch({
                reset: true,
                data: {
                    IsPagination: false
                }
            });
        },

        setTemplateStage: function() {
            var self = this;
            var data = this.modelStageTahunRiskMatrix.attributes.StageValue;
            if(data.length > 0) {
                $.each(data, function (index, item) {
                    var stageId = item.StageId;
                    self.$('[name="data-start-stage-'+ stageId +'"]').text(item.Values[0]);
                    self.$('[name="data-end-stage-'+ stageId +'"]').text(item.Values[1]);                    
                });
            }
        },

        // setTemplate: function() {
        //     var self = this;
        //     var data = this.modelStageTahunRiskMatrixDetail.attributes.RiskMatrixCollection;
        //     if(data.length > 0) {
        //         $.each(data, function (index, value) {
        //             var yearId = value.StageTahunRiskMatrixId;
        //             $.each(value.RiskMatrixValue, function(index, item) {
        //                 var riskId = item.RiskRegistrasiId;
        //                 var exp = item.Values[0];
        //                 var likehood = item.Values[1];
        //                 self.$('[data-risk="'+ riskId +'-ForYear-'+yearId+'"]').val(item.Values[0]);
        //                 self.$('[name="DefinisiLikehoodFor-'+ yearId +'-RiskFor-'+ riskId +'"]').val(likehood);
        //             });
        //         });
        //     }
            
        // },
        getRiskRegistrasi: function(){
            var riskRegistrasi = [];
            this.$('[data-risk-registrasi-id]').each(function(){
                var val = this.value;
                riskRegistrasi.push(val);
            });
            return riskRegistrasi;
        },
        getStageTahunRiskMatrix: function() {
            var years = [];
            this.$('[data-year-id]').each(function(){
                var val = this.innerText;
                var id = val.trim();
                years.push(id);
            });
            return years;
        },
        getExposureValue: function(dom) {

        },
        formStage: function(data) {
          var self = this;
        if(this.collectionStage){
            for (var i = 0; i < this.collectionStage.length; i++){
                var namaStage = this.collectionStage.models[i].attributes.NamaStage;
                var idStage = this.collectionStage.models[i].attributes.Id;
                var html = '<tr>'
                html += '<td style="width: 10%;">'
                html += '<label>'+ namaStage +'</label>'
                html += '</td>'
                html += '<td style="width: 10%;">'
                html += '<label>=</label>'
                html += '</td>'
                html += '<td style="width: 10%;">'
                html += '<label name="data-start-stage-'+ idStage +'"></label>'
                html += '</td>'
                html += '<td style="width: 10%;">'
                html += '<label>s/d</label>'
                html += '</td>'
                html += '<td style="width: 10%;">'
                html += '<label name="data-start-stage-'+ idStage +'"></label>'
                html += '</td>'
                html += '</tr>'
                self.$('[tab-content]').append(html);
              }
            }
        },
        getConfirmation: function(){
          var action = "save";
          var retVal = confirm("Are you sure want to " + action + " this data ?");
          if( retVal == true ){
             this.saveRiskMatrix();
          }
          else{
            this.$('[btn-save]').attr('disabled', false);
          }
        },
        saveRiskMatrix: function() {
            var self = this;
            var stageTahunRiskMatrixIds = this.getStageTahunRiskMatrix();
            var riskRegistrasi = this.getRiskRegistrasi();
            var data = {};
            var riskMatrixCollection = [];
            for (var i = 0; i < stageTahunRiskMatrixIds.length; i++) {
                var stageTahunRiskMatrix = {};
                var year = stageTahunRiskMatrixIds[i];
                var riskMatrixValue = [];

                stageTahunRiskMatrix.StageTahunRiskMatrixId = year;
                for (var e = 0; e < riskRegistrasi.length; e++) {
                    console.log('hai');
                    var riskMatrixValueItem = {};
                    var riskId = riskRegistrasi[e];
                    riskMatrixValueItem.RiskRegistrasiId = riskId;
                    
                    var value = [];
                    var exposureValue = self.$('[data-risk="'+ riskId +'-ForYear-'+ year +'"]').val();
                    value.push(exposureValue);
                    var likelihoodDetailId = self.$('[name="DefinisiLikehoodFor-'+ year +'-RiskFor-'+ riskId +'"]').val();
                    value.push(likelihoodDetailId);

                    riskMatrixValueItem.Values = value;

                    riskMatrixValue.push(riskMatrixValueItem);
                }
                stageTahunRiskMatrix.RiskMatrixValue = riskMatrixValue;
                riskMatrixCollection.push(stageTahunRiskMatrix);
            }
            data.RiskMatrixCollection = riskMatrixCollection;
            data.Id = this.parentId;
            this.modelStageTahunRiskMatrixDetail.save(data);
        }
    });
});