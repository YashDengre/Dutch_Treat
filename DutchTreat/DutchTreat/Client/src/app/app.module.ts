import { IterableDiffers, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { Store } from './services/store.service';
import ProductListView from './views/productListView.component';
import { CartView } from './views/cartView.component';
import router from './router';
import { ShopPage } from './pages/shopPage.component';
import { Checkout } from './pages/checkout.component';
import { DataService } from './services/dataService.service';

@NgModule({
    declarations: [
        AppComponent,
        ProductListView, // To use this ProductListView.Component in our page we are including this here
        CartView,
        ShopPage,
        Checkout],
    imports: [
        BrowserModule,
        HttpClientModule,
        router
    ],
    providers: [
        Store,
        DataService],
    bootstrap: [AppComponent] //it says we first render the AppComponent which is in app folder 
})
export class AppModule { }

//Error: No base href set.Please provide a value for the APP_BASE_HREF token or
//add a base element to the document.
//This error comes and it basically asking us to speficy for app base href- basic element - which means where is our application is running
//There are two ways to solve
//Router - we have to use an option useHash: true -> it will use a # before the router