$(document).ready(function () {
    $('select[name="sortBy"]').change(function () {
        var pictures = "";
        $('#photoOverview li').each(function () {
            if (typeof($(this).attr('id')) != 'undefined') {
                pictures += $(this).attr('id') + ',';
            }
        });

        pictures = pictures.substr(0, pictures.length - 1);

        $.post("http://localhost:52802/Ajax/OrderPhotos", { order: $(this).val(), pictures: pictures }, function (data) {
            if (!data) {
                alert("There are no images to sort.");
            } else {
                buildOverview(data);
            }
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
        $('input[name="keyword"]').val(searchTerm);
        console.log(data);
        if (!data) {            
            alert("No images were found with the provided search request. Please try again.");
        } else {
            buildOverview(data);
        }
    });
});

function buildPagination() {
    $('#pagination .left, #pagination .center, #pagination .right').remove();
    $('#pagination').append('<div class="left"></div><div class="center"></div><div class="right"></div>');
    // Pagination
    var maxInCollections = $('.collection_1').length;
    var collections = Math.ceil($('.collection').length / maxInCollections);

    // Show page 1
    $('.pageIndicator').text('');
    if ($('.collection_2').length != 0) {
        $('.collection').each(function () {
            if (!$(this).hasClass('collection_1')) {
                $(this).hide();
            }
        })

        // Create buttons
        if ($('.paginationBtn_prev').length == 0) {
            $('#pagination .left').append('<a class="paginationBtn_prev" href="javascript:void(0);">&lt;</a>');
        }
        for (var i = 1; i <= collections; i++) {
                if (i == 1) {
                    $('#pagination .center').append('<a class="paginationBtn_' + i + ' active" href="javascript:void(0);" onclick="showPage(' + i + ')">' + i + '</a>');
                } else if (collections < 7) {
                    $('#pagination .center').append('<a class="paginationBtn_' + i + '" href="javascript:void(0);" onclick="showPage(' + i + ')">' + i + '</a>');
                } else if (i < 4) {
                    $('#pagination .center').append('<a class="paginationBtn_' + i + '" href="javascript:void(0);" onclick="showPage(' + i + ')">' + i + '</a>');
                } else if (i == 4) {
                    $('.paginationBtn_x').remove();
                    $('#pagination .center').append('<a class="paginationBtn_x" href="javascript:void(0);" onclick="jumpTo()">...</a>');
                } else if (collections - 3 < i) {
                    $('#pagination .center').append('<a class="paginationBtn_' + i + '" href="javascript:void(0);" onclick="showPage(' + i + ')">' + i + '</a>');
                }
        }
        if ($('.paginationBtn_next').length == 0) {
            $('#pagination .right').append('<a class="paginationBtn_next" onclick="showPage(2)" href="javascript:void(0);">&gt;</a>');
        }
        $('.pageIndicator').text('Pagina: 1 / ' + collections);

        $('#paginationBtns').append($('.resultsPerPage'));
    }

    // Results per page functionality
    $('.resultsPerPage').hover(function () {
        $('.resultsPerPage li').toggle();
    }, function () {
        $('.resultsPerPage li').toggle();
    });
}

// Paginations buttons function
function showPage(page) {
    if (!$('.paginationBtn_' + page).hasClass('active')) {

        $('.active').removeClass('active');
        $('.paginationBtn_' + page).addClass('active');

        if (page - 1 != -1) {
            var check = page;
            if (check - 1 == 0) {
                $('.paginationBtn_prev').attr('onclick', 'showPage(1)');
            } else {
                $('.paginationBtn_prev').attr('onclick', 'showPage(' + Math.ceil(page - 1) + ')');
            }
        }

        var maxInCollections = $('.collection_1').length;
        var collections = Math.ceil($('.collection').length / maxInCollections);

        if (page != collections) {
            $('.paginationBtn_next').attr('onclick', 'showPage(' + Math.ceil(page + 1) + ')');
        }

        $('.collection').hide();
        $('.collection_' + page).show();

        $('.pageIndicator').text('Pagina: ' + page + ' / ' + collections);        
    }
    FixThumbnails();
}

// Pagination jump to page
function jumpTo() {
    var maxInCollections = $('.collection_1').length;
    var collections = Math.ceil($('.collection').length / maxInCollections);
    var to = prompt('Spring naar pagina:');

    if (to != null) {
        if (to > collections || to < 1) {
            alert('Deze pagina bestaat niet.');
        } else {
            showPage(parseInt(to));
        }
    }
}

function buildOverview(pictures) {
    if ($('#photoOverview').length == 1) {
        $('#photoOverview').remove();
    }

    var html = '<ul id="photoOverview">';
    var pCounter = 1; // Photo counter
    var pInCollection = 12;
    var collection = 1;
    var pOnRow = 4; // Photos on row
    var bulCounter = 1; // begin ul counter
    var eulCounter = pOnRow; // end ul counter    
    var Class = "";
    var liClass = "";
    pictures.forEach(function (item) {
        Class = item.Color;

        if (pCounter % pOnRow == 0) {
            Class += " last";
            }

        if (pCounter <= pOnRow) {
            Class += " toprow";
        }

        if (pCounter == pInCollection + 1) {            
            pInCollection += pInCollection;
            collection++;
        }

        if (pCounter == bulCounter) {
            html += '<ul>';
            if (pCounter == pInCollection) {
                collection++;
                pInCollection += pInCollection;
            }
            bulCounter += pOnRow;
        }
        html += '<li id="' + item.Id + '" class="collection collection_' + collection + ' photo ' + Class + '">';
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
            html += '</ul>';
            eulCounter += pOnRow;
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
    buildPagination();
}