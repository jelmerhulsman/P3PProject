<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Jan_die_alles_kan.Models.PictureModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ImageIndex
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>ImageIndex</h2>
        <p><a href="/Dashboard/Index">Back to dashboard</a></p>
<p>
    <%: Html.ActionLink("Create New", "ImageUpload") %>
</p>
<table>
    <tr>
        <th>
            <%: Html.DisplayNameFor(model => model.Name) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.Category) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.MTime) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.CTime) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.Color) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.Description) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.File_name) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.Price) %>
        </th>
        <th></th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
        <td>
            <%: Html.DisplayFor(modelItem => item.Name) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Category) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.MTime) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.CTime) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Color) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Description) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.File_name) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Price) %>
        </td>
        <td>
            <%: Html.ActionLink("Edit", "ImageEdit", new { id=item.Id }) %> |
            <%: Html.ActionLink("Details", "Details", new { id=item.Id }) %> |
            <%: Html.ActionLink("Delete", "ImageDelete", new { id=item.Id }) %>
        </td>
    </tr>
<% } %>

</table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
