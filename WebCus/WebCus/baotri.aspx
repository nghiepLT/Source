<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="baotri.aspx.cs" Inherits="WebCus.baotri" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:TextBox ID="valuebt" runat="server"> </asp:TextBox>--%> 
    <asp:Button ID="btn_updatabaotri" runat="server" Text="ON"
        onclick="btn_updatabaotri_Click" />
        <asp:Label ID="lal_tile" runat="server"> </asp:Label>
</asp:Content>
