define(function(require, exports, module) {
    'use strict';
    var View = require('modaldialogdefault');
    var template = require('text!./template.html');
    var Model = require('./../model');
    var commonFunction = require('commonfunction');
    var commonConfig = require('commonconfig');
    var eventAggregator = require('eventaggregator');

    module.exports = View.extend({
        template: _.template(template),
        initialize : function(){
          var self = this;
          this.listenTo(this.model, 'sync', function() {
            commonFunction.responseSuccessUpdateAddDelete('Color Comment successfully deleted.');
            self.$el.modal('hide');
            eventAggregator.trigger('master/comment/delete:fecth');
          });
        },
        events: {
          'click [name="delete"]':'doDelete'
        },
        doDelete: function() {
          this.model.destroy();
        }
    });
});
