<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DashboardSite.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Jan_die_alles_kan.Models.Category>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add or remove a category</h2>
    <p>
        <%: Html.ActionLink("Back to dashboard", "Index") %>
    </p>
    <p>
        <%: Html.ActionLink("Create a new category", "CategoryCreate") %>
    </p>
    <table>
        <tr>
            <th>
                <%: Html.DisplayNameFor(model => model.Name) %>
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
                <%: Html.ActionLink("Delete Category", "CategoryDelete", new { id=item.ID }) %>

            </td>
        </tr>
        <% } %>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
