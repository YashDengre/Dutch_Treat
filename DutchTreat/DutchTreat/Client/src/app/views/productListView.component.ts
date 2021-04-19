import { Component, OnInit } from "@angular/core";
import { Store } from "../services/store.service";

@Component(
    {
        selector:"product-list",
        templateUrl:"productListView.component.html"
    })
export default class ProductListView implements OnInit {
    //public products = [
    //    {
    //        title: "Van Gogh Mug",
    //        price: "19.99"
    //    },
    //    {
    //        title: "Van Gogh Poster",
    //        price: "29.99"
    //    }]; //moved this into service - store
   // public products = [];
    constructor(public store: Store) {
       // this.products = store.products;
    }
    ngOnInit(): void {
        this.store.loadProducts()
            .subscribe(() => { } //we can use this as this will return nothing; -store service.ts related
               //but if we use val => { } will give us issue;
            ); //<- kicks off the operations

    }

}