$(document).ready(function () {
    $('.number').autoNumeric('init', { minimumValue: '1', maximumValue: '9999999999999', digitGroupSeparator: ',', decimalPlacesOverride: '0' });
});

function doneOrder() {
    $.ajax({
        url: '/QuanLy/ChiTietPhieu/CheckOut',
        type: 'POST',
        dataType: 'json',
        data: { orderId: $('#orderId').val() },
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
                    window.location.href = '/QuanLy/OrderList/Processing';
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

function cancelOrder() {
    $.ajax({
        url: '/QuanLy/ChiTietPhieu/cancelOrder',
        dataType: 'json',
        data: { orderId: $('#orderId').val(), note: $('#reason').val() },
        success: function () {
            document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="/Assets/dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
            swal({
                title: '<img src="/Assets/dist/img/messagePic_10.png"/>',
                type: 'success'
            }).then((result) => {
                if (result.value) {
                    window.location.href = '/QuanLy/Home/Index';
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