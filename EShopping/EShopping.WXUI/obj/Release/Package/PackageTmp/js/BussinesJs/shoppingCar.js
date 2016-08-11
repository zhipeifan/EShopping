

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

    $(".isbuyAll").click(function () {
        fun_orderBuyAll(this);
    });


    $(".orderSave").click(function () {
        createOrder();
    });
});


function BaingClick()
{
    $(".item-inner .mon span").click(function () {
        AddProductNum(this);
    });

    $(".carlist-inners em").click(function () {
        AddProductNum(this);
    });

    $(".lr-pad .del").click(function () {
        AddProductNum(this);
    });
}



function AddProductNum(obj)
{
    var id = $(obj).attr("pid");
    var spellBuyProductId = $(obj).attr("spellBuyProductId");
    var num = $(obj).attr("num");
    var data = {
        id: id,
        spellBuyProductId: spellBuyProductId,
        num:num
    };

    $.ajax({
        type: "post",
        url: shppoingUrl,
        data: data,
        dataType: "text",
        success: function (response) {
           // $(obj).parents(".item-inner").find("#carCount").val(num);
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


function fun_orderBuyAll(obj)
{
    $(obj).next().val("true");
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