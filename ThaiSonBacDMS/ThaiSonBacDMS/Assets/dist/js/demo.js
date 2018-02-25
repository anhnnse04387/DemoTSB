/**
 * AdminLTE Demo Menu
 * ------------------
 * You should not use this file in production.
 * This file is for demo purposes only.
 */
var limit = 5;

function saveDraft() {
    swal({
        title: '<img src="dist/img/messagePic_2.png"/>',
        type: 'success'
    })
    document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
}

function chotDon() {
    document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
    swal({
        title: '<img src="dist/img/messagePic_6.png"/>',
        type: 'success'
    }).then((result) => {
        if (result.value) {
            window.location.href = 'phanphoi_donhang_danh-sach-phieu-dang-soan.html';
        }
    });   
}

function create() {
    swal({
        title: '<img src="dist/img/messagePic_1.png"/>',
        imageUrl: 'dist/img/Noti.gif',
        imageWidth: 400,
        imageHeight: 300,
        imageAlt: 'Custom image',
        animation: false
    })
    document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
}

function deliveryQtt() {
    if(document.getElementById('delivery').value === '1') {
        $('#turn1').addClass('noDisplay');
        $('#turn2').addClass('noDisplay');
    } else if(document.getElementById('delivery').value === '2') {
        $('#turn1').removeClass('noDisplay');
        $('#turn2').removeClass('noDisplay');
        document.getElementById('lo_1').value = $('#lo').val() + '#1';
        document.getElementById('lo_2').value = $('#lo').val() + '#2';
    } else {
        $('#turn1').addClass('noDisplay');
        $('#turn2').addClass('noDisplay');
    }
    lookup();
}

function checkQttTong() {
    if(parseInt(document.getElementById('cai_5').value) > limit) {
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
            cancelButtonText: '<i class="fa fa-close"></i>',
        }).then((result) => {
            if (result.value) {
                limit += parseInt(document.getElementById("sl1").value) + parseInt(document.getElementById("sl2").value);
            }
        });
    }
}

function checkQtt() {
    if(parseInt(document.getElementById('cai_7').value) > parseInt(document.getElementById('cai_5').value)) {
        document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
        swal({
            title: '<img src="dist/img/messagePic_3.png"/>',
            type: 'error',
            showCancelButton: false,
            showConfirmButton: false,
            timer: 2000
        });
    }
}

function checkOut() {
    var addDebt = 0;
    var pay = 0;
    if (parseInt(document.getElementById("addDebt").value.replace(new RegExp(',', 'g'), '')) > 0) {
        addDebt = parseInt(document.getElementById("addDebt").value.replace(new RegExp(',', 'g'), ''));
    } 
    document.getElementById("totalDebt").value = (parseInt(document.getElementById('debt').value.replace(new RegExp(',', 'g'), '')) + addDebt).toLocaleString('en');
}

function checkOutProvider() {
    var addDebt = 0;
    var pay = 0;
    if (parseInt(document.getElementById("addDebtP").value.replace(new RegExp(',', 'g'), '')) > 0) {
        addDebt = parseInt(document.getElementById("addDebtP").value.replace(new RegExp(',', 'g'), ''));
    } 
    document.getElementById("totalDebtP").value = (parseInt(document.getElementById('debtP').value.replace(new RegExp(',', 'g'), '')) + addDebt).toLocaleString('en');
}

function redirect() {
    if ($("#page").val() === '1') {
        var win = window.open('cong_no_khach_hang.html', '_blank');
        win.focus();
    } else {
        var win = window.open('cong_no_nha_cung_cap.html', '_blank');
        win.focus();
    }
}

function purchase() {
    document.getElementById('address').value = '17F, Yonsei Jaedan Severance Bldg. 84-11, 5ga, Namdaemun-ro, Jung-gu, Seoul 100-753, Korea.';
    document.getElementById('phone').value = '82-2-2034-4919';
    document.getElementById('email').value = 'lgindust@emirates.net.ae';
    document.getElementById('payment').value = 'L/C 90 days';
}

function choosePo() {
    document.getElementById('commodity').value = 'LS ELECTRICAL PRODUCT';
    document.getElementById('payment').value = 'BY T/T IN ADVANCE OR USANCE L/C 90 DAYS';
    document.getElementById('price').value = 'CIF HAIPHONG PORT, VIETNAM IN US DOLLARS';
    document.getElementById('port').value = 'BUSAN';
    document.getElementById('destination').value = 'HAIPHONG';
    document.getElementById('inspection').value = "MAKER'S TO BE FINAL";
    document.getElementById('package').value = "TO BE PROCEEDED BY SELLER AT SELLER'S EXPENSE, SUITABLE FOR AIR AND INLAND TRANSFORTATION";
    document.getElementById('origin').value = 'REPUBLIC OF KOREA';
    document.getElementById('remark').value = '1. PAYMENT SHALL BE DONE BEFORE SHIPMENT. PROJECT SUPPORT:2,534$';
}

function nhaphang() {
    var cai = 0;
    if (parseInt($("#sl").val().replace(new RegExp(',', 'g'), '')) > 0) {
        cai = parseInt($("#sl").val().replace(new RegExp(',', 'g'), ''));
    }
    document.getElementById('tong_so').value = (cai + 3370).toLocaleString('en');
    document.getElementById('tien').value = ((cai * parseFloat($("#gia").val().replace('.', '')) / 100)).toFixed(2);
    document.getElementById('tong_tien').value = ((cai * parseFloat($("#gia").val().replace('.', '')) / 100) + 15858.1).toFixed(2);
}

function autoFillCustomer() {
    if ($("#donVi").val() === '1') {
        document.getElementById("debt").value = "10,298,157,979";
        document.getElementById("TaxCode").value = "0101788080";
    } else if ($("#donVi").val() === '2') {
        document.getElementById("debt").value = "11,011,055,360";
        document.getElementById("TaxCode").value = "0104879265";
    }
}

function nhap() {
    document.getElementById("address").value = "17F, Yonsei Jaedan Severance Bldg. 84-11, 5ga, Namdaemun-ro, Jung-gu, Seoul 100-753, Korea.";
    document.getElementById("donVi").value = "LS Industrial Systems Co., Ltd.";
    document.getElementById("phone").value = "82-2-2034-4919";
    document.getElementById("day").value = "25/10/2017";
    document.getElementById("requestDay").value = "26/10/2017";
    $('#nhaphang').removeClass('noDisplay');
}

function doi() {
    document.getElementById("address").value = "P9 H5 TT Nguyen Cong Tru, Ha Noi";
    document.getElementById("donVi").value = document.getElementById("temp").value;
    document.getElementById("phone").value = "9781284/ 9762331/ 097";
    document.getElementById("day").value = "13/01/2018";
    document.getElementById("requestDay").value = "13/01/2018";
    $('#doitra').removeClass('noDisplay');
}

function tongCai(stt) {
    var tong_cai = 0;
    if(parseInt(stt) <= 10) {
        tong_cai = ($("#cai_1").val() === '' ? 0 : parseInt($("#cai_1").val())) +
                   ($("#cai_2").val() === '' ? 0 : parseInt($("#cai_2").val())) +
                   ($("#cai_3").val() === '' ? 0 : parseInt($("#cai_3").val())) +
                   ($("#cai_4").val() === '' ? 0 : parseInt($("#cai_4").val())) +
                   ($("#cai_5").val() === '' ? 0 : parseInt($("#cai_5").val())) +
                   ($("#cai_6").val() === '' ? 0 : parseInt($("#cai_6").val())) +
                   ($("#cai_7").val() === '' ? 0 : parseInt($("#cai_7").val())) +
                   ($("#cai_8").val() === '' ? 0 : parseInt($("#cai_8").val())) +
                   ($("#cai_9").val() === '' ? 0 : parseInt($("#cai_9").val())) +
                   ($("#cai_10").val() === '' ? 0 : parseInt($("#cai_10").val()));
        document.getElementById('tong_cai_1').value = (tong_cai).toLocaleString('en');
    } else {
        tong_cai = ($("#cai_11").val() === '' ? 0 : parseInt($("#cai_11").val())) +
                   ($("#cai_12").val() === '' ? 0 : parseInt($("#cai_12").val())) +
                   ($("#cai_13").val() === '' ? 0 : parseInt($("#cai_13").val())) +
                   ($("#cai_14").val() === '' ? 0 : parseInt($("#cai_14").val())) +
                   ($("#cai_15").val() === '' ? 0 : parseInt($("#cai_15").val())) +
                   ($("#cai_16").val() === '' ? 0 : parseInt($("#cai_16").val())) +
                   ($("#cai_17").val() === '' ? 0 : parseInt($("#cai_17").val())) +
                   ($("#cai_18").val() === '' ? 0 : parseInt($("#cai_18").val())) +
                   ($("#cai_19").val() === '' ? 0 : parseInt($("#cai_19").val())) +
                   ($("#cai_20").val() === '' ? 0 : parseInt($("#cai_20").val()));
        document.getElementById('tong_cai_2').value = (tong_cai).toLocaleString('en');
    }
}

function changeStatus() {
    if($('#status').is(":checked")) {
        document.getElementById("address").value = '';
        document.getElementById("donVi").value = '';
        document.getElementById("phone").value = '';
        document.getElementById("day").value = '';
        document.getElementById("requestDay").value = '';
        $('#nameLo').addClass('noDisplay');
        $('#namePi').removeClass('noDisplay');
        $('#doitra').addClass('noDisplay');
        $('#piNo').removeClass('noDisplay');
        $('#loso').addClass('noDisplay');
    } else {
        document.getElementById("address").value = '';
        document.getElementById("donVi").value = '';
        document.getElementById("phone").value = '';
        document.getElementById("day").value = '';
        document.getElementById("requestDay").value = '';
        $('#namePi').addClass('noDisplay');
        $('#nameLo').removeClass('noDisplay');
        $('#nhaphang').addClass('noDisplay');
        $('#piNo').addClass('noDisplay');
        $('#loso').removeClass('noDisplay');
    }
}

function autoFillProvider() {
    if ($("#donViP").val() === '1') {
        document.getElementById("debtP").value = "26,262,716,484";
        document.getElementById("TaxCodeP").value = "";
    } else if ($("#donViP").val() === '2') {
        document.getElementById("debtP").value = "24,305,440,452";
        document.getElementById("TaxCodeP").value = "";
    }
}

function timeline() {
    var win = window.open('phanphoi_donhang_lich-giao-hang-ma-hang.html', '_blank');
    win.focus();
}

function openNewTab() {
    var win = window.open('dist/Preview.pdf', '_blank');
    win.focus();
}

function taomoi() {
    var win = window.open('phanphoi_donhang_lap-phieu.html', '_blank');
    win.focus();
}

function taomoi_lichsu_quanly() {
    var win = window.open('quanly_hanghoa_tao-yeu-cau-nhap.html', '_blank');
    win.focus();
}

function taomoi_lichsu_phanphoi() {
    var win = window.open('phanphoi_hanghoa_tao-yeu-cau-nhap.html', '_blank');
    win.focus();
}

function taomoi_lichsunhaphang_quanly() {
    var win = window.open('quanly_hanghoa_tao-dot-nhap-kho.html', '_blank');
    win.focus();
}

function taomoi_lichsunhaphang_phanphoi() {
    var win = window.open('phanphoi_hanghoa_tao-dot-nhap-kho.html', '_blank');
    win.focus();
}

function taomoi_danhmuc_quanly() {
    var win = window.open('quanly_hanghoa_tao-danh-muc.html', '_blank');
    win.focus();
}

function taomoi_danhmuc_phanphoi() {
    var win = window.open('phanphoi_hanghoa_tao-danh-muc.html', '_blank');
    win.focus();
}

function taomoi_danhmuc_quantri() {
    var win = window.open('quantri_hanghoa_tao-danh-muc.html', '_blank');
    win.focus();
}

function poPreview() {
    var win = window.open('dist/Po_Preview.pdf', '_blank');
    win.focus();
}

function piPreview() {
    var win = window.open('dist/Pi_Preview.pdf', '_blank');
    win.focus();
}

function configCkAll(stt) {
    var ckTong = 0;
    var tien_ck = 0;
    var vat = 0;
    var tong_cai = 0;
    var tong_thung = 0;
    var tien_vat = 0;
    var tien_chua_ck = 0;
    if (parseFloat(document.getElementById("tong_ck_" + stt).value) > 1) {
        ckTong = parseFloat(document.getElementById("tong_ck_" + stt).value);
    }
    if (parseFloat(document.getElementById("vat_" + stt).value) > 1) {
        vat = parseFloat(document.getElementById("vat_" + stt).value);
    }
    if(stt === '1') {
        tien_chua_ck = ($("#tien_da_ck_1").val() === '' ? 0 : parseInt($("#tien_da_ck_1").val().replace(new RegExp(',', 'g'), ''))) +
            ($("#tien_da_ck_2").val() === '' ? 0 : parseInt($("#tien_da_ck_2").val().replace(new RegExp(',', 'g'), ''))) + 
            ($("#tien_da_ck_3").val() === '' ? 0 : parseInt($("#tien_da_ck_3").val().replace(new RegExp(',', 'g'), ''))) +
            ($("#tien_da_ck_4").val() === '' ? 0 : parseInt($("#tien_da_ck_4").val().replace(new RegExp(',', 'g'), ''))) +
            ($("#tien_da_ck_5").val() === '' ? 0 : parseInt($("#tien_da_ck_5").val().replace(new RegExp(',', 'g'), ''))) +
            ($("#tien_da_ck_6").val() === '' ? 0 : parseInt($("#tien_da_ck_6").val().replace(new RegExp(',', 'g'), '')));
        tong_cai = ($("#cai_1").val() === '' ? 0 : parseInt($("#cai_1").val())) +
                   ($("#cai_2").val() === '' ? 0 : parseInt($("#cai_2").val())) +
                   ($("#cai_3").val() === '' ? 0 : parseInt($("#cai_3").val())) +
                   ($("#cai_4").val() === '' ? 0 : parseInt($("#cai_4").val())) +
                   ($("#cai_5").val() === '' ? 0 : parseInt($("#cai_5").val())) +
                   ($("#cai_6").val() === '' ? 0 : parseInt($("#cai_6").val()));
        tong_thung = ($("#thung_1").val() === '' ? 0 : parseInt($("#thung_1").val())) +
                     ($("#thung_2").val() === '' ? 0 : parseInt($("#thung_2").val())) +
                     ($("#thung_3").val() === '' ? 0 : parseInt($("#thung_3").val())) +
                     ($("#thung_4").val() === '' ? 0 : parseInt($("#thung_4").val())) +
                     ($("#thung_5").val() === '' ? 0 : parseInt($("#thung_5").val())) +
                     ($("#thung_6").val() === '' ? 0 : parseInt($("#thung_6").val()));
    } else if(stt === '2') {
        tien_chua_ck = ($('#tien_da_ck_7').val() === '' ? 0 : parseInt($("#tien_da_ck_7").val().replace(new RegExp(',', 'g'), ''))) +
                       ($("#tien_da_ck_8").val() === '' ? 0 : parseInt($("#tien_da_ck_8").val().replace(new RegExp(',', 'g'), '')));
        tong_thung = ($("#thung_7").val() === '' ? 0 : parseInt($("#thung_7").val())) +
                     ($("#thung_8").val() === '' ? 0 : parseInt($("#thung_8").val()));
        tong_cai = ($("#cai_7").val() === '' ? 0 : parseInt($("#cai_7").val())) + 
                   ($("#cai_8").val() === '' ? 0 : parseInt($("#cai_8").val()));
    } else {
        tien_chua_ck = ($("#tien_da_ck_9").val() === '' ? 0 : parseInt($("#tien_da_ck_9").val().replace(new RegExp(',', 'g'), '')));
        tong_thung = $("#thung_9").val() === '' ? 0 : parseInt($("#thung_9").val());
        tong_cai = $("#cai_9").val() === '' ? 0 : parseInt($("#cai_9").val());
    }
    document.getElementById("tong_cai_" + stt).value = tong_cai;
    document.getElementById("tong_thung_" + stt).value = tong_thung;
    document.getElementById("tong_tien_chua_ck_" + stt).value = tien_chua_ck.toLocaleString('en');
    if (ckTong > 0) {
        tien_ck = tien_chua_ck * ckTong / 100;
    }
    document.getElementById("tien_ck_" + stt).value = tien_ck.toLocaleString('en');
    document.getElementById("con_lai_" + stt).value = (tien_chua_ck - tien_ck).toLocaleString('en');
    if (vat > 0) {
        tien_vat = (tien_chua_ck - tien_ck) * vat / 100;
    }
    document.getElementById("tien_vat_" + stt).value = tien_vat.toLocaleString('en');
    document.getElementById("tong_tien_da_ck_" + stt).value = (tien_chua_ck - tien_ck + tien_vat).toLocaleString('en');                              
}

function configCk(stt) {
    var ck = 0;
    var cai = 0;
    var dongia = 0;
    if (parseFloat(document.getElementById("ck_" + stt).value) > 0) {
        ck = parseFloat(document.getElementById("ck_" + stt).value);
    }
    if (parseInt(document.getElementById("cai_" + stt).value) > 0) {
        cai = parseInt(document.getElementById("cai_" + stt).value);
    }
    if (parseInt($("#dongia_" + stt).val().replace(new RegExp(',', 'g'), '')) > 0) {
        dongia = parseInt($("#dongia_" + stt).val().replace(new RegExp(',', 'g'), ''));
    }
    if (cai > 0) {
        if (ck > 0) {
            document.getElementById("tien_da_ck_" + stt).value = (cai * dongia * (100 - ck) / 100).toLocaleString('en');
            document.getElementById("tien_chua_ck_" + stt).value = (cai * dongia).toLocaleString('en');           
        } else {
            document.getElementById("tien_da_ck_" + stt).value = (cai * dongia).toLocaleString('en');
            document.getElementById("tien_chua_ck_" + stt).value = (cai * dongia).toLocaleString('en');
        }
        if(parseInt(stt) < 7) {
            configCkAll('1');
        } else if(parseInt(stt) === 9) {
            configCkAll('3');
        } else {
            configCkAll('2');
        }
    }
}

function configThung(stt) {
    var thung = 0;
    if (parseInt(document.getElementById("thung_" + stt).value) > 0) {
        thung = parseInt(document.getElementById("thung_" + stt).value);
    }
    if (thung > 0) {
        if (stt === '5') {
            document.getElementById("cai_" + stt).value = thung;
        } else {
            document.getElementById("cai_" + stt).value = thung * 20;
        }
        configSp(stt);
    }
}

function configSp(stt) {
    var cai = 0;
    var tong_thung = 0;
    var tong_cai = 0;
    if (parseInt(document.getElementById("cai_" + stt).value) > 0) {
        cai = parseInt(document.getElementById("cai_" + stt).value);
    }
    if (cai > 0) {    
        if (stt === '5') {
            checkQttTong();
        }
        if (stt === '7') {
            checkQtt();                       
        }
        document.getElementById("thung_" + stt).value = parseInt(cai / 20);
        configCk(stt);
    } else {
        document.getElementById("thung_" + stt).value = 0;        
        document.getElementById("tien_chua_ck_" + stt).value = 0;        
        document.getElementById("tien_da_ck_" + stt).value = 0;        
        document.getElementById("cai_" + stt).value = 0; 
        if(parseInt(stt) < 7) {
            configCkAll('1');
        } else if(parseInt(stt) === 9) {
            configCkAll('3');
        } else {
            configCkAll('2');
        }
    }
}

function lookup() {
    var soCk = 6;
    var i = 0;   
    if($("#delivery").val() === '2') {
        soCk = 9;        
    }
    if ($("#donVi").val() === '1') {
        document.getElementById("AddressDeliver").value = "Chu Sang - Nguyen Cong Tru, HN";
        document.getElementById("AddressContact").value = "P9 H5 TT Nguyen Cong Tru, Ha Noi";
        document.getElementById("TaxCode").value = "0101788080";
        for(i = 0; i < soCk; i++) {
            document.getElementById("ck_" + (i+1)).value = "10";
            if($('#dongia_' + (i + 1)).val() !== '') {
                configSp('' + (i + 1));
            }
        }
    } else if ($("#donVi").val() === '2') {
        document.getElementById("AddressDeliver").value = "368, Tran Khat Chan, Ha Noi";
        document.getElementById("AddressContact").value = "9, 3.5 Gamuda Gardens, Ha Noi";
        document.getElementById("TaxCode").value = "0104879265";
        for(i = 0; i < soCk; i++) {
            document.getElementById("ck_" + (i+1)).value = "10";
            if($('#dongia_' + (i + 1)).val() !== '') {
                configSp('' + (i + 1));
            }
        }
    }
}

$(function () {
    'use strict'

    /**
     * Get access to plugins
     */

    $('[data-toggle="control-sidebar"]').controlSidebar()
    $('[data-toggle="push-menu"]').pushMenu()

    var $pushMenu = $('[data-toggle="push-menu"]').data('lte.pushmenu')
    var $controlSidebar = $('[data-toggle="control-sidebar"]').data('lte.controlsidebar')
    var $layout = $('body').data('lte.layout')

    /**
     * List of all the available skins
     *
     * @type Array
     */
    var mySkins = [
        'skin-blue',
        'skin-black',
        'skin-red',
        'skin-yellow',
        'skin-purple',
        'skin-green',
        'skin-blue-light',
        'skin-black-light',
        'skin-red-light',
        'skin-yellow-light',
        'skin-purple-light',
        'skin-green-light'
    ]

    /**
     * Get a prestored setting
     *
     * @param String name Name of of the setting
     * @returns String The value of the setting | null
     */
    function get(name) {
        if (typeof (Storage) !== 'undefined') {
            return localStorage.getItem(name)
        } else {
            window.alert('Please use a modern browser to properly view this template!')
        }
    }

    /**
     * Store a new settings in the browser
     *
     * @param String name Name of the setting
     * @param String val Value of the setting
     * @returns void
     */
    function store(name, val) {
        if (typeof (Storage) !== 'undefined') {
            localStorage.setItem(name, val)
        } else {
            window.alert('Please use a modern browser to properly view this template!')
        }
    }

    /**
     * Toggles layout classes
     *
     * @param String cls the layout class to toggle
     * @returns void
     */
    function changeLayout(cls) {
        $('body').toggleClass(cls)
        $layout.fixSidebar()
        if ($('body').hasClass('fixed') && cls == 'fixed') {
            $pushMenu.expandOnHover()
            $layout.activate()
        }
        $controlSidebar.fix()
    }

    /**
     * Replaces the old skin with the new skin
     * @param String cls the new skin class
     * @returns Boolean false to prevent link's default action
     */
    function changeSkin(cls) {
        $.each(mySkins, function (i) {
            $('body').removeClass(mySkins[i])
        })

        $('body').addClass(cls)
        store('skin', cls)
        return false
    }

    /**
     * Retrieve default settings and apply them to the template
     *
     * @returns void
     */
    function setup() {
        var tmp = get('skin')
        if (tmp && $.inArray(tmp, mySkins))
            changeSkin(tmp)

        // Add the change skin listener
        $('[data-skin]').on('click', function (e) {
            if ($(this).hasClass('knob'))
                return
            e.preventDefault()
            changeSkin($(this).data('skin'))
        })

        // Add the layout manager
        $('[data-layout]').on('click', function () {
            changeLayout($(this).data('layout'))
        })

        $('[data-controlsidebar]').on('click', function () {
            changeLayout($(this).data('controlsidebar'))
            var slide = !$controlSidebar.options.slide

            $controlSidebar.options.slide = slide
            if (!slide)
                $('.control-sidebar').removeClass('control-sidebar-open')
        })

        $('[data-sidebarskin="toggle"]').on('click', function () {
            var $sidebar = $('.control-sidebar')
            if ($sidebar.hasClass('control-sidebar-dark')) {
                $sidebar.removeClass('control-sidebar-dark')
                $sidebar.addClass('control-sidebar-light')
            } else {
                $sidebar.removeClass('control-sidebar-light')
                $sidebar.addClass('control-sidebar-dark')
            }
        })

        $('[data-enable="expandOnHover"]').on('click', function () {
            $(this).attr('disabled', true)
            $pushMenu.expandOnHover()
            if (!$('body').hasClass('sidebar-collapse'))
                $('[data-layout="sidebar-collapse"]').click()
        })

        //  Reset options
        if ($('body').hasClass('fixed')) {
            $('[data-layout="fixed"]').attr('checked', 'checked')
        }
        if ($('body').hasClass('layout-boxed')) {
            $('[data-layout="layout-boxed"]').attr('checked', 'checked')
        }
        if ($('body').hasClass('sidebar-collapse')) {
            $('[data-layout="sidebar-collapse"]').attr('checked', 'checked')
        }

    }

    // Create the new tab
    var $tabPane = $('<div />', {
        'id': 'control-sidebar-theme-demo-options-tab',
        'class': 'tab-pane active'
    })

    // Create the tab button
    var $tabButton = $('<li />', {'class': 'active'})
            .html('<a href=\'#control-sidebar-theme-demo-options-tab\' data-toggle=\'tab\'>'
                    + '<i class="fa fa-wrench"></i>'
                    + '</a>')

    // Add the tab button to the right sidebar tabs
    $('[href="#control-sidebar-home-tab"]')
            .parent()
            .before($tabButton)

    // Create the menu
    var $demoSettings = $('<div />')

    // Layout options
    $demoSettings.append(
            '<h4 class="control-sidebar-heading">'
            + 'Layout Options'
            + '</h4>'
            // Fixed layout
            + '<div class="form-group">'
            + '<label class="control-sidebar-subheading">'
            + '<input type="checkbox"data-layout="fixed"class="pull-right"/> '
            + 'Fixed layout'
            + '</label>'
            + '<p>Activate the fixed layout. You can\'t use fixed and boxed layouts together</p>'
            + '</div>'
            // Boxed layout
            + '<div class="form-group">'
            + '<label class="control-sidebar-subheading">'
            + '<input type="checkbox"data-layout="layout-boxed" class="pull-right"/> '
            + 'Boxed Layout'
            + '</label>'
            + '<p>Activate the boxed layout</p>'
            + '</div>'
            // Sidebar Toggle
            + '<div class="form-group">'
            + '<label class="control-sidebar-subheading">'
            + '<input type="checkbox"data-layout="sidebar-collapse"class="pull-right"/> '
            + 'Toggle Sidebar'
            + '</label>'
            + '<p>Toggle the left sidebar\'s state (open or collapse)</p>'
            + '</div>'
            // Sidebar mini expand on hover toggle
            + '<div class="form-group">'
            + '<label class="control-sidebar-subheading">'
            + '<input type="checkbox"data-enable="expandOnHover"class="pull-right"/> '
            + 'Sidebar Expand on Hover'
            + '</label>'
            + '<p>Let the sidebar mini expand on hover</p>'
            + '</div>'
            // Control Sidebar Toggle
            + '<div class="form-group">'
            + '<label class="control-sidebar-subheading">'
            + '<input type="checkbox"data-controlsidebar="control-sidebar-open"class="pull-right"/> '
            + 'Toggle Right Sidebar Slide'
            + '</label>'
            + '<p>Toggle between slide over content and push content effects</p>'
            + '</div>'
            // Control Sidebar Skin Toggle
            + '<div class="form-group">'
            + '<label class="control-sidebar-subheading">'
            + '<input type="checkbox"data-sidebarskin="toggle"class="pull-right"/> '
            + 'Toggle Right Sidebar Skin'
            + '</label>'
            + '<p>Toggle between dark and light skins for the right sidebar</p>'
            + '</div>'
            )
    var $skinsList = $('<ul />', {'class': 'list-unstyled clearfix'})

    // Dark sidebar skins
    var $skinBlue =
            $('<li />', {style: 'float:left; width: 33.33333%; padding: 5px;'})
            .append('<a href="javascript:void(0)" data-skin="skin-blue" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
                    + '<div><span style="display:block; width: 20%; float: left; height: 7px; background: #367fa9"></span><span class="bg-light-blue" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
                    + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #222d32"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
                    + '</a>'
                    + '<p class="text-center no-margin">Blue</p>')
    $skinsList.append($skinBlue)
    var $skinBlack =
            $('<li />', {style: 'float:left; width: 33.33333%; padding: 5px;'})
            .append('<a href="javascript:void(0)" data-skin="skin-black" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
                    + '<div style="box-shadow: 0 0 2px rgba(0,0,0,0.1)" class="clearfix"><span style="display:block; width: 20%; float: left; height: 7px; background: #fefefe"></span><span style="display:block; width: 80%; float: left; height: 7px; background: #fefefe"></span></div>'
                    + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #222"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
                    + '</a>'
                    + '<p class="text-center no-margin">Black</p>')
    $skinsList.append($skinBlack)
    var $skinPurple =
            $('<li />', {style: 'float:left; width: 33.33333%; padding: 5px;'})
            .append('<a href="javascript:void(0)" data-skin="skin-purple" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
                    + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-purple-active"></span><span class="bg-purple" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
                    + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #222d32"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
                    + '</a>'
                    + '<p class="text-center no-margin">Purple</p>')
    $skinsList.append($skinPurple)
    var $skinGreen =
            $('<li />', {style: 'float:left; width: 33.33333%; padding: 5px;'})
            .append('<a href="javascript:void(0)" data-skin="skin-green" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
                    + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-green-active"></span><span class="bg-green" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
                    + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #222d32"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
                    + '</a>'
                    + '<p class="text-center no-margin">Green</p>')
    $skinsList.append($skinGreen)
    var $skinRed =
            $('<li />', {style: 'float:left; width: 33.33333%; padding: 5px;'})
            .append('<a href="javascript:void(0)" data-skin="skin-red" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
                    + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-red-active"></span><span class="bg-red" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
                    + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #222d32"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
                    + '</a>'
                    + '<p class="text-center no-margin">Red</p>')
    $skinsList.append($skinRed)
    var $skinYellow =
            $('<li />', {style: 'float:left; width: 33.33333%; padding: 5px;'})
            .append('<a href="javascript:void(0)" data-skin="skin-yellow" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
                    + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-yellow-active"></span><span class="bg-yellow" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
                    + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #222d32"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
                    + '</a>'
                    + '<p class="text-center no-margin">Yellow</p>')
    $skinsList.append($skinYellow)

    // Light sidebar skins
    var $skinBlueLight =
            $('<li />', {style: 'float:left; width: 33.33333%; padding: 5px;'})
            .append('<a href="javascript:void(0)" data-skin="skin-blue-light" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
                    + '<div><span style="display:block; width: 20%; float: left; height: 7px; background: #367fa9"></span><span class="bg-light-blue" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
                    + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #f9fafc"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
                    + '</a>'
                    + '<p class="text-center no-margin" style="font-size: 12px">Blue Light</p>')
    $skinsList.append($skinBlueLight)
    var $skinBlackLight =
            $('<li />', {style: 'float:left; width: 33.33333%; padding: 5px;'})
            .append('<a href="javascript:void(0)" data-skin="skin-black-light" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
                    + '<div style="box-shadow: 0 0 2px rgba(0,0,0,0.1)" class="clearfix"><span style="display:block; width: 20%; float: left; height: 7px; background: #fefefe"></span><span style="display:block; width: 80%; float: left; height: 7px; background: #fefefe"></span></div>'
                    + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #f9fafc"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
                    + '</a>'
                    + '<p class="text-center no-margin" style="font-size: 12px">Black Light</p>')
    $skinsList.append($skinBlackLight)
    var $skinPurpleLight =
            $('<li />', {style: 'float:left; width: 33.33333%; padding: 5px;'})
            .append('<a href="javascript:void(0)" data-skin="skin-purple-light" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
                    + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-purple-active"></span><span class="bg-purple" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
                    + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #f9fafc"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
                    + '</a>'
                    + '<p class="text-center no-margin" style="font-size: 12px">Purple Light</p>')
    $skinsList.append($skinPurpleLight)
    var $skinGreenLight =
            $('<li />', {style: 'float:left; width: 33.33333%; padding: 5px;'})
            .append('<a href="javascript:void(0)" data-skin="skin-green-light" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
                    + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-green-active"></span><span class="bg-green" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
                    + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #f9fafc"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
                    + '</a>'
                    + '<p class="text-center no-margin" style="font-size: 12px">Green Light</p>')
    $skinsList.append($skinGreenLight)
    var $skinRedLight =
            $('<li />', {style: 'float:left; width: 33.33333%; padding: 5px;'})
            .append('<a href="javascript:void(0)" data-skin="skin-red-light" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
                    + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-red-active"></span><span class="bg-red" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
                    + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #f9fafc"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
                    + '</a>'
                    + '<p class="text-center no-margin" style="font-size: 12px">Red Light</p>')
    $skinsList.append($skinRedLight)
    var $skinYellowLight =
            $('<li />', {style: 'float:left; width: 33.33333%; padding: 5px;'})
            .append('<a href="javascript:void(0)" data-skin="skin-yellow-light" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
                    + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-yellow-active"></span><span class="bg-yellow" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
                    + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #f9fafc"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
                    + '</a>'
                    + '<p class="text-center no-margin" style="font-size: 12px">Yellow Light</p>')
    $skinsList.append($skinYellowLight)

    $demoSettings.append('<h4 class="control-sidebar-heading">Skins</h4>')
    $demoSettings.append($skinsList)

    $tabPane.append($demoSettings)
    $('#control-sidebar-home-tab').after($tabPane)

    setup()

    $('[data-toggle="tooltip"]').tooltip()
})
