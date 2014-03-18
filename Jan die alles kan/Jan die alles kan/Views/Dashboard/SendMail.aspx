<%@ Page Title="" validateRequest="false" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    MailSend
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>MailSend</h2>
    <p><a href="/DashBoard">Back to dashboard</a></p>

        <% using ( Html.BeginForm("SendMail","Mail", FormMethod.Post, new { enctype = "multipart/form-data"} )) { %>
    Email-adress:<br>
    <input type="text" name="email" value="email" /><br>
    subject:<br>
    <input type="text" name="subject" value="subject" /><br>
    content:<br>
    <textarea name="content" placeholder="content"></textarea>
    <br><br>
    <input type="submit" name="submit" value="Upload" />

    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>