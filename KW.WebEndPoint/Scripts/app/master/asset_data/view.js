define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var template = require('text!./template.html');
    var commonFunction = require('commonfunction');
    var eventAggregator = require('eventaggregator');
    var Table = require('./table/table');
    var Collection = require('./collection');
    var Paging = require('paging');
    var TablePMN = require('./pmn/table/table');
    var CollectionPMN = require('./pmn/collection');
    var PagingPMN = require('paging');

    module.exports = LayoutManager.extend({
        className: 'container-fluid main-content tbl bg-white',
        template: _.template(template),
        events : {
         'click [name="add"]':'add',
         'click [name="addPMN"]':'addPMN'
        },
        initialize: function() {
            var self = this;
            this.table = new Table({
                collection: new Collection()
            });
            this.tablepmn = new TablePMN({
                collection: new CollectionPMN()
            });
            this.paging = new Paging({
                collection: this.table.collection
            });
            this.pagingpmn = new PagingPMN({
                collection: this.tablepmn.collection
            });
            this.listenTo(eventAggregator, 'master/asset_data/add:fetch', function() {
              self.fetchData();
            });
            this.listenTo(eventAggregator, 'master/asset_data/edit:fetch', function() {
             self.fetchData();
            });
            this.listenTo(eventAggregator, 'master/asset_data/delete:fetch', function(model) {
              self.fetchData();
            });
            this.listenTo(eventAggregator, 'master/asset_data/pmn/add:fetch', function() {
              self.fetchDataPMN();
            });
            this.listenTo(eventAggregator, 'master/asset_data/pmn/edit:fetch', function() {
              self.fetchDataPMN();
            });
            this.listenTo(eventAggregator, 'master/asset_data/pmn/delete:fetch', function(model) {
              self.fetchDataPMN();
            });
        },
        afterRender: function() {
            this.$('[obo-table-assetdata]').append(this.table.el);
            this.table.render();
            this.insertView('[obo-paging-assetdata]', this.paging);
            this.paging.render();
            this.fetchData();
            this.$('[obo-table-pmn]').append(this.tablepmn.el);
            this.tablepmn.render();
            this.insertView('[obo-paging-pmn]', this.pagingpmn);
            this.pagingpmn.render();
            this.fetchDataPMN();
        },
        fetchData: function() {
          this.table.collection.fetch({
            reset:true
          })
        },
        fetchDataPMN: function() {
          this.tablepmn.collection.fetch({
            reset:true
          })
        },
        add : function(){
          var self = this;
          require(['./add/view'], function(View) {
            commonFunction.setDefaultModalDialogFunction(self, View);
          });
        },
        addPMN : function(){
          var self = this;
          require(['./pmn/add/view'], function(View) {
            commonFunction.setDefaultModalDialogFunction(self, View);
          });
        },
    });
});