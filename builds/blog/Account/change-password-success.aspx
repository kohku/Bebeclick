<%@ page title="Change Password" language="C#" masterpagefile="account.master" autoeventwireup="true" inherits="Account.ChangePasswordSuccess, App_Web_5wobj0ay" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="page-header clearfix">
        <h3>
            <%=Resources.labels.changePassword %>
        </h3>
    </div>
    <div id="ChangePwd" class="alert alert-success">
        <%=Resources.labels.passwordChangeSuccess %>
    </div>
</asp:Content>
