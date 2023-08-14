
// boolean
let createByNewBoolean: boolean = true;

// number
let decLiteral: number = 5;

// string
let myName: string = 'Tom';

// any type -> no more strict type
let favoriteNum: any = 'seven';
console.log(favoriteNum);

favoriteNum = 32;
console.log(favoriteNum);

// union type -> combine multiple types
let numberAgain: string | number;
numberAgain = '1234';
numberAgain = 1234

// union type input -> could only call the param they both have
function getString(something: string | number): string {
    return something.toString();
}


console.log(createByNewBoolean);
console.log(decLiteral);
console.log(myName);
