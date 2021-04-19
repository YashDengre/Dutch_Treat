import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { Store } from './services/store.service';
import ProductListView from './views/productListView.component';

@NgModule({
  declarations: [
        AppComponent,
        ProductListView // To use this ProductListView.Component in our page we are including this here
  ],
  imports: [
      BrowserModule,
      HttpClientModule
  ],
    providers: [
        Store],
  bootstrap: [AppComponent] //it says we first render the AppComponent which is in app folder 
})
export class AppModule { }
