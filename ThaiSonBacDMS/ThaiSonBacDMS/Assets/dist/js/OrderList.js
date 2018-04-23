$(document).ready(function () {
    $('#btnSearch').click(function () {
        $.ajax({
            url: "/PhanPhoi/OrderList/Search",
            contentType: 'application/json; charset=utf-8',
            datatype: 'json',
            data: getData(),
            success: function (data) {
                $('#item').html(data);
                setTable();
            }
        });
    });
    setTable();
});
function setTable() {
    $('#dtTable').DataTable({
        'paging': true,
        'lengthChange': true,
        'searching': false,
        'ordering': true,
        'info': true,
        'autoWidth': true,
        'language': {
            "info": "Đang hiển thị _START_ tới _END_ tổng số _TOTAL_ kết quả",
            "lengthMenu": "Hiển thị _MENU_ kết quả",
            "paginate": {
                "first": "Trang đầu",
                "last": "Trang cuối",
                "next": "Tiếp",
                "previous": "Trước"
            }
        }
    });
    $('.datepicker').datepicker();
    $('.number').autoNumeric('init', { minimumValue: '1', maximumValue: '9999999999999', digitGroupSeparator: ',', decimalPlacesOverride: '0' });
}
function getData() {
    var orderId = $('#orderId').val();
    var customer = $('#customer').val();
    var fromDate = $('#fromDate').val();
    var toDate = $('#toDate').val();
    var status = $('#status').val();
    var type = $('#type').val();
    var fromTotal = $('#fromTotal').val();
    var toTotal = $('#toTotal').val();
    var model = {
        orderId: orderId, customer: customer, fromDate: fromDate, toDate: toDate,
        fromTotal: fromTotal, toTotal: toTotal, statusId: status, orderType: type
    };
    return model;
}