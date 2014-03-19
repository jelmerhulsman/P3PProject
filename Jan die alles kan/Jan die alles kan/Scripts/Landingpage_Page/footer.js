$(document).ready(function () {
    $('#footer .open').click(function () {
        $('#footer').animate({ 'height': '140px' }, 300).css('bottom', 0);

        $('#left, #right').show()
        $('#footer .floating').hide();

        $('.close').show();
        $('.open').hide();
    });

    $('#footer .close').click(function () {
        $('#footer').animate({ 'height': '0px' }, 300).css('bottom', 0);
        setTimeout(function () {
            $('#left, #right').hide()
            $('#footer .floating').show();
        }, 600);
        $('.close').hide();
        $('.open').show();
    });
});