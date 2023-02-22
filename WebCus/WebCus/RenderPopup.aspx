<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RenderPopup.aspx.cs" Inherits="WebCus.RenderPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Render popup</title>
    <link href="/AdminCss/admin.css" rel="stylesheet" type="text/css"/>
	<script type="text/javascript" src="Include/JS/CommonUtilities.js"></script>

	<script type="text/javascript" src="Include/JS/CommPrototype.js"></script>

    <link href="css/styleAdmin.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Gray/default.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-1.8.2.js" type="text/javascript"></script>
</head>
<body style="background-color:#fff;">
	<form id="form1" runat="server">
		<div>
            <asp:toolkitscriptmanager ID="Toolkitscriptmanager1" runat="server"></asp:toolkitscriptmanager>
			<%--<%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
			<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td id="RenderCell" runat="server" style="height: 27px; padding: 5px">
					</td>
				</tr>
			</table>
		</div>
	</form>
</body>
</html>

