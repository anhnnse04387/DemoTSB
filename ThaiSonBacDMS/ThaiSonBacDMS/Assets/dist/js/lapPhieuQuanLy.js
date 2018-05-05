﻿$(document).ready(function () {
    $('.datepicker').datepicker();
    $('#customer').change(function () {
        var id = parseInt($('#customer').val());
        $.ajax({
            url: "/QuanLy/LapPhieu/ChangeCustomer",
            data: { customerId: id },
            datatype: "json",
            success: function (data) {
                document.getElementById('deliveryAddress').value = data.deliveryAddress;
                document.getElementById('taxCode').value = data.taxCode;
                document.getElementById('invoiceAddress').value = data.invoiceAddress;
            }
        });
    });
    $("#btnPreview").click(function (event) {
        if (validate() === 0) {
            event.preventDefault();
            $('#preview').removeClass('noDisplay');
            $('#normal').addClass('noDisplay');
            $('form :input').not($('#btnSubmit')).prop('disabled', true);
        } else {
            $('form')[0].checkValidity();
        }
    });
    $('#btnCancelPreview').click(function () {
        $('#preview').addClass('noDisplay');
        $('#normal').removeClass('noDisplay');
        $('form :input').prop('disabled', false);
    });
    $("#btnSave").click(function (event) {
        if (validate() === 0) {
            event.preventDefault();
            $('#confirmSave').modal();
        } else {
            $('form')[0].checkValidity();
        }
    });
    $("#btnSubmit").click(function (event) {
        if (validate() === 0) {
            event.preventDefault();
            $('#confirmComplete').modal();
        } else {
            $('form')[0].checkValidity();
        }
    });
    $("#btnDone").click(function (event) {
        if (validate() === 0) {
            event.preventDefault();
            $('#confirmComplete').modal();
        } else {
            $('form')[0].checkValidity();
        }
    });
    initMain();
});

function donePendingOrder() {
    $.ajax({
        url: '/QuanLy/LapPhieu/CheckOut',
        type: 'POST',
        dataType: 'json',
        data: getAllData(),
        success: function () {
            document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="/Assets/dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
            swal({
                title: '<img src="/Assets/dist/img/messagePic_1.png"/>',
                imageUrl: '/Assets/dist/img/Noti.gif',
                imageWidth: 400,
                imageHeight: 300,
                imageAlt: 'Custom image',
                animation: false
            }).then((result) => {
                if (result.value) {
                    window.location.href = '/QuanLy/OrderList/Store';
                }
            });
        },
        error: function () {
            swal({
                title: '<img src="/Assets/dist/img/messagePic_9.png"/><img src="/Assets/dist/img/messagePic_8.png"/>',
                type: 'error',
                showCancelButton: false,
                showConfirmButton: true
            });
        }
    });
}

function deleteRow(comp) {
    var table = $(comp).closest("table");
    comp.closest("table").deleteRow(comp.closest("tr").rowIndex);
    calcTotal(table);
    total(table);
}

function initMain() {
    $('.number').autoNumeric('init', { minimumValue: '1', maximumValue: '9999999999999', digitGroupSeparator: ',', decimalPlacesOverride: '0' });
    configCai();
    configThung();
    configCk();
    productATC();
    productSubATC();
    configCkAll();
    var arr = $('.addingRow');
    var arrSub = $('.addingSubRow');
    for (var i = 0; i < arr.length ; i++) {
        for (var j = 0; j < arrSub.length ; j++) {
            if (arr.eq(i).find('.productId').val() === arrSub.eq(j).find('.productId').val()) {
                arrSub.eq(j).find('.qttInven').val(arr.eq(i).find('.cai').val());
            }
        }
    }
}

function changeQtt() {
    var i;
    var qtt = parseInt($('#deliveryQtt').val());
    var div = '';
    var inside = document.getElementById('template').innerHTML;
    document.getElementById('deliveryDiv').innerHTML = '';
    if (qtt > 1) {
        for (i = 0; i < qtt; i++) {
            var fieldset = '<fieldset class="deliveryFS">';
            fieldset += inside.replace('--loso--', ($('#orderId').val() + '-' + (i + 1)))
                    .replace('--giaohangdot--', (i + 1)).replace('--req--', 'required');
            fieldset += '</fieldset>';
            div += fieldset;
        }
        $('#exportDiv').addClass('noDisplay');
        $('#exportDate').removeAttr('required');
    } else {
        $('#exportDiv').removeClass('noDisplay');
        $('#exportDate').prop("required", true);
    }
    document.getElementById('deliveryDiv').innerHTML = div;
    initMain();
    $('.datepicker').datepicker();
}

function validate() {
    var count = 0;
    var deliveryQtt = parseInt($('#deliveryQtt').val());
    if (deliveryQtt > 1) {
        var date = $(".invoiceDate");
        for (var i = 0; i < date.length; i++) {
            if (date.eq(i).closest("fieldset").attr("class") !== 'noDisplay') {
                if (date.eq(i).val() === "") {
                    count++;
                }
            }
        }
    } else {
        if ($('#exportDate').val() === "") {
            count++;
        }
    }
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
            + '<td style="vertical-align: middle; width: 20%;"><input type="text" required class="form-control code" />'
            + '<input type="hidden" class="productId"/>'
            + '<input type="hidden" class="qttInven" /><input type="hidden" class="qttBox" /></td>'
            + '<td style="vertical-align: middle;"><input type="text" required class="form-control param" /></td>'
            + '<td><input type="text" class="form-control cai" style="text-align: right;" required/></td>'
            + '<td><input type="text" class="form-control thung" style="text-align: right;" required /></td>'
            + '<td><output class="dongia number"></output><input type="hidden" class="priceBeforeVAT" /></td>'
            + '<td><output class="tienchuack number"></output></td>'
            + '<td><input type="text" class="form-control ck number"/></td>'
            + '<td><output class="tiendack number"></output></td>'
            + '<td style="vertical-align: middle;">'
            + '<a data-toggle="tooltip" title="Xóa" data-placement="bottom" onclick="deleteRow(this)" href="javascript:void(0)">'
            + '<i class="fa fa-close text-red" style="color: #3c8dbc; font-size: 20px"></i>'
            + '</a></td>'
            + '</tr>';
    table.find('.addingRow').last().after(div);
    initMain();
}

function addLstSubItem(e) {
    var table = $(e).closest("table");
    var rowCount = table.find('.addingSubRow').length + 1;
    var div = '<tr class="addingSubRow">'
            + '<td>' + rowCount + '</td>'
            + '<td style="vertical-align: middle; width: 20%;"><input type="text" required class="form-control subCode" />'
            + '<input type="hidden" class="productId"/>'
            + '<input type="hidden" class="qttInven" /><input type="hidden" class="qttBox" /></td>'
            + '<td style="vertical-align: middle;"><input type="text" required class="form-control param" /></td>'
            + '<td><input type="text" class="form-control cai" style="text-align: right;" required/></td>'
            + '<td><input type="text" class="form-control thung" style="text-align: right;" required /></td>'
            + '<td><output class="dongia number"></output><input type="hidden" class="priceBeforeVAT" /></td>'
            + '<td><output class="tienchuack number"></output></td>'
            + '<td><input type="text" class="form-control ck number"/></td>'
            + '<td><output class="tiendack number"></output></td>'
            + '<td style="vertical-align: middle;">'
            + '<a data-toggle="tooltip" title="Xóa" data-placement="bottom" onclick="deleteRow(this)" href="javascript:void(0)">'
            + '<i class="fa fa-close text-red" style="color: #3c8dbc; font-size: 20px"></i>'
            + '</a></td>'
            + '</tr>';
    table.find('.addingSubRow').last().after(div);
    initMain();
}

function saveOrder() {
    $.ajax({
        url: '/QuanLy/LapPhieu/SaveOrder',
        type: 'POST',
        dataType: 'json',
        data: getAllData(),
        success: function () {
            document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="/Assets/dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
            swal({
                title: '<img src="/Assets/dist/img/messagePic_2.png"/>',
                type: 'success'
            }).then((result) => {
                if (result.value) {
                    window.location.href = '/QuanLy/OrderList/Index';
                }
            });
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

function cancelOrder() {
    $.ajax({
        url: '/QuanLy/LapPhieu/CancelOrder',
        dataType: 'json',
        data: { orderId: $('#orderId').val(), note: $('#reason').val() },
        success: function () {
            document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="/Assets/dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
            swal({
                title: '<img src="/Assets/dist/img/messagePic_10.png"/>',
                type: 'success'
            }).then((result) => {
                if (result.value) {
                    window.location.href = '/QuanLy/OrderList/Cancel';
                }
            });
        },
        error: function () {
            swal({
                title: '<img src="/Assets/dist/img/messagePic_11.png"/>',
                type: 'error'
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
                        url: '/QuanLy/LapPhieu/ChooseProduct',
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
                                    price: parseFloat(m.Price_before_VAT_VND) * (100 + parseFloat(m.VAT)) / 100,
                                    priceBeforeVAT: parseFloat(m.Price_before_VAT_VND) * parseFloat(m.VAT) / 100
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
                thisRow.find(".param").val(ui.item.param);
                thisRow.find(".qttInven").val(ui.item.qttInven);
                thisRow.find(".qttBox").val(ui.item.qttBox);
                thisRow.find(".dongia").autoNumeric('set', ui.item.price);
                thisRow.find(".priceBeforeVAT").val(ui.item.priceBeforeVAT);
                calcVAT(thisRow);
            }, change: function (event, ui) {
                if (ui.item) {
                    event.preventDefault();
                    thisRow.find(".code").val(ui.item.label);
                    thisRow.find(".productId").val(ui.item.value);
                    thisRow.find(".param").val(ui.item.param);
                    thisRow.find(".qttInven").val(ui.item.qttInven);
                    thisRow.find(".qttBox").val(ui.item.qttBox);
                    thisRow.find(".dongia").autoNumeric('set', ui.item.price);
                    thisRow.find(".priceBeforeVAT").val(ui.item.priceBeforeVAT);
                    calcVAT(thisRow);
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

function calcVAT(thisRow) {
    var arr = thisRow.closest("table").find(".priceBeforeVAT");
    var tienVat = 0;
    for (var i = 0; i < arr.length; i++) {
        var per = parseInt(arr.eq(i).val().replace(new RegExp(',', 'g'), ''));
        if (per > 0) {
            tienVat += per;
        }
    }
    thisRow.closest("table").find(".tienvat").autoNumeric('set', tienVat);
}

function productSubATC() {
    $('.subCode').each(function () {
        var thisRow = $(this).parents("tr");
        $(this).autocomplete({
            source: function (req, responseFn) {
                var re = req.term;
                var data = [];
                var arr = $('.addingRow');
                for (var i = 0; i < arr.length; i++) {
                    var subCode = arr.eq(i).find('.code').val();
                    var productId = arr.eq(i).find('.productId').val();
                    var param = arr.eq(i).find('.param').val();
                    var priceBeforeVAT = arr.eq(i).find(".priceBeforeVAT").val();
                    var dongia = arr.eq(i).find('.dongia').val();
                    var qttBox = arr.eq(i).find('.qttBox').val();
                    var qttInven = arr.eq(i).find('.cai').val();
                    if (subCode.length > 2) {
                        var items = {
                            label: subCode,
                            value: productId,
                            param: param,
                            price: dongia,
                            qttBox: qttBox,
                            qttInven: qttInven,
                            priceBeforeVAT: priceBeforeVAT
                        };
                        data.push(items);
                    }
                }
                if (re.length > 2) {
                    responseFn($.map(data, function (m) {
                        return {
                            label: m.label,
                            value: m.value,
                            param: m.param,
                            qttInven: m.qttInven,
                            qttBox: m.qttBox,
                            price: m.price,
                            priceBeforeVAT: m.priceBeforeVAT

                        };
                    }));
                } else {
                    $(this).val("");
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
                thisRow.find(".priceBeforeVAT").val(ui.item.priceBeforeVAT);
                calcVAT(thisRow);
            }, change: function (event, ui) {
                if (ui.item) {
                    event.preventDefault();
                    thisRow.find(".subCode").val(ui.item.label);
                    thisRow.find(".productId").val(ui.item.value);
                    thisRow.find(".param").val(ui.item.param);
                    thisRow.find(".qttInven").val(ui.item.qttInven);
                    thisRow.find(".qttBox").val(ui.item.qttBox);
                    thisRow.find(".dongia").val(ui.item.price);
                    thisRow.find(".priceBeforeVAT").val(ui.item.priceBeforeVAT);
                    calcVAT(thisRow);
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
    var vat = parseFloat($('.tienvat').eq(0).val().replace(new RegExp(',', 'g'), ''));
    var total = parseFloat($('.tongtiendack').eq(0).val().replace(new RegExp(',', 'g'), ''));
    var discount = parseFloat($('.tongck').eq(0).val());
    $('.addingRow').each(function () {
        var tiendack = parseFloat($(this).find('.tiendack').val().replace(new RegExp(',', 'g'), ''));
        var productId = $(this).find('.productId').val();
        var cai = $(this).find('.cai').val();
        var ck = $(this).find('.ck').val();
        var thung = $(this).find('.thung').val();
        var item = {
            Order_ID: orderId,
            Price: tiendack,
            Quantity: cai,
            Product_ID: productId,
            Box: thung,
            Discount: ck
        };
        items.push(item);
    });
    if (deliveryQtt > 1) {
        for (var i = 0; i < arr.length; i++) {
            var part = [];
            var orderPartId = arr.eq(i).find('.orderPartId').val();
            var Date_export_order = arr.eq(i).find('.invoiceDate').val();
            var table = arr.eq(i).find('.mainTable');
            var partVat = parseFloat(table.find('.tienvat').val().replace(new RegExp(',', 'g'), ''));
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
                items.push(partItem);
            });
            part = {
                Part_ID: i + 1, Order_part_ID: orderPartId, VAT: partVat,
                Total_price: partTotal, Request_stockout_date: Date_export_order
            };
            parts.push(part);
        }
    } else {
        var dateExport = $('#exportDate').val();
    }
    var data = {
        orderId: orderId, customerId: customerId,
        deliveryAddress: deliveryAddress, deliveryQtt: deliveryQtt,
        invoiceAddress: invoiceAddress, rate: rate, taxCode: taxCode,
        subTotal: subTotal, vat: vat, total: total, discount: discount,
        items: items, part: parts, dateExport: dateExport
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
            if (thisRow.attr('class') === 'addingRow') {
                var arr = $('.addingSubRow');
                for (var i = 0; i < arr.length ; i++) {
                    if (arr.eq(i).find('.productId').val() === thisRow.find('.productId').val()) {
                        arr.eq(i).find('.qttInven').val(parseInt(cai));
                    }
                }
            }
            if (parseInt(cai) > 0) {
                if (parseInt(cai) > conlai) {
                    checkQtt(thisRow);
                } else {
                    thisRow.find(".thung").val(parseInt(cai) / per);
                    thisRow.find(".tienchuack").autoNumeric('set', cai * parseFloat(thisRow.find(".dongia").val().replace(new RegExp(',', 'g'), '')));
                    ck(thisRow);
                }
            } else {
                $(this).val("");
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
            if (thisRow.attr('class') === 'addingRow') {
                var arr = $('.addingSubRow');
                for (var i = 0; i < arr.length ; i++) {
                    if (arr.eq(i).find('.productId').val() === thisRow.find('.productId').val()) {
                        arr.eq(i).find('.qttInven').val(parseInt(thung) * per);
                    }
                }
            }
            if (parseInt(thung) * per > 0) {
                if (parseInt(thung) * per > conlai) {
                    checkQtt(thisRow);
                } else {
                    thisRow.find(".cai").val(parseInt(thung * per));
                    thisRow.find(".tienchuack").autoNumeric('set', parseInt(thung * per) * parseFloat(thisRow.find(".dongia").val().replace(new RegExp(',', 'g'), '')));
                    ck(thisRow);
                }
            } else {
                $(this).val("");
            }
            calcTotal(thisTable);
        });
    });
}

function checkQtt(thisRow) {
    if (thisRow.attr('class') === 'addingRow') {
        $.ajax({
            url: '/QuanLy/LapPhieu/CheckQtt',
            dataType: 'json',
            data: { productId: thisRow.find(".productId").val() },
            success: function (data) {
                var div = '<div style="margin-left: 377px;"><i class="fa fa-eye text-black"></i><a onclick="timeline();"><img src="/Assets/dist/img/xem.png"/></a></div>'
                            + '<table class="table table-striped mainTable" style="margin-top: 10px;">'
                            + '<thead>'
                            + '<tr><th style="background-color: white"><img src="/Assets/dist/img/loso.png"/></th><th style="background-color: white"><img src="/Assets/dist/img/soluong.png"/></th><th style="background-color: white"><img src="/Assets/dist/img/ngay.png"/></th><th style="background-color: white"><img src="/Assets/dist/img/soluonglay.png"/></th></tr>'
                            + '</thead>'
                            + '<tbody>';
                for (var i = 0; i < data.length; i++) {
                    var tr = '<tr><td><output class="itemOrderId" style="padding-top: initial; font-size: 18px;">' + data[i].orderId + '</output></td><td style="text-align: right;">' + data[i].qtt
                        + '</td><td>' + data[i].date +
                        '</td><td><input type="text" class="form-control itemQtt" style="text-align: right; width: 50px; float: right;"/><input type="hidden" class="itemId" value="' + data[i].id + '"/></td></tr>';
                    div += tr;
                }
                div += '</tbody></table>';
                swal({
                    title: 'Chỉ còn ' + thisRow.find(".qttInven").val() + ' sản phẩm',
                    type: 'error',
                    html: div,
                    showCancelButton: true,
                    width: 550,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: '<i class="fa fa-check"></i>',
                    cancelButtonText: '<i class="fa fa-close"></i>'
                }).then((result) => {
                    if (result) {
                        var items = [];
                        var arr = $('.itemQtt');
                        for (var i = 0; i < arr.length; i++) {
                            if (arr.eq(i).val() !== "") {
                                var item = {
                                    qtt: parseInt(arr.eq(i).val()), id: parseInt($('.itemId').eq(i).val()),
                                    orderId: $('.itemOrderId').eq(i).val()
                                };
                                items.push(item);
                            }
                        }
                        $.ajax({
                            url: '/QuanLy/LapPhieu/Steal',
                            dataType: 'json',
                            data: { lst: items },
                            success: function () {
                                document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="/Assets/dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
                                swal({
                                    title: '<img src="/Assets/dist/img/messagePic_6.png"/>',
                                    type: 'success'
                                });
                            }
                        });
                    } else {
                        thisRow.find('.cai').val("");
                    }
                });
            }
        });
    } else {
        swal({
            title: '<img src="/Assets/dist/img/messagePic_3.png"/>',
            type: 'error',
            showCancelButton: false,
            showConfirmButton: false,
            timer: 2000
        });
        thisRow.find('.cai').val("");
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
        var iThung = parseFloat(arrThung.eq(i).val());
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

function total(thisTable) {
    var tongck = parseFloat(thisTable.find('.tongck').val());
    var tienck = 0;
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