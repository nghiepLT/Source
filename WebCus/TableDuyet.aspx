<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="TableDuyet.aspx.cs" Inherits="WebCus.TableDuyet" %>

<%@ Register Src="Payment/TTableDuyet.ascx" TagName="TTableDuyet" TagPrefix="uckey" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uckey:TTableDuyet ID="TTableDuyet1" runat="server" />
    
</asp:Content>
