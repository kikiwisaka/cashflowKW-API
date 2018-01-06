define(function(require, exports, module) {
    'use strict';

    var Subroute = require('backbone.subroute');
    var commonFunction = require('commonfunction');

    var fnSetContentView = function(pathViewFile) {
        var hashtag = '#correlation_risk';
        require([pathViewFile + '/view'], function(View) {
            if (View)
                commonFunction.setContentViewWithNewModuleView(new View(), hashtag);
        });
    };

    module.exports = Subroute.extend({
        initialize: function() {
            this.app = {};
        },
        routes: {
            '':'showList',
            ':id': 'showDetail',
        },
        showList:function(){
            fnSetContentView('.');
        },
        showDetail: function(){
            fnSetContentView('./edit');
        }
    });
});
