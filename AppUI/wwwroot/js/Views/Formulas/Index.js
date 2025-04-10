var dataTable

$(document).ready(async function () {
    await DataLoad()
})

async function DataLoad() {
    let subjectId = $("#SubjectId").val()

    dataTable = await $("#dataTable").DataTable({
        responsive: true,
        pageLength: 25,
        ajax: {
            url: `/School/Formulas/GetFormulasBySubjectId?subjectId=${subjectId}`,
            dataSrc: ''
        },
        autoWidth: true,
        columns: [
            { data: 'formulaId', visible: false, searchable: false },
            { data: 'name' },
            { data: 'content' },
            { data: 'description' },
            {
                data: 'formulaId',
                render: function (data) {
                    return `<a class="btn btn-sm btn-primary me-2" data-bs-toggle="tooltip" data-bs-placement="top" title="Editar" href="/School/Subjects/${subjectId}/Formulas/Upsert/${data}"><i class="fas fa-pencil-alt"></i></a>
                    <a class="btn btn-sm btn-danger btn-eliminar" data-bs-toggle="tooltip" data-bs-placement="top" title="Eliminar"><i class="fas fa-trash"></i></a>`
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

    dataTable.on('draw', function () {
        renderMathInElement(document.body, {
            delimiters: [
                { left: '$', right: '$', display: true }
            ],
            throwOnError: false
        })
    })
}

$("#dataTable tbody").on("click", ".btn-eliminar", async function () {
    let fila = $(this).closest("tr")
    const data = dataTable.row(fila).data()

    Swal.fire({
        title: "¿Deseas eliminar?",
        text: `Eliminar formula: ${data.name}.`,
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        confirmButtonText: "Eliminar",
        cancelButtonColor: "#3085d6",
        cancelButtonText: "Cancelar"
    }).then(async (result) => {
        if (result.isConfirmed) {
            $(".swal2-popup").LoadingOverlay("show");
            await Delete(data.formulaId)
        }
    })
})

async function Delete(id) {
    await $.ajax({
        url: `/School/Formulas/Delete/${id}`,
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