define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var template = require('text!./template.html');
    var commonFunction = require('commonfunction');
    var eventAggregator = require('eventaggregator');
    var Table = require('./table/table');
    var Collection = require('./collection');
    var Paging = require('paging');
    var Model = require('./../model');
    var ModelParent = require('./../model');

    module.exports = LayoutManager.extend({
        className: 'container-fluid main-content tbl bg-white',
        template: _.template(template),
        initialize: function(options) {
            var self = this;
            this.model = new Model();
            this.modelParent = new ModelParent();
            this.table = new Table({
                collection: new Collection()
            });
            this.paging = new Paging({
                collection: this.table.collection
            });
            this.parentId = commonFunction.getUrlHashSplit(3);
            this.listenTo(this.model, 'sync', () => {
                this.render();
            });
            this.modelParent.fetch({
                reset: true,
                data: {
                    id: this.parentId
                }
            });
            // this.model.set(this.model.idAttribute, commonFunction.getLastSplitHash());
            this.model.fetch({
                reset: true,
                data: {
                    id: this.parentId
                }
            });
            this.once('afterRender', () => {
                this.fetchData();
            });
            this.listenTo(eventAggregator, 'master/comment/detail/add:fetch', function() {
              self.fetchData();
            });
            this.listenTo(eventAggregator, 'master/comment/detail/edit:fetch', function() {
             self.fetchData();
            });
            this.listenTo(eventAggregator, 'master/comment/detail/delete:fetch', function(model) {
              self.fetchData();
            });
        },
        events: {
            'click [name="add"]': 'add'
        },
        afterRender: function() {
            this.$('[obo-table-comment]').append(this.table.el);
            this.table.render();
            this.insertView('[obo-paging-comment]', this.paging);
            this.paging.render();
            // this.setTemplate(this.modelParent);
            this.fetchData();
        },
        // setTemplate: function() {
        //     this.$('[name="Warna"]').text(this.modelParent.get('Warna'));
        // },
        fetchData: function() {
            this.table.collection.fetch({
                reset: true,
                data: {
                    ParentId: this.parentId
                },
            })
        },
        add: function() {
            var self = this;
            var modelParent = this.modelParent;
            require(['./add/view'], function(View) {
                commonFunction.setDefaultModalDialogFunction(self, View, modelParent);
            });
        }
    });
});