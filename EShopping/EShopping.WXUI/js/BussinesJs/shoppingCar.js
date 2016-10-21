

$(function () {


    $("a.iaddcar").click(function () {
        var id = $(this).attr("pid");
        var spellBuyProductId = $(this).attr("spellBuyProductId");

        var data = {
            id: id,
            spellBuyProductId: spellBuyProductId
        };

        $.ajax({
            type: "post",
            url: addurl,
            data: data,
            dataType: "json",
            success: function (response) {
                $("#carCount").html(response);
            }
        });
    });

    BaingClick();


    $(".orderSave").click(function () {
        createOrder();
    });

    IsBuyAll();//包尾
});

function IsBuyAll()
{
    $(".carend").click(function () {
        var _style = $(this).attr("style");
        var _surplus =parseInt($(this).attr("Surplus"));
        if (_style != "" && _style != undefined)
        {
            $(this).removeAttr("style");
            $(this).next().val("true");
        } else {
            $(this).attr("style", "background:#999;");
            $(this).next().val("false");
        }
    });
}

function BaingClick()
{
    $("form").attr("action", changeProductUrl);
    $(".item-inner .mon span").click(function () {
       // AddProductNum(this);
    });

    $(".carlist-inners em").click(function () {
        //AddProductNum(this);
    });

    //$(".orderDel").click(function () {
    //    if (confirm("您确定要从购物车中删除该商品？")) {
    //        AddProductNum(this);
    //    }
    //});
}


function ChangebuyNum(obj)
{
    $("#form1").attr("action", changeProductUrl);
    var opt = parseInt($(obj).attr("opt"));
    if (opt == 0)
    {
        $(obj).parents(".car-inner-item").find("#carCount").val($(obj).attr("num"));
    } else {
        $(obj).parents(".car-inner-item").find("#OperationType").val($(obj).attr("opt"));
    }
    $("#form1").submit();
}



function AddProductNum(obj)
{
        var id = $(obj).attr("pid");
        var spellBuyProductId = $(obj).attr("spellBuyProductId");
        var num = $(obj).attr("num");
        var data = {
            id: id,
            spellBuyProductId: spellBuyProductId,
            num: num
        };

        $.ajax({
            type: "post",
            url: shppoingUrl,
            data: data,
            dataType: "text",
            success: function (response) {
                window.location.reload();
            }
        });
  
}



//发起秘密团
function fun_SecretStart()
{
    var code = $("#secretCode").val();

    if(code=="")
    {
        alert("请填写秘密团代码");
    }


}


function createOrder()
{
    var cks = $(".lr-pad :checked");

    var ids = new Array();

    for(var i=0;i<cks.length;i++)
    {
        ids[i] = $(cks[i]).val() + "_" + $(cks[i]).attr("spellbuyproductid");
    }

    var data = {
        keys: ids
    };
   
    $.ajax({
        type: "post",
        url: ourl,
        data: data,
        dataType: "json",
        success: function (response) {
            // $(obj).parents(".item-inner").find("#carCount").val(num);
        }
    });
    
}