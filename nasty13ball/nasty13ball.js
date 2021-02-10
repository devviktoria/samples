//------------------------------------------------------
function generateAnswer() {
    const answers = [
        "No, but thanks for asking.",
        "Sure thing.",
        "Hell, no.",
        "Let me sleep on it.",
        "Get lost!",
        "Great idea.",
        "No chance.",
        "Give me a break!",
        "Not today, nor tomorrow.",
        "Best of luck.",
        "No way.",
        "Don't ask this!",
        "Drop it like a potato.",
        "Solid plan.",
        "Just forget it.",
        "Later gator.",
        "Thumbs down.",
        "Best decision.",
        "Bad move.",
        "Peace out!"
    ]

    let index = Math.floor(Math.random() * answers.length);

    return answers[index];
}

//--------------------------------------------------------

var canvas, context, canvasWidth, canvasHeight;
let answerArea;
let isBallShowingNumber = true;


window.onload = function init() {
    canvas = document.querySelector("#gameCanvas");
    answerArea = document.querySelector("#answerArea");

    canvas.onclick = ballFlip;

    canvasWidth = canvas.width;
    canvasHeight = canvas.height;
    context = canvas.getContext('2d');

    draw13Ball(isBallShowingNumber, "");
};

function ballFlip(event) {
    if (!clickedOnBall(event)) {
        answerArea.innerHTML = "";
        return;
    }

    isBallShowingNumber = !isBallShowingNumber;

    if (isBallShowingNumber) {
        answerArea.innerHTML = "";
        draw13Ball(isBallShowingNumber, "");
    } else {
        answerQuestion(event);
    }
}

function answerQuestion(event) {
    let answer = generateAnswer();
    answerArea.innerHTML = answer;
    draw13Ball(isBallShowingNumber, answer);
}

function clickedOnBall(event) {
    let canvasClientRect = canvas.getBoundingClientRect();
    let coordCanvasX = event.clientX - canvasClientRect.x;
    let coordCanvasY = event.clientY - canvasClientRect.y;

    if (Math.pow(coordCanvasX - 150, 2) + Math.pow(coordCanvasY - 150, 2) < Math.pow(150, 2)) {
        return true;
    }

    return false;
}

function draw13Ball(isBallShowingNumber, text) {

    context.clearRect(0, 0, canvas.width, canvas.height);
    context.save();

    context.translate(150, 150);
    context.scale(1.5, 1.5);
    context.beginPath();
    context.fillStyle = "#FFFFF8"
    context.arc(0, 0, 100, 0, 2 * Math.PI, false);
    context.fill();

    context.beginPath();
    context.fillStyle = "coral"
    context.arc(0, 0, 100, -Math.PI / 6, Math.PI / 6, false);
    context.arc(0, 0, 100, 5 * Math.PI / 6, 7 * Math.PI / 6, false);
    context.fill();



    if (isBallShowingNumber) {
        context.beginPath();
        context.fillStyle = "#FFFFF8"
        context.arc(0, 0, 40, 0, 2 * Math.PI, false);
        context.fill();

        context.beginPath();
        context.font = "bold 50px  'Times New Roman', Times, serif";
        context.fillStyle = "black"
        let textMeasurement = context.measureText("13");
        let textYPosition = (textMeasurement.actualBoundingBoxAscent + textMeasurement.actualBoundingBoxDescent) / 2;
        context.fillText("13", -textMeasurement.width / 2, textYPosition);
    } else {
        context.beginPath();
        context.font = "bold 10px  'Times New Roman', Times, serif";
        context.fillStyle = "black"
        let textMeasurement = context.measureText(text);
        let textYPosition = (textMeasurement.actualBoundingBoxAscent + textMeasurement.actualBoundingBoxDescent) / 2;
        context.fillText(text, -textMeasurement.width / 2, textYPosition);
    }

    context.restore();
}