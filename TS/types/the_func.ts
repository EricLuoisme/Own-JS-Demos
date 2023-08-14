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