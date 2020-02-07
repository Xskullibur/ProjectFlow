/**
 * Note Server Functions Called With Lowercase
 **/

// Default Variables
var docLoaded = false, hubConnected = false, canvasInitialized = false
var penWidth = 1;
var penColor = '#000';
var oldX, oldY;
 
//Server session variables
var sessionId;
var connected_users = [];

//Get Hub Instance
var hub = $.connection.Whiteboard

$(document).ready(function () {

    // Setup Pen
    var canvas = document.getElementById('board')
    var ctx = canvas.getContext("2d");
    ctx.lineCap = "round";
    ctx.lineJoin = "round";
    ctx.strokeStyle = "#000";
    ctx.lineWidth = 1;

    /**
     * Canvas Events
     * */
    function canvasEvents() {
        if ((docLoaded && hubConnected) && !canvasInitialized) {
            // Mouse only control, you cant draw if mosedown is outside canvas nor moving the mouse over unclicked
            var clicked = 0;

            // Common mousedown
            function start(e) {
                if (window.fullscreen) return;

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

    hub.client.drawMove = function (p, color) {
        DrawMove(p, color);
    }

    hub.client.createdWhiteboardSessionComplete = function (_sessionId) {
        sessionId = _sessionId;
    }

    hub.client.joinWhiteboardSessionComplete = function (listOfPoints, editingUsers) {
        for (var i = 0; i < listOfPoints.length; i++) {
            var point = JSON.parse(listOfPoints[i]);
            DrawMove(point.points, point.strokeColor);
        }

        for (var i = 0; i < editingUsers.length; i++) {
            addUserName(editingUsers[i]);
        }

    }

    function addUserName(name) {
        var nospace_name = name.replace(/\s/g, '');
        $('#connected_users').append(`<span id='user-${nospace_name}'>${name}</span>&nbsp;`);
    }

    hub.client.alertUserJoin = function (name) {
        addUserName(name);
        $.toast({
            title: 'Alert!',
            content: name + ' has Joined',
            type: 'info',
            delay: 3000,
            pause_on_hover: false
        });
    }

    hub.client.alertUserLeave = function (name) {
        var nospace_name = name.replace(/\s/g, '');
        $(`#user-${nospace_name}`).remove();
        $.toast({
            title: 'Alert!',
            content: name + ' has Left',
            type: 'danger',
            delay: 3000,
            pause_on_hover: false
        });
    }

    hub.client.whiteboardSaveSuccessful = function () {
        $.toast({
            title: 'Saved!',
            content: 'Board Successfully Saved',
            type: 'success',
            delay: 3000,
            pause_on_hover: false
        });
    }

    //Errors

    hub.client.illegalAccess = function () {
        $.toast({
            title: 'Illegal Access!',
            content: 'You are not invited!',
            type: 'danger',
            delay: 3000,
            pause_on_hover: false
        });
    }

    hub.client.unableToReadSessionId = function () {
        $.toast({
            title: 'Oops!',
            content: 'Something has gone wrong, please try again.',
            type: 'danger',
            delay: 3000,
            pause_on_hover: false
        });
    }

    hub.client.clear = function () {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
    }

    $.connection.hub.start().done(function () {
        hubConnected = true;

        initDrawboard();
        canvasEvents();

        //Create new whiteboard session
        const urlParams = new URLSearchParams(window.location.search);
        if (urlParams.get('Action') === 'Create') {
            const groupName = urlParams.get('GroupName');
            if (groupName != null) {
                hub.server.startNewWhiteboardGroup(groupName, $('#TeamID').val());
            }

        } else if (urlParams.get('Action') === 'Join') {
            const sessionID = urlParams.get('SessionID');
            if (sessionID != null) {
                sessionId = sessionID;
                hub.server.joinWhiteboardGroup(sessionID);
            }
        }
    })

    

    //End of errors

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


    window.onbeforeunload = function () {
        
        hub.server.leaveWhiteboardGroup();
        window.onbeforeunload = null;
    }

})

function clearCanvas() {
    hub.server.clear(sessionId);
}

function saveCanvas() {
    hub.server.save(sessionId);
}

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

function changeColor(color) {
    penColor = color;
}

function changeSize(size) {
    penWidth = size
}

$(document).on('hidden.bs.toast', '.toast', function (e) {
    $(this).remove();
});