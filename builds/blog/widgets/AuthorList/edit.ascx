﻿<%@ control language="C#" autoeventwireup="true" inherits="Widgets.AuthorList.Edit, App_Web_k0xtoydu" %>
<div>
    <label style="display: block; margin: 10px 0 5px 0" for="<%=txtMaxCount %>"><%=Resources.labels.authorDispalyCount %></label>
    <asp:TextBox runat="server" ID="txtMaxCount" Width="300" />
</div>
<div>
    <label style="display: block; margin: 10px 0 5px 0" for="<%=txtDisplayPattern %>"><%=Resources.labels.authorDisplayPattern %></label>
    <asp:TextBox runat="server" ID="txtDisplayPattern" Width="300" />
</div>
<div>
    <label style="display: block; margin: 10px 0 5px 0" for="<%=txtDisplayPatternAggregated %>"><%=Resources.labels.authorDisplayPatternAggregated %></label>
    <asp:TextBox runat="server" ID="txtDisplayPatternAggregated" Width="300" />
</div>