$(document).ready(function () {
    $('#top .inner').css('max-width', $(window).width() - 20);
    $('#content .right').css('max-width', $('#content').width() - 300);
    $('#main .top .inner, #main .top .bottom').css('max-width', $(window).width() - 20);
});

$(window).resize(function () {
    $('#top .inner').css('max-width', $(window).width() - 20);
    $('#content .right').css('max-width', $('#content').width() - 300);
    $('#main .top .inner, #main .top .bottom').css('max-width', $(window).width() - 20);
});