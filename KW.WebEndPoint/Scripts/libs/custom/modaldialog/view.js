// modaldialogeditfilter
define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    // var template = require('text!./template.html');

    module.exports = LayoutManager.extend({
        className: 'modal fade',
        // template: _.template(template),
        attributes: {
            tabindex: '-1',
            role: 'dialog',
            'aria-labelledby': 'modalDialogEditFilter',
            'aria-hidden': 'true'
        },
        initialize: function(options) {
            this.fieldsShowed = options.viewFilter.fieldsShowed;
        },
        events: {
            'change input[type="checkbox"]': 'selectedFilter',
            'click [obo-save]': 'doSaveFilter'
        },
        afterRender: function() {
            for (var i = 0; i <= this.viewFilter.numberFieldsShowed; i++) {
                var selector = this.fieldsShowed[i - 1];
                this.$('input[type="checkbox"][name="' + selector + '"]').prop('checked', 'checked');
            }
            this.setDisabledFilter();
        },
        selectedFilter: function(e) {
            if (e.currentTarget) {
                var selector = e.currentTarget;
                if ($(selector).prop('checked')) {
                    this.fieldsShowed.push($(selector).attr('name'));
                } else {
                    this.fieldsShowed = _.without(this.fieldsShowed, $(selector).attr('name'));
                }
                this.setDisabledFilter();
            }
        },
        doSaveFilter: function() {
            this.viewFilter.fieldsShowed = this.fieldsShowed;
            this.viewFilter.showFilterFields();
            this.$el.modal('hide');
        },
        setDisabledFilter: function() {
            if (this.fieldsShowed.length >= this.viewFilter.numberFieldsShowed) {
                this.$('input[type="checkbox"]:not(:checked)').attr('disabled', 'disabled');
            } else {
                this.$('input[type="checkbox"]:not(:checked)').removeAttr('disabled');
            }

            if (this.fieldsShowed.length == this.viewFilter.numberFieldsShowed) {
                this.$('[obo-save]').removeAttr('disabled');
                this.$('[obo-save]').removeAttr('title');
            }else{
                this.$('[obo-save]').attr('disabled', 'disabled');
                this.$('[obo-save]').attr('title', 'please pick 5 filters first');
            }
        }
    });
});
