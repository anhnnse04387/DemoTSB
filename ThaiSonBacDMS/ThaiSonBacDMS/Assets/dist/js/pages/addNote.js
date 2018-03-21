$(document).ready(function () {
    $('#addNoteBtn').click(function () {
        var account_id = $('#accountID').val();
        var c = $('#txtNote').val();
        $.ajax({
            url: "/HangHoa/Home/NoteEdit",
            data: { accID: account_id, content: c },
            datatype: "json",
            type: "POST",
            success: function (data) {
                document.getElementById('txtNote').value = data;
            }
        });
    });
});