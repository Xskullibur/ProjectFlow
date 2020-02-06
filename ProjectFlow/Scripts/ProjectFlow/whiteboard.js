﻿/**
 * Note Server Functions Called With Lowercase
 **/

$(function () {

    //Server session variables
    var sessionId;

    // Setup Pen
    var canvas = document.getElementById('board')
    var ctx = canvas.getContext("2d");
    ctx.lineCap = "round";
    ctx.lineJoin = "round";
    ctx.strokeStyle = "#000";
    ctx.lineWidth = 1;

    // Default Variables
    var docLoaded = false, hubConnected = false, canvasInitialized = false
    var penWidth = 1;
    var penColor = '#000';
    var oldX, oldY;
    

    //Get Hub Instance
    var hub = $.connection.Whiteboard
    hub.logging = true;

    /**
     * Canvas Events
     * */
    function canvasEvents() {
        if ((docLoaded && hubConnected) && !canvasInitialized) {
            // Mouse only control, you cant draw if mosedown is outside canvas nor moving the mouse over unclicked
            var clicked = 0;

            // Common mousedown
            function start(e) {
                clicked = 1;
                var pos = getMousePos(canvas, e);
                oldX = pos.x;
                oldY = pos.y;
                DrawMove([oldX, oldY, oldX - 1, oldY - 1, penWidth], penColor);
                hub.server.drawMove(sessionId, penColor, [oldX, oldY, oldX - 1, oldY - 1, penWidth]);
            };
            function touchStart(e) {
                var touchEvent = e.originalEvent.changedTouches[0];
                e.preventDefault();
                oldX = touchEvent.clientX - touchEvent.target.offsetLeft;
                oldY = touchEvent.clientY - touchEvent.target.offsetTop;
                DrawMove([oldX, oldY, oldX - 1, oldY - 1, penWidth], penColor);
                hub.server.drawMove(sessionId, penColor, [oldX, oldY, oldX - 1, oldY - 1, penWidth]);
            };

            // Common mousemove
            function move(e) {
                if (clicked) {
                    var pos = getMousePos(canvas, e);
                    x = pos.x;
                    y = pos.y;
                    DrawMove([oldX, oldY, x, y, penWidth], penColor);
                    hub.server.drawMove(sessionId, penColor, [oldX, oldY, x, y, penWidth]);
                    oldX = x;
                    oldY = y;
                }
            };
            function touchMove(e) {
                var touchEvent = e.originalEvent.changedTouches[0];
                e.preventDefault();
                x = touchEvent.clientX - touchEvent.target.offsetLeft;
                y = touchEvent.clientY - touchEvent.target.offsetTop;
                DrawMove([oldX, oldY, x, y, penWidth], penColor);
                hub.server.drawMove(sessionId, penColor, [oldX, oldY, x, y, penWidth]);
                oldX = x;
                oldY = y;
            };

            // Mouse only control
            function stop(e) {
                clicked = 0;
            };

            // Canvas EventListeners
            canvas.addEventListener("mousedown", start, false);
            canvas.addEventListener("mousemove", move, false);
            $(canvas).on("touchstart", touchStart);
            $(canvas).on("touchmove", touchMove);
            $(this).on("mouseup", stop);
        }
    }

    $.connection.hub.start().done(function () {
        hubConnected = true;

        initDrawboard();
        canvasEvents();

        //Create new whiteboard session
        hub.server.startNewWhiteboardGroup("test", $('#TeamID').val());
    })

    hub.client.drawMove = function (p, color) {
        DrawMove(p, color);
    }

    hub.client.createdWhiteboardSessionComplete = function (_sessionId) {
        sessionId = _sessionId;
    }

    function getMousePos(canvas, evt) {
        var rect = canvas.getBoundingClientRect(),
            scaleX = canvas.width / rect.width,  
            scaleY = canvas.height / rect.height; 

        return {
            x: (evt.clientX - rect.left) * scaleX,  
            y: (evt.clientY - rect.top) * scaleY
        }
    }

    function getRandomColor() {
        var letters = '0123456789ABCDEF';
        var color = '#';
        for (var i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return color;
    }

    function initDrawboard() {
        ctx.beginPath();
        ctx.fillStyle = 'white';
        ctx.rect(0, 0, canvas.width, canvas.height);
        ctx.fill();
    }

    function DrawMove(p, color) {
        ctx.beginPath();
        ctx.moveTo(p[0], p[1])
        ctx.lineWidth = p[4];
        ctx.strokeStyle = color;
        ctx.lineTo(p[2], p[3]);
        ctx.stroke();
    }

    $(document).ready(function () {
        docLoaded = true;
        canvasEvents();
    })

})

function requestFullScreen() {
    const canvas = document.getElementById('board');
    if (canvas.requestFullscreen) {
        canvas.requestFullscreen();
    } else if (canvas.mozRequestFullScreen) { /* Firefox */
        canvas.mozRequestFullScreen();
    } else if (canvas.webkitRequestFullscreen) { /* Chrome, Safari and Opera */
        canvas.webkitRequestFullscreen();
    } else if (canvas.msRequestFullscreen) { /* IE/Edge */
        canvas.msRequestFullscreen();
    }
}

