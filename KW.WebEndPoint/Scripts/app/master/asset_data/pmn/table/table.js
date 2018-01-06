define(function(require, exports, module) {
    'use strict';
    var TablePMN = require('table-2.table');
    var template = require('text!./table.html');
    var Tbody = require('./tbody');

    module.exports = TablePMN.extend({
        template: _.template(template),
        // collection: new CollectionPMN(),
        Tbody: Tbody,
        regions: {
            body: {
                el: 'tbody',
                replaceElement: true
            }
        }
    });
});
