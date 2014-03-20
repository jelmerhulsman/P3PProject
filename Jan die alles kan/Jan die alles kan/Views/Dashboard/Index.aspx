<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DashboardSite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Dashboard
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Dashboard</h2>
    <p> <%: Html.ActionLink("Image Controls","ImageIndex") %> </p>
    <p> <%: Html.ActionLink("Page Controls","PageIndex") %> </p>
    <p> <%: Html.ActionLink("Upload an Image","ImageUpload") %> </p>
    <p> <%: Html.ActionLink("Add/Remove a Category","CategoryIndex") %> </p>
    <p> <%: Html.ActionLink("Send a Mail", "SendMail") %></p>

            
        
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
