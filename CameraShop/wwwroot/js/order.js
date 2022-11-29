//var dataTable;

//$(document).ready(function () {
//    loadDataTable();
//});


//function loadDataTable() {
//    dataTable = $('#tblData').DataTable({
//        "ajax": {
//            "url": "/Order/GetAll"
//        },
//        "columns": [
//            { "data": "id", "width": "15%" },
//            { "data": "FirstName", "width": "15%" },
//            { "data": "price", "width": "15%" },
//            { "data": "applicationUser.email", "width": "15%" },
//            { "data": "orderStatus", "width": "15%" },
//            { "data": "orderTotal", "width": "15%" },
//            {
//                "data": "id",
//                "render": function (data) {
//                    return `
//                            <div class="text-center">
//                                <a href="/Order/Details?orderId${data}" 
//                                    <i class="fas fa-edit"></i> 
//                                </a>
//                            </div>
//                           `;
//                }, "width": "40%"
//            }
//        ]
//    });
//}
