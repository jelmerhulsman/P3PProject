$(document).ready(function () {
    $('.handler.close').click(function () {
        if ($(this).hasClass('open')) {
            $($(this)[0].parentNode).animate({ height: '17px' }, 300);
            $(this).next().fadeOut(300);
            $(this).addClass('close').removeClass('open');
        } else {
            $($(this)[0].parentNode).animate({ height: $($(this)[0].parentNode).height() + $(this).next().height() + 20 }, 300);
            $(this).next().fadeIn(300);
            $(this).addClass('open').removeClass('close');
        }
    });

    $('.color').hover(function () {
        $(this).prev().stop().show().animate({ opacity: 1 }, 300);
    }, function () {
        $(this).prev().stop().hide().animate({ opacity: 0 }, 300);
    });

    $('.color').click(function () {
        $(this).toggleClass('clicked');
    })

    $('.tooltip').hide();

    $('.categoryHolder .checkbox, .categoryHolder .label, .orientationHolder .checkbox, .orientationHolder .label').click(function () {
        if ($(this).hasClass('label')) {
            $(this).prev().toggleClass('clicked');
        } else {
            $(this).toggleClass('clicked');
        }
    })

    $("#slider-range").slider({
        range: true,
        min: 0,
        max: 30,
        values: [0, 15],
        slide: function (event, ui) {
            $("#min").text("€" + ui.values[0]);
            $("#max").text("€" + ui.values[1]);
        }
    });

    $("#min").text("€" + $("#slider-range").slider("values", 0));
    $("#max").text("€" + $("#slider-range").slider("values", 1));

    $('button.search').click(function () {
        var filterName = $('#filters input[name="keyword"]').val();

        var filterColors = "";
        $('.colorHolder li .color').each(function () {
            if ($(this).hasClass('clicked')) {
                var color = $(this).attr('class');
                color = color.split(' ');
                color = color[1];
                filterColors += color + ",";
            };
        });

        filterColors = filterColors.substr(0, filterColors.length - 1);

        var filterOrientation = "";
        $('.orientationHolder li .checkbox').each(function () {
            if ($(this).hasClass('clicked')) {
                filterOrientation += $(this).next().text() + ",";
                
            };
        });
        
        filterOrientation = filterOrientation.substr(0, filterOrientation.length - 1);

        var filterCategories = "";
        $('.categoryHolder li .checkbox').each(function () {
            if ($(this).hasClass('clicked')) {
                filterCategories += $(this).next().text() + ",";
            };
        });

        filterCategories = filterCategories.substr(0, filterCategories.length - 1);

        var priceRange = $("#min").text().replace('€', '') + "," + $("#max").text().replace('€', '');

        $.post("http://localhost:52802/Ajax/Filter", { name: filterName, colors: filterColors, orientation: filterOrientation, categories: filterCategories, pricerange: priceRange }, function (data) {
            console.log(data);

            if (!data) {
                alert("No images were found with the provided filters. Please try another filter or combination of filters.");
            } else {
                buildOverview(data);
            }
        });
    });

    $('button.cancel').click(function () {
        window.location = "http://localhost:52802/Page/Overview";
    })
});