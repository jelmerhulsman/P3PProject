<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, user-scalable=no" />
    <title>Gerlof Productions</title>
    <link href="../../Content/Landingpage.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <script src="../../Scripts/jquery-ui-1.8.24.min.js"></script>
    <script src="../../Scripts/backstretch.js"></script>

    <script type="text/javascript">
        function resizeField() {
            var formWidth = $('#main form').width();
            var screenWidth = $(window).width();

            if (screenWidth >= 600)
                $('#searchField').width(formWidth - 150);
            else
                $('#searchField').width(formWidth);
        };

        $(window).resize(function () {
            resizeField();
        });

        $(document).ready(function () {
            resizeField();

            $('.slideshow').backstretch([
                "http://localhost:52802/Images/Slides/slide1.jpg",
                "http://localhost:52802/Images/Slides/slide2.jpg",
                "http://localhost:52802/Images/Slides/slide3.jpg",
                "http://localhost:52802/Images/Slides/slide4.jpg",
                "http://localhost:52802/Images/Slides/slide5.jpg"
            ], { duration: 6000, fade: 1500 });

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

            $('body *').not(':has(input)').not('input').disableSelection();

            $('#login').click(function (event) {
                event.preventDefault();

                $.post("http://localhost:52802/Account/FormLogin", { username: $('input[name="username"]').val(), password: $('input[name="password"]').val() }, function (data) {
                    alert(data);
                });
            })
        });
    </script>
</head>
<body>
    <div id="main">
        <img src="../../Images/milanovLogoBlack.png" />
        <form action="http://localhost:52802/Page/Overview" method="get">
            <input name="search" id="searchField" type="text" placeholder="Search images or just browse..." />
            <input id="searchButton" type="submit" value="Search >" />
        </form>
    </div>

    <div id="footer">
        <div class="open">
            <img src="../../Images/Btn/btn_up.png" />
        </div>
        <div class="close">
            <img src="../../Images/Btn/btn_down.png" />
        </div>
        <div class="floating">
            <img src="../../Images/sticker.png" class="sticker" />
        </div>
        <div id="left">
            <h1>Direct Login</h1>
            <form id="loginForm" action="http://localhost:52802/Account/FormLogin" method="post">
                <input type="text" name="username" id="username" class="field" placeholder="Username" />
                <input type="password" name="password" id="password" class="field" placeholder="Password" />
                <input type="submit" id="login" value="Login" />
            </form>
        </div>

        <div id="stroke">
        </div>

        <div id="right">
            <img src="../../Images/sticker.png" />
            <a href="../../Account/Register"></a>
            <p>
                New on
                <br />
                Gerlof Productions?
            </p>
            <img src="../../Images/arrow_big.png" id="arrow" />
        </div>
        <div class="clear"></div>
    </div>

    <div class="slideshow"></div>

</body>
</html>
