<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryInfo01.ascx.cs"
    Inherits="NewsMng.CategoryInfo01" %>
<%@ Register Src="TextBoxMultiLanguage.ascx" TagName="TextBoxMultiLanguage" TagPrefix="pqt" %>
<%@ Register Src="FCKMultiLanguage.ascx" TagName="FCKMultiLanguage" TagPrefix="pqt" %>
<script type="text/javascript">

    function ShowCategory() {
        WindowOpen('RenderPopup.aspx?smid=UserMng&renderPage=FileUploadMng.ascx&UK=UploadFile', '', 900, 550, 'no');
        return false;
    }

    function CheckValidate() {



        return true;
    }

    function CheckDelete() {
        if (confirm('Do you want delete it?')) {
            if (<%=NewsCategoryID %> == -1) {
                alert('Pls select category to delete');
                return false;
            }

            return true;
        }
        else
            return false;
    }
    
</script>
<script type="text/javascript">

    var activeArea = null;
    var winW = 0;
    var winH = 0;
    var clickX = 0;
    var clickY = 0;
    function GetWindowSize() {
        if (navigator.appName.indexOf("Microsoft") != -1) {
            winH = document.body.clientHeight;
            winW = document.body.clientWidth;
        } else {
            winH = window.innerHeight;
            winW = window.innerWidth;
        }
    }
    function GetMouseXY(e) {
        if (navigator.appName.indexOf("Microsoft") != -1) { // grab the x-y pos.s if browser is IE
            clickX = e.clientX + document.body.scrollLeft
            clickY = e.clientY + document.body.scrollTop
        } else {  // grab the x-y pos.s if browser is NS
            clickX = e.pageX
            clickY = e.pageY
        }
        // catch possible negative values in NS4
        if (clickX < 0) clickX = 0;
        if (clickY < 0) clickY = 0;
        return true
    }

    function AreaContains(parent, child) {
        while (child)
            if (parent == child) return true;
            else
                child = child.parentNode;

        return false;
    }
    function PopupAreaMouseOut(e) {
        if (!e)
            var e = window.event;
        var targ = e.relatedTarget ? e.relatedTarget : e.toElement;
        if (activeArea != null && !AreaContains(activeArea, targ)) {
            activeArea.style.display = 'none';
            activeArea = null;
        }
    }

    function PopupArea(e) {
        // get viewing size 
        GetWindowSize();
        GetMouseXY(e);

        // hide the old area
        if (activeArea != null) {
            activeArea.style.display = 'none';
        }

        // pop-up area
        var popupArea = document.getElementById("divImageProduct");
        popupArea.style.position = 'absolute';
        popupArea.style.display = 'inline';
        popupArea.style.zIndex = "9999";
        popupArea.style.top = clickY - 78 + 'px';
        popupArea.style.left = clickX + 10 + 'px';

        popupArea.onmouseout = PopupAreaMouseOut;
        document.body.appendChild(popupArea);

        // keep the pop-up area
        activeArea = popupArea;
    }

    function ShowImage(event, isShow) {

        if (isShow == 0) {
            document.getElementById("divImageProduct").style.display = 'none';
            return false;
        } else {
            PopupArea(event);
            return false;
        }

    }

</script>
<div class="page-title">
    <h2 class="icon-title">
        <span>Nội dung</span>
    </h2>
</div>
<br/>
<table border="0" cellpadding="0" cellspacing="0" style="float:right;">
    <tbody>
        <tr>
            <td>
                &nbsp;
            </td>
            <td class="R">
                <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn-1" OnClientClick="return CheckValidate();"
                    OnClick="btnSave_Click" />
            </td>
            <td class="R">
                <asp:Button ID="btn_upload" runat="server" Text="Upload File" CssClass="btn-1" OnClientClick="return ShowCategory();"/>
                    
            </td>
        </tr>
    </tbody>
</table>
<div class="TboardBox">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td colspan="2" class="Line2">
            </td>
        </tr>
        <tr style="display:none;">
            <th class="RB_L" width="20%">
                <asp:Label ID="Label8" runat="server" Text="IsView"></asp:Label>
            </th>
            <td class="B_L" style="width: 80%;">
                <asp:RadioButton ID="rdoIsViewY" runat="server" GroupName="IsView" Text="Yes" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdoIsViewN" runat="server" GroupName="IsView" Text="No" />
            </td>
        </tr>
        <tr id="tr_img" runat="Server">
            <th class="RB_L" width="20%">
                <asp:Label ID="Label1" runat="server" Text="Image"></asp:Label>
            </th>
            <td class="B_L" width="30%" style="width: 80%">
                <table>
                    <tr>
                        <td onmouseover="return ShowImage(event, 1);" onmouseout="return ShowImage(event, 0);">
                            <asp:Label ID="LinkImg" runat="server" Text="" ForeColor="BlueViolet" Style="cursor: pointer"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:FileUpload ID="fileImage" runat="server" Width="300px" Height="24px" />
                <asp:LinkButton ID="lbtn_deleteImg" runat="server" OnClick="btnDeleteImg_Click" OnClientClick="return CheckDelete();">Xóa ảnh</asp:LinkButton>
            </td>
        </tr>
        <tr id="tr_emailad" runat="server">
            <th class="RB_L" width="20%">
                <asp:Label ID="Label2" runat="server" Text="Email"></asp:Label>
            </th>
            <td class="B_L" style="width: 80%;">
                  <asp:TextBox ID="emailad" runat="server"></asp:TextBox>
                   
            </td>
        </tr>
    </table>
    
    <pqt:TextBoxMultiLanguage ID="tbmlName" TextWidth="95%" Title="Tiêu đề" TitleWidth="20%"
        runat="server" />
    <div id="styleMota" runat="Server">
        <pqt:TextBoxMultiLanguage ID="tbmlMetaDescription" TextHeight="100px" Title="Mô tả"
        TextWidth="95%" TitleWidth="20%" TextMode="MultiLine" runat="server" />

        <pqt:FCKMultiLanguage ID="fckmlMetaDescription" ToolbarSet="Basic" TextWidth="95%" TextHeight="300px" Title="Mô tả"
        TitleWidth="20%" runat="server" />
    </div>
    <div id="divmeta" runat="server">
        <pqt:TextBoxMultiLanguage ID="txt_title_tag" TextWidth="95%" Title="Meta Title" TitleWidth="20%"
            runat="server" />
        <pqt:TextBoxMultiLanguage ID="txt_key_tag" TextWidth="95%" Title="Keyword tag" TitleWidth="20%"
                    runat="server" />
        <pqt:TextBoxMultiLanguage ID="txt_des_tag" TextWidth="95%" Title="Description tag" TitleWidth="20%"
            runat="server" />
    </div>

    
    <pqt:FCKMultiLanguage ID="fckmlDescription" TextWidth="95%" TextHeight="500px" Title="Nội dung"
        TitleWidth="20%" runat="server" />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="2" class="Line1">
            </td>
        </tr>
    </table>
</div>
<div id="divImageProduct" style="display: none;background-color: Gray; padding: 2px">
    <div style="padding: 3px; background-color: White">
        <asp:Image ID="imgProduct" runat="server" Width="300px" />
    </div>
</div>
