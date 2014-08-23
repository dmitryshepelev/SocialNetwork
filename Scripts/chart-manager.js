$(function () {
    $("#Chart").click(function (e) {
        e.preventDefault();
        chartBuilder($("#ChartName").val(), $("#AxisXName").val(), $("#AxisYName").val(),
            $("#Expression").val(), $("#From").val(), $("#To").val(), $("#Step").val());
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