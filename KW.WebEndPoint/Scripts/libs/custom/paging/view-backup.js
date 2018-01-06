define(function(require, exports, module) {
    'use strict';
    var test = require('require');
    var LayoutManager = require('layoutmanager');
    var template = require('text!pathLibsCustom/paging/template.html');

    module.exports = LayoutManager.extend({
        tagName: 'nav',
        className: 'paging hide',
        template: _.template(template),
        initialize: function(){
            var self = this;
            this.listenTo(this.collection, 'request', function(){
                this.$el.css('display','none');
            });

            this.listenTo(this.collection, 'sync', function(collection){
                this.$el.css('display','table').css('margin', '0 auto');
                this.$('[name="Page"]').val(collection.paramPaging.Page).attr('max', collection.paramPaging.TotalPage);
                this.$('[name="TotalPage"]').text(collection.paramPaging.TotalPage);

                self.changeButtonPaging();

            }, this);
        },
        events:{
            'click [obo-first]': 'goFirstPage',
            'click [obo-previous]': 'goPreviousPage',
            'click [obo-next]': 'goNextPage',
            'click [obo-last]': 'goLastPage',
            'keypress [name="Page"]': 'goToPageByKeyPress',
            'focusout [name="Page"]': 'goToPage',
        },
        beforeRender: function(){
            this.setIsHide();
        },
        afterRender: function(){
            var self = this;

            ['first', 'previous', 'next', 'last'].forEach(function(item){
                self.$('[obo-'+item + '] > img').attr('src','/Scripts/img/pagination/' + item + '_off.png');
            });
        },
        goFirstPage: function(){
            var page = 1;
            this.setChangePage(page);
        },
        goPreviousPage: function(){
            var page = this.$('[name="Page"]').val();

            if (--page <= this.collection.paramPaging.TotalPage && page >= 1){
                this.setChangePage(page);
            }
        },
        goNextPage: function(){
            var page = this.$('[name="Page"]').val();
            if (++page <= this.collection.paramPaging.TotalPage ){
                this.setChangePage(page);
            }
        },
        goLastPage: function(){
            var page = this.collection.paramPaging.TotalPage;
            this.setChangePage(page);
        },
        goToPageByKeyPress: function(e){
            if (e.charCode == 13){
                this.goToPage();
            }
        },
        goToPage: function(e){
            this.setChangePage(this.$('[name="Page"]').val());
        },
        changeButtonPaging: function(){
            var self = this;
            var isPageOne = this.collection.paramPaging.Page == 1;
            var isLastPage = this.collection.paramPaging.Page == this.collection.paramPaging.TotalPage;

            var fnChangeButtonPaging = function(arrayButtonSelector, isTrue ){
                _.each(self.$(arrayButtonSelector), function(item){
                    var src = $('img', item).attr('src');
                    src = src.replace('_'+ (isTrue ? 'on' : 'off') + '.png', '_'+ (!isTrue ? 'on' : 'off') + '.png');
                    $('img', item).attr('src', src);

                    if (isTrue){
                        $(item).attr('disabled', 'disabled');
                    }else{
                        $(item).removeAttr('disabled');
                    }
                });
            }

            fnChangeButtonPaging('[obo-first], [obo-previous]', isPageOne);
            fnChangeButtonPaging('[obo-next], [obo-last]', isLastPage);
            this.setIsHide();
        },
        setChangePage: function(page){
            if (page >= 1 && page <= this.collection.paramPaging.TotalPage){
                this.collection.paramPaging.Page = page;
                this.$('[name="Page"]').val(page)
                this.collection.fetch();
                this.changeButtonPaging();
            }
        },
        setIsHide: function(){
            if (this.collection.paramPaging.Page > 1 || this.collection.length){
                this.$el.removeClass('hide');
            }else{
                this.$el.addClass('hide')
            }
        }
    })
});
