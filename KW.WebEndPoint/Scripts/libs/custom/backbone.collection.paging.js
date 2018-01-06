define(function(require, exports, module) {
    'use strict';
    var Backbone = require('backbone');
    var commonFunction = require('commonfunction');

    module.exports = Backbone.Collection.extend({
        initialize: function(options) {
            var self = this;
            var _self = this;
            if (this.beforeInitialize) {
                this.beforeInitialize(options);
            }

            this.listenTo(this, 'request', function(){
                this.isRequesting = true;
                this.reset();
            });



            this.listenTo(this, 'error', function(collection, xhr) {
                commonFunction.responseStatusNot200({
                    'xhr': xhr
                });
            });

            this.on('sync error', function(){
                this.isRequesting = false;
            });

            /*PageCount :            2
            PageNo            :            1
            PageSize            :            1
            */

            this.parameters = {
                PageSize: 10,
                PageNo: 1,
                Search: '',
                SearchBy: '',
                SortBy: '',
                SortDirection: '',
                FilterParams: {}
            };

            this.paramPaging = {
                //DisplayRows: 0,
                PageSize: 10,//jumlah data yang akan ditampilkan
                //Page: 0,
                PageNo: 0,//no halaman yang terpilih
                //TotalPage: 0,
                PageCount: 0//jumlah halaman , bukan jumlah record
            };

            _.each(this.paramPaging, function(value, index) {
                this.paramPaging[index] = this.parameters[index];
            }, this);
        },
        parse: function(data) {
            if (data) {
                this.parameters.PageNo = data.PageNo;
                //this.parameters.DisplayRowPage = data.DisplayRowPage;
                this.parameters.PageNo = data.PageNo;
                this.parameters.PageSize = data.PageSize;
            }

            //_.each(['DisplayRows', 'Page', 'TotalPage', 'OrderBy', 'TotalRows'], function(value) {
            _.each(['PageNo', 'PageCount', 'PageSize'], function(value){
                if (data[value] != undefined)
                    this.paramPaging[value] = data[value];
            }, this);
            return data.results;
        },
        fetch: function(options) {
            options = options || {};
            //this.parameters.Page = this.paramPaging.Page;
            this.parameters.PageNo = this.paramPaging.PageNo;
            options.data = _.extend({}, this.parameters, options.data);

            return Backbone.Collection.prototype.fetch.call(this, options);
        },
        setOrderBy: function() {
            this.parameters.OrderBy = this.paramPaging.SortField + '-' + this.paramPaging.SortOrder;
        },
        SetOrderByEmpty: function() {
            this.paramPaging.SortField = '';
            this.paramPaging.SortOrder = '';
            this.parameters.OrderBy = '';
        },
        setParametersFilterParamsEmpty: function() {
            this.SetOrderByEmpty();
            this.parameters.FilterParams = {};
            this.setPageNo1();
            this.trigger('changeViewSorting');
        },
        setParamPagingSortFieldSortOrder: function(SortField, SortOrder) {
            this.paramPaging.SortField = SortField;
            this.paramPaging.SortOrder = SortOrder;
            this.setPageNo1();
            this.setOrderBy();
            this.trigger('changeViewSorting');
        },
        setPageNo1: function() {
            this.paramPaging.PageNo = 1;
        }
    });
});
