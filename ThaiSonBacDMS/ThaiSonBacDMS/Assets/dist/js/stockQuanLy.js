$(document).ready(function () {
    $('.number').autoNumeric('init', { minimumValue: '1', maximumValue: '9999999999999', digitGroupSeparator: ',', decimalPlacesOverride: '0' });
    $('.datepicker').datepicker({
        autoclose: true,
        startDate: new Date()
    });
    $("#btnSubmit").click(function (event) {
        if (validate() === 0) {
            event.preventDefault();
            $('#confirmSubmit').modal();
        } else {
            $('form')[0].checkValidity();
        }
    });
    $('#status').on('change', function () {
        if ($(this).prop("checked")) {
            $('#namePi').removeClass('noDisplay');
            $('#nameLo').addClass('noDisplay');
            $('#valueLo').addClass("noDisplay");
            $('#valueLo').removeAttr("required");
            $('#valuePi').removeClass('noDisplay');
            $('#valuePi').attr("required");
        } else {
            $('#nameLo').removeClass('noDisplay');
            $('#namePi').addClass('noDisplay');
            $('#valuePi').addClass("noDisplay");
            $('#valuePi').removeAttr("required");
            $('#valueLo').removeClass('noDisplay');
            $('#valueLo').attr("required");
        }
    });
    $('#valuePi').on('change', function () {
        $.ajax({
            url: "/QuanLy/Stock/ChooseNo",
            data: { no: $(this).val(), status: true },
            datatype: "html",
            success: function (data) {
                $("#lstItem").html(data);
                document.getElementById('address').value = $('#addressChange').val();
                document.getElementById('tel').value = $('#telChange').val();
                document.getElementById('donVi').value = $('#donViChange').val();
                $('#dayRequested').datepicker('setDate', $('#dateRequestedChange').val());
            }
        });
    });
    $('#valueLo').on('change', function () {
        $.ajax({
            url: "/QuanLy/Stock/ChooseNo",
            data: { no: $(this).val(), status: false },
            datatype: "json",
            success: function (data) {
                $("#lstItem").html(data);
                document.getElementById('address').value = $('#addressChange').val();
                document.getElementById('tel').value = $('#telChange').val();
                document.getElementById('donVi').value = $('#donViChange').val();
                $('#dayRequested').datepicker('setDate', $('#dateRequestedChange').val());
            }
        });
    });
});

function calc() {
    var qttAll = 0;
    var lst = $('.qtt');
    for (var i = 0; i < lst.length; i++) {
        if (lst.eq(i).val() !== "") {
            qttAll += parseInt(lst.eq(i).val());
        }
    }
    $('#qttAll').val(qttAll);
}

function getAllData() {
    var items = [];
    var status = true;
    var no = $('#valuePi').val();
    if ($('#status').prop("checked") === false) {
        status = false;
        no = $('#valueLo').val();
    }
    var dateImported = $('#dayImported').val();
    $('.item').each(function () {
        var productId = parseInt($(this).find('.productId').val());
        var qtt = parseInt($(this).find('.qtt').val());
        var note = $(this).find('.note').val();
        var item = {
            Quantities: qtt,
            Product_ID: productId,
            Note: note
        };
        items.push(item);
    });
    var data = {
        no: no, status: status,
        dateImported: dateImported, items: items
    };
    return JSON.stringify({ 'model': data });
}

function doneOrder() {
    $.ajax({
        url: '/QuanLy/Stock/CheckOut',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        data: getAllData(),
        success: function () {
            document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="/Assets/dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
            swal({
                title: '<img src="/Assets/dist/img/messagePic_6.png"/>',
                type: 'success'
            }).then((result) => {
                if (result.value) {
                    window.location.href = '/QuanLy/Stock/History';
                }
            });
        },
        error: function () {
            swal({
                title: '<img src="/Assets/dist/img/messagePic_9.png"/>',
                type: 'error',
                showCancelButton: false,
                showConfirmButton: true
            });
        }
    });
}

function validate() {
    var count = 0;
    if ($('#status').prop("checked")) {
        if ($('#valuePi').val() === "") {
            count++;
        }
    } else {
        if ($('#valueLo').val() === "") {
            count++;
        }
    }
    if ($('#donVi').val() === "") {
        count++;
    }
    if ($('#address').val() === "") {
        count++;
    }
    if ($('#tel').val() === "") {
        count++;
    }
    if ($('#dayRequested').val() === "") {
        count++;
    }
    if ($('#dayImported').val() === "") {
        count++;
    }
    arr = $(".qtt");
    for (var i = 0; i < arr.length; i++) {
        if (arr.eq(i).val() === "") {
            count++;
        }
    }
    return count;
}