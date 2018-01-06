define(function (require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var eventAggregator = require('eventaggregator');

    var NoData = LayoutManager.extend({
        tagName: 'tr',
        className: 'text-center',
        afterRender: function () {
            if (this.options.messageEmptyDataTd && this.options.messageEmptyDataTd.attr) {
                if (!this.options.parent.isRequest) {
                    this.removeView('');
                    this.$el.append('<td>No Data</td>');
                    this.$('td').attr(this.options.messageEmptyDataTd.attr);
                }
            }
        }
    });

    module.exports = LayoutManager.extend({
        tagName: 'tbody',
        initialize: function (options) {
            var self = this;
            if (this.beforeInitialize)
                this.beforeInitialize(options);

            if(this.afterInitialize)
				this.afterInitialize();

            this.listenTo(this.collection, 'request', function () {
                self.isRequest = true;
                self.removeView('');
                $('[data-name="LoadingData"]').remove();
                self.$el.append('<tr><td class="text-center" colspan="' + this.options.messageEmptyDataTd.attr.colspan + '" data-name="LoadingData">Loading..</td></tr>');
            });

            this.listenTo(this.collection, 'error', function () {
                self.isRequest = false;
            });

            this.listenTo(this.collection, 'reset', function () {
                self.isRequest = false;
                self.$('[data-name="LoadingData"]').remove();
                self.collection.trigger('beforeReset');
                self.removeView('');
                self.$el.empty();
                if (self.collection.length) {
                    self.collection.each(self.addOne, self);
                } else {
                    self.addMessageEmptyData();
                }
                self.collection.trigger('afterReset');
            });

            this.listenTo(this.collection, 'sync', function () {
                self.$('[data-name="LoadingData"]').remove();
            });

            this.listenTo(this.collection, 'add', function (model) {
                if (self.noData) {
                    self.noData.remove();
                    self.noData = undefined;
                }
                self.addOne(model);
            });

            this.on('cleanup', function () {
                if (self.collection) {
                    self.collection.stopListening(eventAggregator);
                }
            });
        },
        addOne: function (model) {
            if (this.Tr) {
                var view = new this.Tr({
                    model: model
                });
                this.insertView('', view);
                view.render();
            }

        },
        beforeRender: function () {
            this.trigger('afterBeforeRender');
        },
        afterRender: function () {
            this.removeView('');
            if (this.collection.length) {
                this.collection.each(this.addOne, this);
            } else {
                this.addMessageEmptyData();
            }
            this.trigger('afterAfterRender');
        },
        addMessageEmptyData: function () {
            var options = this.options;
            options.parent = this;
            this.noData = new NoData(options);
            this.insertView('', this.noData);
            this.noData.render();
        },
        setUrlRegionIdAndFetch: function (val) {
            this.collection.urlAddRegionId(val);
            this.collection.fetch({ reset: true });
        }
    });
});
