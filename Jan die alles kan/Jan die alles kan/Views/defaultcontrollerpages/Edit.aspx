<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.PagesModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Edit</h2>

<% using (Html.BeginForm()) { %>
    <%: Html.AntiForgeryToken() %>
    <%: Html.ValidationSummary(true) %>

    <fieldset>
        <legend>PagesModels</legend>

        <%: Html.HiddenFor(model => model.Id) %>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Name) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Name) %>
            <%: Html.ValidationMessageFor(model => model.Name) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Permalink) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Permalink) %>
            <%: Html.ValidationMessageFor(model => model.Permalink) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Content) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Content) %>
            <%: Html.ValidationMessageFor(model => model.Content) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Status) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Status) %>
            <%: Html.ValidationMessageFor(model => model.Status) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Pageposition) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Pageposition) %>
            <%: Html.ValidationMessageFor(model => model.Pageposition) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Seokey) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Seokey) %>
            <%: Html.ValidationMessageFor(model => model.Seokey) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Seodiscription) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Seodiscription) %>
            <%: Html.ValidationMessageFor(model => model.Seodiscription) %>
        </div>

        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
<% } %>

<div>
    <%: Html.ActionLink("Back to List", "Index") %>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
    <%: Scripts.Render("~/bundles/jqueryval") %>
</asp:Content>
