async function VoiceCommands(command) {
    const cmd = RemoveAccents(command.toLowerCase().trim())

    if (cmd.startsWith("mi perfil") || cmd.startsWith("perfil")) {
        window.location.href = "/Students/Home/Profile"
        await SpeechSynthesis('Redirigiendo a mi perfil')
    } else if (cmd.startsWith("mis materias") || cmd.startsWith("materias")) {
        window.location.href = "/Students/Subjects/"
        await SpeechSynthesis('Redirigiendo a mi materias')
    } else if (cmd === 'cerrar sesion') {
        window.location.href = "/Students/Home/LogOut"
        await SpeechSynthesis('Cerrando sesión')
    }
}