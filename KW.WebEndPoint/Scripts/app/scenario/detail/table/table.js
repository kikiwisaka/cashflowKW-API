define(function(require, exports, module) {
    'use strict';
    var Table = require('table-2.table');
    var template = require('text!./table.html');
    var Tbody = require('./tbody');
    var Collection = require('./collection');

    module.exports = Table.extend({
        template: _.template(template),
        collection: new Collection(),
        Tbody: Tbody,
        regions: {
            body: {
                el: 'tbody',
                replaceElement: true
            }
        }
    });
});
