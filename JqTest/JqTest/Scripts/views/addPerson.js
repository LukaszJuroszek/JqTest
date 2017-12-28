$("#showAddPersonModal").click(function () {
    $('#addModal').modal('show');
});

$("#btnCancelAddPerson").click(function () {
    $('#addModal').modal('toggle');
});

$('#addModal').on('hidden.bs.modal', function () {
    $(this).find("#FirstName,#SecondName").val('').end();
});

$("#btnSubmittAddPerson").click(function (e) {
    e.preventDefault();
    var model = {};
    model.FirstName = $('#FirstName').val();
    model.SecondName = $('#SecondName').val();
    $.ajax({
        type: "POST",
        url: "/Home/AddPerson",
        data: '{model: ' + JSON.stringify(model) + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#addModal').modal('toggle');
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