const instructions = $("#Instructions").val()

$(document).ready(async function () {
    await SpeechSynthesis(instructions)

    await LoadAccesibilityLabels()

    await InitializeSpeechRecognition()
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

async function InitializeSpeechRecognition() {
    const speechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition

    if (speechRecognition) {
        const recognition = new speechRecognition()

        recognition.continuous = true
        recognition.lang = "es-MX"
        recognition.interimResults = false
        recognition.start()

        recognition.onresult = async (event) => {
            let finalTranscript = ""
            let interimTranscript = ""

            for (let i = event.resultIndex; i < event.results.length; ++i) {
                if (event.results[i].isFinal) {
                    finalTranscript += event.results[i][0].transcript.trim().toLowerCase()
                } else {
                    interimTranscript += event.results[i][0].transcript
                }
            }

            await VoiceCommands(finalTranscript)
        }

        recognition.onerror = function (event) {
            console.error(event.error)
        }
    } else {
        alert('Tu navegador no soporta reconocimiento de voz.')
    }
}

async function VoiceCommands(command) {
    if (command[0] === "ayuda") {
        await SpeechSynthesis(instructions)
    }
}