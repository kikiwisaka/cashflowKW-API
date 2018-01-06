define(function(require, exports, module) {
    'use strict';

    var Subroute = require('backbone.subroute');
    var commonFunction = require('commonfunction');

    var fnSetContentView = function(pathViewFile) {
        var hashtag = '#overall_comment';
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
            //':id/*subrouter': 'redirectToDetailModule'
        },
        showList:function(){
            fnSetContentView('.');
        },
        showDetail: function(){
            console.log('masuk show detail');
            fnSetContentView('./detail');
        },
        // redirectToDetailModule: function(id, subrouter) {
        //     var self = this;
        //     debugger;
        //     require(['./view'], function(Router) {
        //         if (!self.app.detailRouter) {
        //             self.app.detailRouter = new Router(self.prefix + '/detail/:id', {
        //                 createTrailingSlashRoutes: true
        //             });
        //         }
        //     });
        // }
    });
});
