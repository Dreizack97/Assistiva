var dataTable
let subjects = {}


$(document).ready(async function () {
    await DataLoad()
})

async function DataLoad() {
    let studentId = $("#StudentId").val()

    dataTable = await $("#dataTable").DataTable({
        responsive: true,
        pageLength: 25,
        ajax: {
            url: `/Students/Subjects/GetSubjectsByStudentId?studentId=${studentId}`,
            dataSrc: ''
        },
        autoWidth: true,
        columns: [
            { data: 'subjectId', visible: false, searchable: false },
            { data: 'code' },
            { data: 'subjectName' },
            {
                data: 'subjectId',
                render: function (data) {
                    return `<a class="btn btn-sm btn-primary me-2" data-bs-toggle="tooltip" data-bs-placement="top" title="Ver" href="/Students/Subjects/Subject/${data}"><i class="fas fa-eye"></i></a>`
                },
                orderable: false,
                searchable: false,
                width: '50px'
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

    $('#dataTable').on('xhr.dt', function (e, settings, json, xhr) {
        subjects = {};

        json.forEach(row => {
            subjects[RemoveAccents(row.subjectName.toLowerCase().trim())] = row.subjectId
        })
    })
}