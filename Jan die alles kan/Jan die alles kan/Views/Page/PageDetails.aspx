<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.PagesModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server" />
 
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <% string antiFogeryToken = Convert.ToString(AntiForgery.GetHtml());

      for (int i = 0; i < Model.Content.Length; i++)
      {
          if (((i + 5) <= Model.Content.Length) && Model.Content.Substring(i, 5) == "<form")
          {
              for (int ii = i + 5; ii < Model.Content.Length; ii++)
              {
                  if (((ii + 7) <= Model.Content.Length) && Model.Content.Substring(ii, 7) == "</form>")
                  {
                      Model.Content = Model.Content.Insert(ii, antiFogeryToken);
                      break;
                  }
              }
          }
      } %>
    <h2><%= Server.HtmlDecode(Model.Name)   %></h2>
    <%= Server.HtmlDecode(Model.Content)   %>
    
</asp:Content>
