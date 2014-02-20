<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Jan_die_alles_kan.Models.UploadDownloadModel>>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>
    <table>
        <tr>
            <th>
                <%: Html.DisplayNameFor(model => model.Name) %>
            </th>
            <th>
                <%: Html.DisplayNameFor(model => model.Route) %>
            </th>
            <th>
                <%: Html.DisplayNameFor(model => model.MainCategorie) %>
            </th>
            <th>
                <%: Html.DisplayNameFor(model => model.MTime) %>
            </th>
            <th>
                <%: Html.DisplayNameFor(model => model.CTime) %>
            </th>
            <th>
                <%: Html.DisplayNameFor(model => model.Size) %>
            </th>
  
            <th></th>
        </tr>
    
    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%: Html.DisplayFor(modelItem => item.Name) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Route) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.MainCategorie) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.MTime) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.CTime) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Size) %>
            </td>

            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id=item.Id }) %> |
                <%: Html.ActionLink("Download File", "download", new { id=item.Id }, null) %> |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.Id }) %>
            </td>
        </tr>
    <% } %>
    
    </table>
</body>
</html>
