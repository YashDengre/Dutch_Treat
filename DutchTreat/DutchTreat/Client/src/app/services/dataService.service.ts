import { Injectable } from "@angular/core";
import { Store } from "./store.service";

@Injectable() //in order to make this injectable and to access this every where we have to told explictly that we are going to provide this -
//so we have to put this in App Module - provider {dependency injection}
export class DataService {
    public CartInfo: CheckoutInfo = new CheckoutInfo();
    constructor(public data: Store) {
        let order = data.order;
        this.CartInfo.Order =  data;
        this.CartInfo.ShippingCharge = 70;
        this.CartInfo.Distance = 50;

    }
   
     
    }

export class CheckoutInfo {
    public Address: string;
    public Pin: string;
    public Distance: number;
    public ShippingCharge: number;
    public Order: Store;

}


   
         