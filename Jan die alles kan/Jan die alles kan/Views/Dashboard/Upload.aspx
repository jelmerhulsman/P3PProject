<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Jan_die_alles_kan.Models.UploadModel>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Upload
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    <% using ( Html.BeginForm("index","Upload", FormMethod.Post, new { enctype = "multipart/form-data"} )) {%>
   
   <table>
       <tr>
           <td>File:</td>
           <td><input type="file" name="File" id="File" /></td>
       </tr>
       <tr>
           <td>&nbsp;</td>
           <td><input type="submit" name="submit" value="Upload" /></td>
       </tr>
   </table>
    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
