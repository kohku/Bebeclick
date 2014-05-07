<%@ control language="C#" autoeventwireup="true" inherits="Widgets.ModeComments.Edit, App_Web_jhe25evh" %>

<label for="<%=cbShowComments.ClientID %>"><%=Resources.labels.showComments %></label><br />
<asp:CheckBox runat="Server" ID="cbShowComments" />

<br /><br />

<label for="<%=txtSize.ClientID %>"><%=Resources.labels.avatarSize %></label><br />
<asp:TextBox runat="Server" ID="txtSize" />
<asp:CompareValidator runat="Server" ControlToValidate="txtSize" Operator="dataTypeCheck" Type="integer" ErrorMessage="<%$Resources:labels, enterValidNumber %>" />

<br /><br />

<label for="<%=txtNumber.ClientID %>"><%=Resources.labels.numberOfCommenters %></label><br />
<asp:TextBox runat="Server" ID="txtNumber" />
<asp:CompareValidator runat="Server" ControlToValidate="txtNumber" Operator="dataTypeCheck" Type="integer" ErrorMessage="<%$Resources:labels, enterValidNumber %>" />

<br /><br />

<label for="<%=txtDays.ClientID %>"><%=Resources.labels.maxAgeInDays %></label><br />
<asp:TextBox runat="Server" ID="txtDays" />
<asp:CompareValidator runat="Server" ControlToValidate="txtDays" Operator="dataTypeCheck" Type="integer" ErrorMessage="<%$Resources:labels, enterValidNumber %>" />