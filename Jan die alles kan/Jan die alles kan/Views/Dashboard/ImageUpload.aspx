<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DashboardSite.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Jan_die_alles_kan.Models.Category>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Upload
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p><a href="/Dashboard/Index">Back to dashboard</a></p>

    <% using (Html.BeginForm("ImageUpload", "Dashboard", FormMethod.Post, new { enctype = "multipart/form-data" }))
       { %>
   File:<br>
    <input type="file" name="File"/><br>
    Name:<br>
    <input type="text" name="Name"/><br>
    Category:<br>
    <select name="Category">
        <% foreach (var item in Model)
           {%>

            <option value="<%: item.Name  %>"><%: item.Name  %></option>

            <%}
        %>
    </select><br>
        <p> <%: Html.ActionLink("Add/Remove a Category","CategoryIndex") %> </p>
    Price:<br>
    <input type="number" name="Price"/><br>
    Color:<br>
    <select name="Color">
        <option value="">None specific</option>
        <option value="red">Red</option>
        <option value="orange">Orange</option>
        <option value="yellow">Yellow</option>
        <option value="green">Green</option>
        <option value="cyan">Cyan</option>
        <option value="blue">Blue</option>
        <option value="purpke">Purple</option>
        <option value="pink">Pink</option>
        <option value="white">White</option>
        <option value="grey">Grey</option>
        <option value="black">Black</option>
        <option value="brown">Brown</option>
    </select><br>
    Description:<br>
    <textarea name="Description"></textarea>
    <br>
    <br>
    <input type="submit" name="submit" value="Upload" />

    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
