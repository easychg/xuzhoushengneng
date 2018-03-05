"use strict";
var screenWidth = $(window).width();
var screenHeight = $(window).height();
var containerWidth = $('.container').width();

function banner(){
	$('.slider').slick({
		adaptiveHeight: true,
		dots: true,
		autoplay: true
	});

	var bannerWidth = $('.slider-banner').width();
	$('.slider-banner .slick-slide').height(bannerWidth*608/1080);

    var productWidth = $('.slider-product').width();
    $('.slider-product .slick-slide').height(productWidth);
}

function autoWidth(){
    var headerBtn = $('.header-index .btn-login').width();
    $('.header-index .form-search').width(screenWidth-headerBtn-18);

    var messageWidth = $('.mod-property .message').width();
    $('.mod-property .textarea-control').width(messageWidth-68);

    var cartItemWidth = $('.cart-list li').width();
    $('.cart-list .target').width(cartItemWidth-48);
}

function autoHeight(){
    var proPicWidth = $('.product-list .pic').width();
    $('.product-list .pic').height(proPicWidth);

    var collectPicWidth = $('.collect-list .pic').width();
    $('.collect-list .pic').height(collectPicWidth);

    var uploadPicWidth = $('.add-images-list li').width();
    $('.add-images-list li, .upload-outer, .upload-outer .input-upload').height(uploadPicWidth);

    var commentPicWidth = $('.mod-comment .pic-list .pic').width();
    $('.mod-comment .pic-list .pic').height(commentPicWidth);

    var slideUpHeight = $('.slideUp').height();
    $('.slideUp-ensure .content').height(slideUpHeight-88);
    $('.slideUp-cart .tb-sku').height(slideUpHeight-163);
    $('.slideUp-track .textarea-control').height(slideUpHeight-101);

    // model
    $('.model-dialog').height(screenHeight);
    var dialogHeaderHeight = $('.model-header').height();
    var dialogfooterHeight = $('.model-footer').outerHeight();
    var dialogBodyHeight = screenHeight-dialogHeaderHeight-dialogfooterHeight-38;
    $('.model-dialog .model-body').height(dialogBodyHeight);
}

function proCart(){
    // 根据选中的属性进行价格和库存的控制
    $('.tb-sku label').each(function(index){
        var tbPriceText = $(this).find('em.price').text();
        var tbPriceValue = parseFloat(tbPriceText);
        var tbSkuText = $(this).find('em.sku').text();
        var tbSkuValue = parseInt(tbSkuText);
        $(this).click(function(){
            $('#amountInput').val(1);
            $('#proPrice').text(tbPriceValue);
            $('#emStock').text(tbSkuValue);
        });

        if($(this).find('input').is(':checked')){
            var tbPriceText = parseFloat($(this).find('em.price').text());
            var tbSkuText = parseInt($(this).find('em.sku').text());
            $('#proPrice').text(tbPriceText);
            $('#emStock').text(tbSkuValue);
        }
    });

    function judgeChecked(){
        if($(this).find('input').is(':checked')){
            $('#amountInput').val(1);
            var tbPriceText = parseFloat($(this).find('em.price').text());
            var tbSkuText = parseInt($(this).find('em.sku').text());
            $('#proPrice').text(tbPriceText);
            $('#emStock').text(tbSkuValue);
        }
    }

    // 产品数量增加和减少
    $('.amount-btn .amount-increase').click(function(){
        $('.sku-tips').hide();
        judgeChecked();
        var num = $('#amountInput').val();
        var emStockText = $('#emStock').text();
        var emStockValue = parseInt(emStockText);
        if(num<emStockValue){
            num++;
           $('#amountInput').val(num); 
        }else if(num>=emStockValue){
            $('.sku-tips').text('您所填写的商品数量超过库存数！').show();
        }
    });
    $('.amount-btn .amount-decrease').click(function(){
        $('.sku-tips').hide();
        judgeChecked();
        var num = $('#amountInput').val();
        if(num>1){
            num --;
            $('#amountInput').val(num);
        }else if(num<=1){
            $('.sku-tips').text('您所填写的商品数量不能少于1！').show();
        }
    });

    $('#amountInput').bind('input propertychange', function(){
        var amountInput = $('#amountInput').val();
        var tbSkuText = parseInt($('#emStock').text());
        if(amountInput>tbSkuText){
            $('.sku-tips').text('您所填写的商品数量超过库存数！').show();
        }else if(amountInput<1){
            $('.sku-tips').text('您所填写的商品数量不能少于1！').show();
            setTimeout(function(){
                $('#amountInput').val(1);
                $('.sku-tips').hide();
            }, 300);
        }else{
            $('.sku-tips').hide();
        }
    });


    // settle 结算页
    if($('.init').hasClass('settle')){
        var settleUnit = parseInt($('.settle #unit').text());
        var settleNum = parseInt($('.settle #num').text());
        $('#amountInput').val(settleNum);
        $('.amount-btn i').click(function(){
            var amountInput = $('#amountInput').val();
            $('.settle .num').text(amountInput);
            var subtotal = amountInput * settleUnit;
            $('.fixed-footer .price, .settle .price').text(subtotal);
        });
    } 
}

function tabSwitch(){
    // $('.tab-holder>li').each(function(i){
    //     $('.tab-holder>li:first').addClass('tab-active');
    //     $('.tab-content>.tab-panel:first').addClass('active');
    //     $(this).click(function(){
    //         var _this = $(this);
    //         _this.addClass('tab-active').siblings().removeClass('tab-active').parents('.tab-holder').siblings('.tab-content').find('.tab-panel').eq(i).addClass('active').show().siblings().removeClass('active').hide();
    //         console.log(i);
    //     });
    // });

    $('.header-product>a').each(function(i){
        $('.header-product>a:first').addClass('active');
        $('.init>.tab-pane:first').addClass('tab-top').show();
        $(this).click(function(){
            var _this = $(this);
            _this.addClass('active').siblings().removeClass('active').parents('.header-product').siblings('.init').find('.tab-pane').eq(i).addClass('active').show().siblings().removeClass('active').hide();

            if(i>=1){
                $('.header-product').addClass('header-bg');
            }else{
                $('.header-product').removeClass('header-bg');
            }
        });
    });
}

function filter(){
    $('.filter').click(function(){
        $(this).toggleClass('active').siblings('.filter-all').removeClass('active').siblings('.filter-list').slideUp();
    });

    $('.filter-all').click(function(){
        $('.wrapper').fadeToggle();
        $('body').toggleClass('body-hidden');
        $(this).addClass('active').siblings('.filter-list').slideToggle();
    });

     $('.filter-list li').click(function(){
        var filterClass = $(this).attr('class');
        var filterMark = $(this).data('mark');
        $(this).parents('.filter-list').siblings('.filter-all').find('em').text(filterMark)
            .end().find('i').attr('class', 'icon-'+filterClass);
        $(this).parents('.filter-list').slideUp();
        $('.wrapper').fadeOut();
        $('body').removeClass('body-hidden');
     });
}

function clickEvent(){
    var count = 0;
    $('.fixed-nav-pro .btn-collect').click(function(){
        count++;
        $(this).toggleClass('active');
        if(count%2){
            var alertText = "成功收藏！"
        }else{
            var alertText = "取消收藏！"
        }

        $(this).after('<div class="alert border-rect text-center">'+ alertText +'</div>');
        setTimeout(function(){ $('.alert').remove(); }, 2000);
    });

    // 更多保障
    $('.ensure-outer .icon-right').click(function(){
        $('.slideUpWrapper').show();
        $('.slideUp-ensure').slideDown();
        $('body').css('overflow', 'hidden');
    });

    // 选择商品规格
    $('.choose-good, .btn-cart').click(function(){
        var urlImg = $('.slider-product img').attr('src');
        var unit = $('.summary .unit em').text();
        var sku = $('.summary .sku em').text();
        $('.slideUp-cart img').attr('src', urlImg);
        $('.slideUp #proPrice').text(unit);
        $('.slideUp #emStock').text(sku);

        $('.slideUpWrapper').show();
        $('.slideUp-cart').slideDown();
        $('body').css('overflow', 'hidden');
    });

    $('.slideUp .btn-confirm, .slideUpWrapper').click(function(){
        $('.slideUpWrapper').hide();
        $('.slideUp').slideUp();
        $('body').css('overflow', 'scroll');
    });

    // 追加评论
    $('.btn-track').click(function(){
        $('.slideUpWrapper').show();
        $('.slideUp-track').slideDown();
        $('body').css('overflow', 'hidden');
    });

    // 我的--成为供应商
    $('.btn-role').click(function(){
        $(this).animate({'right':'0'});
    });

    // 分类
    $('.aside-menu li').click(function(){
        $(this).addClass('active').siblings().removeClass('active');
    });

    // 我的地址
    $('.mydelivery-list li').each(function(){
        var isChecked = $(this).find('input').is(':checked');
        if(isChecked){
            var _this = $(this);
            _this.siblings('.name').addClass('red').text('默认选中');
        }
    });

    $('.mydelivery-list .label-check').click(function(){
        $(this).find('.name').addClass('red').text('默认选中');
        $(this).parents('.mod-delivery').siblings().find('.label-check .name').removeClass('red').text('设为默认');
    });

    // $('.mydelivery-list li').each(function(){
        // $(this).find('.btns-opera .btn-edit').click(function(){
        //     var _this = $(this);
        //     _this.addClass('hide').siblings('.btn-ok').removeClass('hide').parents('.mod-delivery').find('input, textarea').addClass('form-control').prop('disabled',false);
        //     _this.parents('.mod-delivery').siblings().find('input, textarea').removeClass('form-control').prop('disabled',true);

        //     if(_this.parents('li').siblings().find('.btns-opera .btn-ok').hasClass('hide')){
        //         console.log(111);
        //         var __this = $(_this);
        //         __this.removeClass('hide').siblings('.btn-edit').addClass('hide');
        //     }
        // });
    // });

    $('.btns-opera .btn-edit').click(function(){
        $(this).addClass('hide').siblings('.btn-ok').removeClass('hide').parents('.mod-delivery').find('input, textarea').addClass('form-control').prop('disabled',false);
        $(this).parents('.mod-delivery').siblings().find('input, textarea').removeClass('form-control').prop('disabled',true);
    }); 
    
    $('.btns-opera .btn-ok').click(function(){
        $(this).addClass('hide').siblings('.btn-edit').removeClass('hide').parents('.mod-delivery').find('input, textarea').removeClass('form-control').prop('disabled',true);
    });
    

    // 右侧弹窗
    $('.btn-right').click(function(){
        $('.model-dialog').animate({right: '0'}, 'fast');
        $('body').css('overflow', 'hidden');
    });

    $('.model-header .btn-back, .model-footer .btn, .model-header .btn-close').click(function(){
        $(this).parents('.model-dialog').animate({right: '-100%'}, 'fast');
        $('body').css('overflow', 'scroll');
    });
    
}

function collectPress(){
    var time = 0;
    $('.collect-list a').on('touchstart',function(e){
         e.stopPropagation();
         var DragElement = this;
         time = setTimeout(function(){
            $(DragElement).parents('li').addClass('active').find('.btn-delete').show();
         },1000);
    })

    $(".collect-list a").on('touchend', function(e){
        e.stopPropagation();
        clearTimeout(time);  
    });


    $('.btn-delete').click(function(){
        var deleteHtml = '<div class="dialog-confirm border-rect">'+
            '<div class="title">您是否要删除？</div>'+
            '<div class="btns clearfix">'+
                '<a class="fl btn-confirm">确定</a>'+
                '<a class="fr btn-cancel">取消</a>'+
            '</div>'+
        '</div>';
        $('body').append(deleteHtml);

        $('.btn-cancel').click(function(){
            $('.dialog-confirm').remove();
        });

        var _this = $(this);
        $('.btn-confirm').click(function(){
            $('.common-list').after('<div class="alert alert-delete border-rect">删除成功</div>');
            setTimeout(function(){ $('.alert-delete').remove(); }, 1000);
            _this.parents('li').remove();
            $('.dialog-confirm').remove();
        });
    });

    window.ontouchstart = function (e) { e.preventDefault(); };
}

function cart(){
    $('.btnEdit').click(function(){
        $(this).hide().siblings().show();
        $('.show-info, .btn-submit').hide();
        $('.edit-wrapper, .btn-cartdel').show();
    });

    $('.btn-finish').click(function(){
        $(this).hide().siblings().show();
        $('.show-info, .btn-submit').show();
        $('.edit-wrapper, .btn-cartdel').hide();
    });


    $('.cart-list .btn-spec').click(function(){
        var item = $(this).parents('.item');
        var urlImg = item.find('.pic img').attr('src');
        var unit = item.find('.unit').text();
        var sku = item.find('.emStock').text();
        $('.slideUp-cart img').attr('src', urlImg);
        $('.slideUp #proPrice').text(unit);
        $('.slideUp #emStock').text(sku);

        $('.slideUpWrapper').show();
        $('.slideUp-cart').slideDown();
        $('body').css('overflow', 'hidden');
    });


    var trGoods = $('.cart-list li');
    // 初始值
    for(var x=0; x<trGoods.length; x++){
        var tr = trGoods.eq(x);
        var unit = parseFloat(tr.find('.unit').text());
        var num = parseInt(tr.find('.num').text());
        var sum = parseFloat(unit * num);

        tr.find('.amount-input').val(num);
        tr.find('.sum').text(sum);
    }

    // 小计
    function getSubTotal(tr){
        var unit = parseFloat(tr.find('.unit').text());
        var amountInput = parseInt(tr.find('.amount-input').val());
        var subTotal = parseFloat(unit * amountInput);
        tr.find('.sum').text(subTotal.toFixed(2));
    }

    // 为每行元素添加事件
    for(var i=0; i<trGoods.length; i++){
        trGoods.eq(i).find('i').click(function(){
            var tr = $(this).parents('.tr-goods');
            var amountInput = tr.find('.amount-input');
            var val = parseInt(amountInput.val());
            var amountSku = parseInt(tr.find('.emStock').text());
            var cls = $(this).attr('class');
            var unit = parseFloat(tr.find('.unit').text());
            console.log(val, amountSku);
            switch (cls){
                case 'amount-increase':
                    if(amountSku>val){ 
                        val ++; 
                        $('.sku-tips').hide();
                    }else{ 
                        $('.sku-tips').hide();
                        amountInput.parents('.tr-goods').find('.sku-tips').text('您所填写的商品数量超过库存数!').show();
                        setTimeout(function(){$('.sku-tips').hide();}, 1000);
                    }
                    amountInput.val(val);
                    tr.find('.num').text(val);
                    getSubTotal(tr);
                    break;
                case 'amount-decrease':
                    if(val>1){
                        val --;
                        $('.sku-tips').hide();
                    }else{
                        $('.sku-tips').hide();
                        amountInput.parents('.tr-goods').find('.sku-tips').text('您所填写的商品数量至少为1!').show();
                        setTimeout(function(){$('.sku-tips').hide();}, 1000);
                    }
                    amountInput.val(val);
                    tr.find('.num').text(val);
                    getSubTotal($(this).parents('.tr-goods'));
                    break;
                default:
                    break;
            }

            getTotal();
        });
        
        // 改变input数量
        trGoods.eq(i).find('.amount-input').keyup(function(){
            var tr = $(this).parents('.tr-goods');
            var num = parseInt($(this).val());
            var sku = parseInt(tr.find('.emStock').text());
            console.log(num, sku);
            if(isNaN(num) || num<1){
                num = 1;
            }else if(num>sku){
                num = sku;
                console.log(num);
            }
            $(this).val(num);
            tr.find('.num').text(num);

            getSubTotal(tr);
            getTotal();
        });
    }

    $('.btn-cartdel').click(function(){
        var count = 0;
        $('.cart-list li').each(function(){
            var isChecked = $(this).find('.check').is(':checked');
            if(isChecked){
                count ++;
                var _this = $(this);
                _this.remove();
                $('.cart-list').after('<div class="alert alert-delete border-rect">删除成功</div>');
                setTimeout(function(){ $('.alert-delete').remove(); }, 1000);
            }
            getTotal();
        });

        if(count == 0){
            $('.cart-list').after('<div class="alert alert-delete border-rect">请选择要删除的商品</div>');
            setTimeout(function(){ $('.alert-delete').remove(); }, 1000);
        }
    });

    // 选择产品
    $('.check').click(function(){ getTotal();});

    $('.check-all').click(function(){
        var thisChecked = $(this).is(':checked');
        $('.cart-list .item').find('.check').prop('checked', thisChecked);

        getTotal();
    });

    // 计算
    function getTotal(){
        var selected = 0;
        var price = 0;
        var Imghtml = '';
        $('.cart-list .item').each(function(){
            var isChecked = $(this).find('.check').is(':checked');
            if(isChecked){
                var unit = parseInt($(this).find('.unit').text());
                var num = parseInt($(this).find('.num').text());
                var sum = unit*num;
                selected += num;
                price += sum;
            }
        });

        $('#orderNum').text(selected);
        $('#subtotal').text(price);
    }
}

function scrollToTop(){
    $('#JScrollToTop').click(function(){
        $('html, body').animate({scrollTop:0}, 'slow');
    });
}

$(document).ready(function(){
	banner();
    autoWidth();
	autoHeight();
    proCart();
    tabSwitch();
    filter();
    clickEvent();
    collectPress()
    cart();
	scrollToTop();
});

$(window).on('scroll', function () {
    var scrollTop = $(window).scrollTop();

    var headerTop = $('.slider-banner').height()-58;
    if(scrollTop>headerTop){
        $('.header-index').addClass('bg-red');
    }else{
        $('.header-index').removeClass('bg-red');
    }

    if(scrollTop>58){
        $('.header-product').addClass('bg-product');
    }else{
        $('.header-product').removeClass('bg-product');
    }

	if(scrollTop >= 100){
        $('#JScrollToTop').addClass('rollIn');
    }else{
        $('#JScrollToTop').removeClass('rollIn');
    }

    $('.menu-right .tab-pane').each(function(i){
        var itemTop = $(this).offset().top-20;
        if(scrollTop>itemTop){
            $('.aside-menu li').eq(i).addClass('active').siblings().removeClass('active');
        }
    });
});