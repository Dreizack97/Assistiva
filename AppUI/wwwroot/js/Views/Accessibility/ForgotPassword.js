async function VoiceCommands(command) {
    const cmd = RemoveAccents(command.toLowerCase().trim())

    if (cmd.startsWith("usuario")) {
        $("#Email").focus().css("border", "2px solid blue")

        const parts = cmd.substring('usuario'.length).trim().split(' ')

        let userEmail = ''

        const numberMap = { cero: '0', uno: '1', dos: '2', tres: '3', cuatro: '4', cinco: '5', seis: '6', siete: '7', ocho: '8', nueve: '9' }

        const symbolMap = {
            asterisco: '*', guion: '-', 'guion_bajo': '_', guionbajo: '_', punto: '.', coma: ',', arroba: '@', numeral: '#', paralelo: '|', admiracion: '!', interrogacion: '?', dolar: '$', porcentaje: '%', ampersand: '&', mas: '+'
        }

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

function RemoveAccents(str) {
    return str.normalize("NFD").replace(/[̀-ͯ]/g, "")
}

function Capitalize(text) {
    return text.charAt(0).toUpperCase() + text.slice(1)
}
