async function VoiceCommands(command) {
    const cmd = RemoveAccents(command.toLowerCase().trim())

    if (cmd.startsWith("seleccionar imagen") || cmd.startsWith("seleccionar")) {
        $("#UrlPicture").click()
        await SpeechSynthesis('Abriendo el selector de imagen. Por favor, selecciona una imagen.')
    } else if (cmd.startsWith("subir imagen") || cmd.startsWith("subir")) {
        $("#BtnUpload").click()
        await SpeechSynthesis('Subiendo la imagen. Por favor, espera un momento.')
    } else if (cmd.startsWith('') || cmd.startsWith('')) {
        // Do nothing
    }
}