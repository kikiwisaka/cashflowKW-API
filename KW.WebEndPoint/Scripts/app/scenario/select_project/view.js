define(function(require, exports, module) {
  'use strict';
  var View = require('modaldialogdefault');
  var template = require('text!./template.html');
  var commonFunction = require('commonfunction');
  var Table = require('./table/table');
  var Collection = require('./collection');
  var Paging = require('paging');
  var eventAggregator = require('eventaggregator');


  module.exports = View.extend({
    template: _.template(template),
    initialize: function(options) {
      var self = this;
      var ids = null;
      var projectIds = [];
      if(options.model.get('ProjectSelected')){
        ids = options.model.get('ProjectSelected');
        _.each(ids, function(item){
          projectIds.push(item);
        });
      } else {
        ids = options.model.get('Project');
        _.each(ids, function(item){
          projectIds.push(item.Id);
        });
      }

      this.table = new Table({
        collection: new Collection()
      });
      var originalParse = this.table.collection.parse;
      this.table.collection.parse = function(response) {
        var result = originalParse.call(this, response);
        if(projectIds.length > 0){
          _.each(result, function(item){
            var found = _.find(projectIds, (id) => {
              return id == item.Id
            });
            item.isChecked = Boolean(found);
          });
          return result;
        } else {
          _.each(result, function(item) {
            var selectedId = self.model.get('ProjectSelected') || [];
            
            var found = _.find(selectedId, (id) => {
              return id == item.Id
            });
            item.isChecked = Boolean(found);
          });
          return result;
        }
      }

      this.paging = new Paging({
        collection: this.table.collection
      });
      this.on('cleanup', function() {
        this.table.destroy();
        this.modalDialog && this.modalDialog.remove && this.modalDialog.remove();
      }, this)
    },
    afterRender: function() {
      this.$('[project-table]').append(this.table.el);
      this.table.render();

      this.table.collection.fetch({
        reset: true,
        data: {
          IsPagination: false,
          PageSize: 100
        }
      });
      this.setTemplate();
    },
    events: {
      // 'click [name="allProject"]': 'selectRemoveAllRisk',
      'click [btn-save-chosen-risk]': 'getChosenRisk'
    },
    setTemplate: function() {
      
    },
    selectRemoveAllRisk: function() {
      var status = this.$('[name="allProject"]').prop('checked')
      if (status) {
        this.$('input[type="checkbox"]').prop('checked', true);
      } else {
        this.$('input[type="checkbox"]').prop('checked', false);
      }
    },
    getChosenRisk: function() {
      var collection = this.table.collection.filter((model) => {
        return model.get('isChecked');
      });
      this.model.set('ProjectSelected', collection.map(
        (model) => {
          return model.get('Id')
        }))
      this.model.set('ProjectNameSelected', collection.map(
        (model) => {
          return model.get('NamaProject')
        }))
      eventAggregator.trigger('scenario/add/select_project:project_selected', this.model.get('ProjectNameSelected'), this.model.get('ProjectSelected'));
      this.$el.modal('hide');
    }
  });
});