define(function(require, exports, module) {
    'use strict';
    var Model = require('./model');
    var Collection = require('backbone.collection.paging');

    module.exports = Collection.extend({
        url: Model.prototype.urlRoot,
        model: Model
    });
});
