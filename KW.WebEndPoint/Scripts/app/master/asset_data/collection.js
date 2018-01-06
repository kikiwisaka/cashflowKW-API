define(function(require, exports, module) {
    'use strict';
    var Model = require('./model');
    // var ModelPMN = require('./pmn/model');
    var Collection = require('backbone.collection.paging');
    // var CollectionPMN = require('backbone.collection.paging');

    module.exports = Collection.extend({
        url: Model.prototype.urlRoot,
        modelpmn: Model
    });
});
