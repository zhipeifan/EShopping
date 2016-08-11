$(function(){
	$('#footer').find('ul li').each(function(i, ele){
		$(ele).on('click', function(){
			$('#footer').find('ul li').removeClass('curr');
			$(this).addClass('curr');
		});
	});
	var mySwiper = new Swiper ('.swiper-container', {
			width:'100%',
			height:'200px',
		    direction: 'horizontal',
		    loop: true,
		    // 分页器
		    pagination: '.swiper-pagination'
	});
	var timer=null;
	var oHour=$('.andtime').find('li.hour').html();
	var oMinu=$('.andtime').find('li.minu').html();
	var oSec=$('.andtime').find('li.sero').html();
	var infoTime={
			updateTime:function(){
				var andTime=$('.andtime');
				var oDateEnd=new Date();
				var oDateNow=new Date();
				var iRemain=0;
				var iDay=0;
				var iHour=0;
				var iMin=0;
				var iSec=0;
				oDateEnd.setFullYear(parseInt(2016));
				oDateEnd.setMonth(parseInt(6));
				oDateEnd.setDate(parseInt(4));
				oDateEnd.setHours(parseInt(oHour));
				oDateEnd.setMinutes(parseInt(oMinu));
				oDateEnd.setSeconds(parseInt(oSec));
				iRemain=(oDateEnd.getTime()-oDateNow.getTime())/1000;
				
				if(iRemain<0){
					clearInterval(timer);
					iRemain=0;
					alert('时间已过');
				};
				iDay=parseInt(iRemain/86400);
				iRemain%=86400;
				iHour=parseInt(iRemain/3600);
				if(iHour <10){
					iHour='0'+iHour;
				}
				iRemain%=3600;
				iMin=parseInt(iRemain/60);
				if(iMin < 10){
					iMon='0'+iMin
				}
				iRemain%=60;
				iSec=iRemain;
				if(iSec < 10){
					iSec='0'+iSec;
				}
				andTime.html('<li class="hour">'+iHour+'</li><li class="minu">'+iMin+'</li><li class="sero">'+iSec+'</li>');
			},
		};
	infoTime.updateTime();
	setInterval(infoTime.updateTime,1000);
	var carlist={
		bStop:true,
		carSelect:function(){
			var That=$(this);
			if(carlist.bStop){
				That.attr('class', 'select-curr')
				carlist.bStop=false;
			}else{
				That.attr('class','select');
				carlist.bStop=true;
			};
		}
	}
	$('.car-inner-list').find('.select').bind('click',carlist.carSelect);
	$.each($('.car-inner-list .car-inner-item'), function(i, ele){
		$(ele).delegate('span.pans em:eq(0)', 'click', function(){
			var iVal=$(this).next('input').val();
			if(iVal <= 0){
				return;
			};
			iVal--
			$(this).next('input').val(iVal);
		});
	});
	$.each($('.car-inner-list .car-inner-item'), function(i, ele){
		$(ele).delegate('span.pans em:eq(1)', 'click', function(){
			var iVal=parseInt($(this).prev('input').val());
			iVal++;
			$(this).prev('input').val(iVal);
		});
	});
	$('.car-inner-list').find('li.mon span').each(function(i, ele){
		$(ele).bind('click', function(){
			$('.car-inner-list').find('li.mon span').removeClass('curr');
			$(ele).addClass('curr');
		});
	});
	$('.car-inner-list').find('a.del').bind('click', function(){
		$(this).parent().parent().parent($('.car-inner-item')).empty();
		
	});
	
});