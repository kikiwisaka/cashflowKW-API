define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var template = require('text!./row.html');
    var eventAggregator = require('eventaggregator');

    module.exports = LayoutManager.extend({
        tagName: 'tr',
        template: _.template(template),
        events: {
            'change input[type="checkbox"]': 'changeValue'
        },
        changeValue: function(e){
            this.model.set('isChecked', $(e.currentTarget).is(':checked'));
        }
    });
});
