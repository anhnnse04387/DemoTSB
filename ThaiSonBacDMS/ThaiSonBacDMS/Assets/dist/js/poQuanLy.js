$(document).ready(function () {
    $('.datepicker').datepicker();
    $('#supplier').change(function () {
        var id = parseInt($(this).val());
        $.ajax({
            url: "/QuanLy/PO/ChangeSupplier",
            data: { supplierId: id },
            datatype: "json",
            success: function (data) {
                document.getElementById('address').value = data.address;
                document.getElementById('tel').value = data.tel;
                document.getElementById('email').value = data.email;
            }
        });
    });
    $("#btnSubmit").click(function (event) {
        if (validate() === 0) {
            event.preventDefault();
            $('#confirmSubmit').modal();
        } else {
            $('form')[0].checkValidity();
        }
    });
    addLstItem();
    init();
});

function toBeContinued() {
    swal({
        title: 'Chức năng này đang được phát triển!',
        type: "warning",
    });
}

function deleteRow(comp) {
    comp.closest("table").deleteRow(comp.closest("tr").rowIndex);
    calcTotal();
}

function init() {
    $('.number').autoNumeric('init', { minimumValue: '1', maximumValue: '9999999999999', digitGroupSeparator: ',', decimalPlacesOverride: '0' });
    productATC();
    configQtt();
}

function addLstItem() {
    $('#addItem').click(function () {
        var rowCount = $('.addingRow').length + 1;
        var div = '<tr class="addingRow">'
                + '<td>' + rowCount + '</td>'
                + '<td style="vertical-align: middle;"><input type="text" required class="form-control code" />'
                + '<input type="hidden" class="productId" /></td>'
                + '<td><input type="text" class="form-control qtt" style="text-align: right;" required /></td>'
                + '<td><output class="per number" style="text-align: right;"></output></td>'
                + '<td><output class="subtotal number" style="text-align: right;"></output></td>'
                + '<td><input type="text" class="form-control note"/></td>'
                + '<td style="vertical-align: middle;">'
                + '<a data-toggle="tooltip" title="Xóa" data-placement="bottom" onclick="deleteRow(this)" href="javascript:void(0)">'
                + '<i class="fa fa-close text-red" style="color: #3c8dbc; font-size: 20px"></i>'
                + '</a></td>'
                + '</tr>';
        $('.addingRow').last().after(div);
        init();
    });
}

function productATC() {
    $('.code').each(function () {
        var thisRow = $(this).parents("tr");
        $(this).autocomplete({
            source: function (req, responseFn) {
                var re = req.term;
                if (re.length > 2) {
                    $.ajax({
                        url: '/QuanLy/PO/ChooseProduct',
                        data: {
                            input: re
                        },
                        dataType: "json",
                        success: function (data) {
                            var array = data.error ? [] : $.map(data, function (m) {
                                return {
                                    label: m.Product_code,
                                    value: m.Product_ID,
                                    price: m.CIF_USD
                                };
                            });
                            responseFn(array);
                        }
                    });
                } else {
                    $(this).val("");
                }
            },
            select: function (event, ui) {
                event.preventDefault();
                thisRow.find(".code").val(ui.item.label);
                thisRow.find(".productId").val(ui.item.value);
                thisRow.find(".per").autoNumeric('set', ui.item.price);
            }, change: function (event, ui) {
                if (ui.item) {
                    event.preventDefault();
                    thisRow.find(".code").val(ui.item.label);
                    thisRow.find(".productId").val(ui.item.value);
                    thisRow.find(".per").autoNumeric('set', ui.item.price);
                } else {
                    thisRow.find(".code").val("");
                }
            }, focus: function (event, ui) {
                thisRow.find(".code").val(ui.item.label);
                event.preventDefault();
            }
        }).autocomplete("widget").addClass("fixed-height");
    });
}

function getAllData() {
    var items = [];
    var supplierId = $('#supplier').val();
    var no = $('#no').val();
    var dateCreate = $('#dateCreate').val();
    var dateRequest = $('#dateRequest').val();
    var payment = $('#payment').val();
    var total = parseFloat($('#total').val().replace(new RegExp(',', 'g'), ''));
    $('.addingRow').each(function () {
        var price = parseFloat($(this).find('.subtotal').val().replace(new RegExp(',', 'g'), ''));
        var productId = $(this).find('.productId').val();
        var qtt = $(this).find('.qtt').val();
        var note = $(this).find('.note').val();
        var item = {
            Price: price,
            Quantity: qtt,
            Product_ID: productId,
            NOTE: note
        };
        items.push(item);
    });
    var data = {
        PO_no: no, Supplier_ID: supplierId,
        Date_create: dateCreate, Date_request_ex_work: dateRequest,
        Payment: payment, items: items, Total_price: total
    };
    return data;
}

function doneOrder() {
    $.ajax({
        url: '/QuanLy/PO/CheckOut',
        type: 'POST',
        dataType: 'json',
        data: { model: getAllData() },
        success: function () {
            document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="/Assets/dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
            swal({
                title: '<img src="/Assets/dist/img/messagePic_6.png"/>',
                type: 'success'
            }).then((result) => {
                if (result.value) {
                    window.location.href = '/QuanLy/PO/History';
                }
            });
        },
        error: function () {
            swal({
                title: '<img src="/Assets/dist/img/messagePic_9.png"/>',
                text: '<img src="/Assets/dist/img/messagePic_8.png"/>',
                type: 'error',
                showCancelButton: false,
                showConfirmButton: true
            });
        }
    });
}

function validate() {
    var count = 0;
    if ($('#supplier').val() === "") {
        count++;
    }
    if ($('#no').val() === "") {
        count++;
    }
    if ($('#address').val() === "") {
        count++;
    }
    if ($('#tel').val() === "") {
        count++;
    }
    if ($('#email').val() === "") {
        count++;
    }
    if ($('#payment').val() === "") {
        count++;
    }
    if ($('#dateCreate').val() === "") {
        count++;
    }
    if ($('#dateRequest').val() === "") {
        count++;
    }
    var arr = $(".code");
    for (var i = 0; i < arr.length; i++) {
        if (arr.eq(i).val() === "") {
            count++;
        }
    }
    arr = $(".qtt");
    for (var i = 0; i < arr.length; i++) {
        if (arr.eq(i).val() === "") {
            count++;
        }
    }
    return count;
}

function configQtt() {
    $('.qtt').each(function () {
        $(this).keyup(function () {
            var qtt = $(this).val();
            var thisRow = $(this).parents("tr");
            if (parseInt(qtt) > 0) {
                thisRow.find(".subtotal").autoNumeric('set', parseInt(qtt) * parseFloat(thisRow.find(".per").val().replace(new RegExp(',', 'g'), '')));
            } else {
                $(this).val(0);
            }
            calcTotal();
        });
    });
}

function calcTotal() {
    var tong_qtt = 0;
    var total = 0;
    var arrQtt = $(".qtt");
    for (var i = 0; i < arrQtt.length; i++) {
        var iQtt = parseInt(arrQtt.eq(i).val());
        tong_qtt += iQtt;
    }
    $('#qttAll').val(tong_qtt);
    var arrTotal = $('.subtotal');
    for (var i = 0; i < arrTotal.length; i++) {
        var iTotal = parseFloat(arrTotal.eq(i).val().replace(new RegExp(',', 'g'), ''));
        total += iTotal;
    }
    $('#total').val(total);
}