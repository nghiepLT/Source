<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="TableNhanVien.aspx.cs" Inherits="WebCus.TableNhanVien" %>

<%@ Register Src="Payment/TTNhanvien.ascx" TagName="TTNhanvien" TagPrefix="uckey" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uckey:TTNhanvien ID="TTNhanvien1" runat="server" />
    
</asp:Content>
