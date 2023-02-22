<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master"
     AutoEventWireup="true" CodeBehind="Thongketuyendung.aspx.cs" Inherits="WebCus.Thongketuyendung" %>

<%@ Register Src="~/Payment/TTThongketuyendung.ascx" TagPrefix="uc1" TagName="TTThongketuyendung" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:TTThongketuyendung runat="server" ID="TTThongketuyendung" />
</asp:Content>
