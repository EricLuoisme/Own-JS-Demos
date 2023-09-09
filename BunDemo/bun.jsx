const server = Bun.serve({
    port: 3030,
    fetch(request) {
        return new Response("Hello, Bun!");
    },
});

console.log(`Listening on localhost:${server.port}`)