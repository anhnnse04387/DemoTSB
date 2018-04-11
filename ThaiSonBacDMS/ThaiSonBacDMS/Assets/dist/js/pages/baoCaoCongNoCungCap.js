Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
};

$(document).ready(function () {
    $('.number').autoNumeric('init', { minimumValue: '1', maximumValue: '9999999999999', digitGroupSeparator: ',', mDec: '0' })
});

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
    $("#selectedWeek").val(moment(firstDate).format());
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


/*
 * Chart JS
 *
 */


$(document).ready(function () {
    changeData();
});

var lineChart1;
var lineChart2;
var lineChart3;
function changeData() {
    if (typeof lineChart1 != 'undefined') {
        lineChart1.destroy();
    }
    if (typeof lineChart2 != 'undefined') {
        lineChart2.destroy();
    }
    if (typeof lineChart3 != 'undefined') {
        lineChart3.destroy();
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
        url: "/PhanPhoi/BaoCaoCongNoCungCap/ChangeData",
        data: { model: dateData },
        datatype: "json",
        type: "POST",
        success: function (data) {            
            //Begin Data TSN
            var myMap = new Map();
            var lavelValue = [];
            $.each(data.supp_TSN, function (index, value) {
                lavelValue.push(index);
                value.forEach(function (entry) {
                    if (myMap.has(entry.categoryName)) {
                        myMap.get(entry.categoryName).push({
                            totalQuantity: entry.totalQuantity,
                            totalPrice: entry.totalPrice
                        });
                    } else {
                        var totalMap = [];
                        totalMap.push({
                            totalQuantity: entry.totalQuantity,
                            totalPrice: entry.totalPrice
                        });
                        myMap.set(entry.categoryName, totalMap);
                    }
                });
            });
            var datasetsValue = [];          
            var dataTable1 = [];
            myMap.forEach(function (value, key) {
                var color = randomColorGenerator();
                var arraySet = [];
                var totalQuantity = 0;
                var totalPrice = 0;
                value.forEach(function (entry) {
                    totalPrice += entry.totalPrice;
                    totalQuantity += entry.totalQuantity;
                    arraySet.push(entry.totalQuantity);
                });
                dataTable1.push({
                    category: key,
                    quantity: totalQuantity,
                    price : totalPrice
                });
                datasetsValue.push({
                    data: arraySet,
                    label: key,
                    borderColor: color,
                    backgroundColor: color,
                    fill: false
                });

            });
            console.log(dataTable1);
            //Chart
            lineChart1 = new Chart(document.getElementById('lineChart'), {
                type: 'line',
                data: {
                    labels: lavelValue,
                    datasets: datasetsValue
                },
                options: {
                    legend: { display: true },
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
                            ticks: {
                                min: 0,
                            }
                        }]
                    }
                }
            });
            //Total Price
            $('#sumTSN').replaceWith('<h5 class="description-header number" id="sumTSN">' + data.sumTNS + '</h5>');
            //Table
            $("#bodyTSN").empty();
            $("#footTSN").empty();
            var i = 1;
            var sumQuantityTSN = 0;
            var sumPriceTSN = 0;
            dataTable1.forEach(function (entry) {
                $('#bodyTSN').append('<tr>'
                + '<td class="text-center">' + i + '</td>'
                + '<td style="text-align:left" class="number">' + entry.category + '</td>'
                + '<td style="text-align:right" class="number">' + entry.quantity + '</td>'
                + '<td style="text-align:right" class="number">' + entry.price + '</td>'
                + '</tr>');
                i++;
                sumQuantityTSN += entry.quantity;
                sumPriceTSN += entry.price;
            });
            $('#footTSN').append('<tr>'
                + '<tr style="background-color: #544d61; color: white; font-weight: bold;">'
                + '<td colspan="2" style="font-size:17px" class="text-center">Tổng cộng</td>'
                + '<td class="text-right number">' + sumQuantityTSN + '</td>'
                + '<td class="text-right number">' + sumPriceTSN + '</td>'
                + '</tr>');
            //End data TSN

            //Chart 2
            var myMap2 = new Map();
            var lavelValue2 = [];
            $.each(data.supp_LS, function (index, value) {
                lavelValue2.push(index);
                value.forEach(function (entry) {
                    if (myMap2.has(entry.categoryName)) {
                        myMap2.get(entry.categoryName).push(entry.totalQuantity);
                    } else {
                        var totalMap = [];
                        totalMap.push(entry.totalQuantity);
                        myMap2.set(entry.categoryName, totalMap);
                    }
                });
            });

            var datasetsValue2 = [];
            myMap2.forEach(function (value, key) {
                var color = randomColorGenerator();
                datasetsValue2.push({
                    data: value,
                    label: key,
                    borderColor: color,
                    backgroundColor: color,
                    fill: false
                });
            });

            lineChart2 = new Chart(document.getElementById('lineChart2'), {
                type: 'line',
                data: {
                    labels: lavelValue2,
                    datasets: datasetsValue2
                },
                options: {
                    legend: { display: true },
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
                            ticks: {
                                min: 0,
                            }
                        }]
                    }
                }
            });
            $('#sumLS').replaceWith('<h5 class="description-header number" id="sumLS">' + data.sumLS + '</h5>');
            //Chart 3
            var myMap3 = new Map();
            var lavelValue3 = [];
            $.each(data.supp_HanQuoc, function (index, value) {
                lavelValue3.push(index);
                value.forEach(function (entry) {
                    if (myMap3.has(entry.categoryName)) {
                        myMap3.get(entry.categoryName).push(entry.totalQuantity);
                    } else {
                        var totalMap = [];
                        totalMap.push(entry.totalQuantity);
                        myMap3.set(entry.categoryName, totalMap);
                    }
                });
            });

            var datasetsValue3 = [];
            myMap3.forEach(function (value, key) {
                var color = randomColorGenerator();
                datasetsValue3.push({
                    data: value,
                    label: key,
                    borderColor: color,
                    backgroundColor: color,
                    fill: false
                });
            });

            lineChart3 = new Chart(document.getElementById('lineChart3'), {
                type: 'line',
                data: {
                    labels: lavelValue3,
                    datasets: datasetsValue3
                },
                options: {
                    legend: { display: true },
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
                            ticks: {
                                min: 0,
                            }
                        }]
                    }
                }
            });
            $('#sumHanQuoc').replaceWith('<h5 class="description-header number" id="sumHanQuoc">' + data.sumHanQuoc + '</h5>');
        },
        async: false
    });
};

var randomColorGenerator = function () {
    return '#' + (Math.random().toString(16) + '0000000').slice(2, 8);
};

function drawData(data) {
    //Chart 1
    var myMap = new Map();
    var lavelValue = [];
    $.each(data, function (index, value) {
        lavelValue.push(index);
        value.forEach(function (entry) {
            if (myMap.has(entry.categoryName)) {
                myMap.get(entry.categoryName).push(entry.totalQuantity);
            } else {
                var totalMap = [];
                totalMap.push(entry.totalQuantity);
                myMap.set(entry.categoryName, totalMap);
            }
        });
    });

    var datasetsValue = [];
    myMap.forEach(function (value, key) {
        var color = randomColorGenerator();
        datasetsValue.push({
            data: value,
            label: key,
            borderColor: color,
            backgroundColor: color,
            fill: false
        });
    });

    new Chart(chartData, {
        type: 'line',
        data: {
            labels: lavelValue,
            datasets: datasetsValue
        },
        options: {
            legend: { display: true },
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
                    ticks: {
                        min: 0,
                    }
                }]
            }
        }
    });
}