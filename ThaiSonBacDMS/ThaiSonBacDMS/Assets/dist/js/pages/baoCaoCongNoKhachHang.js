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
var myChart; 
$(document).ready(function () {
    changeData();
});

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
        url: "/PhanPhoi/BaoCaoCongNoKhachHang/ChangeData",
        data: { model: dateData },
        datatype: "json",
        type: "POST",
        success: function (data) {
            var randomColorGenerator = function () {
                return '#' + (Math.random().toString(16) + '0000000').slice(2, 8);
            };

            var myMap = new Map();
            var lavelValue = [];
            $.each(data.dataCongNo, function (index, value) {
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
            
            myChart = new Chart(document.getElementById("lineChart"), {
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
            $("#totalPrice").replaceWith("<span id='totalPrice'>" + data.totalPrice + "</span>");            
            
        },
        async: false
    });

}




