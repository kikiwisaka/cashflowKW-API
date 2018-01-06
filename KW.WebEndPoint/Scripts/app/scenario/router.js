define(function(require, exports, module) {
  'use strict';
  var Subroute = require('backbone.subroute');
  var commonFunction = require('commonfunction');

  var fnSetContentView = function(pathViewFile) {
    console.log('masuk ke set content scenario cuy');
      var hashtag = '#scenario';
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
      console.log('masuk show list scenario');
      fnSetContentView('.');
    },
    showDetail: function(){
      console.log('masuk show detail scenario');
      fnSetContentView('./detail');
    }
  });
});
