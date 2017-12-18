/**
 * AdminLTE Demo Menu
 * ------------------
 * You should not use this file in production.
 * This file is for demo purposes only.
 */
function redirect() {
    if ($("#page").val() === '1') {
        var win = window.open('cong_no_khach_hang.html', '_blank');
        win.focus();
    } else {
        var win = window.open('cong_no_nha_cung_cap.html', '_blank');
        win.focus();
    }
}

function autoFillCustomer() {
    if ($("#donVi").val() === '1') {
        document.getElementById("debt").value = "10298157979";
        document.getElementById("TaxCode").value = "0101788080";
    } else if ($("#donVi").val() === '2') {
        document.getElementById("debt").value = "11011055360";
        document.getElementById("TaxCode").value = "0104879265";
    }
}

function autoFillProvider() {
    if ($("#donVi").val() === '1') {
        document.getElementById("debt").value = "26262716484";
        document.getElementById("TaxCode").value = "";
    } else if ($("#donVi").val() === '2') {
        document.getElementById("debt").value = "24305440452";
        document.getElementById("TaxCode").value = "";
    }
}

function debt() {
    var addDebt = 0;
    var pay = 0;
    if (parseInt(document.getElementById("addDebt").value) > 0) {
        addDebt = parseFloat(document.getElementById("addDebt").value);
    }
    if (parseInt(document.getElementById("pay").value) > 0) {
        pay = parseFloat(document.getElementById("pay").value);
    }
    document.getElementById("totalDebt").value = parseInt(document.getElementById('debt').value.replace(',', '')) + addDebt - pay;
}

function openNewTab() {
    var win = window.open('dist/Preview.pdf', '_blank');
    win.focus();
}

function create() {
    $('#print').removeClass('noDisplay');
    $('#saveDraft').addClass('noDisplay');
    $('#preview').addClass('noDisplay');
    swal("Successfully", "You created an order with id - O1349", "success");
}

function configCkAll() {
    var ckTong = 0;
    var tien_ck = 0;
    var vat = 0;
    var tien_vat = 0;
    if (parseFloat(document.getElementById("ck").value) > 1) {
        ckTong = parseFloat(document.getElementById("ck").value);
    }
    if (parseFloat(document.getElementById("vat").value) > 1) {
        vat = parseFloat(document.getElementById("vat").value);
    }
    var tien_chua_ck = ($("#tien_da_ck_1").val() === '' ? 0 : parseInt($("#tien_da_ck_1").val())) +
            ($("#tien_da_ck_2").val() === '' ? 0 : parseInt($("#tien_da_ck_2").val()));
    document.getElementById("tien_chua_ck").value = tien_chua_ck;
    if (ckTong > 0) {
        tien_ck = tien_chua_ck * ckTong / 100;
    }
    document.getElementById("tien_ck").value = tien_ck;
    document.getElementById("con_lai").value = tien_chua_ck - tien_ck;
    if (vat > 0) {
        tien_vat = (tien_chua_ck - tien_ck) * vat / 100;
    }
    document.getElementById("tien_vat").value = tien_vat;
    document.getElementById("tien_da_ck").value = tien_chua_ck - tien_ck + tien_vat;
}

function configCk(stt) {
    var ck = 0;
    var cai = 0;
    if (parseFloat(document.getElementById("ck" + stt).value) > 0) {
        ck = parseFloat(document.getElementById("ck" + stt).value);
    }
    if (parseInt(document.getElementById("cai_" + stt).value) > 0) {
        cai = parseInt(document.getElementById("cai_" + stt).value);
    }
    if (cai > 0) {
        if (ck > 0) {
            document.getElementById("tien_da_ck_" + stt).value = cai * parseInt($("#dongia_" + stt).val()) * (100 - ck) / 100;
            document.getElementById("tien_chua_ck_" + stt).value = cai * parseInt($("#dongia_" + stt).val());
        } else {
            document.getElementById("tien_da_ck_" + stt).value = cai * parseInt($("#dongia_" + stt).val());
            document.getElementById("tien_chua_ck_" + stt).value = cai * parseInt($("#dongia_" + stt).val());
        }
        configCkAll();
    }
}

function configThung(stt) {
    var thung = 0;
    if (parseInt(document.getElementById("thung_" + stt).value) > 0) {
        thung = parseInt(document.getElementById("thung_" + stt).value);
    }
    if (thung > 0) {
        if (stt === '1') {
            document.getElementById("cai_" + stt).value = thung * 20;
        } else {
            document.getElementById("cai_" + stt).value = thung * 13;
        }
        configSp(stt);
    }
}

function configSp(stt) {
    var cai = 0;
    if (parseInt(document.getElementById("cai_" + stt).value) > 0) {
        cai = parseInt(document.getElementById("cai_" + stt).value);
    }
    if (cai > 0) {
        configCk(stt);
        if (stt === '1') {
            document.getElementById("thung_" + stt).value = parseInt(cai / 20);
        } else {
            document.getElementById("thung_" + stt).value = parseInt(cai / 13);
        }

        var tong_thung = ($("#thung_1").val() === '' ? 0 : parseInt($("#thung_1").val())) +
                ($("#thung_2").val() === '' ? 0 : parseInt($("#thung_2").val()));
        var tong_cai = (document.getElementById("cai_1").value === '' ? 0 : parseInt(document.getElementById("cai_1").value)) +
                (document.getElementById("cai_2").value === '' ? 0 : parseInt(document.getElementById("cai_2").value));
        document.getElementById("thung").value = tong_thung;
        document.getElementById("cai").value = tong_cai;
    } else {
        document.getElementById("thung_" + stt).value = 0;
        document.getElementById("thung").value = 0;
        document.getElementById("cai").value = 0;
        document.getElementById("tien_chua_ck_" + stt).value = 0;
        document.getElementById("tien_da_ck_" + stt).value = 0;
        document.getElementById("tien_chua_ck").value = 0;
        document.getElementById("tien_da_ck").value = 0;
    }
}

function lookup() {
    if ($("#donVi").val() === '1') {
        document.getElementById("AddressDeliver").value = "Chu Sang - Nguyen Cong Tru, HN";
        document.getElementById("AddressContact").value = "P9 H5 TT Nguyen Cong Tru, Ha Noi";
        document.getElementById("TaxCode").value = "0101788080";
    } else if ($("#donVi").val() === '2') {
        document.getElementById("AddressDeliver").value = "368, Tran Khat Chan, Ha Noi";
        document.getElementById("AddressContact").value = "9, 3.5 Gamuda Gardens, Ha Noi";
        document.getElementById("TaxCode").value = "0104879265";
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
