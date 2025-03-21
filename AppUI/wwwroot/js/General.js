const instructions = $("#Instructions").val()

$(document).ready(async function () {
    await SpeechSynthesis(instructions)

    await LoadAccesibilityLabels()

    InitializeSpeechRecognition()
})

function SpeechSynthesis(text) {
    return new Promise((resolve) => {
        const speech = new SpeechSynthesisUtterance(text)

        speech.volume = 1
        speech.rate = 1
        speech.pitch = 0.5
        speech.lang = "es-MX"

        speech.onend = () => {
            resolve()
        }

        window.speechSynthesis.speak(speech)
    })
}

async function LoadAccesibilityLabels() {
    const elements = document.querySelectorAll("h1, h2, h3, p, button, a, label")

    for (const element of elements) {
        let text = element.getAttribute("aria-label") || element.innerText || "Elemento sin texto"

        element.style.border = "2px solid red"

        await SpeechSynthesis(text)

        element.style.border = ""
    }
}

function InitializeSpeechRecognition() {
    const speechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition

    if (speechRecognition) {
        const recognition = new speechRecognition()

        recognition.continuous = true
        recognition.lang = "es-MX"
        recognition.interimResults = false
        recognition.start()

        recognition.onresult = (event) => {
            const last = event.results.length - 1
            const text = event.results[last][0].transcript.trim()

            const words = text.toLowerCase().split(" ")

            console.log(words)
        }

        recognition.onerror = function (event) {
            console.error(event.error)
        }
    } else {
        alert('Tu navegador no soporta reconocimiento de voz.')
    }
}

function VoiceCommands(command) {
    if (command === "Nombre de usuario") {
        const username = command.slice(1)

        if (username) {
            $("#Username").val(username)
        }
    }
}