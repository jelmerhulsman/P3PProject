﻿<%@ Page Title="" validateRequest="false" Language="C#" MasterPageFile="~/Views/Shared/DashboardSite.Master" Inherits="System.Web.Mvc.ViewPage<Jan_die_alles_kan.Models.Category>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Send Mail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Send Mail</h2>
    <p><a href="/DashBoard/Index">Back to dashboard</a></p>

        <% using ( Html.BeginForm("SendMail2","Dashboard", FormMethod.Post, new { enctype = "multipart/form-data"} )) { %>
    To:<br>
    <select id="emaildropdown" name="email">
        <%foreach(string s in ViewBag.username){ %>
        <option><%:s %></option>
        <%} %>
    </select><br />
    subject:<br>
    <input type="text" name="subject" value="subject" /><br>
    content:<br>
    <textarea name="content" placeholder="content"></textarea>
    <br><br>
    <input type="submit" name="submit" value="Send" />

    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>