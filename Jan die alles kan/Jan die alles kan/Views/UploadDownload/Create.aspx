<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.UploadDownloadModel>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Create</title>
</head>
<body>
    <script src="<%: Url.Content("~/Scripts/jquery-1.8.2.min.js") %>"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>"></script>
    
    <% using (Html.BeginForm()) { %>
        <%: Html.AntiForgeryToken() %>
        <%: Html.ValidationSummary(true) %>
    
        <fieldset>
            <legend>UploadDownloadModel</legend>
    
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.Name) %>
                <%: Html.ValidationMessageFor(model => model.Name) %>
            </div>
    
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Route) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.Route) %>
                <%: Html.ValidationMessageFor(model => model.Route) %>
            </div>
    
            <div class="editor-label">
                <%: Html.LabelFor(model => model.MainCategorie) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.MainCategorie) %>
                <%: Html.ValidationMessageFor(model => model.MainCategorie) %>
            </div>

    
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Size) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.Size) %>
                <%: Html.ValidationMessageFor(model => model.Size) %>
            </div>
    
            
    
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>
    <% } %>
    
    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>
</body>
</html>
