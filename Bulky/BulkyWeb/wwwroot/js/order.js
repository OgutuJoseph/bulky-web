var dataTable;
 
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    }
    else {
        if (url.includes("pending")) {
            loadDataTable("pending");
        }
        else {
            if (url.includes("completed")) {
                loadDataTable("completed");
            }
            else {
                if (url.includes("approved")) {
                    loadDataTable("approved");
                }
                else {
                    loadDataTable("all");
                }
            }
        }
    }

    loadDataTable();
});

function loadDataTable(status) {
    dataTable = $('#ordersTable').DataTable({
        "ajax": { url: '/admin/order/getall?status=' + status },
        "columns": [
            { data: 'id' },
            { data: 'name' },
            { data: 'phoneNumber' },
            { data: 'applicationUser.email' },
            { data: 'orderStatus' },
            { data: 'paymentStatus' },
            { data: 'orderTotal' },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/admin/order/details?orderId=${data}" class="btn btn-info mx-2"><i class="bi bi-eye"></i></a>
                     </div >`
                },
                "width": "15%"
            }
        ]
    });
};
