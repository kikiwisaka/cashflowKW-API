define(function(require, exports, module) {
    'use strict';

    var Backbone = require('backbone');
    var commonFunction = require('commonfunction');

    require('backbone.subroute');

    var createFnSetContentView = function(options){
        var opt = options;
        this.setContentView = function(pathViewFile, replaceMainContent, options){
            var hashtag = opt.hashtag + ((options && options.appendHashTag) || pathViewFile);
            opt.require(['./' + pathViewFile + '/view'], function(View) {
                var view = new View();

                if (replaceMainContent) {
                    commonFunction.setContentViewWithNewModuleView(view, hashtag);
                } else {
                    var previousContentView = commonFunction.getContentView();
                    var currentContentView = commonFunction.getContentView().getView('');
                    var name = previousContentView.getView('') && previousContentView.getView('').name;

                    if (name != opt.name) {
                        var mainView = new opt.MainView();
                        mainView.setView('[obo-content]', view);
                        commonFunction.setContentViewWithNewModuleView(mainView, hashtag);
                    } else {
                        currentContentView.getView('[obo-menu]').doActiveButton();
                        currentContentView.setView('[obo-content]', view);
                        view.render();
                    }
                }
            });
        }
    }

    module.exports = Backbone.SubRoute.extend({
        initialize: function(options) {
            this.app = {};

            if (options && options.fnSetContentViewOptions){
                createFnSetContentView.call(this, options.fnSetContentViewOptions);
            }
        }
    });
});
