$(document).ready(function () {
    StyleLeftMenu();
});

$(window).resize(function () {
    if ($('#imageOverlay img').hasClass('landscape')) {
        $('#imageOverlay').css('height', $('#boxOverlay').height() * 0.7 - 100);
        $('#boxOverlay .bottom').css('height', $('#boxOverlay').height() * 0.3 + 100);
    } else {
        $('#imageOverlay').css('height', $('#boxOverlay').height() * 0.7);
        $('#boxOverlay .bottom').css('height', $('#boxOverlay').height() * 0.3);
    }

    $('#imageOverlay img').height($('#imageOverlay').height());

    FixThumbnails();
    StyleLeftMenu();
});

function FixThumbnails() {
    var heightImgBox = $('#photoOverview li').outerHeight();
    $('#photoOverview img').each(function () {
        $(this).height(heightImgBox);
        if ($('#photoOverview li').width() < $(this).width()) {
            $(this).css('margin-left', -($(this).width() - $('#photoOverview li').width()) / 2);
        } else {
            $(this).width($('#photoOverview li').width());
            $(this).css('height', 'auto');
        }
    })
}

function StyleLeftMenu() {
    $('#filtersContainer').css("height", $('body').height());
    $('.stroke').css("height", $('body').height());
    $('#main .top .inner, #main .top .bottom').css('max-width', $(window).width() - 280);
}