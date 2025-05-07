async function VoiceCommands(command) {
    const cmd = RemoveAccents(command.toLowerCase().trim())

    if (cmd.startsWith("usuario")) {
        $("#Email").focus().css("border", "2px solid blue")

        const parts = cmd.substring('usuario'.length).trim().split(' ')

        let userEmail = ''

        for (let i = 0; i < parts.length; i++) {
            const token = parts[i]

            if (numberMap[token] !== undefined) {
                userEmail += numberMap[token]
            } else if (symbolMap[token] !== undefined) {
                userEmail += symbolMap[token]
            } else {
                userEmail += token
            }
        }

        if (userEmail) {
            $("#Email").val(userEmail)
            await SpeechSynthesis(`Usuario ${userEmail} ingresado correctamente`)
        }
    } else if (cmd === 'enviar enlace' || cmd === 'enviar') {
        $("#BtnSend").click()
        await SpeechSynthesis('Formulario enviado')
    }
}