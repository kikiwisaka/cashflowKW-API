define(function(require, exports, module) {
  'use strict';
  var LayoutManager = require('layoutmanager');
  var eventAggregator = require('eventaggregator');

  module.exports = LayoutManager.extend({
    className: 'container-fluid main-content',
    initialize: function() {
      if (this.Collection) {
        if (this.Table) {
          this.table = new this.Table({
            collection: new this.Collection()
          });

          if (this.Paging) {
            this.paging = new this.Paging({
              collection: this.table.collection
            });
          }

          if (this.Filter) {
            this.filter = new this.Filter({
              collection: this.table.collection
            });
          }

          this.on('cleanup', function() {
            if (this.table.destroy) {
              this.table.destroy();
            } else {
              this.table.remove && this.table.remove();
            }
          })
        }
      }

    },
    events: {
      'click [obo-defaultfilter]': 'setFilterToDefault',
      'click [obo-showeditfilters]': 'showEditFilters',
      'click [add]': 'setAdd'
    },
    afterRender: function() {
      var self = this;
      if (this.filter) {
        this.insertView('[obo-filter]', this.filter);
        this.filter.render();
      }

      if (this.table) {
        this.$('[obo-table]').append(this.table.el);
        this.table.render();
      }
      if (this.table && this.table.collection) {
        this.table.collection.fetch({
          reset: true
        });

        if (this.paging) {
          this.insertView('[pagging]', this.paging);
          this.paging.render();
        } else {
        }
      }
      this.listenTo(eventAggregator, 'Employee/filter:fecth', function(param) {
        if (self.table && self.table.collection) {
          self.table.collection.fetch({
            reset: true,
            data: param
          });
        }
      });
    },
    setFilterToDefault: function() {
      this.filter.setFilterToDefault();
    },
    showEditFilters: function() {
      var self = this;
      if (this.ModalDialog) {
        this.modalDialog = new this.ModalDialog({
          viewFilter: self.filter
        });

        $('body').append(self.modalDialog.el);
        this.modalDialog.$el.on('hidden.bs.modal', function() {
          self.modalDialog.remove();
        });

        this.modalDialog.once('afterRender', function() {
          self.modalDialog.$el.modal();
        });
        this.modalDialog.render();
      }
    },
    setAdd: function() {
      var self = this;
      if (this.ModalDialog) {
        this.modalDialog = new this.ModalDialogAdd({
          viewFilter: self.filter
        });

        $('body').append(self.modalDialog.el);
        this.modalDialog.$el.on('hidden.bs.modal', function() {
          self.modalDialog.remove();
        });

        this.modalDialog.once('afterRender', function() {
          self.modalDialog.$el.modal();
        });
        this.modalDialog.render();
      }
    }
  });
});
