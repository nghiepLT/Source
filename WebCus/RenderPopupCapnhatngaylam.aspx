<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RenderPopupCapnhatngaylam.aspx.cs" Inherits="WebCus.RenderPopupCapnhatngaylam" %>

<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="AdminCss/admin.css" rel="stylesheet" />
  <div style="padding:10px;">
       <div>
        <radCln:RadDatePicker ID="txtDateTo" CssClass="datePicker" Width="220px" AllowEmpty="false"
        MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
        <DateInput DisplayPromptChar="_" PromptChar=" " onclick="ToggleSecondPopup()" DateFormat="dd/MM/yyyy">
        </DateInput>
        <PopupButton Visible="False"></PopupButton>
    </radCln:RadDatePicker>
             <asp:button id="btnSaveBanner" runat="server" text="Cập nhật" cssclass="btnduyet" onclick="btnSaveBanner_Click"
                onclientclick="return CheckValidBanner();" />
   </div>
     
  </div>
    <script>
        function ToggleSecondPopup()
        {
            <%= txtDateTo.ClientID %>.ShowPopup();
         }
    </script>
    <style>
        body{
            background-color:#ffffff;
        }
    </style>
</asp:Content>
