function likes(taskId) {
    var likeValue = !$("#isLiked" + taskId).is(":checked");
    $.ajax({
        url: "/UserTask/LikeValueChanged",
        type: "GET",
        data: "likeValue=" + likeValue + "&taskId=" + taskId
    }).done(function (data) {
        $("#taskStatistic-" + taskId).children().remove();
        $("#taskStatistic-" + taskId).append(data);
    });
}