$(document).ready(function () {
    $('.number').autoNumeric('init', { minimumValue: '1', maximumValue: '9999999999999', digitGroupSeparator: ',', decimalPlacesOverride: '0' });
});

function doneOrder() {
    var data = JSON.stringify(getAllData());
    $.ajax({
        url: '/PhanPhoi/ChiTietPhieu/CheckOut',
        type: 'POST',
        dataType: 'json',
        data: JSON.stringify({ orderId: $('#orderId').val() }),
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

function cancelOrder() {
    $.ajax({
        url: '/PhanPhoi/ChiTietPhieu/cancelOrder',
        dataType: 'json',
        data: JSON.stringify({ orderId: $('#orderId').val(), note: $('#reason').val() }),
        success: function () {
            swal({
                title: '<img src="/Assets/dist/img/messagePic_10.png"/>',
                type: 'success'
            });
            document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="/Assets/dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
        },
        error: function () {
            swal({
                title: '<img src="/Assets/dist/img/messagePic_11.png"/>',
                type: 'error'
            });
        }
    });
}