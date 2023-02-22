<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ThongKeNgayPhep.aspx.cs" Inherits="WebCus.ThongKeNgayPhep" %>

<%@ Register Src="Payment/TThongKePhep.ascx" TagName="TThongKePhep" TagPrefix="uckey" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uckey:TThongKePhep ID="TThongKePhep1" runat="server" />
</asp:Content>
