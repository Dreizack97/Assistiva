
var dataTable

$(document).ready(async function () {
    await DataLoad()
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
                width: '100px'
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

$("#dataTable tbody").on("click", ".btn-eliminar", async function () {
    let fila = $(this).closest("tr")
    const data = dataTable.row(fila).data()

    Swal.fire({
        title: "¿Deseas eliminar?",
        text: `Eliminar usuario: ${data.username}.`,
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        confirmButtonText: "Eliminar",
        cancelButtonColor: "#3085d6",
        cancelButtonText: "Cancelar"
    }).then(async (result) => {
        if (result.isConfirmed) {
            $(".swal2-popup").LoadingOverlay("show");
            await Delete(data.userId)
        }
    })
})

async function Delete(userId) {
    await $.ajax({
        url: `/School/Users/Delete/${userId}`,
        type: 'DELETE',
        success: function (data) {
            if (data.success) {
                dataTable.ajax.reload()
                Swal.fire('', data.message, 'success')
            } else {
                Swal.fire('', data.message, 'error')
            }

            $(".swal2-popup").LoadingOverlay("hide");
        }
    })
}