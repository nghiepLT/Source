<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="sinhnhatnv.aspx.cs" Inherits="WebCus.sinhnhatnv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
#Table_01 th, #Table_01 td {padding:0 !important;}
</style>
<table id="Table_01" width="600" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td bgcolor="#FFFFFF">
			<img style="display:block" border="0" src="hapydayimages/img_01.png" width="125" height="160" alt=""></td>
		<td bgcolor="#FFFFFF">
			<img style="display:block" border="0" src="hapydayimages/img_02.png" width="400" height="160" alt=""></td>
		<td bgcolor="#FFFFFF">
			<img style="display:block" border="0" src="hapydayimages/img_03.png" width="75" height="160" alt=""></td>
	</tr>
	<tr>
		<td bgcolor="#FFFFFF">
			&nbsp;</td>
		<td style="border-left-style: solid; border-left-color: #898989; border-left-width: 1px; border-right-style: solid; border-right-color: #898989; border-right-width: 1px" bgcolor="#F4F4F4">
		<table border="0" width="100%" cellspacing="10" cellpadding="0">
			<tr>
				<td colspan="3" align="center" style="text-align:center;"><b><i>
				<font size="5" color="#800080">Nhân viên sinh nhật tháng <%=DateTime.Now.Month %></font></i></b></td>
			</tr>
			<tr>
				<td width="80">&nbsp;</td>
				<td>
				<div>
                  <asp:Label ID="lbl_name" runat="server"></asp:Label>
                 </div>
                 </td>
				<td width="20">&nbsp;</td>
			</tr>
			</table>
		</td>
		<td bgcolor="#FFFFFF">
			&nbsp;</td>
	</tr>
	<tr>
		<td bgcolor="#FFFFFF">
			<img style="display:block" border="0" src="hapydayimages/img_07.png" width="125" height="206" alt=""></td>
		<td bgcolor="#FFFFFF">
			<img style="display:block" border="0" src="hapydayimages/img_08.png" width="400" height="206" alt=""></td>
		<td bgcolor="#FFFFFF">			
			<img style="display:block" border="0" src="hapydayimages/img_09.png" width="75" height="206" alt=""></td>
	</tr>
	<tr>
		<td bgcolor="#FFFFFF" colspan="3">
			<div align="right">	
</div></td>
	</tr>
</table>
</asp:Content>
