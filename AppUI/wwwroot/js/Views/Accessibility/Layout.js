async function VoiceCommands(command) {
    const cmd = RemoveAccents(command.toLowerCase().trim())

    if (cmd.startsWith("mi perfil") || cmd.startsWith("perfil")) {
        await SpeechSynthesis('Redirigiendo a mi perfil')
        window.location.href = "/Students/Home/Profile"
    } else if (cmd.startsWith("mis materias") || cmd.startsWith("materias")) {
        await SpeechSynthesis('Redirigiendo a mi materias')
        window.location.href = "/Students/Subjects/"
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
    const $elements = $(".main").find("h1, h2, h3, p, button, a, label")

    for (const element of $elements) {
        const $el = $(element)
        let text = $el.attr("aria-label") || $el.text() || "Elemento sin texto"

        $el.css("border", "2px solid red")

        await SpeechSynthesis(text)

        $el.css("border", "")
    }
}