let debounceTimer

$(document).ready(async function () {
    if ($("#StudentId").val() > 0){
        await DataLoad()
    }
})

async function DataLoad() {
    let studentId = $("#StudentId").val()

    await fetch(`/School/ClassroomStudents/GetStudentById?studentId=${studentId}`).then(response => {
        return response.ok ? response.json() : Promise.reject(response)
    }).then(responseJson => {
        if (responseJson != null) {
            $("#StudentName").val(responseJson.fullName)
        }
        else {
            $("#StudentName").val("")
        }
    })
}

async function SearchStudent() {
    let studentName = $("#StudentName").val()
    $("form").validate().form()

    if (!studentName) {
        $("#StudentId").val("")
        $("form").validate().form()
        return;
    }

    clearTimeout(debounceTimer)

    debounceTimer = setTimeout(async () => {
        await fetch(`/School/ClassroomStudents/GetStudentByName?studentName=${studentName}`).then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        }).then(responseJson => {
            if (responseJson != null) {
                $("#StudentId").val(responseJson.studentId)
            }
            else {
                $("#StudentId").val("")
                $("form").validate().form()
            }
        })
    }, 300)
}

$.validator.setDefaults({
    ignore: [] // Esto fuerza a jQuery Validation a validar campos ocultos
})