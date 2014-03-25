<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AccountSite.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.CartModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Checkout
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Checkout</h2>
    <% using (Html.BeginForm("CheckoutConfirmed", "Account", FormMethod.Post)) { %>
    <b>Your Items:</b>
    <table class="orderList" cellspacing="0" cellpadding="0">
         <tr>
             <td><b>Preview</b></td>
             <td><b>Name of Product</b></td>
             <td><b>Category</b></td>
             <td align="right"><b>Price</b></td>
        </tr>
     <% foreach (var x in ViewBag.photoList)
       {
    %>
    
        <tr>
            <td><img width="100" src="../../Images/Categories/<%= x.Category %>/Thumbnails/<%= x.File_name %>" /></td>
            <td><%= x.Name %></td>
            <td><%= x.Category %></td>
            <td align="right">€<%= x.Price %></td>
        </tr>
    <%     
       } 
    %>
        <tr>
            <td colspan="4" align="right"><b>Total price: €<%double price = 0; foreach (var x in ViewBag.photoList)
                       { %> <% price += Convert.ToDouble(x.Price);  %> <% } Response.Write(price);  %></b></td>
        </tr>
    </table>

    <div>
        <span class="row">
            <b>Select Payment method</b>
        </span>
        <span class="row">
            <!-- < %= Html.RadioButton("paymentmethod", "Paypal", true, "id='paypal'") %> -->
            <input name="paymentmethod" id="Paypal" type="radio" value="Paypal" />
            <label for="Paypal"><img src="../../Images/Btn/paypal-button.png" /></label>
        </span>
        <span class="row last">
            <!-- < %= Html.RadioButton("paymentmethod", "Ideal", false, "id='ideal'") %> -->
            <input name="paymentmethod" id="Ideal" type="radio" value="Ideal" />
            <label for="Ideal"><img src="../../Images/Btn/ideal-button.png" /></label>
        </span>
        <div class="clear"></div>
        <input type="submit" value="Buy" />
    </div>
    <% } %>

    <a href="downloadpage">Next step</a>
</asp:Content>
   
<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
