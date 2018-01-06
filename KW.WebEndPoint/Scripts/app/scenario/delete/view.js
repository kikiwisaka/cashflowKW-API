define(function(require, exports, module) {
    'use strict';
    var View = require('modaldialogdefault');
    var template = require('text!./template.html');
    var Model = require('./../model');
    var commonFunction = require('commonfunction');
    var commonConfig = require('commonconfig');
    var eventAggregator = require('eventaggregator');
    var CollectionRiskMatrix = require('./../../risk_matrix/collection');

    module.exports = View.extend({
        template: _.template(template),
        initialize : function(){
          var self = this;
          this.scenarioId = this.model.get('Id');
          this.listenTo(this.model, 'sync', function() {
            commonFunction.responseSuccessUpdateAddDelete('Scenario successfully deleted.');
            self.$el.modal('hide');
            eventAggregator.trigger('scenario/delete:fecth');
          });
          this.collectionRiskMatrix = new CollectionRiskMatrix();
          this.collectionRiskMatrix.fetch({
            data: {
              IsAllData: true
            },
            success: function(result, data) {
              self.stageTahun = data;
            }
          });
        },
        events: {
          'click [name="delete"]':'isAlreadyUserByRiskMatrix'
        },
        isAlreadyUserByRiskMatrix: function() {
            var self = this;
            var scenarioName = this.model.get('NamaScenario');
            var isUsed = false;
            $.each(this.stageTahun, function(index, data){
              if(data.ScenarioId == self.scenarioId) {
                isUsed = true;
              }
            });
            if(isUsed) {
              this.$el.modal('hide');
              commonFunction.responseWarningCannotExecute('You cannot delete Scenario '+ scenarioName +' because the Scenario already used by Risk Matrix.');
            } else {
              this.getConfirmation();
            }
        },
        getConfirmation: function(){
          var data = this.model.get('NamaScenario');
          var action = "delete";
          var retVal = confirm("Are you sure want to " + action + " Scenario : "+ data +" ?");
          if( retVal == true ){
             this.doDelete();
          }
          else{
            this.$('[type="submit"]').attr('disabled', false);
          }
        },
        doDelete: function() {
          this.model.destroy();
        }
    });
});
