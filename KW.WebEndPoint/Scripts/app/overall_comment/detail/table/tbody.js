define(function(require, exports, module) {
    'use strict';
    var Tbody = require('table-2.tbody');
    var Row = require('./row');

    module.exports = Tbody.extend({
        childView: Row
    });
});
