function resizeField() {
    var formWidth = $('#main form').width();
    var screenWidth = $(window).width();

    if (screenWidth >= 600)
        $('#searchField').width(formWidth - 150);
    else
        $('#searchField').width(formWidth);
};

$(document).ready(function () {
    resizeField();
});

$(window).resize(function () {
    resizeField();
});

