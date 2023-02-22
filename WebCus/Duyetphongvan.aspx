<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
     CodeBehind="Duyetphongvan.aspx.cs" Inherits="WebCus.Duyettuyendung" %>

<%@ Register Src="~/Payment/TTDuyetPV.ascx" TagPrefix="uc1" TagName="TTDuyetPV" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:TTDuyetPV runat="server" id="TTDuyetPV" />
</asp:Content>
