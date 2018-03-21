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
    $("#btnSubmit").click(function () {
        if (validate() === 0) {
            previewOrder();
        }
        return false;
    });
    $("#btnSave").click(function () {
        if (validate() === 0) {
            $('#confirmSave').modal();
        }
        return false;
    });
    initMain();
});

function initMain() {
    $('.number').autoNumeric('init', {minimumValue: '1', maximumValue: '9999999999999', digitGroupSeparator: ',', decimalPlacesOverride: '0'});
    configCai();
    configThung();
    configCk();
    vat();
    productATC();
    productSubATC();
    configCkAll();
}

function changeQtt() {
    var i;
    var qtt = parseInt($('#deliveryQtt').val());
    var div = '';
    var inside = document.getElementById('template').innerHTML;
    document.getElementById('deliveryDiv').innerHTML = '';
    if (qtt > 0) {
        for (i = 0; i < qtt; i++) {
            var fieldset = '<fieldset class="deliveryFS">';
            fieldset += inside.replace('--loso--', ($('#orderId').val() + '-' + (i + 1)))
                    .replace('--giaohangdot--', (i + 1)).replace('--req--', 'required');
            fieldset += '</fieldset>';
            div += fieldset;
        }
    }
    document.getElementById('deliveryDiv').innerHTML = div;
    initMain();
    $('.datepicker').datepicker();
}

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
    arr = $(".subCode");
    for (var i = 0; i < arr.length; i++) {
        if (arr.eq(i).closest("fieldset").attr("class") !== 'noDisplay') {
            if (arr.eq(i).val() === "") {
                count++;
            }
        }
    }
    arr = $(".param");
    for (var i = 0; i < arr.length; i++) {
        if (arr.eq(i).closest("fieldset").attr("class") !== 'noDisplay') {
            if (arr.eq(i).val() === "") {
                count++;
            }
        }
    }
    arr = $(".cai");
    for (var i = 0; i < arr.length; i++) {
        if (arr.eq(i).closest("fieldset").attr("class") !== 'noDisplay') {
            if (arr.eq(i).val() === "") {
                count++;
            }
        }
    }
    arr = $(".thung");
    for (var i = 0; i < arr.length; i++) {
        if (arr.eq(i).closest("fieldset").attr("class") !== 'noDisplay') {
            if (arr.eq(i).val() === "") {
                count++;
            }
        }
    }
    return count;
}

function addLstItem(e) {
    var table = $(e).closest("table");
    var rowCount = table.find('.addingRow').length + 1;
    var div = '<tr class="addingRow">'
            + '<td>' + rowCount + '</td>'
            + '<td style="vertical-align: middle; width: 24.7%;"><input type="text" required class="form-control code" />'
            + '<input type="hidden" class="productId"/>'
            + '<input type="hidden" class="qttInven" /><input type="hidden" class="qttBox" /></td>'
            + '<td style="vertical-align: middle;"><input type="text" required class="form-control param" /></td>'
            + '<td><input type="text" class="form-control cai" style="text-align: right;" required/></td>'
            + '<td><input type="text" class="form-control thung" style="text-align: right;" required /></td>'
            + '<td><output class="dongia number"></output></td>'
            + '<td><output class="tienchuack number"></output></td>'
            + '<td><input type="text" class="form-control ck number"/></td>'
            + '<td><output class="tiendack number"></output></td>'
            + '</tr>';
    table.find('.addingRow').last().after(div);
    initMain();
}

function addLstSubItem(e) {
    var table = $(e).closest("table");
    var rowCount = table.find('.addingSubRow').length + 1;
    var div = '<tr class="addingSubRow">'
            + '<td>' + rowCount + '</td>'
            + '<td style="vertical-align: middle; width: 24.7%;"><input type="text" required class="form-control subCode" />'
            + '<input type="hidden" class="productId"/>'
            + '<input type="hidden" class="qttInven" /><input type="hidden" class="qttBox" /></td>'
            + '<td style="vertical-align: middle;"><input type="text" required class="form-control param" /></td>'
            + '<td><input type="text" class="form-control cai" style="text-align: right;" required/></td>'
            + '<td><input type="text" class="form-control thung" style="text-align: right;" required /></td>'
            + '<td><output class="dongia number"></output></td>'
            + '<td><output class="tienchuack number"></output></td>'
            + '<td><input type="text" class="form-control ck number"/></td>'
            + '<td><output class="tiendack number"></output></td>'
            + '</tr>';
    table.find('.addingSubRow').last().after(div);
    initMain();
}

function saveOrder() {
    $.ajax({
        url: '/LapPhieu/SaveOrder',
        type: 'POST',
        dataType: 'json',
        data: getAllData(),
        success: function () {
            swal({
                title: '<img src="/Assets/dist/img/messagePic_2.png"/>',
                type: 'success'
            });
            document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="/Assets/dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
        },
        error: function () {
            swal({
                title: '<img src="/Assets/dist/img/messagePic_7.png"/><img src="/Assets/dist/img/messagePic_8.png"/>',
                type: 'error',
                showCancelButton: false,
                showConfirmButton: true
            });
        }
    });
}

function previewOrder() {
    var data = JSON.stringify(getAllData());
    $.ajax({
        url: '/Preview/Index',
        type: 'GET',
        dataType: 'json',
        data: JSON.stringify({model: data})
    });
}

function doneOrder() {
    var data = JSON.stringify(getAllData());
    $.ajax({
        url: '/Preview/CheckOut',
        type: 'POST',
        dataType: 'json',
        data: JSON.stringify({model: data}),
        success: function () {
            swal({
                title: '<img src="/Assets/dist/img/messagePic_1.png"/>',
                imageUrl: '/Assets/dist/img/Noti.gif',
                imageWidth: 400,
                imageHeight: 300,
                imageAlt: 'Custom image',
                animation: false
            });
            document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="/Assets/dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
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

function productATC() {
    $('.code').each(function () {
        var thisRow = $(this).parents("tr");
        $(this).autocomplete({
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
                                    price: parseFloat(m.Price_before_VAT_VND) * (100 + parseFloat(m.VAT)) / 100
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
            }, focus: function (event, ui) {
                thisRow.find(".code").val(ui.item.label);
                event.preventDefault();
            }
        }).autocomplete("widget").addClass("fixed-height");
    });
}

function productSubATC() {
    $('.subCode').each(function () {
        var thisRow = $(this).parents("tr");
        $(this).autocomplete({
            source: function (req, responseFn) {
                var re = req.term;
                var data = [];
                $('.addingRow').each(function () {
                    var subCode = $('.addingRow').find('.code').val();
                    var productId = $('.addingRow').find('.productId').val();
                    var param = $('.addingRow').find('.param').val();
                    var dongia = $('.addingRow').find('.dongia').val();
                    var qttBox = $('.addingRow').find('.qttBox').val();
                    var qttInven = $('.addingRow').find('.cai').val();
                    if (subCode.length > 2) {
                        var items = {
                            label: subCode,
                            value: productId,
                            param: param,
                            price: dongia,
                            qttBox: qttBox,
                            qttInven: qttInven
                        };
                        data.push(items);
                    }
                });
                if (re.length > 2) {
                    responseFn($.map(data, function (m) {
                        return {
                            label: m.label,
                            value: m.value,
                            param: m.param,
                            qttInven: m.qttInven,
                            qttBox: m.qttBox,
                            price: m.price
                        };
                    }));
                }
            },
            select: function (event, ui) {
                event.preventDefault();
                thisRow.find(".subCode").val(ui.item.label);
                thisRow.find(".productId").val(ui.item.value);
                thisRow.find(".param").val(ui.item.param);
                thisRow.find(".qttInven").val(ui.item.qttInven);
                thisRow.find(".qttBox").val(ui.item.qttBox);
                thisRow.find(".dongia").val(ui.item.price);
            }, change: function (event, ui) {
                if (ui.item) {
                    event.preventDefault();
                    thisRow.find(".subCode").val(ui.item.label);
                    thisRow.find(".productId").val(ui.item.value);
                    thisRow.find(".param").val(ui.item.param);
                    thisRow.find(".qttInven").val(ui.item.qttInven);
                    thisRow.find(".qttBox").val(ui.item.qttBox);
                    thisRow.find(".dongia").val(ui.item.price);
                } else {
                    thisRow.find(".subCode").val("");
                }
            }, focus: function (event, ui) {
                thisRow.find(".subCode").val(ui.item.label);
                event.preventDefault();
            }
        }).autocomplete("widget").addClass("fixed-height");
    });
}

function getAllData() {
    var items = [];
    var arr = $('.deliveryFS');
    var parts = [];
    var customerId = $('#customer').val();
    var orderId = $('#orderId').val();
    var deliveryAddress = $('#deliveryAddress').val();
    var invoiceAddress = $('#invoiceAddress').val();
    var rate = parseInt($('#rate').val());
    var taxCode = $('#taxCode').val();
    var deliveryQtt = parseInt($('#deliveryQtt').val());
    var subTotal = parseFloat($('.tongtienchuack').eq(0).val().replace(new RegExp(',', 'g'), ''));
    var vat = parseFloat($('.vat').eq(0).val());
    var total = parseFloat($('.tongtiendack').eq(0).val().replace(new RegExp(',', 'g'), ''));
    var discount = parseFloat($('.tongck').eq(0).val());
    $('.addingRow').each(function () {
        var tiendack = parseFloat($(this).find('.tiendack').val().replace(new RegExp(',', 'g'), ''));
        var productId = $(this).find('.productId').val();
        var cai = $(this).find('.cai').val();
        var ck = $(this).find('.ck').val();
        var thung = $(this).find('.thung').val();
        var item = {
            Price: tiendack,
            Quantity: cai,
            Product_ID: productId,
            Box: thung,
            Discount: ck
        };
        items.push(item);
    });
    if (deliveryQtt > 0) {
        for (var i = 0; i < arr.length; i++) {
            var part = [];
            var partsItem = [];
            var orderPartId = arr.eq(i).find('.orderPartId').val();
            var invoiceDate = arr.eq(i).find('.invoiceDate').val();
            var table = arr.eq(i).find('.mainTable');
            var partVat = parseFloat(table.find('.vat').val());
            var partTotal = parseFloat(table.find('.tongtiendack').val().replace(new RegExp(',', 'g'), ''));            
            table.find('.addingSubRow').each(function () {
                var tiendack = parseFloat($(this).find('.tiendack').val().replace(new RegExp(',', 'g'), ''));
                var productId = $(this).find('.productId').val();
                var cai = $(this).find('.cai').val();
                var ck = $(this).find('.ck').val();
                var thung = $(this).find('.thung').val();
                var partItem = {
                    Order_ID: orderId,
                    Order_part_ID: orderPartId,
                    Price: tiendack,
                    Quantity: cai,
                    Product_ID: productId,
                    Box: thung,
                    Discount: ck
                };
                partsItem.push(partItem);
            });
            part = {Part_ID: i + 1, Order_part_ID: orderPartId, VAT: partVat,
                Total_price: partTotal, Date_reveice_invoice: invoiceDate,
                Order_items: partsItem};
            parts.push(part);
        }
    }
    var data = {orderId: orderId, customerId: customerId,
        deliveryAddress: deliveryAddress, deliveryQtt: deliveryQtt,
        invoiceAddress: invoiceAddress, rate: rate, taxCode: taxCode,
        subTotal: subTotal, vat: vat, total: total, discount: discount,
        items: items, part: parts
    };
    return data;
}

function configCai() {
    $('.cai').each(function () {
        $(this).keyup(function () {
            var cai = $(this).val();
            var thisTable = $(this).closest("table");
            var thisRow = $(this).parents("tr");
            var conlai = parseInt(thisRow.find(".qttInven").val());
            var per = parseInt(thisRow.find(".qttBox").val());
            if (parseInt(cai) > 0) {
                if (parseInt(cai) > conlai) {
                    checkQtt(thisRow);
                } else {
                    thisRow.find(".thung").val(parseInt(parseInt(cai) / per));
                    thisRow.find(".tienchuack").autoNumeric('set', cai * parseFloat(thisRow.find(".dongia").val().replace(new RegExp(',', 'g'), '')));
                    ck(thisRow);
                }
            } else {
                $(this).val(0);
            }
            calcTotal(thisTable);
        });
    });
}

function configThung() {
    $('.thung').each(function () {
        $(this).keyup(function () {
            var thung = $(this).val();
            var thisTable = $(this).closest("table");
            var thisRow = $(this).parents("tr");
            var conlai = parseInt(thisRow.find(".qttInven").val());
            var per = parseInt(thisRow.find(".qttBox").val());
            if (parseInt(thung) * per > 0) {
                if (parseInt(thung) * per > conlai) {
                    checkQtt(thisRow);
                } else {
                    thisRow.find(".cai").val(parseInt(thung) * per);
                    thisRow.find(".tienchuack").autoNumeric('set', parseInt(thung) * per * parseFloat(thisRow.find(".dongia").val().replace(new RegExp(',', 'g'), '')));
                    ck(thisRow);
                }
            } else {
                $(this).val(0);
            }
            calcTotal(thisTable);
        });
    });
}

function checkQtt(thisRow) {
    document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="/Assets/dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
    if (thisRow.attr('class') === 'addingRow') {
        swal({
            title: '<img src="/Assets/dist/img/messagePic_4.png"/>',
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
    } else {
        swal({
            title: '<img src="/Assets/dist/img/messagePic_3.png"/>',
            type: 'error',
            showCancelButton: false,
            showConfirmButton: false,
            timer: 2000
        });
    }
}

function calcTotal(thisTable) {
    var tong_thung = 0;
    var tong_cai = 0;
    var arrCai = thisTable.find(".cai");
    for (var i = 0; i < arrCai.length; i++) {
        var iCai = parseInt(arrCai.eq(i).val());
        if (iCai > 0) {
            tong_cai += iCai;
        }
    }
    var arrThung = thisTable.find(".thung");
    for (var i = 0; i < arrThung.length; i++) {
        var iThung = parseInt(arrThung.eq(i).val());
        if (iThung > 0) {
            tong_thung += iThung;
        }
    }
    thisTable.find('.tongcai').val(tong_cai);
    thisTable.find('.tongthung').val(tong_thung);
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
    var thisTable = thisRow.closest("table");
    var dongia = parseFloat(thisRow.find(".dongia").val().replace(new RegExp(',', 'g'), ''));
    if (cai > 0) {
        if (ck > 0) {
            thisRow.find(".tiendack").autoNumeric('set', cai * dongia * (100 - ck) / 100);
        } else {
            thisRow.find(".tiendack").val(thisRow.find(".tienchuack").val());
        }
        total(thisTable);
    }
}

function vat() {
    $('.vat').each(function () {
        $(this).keyup(function () {
            var thisTable = $(this).closest("table");
            total(thisTable);
        });
    });
}

function total(thisTable) {
    var tongck = parseFloat(thisTable.find('.tongck').val());
    var tienck = 0;
    var vat = parseFloat(thisTable.find('.vat').val());
    var tienvat = 0;
    var tongtienchuack = 0;
    var tongtiendack = 0;
    var arrTienDaCk = thisTable.find('.tiendack');
    for (var i = 0; i < arrTienDaCk.length; i++) {
        tongtienchuack += parseFloat(arrTienDaCk.eq(i).val().replace(new RegExp(',', 'g'), ''));
    }
    if (tongtienchuack > 0) {
        tongtiendack = tongtienchuack;
        thisTable.find('.tongtienchuack').autoNumeric('set', tongtienchuack);
        if (tongck > 0) {
            tienck = tongtienchuack * tongck / 100;
            tongtiendack -= tienck;
            thisTable.find('.tienck').autoNumeric('set', tienck);
        } else {
            thisTable.find('.tienck').val(0);
        }
        thisTable.find('.conlai').autoNumeric('set', tongtiendack);
        if (vat > 0) {
            tienvat = tongtiendack * vat / 100;
            tongtiendack += tienvat;
            thisTable.find('.tienvat').autoNumeric('set', tienvat);
        } else {
            thisTable.find('.tienvat').val(0);
        }
        thisTable.find('.tongtiendack').autoNumeric('set', tongtiendack);
    }
}

function configCkAll() {
    $('.tongck').each(function () {
        $(this).keyup(function () {
            var thisTable = $(this).closest("table");
            total(thisTable);
        });
    });
}