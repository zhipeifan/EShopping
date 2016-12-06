$(function () {
    $('#footer').find('ul li').each(function (i, ele) {
        $(ele).on('click', function () {
            $('#footer').find('ul li').removeClass('curr');
            $(this).addClass('curr');
        });
    });
    var mySwiper = new Swiper('.swiper-container', {
        width: '100%',
        height: '200px',
        direction: 'horizontal',
        loop: true,
        // 分页器
        pagination: '.swiper-pagination'
    });
    var timer = null;
    var oHour = $('.andtime').find('li.hour').html();
    var oMinu = $('.andtime').find('li.minu').html();
    var oSec = $('.andtime').find('li.sero').html();
    var infoTime = {
        updateTime: function () {
            var andTime = $('.andtime');
            var oDateEnd = new Date();
            var oDateNow = new Date();
            var iRemain = 0;
            var iDay = 0;
            var iHour = 0;
            var iMin = 0;
            var iSec = 0;
            oDateEnd.setFullYear(parseInt(2016));
            oDateEnd.setMonth(parseInt(6));
            oDateEnd.setDate(parseInt(4));
            oDateEnd.setHours(parseInt(oHour));
            oDateEnd.setMinutes(parseInt(oMinu));
            oDateEnd.setSeconds(parseInt(oSec));
            iRemain = (oDateEnd.getTime() - oDateNow.getTime()) / 1000;

            if (iRemain < 0) {
                clearInterval(timer);
                iRemain = 0;
            };
            iDay = parseInt(iRemain / 86400);
            iRemain %= 86400;
            iHour = parseInt(iRemain / 3600);
            if (iHour < 10) {
                iHour = '0' + iHour;
            }
            iRemain %= 3600;
            iMin = parseInt(iRemain / 60);
            if (iMin < 10) {
                iMon = '0' + iMin
            }
            iRemain %= 60;
            iSec = iRemain;
            if (iSec < 10) {
                iSec = '0' + iSec;
            }
            andTime.html('<li class="hour">' + iHour + '</li><li class="minu">' + iMin + '</li><li class="sero">' + iSec + '</li>');
        },
    };
    infoTime.updateTime();
    setInterval(infoTime.updateTime, 1000);
    var carlist = {
        carSelect: function () {
            var That = $(this);
            if (That.attr('class') == 'select') {
                That.attr('class', 'select-curr')
            } else {
                That.attr('class', 'select');
                carlist.bStop = true;
            };
        }
    }
    $('.car-inner-list').find('.select').bind('click', carlist.carSelect);
    $.each($('.car-inner-list .car-inner-item'), function (i, ele) {
        $(ele).delegate('span.pans em:eq(0)', 'click', function () {
            var iVal = $(this).next('input').val();
            var iPrice = $(this).parents('ul.carlist-inners').attr('data-price');
            var iRest = parseInt($(this).parents('ul.carlist-inners').attr('data-rest'));
            iVal--
            if (iVal <= 0) {
                return;
            };
            $(this).next('input').val(iVal);
            var iSum = parseFloat(iPrice * iVal);
            $(this).parents('ul.carlist-inners').find('em.stockNum').html(iRest - iVal);
            //$('#set-price').html('总计：<em>' + iSum + '</em>易购币')
            productSumPrice();

            var id = $(this).parents('ul.carlist-inners').attr('data-id');
            var spellbuyproductid = $(this).parents('ul.carlist-inners').attr('data-spellbuyproductid');

            ChangeBuyNumNew(id, spellbuyproductid, iVal, -1);
        });
    });
    $.each($('.car-inner-list .car-inner-item'), function (i, ele) {
        $(ele).delegate('span.pans em:eq(1)', 'click', function () {
            var iVal = parseInt($(this).prev('input').val());
            //var iRest=parseInt($(this).attr('data-rest'));
            var iRest = parseInt($(this).parents('ul.carlist-inners').attr('data-rest'));
            var iPrice = $(this).parents('ul.carlist-inners').attr('data-price');
            iVal++;
            if (iVal > iRest) {
                return;
            };
            $(this).prev('input').val(iVal);
            var iSum = parseFloat(iPrice * iVal);
            //$('#set-price').html('总计：<em>' + iSum + '</em>易购币');
            productSumPrice();
            $(this).parents('ul.carlist-inners').find('em.stockNum').html(iRest - iVal);

            var id = $(this).parents('ul.carlist-inners').attr('data-id');
            var spellbuyproductid = $(this).parents('ul.carlist-inners').attr('data-spellbuyproductid');

            ChangeBuyNumNew(id, spellbuyproductid, iVal, 1);
        });
    });
    $('.car-inner-list').find('li.mon span').each(function (i, ele) {
        $(ele).bind('click', function () {
            var iRests = $(this).parent('li.mon').attr('data-rest');
            var iNum = parseInt($(this).text());
            var liNum = $(this).parent('li.mon').prev('li.num').find('input.buyNum');
            var iPrice = $(this).parents('ul.carlist-inners').attr('data-price');
            $(this).parents('ul.carlist-inners').find('li.mon span').removeClass('curr');

            if (!iPrice) return;
            var iSum;
            if (iRests >= iNum) {
                liNum.val(iNum);
                iSum = parseFloat(iPrice * iNum);
            } else {
                liNum.val(iRests);
                iSum = parseFloat(iPrice * iRests);
            };
            if (liNum.val() == iNum) {
                $(ele).addClass('curr');
            }

            //$('#set-price').html('总计：<em>' + iSum + '</em>易购币');
            productSumPrice();
            $(this).parents('ul.carlist-inners').find('em.stockNum').html(iRests - liNum.val());


            var id = $(this).parents('ul.carlist-inners').attr('data-id');
            var spellbuyproductid = $(this).parents('ul.carlist-inners').attr('data-spellbuyproductid');

            ChangeBuyNumNew(id, spellbuyproductid, liNum.val(), 0);
        });
    });


    $('.car-inner-list').find('span.carend').each(function (i, ele) {
        $(ele).bind('click', function () {
            

            var iRests = $(this).parents('ul.carlist-inners').attr('data-rest');
            var iPrice = $(this).parents('ul.carlist-inners').attr('data-price');

            var operateType = 0;
            if (!$(this).hasClass("baowei"))
            {
                $(this).removeClass("baowei").attr("style", "");;
                operateType = -2;
                $(this).parents('ul.carlist-inners').find('input.buyNum').val(0);
                $(this).parents('ul.carlist-inners').find('em.stockNum').html(iRests);

            } else {
                $(this).addClass("baowei").attr("style","background:#999;");
                operateType = 2;
                $(this).parents('ul.carlist-inners').find('input.buyNum').val(iRests);
                $(this).parents('ul.carlist-inners').find('em.stockNum').html(0);
            }

            if (!iPrice) return;
             
            productSumPrice();


            var id = $(this).parents('ul.carlist-inners').attr('data-id');
            var spellbuyproductid = $(this).parents('ul.carlist-inners').attr('data-spellbuyproductid');

            ChangeBuyNumNew(id, spellbuyproductid, iRests, operateType);
        });
    });


    $('.car-inner-list').find('a.del').bind('click', function () {

        var id = $(this).parents('ul.carlist-inners').attr('data-id');
        var spellbuyproductid = $(this).parents('ul.carlist-inners').attr('data-spellbuyproductid');

        $(this).parent().parent().parent($('.car-inner-item')).empty();

    });

});




function ChangeBuyNumNew(id, spellBuyProductId, buyNum, operterType) {

    var data = {
        id: id,
        spellBuyProductId: spellBuyProductId,
        BuyNum: buyNum,
        operateTpye: operterType
    };
    $.ajax({
        type: "post",
        url: shppoingUrl,
        data: data,
        dataType: "json",
        success: function (response) {
        }
    });

}

function productSumPrice() {
    var items = $(".carlist-inners");
    var totalPrice = 0;
    $(items).each(function (i, e) {
        var price = $(e).attr("data-price");
        var inum = $(e).find('input.buyNum').val();

        totalPrice += parseFloat(price * inum);
    });

    $('#set-price').html('总计：<em>' + totalPrice + '</em>易购币');
}