/**
 * Note Server Functions Called With Lowercase
 **/

$(function () {

    // Setup Pen
    var canvas = document.getElementById('board')
    var ctx = canvas.getContext("2d");
    ctx.lineCap = "round";
    ctx.lineJoin = "round";
    ctx.strokeStyle = "#000";
    ctx.lineWidth = 1;

    // Default Variables
    var docLoaded = false, hubConnected = false, canvasInitialized = false
    var penWidth = 1; penColor = 0;
    var xOffset, yOffset;
    var colors = ["#000", "#f00", "#ff7f00", "#ff0", "#0f0", "#00f", "#4b0082", "#8f00ff", "#fff"];

    //Get Hub Instance
    var hub = $.connection.Whiteboard

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
                xOffset = e.offsetX;
                yOffset = e.offsetY;
                DrawMove([xOffset, yOffset, xOffset - 1, yOffset - 1, penWidth, penColor]);
                hub.server.drawMove([xOffset, yOffset, xOffset - 1, yOffset - 1, penWidth, penColor]);
            };
            function touchStart(e) {
                var touchEvent = e.originalEvent.changedTouches[0];
                e.preventDefault();
                xOffset = touchEvent.clientX - touchEvent.target.offsetLeft;
                yOffset = touchEvent.clientY - touchEvent.target.offsetTop;
                DrawMove([xOffset, yOffset, xOffset - 1, yOffset - 1, penWidth, penColor]);
                hub.server.drawMove([xOffset, yOffset, xOffset - 1, yOffset - 1, penWidth, penColor]);
            };

            // Common mousemove
            function move(e) {
                if (clicked) {
                    x = e.offsetX;
                    y = e.offsetY;
                    DrawMove([xOffset, yOffset, x, y, penWidth, penColor]);
                    hub.server.drawMove([xOffset, yOffset, x, y, penWidth, penColor]);
                    xOffset = x;
                    yOffset = y;
                }
            };
            function touchMove(e) {
                var touchEvent = e.originalEvent.changedTouches[0];
                e.preventDefault();
                x = touchEvent.clientX - touchEvent.target.offsetLeft;
                y = touchEvent.clientY - touchEvent.target.offsetTop;
                DrawMove([xOffset, yOffset, x, y, penWidth, penColor]);
                hub.server.drawMove([xOffset, yOffset, x, y, penWidth, penColor]);
                xOffset = x;
                yOffset = y;
            };

            // Mouse only control
            function stop(e) {
                clicked = 0;
            };

            // Canvas EventListeners
            $(canvas).on("mousedown", start);
            $(canvas).on("mousemove", move);
            $(canvas).on("touchstart", touchStart);
            $(canvas).on("touchmove", touchMove);
            $(this).on("mouseup", stop);
        }
    }

    $.connection.hub.start().done(function () {
        hub.server.startUp("Hello World");
        hubConnected = true;

        canvasEvents();
    })

    hub.client.DrawMove = function (p) {
        DrawMove(p);
    }

    function DrawMove(p) {
        ctx.beginPath();
        ctx.moveTo(p[0], p[1])
        ctx.lineWidth = p[4];
        ctx.strokeStyle = colors[p[5]];
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

