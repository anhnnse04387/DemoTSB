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
                + '<input type="hidden" class="productId"/><input type="hidden" class="qtt"/></td>'
                + '<td style="vertical-align: middle;"><input type="text" required class="form-control param" /></td>'
                + '<td><input type="text" class="form-control cai number" required/></td>'
                + '<td><input type="text" class="form-control thung number" required/></td>'
                + '<td><output class="dongia number"></output></td>'
                + '<td><output class="tienchuack number"></output></td>'
                + '<td><input type="text" class="form-control ck number" required/></td>'
                + '<td><output class="tiendack number"></output></td>'
                + '</tr>';
        $('.addingRow').last().after(div);
    });
    productNameATC();
    $('.number').autoNumeric('init', {vMin: '0', vMax: '9999999999999', aSep: ',', mDec: '0'});
});
function productNameATC() {
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
                                    code: m.Product_code,
                                    id: m.Product_ID,
                                    param: m.Product_parameters,
                                    qtt: m.Quantities_in_inventory,
                                    price: parseFloat(m.Price_before_VAT_VND) * (100 - parseFloat(m.VAT_VND))
                                };
                            });
                            responseFn(array);
                        }
                    });
                } else {
                    thisRow.find(".code").val("");
                }
            },
            select: function (event, ui) {
                event.preventDefault();
                thisRow.find(".code").val(ui.item.code);
                thisRow.find(".productId").val(ui.item.id);
                thisRow.find(".param").val(ui.item.param);
                thisRow.find(".qtt").val(ui.item.qtt);
                thisRow.find(".dongia").val(ui.item.price);
            }, change: function (event, ui) {
                if (ui.item) {
                    event.preventDefault();
                    thisRow.find(".code").val(ui.item.code);
                    thisRow.find(".productId").val(ui.item.id);
                    thisRow.find(".param").val(ui.item.param);
                    thisRow.find(".qtt").val(ui.item.qtt);
                    thisRow.find(".dongia").val(ui.item.price);
                } else {
                    thisRow.find(".code").val("");
                }
            }, search: function (event, ui) {
            }, focus: function (event, ui) {
                thisRow.find(".code").val(ui.item.code);
                event.preventDefault();
            }
        }).autocomplete("widget").addClass("fixed-height");
    });
}