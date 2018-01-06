define(function (require, exports, module) {
    'use strict';

    var Backbone = require('backbone');
    var BModel = require('backbone.model');
    var BCollection = require('backbone.collection');
    var commonConfig = require('commonconfig');
    var Cookies = require('Cookies');
    var moment = require('moment');
    var eventAggregator = require('eventaggregator');
    require('select2');
    require('sweetalert');
    require('datetimepicker');

    var _this = {
        collection: {}
    };

    var select2Option = {
        isPlaceholder: true,
        getOptionValue: function (model) {
            return model.id
        }
    }

    var aryYesNo = [{
        value: 'true',
        text: 'Yes'
    }, {
        value: 'false',
        text: 'No'
    }];

    var defaultDisplayRows = {
        medium: 50,
        high: 100
    };



    var select2Destroy = function (view, selector) {
        if (view.$(selector).hasClass('select2-hidden-accessible')) {
            view.$(selector).select2('destroy');
        }
    }

    Number.prototype.toFixedbackup = Number.prototype.toFixed;
    Number.prototype.toFixed = function (precision) {
        // BEWARE: Below method has been enhanced.
        // Dont try to modify if u dont understand. Peace ;) - JL -
        var str = Math.abs(this).toString(),
            negative = this < 0,
            precisionLength = (str.indexOf('.') === -1) ? 0 : str.length - str.indexOf('.') - 1,
            lastNumber, mult;
        str = str.substr(0, (str.indexOf('.') === -1) ? str.length : (str.indexOf('.') + precision + 2));
        lastNumber = str.charAt(str.length - 1);

        if (lastNumber >= 5 && precision < precisionLength) {
            str = str.substr(0, str.length - 1);
            mult = Math.pow(10, str.length - str.indexOf('.') - 1);
            str = (+str + 1 / mult);
        }
        return (negative ? "-" : "") + (str * 1).toFixedbackup(precision);
    };

    var setSelect2 = function (self, view, options) {
        return new Promise(function (resolve, reject) {
            var collection = options.getCollection();
            var selector = options.selector;

            var createSelect2 = function () {
                if (view.$(selector).length && !view.$(selector).hasClass('select2-hidden-accessible')) {
                    var opt = {};
                    if (options.isPlaceholder === true) {
                        opt.placeholder = options.placeholder;
                    }
                    view.$(selector).select2(opt);
                }
            };

            view.listenTo(collection, 'sync', function () {
                this.$(selector).empty().append(new Option());

                collection && _.each(collection.models, function (model) {
                    var option = new Option(options.getOptionText(model), options.getOptionValue(model));
                    if (options.newOptionHtml) {
                        option = options.newOptionHtml(options, model, option);
                    }

                    this.$(selector).append(option);
                }, view);


                if (options.defaultValue) {
                    if (options.defaultValueId) {
                        view.$(selector).val(options.defaultValueId).trigger('change');
                    } else if (options.defaultValueText) {
                        var val = view.$(selector).find('option').filter(function () {
                            return $(this).html() == options.defaultValueText
                        }).val();
                        if (val) {
                            view.$(selector).val(val).trigger('change');
                        }
                    }
                }
                resolve({
                    self: self,
                    view: view,
                    options: options,
                    collection: collection
                });
            });

            view.on('beforeRender', function () {
                this.$(selector).select2('destroy');
            });

            view.on('afterRender', function () {
                createSelect2();
                if (!collection.length) {
                    collection.fetch();
                } else {
                    collection.trigger('sync');
                }
            });

            view.on('beforeRemove cleanup', function () {
                //this.$(selector).select2('destroy');
            });

            createSelect2();
        });
    };

    var createCollection = function (name, idAttribute, url) {
        if (!_this.collection[name]) {
            var Model = BModel.extend({
                idAttribute: idAttribute
            });
            var Collection = BCollection.extend({
                model: Model
            });

            _this.collection[name] = new Collection();
            _this.collection[name].url = url;
        }
        return _this.collection[name];
    }

    module.exports = {
        setDefaultModalDialogFunction: function (self, ModalDialog, model, view) {
            self.modalDialog = new ModalDialog({
                viewFilter: self.filter,
                model: model ? model : '',
                view: view ? view : '',
            });

            self.modalDialog.remove = function () {
                self.modalDialog.trigger('remove');
                _.each(self.modalDialog.$('input.datepicker'), function (datepicker) {
                    if ($(datepicker).data('DateTimePicker')) {
                        $(datepicker).datetimepicker('destroy');
                    }
                });

                return Backbone.View.prototype.remove.apply(this, arguments);
            }

            $('body').append(self.modalDialog.el);
            self.modalDialog.$el.on('hidden.bs.modal', function () {
                self.modalDialog.remove();
            });

            self.modalDialog.once('afterRender', function () {
                self.modalDialog.$el.modal();
            });
            self.modalDialog.render();
        },
        getDayView: function (val) {
            switch (val) {
                case '0':
                    return 'Sun';
                    break;

                case '1':
                    return 'Mon';
                    break;

                case '2':
                    return 'Tue';
                    break;

                case '3':
                    return 'Wed';
                    break;

                case '4':
                    return 'Thu';
                    break;

                case '5':
                    return 'Fri';
                    break;

                case '6':
                    return 'Sat';
                    break;
            }
        },
        setContentView: function (view) {
            _this.contentView = view;
        },
        getContentView: function () {
            return _this.contentView;
        },
        setSideBarView: function (view) {
            _this.sideBarView = view;
        },
        getSideBarView: function () {
            return _this.sideBarView;
        },
        setNavBarView: function (view) {
            _this.navBarView = view;
        },
        getNavBarView: function () {
            return _this.navBarView;
        },
        setLoginView: function (view) {
            _this.loginView = view;
        },
        getLoginView: function () {
            return _this.loginView;
        },
        setContentViewWithNewModuleView: function (newModuleView, moduleActivedByUrl) {
            var self = this;
            var view = undefined;

            var fnRemoveViews = function () {
                var contentView = self.getContentView();
                if (contentView) {
                    var oldModuleView = contentView.getView('');
                    if (oldModuleView) {
                        contentView.removeView('');
                        oldModuleView.remove();
                    }
                    contentView.remove();
                    self.setContentView();
                }

                var sideBarView = self.getSideBarView();
                if (sideBarView) {
                    sideBarView.remove();
                    self.setSideBarView();
                }

                var navBarView = self.getNavBarView();
                if (navBarView) {
                    navBarView.remove();
                    self.setNavBarView();
                }
            }

            var fnRemoveLoginViews = function () {
                view = self.getLoginView();
                view && view.remove();
            }

            if (this.isLoginsHash()) {
                fnRemoveViews()

                if ($('body').hasClass('app')) {
                    $('body > .layout-content').remove();
                    $('body').append('<div class="login"></div>');
                }

                if (newModuleView) {
                    view = self.getLoginView();
                    view && view.remove();
                    view = newModuleView;
                    self.setLoginView(view);

                    $('body').removeAttr();
                    $('body > .login').append(view.el);
                    view.render();
                }

            } else {
                fnRemoveLoginViews();
                if ($('body > .login').length) {
                    $('body > .login').remove();
                    $('body').removeAttr('background').removeAttr('style');
                }

                var SideBarView = require('./layout/sidebar/view');
                var NavBarView = require('./layout/navbar/view');
                var ContentView = require('./layout/content/view');

                var contentView = self.getContentView();
                var navBarView = self.getNavBarView();
                var sideBarView = self.getSideBarView();

                if (!navBarView) {
                    navBarView = new NavBarView();
                    self.setNavBarView(navBarView);
                }

                if (!sideBarView) {
                    sideBarView = new SideBarView();
                    self.setSideBarView(sideBarView);
                }
                if (!contentView) {
                    contentView = new ContentView();
                    self.setContentView(contentView);
                }

                if ($('body > .layout-content').length === 0) {
                    $('body').addClass('app').append('<div class="layout-content"><div class="page-container"><div class="page-content"></div></div></div>');

                    requirejs(['mCustomScrollbar', 'settings', 'actions', 'icheck'], function () {});
                    $('div.page-content', document.body).prepend(contentView.render().el);
                    $('div.page-container').prepend(sideBarView.render().el);
                    $('div.page-content').prepend(navBarView.render().el);
                }


                var oldModuleView = contentView.getView('');
                if (oldModuleView) {
                    contentView.removeView('');
                    oldModuleView.remove();
                }

                contentView.setView('', newModuleView);
                newModuleView.render();

                sideBarView.$('.sidebar-menu li a.active').each(function (index, item) {
                    var domImg = $('img', item);
                    var src = domImg.attr('src') || '';
                    domImg.attr('src', src.replace('_primary.png', '.png'));
                });

                sideBarView.$('.sidebar-menu li a.active').removeClass('active');

                if (moduleActivedByUrl) {
                    var linkFound = sideBarView.$('.sidebar-menu > li  a[href="' + moduleActivedByUrl + '"]');

                    if (linkFound) {
                        linkFound.addClass('active');
                        if (linkFound.parents('li').length) {
                            var length = linkFound.parents('li').length - 1;
                            var link = linkFound.parents('li')[length];
                            $('> a', link).parent().find('> a').addClass('active');
                            var domImg = $('img', link);
                            var src = domImg.attr('src') || '';
                            domImg.attr('src', src.replace('.png', '_primary.png'));
                        }
                    }
                }
            }
        },
        dateFromUTCToLocalFormated: function (dateUTC) {
            var date = moment.utc(dateUTC);
            date = moment(date).local();
            return date.format(commonConfig.momentFormat);
        },
        createDatePickerForFilter: function (options) {
            var self = this;
            var view = options.view;
            var selector = options.selector;
            view.on('afterRender', function () {
                if (view.$(selector + ':visible').length) {
                    self.clearDatePicker(options);
                    view.$(selector).datetimepicker({
                        useCurrent: false,
                        format: commonConfig.datePickerFormat
                    }).on('dp.change', function (event) {
                        view.doFilterAndSorting && view.doFilterAndSorting(event);
                    });

                    view.once('cleanup', function () {
                        self.clearDatePicker(options);
                    });
                } else {
                    self.clearDatePicker(options);
                }
            });
        },
        createDatePicker: function (options) {
            var self = this;
            var view = options.view;
            var selector = options.selector;
            var _createDatePicker = function () {
                self.clearDatePicker(options);
                if (view.$(selector).length) {

                    view.$(selector).datetimepicker({
                        format: commonConfig.datePickerFormat,
                        defaultDate: moment().valueOf(),
                        useCurrent: true
                    });

                    view.once('cleanup', function () {
                        self.clearDatePicker(options);
                    });
                }
            }
            view.on('afterRender', function () {
                _createDatePicker();
            });

            _createDatePicker();
        },
        clearDatePicker: function (options) {
            var view = options.view;
            var selector = options.selector;
            if (view.$(selector).data('DateTimePicker') && view.$(selector).data('DateTimePicker').destroy) {
                view.$(selector).data('DateTimePicker').destroy();
            }
        },
        autoNumericInit: function (item) {
            item.autoNumeric('init', {
                vMin: 0,
                vMax: 99999999999,
                mDec: 0,
                aPad: false,
                wEmpty: 'zero'
            });
        },
        autoNumericInitAccept2Digits: function (item) {
            item.autoNumeric('init', {
                vMin: 0,
                vMax: 99999999999,
                aPad: false,
                mDec: 2,
                wEmpty: 'zero'
            });
        },
        autoNumericInitAccept2DigitsPercent: function (item) {
            item.autoNumeric('init', {
                vMin: 0,
                vMax: 99999999999,
                aPad: false,
                mDec: 2,
                aSign: ' %',
                pSign: 's',
                wEmpty: 'zero'
            });
        },
        autoNumericInitAccept2DigitsPercentLimit100: function (item) {
            item.autoNumeric('init', {
                vMin: 0,
                vMax: 100,
                aPad: false,
                mDec: 2,
                aSign: ' %',
                pSign: 's',
                wEmpty: 'zero'
            });
        },
        autoNumericInitAcceptPercent5Digits: function (item) {
            item.autoNumeric('init', {
                aPad: false,
                mDec: 5,
                pSign: 's',
                aSign: ' %',
                vMin: 0,
                vMax: 100,
                wEmpty: 'zero'
            });
        },
        autoNumericInitAcceptPercent3Digits: function (item) {
            item.autoNumeric('init', {
                aPad: false,
                mDec: 3,
                pSign: 's',
                aSign: ' %',
                vMin: 0,
                vMax: 100,
                wEmpty: 'zero'
            });
        },
        autoNumericInitAcceptPercent: function (item) {
            item.autoNumeric('init', {
                aPad: false,
                mDec: 0,
                pSign: 's',
                aSign: ' %',
                vMin: 0,
                vMax: 100,
                wEmpty: 'zero'
            });
        },
        autoNumericInitAcceptPercentAllow100: function (item) {
            item.autoNumeric('init', {
                aPad: false,
                mDec: 0,
                pSign: 's',
                aSign: ' %',
                vMin: 0,
                vMax: 99999999999,
                wEmpty: 'zero'
            });
        },
        autoNumericInitAcceptPercent3DigitsAllow100: function (item) {
            item.autoNumeric('init', {
                aPad: false,
                mDec: 3,
                pSign: 's',
                aSign: ' %',
                vMin: 0,
                vMax: 99999999999,
                wEmpty: 'zero'
            });
        },
        getURLParamsString: function () {
            return window.location.hash.split('/?')[1];
        },
        getURLParamsObj: function () {
            var param = this.getURLParamsString();
            if (param)
                return JSON.parse('{"' + decodeURI(param).replace(/"/g, '\\"').replace(/&/g, '","').replace(/=/g, '":"') + '"}');
            else
                return undefined;
        },
        getUrlParameter: function (field) {
            field = field.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\?&]" + field + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(location.href);
            return results == null ? null : results[1];
        },
        getUrlHashSplit: function (index) {
            var aryHash = window.location.hash.split('/');
            if (index)
                return aryHash[--index];
            return aryHash;
        },
        getCurrentHash: function () {
            var length = window.location.hash.split('/').length;
            return window.location.hash.split('/').slice(0, length).join('/')
        },
        getCurrentHashToLevel: function (level) {
            if (level) {
                return window.location.hash.split('/').slice(0, level).join('/');
            }
            return '';
        },
        getCurrentHashOmitSuffix: function (amountRemoveSuffix) {
            if (amountRemoveSuffix) {
                var length = window.location.hash.split('/').length;
                var totalHashChunk = length - amountRemoveSuffix;
                return window.location.hash.split('/').slice(0, totalHashChunk).join('/')
            }
        },
        getLastSplitHash: function () {
            var length = window.location.hash.split('/').length;
            var getLasthSplitHashIndex = --length;
            return window.location.hash.split('/')[getLasthSplitHashIndex];
        },
        loadModule: function (pathFile) {
            var pathFiles = [];
            if (typeof pathFile == 'string') {
                pathFiles.push(pathFile);
            } else {
                pathFiles = pathFile;
            }

            if (!window.isLoadingModule) {
                window.isLoadingModule = true;
                require(pathFiles, function () {
                    window.isLoadingModule = false;
                });
            }
        },
        checkCookieAuthorization: function () {
            var self = this;
            var roleName = Cookies.get(commonConfig.cookieFields.roleName);
            var userName = Cookies.get(commonConfig.cookieFields.userName);

            // not yet check to server about token bearer ... it is a must
            return new Promise(function (resolve, reject) {

                if (roleName && userName) {
                    resolve();
                } else {
                    if (self.isLoginsHash()) {
                        resolve();
                    } else {
                        reject();
                    }
                }
            })
        },
        responseSuccessUpdateAddDelete: function (text) {
            swal("", text, "success");
        },
        responseWarningCannotExecute: function (text) {
            swal("", text, "warning");
        },
        responseStatusNot200: function (object) {

            object = object || {};
            // var title = object.title || 'Cannot Connect to Server.';
            // var text = object.text || 'Please Inform Our Technical Support, Support@onebyonedigital.com and told us what the last activity';
            var title = object.title || 'Ops!. we got a problem here';
            var text = object.text || '';
            var xhr = object.xhr || '';

            var responseJSON = xhr.responseJSON;
            var msg = ' ';

            if (responseJSON) {
                //msg += '<br> server response : ' + (responseJSON.Message || responseJSON.error_description || responseJSON.error || responseJSON.responseText) + ' ';
                msg += (responseJSON.Message || responseJSON.error_description || responseJSON.error || responseJSON.responseText || responseJSON);
            } else {
                if (xhr.responseText) {
                    msg += xhr && xhr.responseText;
                } else if (xhr.status == 0) {
                    msg += ('unknown error occured. server response not received.');
                }
            }
            msg += ' <br> if need help, please contact to system administrator.';

            //title = typeof title == 'undefined' ? 'Warning' : title;
            //text = typeof text == 'undefined' ? 'sorry, something went wrong.' : text;
            swal({
                title: "<span class='text-danger'>" + title + "</span>",
                text: text + ' ' + msg,
                html: true
            });
            //swal("Here's a message!", "It's pretty, isn't it?");
            // window.alert('server is down : please tell to logistical maintenance team and the last time what you did... ' + msg);
        },
        formDataToJson: function (data) {
            return _.object(_.pluck(data, 'name'), _.pluck(data, 'value'));
        },
        doIfLocalhost: function (fn) {
            if (window.location.hostname == 'localhost')
                fn();
        },
        isLoginsHash: function () {
            return _.find(commonConfig.aryLogin, function (hash) {
                return location.href.split(/\?|#/)[1] == hash;
            });
        },
        showLoadingScreen: function (message) {
            message = message || 'Please Wait ...';
            if (!$('#loader-wrapper').length) {
                var dom = $('<div id="loader-wrapper">\
    						<div class="loader">\
    						<div class="text-center"><h3>' + message + '</h3></div></div>\
    					</div>');
                $('body').append(dom);
                $(dom).fadeIn();
            }
        },
        removeLoadingScreen: function () {
            if ($('#loader-wrapper').length) {
                $('#loader-wrapper').fadeOut({
                    complete: function () {
                        $('#loader-wrapper').remove();
                    }
                });
            }
        },
        setSelect2Salutation: function (view, options) {
            var Model = BModel.extend({
                idAttribute: 'value'
            });

            var Collection = BCollection.extend({
                model: Model
            });

            var collection = new Collection([{
                value: '1',
                text: 'Mr'
            }, {
                value: '2',
                text: 'Mrs'
            }]);

            return new Promise(function (resolve) {
                var defaultOption = {
                    selector: '[name="Salutation"]',
                    placeholder: 'Select Salutation',
                    getCollection: function () {
                        return collection
                    },
                    getOptionText: function (model) {
                        return model.get('text');
                    }
                }

                setSelect2(self, view, _.extend({}, select2Option, defaultOption, options)).then(resolve);
            });
        },
        setSelect2Gender: function (view, options) {
            var Model = BModel.extend({
                idAttribute: 'value'
            });

            var Collection = BCollection.extend({
                model: Model
            });

            var collection = new Collection([{
                value: '1',
                text: 'Male'
            }, {
                value: '2',
                text: 'Female'
            }]);

            return new Promise(function (resolve) {
                var defaultOption = {
                    selector: '[name="Gender"]',
                    placeholder: 'Select Gender',
                    getCollection: function () {
                        return collection
                    },
                    getOptionText: function (model) {
                        return model.get('text');
                    }
                }

                setSelect2(self, view, _.extend({}, select2Option, defaultOption, options)).then(resolve);
            });
        },
        getCollectionTerminateReason: function () {
            return createCollection('api/TerminationReason', 'Id', 'api/TerminationReason');
        },
        setSelect2TerminateReason: function (view, options) {
            var self = this;
            return new Promise(function (resolve) {
                var defaultOption = {
                    selector: '[name="TerminateReason"]',
                    placeholder: 'Select TerminateReason',
                    getCollection: self.getCollectionTerminateReason,
                    getOptionText: function (model) {
                        return model.get('TerminationReason');
                    }
                }
                setSelect2(self, view, _.extend({}, select2Option, defaultOption, options)).then(resolve);
            });

        },
        getCollectionTahapan: function () {
            return createCollection('api/Tahapan', 'Id', 'api/Tahapan');
        },
        setSelect2Tahapan: function (view, options) {
            var self = this;
            return new Promise(function (resolve) {
                var defaultOption = {
                    selector: '[name="NamaTahapan"]',
                    placeholder: 'Select Tahapan',
                    getCollection: self.getCollectionTahapan,
                    getOptionText: function (model) {
                        return model.get('NamaTahapan');
                    }
                }
                setSelect2(self, view, _.extend({}, select2Option, defaultOption, options)).then(resolve);
            });
        },
        getCollectionSektor: function () {
            return createCollection('api/Sektor', 'Id', 'api/Sektor');
        },
        setSelect2Sektor: function (view, options) {
            var self = this;
            return new Promise(function (resolve) {
                var defaultOption = {
                    selector: '[name="NamaSektor"]',
                    placeholder: 'Select Sektor',
                    getCollection: self.getCollectionSektor,
                    getOptionText: function (model) {
                        return model.get('NamaSektor');
                    }
                }
                setSelect2(self, view, _.extend({}, select2Option, defaultOption, options)).then(resolve);
            });
        },
        getCollectionRisk: function () {
            return createCollection('api/RiskRegistrasi', 'Id', 'api/RiskRegistrasi');
        },
        setSelect2Risk: function (view, options) {
            var self = this;
            return new Promise(function (resolve) {
                var defaultOption = {
                    selector: '[name="NamaCategoryRisk"]',
                    placeholder: 'Select Risk',
                    getCollection: self.getCollectionRisk,
                    getOptionText: function (model) {
                        return model.get('NamaCategoryRisk');
                    }
                }
                setSelect2(self, view, _.extend({}, select2Option, defaultOption, options)).then(resolve);
            });
        },
        getCollectionMatrix: function () {
            return createCollection('api/Matrix', 'Id', 'api/Matrix');
        },
        setSelect2Matrix: function (view, options) {
            var self = this;
            return new Promise(function (resolve) {
                var defaultOption = {
                    selector: '[name="NamaMatrix"]',
                    placeholder: 'Select Matrix',
                    getCollection: self.getCollectionMatrix,
                    getOptionText: function (model) {
                        return model.get('NamaMatrix');
                    }
                }
                setSelect2(self, view, _.extend({}, select2Option, defaultOption, options)).then(resolve);
            });
        },
        getCollectionScenario: function () {
            return createCollection('api/Scenario', 'Id', 'api/Scenario');
        },
        setSelect2Scenario: function (view, options) {
            var self = this;
            return new Promise(function (resolve) {
                var defaultOption = {
                    selector: '[name="NamaScenario"]',
                    placeholder: 'Select Scenario',
                    getCollection: self.getCollectionScenario,
                    getOptionText: function (model) {
                        return model.get('NamaScenario');
                    }
                }
                setSelect2(self, view, _.extend({}, select2Option, defaultOption, options)).then(resolve);
            });
        },
        getCollectionProject: function () {
            return createCollection('api/Project', 'Id', 'api/Project');
        },
        setSelect2Project: function (view, options) {
            var self = this;
            return new Promise(function (resolve) {
                var defaultOption = {
                    selector: '[name="NamaProject"]',
                    placeholder: 'Select Project',
                    getCollection: self.getCollectionProject,
                    getOptionText: function (model) {
                        return model.get('NamaProject');
                    }
                }
                setSelect2(self, view, _.extend({}, select2Option, defaultOption, options)).then(resolve);
            });
        },
        getGoogleMapAutoCompleteService: function () {
            if (!this.googleMapAutoCompleteService) {
                this.googleMapAutoCompleteService = new google.maps.places.AutocompleteService();
            }
            return this.googleMapAutoCompleteService;
        },
        getGoogleMapPlacesPlaceService: function () {
            if (!this.googleMapPlacesPlaceService) {
                this.googleMapPlacesPlaceService = new google.maps.places.PlacesService(document.createElement('div'));
            }
            return this.googleMapPlacesPlaceService;
        },
        getGoogleMapDetailResult: function (places, status) {
            var aryDetailResult = Object.assign({}, commonConfig.emptyAddressInterest);
            if (status == google.maps.places.PlacesServiceStatus.OK) {
                if (places.address_components.length) {
                    _.each(places.address_components, function (address_component) {
                        if (address_component) {
                            _.find(address_component.types, function (type) {
                                var keyFound;
                                var found = _.find(commonConfig.googleMapAddressInterest, function (googleMapAddressInterest, key) {
                                    if (type == googleMapAddressInterest) {
                                        keyFound = key;
                                        return true;
                                    }
                                });
                                if (keyFound) {
                                    var name = (commonConfig.googleMapAddressInterestIsShortName[keyFound] ? 'short' : 'long') + '_name';
                                    aryDetailResult[keyFound] = address_component[name];
                                    return true;
                                }
                                return false;
                            });
                        }
                    });
                }
            }
            if (aryDetailResult['city'] == '') {
                aryDetailResult['city'] = aryDetailResult['locality'];
            }

            return aryDetailResult;
        },
        getStringExtension: function (filename) {
            var regex = /(?:\.([^.]+))?$/;
            return regex.exec(filename)[1];
        },
        requestServerNotAPI: function () {
            return commonConfig.requestServer.replace('api/', '');
        },
        requestServerAPI: function () {
            return commonConfig.requestServerAPI.replace('api/', '');
        },
        printArray: function (array, fieldName) {
            var string = '';
            if (array && array.length) {
                for (var i = 0; i < array.length; i++) {
                    if (i) {
                        string += ', ';
                    }
                    string += array[i][fieldName];
                }
                return string;
            }
        }
    };

    //some array haven't implement find
    if (!Array.prototype.find) {
        Array.prototype.find = function (callback, thisArg) {
            "use strict";
            var arr = this,
                arrLen = arr.length,
                i;
            for (i = 0; i < arrLen; i += 1) {
                if (callback.call(thisArg, arr[i], i, arr)) {
                    return arr[i];
                }
            }
            return undefined;
        };
    }

    /* end Global variable */
});