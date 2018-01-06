var pathLibs = '/scripts/libs/';
var pathLibsCustom = pathLibs + 'custom/';
var pathLibsOther = pathLibs + 'other/';

require.config({
    paths: {
        pathLibsCustom: pathLibsCustom,
        'async': 'lib/async-0.1.2',
        'jquery': [
            // 'https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min',
            // 'https://cdnjs.cloudflare.com/ajax/libs/jquery/2.2.4/jquery.min',
            pathLibs + 'jquery-2.2.4.min'
        ],
        jquerymask : pathLibs + 'jquery.mask.min',
        'underscore': [
            // 'https://cdn.jsdelivr.net/underscorejs/1.8.3/underscore-min',
            // 'https://cdnjs.cloudflare.com/ajax/libs/underscore.js/1.8.3/underscore-min',
            pathLibs + 'underscore-1.8.3.min'
        ],
        //'backboneOriginal': pathLibsCustom + 'backbone.custom-1.3.3',
        'backbone.custom': pathLibsCustom + 'backbone.custom-1.3.3',
        'backbone.subroute': pathLibsCustom + 'backbone.subroute-0.4.6',
        //'backbone.override': pathLibsCustom + 'backbone.override',
        'backbone': pathLibsCustom + 'backbone.override',
        'layoutmanager.original': [
            pathLibs + 'backbone.layoutmanager-1.0.0.min'
        ],
        'layoutmanager': [
            pathLibsCustom + 'backbone.layoutmanager.override'
        ],
        'respond': [
            // 'https://cdn.jsdelivr.net/respond/1.4.2/respond.min',
            // 'https://cdnjs.cloudflare.com/ajax/libs/respond.js/1.4.2/respond.min',
            pathLibs + 'respond-1.4.2.min'
        ],
        'backbone.radio': [
            // 'https://cdnjs.cloudflare.com/ajax/libs/backbone.radio/2.0.0/backbone.radio.min',
            pathLibs + 'backbone.radio-2.0.0.min'
        ],
        'marionette': [
            // 'https://cdnjs.cloudflare.com/ajax/libs/backbone.marionette/3.1.0/backbone.marionette.min',
            //pathLibs + 'backbone.marionette-3.1.0.min'
            pathLibs + 'backbone.marionette-3.1.0'
        ],
        'moment': [
            // 'https://cdn.jsdelivr.net/momentjs/2.10.6/moment.min',
            // 'https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.10.6/moment.min',
            pathLibs + 'moment-2.17.1'
        ],
        'deep-model': pathLibs + 'deep.model-0.10.4.min',
        'jquery-ui': pathLibs + 'jquery-ui-1.12.1.min',
        'recaptcha': 'https://www.google.com/recaptcha/api',
        //'require': pathLibs + 'require-2.3.2.min',
        'require': 'https://cdnjs.cloudflare.com/ajax/libs/require.js/2.3.2/require.js',
        'text': pathLibs + 'requirejs.text-2.0.12.min',
        'simplebar': pathLibs + 'simplebar-1.1.9.min',
        'bootstrap': pathLibs + 'bootstrap.min',
        'settings': pathLibs + 'settings',
        'actions': pathLibs + 'actions',
        'mCustomScrollbar': pathLibsCustom + 'jquery.mCustomScrollbar.min',
        'select2': pathLibs + 'select2-4.0.3.full',
        'Cookies': pathLibs + 'js.cookie-2.1.3.min',
        'datetimepicker': pathLibs + 'bootstrap-datetimepicker-3-v4.17.46',
        'sweetalert': pathLibs + 'sweetalert-1.0.1.min',
        'ie9': pathLibsOther + 'IE9',
        'historis': pathLibsOther + 'jquery.history.min',
        'tabletbody': pathLibsCustom + 'table/tbody',
        'eventaggregator': pathLibsCustom + 'eventaggregator',
        'commonconfig': pathLibsCustom + 'commonconfig',
        'commonfunction': pathLibsCustom + 'commonfunction',
        'backbone.model': pathLibsCustom + 'backbone.model',
        'backbonecollection': pathLibsCustom + 'backbonecollection',
        'backbonemodel': pathLibsCustom + 'backbonemodel',
        'backbone.collection': pathLibsCustom + 'backbone.collection',
        'backbone.collection.paging': pathLibsCustom + 'backbone.collection.paging',
        'backbone.model.file.upload': pathLibsCustom + 'backbone.model.file.upload-1.0.0',
        'bootstrap-validator': pathLibsCustom + 'bootstrapValidator-0.5.2',
        'paging': pathLibsCustom + 'paging/view',
        'paging2': pathLibsCustom + 'paging2/view',
        'sorting.button': pathLibsCustom + 'sorting.button/view',
        'filter': pathLibsCustom + 'filter/view',
        'modaldialogeditfilter': pathLibsCustom + 'modaldialogeditfilter/view',
        'modaldialogdefault':pathLibsCustom + 'modaldialogdefault/view',
        'modaldialogsecond':pathLibsCustom + 'modaldialogsecond/view',
        'defaultmodule': pathLibsCustom + 'defaultmodule/view',
        'defaultmodule-2': pathLibsCustom + 'defaultmodule/view-2',
        'table-2.table': pathLibsCustom + 'table-2/table',
        'table-2.tbody': pathLibsCustom + 'table-2/tbody',
        'menu': pathLibsCustom + 'menu/view',
        'subroute': pathLibsCustom + 'subroute/router',
        'spin': pathLibs + 'spin',
        'ladda': pathLibs + 'ladda-1.0.0',
        'ladda.jquery': pathLibsCustom + 'ladda.jquery-1.0.0',
        'maxlength': pathLibs + 'jquery.maxlength-2.0.1.min',
        'jquery.plugin': pathLibs + 'jquery.plugin-2.0.1.min',
        'jquery.cropit': pathLibs + 'jquery.cropit-0.5.1',
        icheck : pathLibs + 'icheck.min',
        tagsinput : pathLibs + 'bootstrap-tagsinput.min',
        'bootstrap-paginator': pathLibs + 'bootstrap-paginator',
        'dayschedule':pathLibsCustom + 'dayschedule',
        'jquery.simplePagination': pathLibs + 'jquery.simplePagination',
        'jquery.smoothState': pathLibs + 'jquery.smoothState.min',
        'highcharts': pathLibs + 'highcharts'
    },
    shim: {
        jquery: {
            exports: 'jQuery'
        },
        simplebar: {
            deps: ['jquery']
        },

        historis: {
            deps: ['jquery']
        },
        commonfunction: {
            deps: ['jquery']
        },

        underscore: {
            exports: '_'
        },
        'text': {
            deps: ['require']
        },
        bootstrap: {
            exports: '$.bootstrap'
        },
        select2: {
            deps: ['bootstrap']
        },
        backbone: {
            deps: ['backbone.custom', 'bootstrap', 'underscore', 'text'],
            //deps: ['bootstrap', 'underscore', 'text'],
            exports: 'Backbone'
        },
        'backbone.subroute': {
            deps: ['backbone']
        },
        'layoutmanager.original': {
            deps: ['backbone'],
            exports: 'LayoutManager'
        },
        layoutmanager: {
            deps: ['layoutmanager.original']
        },
        'backbone.radio': {
            deps: ['backbone'],
            exports: 'backbone.radio'
        },
        'deep-model': {
            deps: ['underscore', 'backbone']
        },
        'backbone.model.file.upload': {
            deps: ['backbone'],
            exports: 'Backbone'
        },
        marionette: {
            deps: ['backbone.radio'],
            exports: 'Marionette'
        },
        settings : {
          deps: ['jquery','mCustomScrollbar']
        },
        actions : {
          deps: ['settings']
        },
        mCustomScrollbar : {
          deps: ['jquery']
        },
        tagsinput: {
            deps: ['jquery']
        },
        datetimepicker: {
            deps: ['jquery']
        },
        sweetalert: {
            deps: ['jquery']
        },
        'jquery.panzoom': {
            deps: ['jquery']
        },
        'bootstrap-validator': {
            deps: ['jquery']
        },
        spin: {
            deps: ['jquery'],
            exports: 'Spinner'
        },
        ladda: {
            deps: ['spin'],
            exports: 'Ladda'
        },
        'ladda.jquery': {
            deps: ['jquery', 'ladda'],
            init: function(jquery, Ladda) {
                if (!window.Ladda)
                    window.Ladda = Ladda;
            }
        },
        'jquery.plugin': {
            deps: ['jquery']
        },
        maxlength: {
            deps: ['jquery', 'jquery.plugin']

        },
        'jquery-ui': {
            deps: ['jquery']
        },
        'jquery.cropit': {
            deps: ['jquery']
        },
        'bootstrap-slider': {
            deps: ['jquery']
        },
        icheck: {
            deps: ['jquery'],
            exports: 'icheck'
        },
        twbsPagination : {
          deps: ['jquery']
        },
        dayschedule : {
          deps: ['jquery']
        },
        'jquery.simplePagination': {
          deps: ['jquery']
        },
        'jquery.smoothState': {
          deps: ['jquery']
        },
        highcharts : {
          deps : ['jquery']
        }
    },
    callback: function(require) {
        requirejs(['commonfunction'], function(commonFunction) {
            commonFunction.checkCookieAuthorization().then(function() {
                requirejs(['main']);
            }, function() {
                window.location.hash = 'login';
                requirejs(['main']);
            });
        });
    }
});
