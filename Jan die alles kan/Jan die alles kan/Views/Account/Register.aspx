<%@ Page Language="C#" MasterPageFile="~/Views/Shared/AccountSite.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.RegisterModel>" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Register
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1>Register.</h1>
        <h2>Create a new account.</h2>
    </hgroup>

    <% using (Html.BeginForm()) { %>
        <%: Html.AntiForgeryToken() %>
        <%: Html.ValidationSummary() %>

        <fieldset>
            <legend>Registration Form</legend>
            <ol>
                <li>
                    <%: Html.LabelFor(m => m.UserName) %>
                    <%: Html.TextBoxFor(m => m.UserName) %>
                </li>
                <li>
                    <%: Html.LabelFor(m => m.Password) %>
                    <%: Html.PasswordFor(m => m.Password) %>
                </li>
                <li>
                    <%: Html.LabelFor(m => m.ConfirmPassword) %>
                    <%: Html.PasswordFor(m => m.ConfirmPassword) %>
                </li>
                <li>
                    <%: Html.LabelFor(m => m.Email) %>
                    <%: Html.TextBoxFor(m => m.Email) %>
                </li>
                <li>
                    <%: Html.LabelFor(m => m.Street) %>
                    <%: Html.TextBoxFor(m => m.Street) %>
                </li>
                <li>
                    <%: Html.LabelFor(m => m.HouseNumber) %>
                    <%: Html.TextBoxFor(m => m.HouseNumber) %>
                </li>
                <li>
                    <%: Html.LabelFor(m => m.City) %>
                    <%: Html.TextBoxFor(m => m.City) %>
                </li>
                <li>
                    <%: Html.LabelFor(m => m.PostalCode) %>
                    <%: Html.TextBoxFor(m => m.PostalCode) %>
                </li>
            </ol>
            <input type="submit" value="Register" />
        </fieldset>
    <% } %>
</asp:Content>

<asp:Content ID="scriptsContent" ContentPlaceHolderID="ScriptsSection" runat="server">
    <%: Scripts.Render("~/bundles/jqueryval") %>
</asp:Content>
