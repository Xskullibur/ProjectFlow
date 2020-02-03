"use strict";

let websocket_worker = new Worker('/Scripts/ProjectFlow/christina-worker.js');

let recorder = null;

$(document).ready(function () {
    var christinaHub = $.connection.christina;

    //Clear textbox
    //$('#ContentPlaceHolder_TranscriptTxtBox').val('');

    let createdRoom = localStorage.getItem('create_room') === 'true' || false;
    christinaHub.client.sendPassword = function (password) {
        createdRoom = true;
        localStorage.setItem('create_room', true);
        localStorage.setItem('room_accessToken', password);

        websocket_worker.postMessage({
            hostname: window.location.hostname,
            password: password
            
        });

        websocket_worker.addEventListener('message', (e) => {

            //If prediction
            if (e.data.predicted_speaker) {
                // Rerecord microphone once receive the predicted speaker from server
                focus_speaker(e.data.predicted_speaker);
                if (e.data.predicted_speaker != '') {
                    let txtBox = $('#ContentPlaceHolder_TranscriptTxtBox');
                    txtBox.val(e.data.predicted_speaker + ':' + e.data.transcript + '\n' + txtBox.val());
                }


                
            } else {
                focus_speaker('');
            }

            console.log('Resume recording');
            recorder.startRecording();
            startTimeoutForRecording(recorder);
        });

        startRecording();

    }

    christinaHub.client.sendRoomID = function (roomID) {
        localStorage.setItem('room_id', roomID);
        $('#ContentPlaceHolder_RoomID').val(roomID);
        document.getElementById('ContentPlaceHolder_RoomUpdateEventLinkBtn').click();
    }


    $.connection.hub.start().done(function () {

        //Get url params 
        const urlParams = new URLSearchParams(window.location.search);
        const roomName = urlParams.get('RoomName');
        const roomDescription = urlParams.get('RoomDescription');
        const attendees = urlParams.get('Attendees');

        const teamID = parseInt($('#TeamID').val());

        if (!createdRoom) christinaHub.server.createRoom(teamID, roomName, roomDescription, attendees.split(','));
        else {
            const accessToken = localStorage.getItem('room_accessToken');
            christinaHub.server.reconnectRoom(localStorage.getItem('room_id'), teamID, accessToken);
        }
    });

    //When error
    christinaHub.client.expiredRoom = function () {
        window.location.href = '/InvalidRequest.aspx';
    }
    christinaHub.client.illegalAccess = function () {
        window.location.href = '/InvalidRequest.aspx';
    }
});

async function startRecording() {
    let stream = await navigator.mediaDevices.getUserMedia({audio: true, video: false});
    recorder = new RecordRTCPromisesHandler(stream, {
        type: 'audio',
        mimeType: 'audio/wav',
        sampleRate: 16000,
        numberOfAudioChannels: 1,
        disableLogs: false
    });

    recorder.startRecording();

    startTimeoutForRecording(recorder);

}

async function startTimeoutForRecording(recorder) {
    setTimeout(async function () {
       processAudio(recorder);
    }, 3000);
}

async function processAudio(recorder, completed_cb) {
    await recorder.stopRecording();
    console.log('Stopped recording');
    let blob = await recorder.getBlob();

    //Send to websocket
    websocket_worker.postMessage({ blob, completed: completed_cb});
}


const min_size = 60;
const max_size = 130;

const drawHeight = 1000;
const drawWidth = 1000;

let speakers_circles = [];

function init_display(bound_window, display_canvas) {
    animate();

    function animate() {
        let ctx = display_canvas[0].getContext('2d');
        let width = bound_window.width();
        let height = $(window).height() - 300;

        display_canvas[0].width = width;
        display_canvas[0].height = height;

        let scaleX = width / drawWidth;
        let scaleY = height / drawHeight;
        let scale = Math.abs((width * height) / (drawWidth * drawHeight));

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
                ctx.drawImage(speaker_circle.speaker_image, speaker_circle.x * scaleX - (size * scale), speaker_circle.y * scaleY - (size * scale), size * scale * 2, size * scale * 2);
            }
            ctx.restore();

            //Draw name
            ctx.font = "15px Verdana";
            ctx.textAlign = "center"; 
            ctx.fillText(speaker_circle.speaker_name, speaker_circle.x * scaleX, speaker_circle.y * scaleY - (size * scale / 2) + size * scale + 60);

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
        if (speaker_circle.speaker_name == speaker_name) {
            speaker_circle.grow = true;
        } else {
            speaker_circle.ungrow = true;
        }
    }
}


function create_speaker(speaker_name, speaker_image) {
    let range = 300;
    let r = Math.random() * range;
    let theta = Math.random() * (Math.PI * 2); // PI * 2 is the entire circle in radian

    let x = r * Math.cos(theta) + drawWidth / 2;
    let y = r * Math.sin(theta) + drawHeight / 2;
    let img = new Image(300, 300);

    img.onload = function () {
        let scale = .0000001;
        // resize image
        var oc = document.createElement('canvas'),
            octx = oc.getContext('2d');

        oc.width = img.width * scale;
        oc.height = img.height * scale;
        octx.drawImage(img, 0, 0, oc.width, oc.height);

        octx.drawImage(oc, 0, 0, oc.width * scale, oc.height * scale);

        ctx.drawImage(oc, 0, 0, oc.width * scale, oc.height * scale,
            0, 0, canvas.width, canvas.height);
    }

    img.src = window.location.origin + '/' + speaker_image;

    for (let i = 0; i < speakers_circles.length; i++) {
        let speaker_circle = speakers_circles[i];
        if (circle_intersects(x, y, speaker_circle.x, speaker_circle.y, max_size, max_size)) {
            return create_speaker(speaker_name, speaker_image);
        }
    }

    return { speaker_name: speaker_name, speaker_image: img, x: x, y: y, percent: 0, grow: true, ungrow: false };
}

function circle_intersects(cx1, cy1, cx2, cy2, circle_radius1, circle_radius2) {
    let dist = Math.sqrt(((cx2 - cx1) * (cx2 - cx1)) + ((cy2 - cy1) * (cy2 - cy1)));

    let radii = circle_radius1 + circle_radius2;

    if (dist > radii) {
        return false;
    } else {
        return true;
    }

}


Number.prototype.clamp = function (min, max) {
    return Math.min(Math.max(this, min), max);
};
