<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AccountSite.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.PictureModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    downloadpage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>downloadpage</h2>
    <% foreach (var x in ViewBag.photoList)
       {
    %>
    <a href="<%= Url.Content("/Account/FileDownloadPage/"+ x.Category +"/"+ x.File_name)%>"><%= x.File_name%> -> Download</a><br />  
    <%     
       } 
    %>
   

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
