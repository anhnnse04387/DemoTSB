/*
 * Date picker
 *
 */
$(function () {
    //Initialize Select2 Elements
    $('.select2').select2();
});

$('input[name=week]').datepicker({
    format: "dd-mm-yyyy",
    autoclose: true,
    orientation: 'top auto'
}).on('show', function (e) {
    var td = $('td', '.datepicker-days');
    td.mouseover(function () {
    }).mouseout(function () {
        $('.week', '.datepicker-days').removeClass('week');
    });
}).on('hide', function (e) {

    console.log($(this).val());
});

$('.input-group-addon', '.week-select').click(function () {
    $('input[name=week]').datepicker('show');
});

/*
 * DataTable
 *
 */
$(function () {
    $('#example1').DataTable();
    $('#example2').DataTable();
    $('#example3').DataTable();
});

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
}
var defaultLegendClickHandler = Chart.defaults.global.legend.onClick;
var newLegendClickHandler = function (e, legendItem) {
    var index = legendItem.datasetIndex;

    if (index > 1) {
        // Do the original logic
        defaultLegendClickHandler(e, legendItem);
    } else {
        let ci = this.chart;
        [ci.getDatasetMeta(0),
         ci.getDatasetMeta(1)].forEach(function(meta) {
            meta.hidden = meta.hidden === null? !ci.data.datasets[index].hidden : null;
        });
        ci.update();
    }
};
window.onload = function () {
    var ctx = document.getElementById("lineChart").getContext("2d");
    var ctx2 = document.getElementById("lineChart2").getContext("2d");
    var ctx3 = document.getElementById("lineChart3").getContext("2d");
    window.myLine = new Chart(ctx, config);
    window.myLine = new Chart(ctx2, config2);
    window.myLine = new Chart(ctx3, config);
};