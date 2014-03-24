$(document).ready(function () {
    $("#overlay").hide();

    $("#overlay, #closeOverlay").click(function () {
        $("#overlay").fadeOut();
    });

    $("#boxOverlay").click(function (event) {
        event.stopPropagation();
    });

    $("#shoppingCartOverlay").hide();

    $(".shopping").click(function () {
        $("#shoppingCartOverlay").slideToggle();
    });

    $("#shoppingCartOverlay").click(function (event) {
        event.stopPropagation();
    });

    setCartText();
});