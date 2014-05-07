<%@ page title="" language="C#" masterpagefile="admin.master" autoeventwireup="true" inherits="Admin.Extensions.Settings, App_Web_ab34kp2c" %>
<%@ Reference Control = "Settings.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" Runat="Server">
    <div class="content-box-outer">
		<div class="content-box-left">
            <asp:PlaceHolder ID="ucPlaceHolder" runat="server"></asp:PlaceHolder>
            <asp:HiddenField ID="args" runat="server" />
		</div>
	</div>
</asp:Content>
