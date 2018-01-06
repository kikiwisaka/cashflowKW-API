define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');

    module.exports = LayoutManager.extend({
        className: 'container-fluid main-content',
        events:{
            'click [obo-defaultfilter]': 'setFilterToDefault',
            'click [obo-showeditfilters]': 'showEditFilters',
            'click [add]': 'setAdd',
        },
        setFilterToDefault: function(){
            this.filter.setFilterToDefault();
        },
        showEditFilters: function() {
            var self = this;
            if (this.ModalDialog){
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
        setAdd : function(){
          var self = this;
          if (this.ModalDialog){
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
