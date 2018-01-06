define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var template = require('text!./row.html');
    var commonFunction = require('commonfunction');

    module.exports = LayoutManager.extend({
        tagName: 'tr',
        template: _.template(template),
        events: {
            'click [name="Edit"]': 'Edit',
            'click [name="Delete"]': 'Delete',
            'click [name="Default"]': 'Default',
        },
        Edit: function() {
          var self = this;
          require(['./../edit/view'], function(View) {
            commonFunction.setDefaultModalDialogFunction(self, View, self.model);
          });
        },
        Delete: function() {
            var self = this;
            if(this.model.get('Status')) {
                commonFunction.responseWarningCannotExecute('You cannot delete default of Likelihood.');
            } else {
                require(['./../delete/view'], function(View) {
                    commonFunction.setDefaultModalDialogFunction(self, View, self.model);
                });
            }
        },
        Default: function() {
            var self = this;
            require(['./../default/view'], function(View) {
                commonFunction.setDefaultModalDialogFunction(self, View, self.model);
            });
        }
    });
});
