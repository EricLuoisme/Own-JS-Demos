// interface
interface IPerson {
    readonly id: number, // read only type
    name: string;
    age?: number; // then age's val is not compulsory
}

// obj
let tom: IPerson = {
    id: 1,
    name: 'Tom'
}
console.log(tom)


// array
let fibonacci: Array<number> = [1, 2, 3];
fibonacci.push(6);

// func
let mySum: (x: number, y: number) => number = function (x: number, y: number): number {
    return x + y;
}

// func interface
interface SearchFunc {
    (source: string, subString: string, extra?: string): boolean;
}

// is some interface
let mySearch: SearchFunc;

// than implement
mySearch = function (source: string, subString: string) {
    return source.search(subString) !== -1;
}