var Shopper = /** @class */ (function () {
    //firstName = ""; //type defined
    //lastName = "";//type defined
    //comment the above field in order to use the below shortcut method - means after shortcut these prop definition will be optional to us
    //constructor(first, last) { //but this will allow user to pass any type -we have to restrit the user for specific type
    //    this.firstName = first;
    //    this.lastName = last;
    //} 
    //constructor(first:string, last:string) { //but this will allow user to pass any type -we have to restrit the user for specific type
    //    this.firstName = first;
    //    this.lastName = last;
    //}
    //shortcut for these propertiese
    // remove the property and use direct in constructor
    function Shopper(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
    }
    Shopper.prototype.showName = function () {
        alert(this.firstName + " " + this.lastName);
        console.log('${ this.firstName } ${ this.lastName }');
    };
    return Shopper;
}());
//# sourceMappingURL=shopper.js.map