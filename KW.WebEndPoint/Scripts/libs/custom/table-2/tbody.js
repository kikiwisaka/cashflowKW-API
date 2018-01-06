define(function(require, exports, module) {
    'use strict';
    var Marionette = require('marionette');
    var _ = require('underscore');
    var domLoading = _.template('<i class="fa fa-spinner fa-spin fa-2x fa-fw" aria-hidden="true"></i>');
    var domNoData = _.template('no data available');

    var View = Marionette.View.extend({
        className: 'text-center',
        attributes: {
            style: 'position:absolute;width: 100%;padding: 20px;'
        },
        template: _.template(''),
        initialize: function() {
            var self = this;
            if (this.collection) {
                this.collection.on('request', function() {
                    self.template = domLoading;
                    if (self._isAttached)
                            self.render();
                        else
                            self.$el.html(self.template);
                });

                this.collection.on('sync error', function() {
                    if (!self.collection.length){
                        self.template = domNoData;
                        if (self._isAttached)
                                self.render();
                            else
                                self.$el.html(self.template);
                    }
                });
                this.template = this.collection.isRequesting ? domLoading : domNoData;
            }
        }
    });

    module.exports = Marionette.CollectionView.extend({
        tagName: 'tbody',
        className: 'hover text-grey',
        initialize: function() {
            if (this.collection) {
                this.emptyView = View.extend({
                    collection: this.collection
                })
            }
        }
    });
});
