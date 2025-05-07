async function VoiceCommands(command) {
    const cmd = RemoveAccents(command.toLowerCase().trim())

    if (cmd.startsWith("contrasena") || cmd.startsWith("contraseña")) {
        $("#NewPassword").focus().css("border", "2px solid blue")

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
        $("#ConfirmPassword").focus().css("border", "2px solid blue")

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
        $("#BtnChange").click()
        await SpeechSynthesis('Formulario enviado')
    }
}