define(function(require, exports, module) {
    'use strict';
    var View = require('modaldialogdefault');
    var template = require('text!./template.html');
    var commonFunction = require('commonfunction');
    var Table = require('./table/table');
    var Collection = require('./collection');
    var Paging = require('paging');
    var eventAggregator = require('eventaggregator');


    module.exports = View.extend({
        template: _.template(template),
        initialize: function(options) {
          var self = this;
          var ids = null;
          var riskIds = [];
          debugger;
          if(options.model.get('RiskIdSelected')){
            ids = options.model.get('RiskIdSelected');
            _.each(ids, function(item) {
              riskIds.push(item);
            });
          } else {
            ids = options.model.get('RiskRegistrasi');
            _.each(ids, function(item){
              riskIds.push(item.Id);
            });
          }

          this.table = new Table({
              collection: new Collection()
          });
          var originalParse = this.table.collection.parse;
          this.table.collection.parse = function(response) {
            var result = originalParse.call(this, response);
            if(riskIds.length > 0) {
              _.each(result, function(item) {
                var found = _.find(riskIds, (id) => {
                  return id == item.Id
                });
                item.isChecked = Boolean(found);
              });
              return result;
            } else {
              _.each(result, function(item){
                var selectedId = self.model.get('RiskIdSelected') || [];
                var found = _.find(selectedId, (id) => {
                  return id == item.Id
                });
                item.isChecked = Boolean(found);
              });
              return result;
            }
          }
          this.on('cleanup', function() {
              this.table.destroy();
              this.modalDialog && this.modalDialog.remove && this.modalDialog.remove();
          }, this)
        },
        afterRender: function() {
          this.$('[risk-table]').append(this.table.el);
          this.table.render();

          this.table.collection.fetch({
            reset: true,
            data: {
              IsPagination: false,
              PageSize: 20
            }
          });
        },
        events: {
          'click [btn-save-chosen-risk]': 'getChosenRisk'
        },
        selectRemoveAllRisk: function() {
          var status = this.$('[name="allRisk"]').prop('checked')
          if(status) {
            this.$('[name="RiskRegistrasi"]').prop('checked', true);
          } else {
            this.$('[name="RiskRegistrasi"]').prop('checked', false);
          }
        },
        getChosenRisk: function(){
          var collection = this.table.collection.filter((model) => {
            return model.get('isChecked');
          });
          this.model.set('RiskIdSelected', collection.map(
            (model) => {
              return model.get('Id')
            }))
          this.model.set('RiskNameSelected', collection.map(
            (model) => {
              return model.get('NamaCategoryRisk')
            }))
          eventAggregator.trigger('master/project/add/select_risk:risk_selected', this.model.get('RiskNameSelected'), this.model.get('RiskIdSelected'));
          this.$el.modal('hide');
        }
    });
});
