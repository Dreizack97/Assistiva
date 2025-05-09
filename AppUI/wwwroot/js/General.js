const instructions = $("#Instructions").val()

const numberMap = { cero: '0', uno: '1', dos: '2', tres: '3', cuatro: '4', cinco: '5', seis: '6', siete: '7', ocho: '8', nueve: '9' }

const symbolMap = { 
    asterisco: '*', guion: '-', 'guion_bajo': '_', guionbajo: '_', punto: '.', coma: ',', arroba: '@', numeral: '#', paralelo: '|', admiracion: '!', interrogacion: '?', dolar: '$', porcentaje: '%', ampersand: '&', mas: '+'
}

let nextUpper = false

$(document).ready(async function () {
    await SpeechSynthesis(instructions)
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

function RemoveAccents(str) {
    return str.normalize("NFD").replace(/[̀-ͯ]/g, "")
}

function Capitalize(text) {
    return text.charAt(0).toUpperCase() + text.slice(1)
}
