$(document).ready(function () {
    var x = 0;
    var s = "Hello";
    //alert("Hello Yash Dengre");
    console.warn("Hello Yash Dengre");

    //var theForm = document.getElementById("theForm");
    //theForm.hidden = true;
    var theForm = $("#theForm");
    theForm.hide();

    //var button = document.getElementById("buyBtn");
    //button.addEventListener("click", function () { console.log("Buying Item") })
    var button = $("#buyBtn");
    button.on("click", function () { console.log("Buying Item") });

    //var prdInfo = document.getElementsByClassName("product-props");
    //var listItem = prdInfo.item[0].chislren;


    var prdInfo = $(".product-props li");
    prdInfo.on("click", function () {
        console.log("You Clicked on " + $(this).text());
        this.innerHTML = this.innerHTML + " Clicked!";

    });


    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");

    $loginToggle.on("click", function () {
        $popupForm.fadeToggle(200);
    })


});