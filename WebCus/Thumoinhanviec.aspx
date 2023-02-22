<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Thumoinhanviec.aspx.cs" Inherits="WebCus.Thumoinhanviec" %>

<%@ Register Src="~/Payment/TTThumoinhanviec.ascx" TagPrefix="uc1" TagName="TTThumoinhanviec" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:TTThumoinhanviec runat="server" ID="TTThumoinhanviec" />
</asp:Content>
