$(function () {
    var _id = 0;
    $(".deleteMessageButton").click(function () {
        alert(_id);
        $.ajax({
            url: '/Contact/DeleteContact',
            type: 'post',
            data: { 'id': _id },
            success: function (req) {
                window.location.href = '/Contact/GetAllMessages';
            }
        });
    });


    $(".deleteButtonConfirm").click(function () {
        var btnId = $(this).data("id");
        _id = btnId;
        var modal = new bootstrap.Modal(document.getElementById("modal-lg"));
        modal.show();
    });

})