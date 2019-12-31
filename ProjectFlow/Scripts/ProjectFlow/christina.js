"use strict";

$(document).ready(function () {
    var christinaHub = $.connection.christina;


    christinaHub.client.sendPassword = function (password) {
        console.log(password);
        //const byteCharacters = atob(password);
        //const byteNumbers = new Array(byteCharacters.length);
        //for (let i = 0; i < byteCharacters.length; i++) {
        //    byteNumbers[i] = byteCharacters.charCodeAt(i);
        //}
        //const byteArray = new Uint8Array(byteNumbers);
        //const blob = new Blob([byteArray], { type: contentType });

        startRecording();

        const socket = new WebSocket("ws://" + window.location.hostname + ":9000");

        socket.onopen = function (e) {
            alert("[open] Connection established");

            //Handshake
            handshake(socket, password);
        };

        socket.onmessage = function (event) {
            alert(`[message] Data received from server: ${event.data}`);
        };

        socket.onclose = function (event) {
            if (event.wasClean) {
                alert(`[close] Connection closed cleanly, code=${event.code} reason=${event.reason}`);
            } else {
                alert('[close] Connection died');
            }
        };

        socket.onerror = function (error) {
            alert(`[error] ${error.message}`);
        };

    };

    $.connection.hub.start().done(function () {

        christinaHub.server.createRoom(2);
    });
    
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

function handshake(socket, password) {
    socket.send(password);
}

