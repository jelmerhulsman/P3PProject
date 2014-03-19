<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, user-scalable=no" />
    <title>Gerlof Productions</title>
    <link href="../../Content/Content.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <script src="../../Scripts/jquery-ui-1.8.24.min.js"></script>
    <script src="../../Scripts/backstretch.js"></script>
    
    <script src="../../Scripts/Global_Page/slideshow.js"></script>

    <script src="../../Scripts/Content_Page/resize-blocks.js"></script>
</head>

<body>
    <div id="main">
        <div id="top">
            <div class="inner">
                <div class="languages">
                    <a href="#"><img src="../../Images/nl.jpg" alt="Dutch" /></a>
                    <a href="#" class="selected"><img src="../../Images/en.jpg" alt="English" /></a>
                </div>
                <div class="shopping">
                    <a href="javascript:void(0);"><img src="../../Images/Btn/btn_shoppingcart.png" alt="" /><span>3 Photos</span></a>
                </div>
                <div class="login">
                    <a href="#">Login &gt;</a>
                </div>
            </div>
        </div> 
        <a href="" id="sticker"><img src="../../Images/sticker.png" alt="sign in" /></a>
        <div id="content">            
            <div class="left">
                <a href="" class="logo"><img src="../../Images/milanovLogoWhite.png" alt="Gerlof Productions" /></a>
                <ul>
                    <li><a href="">Pagina</a></li>
                    <li><a href="">Pagina</a></li>
                    <li><a href="">Pagina</a></li>
                    <li><a href="">Pagina</a></li>
                    <li><a href="">Pagina</a></li>
                </ul>
            </div>
            <div class="right">
                <h1><%: ViewBag.Name %></h1>
                <p><%: ViewBag.Content %></p>
            </div>
        </div>
    </div>

    <div class="slideshow"></div>
</body>
</html>
