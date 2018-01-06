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
      this.table = new Table({
        collection: new Collection()
      });

      this.paging = new Paging({
        collection: this.table.collection
      });
      this.on('cleanup', function() {
        this.table.destroy();
        this.modalDialog && this.modalDialog.remove && this.modalDialog.remove();
      }, this)
    },
    afterRender: function() {
      this.$('[kw-table]').append(this.table.el);
      this.table.render();

      this.table.collection.fetch({
        reset: true,
        data: {
          IsPagination: false,
          PageSize: 100
        }
      });
    }
  });
});