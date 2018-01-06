define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var template = require('text!./template.html');
    require('settings')
    module.exports = LayoutManager.extend({
      className: 'container-fluid main-content',
        template: _.template(template),
        initialize : function(){
          console.log('initialize');
        }
    });
});
