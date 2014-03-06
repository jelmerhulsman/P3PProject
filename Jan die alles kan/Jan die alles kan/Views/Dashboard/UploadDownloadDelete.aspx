<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.UploadDownloadModel>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Delete</title>
</head>
<body>
    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
        <legend>UploadDownloadModel</legend>
    
        <div class="display-label">
            <%: Html.DisplayNameFor(model => model.Name) %>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.Name) %>
        </div>
    
        <div class="display-label">
            <%: Html.DisplayNameFor(model => model.Route) %>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.Route) %>
        </div>
    
        <div class="display-label">
            <%: Html.DisplayNameFor(model => model.MainCategorie) %>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.MainCategorie) %>
        </div>
    
        <div class="display-label">
            <%: Html.DisplayNameFor(model => model.MTime) %>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.MTime) %>
        </div>
    
        <div class="display-label">
            <%: Html.DisplayNameFor(model => model.CTime) %>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.CTime) %>
        </div>
    
        <div class="display-label">
            <%: Html.DisplayNameFor(model => model.Size) %>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.Size) %>
        </div>
    
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <%: Html.AntiForgeryToken() %>
        <p>
            <input type="submit" value="Delete" /> |
            <%: Html.ActionLink("Back to List", "UploadDownloadIndex") %>
        </p>
    <% } %>
    
</body>
</html>
