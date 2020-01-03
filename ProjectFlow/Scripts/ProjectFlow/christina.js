"use strict";

let websocket_worker = new Worker('/Scripts/ProjectFlow/christina-worker.js');

$(document).ready(function () {
    var christinaHub = $.connection.christina;


    christinaHub.client.sendPassword = function (password) {
        websocket_worker.postMessage({hostname: window.location.hostname, password});
        startRecording();
    };

    $.connection.hub.start().done(function () {

        christinaHub.server.createRoom(2);
    });
    
});

let recording = false;

async function startRecording() {
    let stream = await navigator.mediaDevices.getUserMedia({audio: true, video: false});
    let recorder = new RecordRTCPromisesHandler(stream, {
        type: 'audio',
        mimeType: 'audio/wav',
        sampleRate: 16000,
        numberOfAudioChannels: 1,
        disableLogs: false
    });

    recording = true;
    recorder.startRecording();

    startTimeoutForRecording(recorder, stream);

}

async function startTimeoutForRecording(recorder, stream) {
    setTimeout(async function () {
        if (recording) {
            processAudio(recorder);
            startTimeoutForRecording();
        }
        else await recorder.stopRecording();
    }, 3000);
}

async function processAudio(recorder) {
    await recorder.stopRecording();
    let blob = await recorder.getBlob();
    recorder.startRecording();

    //Send to websocket
    websocket_worker.postMessage({blob});
}


const min_size = 60;
const max_size = 130;

const drawHeight = 1000;
const drawWidth = 1000;

let speakers_circles = [];

function init_display(bound_window, display_canvas) {
    let testImg = new Image();
    testImg.src = 'https://image.flaticon.com/icons/png/512/123/123172.png';
    let testImg2 = new Image();
    testImg2.src = 'https://www.jodilogik.com/wordpress/wp-content/uploads/2016/05/people.png';
    speakers_circles = [create_speaker('Peh Zi Heng', testImg), create_speaker('Peh Zi Heng2', testImg2)];

    animate();

    function animate() {
        let ctx = display_canvas[0].getContext('2d');
        let width = bound_window.width();
        let height = $(window).height() - 300;

        display_canvas[0].width = width;
        display_canvas[0].height = height;

        let scaleX = width / drawWidth;
        let scaleY = height / drawHeight;
        let scale = (width * height) / (drawWidth * drawHeight)

        requestAnimationFrame(animate);
        ctx.clearRect(0, 0, width, height);

        for (let i = 0; i < speakers_circles.length; i++) {
            let speaker_circle = speakers_circles[i];
            if (speaker_circle.grow) {
                speaker_circle.percent = (speaker_circle.percent + .01).clamp(0, 1);
                if (speaker_circle.percent == 1) speaker_circle.grow = false;
            } else if (speaker_circle.ungrow) {
                speaker_circle.percent = (speaker_circle.percent - .01).clamp(0, 1);
                if (speaker_circle.percent == 0) speaker_circle.ungrow = false;
            }

            let size = (ease(speaker_circle.percent) * (max_size - min_size)) + min_size;

            ctx.save();
            ctx.beginPath();
            ctx.strokeStyle = 'blue';
            ctx.globalAlpha = speaker_circle.percent * .8 + .2;
            ctx.arc(speaker_circle.x * scaleX, speaker_circle.y * scaleY, size * scale, 0, Math.PI * 2, false);
            ctx.stroke();
            if (speaker_circle.speaker_image != null) {
                ctx.clip()
                ctx.drawImage(speaker_circle.speaker_image, speaker_circle.x * scaleX - (size * scale / 2), speaker_circle.y * scaleY - (size * scale / 2), size * scale, size * scale);
            }
            ctx.restore();

        }
        
    }

    function ease(x) {
        if (x <= 0.5) {
            return 2 * x * x;
        } else {
            x -= 0.5;
            return 2 * x * (1 - x) + 0.5;
        }
    }

}
function focus_speaker(speaker_name) {
    for (let i = 0; i < speakers_circles.length; i++) {
        let speaker_circle = speakers_circles[i];
        if (speaker_circle.speak_name == speaker_name) {
            speaker_circle.grow = true;
        } else {
            speaker_circle.ungrow = true;
        }
    }
}


function create_speaker(speaker_name, speaker_image) {
    let range = 200;
    let r = Math.random() * range;
    let theta = Math.random() * (Math.PI * 2); // PI * 2 is the entire circle in radian

    let x = r * Math.cos(theta) + drawWidth / 2;
    let y = r * Math.sin(theta) + drawHeight / 2;
    return { speak_name: speaker_name, speaker_image: speaker_image, x: x, y: y, percent: 0, grow: true, ungrow: false };
}



Number.prototype.clamp = function (min, max) {
    return Math.min(Math.max(this, min), max);
};
