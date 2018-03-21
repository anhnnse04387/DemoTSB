$(document).ready(function () {
    function scroll() {
        $("#scrollBox").hover(function () {
            $("#scrollBox").stop();
            window.clearInterval(scrollInterval);
        });
        var x = 0;
        var scrollInterval = setInterval(function () {
            $("#scrollBox").delay(2000).animate({ top: "-=30" }, 1000);
            if (++x === 2) {
                window.clearInterval(scrollInterval);
                $("#scrollBox").delay(2000).animate({ top: "0px" }, 1000);
                scroll();
            }
        });
    };
    scroll();
});
$(document).on('hide.bs.modal', '#noteModal', function () {
    clearTimeout(this.timeout);
    this.timeout = setTimeout(function () {
        location.reload();
    }, 0);
});
