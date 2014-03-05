<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Jan_die_alles_kan.Models.PagesModels>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Index</h2>
    <p><%: Html.ActionLink("Back To Dashboard", "BackToDashboard") %></p>
<p>
    <%: Html.ActionLink("Create New", "Create") %>
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
            <%: Html.DisplayNameFor(model => model.Seodiscription) %>
        </th>
        <th></th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
        <td>
            <%: Html.DisplayFor(modelItem => item.Name) %>
        </td>
        <td>
            <%: Html.ActionLink("Edit", "Edit", new { id=item.Id }) %> |
            <%: Html.ActionLink("Show page", "Details", new { id=item.Id }) %> |
            <%: Html.ActionLink("Delete", "Delete", new { id=item.Id }) %>
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
          <%: Html.DisplayFor(modelItem => item.Seodiscription) %>
        </td>
    </tr>
<% } %>

</table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
