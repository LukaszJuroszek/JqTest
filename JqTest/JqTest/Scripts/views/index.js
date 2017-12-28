var filterValues = {};
var table;

function init() {
    table = $("#jqDateTable").DataTable({
        serverSide: false,
        filter: true,
        orderMulti: true,
        columns: [
            { data: "Id" },
            { data: "FirstName" },
            { data: "SecondName" },
            { data: "CreatedAt" },
            {
                title: "Edit",
                data: "Id",
                render: function (data, type, full, meta) {
                    return '<button class="btn btn-info personEditButton" data-target="#editPersonModal" value=' + data + '>Edit</button>';
                }
            },
            {
                title: "Delete",
                data: "Id",
                render: function (data, type, full, meta) {
                    return '<button class="btn btn-warning personDeleteButton" value=' + data + '>Delete</button>';
                }
            }
        ],
        ajax: function (data, callback, settings) {
            $.ajax({
                url: '/Home/GetPersons',
                method: 'POST',
                data: data
            }).done(callback);
        },
        initComplete: function () {
            $('#jqDateTable_filter').hide();
        }
    });
}

$("#jqDateTable").on('click', 'button.personDeleteButton', function (e) {
    e.preventDefault();
    $.ajax({
        type: "DELETE",
        url: "/Home/DeletePerson",
        data: '{id: ' + this.value + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
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

$(document).ready(init());

$('#jqDateTable > tfoot > tr >').each(function (i) {
    var title = $(this).text();
    $(this).html('<input type="text" class="col-md-3 form-control" placeholder="' + title + '" data-index="' + i + '" />');
});

$('tfoot').on('keyup', 'input', function () {
    table.column($(this).data('index'))
        .search(this.value)
        .draw();
});
