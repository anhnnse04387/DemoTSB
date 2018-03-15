$(document).ready(function () {
    $('#customer').change(function () {
        var id = $('#customer').val();
        $.ajax({
            url: "/LapPhieu/ChangeCustomer",
            data: {customerId: id},
            datatype: "json",
            success: function (data) {
                document.getElementById('deliveryAddress').value = data.deliveryAddress;
                document.getElementById('taxCode').value = data.taxCode;
                document.getElementById('invoiceAddress').value = data.invoiceAddress;
            }
        });
    });
    $('#addLstItem').on('click', function () {
        var rowCount = $('.addingRow').length + 1;
        var div = '<tr class="addingRow">'
                + '<td>' + rowCount + '</td>'
                + '<td style="vertical-align: middle; width: 24.7%;"><input type="text" required class="form-control code" />'
                + '<input type="hidden" class="productId"/><input type="hidden" class="qtt"/>'
                + '<input type="hidden" class="qttInven" /><input type="hidden" class="qttBox" /></td>'
                + '<td style="vertical-align: middle;"><input type="text" required class="form-control param" /></td>'
                + '<td><input type="text" class="form-control cai" style="text-align: right;" required/></td>'
                + '<td><input type="text" class="form-control thung" style="text-align: right;" required /></td>'
                + '<td><output class="dongia number"></output></td>'
                + '<td><output class="tienchuack number"></output></td>'
                + '<td><input type="text" class="form-control ck number"/></td>'
                + '<td><output class="tiendack number"></output></td>'
                + '</tr>';
        $('.addingRow').last().after(div);
        init();
    });
    init();
});

function validate() {
    var count = 0;
    if ($('#customer').val() === "") {
        count++;
    }
    if ($('#orderId').val() === "") {
        count++;
    }
    if ($('#deliveryAddress').val() === "") {
        count++;
    }
    if ($('#invoiceAddress').val() === "") {
        count++;
    }
    if ($('#taxCode').val() === "") {
        count++;
    }
    var arr = $(".code");
    for (var i = 0; i < arr.length; i++) {
        if (arr.eq(i).val() === "") {
            count++;
        }
    }
    arr = $(".param");
    for (var i = 0; i < arr.length; i++) {
        if (arr.eq(i).val() === "") {
            count++;
        }
    }
    arr = $(".cai");
    for (var i = 0; i < arr.length; i++) {
        if (arr.eq(i).val() === "") {
            count++;
        }
    }
    arr = $(".thung");
    for (var i = 0; i < arr.length; i++) {
        if (arr.eq(i).val() === "") {
            count++;
        }
    }
    return count;
}

function init() {
    $('.number').autoNumeric('init', {minimumValue: '1', maximumValue: '9999999999999', digitGroupSeparator: ',', decimalPlacesOverride: '0'});
    productATC();
    configCai();
    configThung();
    configCk();
    configCkAll();
    $("#btnSubmit").click(function () {
        if (validate() === 0) {
            $('#confirmComplete').modal();
            return false;
        }
    });
    $("#btnSave").click(function () {
        if (validate() === 0) {
            $('#confirmSave').modal();
            return false;
        }
    });
}

function addOrder() {
    var data = JSON.stringify(getAllData());
    $.ajax({
        url: '/LapPhieu/Index',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({model: data}),
        success: function () {
            swal({
                title: '<img src="dist/img/messagePic_1.png"/>',
                imageUrl: 'dist/img/Noti.gif',
                imageWidth: 400,
                imageHeight: 300,
                imageAlt: 'Custom image',
                animation: false
            });
            document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
        }
    });
}

function saveOrder() {
    var data = JSON.stringify(getAllData());
    $.ajax({
        url: '/LapPhieu/Index',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({model: data}),
        success: function () {
            swal({
                title: '<img src="dist/img/messagePic_2.png"/>',
                type: 'success'
            });
            document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
        }
    });
}

function productATC() {
    $('.code').each(function () {
        var ctr = $(this);
        var thisRow = $(this).parents("tr");
        $(ctr).autocomplete({
            source: function (req, responseFn) {
                var re = req.term;
                if (re.length > 2) {
                    $.ajax({
                        url: '/LapPhieu/ChooseProduct',
                        data: {
                            input: re
                        },
                        dataType: "json",
                        success: function (data) {
                            var array = data.error ? [] : $.map(data, function (m) {
                                return {
                                    label: m.Product_code,
                                    value: m.Product_ID,
                                    param: m.Product_parameters,
                                    qttInven: m.Quantities_in_inventory,
                                    qttBox: m.Quantity_in_carton,
                                    price: parseFloat(m.Price_before_VAT_VND) * (100 + parseFloat(m.VAT_VND))
                                };
                            });
                            responseFn(array);
                        }
                    });
                }
            },
            select: function (event, ui) {
                event.preventDefault();
                thisRow.find(".code").val(ui.item.label);
                thisRow.find(".productId").val(ui.item.value);
                thisRow.find(".param").val(ui.item.param);
                thisRow.find(".qttInven").val(ui.item.qttInven);
                thisRow.find(".qttBox").val(ui.item.qttBox);
                thisRow.find(".dongia").autoNumeric('set', ui.item.price);
            }, change: function (event, ui) {
                if (ui.item) {
                    event.preventDefault();
                    thisRow.find(".code").val(ui.item.label);
                    thisRow.find(".productId").val(ui.item.value);
                    thisRow.find(".param").val(ui.item.param);
                    thisRow.find(".qttInven").val(ui.item.qttInven);
                    thisRow.find(".qttBox").val(ui.item.qttBox);
                    thisRow.find(".dongia").autoNumeric('set', ui.item.price);
                } else {
                    thisRow.find(".code").val("");
                }
            }, search: function (event, ui) {
            }, focus: function (event, ui) {
                thisRow.find(".code").val(ui.item.label);
                event.preventDefault();
            }
        }).autocomplete("widget").addClass("fixed-height");
    });
}

function getAllData() {
    var items = {};
    var customerId = $('#customer').val();
    var orderId = $('#orderId').val();
    var deliveryAddress = $('#deliveryAddress').val();
    var invoiceAddress = $('#invoiceAddress').val();
    var rate = $('#rate').val();
    var taxCode = $('#taxCode').val();
    var deliveryQtt = $('#deliveryQtt').val();
    $('tr.addingRow').each(function () {
        var tiendack = $(this).find('.tiendack').val();
        var productId = $(this).find('.productId').val();
        var cai = $(this).find('.cai').val();
        var ck = $(this).find('.ck').val();
        var thung = $(this).find('.thung').val();
        items = {
            Price: tiendack,
            Quantity: cai,
            Product_ID: productId,
            Box: thung,
            Discount: ck
        };
    });
    var data = {orderId: orderId, customerId: customerId,
        deliveryAddress: deliveryAddress, deliveryQtt: deliveryQtt,
        invoiceAddress: invoiceAddress, rate: rate, taxCode: taxCode,
        items: items
    };
    return data;
}

function configCai() {
    $('.cai').each(function () {
        $(this).keyup(function () {
            var cai = $(this).val();
            var thisRow = $(this).parents("tr");
            var conlai = parseInt(thisRow.find(".qttInven").val());
            var per = parseInt(thisRow.find(".qttBox").val());
            if (parseInt(cai) > 0) {
                if (parseInt(cai) > conlai) {
                    checkQtt();
                } else {
                    thisRow.find(".thung").val(parseInt(parseInt(cai) / per));
                    thisRow.find(".tienchuack").autoNumeric('set', cai * parseFloat(thisRow.find(".dongia").val().replace(new RegExp(',', 'g'), '')));
                    ck(thisRow);
                }
            } else {
                $(this).val(0);
            }
            calcTotal();
        });
    });
}

function configThung() {
    $('.thung').each(function () {
        $(this).keyup(function () {
            var thung = $(this).val();
            var thisRow = $(this).parents("tr");
            var conlai = parseInt(thisRow.find(".qttInven").val());
            var per = parseInt(thisRow.find(".qttBox").val());
            if (parseInt(thung) * per > 0) {
                if (parseInt(thung) * per > conlai) {
                    checkQtt();
                } else {
                    thisRow.find(".cai").val(parseInt(thung) * per);
                    thisRow.find(".tienchuack").autoNumeric('set', parseInt(thung) * per * parseFloat(thisRow.find(".dongia").val().replace(new RegExp(',', 'g'), '')));
                    ck(thisRow);
                }
            } else {
                $(this).val(0);
            }
            calcTotal();
        });
    });
}

function checkQtt() {
    document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
    swal({
        title: '<img src="dist/img/messagePic_4.png"/>',
        type: 'error',
        html: '<div style="margin-left: 377px;"><i class="fa fa-eye text-black"></i><a onclick="timeline();"><img src="dist/img/xem.png"/></a></div>'
                + '<table class="table table-striped mainTable" style="margin-top: 10px;">'
                + '<thead>'
                + '<tr><th style="background-color: white"><img src="dist/img/loso.png"/></th><th style="background-color: white"><img src="dist/img/soluong.png"/></th><th style="background-color: white"><img src="dist/img/ngay.png"/></th><th style="background-color: white"><img src="dist/img/soluonglay.png"/></th></tr>'
                + '</thead>'
                + '<tbody>'
                + '<tr><td>O1345</td><td style="text-align: right;">7</td><td>01/01/2018</td><td><input type="text" class="form-control" style="text-align: right; width: 50px; float: right;" id="sl1"/></td></tr>'
                + '<tr><td>O1348</td><td style="text-align: right;">12</td><td>03/01/2018</td><td><input type="text" class="form-control" style="text-align: right; width: 50px; float: right;" id="sl2"/></td></tr>'
                + '</tbody>'
                + '</table>',
        showCancelButton: true,
        width: 550,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '<i class="fa fa-check"></i>',
        cancelButtonText: '<i class="fa fa-close"></i>'
    });
}

function calcTotal() {
    var tong_thung = 0;
    var tong_cai = 0;
    var arrCai = $(".cai");
    for (var i = 0; i < arrCai.length; i++) {
        var iCai = parseInt(arrCai.eq(i).val());
        if (iCai > 0) {
            tong_cai += iCai;
        }
    }
    var arrThung = $(".thung");
    for (var i = 0; i < arrThung.length; i++) {
        var iThung = parseInt(arrThung.eq(i).val());
        if (iThung > 0) {
            tong_thung += iThung;
        }
    }
    $('#tongcai').val(tong_cai);
    $('#tongthung').val(tong_thung);
}

function configCk() {
    $('.ck').each(function () {
        $(this).keyup(function () {
            var thisRow = $(this).parents("tr");
            ck(thisRow);
        });
    });
}

function ck(thisRow) {
    var ck = parseFloat(thisRow.find(".ck").val());
    var cai = parseInt(thisRow.find(".cai").val());
    var dongia = parseFloat(thisRow.find(".dongia").val().replace(new RegExp(',', 'g'), ''));
    if (cai > 0) {
        if (ck > 0) {
            thisRow.find(".tiendack").autoNumeric('set', cai * dongia * (100 - ck) / 100);
        } else {
            thisRow.find(".tiendack").val(thisRow.find(".tienchuack").val());
        }
        configCkAll();
    }
}

function configCkAll() {
    var tongck = parseFloat($('#tongck').val());
    var tienck = 0;
    var vat = parseFloat($('#vat').val());
    var tienvat = 0;
    var tongtienchuack = 0;
    var tongtiendack = 0;
    var arrTienDaCk = $(".tiendack");
    for (var i = 0; i < arrTienDaCk.length; i++) {
        tongtienchuack += parseFloat(arrTienDaCk.eq(i).val().replace(new RegExp(',', 'g'), ''));
    }
    if (tongtienchuack > 0) {
        tongtiendack = tongtienchuack;
        $('#tongtienchuack').autoNumeric('set', tongtienchuack);
        if (tongck > 0) {
            tienck = tongtienchuack * tongck / 100;
            tongtiendack -= tienck;
            $('#tienck').autoNumeric('set', tienck);
        } else {
            $('#tienck').val(0);
        }
        $('#conlai').autoNumeric('set', tongtiendack);
        if (vat > 0) {
            tienvat = tongtiendack * vat / 100;
            tongtiendack += tienvat;
            $('#tienvat').autoNumeric('set', tienvat);
        } else {
            $('#tienvat').val(0);
        }
        $('#tongtiendack').autoNumeric('set', tongtiendack);
    }
}