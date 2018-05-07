$(document).ready(function () {
    $('.number').autoNumeric('init', { minimumValue: '1', maximumValue: '9999999999999', digitGroupSeparator: ',', decimalPlacesOverride: '0' });
});

function toBeContinued() {
    swal({
        title: 'Chức năng này đang được phát triển!',
        type: "warning",
    });
}

function cancelOrder() {
    $.ajax({
        url: '/PhanPhoi/ChiTietPhieu/CancelOrder',
        dataType: 'json',
        data: { orderId: $('#orderId').val(), note: $('#reason').val() },
        success: function () {
            document.getElementById("sound").innerHTML = '<audio autoplay="autoplay"><source src="/Assets/dist/facebook_sound.mp3" type="audio/mpeg" /><embed hidden="true" autostart="true" loop="false" src="dist/facebook_sound.mp3" /></audio>';
            swal({
                title: '<img src="/Assets/dist/img/messagePic_10.png"/>',
                type: 'success'
            }).then((result) => {
                if (result.value) {
                    window.location.href = '/PhanPhoi/OrderList/Cancel';
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