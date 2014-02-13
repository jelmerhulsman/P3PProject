<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.PagesModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Details</h2>

<fieldset>
    <legend>PagesModels</legend>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.Name) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Name) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.Permalink) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Permalink) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.Content) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Content) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.Status) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Status) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.Pageposition) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Pageposition) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.Seokey) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Seokey) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.Seodiscription) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Seodiscription) %>
    </div>
</fieldset>
<p>

    <%: Html.ActionLink("Edit", "Edit", new { id=Model.Id }) %> |
    <%: Html.ActionLink("Back to List", "Index") %>
</p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
