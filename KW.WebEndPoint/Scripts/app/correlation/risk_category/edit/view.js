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
        	this.correlatedSektorId = commonFunction.getUrlHashSplit(3);
            this.model = new Model();
            this.modelDetail = new ModelDetail();

            this.listenTo(this.model, 'sync', () => {
                 this.listenToOnce(this.modelDetail, 'sync', function(model) {
                    commonFunction.responseSuccessUpdateAddDelete('Correlation Risk Antar Sektor berhasil tersimpan.');
                }, this);
                this.render();
            });
            this.model.fetch({
                reset: true,
                data: {
                    id: this.correlatedSektorId
                }
            });
            this.modelDetail.fetch({
                reset: true,
                data: {
                    id: this.correlatedSektorId
                }
            });
           
        },
        events: {
            'change [cormat]': 'setCorrelationMatrix',
            'click [btn-save]': 'getConfirmation'
        },
        afterRender: function() {
        	var self = this;
            this.riskRegistrasi = this.model.get('RiskRegistrasi');
            this.setValue();
        },
        setValue: function() {
            var self = this;
            var data = this.modelDetail.toJSON();
            if(data.CorrelatedSektorDetailCollection) {
                for (var i = 0; i < data.CorrelatedSektorDetailCollection.length; i++)
                {
                    var dataDetail = data.CorrelatedSektorDetailCollection[i];
                    var riskRegistrasiId = dataDetail.RiskRegistrasiId;
                    var riskRegistrasiValues = dataDetail.RiskRegistrasiValues;
                    if(riskRegistrasiValues.length > 0) {
                        for (var c = 0; c < riskRegistrasiValues.length; c++)
                        {
                            var cormatValue = riskRegistrasiValues[c].CorrelationMatrixId;
                            var row = riskRegistrasiValues[c].RiskRegistrasiIdRow;
                            var col = riskRegistrasiValues[c].RiskRegistrasiIdCol;

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
          var retVal = confirm("Are you sure want to " + action + " Correlation Risk Antar Sektor ?");
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

            for (var i = 0; i < this.riskRegistrasi.length; i++) {
                var correlatedSektorDetailCollection = [];
                var risRegistrasiValues = [];
                var paramDetail = {};

                for(var r = 0; r < this.riskRegistrasi.length; r++) {
                    var paramDetailValue = {};
                    var row = this.riskRegistrasi[i].Id;
                    var col = this.riskRegistrasi[r].Id;
                    var cormatValue = this.$('[data-cormat="'+ row +'-'+ col +'"]').val();

                    paramDetailValue.RiskRegistrasiIdRow = row;
                    paramDetailValue.RiskRegistrasiIdCol = col;
                    paramDetailValue.CorrelationMatrixId = parseInt(cormatValue);

                    risRegistrasiValues.push(paramDetailValue);
                }

                correlatedSektorDetailCollection.push(risRegistrasiValues);
                paramDetail.RiskRegistrasiId = this.riskRegistrasi[i].Id;
                paramDetail.RiskRegistrasiValues = risRegistrasiValues;
                //paramDetail.RiskRegistrasiValues = correlatedSektorDetailCollection;

                param.CorrelatedSektorId = parseInt(this.correlatedSektorId);
                paramCollection.push(paramDetail);
                param.CorrelatedSektorDetailCollection = paramCollection;
            }
            this.modelDetail.save(param);
        }
    });
});