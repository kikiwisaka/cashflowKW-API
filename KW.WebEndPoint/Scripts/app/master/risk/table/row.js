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
            'click [name="Detail"]': 'Detail',
        },
        Edit: function() {
          var self = this;
          require(['./../edit/view'], function(View) {
            commonFunction.setDefaultModalDialogFunction(self, View, self.model);
          });
        },
        Delete: function() {
            var self = this;
            require(['./../delete/view'], function(View) {
                commonFunction.setDefaultModalDialogFunction(self, View, self.model);
            });
        },
        // Detail: function() {
        //     var self = this;
        //     console.log(this.model);
        //     require(['./../../subrisk/view'], function(View) {
        //         var view = new View({model: self.model});
        //         self.removeView('[risk-content]');
        //         self.insertView('[risk-content]', view);
        //         view.render();
        //     });
        // }
    });
});
