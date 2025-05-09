async function VoiceCommands(command) {
    const cmd = RemoveAccents(command.toLowerCase().trim())

    if (cmd.startsWith("mi perfil") || cmd.startsWith("perfil")) {
        window.location.href = "/Students/Home/Profile"
        await SpeechSynthesis('Redirigiendo a mi perfil')
    } else if (cmd.startsWith("mis materias") || cmd.startsWith("materias")) {
        window.location.href = "/Students/Subjects/"
        await SpeechSynthesis('Redirigiendo a mi materias')
    } else if (cmd.startsWith('cerrar sesion')) {
        window.location.href = "/Students/Home/LogOut"
        await SpeechSynthesis('Cerrando sesión')
    } else if (cmd.startsWith('leer pagina') || cmd.startsWith('leer') || cmd.startsWith('lector de pantalla')) {
        await SpeechSynthesis('Iniciando lector de pantalla')
        await LoadAccesibilityLabels()
    } else if (cmd.startsWith('ayuda')) {
        await SpeechSynthesis(instructions)
    }
}

async function LoadAccesibilityLabels() {
    const $elements = $(".main").find("h1, h2, h3, p, button, a, label");

    for (const element of $elements) {
        const $el = $(element);
        let text = $el.attr("aria-label") || $el.text() || "Elemento sin texto";

        $el.css("border", "2px solid red");

        await SpeechSynthesis(text);

        $el.css("border", "");
    }
}