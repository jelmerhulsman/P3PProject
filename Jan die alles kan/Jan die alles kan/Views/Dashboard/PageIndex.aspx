<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Jan_die_alles_kan.Models.PagesModels>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    PageIndex
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>PageIndex</h2>
    <p><a href="/DashBoard">Back to dashboard</a></p>
<p>
    <%: Html.ActionLink("Create New", "PageCreate") %>
</p>
<table>
    <tr>
        <th>
            <%: Html.DisplayNameFor(model => model.Name) %>
        </th>
        <th>
           Controls
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.Permalink) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.Status) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.Pageposition) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.Seokey) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.Seodescription) %>
        </th>
        <th></th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
        <td>
            <%: Html.DisplayFor(modelItem => item.Name) %>
        </td>
        <td>
            <a href="pageedit/<%:item.Id %>">Edit Page</a> |
            <a href="../page/details/<%:item.Id %>">Show Page</a> |
            <a href="pagedelete/<%:item.Id %>">Delete Page</a>
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
