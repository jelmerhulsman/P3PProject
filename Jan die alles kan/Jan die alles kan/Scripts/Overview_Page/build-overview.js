$(document).ready(function () {
    $('select[name="sortBy"]').blur(function () {
        var pictures = "";
        $('#photoOverview li').each(function () {
            if (typeof($(this).attr('id')) != 'undefined') {
                pictures += $(this).attr('id') + ',';
            }
        });

        pictures = pictures.substr(0, pictures.length - 1);

        $.post("http://localhost:52802/Ajax/OrderPhotos", { order: $(this).val(), pictures: pictures }, function (data) {
            buildOverview(data);
        });
    })

    var QueryString = function () {
        // This function is anonymous, is executed immediately and 
        // the return value is assigned to QueryString!
        var query_string = {};
        var query = window.location.search.substring(1);
        var vars = query.split("&");
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            // If first entry with this name
            if (typeof query_string[pair[0]] === "undefined") {
                query_string[pair[0]] = pair[1];
                // If second entry with this name
            } else if (typeof query_string[pair[0]] === "string") {
                var arr = [query_string[pair[0]], pair[1]];
                query_string[pair[0]] = arr;
                // If third or later entry with this name
            } else {
                query_string[pair[0]].push(pair[1]);
            }
        }
        return query_string;
    }();

    var searchTerm = "";

    if (typeof(QueryString.search) != "undefined") {
        searchTerm = QueryString.search;
    }

    $.post("http://localhost:52802/Ajax/GetPhotos", { searchTerm: searchTerm }, function (data) {
        buildOverview(data);
    });
});

function buildOverview(pictures) {
    if ($('#photoOverview').length == 1) {
        $('#photoOverview').remove();
    }

    var html = '<ul id="photoOverview">';
    var pCounter = 1; // Photo counter
    var bulCounter = 1; // begin ul counter
    var eulCounter = 4; // end ul counter
    var Class = "";
    pictures.forEach(function (item) {
        Class = item.Color;

        if (pCounter % 4 == 0) {
            Class += " last";
        }

        if (pCounter <= 4) {
            Class += " toprow";
        }

        if (pCounter == bulCounter) {
            html += '<ul>';
            bulCounter += 4;
        }
        html += '<li id="' + item.Id + '" class="photo ' + Class + '">';
        html += '<img src="../../Images/Categories/' + item.Category + '/Thumbnails/' + item.File_name + '" alt="" />';

        //<li class="'+ Class +'">
        //<img src="../../Images/Categories/'+ item.Category +'/Thumbnails/'+ item.File_name +'" alt="Image not Found" onError="this.onerror=null;this.src='../../Images/imageNotFound.jpg';"/>

        html += '<div class="description">';
        html += '<h3>' + item.Name + '</h3>';
        html += '<p>Category: ' + item.Category + '</p>';
        html += '<span class="price">&euro; ' + item.Price + '</span>';
        html += '</div>';
        html += ' </li>';

        if (pCounter == eulCounter) {
            html += '</ul><li class="clear"></li>';
            eulCounter += 4;
        }
        pCounter++;
    });

    $('#photos').prepend(html + '</ul>');

    // Foto openen in overlay
    $('#photos li.photo').click(function () {
        $.post("http://localhost:52802/Ajax/PhotoInfo", { id: $(this).attr('id') }, function (data) {
            // Data van foto plaatsen in overlay
            $('#overlay .title').html(data.Name);
            $('#overlay .description').html(data.Description);
            $('#overlay .price').html('&euro; ' + data.Price);
            $('#addToCartButton').attr('class', data.Id);
            $('#imageOverlay').html('<img src="../../Images/Categories/' + data.Category + '/Previews/' + data.File_name + '" alt="" />');

            // Bepalen of de foto landscape of portrait is
            // Afbeelding hoogte bepalen en blok, met informatie, hoogte bepalen
            if ($('#imageOverlay img').height() < $('#imageOverlay img').width()) {
                $('#imageOverlay').css('height', $('#boxOverlay').height() * 0.7 - 100);
                $('#boxOverlay .bottom').css('height', $('#boxOverlay').height() * 0.3 + 100);
            }
            else {
                $('#imageOverlay').css('height', $('#boxOverlay').height() * 0.7);
                $('#boxOverlay .bottom').css('height', $('#boxOverlay').height() * 0.3);
            }

            $('#imageOverlay img').height($('#imageOverlay').height());
        });
        $('#overlay').fadeIn();
    });

    FixThumbnails();
    StyleLeftMenu();
}