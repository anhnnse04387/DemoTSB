$(document).ready(function () {
    $('#btnSearch').click(function () {
        $.ajax({
            url: "/PhanPhoi/OrderList/Search",
            contentType: 'application/json; charset=utf-8',
            datatype: 'json',
            data: getData(),
            success: function (data) {
                $('#item').html(data);
                setTable();
            }
        });
    });
    setTable();
});

function clickBtn(btn) {
    var thisRow = $(btn).parents("tr");
    $('#orderIdToCheckout').val(thisRow.find('.orderId').text());
    $('#confirmDone').modal();
}

function doneOrder() {
    $.ajax({
        url: '/PhanPhoi/OrderList/DoneOrder',
        type: 'POST',
        dataType: 'json',
        data: { orderId: $('#orderIdToCheckout').val() },
        success: function () {
            document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="/Assets/dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
            swal({
                title: '<img src="/Assets/dist/img/messagePic_6.png"/>',
                type: 'success'
            }).then((result) => {
                if (result.value) {
                    window.location.reload();
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

function customerATC() {
    $('#customer').autocomplete({
        source: function (req, responseFn) {
            var re = req.term;
            if (re.length > 2) {
                $.ajax({
                    url: '/PhanPhoi/OrderList/ChooseCustomer',
                    data: {
                        name: re
                    },
                    dataType: "json",
                    success: function (data) {
                        var array = data.error ? [] : $.map(data, function (m) {
                            return {
                                label: m,
                                value: m
                            };
                        });
                        responseFn(array);
                    }
                });
            }
        }
    }).autocomplete("widget").addClass("fixed-height");
}

function setTable() {
    $('#dtTable').DataTable({
        'paging': true,
        'lengthChange': true,
        'searching': false,
        'ordering': true,
        'info': true,
        'autoWidth': true,
        'language': {
            "info": "Đang hiển thị _START_ tới _END_ tổng số _TOTAL_ kết quả",
            "lengthMenu": "Hiển thị _MENU_ kết quả",
            "paginate": {
                "first": "Trang đầu",
                "last": "Trang cuối",
                "next": "Tiếp",
                "previous": "Trước"
            }
        }
    });
    customerATC();
    $('.datepicker').datepicker();
    $('.number').autoNumeric('init', { minimumValue: '1', maximumValue: '9999999999999', digitGroupSeparator: ',', decimalPlacesOverride: '0' });
}
function getData() {
    var orderId = $('#orderId').val();
    var customer = $('#customer').val();
    var fromDate = $('#fromDate').val();
    var toDate = $('#toDate').val();
    var status = $('#status').val();
    var type = $('#type').val();
    var fromTotal = $('#fromTotal').val();
    var toTotal = $('#toTotal').val();
    var model = {
        orderId: orderId, customer: customer, fromDate: fromDate, toDate: toDate,
        fromTotal: fromTotal, toTotal: toTotal, statusId: status, orderType: type
    };
    return model;
}