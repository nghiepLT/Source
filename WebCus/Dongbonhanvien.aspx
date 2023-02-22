<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
     CodeBehind="Dongbonhanvien.aspx.cs" Inherits="WebCus.Dongbonhanvien" %>

<%@ Register Src="~/Payment/TTDongbonhanvien.ascx" TagPrefix="uc1" TagName="TTDongbonhanvien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:TTDongbonhanvien runat="server" ID="TTDongbonhanvien" />
</asp:Content>
