async function VoiceCommands(command) {
    if (command.startsWith("usuario")) {
        $("#Username").focus()
        $("#Username").css("border", "2px solid blue")

        const username = command.substring(command.indexOf(' ') + 1)

        if (username) {
            $("#Username").val(RemoveAccents(username.replace(/\s+/g, '')))
        }
    } else if (command.startsWith("contraseña")) {
        $("#Password").focus()
        $("#Password").css("border", "2px solid blue")

        const password = command.substring(command.indexOf(' ') + 1)

        if (password) {
            $("#Password").val(password)
        }
    } else if (command.startsWith("enviar formulario")) {
        $("#BtnSignIn").click()
    }
}

function RemoveAccents(str) {
    return str.normalize("NFD").replace(/[\u0300-\u036f]/g, ""); 
}
