async function VoiceCommands(command) {
    const cmd = RemoveAccents(command.toLowerCase().trim())

    if (cmd.startsWith("mi perfil") || cmd.startsWith("perfil")) {
        await SpeechSynthesis('Redirigiendo a mi perfil')
        window.location.href = "/Students/Home/Profile"
    } else if (cmd.startsWith('abir formula') || cmd.startsWith('abrir')) {
        const formula = cmd.split(' ').slice(2).join(' ')
        let formulaId = formulas[formula]

        if (formulaId) {
            let subjectId = $("#SubjectId").val()

            await SpeechSynthesis(`Redirigiendo a ${command.split(' ').slice(2).join(' ')}`)
            window.location.href = `/Students/Subjects/Subject/${subjectId}/Formula/${formulaId}`
        }
    } else if (cmd.startsWith('regresar')) {
        await SpeechSynthesis('Regresando a la página anterior')
        window.location.href = '/Students/Subjects'
    } else if (cmd.startsWith('cerrar sesion')) {
        await SpeechSynthesis('Cerrando sesión')
        window.location.href = "/Students/Home/LogOut"
    } else if (cmd.startsWith('leer pagina') || cmd.startsWith('leer página') || cmd.startsWith('leer')) {
        await SpeechSynthesis('Iniciando lector de pantalla')
        await LoadAccesibilityLabels()
    } else if (cmd.startsWith('ayuda')) {
        await SpeechSynthesis(instructions)
    }
}

async function LoadAccesibilityLabels() {
    const $elements = $(".main").find("h1, h2, h3, p, a, label, input, textarea")

    for (const element of $elements) {
        const $el = $(element)
        let text = $el.attr("aria-label") || $el.text()

        $el.css("border", "2px solid red")

        await SpeechSynthesis(text)

        $el.css("border", "")
    }
}