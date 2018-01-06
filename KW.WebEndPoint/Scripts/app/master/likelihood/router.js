define(function(require, exports, module) {
  'use strict';
  var Subroute = require('backbone.subroute');
  var commonFunction = require('commonfunction');

  var fnSetContentView = function(pathViewFile) {
    console.log('masuk ke set content likelihood cuy');
      var hashtag = '#likelihood';
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
      fnSetContentView('.');
    },
    showDetail: function(){
      console.log('masuk show detail likelihood');
      fnSetContentView('./detail');
    }
  });
});
