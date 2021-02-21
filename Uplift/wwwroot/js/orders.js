﻿var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("approved")) {
        loadDataTable("GetAllApprovedOrders")
    }
    else {
        if (url.includes("pending")) {
            loadDataTable("GetAllPendingOrders");
        }
        else {
            loadDataTable("GetAllOrders");
        }
    }
});

function loadDataTable(url) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/order/" + url,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "phone", "width": "20%" },
            { "data": "email", "width": "15%" },
            { "data": "serviceCount", "width": "15%" },
            { "data": "status", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    //return `<div class="text-center">
                    //    <a href="/books/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                    //        Edit
                    //    </a>
                    //    &nbsp;
                    //    <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                    //        onclick=Delete('/books/Delete?id='+${data})>
                    //        Delete
                    //    </a>
                    //    </div>`;
                    return `<div class="text-center">
                                <a href="/Admin/order/Details/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='far fa-edit'></i> Details
                                </a>

                            </div>
                            `;
                }, "width": "15%"
            }
        ],
        "language": {
            "emptyTable": "No Records Found."
        },
        "width": "100%"
    });
}
