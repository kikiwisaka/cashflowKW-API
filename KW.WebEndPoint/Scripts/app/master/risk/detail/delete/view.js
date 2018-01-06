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
            commonFunction.responseSuccessUpdateAddDelete('Risk Event successfully deleted.');
            self.$el.modal('hide');
            eventAggregator.trigger('master/risk/detail/delete:fecth');
          });
        },
        events: {
          'click [name="delete"]':'getConfirmation'
        },
        getConfirmation: function(){
          var data = this.model.get('KodeRisk');
          var action = "delete";
          var retVal = confirm("Are you sure want to " + action + " Risk Event : "+ data +" ?");
          if( retVal == true ){
             this.doDelete();
          }
          else{
            this.$('[type="submit"]').attr('disabled', false);
          }
        },
        doDelete: function() {
          this.model.destroy();
        }
    });
});
