define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var template = require('text!./template.html');
    var commonFunction = require('commonfunction');
    var eventAggregator = require('eventaggregator');
    var Table = require('./table/table');
    var Collection = require('./collection');
    var Paging = require('paging');
    var Model = require('./model');
    var CollectionMatrix = require('./CollectionMatrix');
    var CollectionColorComment = require('./../comment/collection');
    var ModelMatrix = require('./modelMatrix');
    var ModelColorComment = require('./../comment/model');
    var TableRisk = require('./../risk/table/table');
    var CollectionRisk = require('./../risk/collection');

    module.exports = LayoutManager.extend({
        className: 'container-fluid main-content tbl bg-white',
        template: _.template(template),
        events : {
         'click [name="add"]':'add'
        },
        initialize: function() {
            var self = this;
            this.model = new Model();
            this.modelMatrix = new ModelMatrix();
            this.modelColorComment = new ModelColorComment();

            this.collection = new Collection();
            this.collection.fetch();
            this.collectionMatrix = new CollectionMatrix();
            this.collectionColorComment = new CollectionColorComment();

            this.table = new Table({
                collection: new Collection()
            });
            this.paging = new Paging({
                collection: this.table.collection
            });
            this.listenTo(eventAggregator, 'master/functional_risk/add:fecth', function() {
              self.fetchData();
            });
            this.listenTo(eventAggregator, 'master/functional_risk/edit:fecth', function() {
             self.fetchData();
            });
            this.listenTo(eventAggregator, 'master/functional_risk/delete:fecth', function(model) {
              self.fetchData();
            });

            this.once('afterRender', () => {
                // this.model.set(this.model.idAttribute, this.parentId);
                // this.modelMatrix.fetch();

                // this.table.collection.fetch({
                //     reset: true,
                //     data: {
                //         ParentId: this.parentId
                //     }
                // });

            });

            this.collectionMatrix.fetch({
                reset: true,
                data: {
                    IsPagination: false
                }
            });

            
            // this.collectionColorComment.fetch({
            //     success: function(collection, response) {
            //     _.each(collection.models, function(model) {
            //       console.log(model.toJSON());
            //     })
            //     }
            // });
            // this.collectionColorComment.fetch();
            
        },
        afterRender: function() {
            var self = this;
            
            // this.formStage();
            this.$('[obo-table]').append(this.table.el);
            this.table.render();
            this.insertView('[obo-paging]', this.paging);
            this.paging.render();
            this.fetchData();
            // this.fetchMatrix();
            this.formMatrix();
            
        },
        fetchData: function() {
          this.table.collection.fetch({
            reset:true
          })
        },
        // fetchMatrix: function() {
        //   this.collectionMatrix.fetch({
        //     reset:true,
        //     IsPagination : false
        //   })
        // },

        formMatrix: function() {
          var self = this;
        if(this.collectionMatrix){
            for (var i = 0; i < this.collectionMatrix.length; i++){
                var NamaMatrix = this.collectionMatrix.models[i].attributes.NamaMatrix;
                var NamaFormulas = this.collectionMatrix.models[i].attributes.NamaFormula;
                var html = '<div class="form-group">'
                html += '<tr>'
                html += '<td>NamaMatrix</td>'
                html += '<td>NamaFormula</td>'
                html += '<td><input type="text" value="" class="form-control" name="Scenario1"></td>'
                html += '<td><input type="text" value="" class="form-control" name="Scenario2"></td>'
                html += '<td><input type="text" value="" class="form-control" name="Scenario3"></td>'
                html += '<td><input type="text" value="" class="form-control" name="Scenario1"></td>'
                html += '<td><input type="text" value="" class="form-control" name="Scenario2"></td>'
                html += '<td><input type="text" value="" class="form-control" name="Scenario3"></td>'
                html += '<td><input type="text" value="" class="form-control" name="Scenario1"></td>'
                html += '<td><input type="text" value="" class="form-control" name="Scenario2"></td>'
                html += '<td><input type="text" value="" class="form-control" name="Scenario3"></td>'
                html += '</tr>'

                self.$('[tab-contentMatrix]').append(html);
              }
            }
        },
        add : function(){
          var self = this;
          require(['./add/view'], function(View) {
            commonFunction.setDefaultModalDialogFunction(self, View);
          });
        }
    });
});
