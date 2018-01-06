define(function(require, exports, module) {
    'use strict';
    var Table = require('table-2.table');
    var template = require('text!./table.html');
    var Tbody = require('./tbody');

    module.exports = Table.extend({
        template: _.template(template),
        Tbody: Tbody,
        regions: {
            body: {
                el: 'tbody',
                replaceElement: true
            }
        }
    });
});
