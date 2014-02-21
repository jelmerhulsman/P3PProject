<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Jan_die_alles_kan.Models.PagesModels>>" %>
                    <nav>
                        <ul id="menu">
                            <%: Html.DropDownList("MenuList") %>
                           <%foreach (var item in Model) {%>
                            <li><%: Html.ActionLink("Home", "Index", "Page") %></li>
                            <li><%: Html.ActionLink("About", "About", "Page") %></li>
                            <li><%: Html.ActionLink("Contact", "Contact", "Page") %></li>
                           <% } %>
                        </ul>
                    </nav>
