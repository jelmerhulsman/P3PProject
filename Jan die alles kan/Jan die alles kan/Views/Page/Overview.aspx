<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Jan_die_alles_kan.Models.PictureModel>>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Gerlof Productions</title>
    <link href="../../Content/Overview.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <script src="../../Scripts/jquery-ui-1.8.24.min.js"></script>

    <script type="text/javascript">
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

            $('#filtersContainer').css("height", $('body').height());
            $('.stroke').css("height", $('body').height());

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

            $('#main .top .inner, #main .top .bottom').css('max-width', $(window).width() - 280);

            $("#overlay").hide();

            $("#overlay, #closeOverlay").click(function () {
                $("#overlay").fadeOut();
            });

            $("#boxOverlay").click(function (event) {
                event.stopPropagation();
            });

            setCartText();

            $("#shoppingCartOverlay").hide();

            $(".shopping").click(function () {
                $("#shoppingCartOverlay").slideToggle();
            });

            $("#shoppingCartOverlay").click(function (event) {
                event.stopPropagation();
            });

            $('button.search').click(function () {
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

                var filterCategories = "";
                $('.categoryHolder li .checkbox').each(function () {
                    if ($(this).hasClass('clicked')) {
                        filterCategories += $(this).next().text() + ",";
                    };
                });
                filterCategories = filterCategories.substr(0, filterCategories.length - 1);

                var priceRange = $("#min").text().replace('€', '') + "," + $("#max").text().replace('€', '');

                console.log(filterColors);
                console.log(filterCategories);
                console.log(priceRange);
                $.post("http://localhost:52802/Ajax/Filter", { colors: filterColors, categories: filterCategories, pricerange: priceRange }, function (data) {
                    //console.log(data);
                    buildOverview(data);
                });
            });

            // Foto toevoegen aan winkelkarretje
            $('#addToCartButton').click(function () {
                var id = $(this).attr('class');
                $.post("http://localhost:52802/Ajax/PhotoToCart", { id: id }, function (data) {
                    if ($('#slidePhoto').length == 1) {
                        $('#slidePhoto').remove();
                    }
                    $('#main').append('<div id="slidePhoto"></div>');
                    var imgPosition = $('#imageOverlay img').position();
                    var boxPosition = $('#boxOverlay').position();
                    $('#slidePhoto').append($('#imageOverlay img'));
                    $('#slidePhoto').css({ left: imgPosition.left + boxPosition.left, top: imgPosition.top + boxPosition.top, position: 'fixed', zIndex: 20 });
                    $('#overlay').fadeOut();
                    $('#slidePhoto').delay(300).animate({ top: -$('#slidePhoto img').height(), opacity: 0 }, 300);

                    updateCart();
                });
            });

            updateCart();
            $.post("http://localhost:52802/Ajax/GetPhotos", null, function (data) {
                buildOverview(data);
            });
        });

        $(window).resize(function () {
            //$('#main .top').css('max-width', $(window).width() - 260);
            $('#main .top .inner, #main .top .bottom').css('max-width', $(window).width() - 280);

            $('#filtersContainer').css("height", $('body').height());
            $('.stroke').css("height", $('body').height());

            if ($('#imageOverlay img').hasClass('landscape')) {
                $('#imageOverlay').css('height', $('#boxOverlay').height() * 0.7 - 100);
                $('#boxOverlay .bottom').css('height', $('#boxOverlay').height() * 0.3 + 100);
            } else {
                $('#imageOverlay').css('height', $('#boxOverlay').height() * 0.7);
                $('#boxOverlay .bottom').css('height', $('#boxOverlay').height() * 0.3);
            }

            $('#imageOverlay img').height($('#imageOverlay').height());
        });

        function updateCart() {
            $('#shoppingCartBox ul li').each(function () {
                $(this).remove();
            })
            $.post("http://localhost:52802/Ajax/GetOrder", {}, function (data) {
                var order = data;
                if (data != "") {
                    if (order.indexOf(',') == -1) {
                        $.post("http://localhost:52802/Ajax/PhotoInfo", { id: order }, function (data) {
                            $('#shoppingCartBox ul').prepend(
                                '<li class="' + data.Id + '">' +
                                    '<div class="cartImage">' +
                                        '<img src="../../Images/Categories/' + data.Category + '/' + data.File_name + '" alt="" />' +
                                    '</div>' +
                                    '<div class="cartDescription">' +
                                        '<p>' + data.Name + '</p>' +
                                        '<p>' + data.Category + '</p>' +
                                        '<p class="price">€ ' + data.Price + '</p>' +
                                    '</div>' +
                                    '<p class="removeItem ' + data.Id + '"></p>' +
                                    '<div class="clear"></div>' +
                                '</li>'
                            );
                            if ($('#shoppingCartBox li.' + data.Id + ' img').height() < $('#shoppingCartBox li.' + data.Id + ' img').width()) {
                                $('#shoppingCartBox li.' + data.Id + ' img').addClass('landscape');
                            }
                            else {
                                $('#shoppingCartBox li.' + data.Id + ' img').addClass('portrait');
                            }
                            setCartText();
                            addRemoveFunctionality();
                            updatePrice();
                        });
                    } else {
                        var orders = order.split(',');
                        orders.forEach(function (order) {
                            $.post("http://localhost:52802/Ajax/PhotoInfo", { id: order }, function (data) {
                                $('#shoppingCartBox ul').prepend(
                                    '<li class="' + data.Id + '">' +
                                        '<div class="cartImage">' +
                                            '<img src="../../Images/Categories/' + data.Category + '/' + data.File_name + '" alt="" />' +
                                        '</div>' +
                                        '<div class="cartDescription">' +
                                            '<p>' + data.Name + '</p>' +
                                            '<p>' + data.Category + '</p>' +
                                            '<p class="price">€ ' + data.Price + '</p>' +
                                        '</div>' +
                                        '<p class="removeItem ' + data.Id + '"></p>' +
                                        '<div class="clear"></div>' +
                                    '</li>'
                                );
                                setCartText();
                                addRemoveFunctionality();
                                updatePrice();
                            });
                        });
                    }
                }
            });
        }

        function setCartText() {
            var cartButtonText = $("#shoppingCartBox ul li").length;

            if (cartButtonText == 0) {
                $('#cartPrice').prev().css({ paddingBottom: 0, marginBottom: 0, borderBottom: 0 });
                $('#cartPrice').css({ paddingBottom: '15px' });
                $('#cartPrice').html('<p>No photos selected.</p>');
                $('#cartCheckOut').hide();
            } else {
                $('#cartPrice').prev().removeAttr("style");
                $('#cartPrice').removeAttr("style");
                $('#cartPrice').html('<p>Sub total<br />Discount<br />Total</p><p class="prices">€ 00,00<br />10%<br />€ 00,00</p><div class="clear"></div>');
                $('#cartCheckOut').show();
            }

            if (cartButtonText == 1)
                cartButtonText += " Photo";
            else
                cartButtonText += " Photos";


            $(".shopping a span").html(cartButtonText);
        }

        function updatePrice() {
            var price = 0;
            $('#shoppingCartBox ul .price').each(function () {
                var pPrice = parseInt($(this).text().replace('€', ''));
                pPrice = pPrice;
                price += pPrice;
            })

            var discount = 0.10;
            var totalPrice = price * discount;
            totalPrice = price - totalPrice;
            console.log(totalPrice);
            $('#shoppingCartBox #cartPrice .prices').html('&euro; ' + price + '<br/>' + discount * 100 + '%<br/>&euro; ' + totalPrice);
        }

        function addRemoveFunctionality() {
            $(".removeItem").click(function () {
                $(this).parent("li").fadeOut(function () {
                    $(this).remove();
                    setCartText();
                    updatePrice();

                    var order = "";
                    var i = 0;
                    $('#shoppingCartBox ul li').each(function () {
                        if (i == 0) {
                            order = $(this).attr("class");
                            i++;
                        } else {
                            order = order + ', ' + $(this).attr("class");
                        }
                    });

                    $.post("http://localhost:52802/Ajax/RemoveFromCart", { order: order }, function () { });
                });
            });
        }

        function buildOverview(pictures) {
            if ($('#photoOverview').length == 1) {
                $('#photoOverview').remove();
            }
            console.log(pictures);
            var html = '<ul id="photoOverview">'; 
            var pCounter = 1; // Photo counter
            var bulCounter = 1; // begin ul counter
            var eulCounter = 4; // end ul counter
            var Class = "";
            pictures.forEach(function (item) {
                console.log(pCounter);
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
                html += '<img src="../../Images/Categories/' + item.Category + '/' + item.File_name + '" alt="" />';

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

            $('#photos li').each(function () {
                if ($($(this)[0].children[0]).height() < $($(this)[0].children[0]).width()) {
                    $(this).addClass('landscape');
                } else {
                    $(this).addClass('portrait');
                }
            })

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
                        $('#imageOverlay img').addClass('landscape');
                        $('#imageOverlay').css('height', $('#boxOverlay').height() * 0.7 - 100);
                        $('#boxOverlay .bottom').css('height', $('#boxOverlay').height() * 0.3 + 100);
                    }
                    else {
                        $('#imageOverlay img').addClass('portrait');
                        $('#imageOverlay').css('height', $('#boxOverlay').height() * 0.7);
                        $('#boxOverlay .bottom').css('height', $('#boxOverlay').height() * 0.3);
                    }

                    $('#imageOverlay img').height($('#imageOverlay').height());
                });
                $('#overlay').fadeIn();
            })
        }
    </script>
</head>
<body oncontextmenu="">
    <!-- return false TERUG PLAATSEN -->
<div id="filtersContainer">
            <div class="stroke"></div>
            <a href="" class="logo">
                <img src="../../Images/milanovLogoWhite.png" /></a>

            <div id="filters">
                <span class="title">Refine collection</span>

                <form action="">
                    <input type="text" name="keyword" class="field" placeholder="Search..." />
                </form>

                <div class="box">
                    <span>Color</span><span class="handler close"></span>

                    <ul class="colorHolder">
                        <li>
                            <div class="tooltip">Red</div>
                            <a href="javascript:void(0);" class="color red"></a></li>
                        <li>
                            <div class="tooltip">Orange</div>
                            <a href="javascript:void(0);" class="color orange"></a></li>
                        <li>
                            <div class="tooltip">Yellow</div>
                            <a href="javascript:void(0);" class="color yellow"></a></li>
                        <li>
                            <div class="tooltip">Green</div>
                            <a href="javascript:void(0);" class="color green"></a></li>
                        <li>
                            <div class="tooltip">Cyan</div>
                            <a href="javascript:void(0);" class="color cyan"></a></li>
                        <li>
                            <div class="tooltip">Blue</div>
                            <a href="javascript:void(0);" class="color blue"></a></li>
                        <li>
                            <div class="tooltip">Purple</div>
                            <a href="javascript:void(0);" class="color purple"></a></li>
                        <li>
                            <div class="tooltip">Pink</div>
                            <a href="javascript:void(0);" class="color pink"></a></li>
                        <li>
                            <div class="tooltip">White</div>
                            <a href="javascript:void(0);" class="color white"></a></li>
                        <li>
                            <div class="tooltip">Grey</div>
                            <a href="javascript:void(0);" class="color grey"></a></li>
                        <li>
                            <div class="tooltip">Black</div>
                            <a href="javascript:void(0);" class="color black"></a></li>
                        <li>
                            <div class="tooltip">Brown</div>
                            <a href="javascript:void(0);" class="color brown"></a></li>
                    </ul>

                    <div class="clear"></div>
                </div>

                <div class="box">
                    <span>Orientation</span><span class="handler close"></span>

                    <ul class="orientationHolder">
                        <li><span class="checkbox"><span class="inner"></span></span><span class="label">Horizontal</span></li>
                        <li><span class="checkbox"><span class="inner"></span></span><span class="label">Vertical</span></li>
                    </ul>
                </div>

                <div class="box">
                    <span>Category</span><span class="handler close"></span>

                    <ul class="categoryHolder">
                        <% foreach(var c in ViewBag.categories){ %>
                        <li><span class="checkbox"><span class="inner"></span></span><span class="label"><%: c.Name %></span></li>
                        <% } %>
                    </ul>
                </div>

                <div class="box">
                    <span>Price range</span><span class="handler close"></span>

                    <ul>
                        <li>
                            <span id="min"></span>
                            <div id="slider-range"></div>
                            <span id="max"></span>
                            <div class="clear"></div>
                        </li>
                    </ul>
                </div>

                <button class="btn search">Search</button>
                <button class="btn cancel">Cancel</button>
            </div>
        </div>
    <div id="main">
        <div class="top">
            <div class="inner">
                <div class="languages">
                    <a href="#">
                        <img src="../../Images/nl.jpg" alt="Dutch" /></a>
                    <a href="#" class="selected">
                        <img src="../../Images/en.jpg" alt="English" /></a>
                </div>
                <div class="shopping">
                    <a href="javascript:void(0);">
                        <img src="../../Images/Btn/btn_shoppingcart.png" alt="" /><span>... Photos</span></a>
                    <div id="shoppingCartOverlay">
                        <span></span>
                        <div id="shoppingCartBox">
                            <ul>
                            </ul>
                            <div id="cartPrice">
                                <p>
                                    Sub total<br />
                                    Discount<br />
                                    Total
                                </p>
                                <p class="prices">
                                    € 00,00<br />
                                    10%<br />
                                    € 00,00
                                </p>
                                <div class="clear"></div>
                            </div>
                            
                           <a href="/Account/Checkout" id="cartCheckOut">Check out</a>
                              
                        </div>
                    </div>
                </div>
                <div class="login">
                    <% if(User.Identity.IsAuthenticated){ %>
                    <a href="#">Welcome, <%: User.Identity.Name %>! | <a href="http://localhost:52802/Account/LogOff">Log off</a></a>
                    <% } else { %>
                    <a href="http://localhost:52802/Account/Login">Login &gt;</a>
                    <% } %>
                </div>
            </div>
        </div>
        <div class="right">
            <div id="topMenu">
                 <form action="" method="post">
                    <select name="sortBy">
                        <option value="priceHL">Price Descending</option>
                        <option value="priceLH">Price Ascending</option>
                        <option value="nameAZ">Name A - Z</option>
                        <option value="nameZA">Name Z - A</option>
                    </select>
                </form>
            </div>
            <div id="photos">
                <div class="clear"></div>
            </div>
            <div id="pagination">
                <div class="left">
                    <a class="disabled" href="#">&lt;</a>
                </div>
                <div class="center">
                    <a class="disabled" href="">1</a>
                    <a href="">2</a>
                    <a href="">3</a>
                </div>
                <div class="right">
                    <a href="#">&gt;</a>
                </div>
                <div class="clear"></div>
            </div>
        </div>

        <div class="bottom">
            <div class="inner">
                <ul>
                    <% foreach(var page in ViewBag.Pages){ %>
                    <li><a href="http://localhost:52802/Page/Content/<%: page.Permalink %>"><%: page.Name %></a></li>                    
                    <% } %>
                </ul>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <div id="overlay">
        <div id="boxOverlay">
            <div id="closeOverlay">
            </div>
            <div id="imageOverlay">
            </div>
            <div class="bottom">
                <div id="imageDescription">
                    <h2 class="title"></h2>
                    <div class="description"></div>
                </div>
                <div id="addCart">
                    <p class="price"></p>
                    <% if(User.Identity.IsAuthenticated){ %>
                    <input id="addToCartButton" type="button" value="+ Add to cart" />
                    <% } else { %>
                    <a class="login" href="http://localhost:52802/Account/Login">Please login to order &gt;</a>
                    <% } %>
                </div>
             </div>
        </div>
    </div>
</body>
</html>
