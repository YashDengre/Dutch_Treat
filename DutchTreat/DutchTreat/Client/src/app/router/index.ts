import { RouterModule } from "@angular/router";
import { CheckoutPage } from "../pages/checkout.component";
import { LoginPage } from "../pages/loginPage.component";
import { ShopPage } from "../pages/shopPage.component";
import { AuthActivator } from "../services/authActivator.service";

const routes = [
    { path: "", component: ShopPage },
    { path: "checkout", component: CheckoutPage, canActivate: [AuthActivator] },
    { path: "login", component: LoginPage },
    { path: "**", redirectTo: "/" }
     // Above-> [Fall back route:"**"] [ "/": Default Page] This particular route means if none of the other routes works
     // then please execute this once and it iwll simply call the default page which is shop page

];

const router = RouterModule.forRoot(routes, { useHash: false });
export default router;


//Error: No base href set.Please provide a value for the APP_BASE_HREF token or
//add a base element to the document.
//This error comes and it basically asking us to speficy for app base href- basic element - which means where is our application is running
//There are two ways to solve
//Router - we have to use an option useHash: true -> it will use a # before the router
//Second one is -  use base href in the page where it will live on - > see shop.cshtml  and false the useHash