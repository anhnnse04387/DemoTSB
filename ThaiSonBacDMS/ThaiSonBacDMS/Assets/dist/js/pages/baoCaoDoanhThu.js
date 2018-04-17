Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
};

function getFirstDayOfWeek(date) {
    var currentTime = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var firstDayOfWeek = new Date();
    var dayOfWeek = currentTime.getDay();
    if (dayOfWeek == 0) {
        firstDayOfWeek = currentTime.addDays(-6);
    }
    else {
        firstDayOfWeek = currentTime.addDays(-(dayOfWeek - 1));
    }
    return firstDayOfWeek;
};

function getListDay(year, month) {
    var currentTime = new Date(year, month - 1, 1);
    var dateControl = getFirstDayOfWeek(currentTime);
    var returnMap = [];
    var count = false;
    while (dateControl.getMonth() <= currentTime.getMonth()) {
        var item = {};
        if (count) {
            dateControl = dateControl.addDays(1);
            item['key'] = moment(dateControl).format();
            item['value'] = moment(dateControl).format('DD/MM') + ' => ' + moment(dateControl.addDays(6)).format('DD/MM');
            returnMap.push(item);
        } else {
            item['key'] = moment(dateControl).format();
            item['value'] = moment(dateControl).format('DD/MM') + ' => ' + moment(dateControl.addDays(6)).format('DD/MM');
            count = true;
            returnMap.push(item);
        }
        dateControl = dateControl.addDays(6);
        if (dateControl.addDays(1).getDate() == 1) break;
    }
    return returnMap;
};



$(document).ready(function () {
    var date = new Date();
    $("#selectedYear").val(date.getFullYear());
    $('#selectedMonth').val(date.getMonth() + 1);
    var month = $('#selectedMonth').val();
    var year = $('#selectedYear').val();
    changeWeek(year, month);
    var firstDate = getFirstDayOfWeek(date);
    $("#selectedWeek").val(moment(firstDate).format()).change();
    $("#selectedMonth, #selectedYear").change(function () {
        month = $('#selectedMonth').val();
        year = $("#selectedYear").val();
        if (month == -1) {
            $("#selectedWeek").empty();
            $('#selectedWeek')
                .append($("<option></option>")
                .attr("value", "-1")
                .text("Tất cả"));
        } else {
            $("#selectedWeek").empty();
            changeWeek(year, month);
        }

    });
});

function changeWeek(year, month) {
    $('#selectedWeek')
                .append($("<option></option>")
                .attr("value", "-1")
                .text("Tất cả"));
    $.each(getListDay(year, month), function (i, obj) {
        $('#selectedWeek')
                .append($("<option></option>")
                .attr("value", obj.key)
                .text(obj.value));
    });
};
$(document).ready(function () {
    changeData();
});
var myChart;
function changeData() {
    if (typeof myChart != 'undefined') {
        myChart.destroy();
    }
    var selDay = $('#selectedWeek').val();
    var selMonth = $('#selectedMonth').val();
    var selYear = $('#selectedYear').val();
    var selCate = $('#selectedCate').val();
    var dateData = {
        selectedYear: selYear,
        selectedMonth: selMonth,
        selectedDay: selDay,
        selectedCategory: selCate
    };
    $.ajax({
        url: "/PhanPhoi/BaoCaoDoanhThu/Index",
        data: { model: dateData },
        datatype: "json",
        type: "POST",
        success: function (data) {
            var labels = [];
            var nhapVon = [];
            var xuatVon = [];
            var banChoKhach = [];
            var check = 0;
            data.dataLineChart.forEach(function (entry) {
                labels.push(entry.displayTime);
                nhapVon.push(entry.nhapVon);
                xuatVon.push(entry.xuatVon);
                banChoKhach.push(entry.banChoKhach);
                check += entry.nhapVon;
            });
            //Line Chart
            if (check != 0) {
                $('#lineChartNoData').hide();
                $('#lineChart').show();
                myChart = new Chart(document.getElementById("lineChartThang1"), {
                    type: 'line',
                    data: {
                        labels: labels,
                        datasets: [{
                            data: xuatVon,
                            label: "Xuất vốn",
                            borderColor: "#33FF33",
                            backgroundColor: "#33FF33",
                            fill: false
                        }, {
                            data: nhapVon,
                            label: "Nhập vốn",
                            borderColor: "#EE0000",
                            backgroundColor: "#EE0000",
                            fill: false
                        }, {
                            data: banChoKhach,
                            label: "Bán cho khách",
                            borderColor: "yellow",
                            backgroundColor: "yellow",
                            fill: false
                        }
                        ]
                    },
                    options: {
                        legend: { display: false },
                        tooltips: {
                            mode: 'index',
                            intersect: false,
                        },
                        hover: {
                            mode: 'nearest',
                            intersect: true
                        },
                        elements: {
                            line: {
                                tension: 0, // disables bezier curves
                            }
                        },
                        scales: {
                            yAxes: [{
                                min: 0
                            }]
                        }
                    }
                });
            } else {
                $('#lineChart').hide();
                $('#lineChartNoData').show();
            }
            
            //Pie Chart
            google.charts.load("current", { packages: ["corechart"] });
            google.charts.setOnLoadCallback(drawChart);
            function drawChart() {
                var dataPie = new google.visualization.DataTable();
                dataPie.addColumn('string', 'Danh mục');
                dataPie.addColumn('number', 'Tổng số phần trăm');
                var total = 0;
                for (var i = 0; i < data.dataPieChart.length; i++) {
                    total = total + data.dataPieChart[i].numberSold;
                }
                console.log(total);
                if (total != 0) {
                    for (var i = 0; i < data.dataPieChart.length; i++) {
                        dataPie.addRow([data.dataPieChart[i].categoryName, data.dataPieChart[i].numberSold]);
                    }
                }
                var options = {
                    pieHole: 0.5,
                };
                var chart = new google.visualization.PieChart(document.getElementById('donutchart'));
                chart.draw(dataPie, options);
            }
            //Table
            
            $("#tableBody").empty();
            $("#tableFoot").empty();
            var i = 1;
            var tongNhapVon = 0;
            var tongXuatVon = 0;
            var tongBanChoKhach = 0;
            var tongLoiNhuan = 0;
            data.dataPieChart.forEach(function (entry) {
                $('#tableBody').append('<tr>'
                + '<td style="text-align:center">'+i+'</td>'
                + '<td style="text-align:left">' + entry.categoryName + '</td>'
                + '<td style="text-align:right" class="number">' + entry.nhapVon + '</td>'
                + '<td style="text-align:right" class="number">' + entry.xuatVon + '</td>'
                + '<td style="text-align:right" class="number">' + entry.banChoKhach + '</td>'
                + '<td style="text-align:right" class="number">' + entry.loiNhuan + '</td>'
                + '</tr>');
                i++;
                tongNhapVon += entry.nhapVon;
                tongXuatVon += entry.xuatVon;
                tongBanChoKhach += entry.banChoKhach;
                tongLoiNhuan += entry.loiNhuan;
            });
            $('#tableFoot').append('<tr>'
                + '<td class="noBorder"></td>'
                + '<td style="text-align:center">Tổng cộng</td>'
                + '<td style="text-align:right" class="number">' + tongNhapVon + '</td>'
                + '<td style="text-align:right" class="number">' + tongXuatVon + '</td>'
                + '<td style="text-align:right" class="number">' + tongBanChoKhach + '</td>'
                + '<td style="text-align:right" class="number">' + tongLoiNhuan + '</td>'
                + '</tr>');
            $('.number').autoNumeric('init', { minimumValue: '1', maximumValue: '9999999999999', digitGroupSeparator: ',', mDec: '0' })
        },
        async: false
    });
}



