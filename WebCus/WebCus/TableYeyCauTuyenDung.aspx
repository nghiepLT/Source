<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" 
    CodeBehind="TableYeyCauTuyenDung.aspx.cs" Inherits="WebCus.TableYeyCauTuyenDung" %>

<%@ Register Src="~/Payment/TTYeuCauTuyenDung.ascx" TagPrefix="uc1" TagName="TTYeuCauTuyenDung" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:TTYeuCauTuyenDung runat="server" ID="TTYeuCauTuyenDung" />
</asp:Content>
