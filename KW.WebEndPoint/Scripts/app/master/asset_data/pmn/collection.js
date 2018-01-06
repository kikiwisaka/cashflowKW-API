define(function(require, exports, module) {
    'use strict';
    var ModelPMN = require('./model');
    var CollectionPMN = require('backbone.collection.paging');

    module.exports = CollectionPMN.extend({
        url: ModelPMN.prototype.urlRoot,
        model: ModelPMN
    });
});