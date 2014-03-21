<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DashboardSite.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.PagesModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit <%: Html.DisplayFor(model => model.Name) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit page "<%: Html.DisplayFor(model => model.Name) %>"</h2>
    <p>
        <%: Html.ActionLink("Back to dashboard", "Index") %>
    </p>
    <% using (Html.BeginForm())
       { %>
    <%: Html.AntiForgeryToken() %>
    <%: Html.ValidationSummary(true) %>

    <fieldset>
        <legend>PagesModels</legend>

        <%: Html.HiddenFor(model => model.Id) %>

        <div class="editor-label">
            Name
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Name) %>
            <%: Html.ValidationMessageFor(model => model.Name) %>
        </div>

        <div class="editor-label">
            Permalink
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Permalink) %>
            <%: Html.ValidationMessageFor(model => model.Permalink) %>
        </div>
        <div class="editor-label">
            Content
        </div>
        <div class="editor-field">
            <%: Html.TextAreaFor(model => model.Content) %>
            <%: Html.ValidationMessageFor(model => model.Content) %>
        </div>

        <div class="editor-label">
            Satus
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Status) %>
            <%: Html.ValidationMessageFor(model => model.Status) %>
        </div>

        <div class="editor-label">
            Page Position
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Pageposition) %>
            <%: Html.ValidationMessageFor(model => model.Pageposition) %>
        </div>

        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "PageIndex") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
    <%: Scripts.Render("~/bundles/jqueryval") %>
</asp:Content>
