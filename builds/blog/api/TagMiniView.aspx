﻿<%@ page language="C#" autoeventwireup="true" inherits="Api.TagMiniView, App_Web_in32naep" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tag View</title>
    <link href="../api/wlw.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Tags</h1>
        <blog:TagCloud ID="TagCloud1" MinimumPosts="1" TagCloudSize="-1" runat="server" />
    </div>
    </form>
</body>
</html>
