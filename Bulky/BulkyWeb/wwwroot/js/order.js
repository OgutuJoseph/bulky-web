$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#ordersTable').DataTable({
        "ajax": { url: '/admin/order/getall' },
        "columns": [
            { data: 'id' },
            { data: 'name' },
            { data: 'phoneNumber' },
            { data: 'applicationUser.email' },
            { data: 'orderStatus' },
            { data: 'orderTotal' },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/admin/product/details?id=${data}" class="btn btn-info mx-2"><i class="bi bi-eye"></i></a>
                     </div >`
                },
                "width": "15%"
            }
        ]
    });
};
