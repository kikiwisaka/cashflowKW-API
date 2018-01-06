define(function(require, exports, module) {
    'use strict';

    var LayoutManager = require('layoutmanager');
    var template = require('text!./template.html');

    module.exports = LayoutManager.extend({
        el: false,
        initialize: function() {
            var self = this;
            this.isLoadingModule = false;
            if (!window.indexedDB)
                window.indexedDB = window.mozIndexedDB || window.webkitIndexedDB || window.msIndexedDB;
            window.IDBTransaction = window.IDBTransaction || window.webkitIDBTransaction || window.msIDBTransaction;
            window.IDBKeyRange = window.IDBKeyRange || window.webkitIDBKeyRange || window.msIDBKeyRange

            if (!window.indexedDB) {
                window.alert("Your browser doesn't support a stable version of IndexedDB.")
            }
            var db;
            var request = window.indexedDB.open('newDatabase', 1);
            request.onsuccess = function(event) {
                db = request.result;
            }
        },
        template: _.template(template),
        events: {
            //'click [data-name="menuSidebar"] li a[href]:not([href="#"])': 'clickMenuSidebar',
            'click li': 'setActive'
        },
        afterRender: function() {
            require(['commonfunction'], (commonFunction) => {
                var hash = commonFunction.getUrlHashSplit();
                var DOM = $(self.$('.x-navigation')[0]);


                if (DOM) {
                    var linkDOMs = $('a[href]:not([href="#"])', DOM);
                    var found = undefined;

                    for (var i = hash.length-1; i >= 0 ; i--) {
                        var searchHashTag = commonFunction.getCurrentHashToLevel(i+1);                        
                        found = _.find(linkDOMs, function(item) {
                            var hashTag = $(item).attr('href');
                            return (searchHashTag == hashTag);
                        });
                        if (found){
                            linkDOMs.removeClass('active');
                            $(found).addClass('active')
                            .parents('.xn-openable').addClass('active');
                            
                            break;
                        }
                    }
                }

            });
        },
        clickMenuSidebar: function(e) {
            e.preventDefault();
            var currentTarget = this.$(e.currentTarget);
            var href = currentTarget.attr('href');

            var ret = Backbone.history.navigate(href, true);
            if (ret === undefined) {
                Backbone.history.loadUrl(href);
            }
        },
        setActive: function(e){
            var currentTarget  = this.$(e.currentTarget);
            var DOM = $(this.$('.x-navigation')[0]);
            var linkDOMs = $('a[href]:not([href="#"])', DOM);
            linkDOMs.removeClass('active');
            $(currentTarget).addClass('active');
        }
    });
});