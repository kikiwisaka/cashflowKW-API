define(function(require, exports, module) {
    'use strict';
    var Marionette = require('marionette');
    var SortingButton = require('sorting.button');

    module.exports = Marionette.View.extend({
        tagName: 'table',
        className: 'table table-striped table-condensed',
        onRender: function() {
            var self = this;

            if (this.Tbody) {
                this.showChildView('body', new this.Tbody({
                    collection: this.collection
                }));

                _.each(this.$('thead > tr > th > [data-parampaging-sortfield]'), function(item) {
                    var SortField = $(item).attr('data-parampaging-sortfield');
                    self.showChildView(SortField, new SortingButton({
                        SortField: SortField,
                        collection: self.collection
                    }));
                }, this);
            }
        }
    });
});
