﻿function upload(urlAction) {
    $('#fileUpload').fileupload({
        url: urlAction,
        dataType: 'json',
        add: function (e, data) {
            $("#url").val("");
            data.submit();
        },
        done: function (e, data) {
            $("#url").val(data.result.url);
        },
        progressall: function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            $('#progress .progress-bar').css(
                'width',
                progress + '%'
            );
        }
    });
};

function videoAdd(urlAction, urlVideo) {
    $("#addVideoButton").click(function() {
        $.ajax({
            url: urlAction,
            dataType: "json",
            type: "POST",
            data: urlVideo
        }).done(function (data) {
            $("#showVideo").children().remove();
            $("#showVideo").append(data.param);
            $("#url").val(data.url);
        });
    });
}

function likes() {
    $("input:checkbox").change(function () {
        var likeValue = $(this).is(":checked");
        var taskId = $(this).prev().val();
        $.ajax({
            url: "/UserTask/LikeValueChanged",
            type: "GET",
            data: "likeValue=" + likeValue + "&taskId=" + taskId
        }).done(function(data) {
            $("#taskStatistic-" + taskId).children().remove();
            $("#taskStatistic-" + taskId).append(data);
        });
    });
}