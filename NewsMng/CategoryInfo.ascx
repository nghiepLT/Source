<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryInfo.ascx.cs"
    Inherits="NewsMng.CategoryInfo" %>
<%@ Register Src="TextBoxMultiLanguage.ascx" TagName="TextBoxMultiLanguage" TagPrefix="pqt" %>
<%@ Register Src="FCKMultiLanguage.ascx" TagName="FCKMultiLanguage" TagPrefix="pqt" %>
<script type="text/javascript">

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
<br>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tbody>
        <tr>
            <td>
                &nbsp;
            </td>
            <td class="R">
                <asp:Button ID="btnDelete" runat="server" Text="Xóa" CssClass="btn-1" 
                    OnClick="btnDelete_Click" />
                &nbsp;
                <asp:Button ID="btnInsert" runat="server" Text="Tạo mới" CssClass="btn-1" OnClientClick="return CheckValidate();"
                    OnClick="btnInsert_Click" />
                &nbsp;
                <asp:Button ID="btnInsertSub" runat="server" Text="InsertSub" CssClass="btn-1" OnClientClick="return CheckValidate();"
                    OnClick="btnInsertSub_Click" />
                &nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn-1" OnClientClick="return CheckValidate();"
                    OnClick="btnSave_Click" />
            </td>
        </tr>
    </tbody>
</table>
<div class="TboardBox">
    <table width="100%">
        <tr>
            <td colspan="2" class="Line2">
            </td>
        </tr>
        <tr>
            <td width="20%" class="T" id="tdCategory" runat="server">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="pdL5 L ">
                            <asp:Panel ID="Panel1" Width="100%" ScrollBars="Auto" runat="server">
                                <asp:TreeView ID="treeCategory" runat="server" ShowLines="true" OnSelectedNodeChanged="treeCategory_SelectedNodeChanged">
                                    <SelectedNodeStyle CssClass="Link02" />
                                    <NodeStyle Font-Bold="true" />
                                </asp:TreeView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="T">
                <table width="100%" id="tblInfo" runat="server" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <th class="RB_L" width="20%">
                            <asp:Label ID="Label8" runat="server" Text="IsView"></asp:Label>
                        </th>
                        <td class="B_L" width="30%" style="width: 80%">
                            <asp:RadioButton ID="rdoIsViewY" runat="server" GroupName="IsView" Text="Yes" />
                            &nbsp;&nbsp;
                            <asp:RadioButton ID="rdoIsViewN" runat="server" GroupName="IsView" Text="No" />
                        </td>
                    </tr>
                    <tr>
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
                        </td>
                    </tr>
                    <tr>
                        <th class="RB_L" width="20%">
                            <asp:Label ID="Label5" runat="server" Text="SortOrder"></asp:Label>
                        </th>
                        <td class="B_L" width="30%" style="width: 80%">
                            <asp:TextBox ID="txtSortOrder" runat="server" Width="50px" CssClass="Input_text"
                                Text="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th class="RB_L" width="20%">
                            <asp:Label ID="Label6" runat="server" Text="ParentID"></asp:Label>
                            <asp:Label ID="lblRootID" runat="server" Text=""></asp:Label>
                        </th>
                        <td class="B_L" width="30%" style="width: 80%">
                            <asp:TextBox ID="txtParentID" runat="server" Width="50px" CssClass="Input_text"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th class="RB_L" width="20%">
                            <asp:Label ID="Label2" runat="server" Text="KeyWord"></asp:Label>
                        </th>
                        <td class="B_L" style="width: 80%">
                            <asp:TextBox ID="txtKetWord" runat="server" Width="50%" Text="Keyword" CssClass="Input_text"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th class="RB_L" width="20%">
                            <asp:Label ID="Label3" runat="server" Text="KeyWordad"></asp:Label>
                        </th>
                        <td class="B_L" style="width: 80%">
                            <asp:TextBox ID="txtKetWordad" runat="server" Width="50%" Text="Keyword" CssClass="Input_text"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <pqt:TextBoxMultiLanguage ID="tbmlName" TextWidth="95%" Title="Tiêu đề" TitleWidth="20%"
                    runat="server" />
                <pqt:FCKMultiLanguage ID="fckmlDescription" TextWidth="95%" TextHeight="300px" Title="Nội dung"
                    TitleWidth="20%" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="Line1">
            </td>
        </tr>
    </table>
    <div id="divImageProduct" style="display: none; background-color: Gray; padding: 2px">
        <div style="padding: 3px; background-color: White">
            <asp:Image ID="imgProduct" runat="server" Width="300px" Height="300px" />
        </div>
    </div>
</div>
