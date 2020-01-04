let socket = null;

onmessage = function (e) {
    if (e.data.password && e.data.hostname) {
        //Start websocket
        console.log("starting websocket");
        connectToWebsocket(e.data.hostname, e.data.password);
    } else if (e.data.blob) {
        sendBlob(e.data.blob);
    }
}


function connectToWebsocket(hostname, password) {
    socket = new WebSocket("ws://" + hostname + ":9000");

    socket.onopen = function (e) {
        console.log("[open] Connection established");

        //Handshake
        handshake(socket, password);
    };

    socket.onmessage = function (event) {
        console.log(`[message] Data received from server: ${event.data}`);
    };

    socket.onclose = function (event) {
        if (event.wasClean) {
            console.log(`[close] Connection closed cleanly, code=${event.code} reason=${event.reason}`);
        } else {
            console.log('[close] Connection died');
        }
    };

    socket.onerror = function (error) {
        console.log(`[error] ${error.message}`);
    };
}

function handshake(socket, password) {
    socket.send(password);
}

function sendBlob(blob) {
    socket.send('BLOB');
    socket.send(blob);
}

