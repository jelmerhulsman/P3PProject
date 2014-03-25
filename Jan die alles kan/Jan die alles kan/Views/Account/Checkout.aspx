<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AccountSite.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.CartModels>" %>

<style type="text/css">
    input[type]
</style>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Checkout
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Checkout</h2>
    <% using (Html.BeginForm("CheckoutConfirmed", "Account", FormMethod.Post)) { %>
    <b>Your Items:</b>
    <table>
         <tr>
             <td><b>Preview</b></td>
             <td><b>Name of Product</b></td>
             <td><b>Category</b></td>
             <td aling="right"><b>Price</b></td>
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

    <div><b>Select Payment method</b>
        <br />
        <%= Html.RadioButton("paymentmethod", "Paypal") %> <img src="../../Images/Btn/paypal-button.png" />
        <br />
        <%= Html.RadioButton("paymentmethod", "Ideal") %> <img src="../../Images/Btn/ideal-button.png" />

        <br />
        <input type="submit" value="Buy" />
    </div>
    <% } %>

    <a href="downloadpage">Next step</a>
</asp:Content>
   
<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
