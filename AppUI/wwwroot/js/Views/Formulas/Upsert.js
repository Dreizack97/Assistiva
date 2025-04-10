$(document).ready(function () {
    renderMathInElement(document.body, {
        delimiters: [
            { left: "$", right: "$", display: true }
        ],
        throwOnError: false
    })

    renderPreview()
})

function renderPreview() {
    let formulaText = $("#Content").val()
    const previewDiv = $("#formulaPreview")

    if (formulaText.trim() === "") {
        previewDiv.html('<span class="text-muted"></span>');
        return;
    }

    try {
        previewDiv.empty();
        let texContent = "$" + formulaText + "$";

        previewDiv.text(texContent);
        renderMathInElement(previewDiv[0], {
            delimiters: [
                { left: "$", right: "$", display: false },
                { left: "$$", right: "$$", display: true }
            ],
            throwOnError: false
        });
    } catch (e) {
        previewDiv.html('<span class="text-danger">Error en la fórmula: ' + e.message + '</span>');
    }
}