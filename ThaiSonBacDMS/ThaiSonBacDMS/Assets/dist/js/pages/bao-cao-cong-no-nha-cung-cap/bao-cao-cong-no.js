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
var timeFormat = 'MM/DD/YYYY HH:mm';
function newDate2(days) {
    var date = new Date(days);
    return date;
}

var config = {
    type: 'line',
    data: {
        labels: [// Date Objects
            newDate2('12/1/2017'),
            newDate2('12/5/2017'),
            newDate2('12/10/2017'),
            newDate2('12/15/2017'),
            newDate2('12/20/2017'),
            newDate2('12/25/2017'),
            newDate2('12/30/2017')
        ],
        datasets: [{
                label: "CB - Aptomat",
                backgroundColor: '#00ed17',
                borderColor: '#00ed17',
                fill: false,
                data: [{
                        x: newDate2('12/1/2017'),
                        y: 1.5
                    }, {
                        x: newDate2('12/6/2017'),
                        y: 4.2
                    }, {
                        x: newDate2('12/12/2017'),
                        y: 3.1
                    }, {
                        x: newDate2('12/31/2017'),
                        y: 4
                    }]
            },
            {
                label: "Inverter: Biến tần",
                backgroundColor: '#ed000b',
                borderColor: '#ed000b',
                fill: false,
                data: [{
                        x: newDate2('12/1/2017'),
                        y: 3
                    }, {
                        x: newDate2('12/4/2017'),
                        y: 5
                    }, {
                        x: newDate2('12/8/2017'),
                        y: 2.2
                    }, {
                        x: newDate2('12/12/2017'),
                        y: 2.5
                    }, {
                        x: newDate2('12/31/2017'),
                        y: 2.9
                    }]
            },
            {
                label: "Cáp thông tin",
                backgroundColor: '#4286f4',
                borderColor: '#4286f4',
                fill: false,
                data: [{
                        x: newDate2('12/1/2017'),
                        y: 1
                    }, {
                        x: newDate2('12/15/2017'),
                        y: 4.1
                    }, {
                        x: newDate2('12/22/2017'),
                        y: 4.3
                    }, {
                        x: newDate2('12/26/2017'),
                        y: 2.8
                    }, {
                        x: newDate2('12/31/2017'),
                        y: 2.4
                    }]
            },
            {
                label: "Công tắc ổ cắm",
                backgroundColor: '#8f41f4',
                borderColor: '#8f41f4',
                fill: false,
                data: [{
                        x: newDate2('12/1/2017'),
                        y: 2.4
                    }, {
                        x: newDate2('12/3/2017'),
                        y: 1.1
                    }, {
                        x: newDate2('12/8/2017'),
                        y: 3.5
                    }, {
                        x: newDate2('12/16/2017'),
                        y: 4.4
                    }, {
                        x: newDate2('12/31/2017'),
                        y: 4.8
                    }]
            },
            {
                label: "Busway - Thanh cái dẫn điện & MTR - Máy biến áp & RMU - Tủ trung thế",
                backgroundColor: '#daf200',
                borderColor: '#daf200',
                fill: false,
                data: [{
                        x: newDate2('12/1/2017'),
                        y: 2.9
                    }, {
                        x: newDate2('12/3/2017'),
                        y: 1.1
                    }, {
                        x: newDate2('12/24/2017'),
                        y: 0.5
                    }, {
                        x: newDate2('12/30/2017'),
                        y: 3.6
                    }, {
                        x: newDate2('12/31/2017'),
                        y: 1.9
                    }]
            }]
    },
    options: {
        elements: {
            line: {
                tension: 0 // disables bezier curves
            }
        },
        scales: {
            xAxes: [{
                    type: "time",
                    time: {
                        format: timeFormat,
                        // round: 'day'
                        tooltipFormat: 'DD/MM',
                        displayFormats: {
                            'day': 'DD/MM'
                        }
                    }
                }
            ], yAxes: [{
                    ticks: {
                        min: 0,
                        stepSize: 1,
                        max: 5
                    }
                }]
        }
    }
};

var config2 = {
    type: 'line',
    data: {
        labels: [// Date Objects
            newDate2('12/1/2017'),
            newDate2('12/5/2017'),
            newDate2('12/10/2017'),
            newDate2('12/15/2017'),
            newDate2('12/20/2017'),
            newDate2('12/25/2017'),
            newDate2('12/30/2017')
        ],
        datasets: [{
                label: "CB - Aptomat",
                backgroundColor: '#00ed17',
                borderColor: '#00ed17',
                fill: false,
                data: [{
                        x: newDate2('12/1/2017'),
                        y: 1.8
                    }, {
                        x: newDate2('12/3/2017'),
                        y: 3.6
                    }, {
                        x: newDate2('12/12/2017'),
                        y: 2.2
                    }, {
                        x: newDate2('12/31/2017'),
                        y: 4
                    }]
            },
            {
                label: "Inverter: Biến tần",
                backgroundColor: '#ed000b',
                borderColor: '#ed000b',
                fill: false,
                data: [{
                        x: newDate2('12/1/2017'),
                        y: 3
                    }, {
                        x: newDate2('12/6/2017'),
                        y: 3.5
                    }, {
                        x: newDate2('12/11/2017'),
                        y: 2.4
                    }, {
                        x: newDate2('12/12/2017'),
                        y: 2.9
                    }, {
                        x: newDate2('12/31/2017'),
                        y: 3.5
                    }]
            },
            {
                label: "Cáp thông tin",
                backgroundColor: '#4286f4',
                borderColor: '#4286f4',
                fill: false,
                data: [{
                        x: newDate2('12/1/2017'),
                        y: 1
                    }, {
                        x: newDate2('12/15/2017'),
                        y: 3.1
                    }, {
                        x: newDate2('12/22/2017'),
                        y: 3.5
                    }, {
                        x: newDate2('12/26/2017'),
                        y: 2.9
                    }, {
                        x: newDate2('12/31/2017'),
                        y: 2.6
                    }]
            },
            {
                label: "Công tắc ổ cắm",
                backgroundColor: '#8f41f4',
                borderColor: '#8f41f4',
                fill: false,
                data: [{
                        x: newDate2('12/1/2017'),
                        y: 2.4
                    }, {
                        x: newDate2('12/5/2017'),
                        y: 1.15
                    }, {
                        x: newDate2('12/8/2017'),
                        y: 3.6
                    }, {
                        x: newDate2('12/16/2017'),
                        y: 4.45
                    }, {
                        x: newDate2('12/31/2017'),
                        y: 4.9
                    }]
            },
            {
                label: "Busway - Thanh cái dẫn điện & MTR - Máy biến áp & RMU - Tủ trung thế",
                backgroundColor: '#daf200',
                borderColor: '#daf200',
                fill: false,
                data: [{
                        x: newDate2('12/1/2017'),
                        y: 0.3
                    }, {
                        x: newDate2('12/3/2017'),
                        y: 1.15
                    }, {
                        x: newDate2('12/24/2017'),
                        y: 0.5
                    }, {
                        x: newDate2('12/30/2017'),
                        y: 3.46
                    }, {
                        x: newDate2('12/31/2017'),
                        y: 1.9
                    }]
            }]
    },
    options: {
        elements: {
            line: {
                tension: 0 // disables bezier curves
            }
        },
        scales: {
            xAxes: [{
                    type: "time",
                    time: {
                        format: timeFormat,
                        // round: 'day'
                        tooltipFormat: 'DD/MM',
                        displayFormats: {
                            'day': 'DD/MM'
                        }
                    }
                }
            ], yAxes: [{
                    ticks: {
                        min: 0,
                        stepSize: 1,
                        max: 5
                    }
                }]
        }
    }
};

var config3 = {
    type: 'line',
    data: {
        labels: [// Date Objects
            newDate2('12/1/2017'),
            newDate2('12/5/2017'),
            newDate2('12/10/2017'),
            newDate2('12/15/2017'),
            newDate2('12/20/2017'),
            newDate2('12/25/2017'),
            newDate2('12/30/2017')
        ],
        datasets: [{
                label: "CB - Aptomat",
                backgroundColor: '#00ed17',
                borderColor: '#00ed17',
                fill: false,
                data: [{
                        x: newDate2('12/1/2017'),
                        y: 1.8
                    }, {
                        x: newDate2('12/16/2017'),
                        y: 1.2
                    }, {
                        x: newDate2('12/26/2017'),
                        y: 3.11
                    }, {
                        x: newDate2('12/31/2017'),
                        y: 4.6
                    }]
            },
            {
                label: "Inverter: Biến tần",
                backgroundColor: '#ed000b',
                borderColor: '#ed000b',
                fill: false,
                data: [{
                        x: newDate2('12/1/2017'),
                        y: 3.2
                    }, {
                        x: newDate2('12/4/2017'),
                        y: 2.5
                    }, {
                        x: newDate2('12/14/2017'),
                        y: 2.2
                    }, {
                        x: newDate2('12/18/2017'),
                        y: 2.5
                    }, {
                        x: newDate2('12/31/2017'),
                        y: 2.9
                    }]
            },
            {
                label: "Cáp thông tin",
                backgroundColor: '#4286f4',
                borderColor: '#4286f4',
                fill: false,
                data: [{
                        x: newDate2('12/1/2017'),
                        y: 1.2
                    }, {
                        x: newDate2('12/15/2017'),
                        y: 3.1
                    }, {
                        x: newDate2('12/22/2017'),
                        y: 4.8
                    }, {
                        x: newDate2('12/26/2017'),
                        y: 2.8
                    }, {
                        x: newDate2('12/31/2017'),
                        y: 2.4
                    }]
            },
            {
                label: "Công tắc ổ cắm",
                backgroundColor: '#8f41f4',
                borderColor: '#8f41f4',
                fill: false,
                data: [{
                        x: newDate2('12/1/2017'),
                        y: 3.1
                    }, {
                        x: newDate2('12/7/2017'),
                        y: 0.5
                    }, {
                        x: newDate2('12/8/2017'),
                        y: 2.1
                    }, {
                        x: newDate2('12/16/2017'),
                        y: 4.8
                    }, {
                        x: newDate2('12/31/2017'),
                        y: 4.3
                    }]
            },
            {
                label: "Busway - Thanh cái dẫn điện & MTR - Máy biến áp & RMU - Tủ trung thế",
                backgroundColor: '#daf200',
                borderColor: '#daf200',
                fill: false,
                data: [{
                        x: newDate2('12/1/2017'),
                        y: 2.6
                    }, {
                        x: newDate2('12/5/2017'),
                        y: 1.15
                    }, {
                        x: newDate2('12/20/2017'),
                        y: 0.8
                    }, {
                        x: newDate2('12/30/2017'),
                        y: 3.4
                    }, {
                        x: newDate2('12/31/2017'),
                        y: 2.2
                    }]
            }]
    },
    options: {
        elements: {
            line: {
                tension: 0 // disables bezier curves
            }
        },
        scales: {
            xAxes: [{
                    type: "time",
                    time: {
                        format: timeFormat,
                        // round: 'day'
                        tooltipFormat: 'DD/MM',
                        displayFormats: {
                            'day': 'DD/MM'
                        }
                    }
                }
            ], yAxes: [{
                    ticks: {
                        min: 0,
                        stepSize: 1,
                        max: 5
                    }
                }]
        }
    }
};



