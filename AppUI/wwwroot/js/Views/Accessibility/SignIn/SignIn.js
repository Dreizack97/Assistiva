async function VoiceCommands(command) {
    const cmd = RemoveAccents(command.toLowerCase().trim())

    if (cmd.startsWith("usuario")) {
        const username = cmd.split(' ').slice(1).join('')

        if (username) {
            $("#Username").val(username)
            await SpeechSynthesis('Usuario ingresado correctamente')
        }

    } else if (cmd.startsWith("contrasena") || cmd.startsWith("contraseña")) {
        const parts = cmd.substring('contraseña'.length).trim().split(' ')

        let password = ''

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
        await SpeechSynthesis('Redirigiendo a recuperación de contraseña')
        window.location.href = "/SignIn/ForgotPassword"
    } else if (cmd === 'iniciar sesion' || cmd === 'iniciar') {
        await SpeechSynthesis('Formulario enviado')
        $("#BtnSignIn").click()
    } else if (cmd.startsWith('leer pagina') || cmd.startsWith('leer página') || cmd.startsWith('leer')) {
        await SpeechSynthesis('Iniciando lector de pantalla')
        await LoadAccesibilityLabels()
    } else if (cmd.startsWith('ayuda')) {
        await SpeechSynthesis(instructions)
    }
}

async function LoadAccesibilityLabels() {
    const $elements = $(".card").find("h1, h2, h3, p, button, a, label");

    for (const element of $elements) {
        const $el = $(element);
        let text = $el.attr("aria-label") || $el.text() || "Elemento sin texto";

        $el.css("border", "2px solid red");

        await SpeechSynthesis(text);

        $el.css("border", "");
    }
}