
define(function(require, exports, module) {
    'use strict';

    var commonConfig = require('commonconfig');
    var commonFunction = require('commonfunction');

    if (commonFunction.isLoginsHash()) {
        module.exports = function(){
            return {
                Start: function(callback){
                    callback();
                }
            }
        }
    } else {
        require('backbone');
        var SideBarView = require('./layout/sidebar/view');
        var NavBarView = require('./layout/navbar/view');
        var ContentView = require('./layout/content/view');

        var contentView = new ContentView();
        var navBarView = new NavBarView();
        var sideBarView = new SideBarView();

        commonFunction.setContentView(contentView);
        commonFunction.setSideBarView(sideBarView);
        commonFunction.setNavBarView(navBarView);
        requirejs(['settings','actions','icheck'], function() {});
        $('div.page-content', document.body).prepend(contentView.render().el);
        $('div.page-container').prepend(sideBarView.render().el);
        $('div.page-content').prepend(navBarView.render().el);

        module.exports = function() {
            return {
                Start: function(callback) {
                    callback(contentView);
                }
            };
        };
    };
});
