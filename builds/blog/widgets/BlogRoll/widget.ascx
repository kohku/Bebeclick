<%@ Import Namespace="BlogEngine.Core" %>
<%@ control language="C#" autoeventwireup="true" inherits="Widgets.BlogRoll.Widget, App_Web_hszr42ik" %>
<blog:Blogroll ID="Blogroll1" runat="server" />
<a href="<%=Utils.AbsoluteWebRoot %>opml.axd" style="display: block; text-align: right"
    title="<%=Resources.labels.downloadOPML %>"><%=Resources.labels.downloadOPML %> <img src="<%=Utils.ApplicationRelativeWebRoot %>pics/opml.png" width="12" height="12"
        alt="OPML" /></a>