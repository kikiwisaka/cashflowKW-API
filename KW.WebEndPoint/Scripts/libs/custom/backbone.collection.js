define(function(require, exports, module) {
    'use strict';
    var Backbone = require('backbone');
    var commonFunction = require('commonfunction');

    module.exports = Backbone.Collection.extend({
        initialize: function(options) {
            if (this.beforeInitialize) {
                this.beforeInitialize(options);
            }

            this.listenTo(this, 'error', function(collection, xhr) {
              var commonFunction = require('commonfunction');
              commonFunction.responseStatusNot200({
                  'xhr': xhr
              });
                // window.alert('server is down : please tell to logistical maintenance team and the last time what you did... current server can\'t access to ' + collection.url);
            });
        }
    });
});
