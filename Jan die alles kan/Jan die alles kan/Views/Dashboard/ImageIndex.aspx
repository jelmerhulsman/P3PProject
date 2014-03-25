<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DashboardSite.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Jan_die_alles_kan.Models.PictureModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Image Controls
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Image Controls</h2>
    <p>
        <%: Html.ActionLink("Back to dashboard", "Index") %>
    </p>
    <p>
        <%: Html.ActionLink("Upload new image", "ImageUpload") %>
    </p>
    <table>
        <tr>
            <th>
                Name
            </th>
            <th>
                Category
            </th>
            <th>
               Modification Time
            </th>
            <th>
                Creation Time
            </th>
            <th>
                Color
            </th>
            <th>
                Description
            </th>
            <th>
                File Name
            </th>
            <th>
                Price
            </th>
            <th></th>
        </tr>

        <% foreach (var item in Model)
           { %>
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
