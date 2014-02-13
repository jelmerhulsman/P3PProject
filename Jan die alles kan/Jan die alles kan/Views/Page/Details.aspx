<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.PagesModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Potato | 
</asp:Content>

 
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <%= Server.HtmlDecode(Model.Content)   %>
    
</asp:Content>
