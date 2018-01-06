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
        events : {
         'click [name="add"]':'add'
        },
        initialize: function() {
            var self = this;
            this.table = new Table({
                collection: new Collection()
            });

            this.paging = new Paging({
                collection: this.table.collection
            });

            // sudah tidak perlu karena sudah di add di backbone.layoutmanager.override.js
            // this.on('cleanup', function() {
                // this.table.destroy();
                //this.modalDialog && this.modalDialog.remove && this.modalDialog.remove();
            // }, this)

            this.listenTo(eventAggregator, 'master/tahapanproject/add:fecth', function() {
              self.fetchData();
            });
            this.listenTo(eventAggregator, 'master/tahapanproject/edit:fecth', function() {
             self.fetchData();
            });
            this.listenTo(eventAggregator, 'master/tahapanproject/delete:fecth', function(model) {
              self.fetchData();
            });
        },
        afterRender: function() {
            this.$('[obo-table]').append(this.table.el);
            this.table.render();

            this.insertView('[obo-paging]', this.paging);
            this.paging.render();

            this.fetchData();
        },
        fetchData: function() {
          this.table.collection.fetch({
            reset:true
          })
        },
        //kalau add .. memang disini karena row.js(child) tidak bisa akese begitu pula table.js (child);
        add : function(){
          var self = this;
          require(['./add/view'], function(View) {
            commonFunction.setDefaultModalDialogFunction(self, View);
          });
        }
    });
});
