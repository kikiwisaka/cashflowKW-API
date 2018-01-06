define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var template = require('text!./template.html');
    var commonFunction = require('commonfunction');
    var eventAggregator = require('eventaggregator');
    var Table = require('./table/table');
    var Paging = require('paging');
    var Model = require('./../model');
    var ModelParent = require('./../model');
    var ModelOverAllComment = require('./model');

    module.exports = LayoutManager.extend({
        className: 'container-fluid main-content tbl bg-white',
        template: _.template(template),
        initialize: function(options) {
            var self = this;
            this.model = new Model();
            this.modelParent = new ModelParent();
            this.modelOverAllComment = new ModelOverAllComment();
            this.parentId = commonFunction.getUrlHashSplit(2);
            this.listenTo(this.model, 'sync', () => {
                this.render();
            });
            this.modelParent.fetch({
                reset: true,
                data: {
                    id: this.parentId
                }
            });
            this.modelOverAllComment.fetch({
                reset: true,
                data: {
                    id: this.parentId
                }
            });
            this.model.set(this.model.idAttribute, commonFunction.getLastSplitHash());
            this.model.fetch();
            this.once('afterRender', () => {
                this.fetchData();
            });
            this.table = new Table();
            this.paging = new Paging({
                collection: this.table.collection
            });
            this.listenTo(eventAggregator, 'overall_comment/detail/add:fetch', function() {
              self.fetchData();
            });
            this.listenTo(eventAggregator, 'overall_comment/detail/edit:fetch', function() {
             self.fetchData();
            });
            this.listenTo(eventAggregator, 'overall_comment/detail/delete:fetch', function(model) {
              self.fetchData();
            });
        },
        events: {
            'click [name="add"]': 'add'
        },
        afterRender: function() {
            this.setTemplateOverAllComment();
            // this.setTemplate();
            this.$('[obo-table-comment]').append(this.table.el);
            this.table.render();
            this.insertView('[obo-paging]', this.paging);
            this.paging.render();
        },
        // setTemplate: function() {
        //     this.$('[name="Warna"]').text(this.modelParent.get('Warna'));
        // },
        setTemplateOverAllComment: function() {
            this.$('[name="OverAllComment"]').text(this.modelOverAllComment.get('OverAllComment'));
        },
        fetchData: function() {
            this.table.collection.fetch({
                reset: true,
                data: {
                    ParentId: this.model.id
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