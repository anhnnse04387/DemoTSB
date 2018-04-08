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

