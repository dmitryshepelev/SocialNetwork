$(function () {
    $("#Chart").click(function (e) {
        e.preventDefault();
        chartBuilder($("#chartName").val(), $("#axisXName").val(), $("#axisYName").val(),
            $("#expression").val(), $("#from").val(), $("#to").val(), $("#step").val());
    });
});

function chartBuilder(chartName, axisXName, axisYName, expression, from, to, step) {
    var arr = [];
    var expr = Parser.parse(expression);
    for (var i = Number(from) ; i < Number(to) ; i = i + Number(step)) {
        arr.push(expr.evaluate({ x: i }));
    }
    var chart = new Highcharts.Chart({
        chart: {
            renderTo: "chart"
        },
        title: {
            text: chartName
        },
        xAxis: {
            title: {
                text: axisXName
            }
        },
        yAxis: {
            title: {
                text: axisYName
            }
        },
        series: [{
            name: chartName,
            data: arr
        }]
    });
}