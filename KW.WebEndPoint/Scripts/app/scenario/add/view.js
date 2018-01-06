define(function(require, exports, module) {
    'use strict';
    var View = require('modaldialogdefault');
    var template = require('text!./template.html');
    var commonFunction = require('commonfunction');
    var Model = require('./../model');
    var CollectionLikehoodDetail = require('./../select_likelihood/detail/collection');
    var Table = require('./../select_likelihood/detail/table/table');
    var eventAggregator = require('eventaggregator');
    require('bootstrap-validator');
    require('jquerymask');

    module.exports = View.extend({
        template: _.template(template),
        initialize: function() {
          var self = this;
          this.table = new Table({
            collection: new CollectionLikehoodDetail()
          });

          this.model = new Model();

          this.listenTo(this.model, 'request', function() {});
          this.listenTo(this.model, 'sync error', function() {});
          this.listenTo(this.model, 'sync', function(model) {
            commonFunction.responseSuccessUpdateAddDelete('Scnerio successfully created.');
            self.$el.modal('hide');
            eventAggregator.trigger('scenario/add:fecth');
          });
          
          this.listenTo(eventAggregator, 'scenario/add/select_likelihood:likelihood_selected', function(obj) {
            self.modelLikehood = obj;
            self.renderLikelihoodDetail();
          });
          this.listenTo(eventAggregator, 'scenario/add/select_project:project_selected', function(projectNames, projectIds) {
            self.setTemplate(projectNames);
            self.projectIds = projectIds;
          });
        },
        afterRender: function() {
          this.renderValidation();
        },
        events: {
          'click [name="SelectProject"]': 'selectProject',
          'click [name="EditSelectProject"]': 'selectProject',
          'click [name="SelectLikelihood"]': 'selectLikelihood'
        },
        renderLikelihoodDetail: function(){
          var likehoodId = this.modelLikehood.Id;
          if(likehoodId){
            this.$('[likelihood-table]').append(this.table.el);
              this.table.render();
              this.table.collection.fetch({
              reset: true,
              data: {
                ParentId: likehoodId,
                PageSize: 100
              }
            });
          }
          var likehoodName = this.modelLikehood.NamaLikehood;
          this.$('[name="NamaLikehood"]').val(likehoodName);
        },
        setTemplate: function(projectNames) {
          var chosenName = projectNames.join(", ");
          var currentChosenProject = this.$('[name="ChosenProject"]').text(chosenName);
          if(this.$('[name="ChosenProject"]').val()){
            this.$('[name="EditSelectProject"]').removeClass('hidden');
            this.$('[name="SelectProject"]').addClass('hidden');
          }
        },
        selectProject: function(){
          require(['./../select_project/view'], (View) => {
            commonFunction.setDefaultModalDialogFunction(this, View, this.model);
          });
        },
        selectLikelihood: function() {
          require(['./../select_likelihood/view'], (View) => {
            commonFunction.setDefaultModalDialogFunction(this, View, this.model);
          });
        },
        renderValidation: function() {
          var self = this;
          this.$('[ehs-form]').bind("keypress", function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
          this.$('[ehs-form]').bootstrapValidator({
              fields: {
                NamaScenario: {
                  validators:{
                    stringLength: {
                        message: 'Nama scenario must be less than 100 characters',
                        max: 100
                    },
                    notEmpty:{
                      message: 'Nama scenario is required'
                    }
                  }
                },
                ChosenProject: {
                  validators: {
                    notEmpty: {
                      message: 'Projects is required'
                    }
                  }
                },
                NamaLikehood: {
                  validators: {
                    notEmpty: {
                      message: 'Likelihood is required'
                    }
                  }
                }
              }
            })
            .on('success.form.bv', function(e) {
              e.preventDefault();
              self.getConfirmation();
            });
        },
        getConfirmation: function(){
          var data = this.$('[name="NamaScenario"]').val();
          var action = "add";
          var retVal = confirm("Are you sure want to " + action + " Scenario : "+ data +" ?");
          if( retVal == true ){
             this.doSave();
          }
          else{
            this.$('[type="submit"]').attr('disabled', false);
          }
        },
        doSave: function() {
          var data = commonFunction.formDataToJson(this.$('form').serializeArray());
          data.ProjectId = this.projectIds;
          data.LikehoodId = this.modelLikehood.Id;
          this.model.save(data);
        }
    });
});
