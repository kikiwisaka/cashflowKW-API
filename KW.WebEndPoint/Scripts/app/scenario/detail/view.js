define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var template = require('text!./template.html');
    var commonFunction = require('commonfunction');
    var eventAggregator = require('eventaggregator');
    var Table = require('./table/table');
    var Paging = require('paging');
    var Model = require('./../model');

    module.exports = LayoutManager.extend({
        className: 'container-fluid main-content tbl bg-white',
        template: _.template(template),
        initialize: function(options) {
            var self = this;
            var parentId = commonFunction.getUrlHashSplit(3);
            this.parentId = parentId;
            this.model = new Model();

            this.listenTo(this.model, 'sync', () => {
                this.render();
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

            this.paging = new Paging({
                collection: this.table.collection
            });
            this.listenTo(eventAggregator, 'master/likelihood/detail/edit:fecth', function() {
                self.fetchData();
            });
            // this.listenTo(eventAggregator, 'master/risk/detail/edit:fecth', function() {
            //     self.fetchData();
            // });
            // this.listenTo(eventAggregator, 'master/risk/detail/delete:fecth', function(model) {
            //     self.fetchData();
            // });
        },
        afterRender: function() {
            if (!this.model.id) {
                return;
            }

            this.$('[obo-table-sublikelihood]').append(this.table.el);
            this.table.render();

            this.insertView('[obo-paging]', this.paging);
            this.paging.render();
        },
        fetchData: function() {
            this.table.collection.fetch({
                reset: true,
                data: {
                    ParentId: this.parentId
                }
            });
        }
    });
});