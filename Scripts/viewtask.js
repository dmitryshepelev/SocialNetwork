$(function () {
    var blockNumber = 2;
    //var noMoreData = false;
    var inProgress = false;
    window.onscroll = function() {
        if ($(window).scrollTop() == $(document).height() - $(window).height() && !inProgress) {
            inProgress = true;
            var taskId = $("#div-main").attr("data-task-id");
            $("#loadingDiv").show();
            $.post("/Comment/InfiniteScroll/", { "taskId": taskId, "blockNumber": blockNumber }, function(data) {
                blockNumber = blockNumber + 1;
                //noMoreData = data.NoMoreData;
                $("#scrollContent").append(data.HtmlString);
                $("#loadingDiv").hide();
                inProgress = false;
            });
        }
    }
})