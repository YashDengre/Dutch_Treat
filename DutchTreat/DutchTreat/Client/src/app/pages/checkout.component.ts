import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { DataService } from "../services/dataService.service";
import { Store } from "../services/store.service";

@Component({
    selector: "checkout",
    templateUrl: "checkout.component.html",
    styleUrls: ['checkout.component.css']
})
export class CheckoutPage {

    public errorMessage = "";
    constructor(public data: DataService, public store:Store, private router:Router) {
        
    }

    onCheckout() {
        // TODO
        // alert("Doing checkout")
        this.errorMessage = "";
        this.store.checkout()
        .subscribe(()=>
        {
            this.router.navigate(["/"]);
        }, error=>{this.errorMessage = `Failed to checkout: ${error}`}
        );
}
}