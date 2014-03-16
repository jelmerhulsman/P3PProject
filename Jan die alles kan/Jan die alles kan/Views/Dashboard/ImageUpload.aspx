<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Jan_die_alles_kan.Models.UploadModel>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Upload
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <p><a href="/DashBoard">Back to dashboard</a></p>

    <% using ( Html.BeginForm("UploadImage","Upload", FormMethod.Post, new { enctype = "multipart/form-data"} )) {%>
   File:<br> 
    <input type="file" name="File" value="File" /><br>
    Name:<br>
    <input type="text" name="Name" value="Name" /><br>
    Category:<br>
    <input type="text" name="Category" value="Category" /><br>
    Price:<br>
    <input type="number" name="Price" value="Price" /><br>
    Color:<br>
    <input type="color" name="Color" value="Color" /><br>
    Description:<br>
    <textarea name="Description" placeholder="Description"></textarea>
    <br><br>
    <input type="submit" name="submit" value="Upload" />

    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
