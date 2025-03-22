async function VoiceCommands(command) {
    if (command.includes("usuario")) {
        $("#Username").focus()
        $("#Username").css("border", "2px solid blue")

        const username = command.slice(1).join("")

        if (username) {
            $("#Username").val(RemoveAccents(username))
        }
    } else if (command.includes("contraseña")) {
        $("#Password").focus()
        $("#Password").css("border", "2px solid blue")

        const password = command.slice(1).join("")

        if (password) {
            $("#Password").val(password)
        }
    } else if (command.includes("enviar")) {
        $("#BtnSignIn").click()
    }
}

function RemoveAccents(str) {
    return str.normalize("NFD").replace(/[\u0300-\u036f]/g, ""); 
}
