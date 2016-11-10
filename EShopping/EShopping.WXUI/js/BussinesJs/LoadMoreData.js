

function ShoppingWinnerListLoadMore() {
    var _pageIndex = $("#pageIndex").val();
    _pageIndex = parseInt(_pageIndex) + 1;
    var data = {
        pageIndex: _pageIndex
    };
    var url = "/MyEShopping/ShoppingWinnedListPartial";
    var _toObj = $(".g_tabs_list");
    LoadMore(data, url, _toObj, _pageIndex);
}







function LoadMore(data,url,toObj,pageIndex)
{
    $.ajax({
        type: "post",
        url: url,
        data: data,
        dataType: "html",
        success: function (response) {
            $(toObj).append(response);
            $(pageIndex).val(pageIndex);
           // $(".loader").hide();
            if ($.trim(response) == "") {
                $(".pullUpLabel").html("亲，没有更多了哟。");
            }
        }
    });
}