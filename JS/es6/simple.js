for(let i = 100; i < 3; i++) {
    console.log(i)
}

var [foo, [[bar], baz]] = [1, [[2], 3]];
console.log(foo)
console.log(bar)
console.log(baz)
