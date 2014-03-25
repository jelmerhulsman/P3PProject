<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DashboardSite.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.PictureModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit <%: Html.DisplayFor(model => model.Name) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <p><a href="/Dashboard/Index">Back to dashboard</a></p>
    <% using (Html.BeginForm())
       { %>
    <%: Html.AntiForgeryToken() %>
    <%: Html.ValidationSummary(true) %>

    <h2>Edit image "<%: Html.DisplayFor(model => model.Name) %>"</h2>
    <fieldset>
        <legend>PictureModel</legend>

        <%: Html.HiddenFor(model => model.Id) %>
        <%: Html.HiddenFor(model => model.CTime) %>
        <%: Html.HiddenFor(model => model.File_name) %>
        <%: Html.HiddenFor(model => model.Category) %>
        <%: Html.HiddenFor(model => model.Orientation) %>

        <div class="editor-label">
            Name
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Name) %>
            <%: Html.ValidationMessageFor(model => model.Name) %>
        </div>

        <div class="editor-label">
            Category
        </div>
        <div class="editor-field read-only">
            <%: Html.DisplayFor(model => model.Category) %>
            <%: Html.ValidationMessageFor(model => model.Category) %>
        </div>

        <div class="editor-label">
            Color
        </div>
        <div class="editor-field">
            <select name="Color">
                <option value="<%: Html.ValueFor(model => model.Color) %>">Current: <%: Html.ValueFor(model => model.Color) %></option>
                <option value="">None Specific</option>
                <option value="red">Red</option>
                <option value="orange">Orange</option>
                <option value="yellow">Yellow</option>
                <option value="green">Green</option>
                <option value="cyan">Cyan</option>
                <option value="blue">Blue</option>
                <option value="purple">Purple</option>
                <option value="pink">Pink</option>
                <option value="white">White</option>
                <option value="grey">Grey</option>
                <option value="black">Black</option>
                <option value="brown">Brown</option>
            </select><br>
            <%: Html.ValidationMessageFor(model => model.Color) %>
        </div>

        <div class="editor-label">
            Description
        </div>
        <div class="editor-field">
            <%: Html.TextAreaFor(model => model.Description) %>
            <%: Html.ValidationMessageFor(model => model.Description) %>
        </div>

        <div class="editor-label">
            Price
        </div>
        <div class="editor-field">
            <input type="number" name="Price" min="1" max="30" step="1" value="<%: Html.ValueFor(model => model.Price) %>">
            <%: Html.ValidationMessageFor(model => model.Price) %>
        </div>

        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "ImageIndex") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
    <%: Scripts.Render("~/bundles/jqueryval") %>
</asp:Content>
