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
class ThirteenBall {

    constructor(context, canvas) {
        this.context = context;
        this.canvas = canvas;

        this.isBallShowingNumber = true;
        this.defaultText = "13";
        this.defaultFont = "bold 50px 'Times New Roman', Times, serif";
        this.defaultStyle = [0, 0, 0];

        this.answer = "";
        this.answerFont = "bold 10px 'Times New Roman', Times, serif"
        this.answerStyle = [70, 130, 180];
        this.answerMeasurement = undefined;

        this.textAlpha = 1.0;
        this.offsetX = 0.0;
        this.textRadius = 40;
    }

    getStyleString(styleArray) {
        return "rgba(" + styleArray.join() + "," + this.textAlpha + ")";
    }

    applyTranformations() {
        context.translate(150, 150);
        context.scale(1.5, 1.5);
    }

    drawBackgroundBall() {
        context.clearRect(0, 0, canvas.width, canvas.height);
        context.save();

        this.applyTranformations();

        context.beginPath();
        context.fillStyle = "#FFFFF8"
        context.arc(0, 0, 100, 0, 2 * Math.PI, false);
        context.fill();

        context.beginPath();
        context.fillStyle = "coral"
        context.arc(0, 0, 100, -Math.PI / 6, Math.PI / 6, false);
        context.arc(0, 0, 100, 5 * Math.PI / 6, 7 * Math.PI / 6, false);
        context.fill();
        context.restore();
    }

    drawTextBackGround() {

        context.save();

        this.applyTranformations();

        context.beginPath();
        context.fillStyle = "#FFFFF8"
        context.arc(this.offsetX, 0, this.textRadius, -Math.PI / 2, Math.PI / 2, false);
        context.arc(-this.offsetX, 0, this.textRadius, Math.PI / 2, 3 * Math.PI / 2, false);
        context.fill();
        context.restore();
    }

    drawText(text, font, fillStyle, textMeasurement) {
        context.save();

        this.applyTranformations();

        context.beginPath();
        context.font = font;
        context.fillStyle = fillStyle;
        let textYPosition = (textMeasurement.actualBoundingBoxAscent + textMeasurement.actualBoundingBoxDescent) / 2;
        context.fillText(text, -textMeasurement.width / 2, textYPosition);
        context.restore();
    }

    measureText(text, font, fillStyle) {
        context.save();
        context.font = font;
        context.fillStyle = fillStyle;
        let textMeasurement = context.measureText(text);
        context.restore();

        return textMeasurement;
    }

    draw13Ball() {
        let currentText = this.isBallShowingNumber ? this.defaultText : this.answer;
        let currentFont = this.isBallShowingNumber ? this.defaultFont : this.answerFont;
        let currentStyle = this.isBallShowingNumber ? this.defaultStyle : this.answerStyle;
        let textMeasurement = this.measureText(currentText, currentFont, currentStyle);
        this.drawBackgroundBall();
        this.drawTextBackGround();
        this.drawText(currentText, currentFont, this.getStyleString(currentStyle), textMeasurement);
    }

    changeTextAlpha(value) {
        this.textAlpha += value;
        if (this.textAlpha < 0.0) {
            this.textAlpha = 0.0;
        }
        if (this.textAlpha > 1.0) {
            this.textAlpha = 1.0;
        }
    }

    hideCurrentText() {
        let maxTextBackGroundOffsetX = 0;
        if (this.answerMeasurement !== undefined) {
            maxTextBackGroundOffsetX = Math.max(Math.floor(this.answerMeasurement.width / 2 - this.textRadius / 2), 0);
        }

        if (maxTextBackGroundOffsetX > this.offsetX) {
            this.offsetX += 1;
            if (this.offsetX > maxTextBackGroundOffsetX) {
                this.offsetX = maxTextBackGroundOffsetX;
            }
        } else if (maxTextBackGroundOffsetX < this.offsetX) {
            this.offsetX -= 1;
            if (this.offsetX < 0) {
                this.offsetX = 0;
            }
        }

        this.changeTextAlpha(-0.05);
        this.draw13Ball();
    }

    showCurrentText() {
        let alphaDelta = this.isBallShowingNumber ? 0.1 : 0.05;
        this.changeTextAlpha(alphaDelta);
        this.draw13Ball();
    }

    setAnswer(answer) {
        this.answer = answer;
        this.answerMeasurement = this.measureText(this.answer, this.answerFont, this.answerStyle);
    }

    flipBall() {
        this.isBallShowingNumber = !this.isBallShowingNumber;
    }
}

//----------------------------------------------------------------

var canvas, context;
let thirteenBall;
let ballFacingUp = true;
let interval;

window.onload = function init() {
    canvas = document.querySelector("#gameCanvas");
    canvas.onclick = ballFlip;

    context = canvas.getContext('2d');
    thirteenBall = new ThirteenBall(context, canvas);

    thirteenBall.draw13Ball();

};

function clickedOnBall(event) {
    let canvasClientRect = canvas.getBoundingClientRect();
    let coordCanvasX = event.clientX - canvasClientRect.x;
    let coordCanvasY = event.clientY - canvasClientRect.y;

    if (Math.pow(coordCanvasX - 150, 2) + Math.pow(coordCanvasY - 150, 2) < Math.pow(150, 2)) {
        return true;
    }

    return false;
}

function showCurrentTextOnBall() {
    if (thirteenBall.textAlpha >= 1.0) {
        console.log("showCurrentTextOnBall - clearInterval();");
        clearInterval(interval);
    } else {
        console.log("thirteenBall.hideCurrentText();");
        thirteenBall.showCurrentText();
    }
}

function hideCurrentTextOnBall() {
    if (thirteenBall.textAlpha <= 0.0) {
        console.log("hideCurrentTextOnBall() - clearInterval();");
        clearInterval(interval);
        console.log("thirteenBall.flipBall()");
        thirteenBall.flipBall();
        interval = setInterval(showCurrentTextOnBall, 50);
    } else {
        console.log("thirteenBall.hideCurrentText();");
        thirteenBall.hideCurrentText();
    }
}

function setAnswerOnBall() {
    let answer = generateAnswer();
    thirteenBall.setAnswer(answer);
}

function ballFlip(event) {
    if (!clickedOnBall(event)) {
        return;
    }

    ballFacingUp = !ballFacingUp;

    if (ballFacingUp) {
        thirteenBall.setAnswer("");
        interval = setInterval(hideCurrentTextOnBall, 50);
    } else {
        setAnswerOnBall();
        console.log("setInterval(hideCurrentTextOnBall, 50);");
        interval = setInterval(hideCurrentTextOnBall, 50);
    }
}