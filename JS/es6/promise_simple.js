
// Creating a new Promise
const myPromise = new Promise((resolve, reject) => {
    setTimeout(() => {
        let num = Math.random();
        console.log(num);
        if (num > 0.5) {
            resolve('The operation success');
        } else {
            reject(new Error('Something went wrong'));
        }
    }, 2000);
});

// Using the Promise
myPromise
    .then((successMsg) => {
        console.log('Success:', successMsg);
    })
    .catch((error) => {
        console.log('Error:', error.message);
    });