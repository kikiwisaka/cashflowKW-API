define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var template = require('text!./row.html');
    var commonFunction = require('commonfunction');

    module.exports = LayoutManager.extend({
        tagName: 'tr',
        template: _.template(template),
        events: {
            'click [name="Detail"]': 'Detail',
        },
        Detail: function() {
            console.log('masuk ke detail over all comment');
            var self = this;
            require(['./../../detail/view'], function(View) {
                commonFunction.setDefaultModalDialogFunction(self, View, self.model);
            });
        }
    });
});
