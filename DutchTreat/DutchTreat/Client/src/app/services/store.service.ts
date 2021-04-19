import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Product } from "../shared/Product";

@Injectable() //in order to make this injectable and to access this every where we have to told explictly that we are going to provide this -
    //so we have to put this in App Module - provider {dependency injection}
export class Store {
    constructor(private http: HttpClient) {

    }
    public products: Product[] = []; //strongly typed byt shared/product.ts class
        //[{
        //    title: "Van Gogh Mug",
        //    price: "19.99"
        //},  
        //{
        //    title: "Van Gogh Poster",
        //    price: "29.99"
        //}]; 

    loadProducts(): Observable<void> {
        return this.http.get<[]>("/api/product").pipe(map(data => { //use generic <[]> to tell product is array type
            this.products = data;
            return;
        }));
    }
}