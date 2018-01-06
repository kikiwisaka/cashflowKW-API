define(function(require, exports, module) {
    'use strict';

    var LayoutManager = require('layoutmanager.original');
    var moment = require('moment');
    var ladda = require('ladda.jquery');

    module.exports = LayoutManager.extend({
        beforeInitialize: function(options) {
            this.ladda = {};
        },
        afterInitialize: function(options) {
            var self = this;

            this.on('cleanup beforeRender', function() {
                this.laddaDestroy();
            });

            this.on('cleanup', function(){
                this.table && this.table.destroy && this.table.destroy();
                this.paging && this.paging.destroy && this.paging.destroy();
            })

            this.once('afterRender', function() {
                if (this.model) {
                    if (this.$('[obo-dosave]') && this.$('[obo-dosave]').length){
                        if (!this.ladda['obo-dosave']){
                            this.ladda['obo-dosave'] = this.$('[obo-dosave]').ladda();
                        }
                    }

                    this.listenTo(this.model, 'request', function() {
                        if (self.$('[obo-dosave]').length)
                            self.$('[obo-dosave]').attr('disabled', 'disabled');

                        if (self.ladda && self.ladda['obo-dosave'])
                            self.ladda['obo-dosave'].ladda('start');
                    });

                    this.listenTo(this.model, 'sync error', function() {
                        if (self.$('[obo-dosave]').length)
                            self.$('[obo-dosave]').removeAttr('disabled', 'disabled');

                        if (self.ladda && self.ladda['obo-dosave'])
                            self.ladda['obo-dosave'].ladda('stop');
                    });
                }
            })
        },
        serialize: function() {
            var commonFunction = require('commonfunction');
            var data = LayoutManager.prototype.serialize.call(this);
            // debugger;
            data = _.extend({}, data, {commonFunction: commonFunction}, {
                _getCurrentHash: commonFunction.getCurrentHash,
                _getCurrentHashToLevel: commonFunction.getCurrentHashToLevel,
                _getCurrentHashOmitSuffix: commonFunction.getCurrentHashOmitSuffix,
                moment: moment
            });
            return data;
        },
        laddaDestroy: function() {
            if (this.ladda['obo-dosave']) {
                this.ladda['obo-dosave'].remove();
                delete this.ladda['obo-dosave'];
            }
        }
    });
});
