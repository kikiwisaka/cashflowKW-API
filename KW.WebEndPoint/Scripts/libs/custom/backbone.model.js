define(function(require, exports, module) {
    'use strict';

    require('deep-model');
    // var commonFunction = require('commonfunction');

    module.exports = Backbone.DeepModel.extend({
        // idAttribute: 'Id',
        initialize: function(options) {
            if (this.beforeInitialize) {
                this.beforeInitialize(options);
            }

            this.on('request', function(){
                this.requestToServer = true
            });

            this.on('sync error', function(){
                this.requestToServer = false
            });

            this.on('error', function(model, xhr) {
                require(['commonfunction'], function(commonFunction) {
                    commonFunction.responseStatusNot200({
                        'xhr': xhr
                    });
                });
            });
        }
    });
});
