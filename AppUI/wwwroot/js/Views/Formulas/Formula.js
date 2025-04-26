$(document).ready(function () {
    renderMathInElement(document.body, {
        delimiters: [
            { left: "$", right: "$", display: true }
        ],
        throwOnError: false
    })

    const API_KEY = 'AIzaSyCSui0yU018KdhdfilKkB79AIWflPUYPWk'

    const ENDPOINT = `https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=${API_KEY}`

    const formula = $("#Content").val()

    $('#Result').html(
        `<div class="d-flex align-items-center justify-content-center">
            <span class="me-2">Generando explicación…</span>
            <div class="spinner-grow" role="status" aria-hidden="true"></div>
       </div>`
    )

    const promptText = `Explica de forma clara y didáctica la siguiente fórmula matemática: \"${formula}\".` +
        `\\n1) Detalla qué representa la fórmula y define cada variable.` +
        `\\n2) Muestra paso a paso cómo se aplica en un ejemplo práctico de la vida cotidiana.` +
        `\\n3) Todas las expresiones o símbolos matemáticos deben ir correctamente delimitados usando:` +
        `\\n- Un solo signo de dólar $...$ para expresiones en línea.` +
        `\\n- Doble signo de dólar $$...$$ para expresiones en bloque.` +
        `\\nNo utilices comillas invertidas \\\` para las fórmulas matemáticas.`

    $.ajax({
        url: ENDPOINT,
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            contents: [
                {
                    parts: [{ text: promptText }]
                }
            ]
        }),
        success: (res) => {
            const raw = res.candidates?.[0]?.content?.parts?.[0]?.text || ''

            let html = raw
                .replace(/```latex\s*([\s\S]*?)```/g, '<pre><code>$1</code></pre>')
                .replace(/`([^`]+?)`/g, '$$$1$$')
                .replace(/\*\*(.+?)\*\*/g, '<strong>$1</strong>')

            const paragraphs = html.split(/\n{2,}/)

            const formatted = paragraphs
                .map(p => `<p class="mb-3">${p.replace(/\n/g, '<br>')}</p>`)
                .join('')

            const cardHtml = `
                <div class="card mb-3">
                    <div class="card-header">
                        Explicación
                    </div>
                    <div class="card-body">
                        ${formatted}
                    </div>
                </div>`

            $('#Result').html(cardHtml)

            renderMathInElement(document.getElementById('Result'), {
                delimiters: [
                    { left: '$$', right: '$$', display: true },
                    { left: '$', right: '$', display: false },
                    { left: '\\(', right: '\\)', display: false },
                    { left: '\\[', right: '\\]', display: true }
                ],
                throwOnError: false
            })
        },
        error: (jqXHR) => {
            const msg = jqXHR.responseJSON?.error?.message || 'Error desconocido'

            $('#Result').html(
                `<div class="alert alert-danger">Error en API: ${msg}</div>`
            )
        }
    })
})