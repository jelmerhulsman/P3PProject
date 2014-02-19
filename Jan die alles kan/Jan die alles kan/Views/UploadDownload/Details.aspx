<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.UploadDownloadModel>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Details</title>
</head>
<body>
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
    
        <div class="display-label">
            <%: Html.DisplayNameFor(model => model.Data) %>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.Data) %>
        </div>
    </fieldset>
    <p>
    
        <%: Html.ActionLink("Edit", "Edit", new { id=Model.Id }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
</body>
</html>
