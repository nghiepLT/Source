<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="TestCapcha.aspx.cs" Inherits="WebCus.TestCapcha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

       <asp:Image ID="Image1" runat="server"  Style="vertical-align: middle;"  ImageUrl="/ImageValidator.aspx?code=0" /> 
       <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
</asp:Content>
