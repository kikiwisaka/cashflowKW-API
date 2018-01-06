define(function(require, exports, module) {
  'use strict';
  var Backbone = require('backbone');
  var Model = require('./model');
  var commonFunction = require('commonfunction');
  require('backbone.subroute');
  var eventAggregator = require('eventaggregator');
  var fnSetContentView = function(pathViewFile, replaceMainContent) {
    var hashtag = '#functional_risk';
    require(['./' + pathViewFile + '/view'], function(View) {
      var view = new View();
      if (replaceMainContent) {
        commonFunction.setContentViewWithNewModuleView(view, hashtag);
      } else {
        var previousContentView = commonFunction.getContentView();
        var currentContentView = commonFunction.getContentView().getView('');
        var name = previousContentView.getView('') && previousContentView.getView('').name;
        if (name != 'functional_risk') {
          var MainView = require('./view');
          var mainView = new MainView();
          commonFunction.setContentViewWithNewModuleView(mainView, hashtag);
          currentContentView = mainView;
        } else {
        }
        if (pathViewFile != '.')
          currentContentView.setView('[obo-content]', view);
      }
    });
  };

  module.exports = Backbone.SubRoute.extend({
    initialize: function() {
      this.model = new Model();
      this.stopListening(eventAggregator, 'getEmployeeModel');
      this.listenTo(eventAggregator, 'getEmployeeModelOnly', function(getModel) {
        getModel(this.model);
      });
      this.listenTo(eventAggregator, 'getEmployeeModel', function(callback, getModel) {
        var id = commonFunction.getUrlHashSplit(3);
        if (getModel) {
          getModel(this.model);
        }
        if (!this.model.id || (this.model.id != id)) {
          this.model.set(this.model.idAttribute, id);
          this.model.once('sync', function(model) {
            callback(model);
          });
          this.model.fetch();
        } else if (this.model.requestToServer) {
          this.model.once('sync', function(model) {
            callback(model)
          });
        } else {
          callback(this.model);
        }
      });
    },
    routes: {
      '/': 'showList',
      'detail/:id': 'redirectToDetailModule',
      'detail/:id/*subrouter': 'redirectToDetailModule'
    },
    showList: function() {
      fnSetContentView('.');
    },
    redirectToDetailModule: function(id, subrouter) {
      var self = this;
      require(['./detail/router'], function(Router) {
        if (!self.app.detailRouter) {
          self.app.detailRouter = new Router(self.prefix + '/detail/:id', {
            createTrailingSlashRoutes: true
          });
        }
      });
    },
  });
});
