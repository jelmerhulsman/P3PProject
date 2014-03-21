<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DashboardSite.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.Category>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete <%: Html.DisplayFor(model => model.Name) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete "<%: Html.DisplayFor(model => model.Name) %>" Category?</h2>

    <h3><%
            string dir = Server.MapPath("~/Images/Categories/" + Model.Name);
            string[] files;
            int numFiles = 0;
            files = System.IO.Directory.GetFiles(dir);
            numFiles = files.Length;

            if (numFiles == 1)
                Response.Write("WARNING, This folder still contains " + numFiles + " image! Are you sure? ");
            else if (numFiles > 1)
                Response.Write("WARNING, This folder still contains " + numFiles + " images! Are you sure? ");
            else
                Response.Write("Are you sure you want to delete this category?");
    %></h3>




<% using (Html.BeginForm()) { %>
    <%: Html.AntiForgeryToken() %>
    <p>
            <%  if( numFiles < 1) { %>
        <input type="submit" value="Delete Category" />
        <% } else { %>
        <a href="/Dashboard/PageIndex"> Click here to remove the image(s) manually.</a>
        <% } %>
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
