<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DashboardSite.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.PictureModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete <%: Html.DisplayFor(model => model.Name) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete image "<%: Html.DisplayFor(model => model.Name) %>"</h2>

    <h3>Are you sure you want to delete "<%: Html.DisplayFor(model => model.Name) %>"?</h3>
    <fieldset>
        <legend>PictureModel</legend>

        <div class="display-label">
            <h4>Name</h4>
        </div>

        <div class="display-field">
            <%: Html.DisplayFor(model => model.Name) %>
        </div>

        <div class="display-label">
            <h4>Category</h4>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.Category) %>
        </div>

        <div class="display-label">
            <h4>Modification Time</h4>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.MTime) %>
        </div>

        <div class="display-label">
            <h4>Creation Time</h4>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.CTime) %>
        </div>

        <div class="display-label">
            <h4>Color</h4>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.Color) %>
        </div>

        <div class="display-label">
            <h4>Description</h4>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.Description) %>
        </div>

        <div class="display-label">
            <h4>File Name</h4>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.File_name) %>
        </div>

        <div class="display-label">
            <h4>Price</h4>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.Price) %>
        </div>
    </fieldset>
    <% using (Html.BeginForm())
       { %>
    <%: Html.AntiForgeryToken() %>
    <p>
        <input type="submit" value="Delete Image" />
        |
        <%: Html.ActionLink("Back to List", "ImageIndex") %>
    </p>
    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
