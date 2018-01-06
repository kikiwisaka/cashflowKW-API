define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var template = require('text!./template.html');
    var commonFunction = require('commonfunction');
    var eventAggregator = require('eventaggregator');
    var Table = require('./table/table');
    // var Collection = require('./collection');
    var Paging = require('paging');
    var Model = require('./model');
    var ModelParent = require('./../model');

    module.exports = LayoutManager.extend({
        className: 'container-fluid main-content tbl bg-white',
        template: _.template(template),
        initialize: function(options) {
            var self = this;
            this.model = new Model();
            this.modelParent = new ModelParent();

            this.listenTo(this.model, 'sync', () => {
                this.render();
            })

            this.once('afterRender', () => {
                this.model.set(this.model.idAttribute, commonFunction.getLastSplitHash());
                this.model.fetch();

                this.modelParent.set(this.modelParent.idAttribute, commonFunction.getLastSplitHash());
                this.modelParent.fetch();
            });

            this.parentId = commonFunction.getUrlHashSplit(3);

            this.table = new Table();

            this.paging = new Paging({
                collection: this.table.collection
            });
            this.listenTo(eventAggregator, 'master/risk/detail/add:fecth', function() {
              self.fetchData();
            });
            this.listenTo(eventAggregator, 'master/risk/detail/edit:fecth', function() {
             self.fetchData();
            });
            this.listenTo(eventAggregator, 'master/risk/detail/delete:fecth', function(model) {
              self.fetchData();
            });
        },
        events: {
            'click [name="add"]': 'add'
        },
        afterRender: function() {

            // if(!this.model.id)
            //     return;

            this.$('[obo-table-subrisk]').append(this.table.el);
            this.table.render();

            this.table.collection.fetch({
                reset: true,
                data: {
                    ParentId: this.parentId
                }
            });

            this.insertView('[obo-paging]', this.paging);
            this.paging.render();
            this.setTemplate(this.modelParent);
        },
        setTemplate: function() {
            this.$('[name="KodeMRisk"]').text(this.modelParent.get('KodeMRisk').charAt(0));
            this.$('[name="NamaCategoryRisk"]').text(this.modelParent.get('NamaCategoryRisk'));
            this.$('[name="Definisi"]').text(this.modelParent.get('Definisi'));
        },
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