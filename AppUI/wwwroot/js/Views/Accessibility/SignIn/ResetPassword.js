async function VoiceCommands(command) {
    const cmd = RemoveAccents(command.toLowerCase().trim())

    if (cmd.startsWith("contrasena") || cmd.startsWith("contraseña")) {
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
            $("#NewPassword").val(password)
            await SpeechSynthesis(`Contraseña ${password} ingresada correctamente}`)
        }
    } else if (cmd.startsWith("confirmar contraseña") || cmd.startsWith("confirmar")) {
        const parts = cmd.substring('confirmar contraseña'.length).trim().split(' ')

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
            $("#ConfirmPassword").val(password)
            await SpeechSynthesis(`Contraseña ${password} ingresada correctamente}`)
        }
    } else if (cmd === 'cambiar contraseña' || cmd === 'cambiar contrasena' || cmd === 'cambiar') {
        await SpeechSynthesis('Formulario enviado')
        $("#BtnChange").click()
    } else if (cmd.startsWith('leer pagina') || cmd.startsWith('leer página') || cmd.startsWith('leer')) {
        await SpeechSynthesis('Iniciando lector de pantalla')
        await LoadAccesibilityLabels()
    } else if (cmd.startsWith('ayuda')) {
        await SpeechSynthesis(instructions)
    }
}

async function LoadAccesibilityLabels() {
    const $elements = $(".card").find("h1, h2, h3, p, button, a, label, input");

    for (const element of $elements) {
        const $el = $(element);
        let text = $el.attr("aria-label") || $el.text();

        $el.css("border", "2px solid red");

        await SpeechSynthesis(text);

        $el.css("border", "");
    }
}