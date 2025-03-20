let synth = window.speechSynthesis
let elements = []
let index = 0
let utterance

$(document).ready(function () {
    elements = document.querySelectorAll("h1, h2, h3, p, button, a, label");
    index = 0;
    LeerElemento();
})

function LeerElemento() {
    if (index < elements.length) {
        let element = elements[index]
        let texto = element.getAttribute("aria-label") || element.innerText || "Elemento sin texto"

        // Resaltar el elemento que se está leyendo
        element.style.border = "2px solid red"

        // Crear la voz
        utterance = new SpeechSynthesisUtterance(texto)
        utterance.lang = "es-MX"

        utterance.onend = function () {
            element.style.border = "" // Quitar el borde después de leer
            index++
            LeerElemento() // Leer el siguiente elemento
        }

        synth.speak(utterance)
    }
}