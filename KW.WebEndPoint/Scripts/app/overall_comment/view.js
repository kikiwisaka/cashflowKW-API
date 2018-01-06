define(function(require, exports, module) {
    'use strict';

    var LayoutManager = require('layoutmanager');
    var template = require('text!./template.html');
    var commonFunction = require('commonfunction');
    var eventAggregator = require('eventaggregator');
    var Table = require('./table/table');
    var Collection = require('./collection');
    var Paging = require('paging');

    module.exports = LayoutManager.extend({
        className: 'container-fluid main-content tbl bg-white',
        template: _.template(template),
        initialize: function() {
            var self = this;
            this.table = new Table({
                collection: new Collection()
            });

            this.paging = new Paging({
                collection: this.table.collection
            });
            this.listenTo(eventAggregator, 'overall_comment/add:fecth', function() {
                self.fetchData();
            });
            this.listenTo(eventAggregator, 'overall_comment/edit:fecth', function() {
                self.fetchData();
            });
            this.listenTo(eventAggregator, 'overall_comment/delete:fecth', function(model) {
                self.fetchData();
            });
        },
        events: {
            'click [name="add"]': 'add'
        },
        afterRender: function() {
            this.$('[obo-table-colorcomment]').append(this.table.el);
            this.table.render();

            this.insertView('[obo-paging]', this.paging);
            this.paging.render();

            this.fetchData();
        },
        fetchData: function() {
            this.table.collection.fetch({
                reset: true
            })
        },
        add: function() {
            var self = this;
            require(['./add/view'], function(View) {
                commonFunction.setDefaultModalDialogFunction(self, View);
            });
        }
    });
});