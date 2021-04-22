import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Order, OrderItem } from "../shared/Order";
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
    public order: Order = new Order();
    loadProducts(): Observable<void> {
        return this.http.get<[]>("/api/product").pipe(map(data => { //use generic <[]> to tell product is array type
            this.products = data;
            return;
        }));
    }

    addToOrder(product: Product) {

        let item: OrderItem;
        item = this.order.items.find(o => o.productId == product.id);
        if (item) {
            item.quantity++;
        }
        else {
            item = new OrderItem();
            item.productId = product.id;
            item.productTitle = product.title;
            item.productArtId = product.artId;
            item.productArtist = product.artist;
            item.productCategory = product.category;
            item.productSize = product.size;
            item.unitPrice = product.price;
            item.quantity = 1; //initial making it as 1 always
            this.order.items.push(item);

        }


    }
}