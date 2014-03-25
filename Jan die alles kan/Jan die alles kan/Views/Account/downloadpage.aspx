<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AccountSite.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.PictureModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    downloadpage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Download page</h2>
    <p>Here are your available downloads:</p>
    <table class="listTable" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <b>Preview</b>
            </td>
            <td>
                <b>Name of Product</b>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    <% foreach (var x in ViewBag.photoList)
       {
           for (int i = 0; i < x.File_name.Length; i++)
           {
               if (x.File_name[i] == '.')
               {
                   x.File_name = x.File_name.Substring(0, i);
                   break;
               }
           }
    %>
        <tr>
            <td>
                <img width="100" src="../../Images/Categories/<%= x.Category %>/Thumbnails/<%= x.File_name %>.jpg" />
            </td>
            <td>
                <%= x.Name %>
            </td>
            <td>
                <a class="btnDownload" href="<%= Url.Content("/Account/FileDownloadPage/"+ x.Category +"/"+ x.File_name)%>">Download</a><br />  
            </td>
        </tr>
    <%     
       } 
    %>
    </table>
   

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
