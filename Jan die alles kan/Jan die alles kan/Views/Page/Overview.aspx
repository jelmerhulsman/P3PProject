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

            if ($("#shoppingCartBox ul li").length > 0) {
                $("#shoppingCartBox ul li:even").css("background-color", "rgba(0,0,0,0.6)");
            }
            if ($("#shoppingCartBox ul li").length > 1) {
                $("#shoppingCartBox ul li:odd").css("background-color", "none");
            }

            $(".removeItem").click(function () {
                $(this).parent("li").fadeOut(function () {
                    $(this).remove();

                    setCartText();

                    if ($("#shoppingCartBox ul li").length > 0) {
                        $("#shoppingCartBox ul li:even").css("background-color", "rgba(0,0,0,0.6)");
                    }
                    if ($("#shoppingCartBox ul li").length > 1) {
                        $("#shoppingCartBox ul li:odd").css("background-color", "none");
                    }
                });
            });

            $('btn.search').click(function () {
                $.post("http://localhost:52802/Ajax/Filter", null, function (data) {
                    console.log(data);
                });
            })

            $('#photos li').each(function () {
                if ($($(this)[0].children[0]).height() < $($(this)[0].children[0]).width()) {
                    $(this).addClass('landscape');
                } else {
                    $(this).addClass('portrait');
                }
            })
        });

        $(window).resize(function () {
            //$('#main .top').css('max-width', $(window).width() - 260);
            $('#main .top .inner, #main .top .bottom').css('max-width', $(window).width() - 280);

            $('#filtersContainer').css("height", $('body').height());
            $('.stroke').css("height", $('body').height());
        });

        function setCartText() {
            var cartButtonText = $("#shoppingCartBox ul li").length

            if (cartButtonText < 1)
                cartButtonText += " Photo";
            else
                cartButtonText += " Photos";


            $(".shopping a span").html(cartButtonText);
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
                        <li><span class="checkbox"><span class="inner"></span></span><span class="label">Abstract</span></li>
                        <li><span class="checkbox"><span class="inner"></span></span><span class="label">Animals</span></li>
                        <li><span class="checkbox"><span class="inner"></span></span><span class="label">The Arts</span></li>
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
                                <li>
                                    <div class="cartImage">
                                    </div>
                                    <div class="cartDescription">
                                        <p>Title here</p>
                                        <p>Category</p>
                                        <p>€ 00,00</p>
                                    </div>
                                    <p class="removeItem"></p>
                                    <div class="clear"></div>
                                </li>
                                <li>
                                    <div class="cartImage">
                                    </div>
                                    <div class="cartDescription">
                                        <p>Title2 here</p>
                                        <p>Category2</p>
                                        <p>€ 00,00</p>
                                    </div>
                                    <p class="removeItem"></p>
                                    <div class="clear"></div>
                                </li>
                            </ul>
                            <div id="cartPrice">
                                <p>
                                    Sub total<br />
                                    Discount<br />
                                    Total
                                </p>
                                <p>
                                    € 00,00<br />
                                    10%<br />
                                    € 00,00
                                </p>
                                <div class="clear"></div>
                            </div>
                            <a href="#" id="cartCheckOut">Check out</a>
                        </div>
                    </div>
                </div>
                <div class="login">
                    <a href="#">Login &gt;</a>
                </div>
            </div>
        </div>
        <div class="right">
            <div id="topMenu">
                 <form action="" method="post">
                    <select name="sortBy">
                        <option value="priceHL">Prijs hoog - laag</option>
                        <option value="priceLH">Prijs laag - hoog</option>
                        <option value="nameAZ">Naam A - Z</option>
                        <option value="nameZA">Naam Z - A</option>
                    </select>
                </form>
            </div>
            <div id="photos">
                <ul>
                    <% 
                        int pCounter = 1; // Photo counter
                        int bulCounter = 1; // begin ul counter
                        int eulCounter = 4; // end ul counter
                        string Class = "";
                        foreach(var item in Model){
                            if (pCounter % 4 == 0)
                            {
                                Class = "last";
                            }
                            else {
                                Class = "";
                            }

                            if (pCounter <= 4)
                            { 
                                Class += " toprow";
                            }

                            if (pCounter == bulCounter)
                            { 
                                %> <ul> <%
                                bulCounter += 4;
                            }
                    %>
                    <li class="<%: Class %>">
                        <img src="../../Images/Categories/<%: Html.DisplayFor(modelItem => item.Category) %>/Thumbnails/<%: Html.DisplayFor(modelItem => item.File_name) %>" alt="" />
                        <div class="description">
                            <h3><%: Html.DisplayFor(modelItem => item.Name) %></h3>
                            <p>Category: <%: Html.DisplayFor(modelItem => item.Category) %></p>
                            <span class="price">&euro; <%: Html.DisplayFor(modelItem => item.Price) %></span>
                        </div>
                    </li>
                    <% 
                            if (pCounter == eulCounter)
                            { 
                                %> </ul><li class="clear"></li> <%
                                eulCounter += 4;
                            }
                            pCounter++;
                        } 
                    %>                    
                </ul>
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
                    <li><a href="#">Home</a></li>
                    <li><a href="#">About us</a>
                        <ul>
                            <li><a href="#">History</a></li>
                            <li><a href="#">Eco photos</a></li>
                        </ul>
                    </li>
                    <li><a href="#" class="special">Sign up, it's free!</a></li>
                    <li><a href="#">Contact</a></li>
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
            <div id="imageDescription">
                <h2>Titel hier invoeren</h2>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent tincidunt arcu vel tortor accumsan, nec sodales purus blandit. Donec convallis nibh a odio pretium pellentesque. Proin vestibulum enim nisi, nec fringilla magna adipiscing non.</p>
            </div>
            <div id="addCart">
                <p>€ 00,00 </p>
                <input id="addToCartButton" type="button" value="+ Add to cart" />
            </div>
        </div>
    </div>
</body>
</html>
