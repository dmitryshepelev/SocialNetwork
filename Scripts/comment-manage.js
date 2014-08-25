function commentDelete(id) {
    $.ajax({
        type: "GET",
        url: "/Comment/DeleteComment",
        data: "id=" + id
    }).done(function(result) {
        
    });
}