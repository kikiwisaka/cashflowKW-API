// scripts/app/storage/detail/menu
define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var commonFunction = require('commonfunction');

    module.exports = LayoutManager.extend({
        className: 'submenu-content',
        afterRender: function() {
            this.setUrl();
            this.doActiveButton();
        },
        doActiveButton: function() {
            var fragmentHash = commonFunction.getLastSplitHash();
            var classActive = 'bs-callout-active';
            this.$('a').removeClass(classActive);

            if (fragmentHash) {
                var buttonIsActive = fragmentHash.replace(/\?(.*)/ig, '');
                this.$('a[href$=' + buttonIsActive + ']').addClass(classActive);
            }
        },
        setUrl: function() {
            this.$('a[href]').each(function(index, item) {
                var href = $(item).attr('href');
                $(item).attr('href', commonFunction.getCurrentHashOmitSuffix(1) + href);
            })
        }
    });
});
