//scripts/app
define(function(require, exports, module) {
    'use strict';

    require('backbone');
    var commonFunction = require('commonfunction');
    var fnSetContentView = function(pathViewFile, hashtag, options) {
        require(['./' + pathViewFile + '/view'], function(View) {
            if (View)
                commonFunction.setContentViewWithNewModuleView(new View(), hashtag, options);
        });
    };

    module.exports = Backbone.Router.extend({
        initialize: function() {
            this.app = {};
        },
        routes: {
            '': 'showMainMenu',
            'login': 'showLogin',
            'test': 'showTest',
            'forgot_password': 'showForgotPassword',
            'reset_password': 'showResetPassword',
            'employee(/*subrouter)': 'redirectToEmployeeModule',
            'master(/*subrouter)': 'callMasterSubRouter',  
            'scenario(/*subrouter)': 'callScenarioModule', 
            'risk_matrix(/*subrouter)': 'callRiskMatrixModule',
            'correlation(/*subrouter)': 'callCorrelationSubRouter',  
            'overall_comment(/*subrouter)': 'callOverallCommentModule',  
            'dashboard(/*subrouter)': 'callDashboardSubRouter',  
            '*actions': 'notFound'
        },
        showTest: function() {
            console.log('masuk rut testtttt');
        },
        start: function() {
            Backbone.history.start();
        },
        showMainMenu: function() {
            fnSetContentView('./mainmenu');
        },
        showLogin: function() {
            fnSetContentView('./login');
        },
        showForgotPassword: function() {
            fnSetContentView('./forgot_password');
        },
        showResetPassword: function() {
            fnSetContentView('./reset_password');
        },
        redirectToEmployeeModule: function() {
            var self = this;
            if (!this.app.employeeRouter) {
                require(['./employee/router'], function(Router) {
                    self.app.employeeRouter = new Router('employee', {
                        createTrailingSlashRoutes: true
                    });
                });
            }
        },
        callMasterSubRouter: function(){
            if(!this.app.masterSubRouter){
                require(['./master/router'], Router => {
                    this.app.masterRouter = new Router('master');
                });
            }
        },
        callScenarioModule: function() {
            console.log('masuk module scenario');
            if(!this.app.scenarioRouter){
                require(['./scenario/router'], Router => {
                    this.app.scenarioRouter = new Router('scenario');
                });
            }
        },
        callRiskMatrixModule: function() {
            console.log('masuk module risk_matrix');
            if(!this.app.risk_matrixRouter){
                require(['./risk_matrix/router'], Router => {
                    this.app.risk_matrixRouter = new Router('risk_matrix');
                });
            }
        },
        callOverallCommentModule: function() {
            console.log('masuk module overall_comment');
            if(!this.app.overall_commentRouter){
                require(['./overall_comment/router'], Router => {
                    this.app.overall_commentRouter = new Router('overall_comment');
                });
            }
        },
        callCorrelationSubRouter: function(){
            if(!this.app.correlaltionSubRouter){
                require(['./correlation/router'], Router => {
                    this.app.masterRouter = new Router('correlation');
                });
            }
        },
        callDashboardSubRouter: function() {
            if(!this.app.dashboardSubRouter){
                require(['./dashboard/router'], Router => {
                    this.app.masterRouter = new Router('dashboard');
                });
            }
        },
        notFound: function() {
            require(['./errorpages/notfound/view'], function(View) {
                commonFunction.setContentViewWithNewModuleView(new View(), '#');
            });
        },
    });
});
