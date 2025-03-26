
var dataTable

$(document).ready(function () {
    DataLoad()
})

async function DataLoad() {
    dataTable = await $("#dataTable").DataTable({
        responsive: true,
        pageLength: 25,
        ajax: {
            url: '/School/Users/GetUsers',
            dataSrc: ''
        },
        autoWidth: true,
        columns: [
            { data: 'userId', visible: false, searchable: false },
            { data: 'role' },
            { data: 'username' },
            { data: 'email' },
            { data: 'isPasswordReset' },
            { data: 'lastPasswordReset' },
            { data: 'isPasswordDefect' },
            { data: 'lastPasswordChange' },
            { data: 'createdAt' },
            { data: 'updatedAt' },
            { data: 'isActive' },
            {
                data: 'userId',
                render: function (data) {
                    return `<a class="btn btn-sm btn-primary me-2" data-bs-toggle="tooltip" data-bs-placement="top" title="Editar" href="/School/Users/Upsert/${data}"><i class="fas fa-pencil-alt"></i></a>
                    <a class="btn btn-sm btn-danger btn-eliminar" data-bs-toggle="tooltip" data-bs-placement="top" title="Eliminar"><i class="fas fa-user-slash"></i></a>`
                },
                orderable: false,
                searchable: false,
                width: '110px'
            }
        ],
        order: [[1, "asc"]],
        dom: "Bfrtip",
        buttons: [
            "pageLength",
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/2.0.2/i18n/es-MX.json"
        },
        drawCallback: function (settings) {
            const newTooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
            const newTooltipList = [...newTooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));
        }
    })
}