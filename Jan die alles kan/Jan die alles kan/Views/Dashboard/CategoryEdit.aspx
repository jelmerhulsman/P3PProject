<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DashboardSite.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.Category>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit <%: Html.DisplayFor(model => model.Name) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



<% using (Html.BeginForm()) { %>
    <%: Html.AntiForgeryToken() %>
    <%: Html.ValidationSummary(true) %>
    <h2>Edit Category "<%: Html.DisplayFor(model => model.Name) %>"</h2>
    <fieldset>
        <legend>Category</legend>

        <%: Html.HiddenFor(model => model.ID) %>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Name) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Name) %>
            <%: Html.ValidationMessageFor(model => model.Name) %>
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
