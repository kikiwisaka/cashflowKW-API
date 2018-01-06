//scripts/app
define(function(require, exports, module) {
    'use strict';
    var Subroute = require('subroute');
    var commonFunction = require('commonfunction');

    module.exports = Subroute.extend({
        initialize: function() {
            this.app = {};
        },
        routes: {
            'risk(/*subrouter)': 'callSubRouters',
            'sektor(/*subrouter)': 'callSubRouters',
            'project(/*subrouter)': 'callSubRouters',
            'likelihood(/*subrouter)': 'callSubRouters',
            'asset_data(/*subrouter)': 'callSubRouters',
            'tahapan_project(/*subrouter)': 'callSubRouters',
            'employee(/*subrouter)': 'callSubRouters',
            'stage(/*subrouter)': 'callSubRouters',
            'comment(/*subrouter)': 'callSubRouters',
            'correlation_matrix(/*subrouter)': 'callSubRouters',
            'functional_risk(/*subrouter)': 'callSubRouters',
            '*actions': 'notFound'
        },
        callSubRouters: function(){
            var namaModul = commonFunction.getUrlHashSplit(2);
            var listNamaModule = ['risk','sektor','project','likelihood','asset_data','tahapan_project','employee','stage','comment','correlation_matrix','functional_risk'];
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
