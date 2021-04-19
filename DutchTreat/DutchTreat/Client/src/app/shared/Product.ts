export class Product {
    //using this for type safety for products -  using typscript power which is type safety.
    //open the api in browser -open the raw json and generated the typescript code through json2ts
    id: number;
    category: string;
    size: string;
    price: number;
    title: string;
    artDescription: string;
    artDating: string;
    artId: string;
    artist: string;
    artistBirthDate: Date;
    artistDeathDate: Date;
    artistNationality: string;
}