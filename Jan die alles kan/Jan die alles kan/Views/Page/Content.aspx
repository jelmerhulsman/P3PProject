<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, user-scalable=no" />
    <title>Gerlof Productions</title>
    <link href="~/Content/Content.css" rel="stylesheet" />
    <script src="~/Scripts/jquery-1.8.2.min.js"></script>
    <script src="~/Scripts/jquery-ui-1.8.24.min.js"></script>
    <script src="~/Scripts/backstretch.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.slideshow').backstretch([
                "http://localhost:52802/Images/Slides/slide1.jpg",
                "http://localhost:52802/Images/Slides/slide2.jpg",
                "http://localhost:52802/Images/Slides/slide3.jpg",
                "http://localhost:52802/Images/Slides/slide4.jpg",
                "http://localhost:52802/Images/Slides/slide5.jpg"
            ], { duration: 6000, fade: 1500 });


            $('#top .inner').css('max-width', $(window).width() - 20);
            $('#content .right').css('max-width', $('#content').width() - 300);
        });

        $(window).resize(function () {
            $('#top .inner').css('max-width', $(window).width() - 20);
            $('#content .right').css('max-width', $('#content').width() - 300);
        });
    </script>
</head>

<body>
    <div id="main">
        <div id="top">
            <div class="inner">
                <div class="languages">
                    <a href="#"><img src="~/Images/nl.jpg" alt="Dutch" /></a>
                    <a href="#" class="selected"><img src="~/Images/en.jpg" alt="English" /></a>
                </div>
                <div class="shopping">
                    <a href="javascript:void(0);"><img src="~/Images/Btn/btn_shoppingcart.png" alt="" /><span>3 Photos</span></a>
                </div>
                <div class="login">
                    <a href="#">Login &gt;</a>
                </div>
            </div>
        </div> 
        <a href="" id="sticker"><img src="~/Images/sticker.png" alt="sign in" /></a>
        <div id="content">            
            <div class="left">
                <a href="" class="logo"><img src="~/Images/milanovLogoWhite.png" alt="Gerlof Productions" /></a>
                <ul>
                    <li><a href="">Pagina</a></li>
                    <li><a href="">Pagina</a></li>
                    <li><a href="">Pagina</a></li>
                    <li><a href="">Pagina</a></li>
                    <li><a href="">Pagina</a></li>
                </ul>
            </div>
            <div class="right">
                <h1>@ViewBag.Name</h1>
                <p>@ViewBag.Content</p>
            </div>
        </div>
    </div>

    <div class="slideshow"></div>
</body>
</html>
