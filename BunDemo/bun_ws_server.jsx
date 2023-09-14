const server = Bun.serve({
    port: 9887,
    fetch(req, server) {
        if (server.upgrade(req)) {
            return; // do not return a Response
        }
        return new Response("Upgrade failed :(", { status: 500 });
    },
    websocket: {
        message(ws, message) {
            console.log(`Received: ${message}`);
            ws.send(message);
        },
    },
});

console.log(`Listening on localhost:${server.port}`);