$(function () {
    var blockNumber = 2;
    var inProgress = false;
    window.onscroll = function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height() && !inProgress) {
            inProgress = true;
            $("#loadingDiv").show();
            $.post("/UserTask/InfiniteScroll/", { "blockNumber": blockNumber }, function (data) {
                blockNumber = blockNumber + 1;
                $("#scrollContent").append(data.HtmlString);
                $("#loadingDiv").hide();
                inProgress = false;
            });
        }
    }
})