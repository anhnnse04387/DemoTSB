
$('#row1').hide();
$('#row2').hide();
$('#row3').hide();
$('#row4').hide();
$('#row5').hide();
$('#row6').hide();

$('#btn_row1').click(function () {
    $('#row1').toggle();
});
$('#btn_row2').click(function () {
    $('#row2').toggle();
});
$('#btn_row3').click(function () {
    $('#row3').toggle();
});
$('#btn_row4').click(function () {
    $('#row4').toggle();
});
$('#btn_row5').click(function () {
    $('#row5').toggle();
});
$('#btn_row6').click(function () {
    $('#row6').toggle();
});
$('#btn_row11').click(function () {
    $('#row1').toggle();
});
$('#btn_row21').click(function () {
    $('#row2').toggle();
});
$('#btn_row31').click(function () {
    $('#row3').toggle();
});
$('#btn_row41').click(function () {
    $('#row4').toggle();
});
$('#btn_row51').click(function () {
    $('#row5').toggle();
});
$('#btn_row61').click(function () {
    $('#row6').toggle();
});
$('#noti').click(function () {
    $('#noti_span').hide();
});


$(function () {
    //Date picker
    $('#datepicker').datepicker({
        autoclose: true
    });
});
