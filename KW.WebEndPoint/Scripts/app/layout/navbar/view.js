define(function(require, exports, module) {
    'use strict';
    var LayoutManager = require('layoutmanager');
    var Model = require('backbone.model');
    var template = require('text!./template.html');

    var commonConfig = require('commonconfig');
    var commonFunction = require('commonfunction');
    var Cookies = require('Cookies');
    var cswall = require('sweetalert');

    module.exports = LayoutManager.extend({
        el: false,
        initialize: function() {
            var NewModel = Model.extend({
                urlRoot: 'Logout'
            });
            this.model = new NewModel();

            this.listenTo(this.model, 'sync', function() {
                cswall.close();
                window.location.hash = 'login';

            });
        },
        template: _.template(template),
        events: {
            'click .navbar-collapse a[href]:not([href="#"])': 'bindingEventNagivate',
            'click [topnav] li':'clickTopNav'
        },
        clickTopNav : function(){
          console.log('clickTopNav');
        },
        afterRender: function() {
            var self = this;
            if (navigator.userAgent.toLowerCase().indexOf('ie') > 1 || navigator.userAgent.toLowerCase().indexOf('firefox') > 1) {
                $.Bootstrap.pushMenu.activate("[data-toggle='offcanvas']");
            }
            var userName = Cookies.get(commonConfig.cookieFields.userName);
            if (userName) {
                this.$('[name="firstName"]').text(userName);
            }
            this.renderPopHover();
            this.$('[name="logout"]').click(function() {
              self.doLogout()
            });
        },
        bindingEventNagivate: function(e) {
            e.preventDefault();
            var href = $(e.currentTarget).attr('href');

            var ret = Backbone.history.navigate(href, true);

            if (ret === undefined) {
                Backbone.history.loadUrl(href);
            }
        },
        renderPopHover : function(){
          $('#profile-popover').popover({
            container: 'html',
            html : true,
            content: function() {
              return $('#profile-content').html();
            }
          });
          //disini mungkin taruh event logout

        },
        doLogout: function() {
            var self = this;
            console.log('logout click');
            swal({
                    title: "Are you sure you want to log out?",
                    text: "Press Cancel if you want to continue work. </br> Or Press Yes to logout current user.",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonClass: "btn-info",
                    confirmButtonText: "Yes",
                    closeOnConfirm: false,
                    html: true
                },
                function() {
                    _.each(commonConfig.cookieFields, function(item) {
                        Cookies.remove(item);
                    });

                    self.model.save();
                });
        }
    });
});
