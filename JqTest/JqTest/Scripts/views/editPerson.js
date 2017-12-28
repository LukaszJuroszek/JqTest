$("#editPersonModal").on('click', '#btnSubmittEditPerson', function (e) {
    e.preventDefault();
    var modal = $("#editPersonModal");
    var model = {};
    model.Id = modal.find('#Id').val();
    model.FirstName = modal.find('#FirstName').val();
    model.SecondName = modal.find('#SecondName').val();
    $.ajax({
        type: "POST",
        url: "/Home/EditPerson",
        data: '{model: ' + JSON.stringify(model) + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#editPersonModal').modal('toggle');
            $("#jqDateTable").DataTable().ajax.reload(null, false);
        },
        failure: function (response) {
            alert(response.json());
        },
        error: function (response) {
            alert(response.json());
        }
    });
});

$("#jqDateTable").on('click', 'button.personEditButton', function (e) {
    $('#editPersonModal').load('/Home/GetEditedPerson?id=' + this.value);
    $('#editPersonModal').modal('show');
});

$("#editPersonModal").on('click', '#btnCancelEditPerson', function (e) {
    $('#editPersonModal').modal('toggle');
});
