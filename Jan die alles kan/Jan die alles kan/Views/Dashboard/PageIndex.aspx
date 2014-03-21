<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DashboardSite.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Jan_die_alles_kan.Models.PagesModels>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Page Controls
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Page Controls</h2>
    <p>
        <%: Html.ActionLink("Back to dashboard", "Index") %>
    </p>
    <p>
        <%: Html.ActionLink("Create New", "PageCreate") %>
    </p>
    <table>
        <tr>
            <th>
                Name
            </th>
            <th>
                Controls
            </th>
            <th>
                Permalink
            </th>
            <th>
                Status
            </th>
            <th>
                Page Position
            </th>
            <th>
                SEO Key
            </th>
            <th>
                SEO Description
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
                <a href="PageEdit/<%:item.Id %>">Edit Page</a> |
                <a href="PageShow/<%:item.Permalink %>">Show Page</a> |
                <a href="PageDelete/<%:item.Id %>">Delete Page</a>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Permalink) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Status) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Pageposition) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Seokey) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Seodescription) %>
            </td>
        </tr>
        <% } %>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
