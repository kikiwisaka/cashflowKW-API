define(function(require, exports, module) {
  'use strict';
  var Subroute = require('backbone.subroute');
  var commonFunction = require('commonfunction');
  var eventAggregator = require('eventaggregator');

  var fnSetContentView = function(pathViewFile) {
    console.log('masuk ke overall comment');
      var hashtag = '#overall_comment';
      require([pathViewFile + '/view'], function(View) {
          if (View)
              commonFunction.setContentViewWithNewModuleView(new View(), hashtag);
      });
  };

  module.exports = Backbone.SubRoute.extend({
    initialize: function() {
      this.app = {};
    },
    routes: {
      '/': 'showList',
      ':id': 'showDetail',
    },
    showList: function() {
      console.log('masuk show list overall comment');
      fnSetContentView('.');
    },
    showDetail: function(){
      fnSetContentView('./detail');
    }
  });
});
