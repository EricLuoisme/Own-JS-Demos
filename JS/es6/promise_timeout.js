function timeout(ms) {
    return new Promise((resolve, reject) => {
        // handle resolve
        setTimeout(resolve, ms);
    })
}

timeout(3000).then(() => {
    console.log('done');
})