$("#jqDateTable").on('click', 'button.personDeleteButton', function () {
    $('#deletePersonModal').load('/Home/GetPerson?id=' + this.value + '&partialViewName=DeletePerson');
    $('#deletePersonModal').modal('show');
});

$("#deletePersonModal").on('click', '#btnSubmittDeletePerson', function (e) {
    e.preventDefault();
    var modal = $("#deletePersonModal");
    var id = modal.find('#Id').val();
    $.ajax({
        type: "DELETE",
        url: "/Home/DeletePerson",
        data: '{id: ' + id +'}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#deletePersonModal').modal('toggle');
            table.ajax.reload(null, false);
        },
        failure: function (response) {
            alert(response.json());
        },
        error: function (response) {
            alert(response.json());
        }
    });
});

$("#deletePersonModal").on('click', '#btnCancelDeletePerson', function (e) {
    $('#deletePersonModal').modal('toggle');
});
