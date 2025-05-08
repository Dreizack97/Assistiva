async function VoiceCommands(command) {
    const cmd = RemoveAccents(command.toLowerCase().trim())

    if (cmd.startsWith("seleccionar imagen") || cmd.startsWith("seleccionar")) {
        $("#UrlPicture").click()
        await SpeechSynthesis('Abriendo el selector de imagen. Por favor, selecciona una imagen.')
    } else if (cmd.startsWith("subir imagen") || cmd.startsWith("subir")) {
        $("#BtnUpload").click()
        await SpeechSynthesis('Subiendo la imagen. Por favor, espera un momento.')
    } else if (cmd.startsWith('usuario')) {
        const username = cmd.split(' ').slice(1).join('')

        if (username) {
            $("#Username").val(username)
            await SpeechSynthesis('Usuario ingresado correctamente')
        }
    } else if (cmd.startsWith('correo')) {
        const email = cmd.split(' ').slice(1).join('')

        if (email) {
            $("#Email").val(email)
            await SpeechSynthesis(`Correo electrónico ${email} ingresado correctamente`)
        }
    } else if (cmd.startsWith('actualizar datos') || cmd.startsWith('actualizar')) {
        $("#BtnUpdate").click()
        await SpeechSynthesis('Actualizando datos. Por favor, espera un momento.')
    }
}