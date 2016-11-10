
//中奖记录列表
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

//全部购买记录列表
function ShoppingHistoryListLoadMore()
{
    var _pageIndex = $("#pageIndex").val();
    _pageIndex = parseInt(_pageIndex) + 1;
    var data = {
        pageIndex: _pageIndex
    };
    var url = "/MyEShopping/ShoppingHistoryListPartial";
    var _toObj = $(".g_tabs_list");
    LoadMore(data, url, _toObj, _pageIndex);
}

//进行中列表
function ShoppingStartingListLoadMore()
{
    var _pageIndex = $("#pageIndex").val();
    _pageIndex = parseInt(_pageIndex) + 1;
    var data = {
        pageIndex: _pageIndex
    };
    var url = "/MyEShopping/ShoppingStartingListPartial";
    var _toObj = $(".g_tabs_list");
    LoadMore(data, url, _toObj, _pageIndex);
}


//用户中心中奖记录列表
function UserShoppingWinnerListLoadMore(id) {
    var _pageIndex = $("#pageIndex").val();
    _pageIndex = parseInt(_pageIndex) + 1;
    var data = {
        pageIndex: _pageIndex,
        id: id
    };
    var url = "/MyEShopping/ShoppingWinnedListPartial";
    var _toObj = $(".g_tabs_list");
    LoadMore(data, url, _toObj, _pageIndex);
}

//用户中心全部购买记录列表
function UserShoppingHistoryListLoadMore(id) {
    var _pageIndex = $("#pageIndex").val();
    _pageIndex = parseInt(_pageIndex) + 1;
    var data = {
        pageIndex: _pageIndex,
        id: id
    };
    var url = "/UserCenter/ShoppingHistoryListPartial";
    var _toObj = $(".g_tabs_list");
    LoadMore(data, url, _toObj, _pageIndex);
}

//晒单列表
function ShareOrderListLoadMore() {
    var _pageIndex = $("#pageIndex").val();
    _pageIndex = parseInt(_pageIndex) + 1;
    var data = {
        index: _pageIndex
    };
    var url = "/ShareOrder/PartialShareList";
    var _toObj = $(".noclass");
    LoadMore(data, url, _toObj, _pageIndex);
}



//用户中心晒单
function UserShareOrderListLoadMore(id) {
    var _pageIndex = $("#pageIndex").val();
    _pageIndex = parseInt(_pageIndex) + 1;
    var data = {
        pageIndex: _pageIndex,
        id:id
    };
    var url = "/UserCenter/MyShareOrderListPartial";
    var _toObj = $(".noclass");
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
            $("#pageIndex").val(pageIndex);
           // $(".loader").hide();
            if ($.trim(response) == "") {
                $(".pullUpLabel").html("亲，没有更多了哟。");
            }
        }
    });
}