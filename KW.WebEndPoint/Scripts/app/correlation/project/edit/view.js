define(function(require, exports, module) {
	'use strict';
    var LayoutManager = require('layoutmanager');
    var template = require('text!./template.html');
    var commonFunction = require('commonfunction');
    var eventAggregator = require('eventaggregator');
    var Model = require('./../model');
    var ModelDetail = require('./model');

    module.exports = LayoutManager.extend({
    	className: 'container-fluid main-content tbl bg-white',
        template: _.template(template),
        initialize: function(options) {
        	var self = this;
        	this.correlatedProjectId = commonFunction.getUrlHashSplit(3);
            this.model = new Model();
            this.modelDetail = new ModelDetail();

            this.listenTo(this.model, 'sync', () => {
                 this.listenToOnce(this.modelDetail, 'sync', function(model) {
                    commonFunction.responseSuccessUpdateAddDelete('Correlation Risk Antar Project berhasil tersimpan.');
                }, this);
                this.render();
            });
            this.model.fetch({
                reset: true,
                data: {
                    id: this.correlatedProjectId
                }
            });
            this.modelDetail.fetch({
                reset: true,
                data: {
                    id: this.correlatedProjectId
                }
            });
        },
        events: {
            'change [cormat]': 'setCorrelationMatrix',
            'click [btn-save]': 'getConfirmation'
        },
        afterRender: function() {
        	var self = this;
            this.project = this.model.get('Project');
            this.setValue();
        },
        setValue: function() {
            var self = this;
            var data = this.modelDetail.toJSON();
            if(data.CorrelatedProjectDetailCollection) {
                for (var i = 0; i < data.CorrelatedProjectDetailCollection.length; i++)
                {
                    var dataDetail = data.CorrelatedProjectDetailCollection[i];
                    var projectiId = dataDetail.ProjectiId;
                    var projectValues = dataDetail.CorrelatedProjectMatrixValues;
                    if(projectValues.length > 0) {
                        for (var c = 0; c < projectValues.length; c++)
                        {
                            var cormatValue = projectValues[c].CorrelationMatrixId;
                            var row = projectValues[c].ProjectIdRow;
                            var col = projectValues[c].ProjectIdCol;

                            this.$('[data-cormat="'+ row +'-'+ col +'"]').val(cormatValue);
                        }
                    }
                }
            }
        },
        setCorrelationMatrix: function(e) {
            var self = this;
            var attributeValue = e.target.getAttribute('data-cormat');
            var cormatValue = this.$('[data-cormat="'+ attributeValue +'"]').val();

            var row = attributeValue.substring(0, attributeValue.indexOf('-'));
            var col =  attributeValue.substring(attributeValue.indexOf('-')+1);

            this.$('[data-cormat="'+ col +'-'+ row +'"]').val(cormatValue);
        },
        getConfirmation: function(){
          var action = "save";
          var retVal = confirm("Are you sure want to " + action + " Correlation Risk Antar Project ?");
          if( retVal == true ){
             this.doSave();
          }
          else{
            //this.$('[type="submit"]').attr('disabled', false);
          }
        },
        doSave: function() {
            var param = {};
            var paramCollection = [];

            for (var i = 0; i < this.project.length; i++) {
                var correlatedSektorDetailCollection = [];
                var projectValues = [];
                var paramDetail = {};

                for(var r = 0; r < this.project.length; r++) {
                    var paramDetailValue = {};
                    var row = this.project[i].Id;
                    var col = this.project[r].Id;
                    var cormatValue = this.$('[data-cormat="'+ row +'-'+ col +'"]').val();

                    paramDetailValue.ProjectIdRow = row;
                    paramDetailValue.ProjectIdCol = col;
                    paramDetailValue.CorrelationMatrixId = parseInt(cormatValue);

                    projectValues.push(paramDetailValue);
                }

                correlatedSektorDetailCollection.push(projectValues);
                paramDetail.ProjectId = this.project[i].Id;
                paramDetail.CorrelatedProjectMatrixValues = projectValues;

                param.CorrelatedProjectId = parseInt(this.correlatedProjectId);
                paramCollection.push(paramDetail);
                param.CorrelatedProjectDetailCollection = paramCollection;
            }
            this.modelDetail.save(param);
        }
    });
});