import { Injectable } from "@angular/core";
import { Store } from "./store.service";

@Injectable() //in order to make this injectable and to access this every where we have to told explictly that we are going to provide this -
//so we have to put this in App Module - provider {dependency injection}
export class DataService {
    constructor(public data: Store) {

    }
   
     
    }

   
         