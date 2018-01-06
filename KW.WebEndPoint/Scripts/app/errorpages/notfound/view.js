define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var template = require('text!./template.html');

    module.exports = LayoutManager.extend({
        template: _.template(template)
    });
});
