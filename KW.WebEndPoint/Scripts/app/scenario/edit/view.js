define(function (require, exports, module) {
  'use strict';
  var View = require('modaldialogdefault');
  var template = require('text!./template.html');
  var Model = require('./../model');
  var commonFunction = require('commonfunction');
  var commonConfig = require('commonconfig');
  var eventAggregator = require('eventaggregator');
  var Model = require('./../model');
  var CollectionLikehoodDetail = require('./../select_likelihood/detail/collection');
  var Table = require('./../select_likelihood/detail/table/table');
  require('bootstrap-validator');
  require('jquerymask');

  module.exports = View.extend({
    template: _.template(template),
    initialize: function () {
      var self = this;
      var scenarioId = this.model.get('Id');
      this.likelihoodId = this.model.get('LikehoodId');
      this.scenario = this.model;
      this.projectIds = [];
      if (this.model.get('Project').length) {
        _.each(this.model.get('Project'), function (item) {
          self.projectIds.push(item.Id);
        });
      }
      this.model = new Model();
      this.model.set(this.model.idAttribute, scenarioId);
      this.table = new Table({
        collection: new CollectionLikehoodDetail()
      });
      this.listenTo(this.model, 'request', function () {});
      this.listenTo(this.model, 'sync error', function () {});
      this.listenToOnce(this.model, 'sync', function (model) {
        this.render();
        var data = model.toJSON();
        this.listenTo(this.model, 'sync', function () {
          commonFunction.responseSuccessUpdateAddDelete('Scenario successfully updated.');
          self.$el.modal('hide');
          eventAggregator.trigger('scenario/edit:fecth');
        });
      }, this);
      this.listenTo(eventAggregator, 'scenario/add/select_likelihood:likelihood_selected', function (obj) {
        self.likelihoodId = obj.Id;
        self.renderLikelihoodDetail();
      });
      this.listenTo(eventAggregator, 'scenario/add/select_project:project_selected', function (projectNames, projectIds) {
        self.renderProjectAfterEdit(projectNames);
        self.projectIds = projectIds;
      });
      this.once('afterRender', function () {
        this.model.fetch();
      });
    },
    afterRender: function () {
      this.$('[likelihood-table]').append(this.table.el);
      this.table.render();
      this.table.collection.fetch({
        reset: true,
        data: {
          ParentId: this.likelihoodId,
          PageSize: 100
        }
      });
      this.setTemplate(this.scenario);
      this.renderValidation();
    },
    events: {
      'click [name="SelectProject"]': 'selectProject',
      'click [name="EditSelectProject"]': 'selectProject',
      'click [name="SelectLikelihood"]': 'selectLikelihood'
    },
    renderLikelihoodDetail: function () {
      var likehoodId = this.modelLikehood.Id;
      if (likehoodId) {
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
    renderProjectAfterEdit: function (projectNames) {
      var chosenName = projectNames.join(", ");
      var currentChosenProject = this.$('[name="ChosenProject"]').text(chosenName);
      if (this.$('[name="ChosenProject"]').val()) {
        this.$('[name="EditSelectProject"]').removeClass('hidden');
        this.$('[name="SelectProject"]').addClass('hidden');
      }
    },
    setTemplate: function (obj) {
      var projectNames = '';
      var likelihoodName = obj.get('NamaLikehood');
      var project = obj.get('Project');
      if (project.length > 0) {
        for (var i = 0; i < project.length; i++) {
          projectNames += project[i].NamaProject;
          projectNames += ', ';
        }
      }
      this.$('[name="ChosenProject"]').text(projectNames);
      this.$('[name="NamaLikehood"]').val(likelihoodName);
    },
    selectProject: function () {
      require(['./../select_project/view'], (View) => {
        commonFunction.setDefaultModalDialogFunction(this, View, this.model);
      });
    },
    selectLikelihood: function () {
      require(['./../select_likelihood/view'], (View) => {
        commonFunction.setDefaultModalDialogFunction(this, View, this.model);
      });
    },
    renderValidation: function () {
      var self = this;
      this.$('[ehs-form]').bootstrapValidator({
          fields: {
            NamaScenario: {
              validators: {
                stringLength: {
                  message: 'Nama scenario must be less than 100 characters',
                  max: 100
                },
                notEmpty: {
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
        .on('success.form.bv', function (e) {
          e.preventDefault();
          self.getConfirmation();
        });
    },
    getConfirmation: function(){
      var data = this.$('[name="NamaScenario"]').val();
      var action = "edit";
      var retVal = confirm("Are you sure want to " + action + " Scenario : "+ data +" ?");
      if( retVal == true ){
         this.doSave();
      }
      else{
        this.$('[type="submit"]').attr('disabled', false);
      }
    },
    doSave: function () {
      var data = commonFunction.formDataToJson(this.$('form').serializeArray());
      data.ProjectId = this.projectIds;
      data.LikehoodId = this.likelihoodId;
      data.IsDefault = false;
      data.IsUpdate = true;
      this.model.save(data);
    }
  });
});