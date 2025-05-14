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

            // const formatted = paragraphs.map(p => {
            //     const rawText = p
            //         .replace(/\$\$([^$]+)\$\$/g, '$1') // Block math
            //         .replace(/\$([^$]+)\$/g, '$1')     // Inline math
            //         .replace(/<[^>]+>/g, '')           // Strip any HTML tags
            //         .replace(/\n/g, ' ')               // Replace newlines with space
            //         .trim()

            //         const escapedAria = escapeForAriaLabel(rawText) 

            //         return `<p class="mb-3" aria-label="${escapedAria}">${p.replace(/\n/g, '<br>')}</p>`
            // }).join('')

            const formatted = paragraphs.map(p => {
                // Extrae y transforma fórmulas primero
                let textWithNarration = p
                    .replace(/\$\$([^$]+)\$\$/g, (_, expr) => '' + narrateMathExpression(expr))
                    .replace(/\$([^$]+)\$/g, (_, expr) => '' + narrateMathExpression(expr))
                    .replace(/<[^>]+>/g, '') // Elimina HTML
                    .replace(/\n/g, ' ')     // Reemplaza saltos de línea
                    .trim()

                const escapedAria = escapeForAriaLabel(textWithNarration)

                return `<p class="mb-3" aria-label="${escapedAria}">${p.replace(/\n/g, '<br>')}</p>`
            }).join('')


            const cardHtml = `
                <div class="card mb-3">
                    <div class="card-header">
                        <label>Explicación</label>
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

function escapeForAriaLabel(text) {
    return text
        .replace(/&/g, '&amp;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
}

function narrateMathExpression(expr) {
    return expr
        .replace(/\\frac\s*{([^{}]+)}{([^{}]+)}/g, '$1 dividido por $2')
        .replace(/\\times/g, ' por ')
        .replace(/\\div/g, ' dividido ')
        .replace(/\\cdot/g, ' multiplicado por ')
        .replace(/\\sqrt{([^{}]+)}/g, 'raíz cuadrada de $1')
        .replace(/\\sqrt/g, 'raíz cuadrada')
        .replace(/\\pi/g, 'pi')
        .replace(/\\infty/g, 'infinito')
        .replace(/\\leq/g, 'menor o igual que')
        .replace(/\\geq/g, 'mayor o igual que')
        .replace(/\\neq/g, 'distinto de')
        .replace(/\\approx/g, 'aproximadamente igual a')
        .replace(/\\left|([^|]+)\\right\|/g, 'valor absoluto de $1')
        .replace(/\\left\(/g, '(')
        .replace(/\\right\)/g, ')')
        .replace(/_/g, ' sub ')
        .replace(/\^/g, ' a la ')
}
