$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#productsTable').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'title', "width": "25%" },
            { data: 'isbn', "width": "15%" },
            { data: 'author', "width": "15%" },
            { data: 'listPrice', "width": "15%" },
            { data: 'price', "width": "10%" },
            { data: 'category.name', "width": "10%" }
        ]
    });
};