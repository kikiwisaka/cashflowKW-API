define(function(require, exports, module) {
    'use strict';
    var Subroute = require('subroute');
    var commonFunction = require('commonfunction');

    module.exports = Subroute.extend({
        initialize: function() {
            this.app = {};
        },
        routes: {
            // 'calculation(/*subrouter)': 'callSubRouters',
            // 'functional_risk(/*subrouter)': 'callSubRouters',
            'result(/*subrouter)': 'callSubRouters',
            '*actions': 'notFound'
        },
        callSubRouters: function(){
            var namaModul = commonFunction.getUrlHashSplit(2);
            var listNamaModule = ['result'];
            var foundNamaModul = _.find(listNamaModule, (item) => { return item == namaModul});
            if(foundNamaModul){
                console.log(foundNamaModul);
                var appName = this.app[foundNamaModul + 'Router'];
                 if (!appName) {
                    require(['./'+foundNamaModul+'/router'], (Router) => {
                        appName = new Router(this.prefix + '/' + foundNamaModul);
                    });
                }
            }
        },  
        notFound: function() {
            require(['./errorpages/notfound/view'], function(View) {
                commonFunction.setContentViewWithNewModuleView(new View(), '#');
            });
        },
    });
});
