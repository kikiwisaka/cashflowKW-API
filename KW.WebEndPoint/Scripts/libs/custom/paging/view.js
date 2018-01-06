define(function(require, exports, module) {
    'use strict';
    var test = require('require');
    var LayoutManager = require('layoutmanager');
    require('jquery.simplePagination')
    var template = require('text!pathLibsCustom/paging/template.html');

    module.exports = LayoutManager.extend({
        tagName: 'nav',
        className: 'paging hide',
        template: _.template(template),
        initialize: function(){
            var self = this;
            this.listenTo(this.collection, 'request', function(){
                //this.$el.css('display','none');
                self.setIsHide();

            });
            this.listenTo(this.collection, 'sync', function(collection){
              this.createPaging();

              this.setIsHide();
              self.setChangePage(collection.paramPaging.PageNo);
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
            this.createPaging();
        },
        createPaging: function(){
          var self = this;
          this.$el.empty();
          this.$el.pagination({
            items:this.collection.paramPaging.PageCount * this.collection.paramPaging.PageSize,
            itemsOnPage:this.collection.paramPaging.PageSize,
            currentPage: this.collection.paramPaging.PageNo || 1,
            onPageClick:function(PageNo, event){
              event.preventDefault();self.collection.fetch({
                data:{
                  PageNo: PageNo
                }
             });
            }
          })
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

            // var fnChangeButtonPaging = function(arrayButtonSelector, isTrue ){
            //     _.each(self.$(arrayButtonSelector), function(item){
            //         var src = $('img', item).attr('src');
            //         src = src.replace('_'+ (isTrue ? 'on' : 'off') + '.png', '_'+ (!isTrue ? 'on' : 'off') + '.png');
            //         $('img', item).attr('src', src);
            //
            //         if (isTrue){
            //             $(item).attr('disabled', 'disabled');
            //         }else{
            //             $(item).removeAttr('disabled');
            //         }
            //     });
            // }

            // fnChangeButtonPaging('[obo-first], [obo-previous]', isPageOne);
            // fnChangeButtonPaging('[obo-next], [obo-last]', isLastPage);
            // this.setIsHide();
        },
        setChangePage: function(page){
            if (page >= 1 && page <= this.collection.paramPaging.PageCount){
                this.collection.paramPaging.PageNo = page;
                //  return this;
                // this.$('[name="Page"]').val(page);
                // this.$('.simple-pagination').pagination('selectPage', page);
                // this.collection.fetch();
                // this.changeButtonPaging();
            }
        },
        setIsHide: function(){
            if (this.collection.paramPaging.Page > 1 || this.collection.length){
                this.$el.removeClass('hide');
            }else{
                this.$el.addClass('hide')
            }
            // this.setChangePage()
        }
    })
});
