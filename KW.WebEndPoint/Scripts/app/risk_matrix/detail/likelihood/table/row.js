define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var template = require('text!./row.html');
    var commonFunction = require('commonfunction');
    var eventAggregator = require('eventaggregator');

    module.exports = LayoutManager.extend({
        tagName: 'tr',
        template: _.template(template),
        events: {
            'click td': 'clickTr',
        },
        clickTr : function(){
          eventAggregator.trigger('scenario/add/select_likelihood:likelihood_selected', this.model.toJSON());
          this.$el.modal('hide');
        }
        
    });
});
