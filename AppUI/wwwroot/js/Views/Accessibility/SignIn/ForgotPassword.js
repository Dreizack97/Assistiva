async function VoiceCommands(command) {
    const cmd = RemoveAccents(command.toLowerCase().trim())

    if (cmd.startsWith("usuario")) {
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
        await SpeechSynthesis('Formulario enviado')
        $("#BtnSend").click()
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