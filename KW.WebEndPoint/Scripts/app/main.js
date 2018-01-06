define(function(require, exports, module) {
    'use strict';

    if (!window.console) {
        window.console = {};
    };
    if (!console.log) {
        console.log = function() {};
    };
    if (!console.warn) {
        console.warn = function() {};
    };

    require('layoutmanager');
    require('select2');

    //// below this will suppressWarnings when using el: false on Layoutmanager
    Backbone.Layout.configure({
        suppressWarnings: true
    });

    var Router = require('./router');
    var Startup = require('./startup');


    if (!document.addEventListener) { //IE 8 and below
        // below this is used;
        require(['respond'], function() {});
        //require('ie9');
    }

    if (navigator.userAgent.toLowerCase().indexOf('firefox') > 1) {
        //require(['historis'], function () { });
        $('head').append('<link href="/Scripts/css/other/mozilla.css" rel="stylesheet"/>');
    }
    if (navigator.userAgent.toLowerCase().indexOf('ie') > 1) {
        //require(['historis'], function () {});
        $('head').append('<link href="/Scripts/css/other/ie.css" rel="stylesheet"/>');
        $('head').append('<link href="/Scripts/css/other/ie9.css" rel="stylesheet"/>');
    }

    $.fn.select2.defaults.set("theme", "bootstrap");

    var startup = new Startup();
    startup.Start(function() {
        window.Router = new Router();
        window.Router.start();
    });
});
