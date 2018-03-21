$(function () {
    $('#example1').DataTable({
        'searching': false,
        'language': {
            "info": "Đang hiển thị _START_ tới _END_ tổng số _TOTAL_ kết quả",
            "lengthMenu": "Hiển thị _MENU_ dòng",
            "paginate": {
                "first": "Trang đầu",
                "last": "Trang cuối",
                "next": "Tiếp",
                "previous": "Trước"
            }
        }
    })
    $('#example2').DataTable({
        'paging': true,
        'lengthChange': false,
        'searching': false,
        'ordering': true,
        'info': true,
        'autoWidth': false
    })
});
$("#noti").click(function () {
    $("#spanNoti").hide(10);
});;