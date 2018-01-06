define(function(require, exports, module) {
    'use strict';
    var test = require('require');
    var LayoutManager = require('layoutmanager');
    var template = require('text!pathLibsCustom/sorting.button/template.html');

    module.exports = LayoutManager.extend({
        tagName: 'span',
        className: 'pull-right',
        template: _.template(template),
        initialize: function(options) {
            this.listenTo(this.collection, 'sync', function(collection) {
                this.setIsHide();
                this.$('[name="TotalPage"]').text(this.collection.paramPaging.TotalPage);
                this.changeViewSorting();
            }, this);

            this.listenTo(this.collection, 'changeViewSorting', function() {
                this.changeViewSorting();
            }, this)

            this.paramPaging = {
                SortField: ''
            };

            this.paramPaging = _.extend({}, this.paramPaging, options.paramPaging);
            this.iconSort = {
                asc: '.fa-caret-up',
                desc: '.fa-caret-down'
            }
        },
        events: {
            'click button': 'doSorting'
        },
        beforeRender: function() {
            this.setIsHide();
        },
        doSorting: function() {
            this.setChangeSort();
        },
        setChangeSort: function() {
            var self = this;
            this.setIsHide();

            this.collection.paramPaging.SortField = this.SortField;

            if (this.$('button > .isactive').length) {
                this.collection.paramPaging.SortOrder = this.$(this.iconSort.asc).hasClass('isactive') ? 'desc' : 'asc';
            } else {
                this.collection.paramPaging.SortOrder = 'asc';
            }
            this.collection.trigger('changeViewSorting');
            this.collection.setOrderBy();
            this.collection.fetch();
        },
        setChangePage: function(page) {
            if (page >= 1 && page <= this.collection.paramPaging.TotalPage) {
                this.collection.paramPaging.Page = page;
                this.$('[name="Page"]').val(page)
                this.collection.fetch();
                this.changeButtonPaging();
            }
        },
        setIsHide: function() {
            if (this.collection.paramPaging){
                if (this.collection.paramPaging.Page) {
                    this.$el.removeClass('hide');
                } else {
                    this.$el.addClass('hide')
                }
            }
        },
        clearActive: function() {
            this.$(this.iconSort.asc).removeClass('isactive');
            this.$(this.iconSort.desc).removeClass('isactive');
        },
        changeViewSorting: function() {
            if (this.collection.paramPaging.SortField == this.SortField) {
                if (this.collection.paramPaging.SortOrder == 'asc') {
                    this.$(this.iconSort.desc).removeClass('isactive');
                    this.$(this.iconSort.asc).addClass('isactive');
                } else {
                    this.$(this.iconSort.asc).removeClass('isactive');
                    this.$(this.iconSort.desc).addClass('isactive');
                }
            } else {
                this.clearActive();
            }
        }
    });
});
