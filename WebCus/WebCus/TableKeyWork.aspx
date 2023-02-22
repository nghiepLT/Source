<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="TableKeyWork.aspx.cs" Inherits="WebCus.TableKeyWork" %>

<%@ Register Src="Payment/TTableKeyWork.ascx" TagName="TTableKeyWork" TagPrefix="uckey" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uckey:TTableKeyWork ID="TTableKeyWork1" runat="server" />    
</asp:Content>
