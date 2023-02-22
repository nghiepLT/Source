<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RenderPopupCapNhatTrangThai.aspx.cs" Inherits="WebCus.RenderPopupCapNhatTrangThai" %>

<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.1/css/all.min.css" integrity="sha512-MV7K8+y+gLIBoVD59lQIYicR65iaqukzvf/nwasF0nqhPay5w/9lJmVM2hMDcnK1OnMGCdVK+iQrJ7lzPJQd1w==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Anton&family=Lobster&family=Roboto:wght@300;500;700;900&display=swap" rel="stylesheet">

    <div>
        <div>
            <input type="radio" id="rad1" name="radchung" runat="server" />
            <span>Đang thử việc</span>
        </div>
        <div>
            <input type="radio" id="rad2" runat="server" name="radchung" />
            <span>Nhân viên chính thức</span>
        </div>
        <div>
            <input type="radio" id="rad3" runat="server"  name="radchung"/>
            <span>Không phù hợp</span>
        </div>
    </div>
    <div>
        <asp:button id="btnSaveBanner" runat="server" text="Duyệt" cssclass="btnduyet" onclick="btnSaveBanner_Click"
            onclientclick="return CheckValidBanner();" />
    </div>
    <style>
        .btnduyet {
            -moz-border-radius: 4px 4px 4px 4px;
            background: url(images/common/bkg-btn-blue.gif) repeat-x scroll 0 top #3A8FCE;
            border-color: #508FCD #4483BF #2F6EA7 #3F7EB9;
            border-style: solid;
            border-width: 1px;
            color: #FFFFFF;
            letter-spacing: -0.03em;
            padding: 3px 10px;
            text-align: center;
            text-shadow: 0 -1px 2px #2063ab;
            font-size: 15px;
            float: right;
            cursor:pointer;
        }
    </style>
</asp:Content>
