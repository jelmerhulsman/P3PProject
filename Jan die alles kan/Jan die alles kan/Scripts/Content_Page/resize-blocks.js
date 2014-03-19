$(document).ready(function () {
    $('#top .inner').css('max-width', $(window).width() - 20);
    $('#content .right').css('max-width', $('#content').width() - 300);
});

$(window).resize(function () {
    $('#top .inner').css('max-width', $(window).width() - 20);
    $('#content .right').css('max-width', $('#content').width() - 300);
});