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

    <script src="../../Scripts/Global_Page/cart-function.js"></script>
    <script src="../../Scripts/Global_Page/photo-overlay.js"></script>

    <script src="../../Scripts/Overview_Page/build-overview.js"></script>
    <script src="../../Scripts/Overview_Page/filter-function.js"></script>    
    <script src="../../Scripts/Overview_Page/style-fix.js"></script>
</head>
<body oncontextmenu="">
    <!-- return false TERUG PLAATSEN -->
<div id="filtersContainer">
            <div class="stroke"></div>
            <a href="" class="logo">
                <img src="../../Images/milanovLogoWhite.png" /></a>

            <div id="filters">
                <span class="title">Refine collection</span>

                <form action="" method="get">
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
                        <option value="newest">Newest</option>
                        <option value="nameAZ">Name A - Z</option>
                        <option value="nameZA">Name Z - A</option>
                        <option value="priceLH">Price Ascending</option>
                        <option value="priceHL">Price Descending</option>
                    </select>
                </form>
                <span class="pageIndicator""></span>
            </div>
            <div id="photos">
                <div class="clear"></div>
            </div>
            <div id="pagination">
                <div class="left">
                </div>
                <div class="center">
                </div>
                <div class="right">
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
