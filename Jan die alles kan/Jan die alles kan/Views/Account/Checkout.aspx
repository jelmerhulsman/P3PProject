<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AccountSite.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.CartModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Checkout
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Checkout</h2>
    <% using (Html.BeginForm("CheckoutConfirmed", "Account", FormMethod.Post)) { %>
    <div><b>Select Payment method</b>
        <br />
        <%= Html.RadioButton("paymentmethod", "Paypal") %> <img src="../../Images/Btn/paypal-button.png" />
        <br />
        <%= Html.RadioButton("paymentmethod", "Ideal") %> <img src="../../Images/Btn/ideal-button.png" />

        <br />
        <input type="submit" value="Submit!" />
    </div>
    <% } %>
</asp:Content>
    
<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
