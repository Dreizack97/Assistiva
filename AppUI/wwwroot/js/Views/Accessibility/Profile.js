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
    } else if (cmd.startsWith('nueva contraseña') || cmd.startsWith('nueva contrasena') || cmd.startsWith('nueva')) {
        const parts = cmd.substring('nueva contraseña'.length).trim().split(' ')

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
            $("#ChangePassword_NewPassword").val(password)
            await SpeechSynthesis(`Contraseña ${password} ingresada correctamente}`)
        }
    } else if (cmd.startsWith('confirmar contraseña') || cmd.startsWith('confirmar contrasena') || cmd.startsWith('confirmar')) {
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
            $("#ChangePassword_ConfirmPassword").val(password)
            await SpeechSynthesis(`Contraseña ${password} ingresada correctamente}`)
        }
    } else if (cmd.startsWith('contraseña actual') || cmd.startsWith('contrasena actual') || cmd.startsWith('actual')) {
        const parts = cmd.substring('contraseña actual'.length).trim().split(' ')

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
            $("#ChangePassword_ActualPassword").val(password)
            await SpeechSynthesis(`Contraseña ${password} ingresada correctamente}`)
        }
    } else if (cmd.startsWith("cambiar contraseña") || cmd.startsWith("cambiar contrasena") || cmd.startsWith("cambiar")) {
        $("#BtnChange").click()
        await SpeechSynthesis('Cambiando contraseña. Por favor, espera un momento.')
    } else if (cmd.startsWith('leer pagina') || cmd.startsWith('leer página') || cmd.startsWith('leer')) {
        await SpeechSynthesis('Iniciando lector de pantalla')
        await LoadAccesibilityLabels()
    } else if (cmd.startsWith('ayuda')) {
        await SpeechSynthesis(instructions)
    }
}

async function LoadAccesibilityLabels() {
    const $elements = $(".main").find("h1, h2, h3, p, button, a, label, input")

    for (const element of $elements) {
        const $el = $(element)
        let text = $el.attr("aria-label") || $el.text()

        $el.css("border", "2px solid red")

        await SpeechSynthesis(text)

        $el.css("border", "")
    }
}