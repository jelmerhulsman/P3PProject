<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Dashboard
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Dashboard</h2>
    <p> <%: Html.ActionLink("Image Index","ImageIndex") %> </p>
    <p> <%: Html.ActionLink("Page Index","PageIndex") %> </p>
    <p> <%: Html.ActionLink("Upload Image","ImageUpload") %> </p>
    <p> <%: Html.ActionLink("Category Index","CategoryIndex") %> </p>

            
        
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
