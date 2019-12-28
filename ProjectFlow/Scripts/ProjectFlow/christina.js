"use strict";

$(document).ready(function () {
    var christinaHub = $.connection.christina;


    christinaHub.client.sendPassword = function (password) {
        console.log(password);
        const byteCharacters = atob(password);
        const byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
        const blob = new Blob([byteArray], { type: contentType });
    };

    $.connection.hub.start().done(function () {

        christinaHub.server.createRoom("ITP211");
    });
    startRecording();
});

let recording = false;

async function startRecording() {
    let stream = await navigator.mediaDevices.getUserMedia({ video: false, audio: true });
    let recorder = new RecordRTCPromisesHandler(stream, {
        type: 'audio',
        mimeType: 'audio/wav',
        sampleRate: 16000,
        numberOfAudioChannels: 1
    });
    recording = true;
    recorder.startRecording();

    startTimeoutForRecording(recorder);

}

async function startTimeoutForRecording(recorder) {
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
}
