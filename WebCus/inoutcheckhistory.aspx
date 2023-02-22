<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="inoutcheckhistory.aspx.cs" Inherits="WebCus.inoutcheckhistory" %>
<%@ Register src="Payment/historycheckinout.ascx" tagname="historycheckinout" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       
    <uc1:historycheckinout ID="historycheckinout1" runat="server" />
       
</asp:Content>
