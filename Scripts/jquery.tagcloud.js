$().ready(function () {
    var catContainer = $('#tagcloud');
    var categories = catContainer.find('a');
    var maxPercent = 200, minPercent = 80;
    var max = 1, min = 999, count = 0;
    categories.each(function (i) {
        count = parseInt($(this).attr('rel'));
        max = (count > max ? count : max);
        min = (min > count ? count : min);
    });
    var multiplier = (maxPercent - minPercent) / (max - min);
    categories.each(function (i) {
        count = parseInt($(this).attr('rel'));
        console.log(count);
        var size = minPercent + ((max - (max - (count - min))) * multiplier) + '%';
        console.log(size);
        $(this).css("font-size", size);
    });
});