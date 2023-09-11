const server = Bun.serve({
    port: 3030,
    fetch(request) {
        // return new Response("Hello, Bun!");
        throw new Error("woops!")
    },
    error(error) {
        return new Response(`<pre>${error}\n${error.stack}</pre>`, {
            headers: {
                "Content-Type": "text/html"
            }
        })
    }
});

console.log(`Listening on localhost:${server.port}`)