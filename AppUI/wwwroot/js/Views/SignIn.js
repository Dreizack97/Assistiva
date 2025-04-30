async function VoiceCommands(command) {
    const cmd = RemoveAccents(command.toLowerCase().trim())

    if (cmd.startsWith("usuario")) {

        $("#Username").focus().css("border", "2px solid blue")
        $("#Username").on("blur", () => $("#Username").css("border", ""))

        const username = cmd.split(' ').slice(1).join('')

        if (username) {
            $("#Username").val(username)
            await SpeechSynthesis('Usuario ingresado correctamente')
        }

    } else if (cmd.startsWith("contrasena") || cmd.startsWith("contraseña")) {

        $("#Password").focus().css("border", "2px solid blue")
        $("#Password").on("blur", () => $("#Password").css("border", ""))

        const parts = cmd.substring('contraseña'.length).trim().split(' ')

        let password = ''
        let nextUpper = false

        const numberMap = { cero: '0', uno: '1', dos: '2', tres: '3', cuatro: '4', cinco: '5', seis: '6', siete: '7', ocho: '8', nueve: '9' }

        const symbolMap = {
            asterisco: '*', guion: '-', 'guion_bajo': '_', guionbajo: '_', punto: '.', coma: ',', arroba: '@', numeral: '#', paralelo: '|', admiracion: '!', interrogacion: '?', dolar: '$', porcentaje: '%', ampersand: '&', mas: '+'
        }

        for (let i = 0; i < parts.length; i++) {
            const token = parts[i]

            if (token === 'mayuscula' || token === 'mayúscula') {
                nextUpper = true
            } else if (numberMap[token] !== undefined) {
                password += numberMap[token]
                nextUpper = false
            } else if (symbolMap[token] !== undefined) {
                password += symbolMap[token]
                nextUpper = false
            } else if (token.length === 1) {
                password += nextUpper ? token.toUpperCase() : token
                nextUpper = false
            } else {
                password += nextUpper ? Capitalize(token) : token
                nextUpper = false
            }
        }

        if (password) {
            $("#Password").val(password)
            await SpeechSynthesis(`Contraseña ${password} ingresada correctamente}`)
        }

    } else if (cmd.startsWith('olvide') || cmd === 'olvide mi contraseña') {
        window.location.href = "/SignIn/ForgotPassword"
        await SpeechSynthesis('Redirigiendo a recuperación de contraseña')
    } else if (cmd === 'iniciar sesion' || cmd === 'iniciar') {
        $("#BtnSignIn").click()
        await SpeechSynthesis('Formulario enviado')
    }
}

function RemoveAccents(str) {
    return str.normalize("NFD").replace(/[̀-ͯ]/g, "")
}

function Capitalize(text) {
    return text.charAt(0).toUpperCase() + text.slice(1)
}
