define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');

    module.exports = LayoutManager.extend({
        className: 'modal fade',
        attributes: {
            tabindex: '-1',
            role: 'dialog',
            'aria-labelledby': 'modalDialogEditFilter',
            'aria-hidden': 'true'
        }
    });
});
