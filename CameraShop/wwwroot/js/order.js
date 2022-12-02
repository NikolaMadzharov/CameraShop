var dataTable;

function loadDataTable(url) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/order/" + url
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "firstname", "width": "15%" },
            { "data": "applicationUser.email", "width": "15%" },
            { "data": "orderStatus", "width": "15%" },
            { "data": "orderTotal", "width": "15%" },
            {
                "data": "id",
                "render":  function (data)debugger {
                    debugger  return `
                            <div class="text-center">
                                <a href="/Order/Details/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                            </div>
                           `;
                }, "width": "5%"
            }
        ]
    });
}
