<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
     CodeBehind="Danhgiaungvien.aspx.cs" Inherits="WebCus.Danhgiaungvien" %>

<%@ Register Src="~/Payment/TTDanhgiaungvien.ascx" TagPrefix="uc1" TagName="TTDanhgiaungvien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:TTDanhgiaungvien runat="server" ID="TTDanhgiaungvien" />

</asp:Content>
