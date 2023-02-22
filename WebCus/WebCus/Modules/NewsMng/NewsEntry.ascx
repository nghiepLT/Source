<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsEntry.ascx.cs" Inherits="NewsMng.NewsEntry" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="TextBoxMultiLanguage.ascx" TagName="TextBoxMultiLanguage" TagPrefix="pqt" %>
<%@ Register Src="FCKMultiLanguage.ascx" TagName="FCKMultiLanguage" TagPrefix="pqt" %>
<%@ Register Src="ckeditor.ascx" TagName="ckeditor" TagPrefix="Nck" %>
<style type="text/css" >
    .Css_cblist label
    {
        padding:0 10px 0 5px;
    }
</style>
<script type="text/javascript">
    function ShowPopupImgMap() {
        WindowOpen('RenderPopup.aspx?smid=NewsMng&renderPage=NewsControl.ascx&md=PopupImgList&ID=' + <%=this.NewsID %>+'&PathImg=NewsImagePath&ConfigSizeImg=CommonImageSize&Key=<%=this.UK %>', 'PopupImgList', 1000, 550, 'yes');
        return true;
    }
</script>
<script type="text/javascript">
    function selectedTab(tabId) {
        document.getElementById("<%= hidSelectedTab.ClientID %>").value = tabId;
        SetVisibledTab(tabId);
        for (i = 1; i <= 3; i++) {
            document.getElementById('L' + i).style.backgroundImage = 'URL(images/icon/TabLeft2.gif)';
            document.getElementById(i).style.backgroundImage = 'URL(images/icon/TabCenter2.gif)';
            document.getElementById('R' + i).style.backgroundImage = 'URL(images/icon/TabRight2.gif)';
        }
        document.getElementById('L' + tabId).style.backgroundImage = 'URL(images/icon/TabLeft1.gif)';
        document.getElementById(tabId).style.backgroundImage = 'URL(images/icon/TabCenter1.gif)';
        document.getElementById('R' + tabId).style.backgroundImage = 'URL(images/icon/TabRight1.gif)';

        return false;
    }
    function selectedTabOnLoad() {
        var tabId = document.getElementById("<%= hidSelectedTab.ClientID %>").value;
        SetVisibledTab(tabId);
        for (i = 1; i <= 3; i++) {
            document.getElementById('L' + i).style.backgroundImage = 'URL(images/icon/TabLeft2.gif)';
            document.getElementById(i).style.backgroundImage = 'URL(images/icon/TabCenter2.gif)';
            document.getElementById('R' + i).style.backgroundImage = 'URL(images/icon/TabRight2.gif)';
        }
        document.getElementById('L' + tabId).style.backgroundImage = 'URL(images/icon/TabLeft1.gif)';
        document.getElementById(tabId).style.backgroundImage = 'URL(images/icon/TabCenter1.gif)';
        document.getElementById('R' + tabId).style.backgroundImage = 'URL(images/icon/TabRight1.gif)';
    }

    function SetVisibledTab(tabId) {
        var divGeneral = document.getElementById('divGeneral');
        var divData = document.getElementById('divData');
        var divComment = document.getElementById('divComment');
        if (tabId == 1) {
            divGeneral.style.display = ''
            divData.style.display = 'none'
            divComment.style.display = 'none'
        }
        else if (tabId == 2){
            divGeneral.style.display = 'none'
            divData.style.display = ''
            divComment.style.display = 'none'
        }
        else {
            divGeneral.style.display = 'none'
            divData.style.display = 'none'
            divComment.style.display = ''
        }
    }

    function ShowPopupNewReleated() {
    
        var hdnReleatedNews = document.getElementById('<%=hdnReleatedNews.ClientID %>');
        WindowOpen('RenderPopup.aspx?smid=NewsMng&renderPage=PopupNewsReleated.ascx&id=' + <%=this.NewsID %>, 'NewsReleated', 950, 550, 'no');
        return false;
    }

    
    function ReloadNewsReleated() {
        var btnRefesh = document.getElementById('<%=btnRefesh.ClientID %>');
        window.location = btnRefesh.href;
    }


    function ReloadCategory(ids) {
        var hdnCategoryIDs = document.getElementById('<%=hdnCategoryIDs.ClientID %>');
        var btnRefesh = document.getElementById('<%=btnRefesh.ClientID %>');
        hdnCategoryIDs.value = ids;
        btnRefesh.click();
    }

    function ConfirmDelete() {
        return confirm('Do you want delete it');
    }

    function fireEvent(obj,evt){
	
	var fireOnThis = obj;
	if( document.createEvent ) {
	  var evObj = document.createEvent('MouseEvents');
	  evObj.initEvent( evt, true, false );
	  fireOnThis.dispatchEvent(evObj);
	} else if( document.createEventObject ) {
	  fireOnThis.fireEvent('on'+evt);
	}
}


    ///////////////////////////////////////////////////////////////////////////
 
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

    function CheckValidate() {
        var check = false;
        var objArrFile = document.getElementsByTagName('input');
        for (i = 0; i < objArrFile.length; i++) {
            if (objArrFile[i].type == 'text') {
                if (objArrFile[i].id.toString().indexOf('txtName') >= 0 && objArrFile[i].value != '')
                    check = true;
            }
        }
        if (!check) {
            alert('Nhập tiêu đề');
            return false;
        }
        if ('<%=this.GetArea_terri %>' == "true") {
            var objArea = document.getElementById('<%=ddl_area.ClientID %>')
            if (objArea.value == -1) {
                alert('Chưa chọn khu vực');
                return false;
            }
            var objTerri = document.getElementById('<%=ddl_territory.ClientID %>')
            if (objTerri.value == -1) {
                alert('Chưa chọn quận/huyện');
                return false;
            }
        }
        
        return true;
    }

</script>
<div class="page-title">
    <h2 class="icon-title">
        <span>Cập nhật bài viết</span>
    </h2>
</div>
<br/>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tbody>
        <tr>
            <td class="C">
            </td>
            <td class="R">
                <asp:Button ID="btnDelete" runat="server" Text="Xóa" CssClass="btn-1" OnClick="btnDelete_Click"
                    OnClientClick="return ConfirmDelete();" />
                &nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn-1" OnClick="btnSave_Click"
                    OnClientClick="return CheckValidate();" />
                &nbsp;
                <asp:Button ID="btnList" runat="server" Text="Về danh sách" CssClass="btn-1" OnClick="btnList_Click" />
                &nbsp;
                <asp:Button ID="btnSelectImgMap" Visible="false" OnClientClick="return ShowPopupImgMap();"
                        runat="server" Text="Danh sách hình ảnh" CssClass="btn-1" />
            </td>
        </tr>
    </tbody>
</table>
<div class="TboardBox">
    <asp:HiddenField ID="hdnReleatedNews" Value="" runat="server" />
    <asp:HiddenField ID="hdnCategoryIDs" Value="" runat="server" />
    <asp:LinkButton ID="btnRefesh" OnClick="btnRefesh_Click" runat="server" Style="display: none;"></asp:LinkButton>
    <asp:HiddenField ID="hidSelectedTab" Value="2" runat="server" />
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td class="Line2">
            </td>
        </tr>
    </table>
    <div id="divGeneral">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            
            <tr>
                <th class="RB_L" width="20%">
                    <asp:Label ID="lblDateMngsd" runat="server" Text="Ngày hiển thị tin" Font-Bold="true"></asp:Label>
                </th>
                <th class="B_L" width="30%">
                    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <cc1:calendarextender id="CalendarExtender1" targetcontrolid="txtDateMng" runat="server" />--%>
                    <asp:TextBox ID="txtDateMng" CssClass="Input_text" Width="40%" runat="server"></asp:TextBox>
                MM-yyyy </th>
            </tr>
            <tr style="display: none;">
                <th class="RB_L" width="20%">
                    <asp:Label ID="lblDateMng0" runat="server" Text="RegUser" Font-Bold="true"></asp:Label>
                </th>
                <td class="B_L" width="30%">
                    <asp:Label ID="lblRegUser" runat="server"></asp:Label>
                </td>
                <th class="RB_L" width="20%">
                    <asp:Label ID="Label3" runat="server" Text="Reg Date" Font-Bold="true"></asp:Label>
                </th>
                <td class="B_L" width="30%">
                    <asp:Label ID="lblRegDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="display: none;">
                <th class="RB_L" width="20%">
                    <asp:Label ID="Label17" runat="server" Text="ModifyUser" Font-Bold="true"></asp:Label>
                </th>
                <td class="B_L" width="30%">
                    <asp:Label ID="lblModifyUser" runat="server"></asp:Label>
                </td>
                <th class="RB_L" width="20%">
                    <asp:Label ID="Label21" runat="server" Text="Modify Date" Font-Bold="true"></asp:Label>
                </th>
                <td class="B_L" width="30%">
                    <asp:Label ID="lblModifyDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="display: none;">
                <th class="RB_L" width="20%">
                    <asp:Label ID="Label18" runat="server" Text="IPAdd" Font-Bold="true"></asp:Label>
                </th>
                <td class="B_L" width="30%">
                    <asp:Label ID="lblIPAdd" runat="server"></asp:Label>
                </td>
                <th class="RB_L" width="20%">
                    <asp:Label ID="Label119" runat="server" Text="CountView" Font-Bold="true"></asp:Label>
                </th>
                <td class="B_L" width="30%">
                    <asp:Label ID="lblCountView" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th class="RB_L" width="20%">
                    <asp:Label ID="Label1" runat="server" Text="Tin liên quan" Font-Bold="true"></asp:Label>
                </th>
                <td class="B_L" colspan="3">
                    <asp:Button ID="btnSelectNewsReleated" OnClientClick="return ShowPopupNewReleated();"
                        runat="server" Text="Chọn tin" CssClass="btnSelect" />
                    <ul>
                        <asp:Repeater ID="rptReleatedNews" runat="server" OnItemCommand="rptReleatedNews_ItemCommand">
                            <ItemTemplate>
                                <li>
                                    <%#Eval("Title")%>&nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton ID="btnDeleteFile" runat="server" ImageUrl="/Images/Icon/delete_s.gif"
                                        CommandArgument='<%# Eval("ReleatedID") %>' Style="padding-right: 25px" OnClientClick="return confirm('Bạn thật sự muốn bỏ tin này?');" />
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </td>
            </tr>
        </table>
    </div>
    <div id="divData">
        <pqt:TextBoxMultiLanguage ID="tbmlTitle" TextWidth="95%" Title="Tiêu đề" TitleWidth="15%" MaxLength="500"
            runat="server" />
             <pqt:TextBoxMultiLanguage ID="tbmlSubTitle" Visible="true" TextMode="SingleLine"
            Title="Thông tin khách" TextWidth="95%" TitleWidth="15%" runat="server" />
            (không nhập nếu bài viết không thuộc mục cảm nhận khách hàng)
        
        <div style="display:none;">
            <pqt:TextBoxMultiLanguage ID="txtSubTitle" TextWidth="95%" Title="Thông tin khách hàng" TitleWidth="15%"
                runat="server" />(không nhập nếu là bài viết không thuộc mục cảm nhận khách hàng)
        </div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr id="newcate" runat="Server">
                <th class="RB_L" width="15%">
                    <asp:Label ID="Label121" runat="server" Text="Chủ đề tin" Font-Bold="true"></asp:Label>
                </th>
                <td class="B_L" colspan="3">
                    <asp:Panel ID="Panel1" Height="150px" Width="100%" ScrollBars="Auto" runat="server">
                        <asp:TreeView ID="treeCategory" runat="server" ShowLines="true" ShowCheckBoxes="All">
                            <SelectedNodeStyle CssClass="Link02" />
                        </asp:TreeView>
                    </asp:Panel>
                </td>
            </tr>
            <tr style="display: none;">
                <th class="RB_L" width="20%">
                    <asp:Label ID="Label22" runat="server" Text="Nguồn tin" Font-Bold="true"></asp:Label>
                </th>
                <td class="B_L" width="30%">
                    <asp:DropDownList ID="ddlNewsSource" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="div_img" runat="server">
                <th class="RB_L" width="15%">
                    <asp:Label ID="Label20" runat="server" Text="Hình ảnh"></asp:Label>
                </th>
                <td class="B_L">
                    <table>
                        <tr>
                            <td width="200px">
                                <asp:FileUpload ID="fileImage" runat="server" Width="90%" Height="20px" />
                            </td>
                            <td onmouseover="return ShowImage(event, 1);" onmouseout="return ShowImage(event, 0);">
                                <asp:Label ID="LinkImg" runat="server" Text="" ForeColor="BlueViolet" Style="cursor: pointer"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="div_downloadTitle" runat="Server">
                <th class="RB_L" width="15%">
                    Tác giả
                </th>
                <td class="B_L">
                    <asp:TextBox ID="txtAuthor" CssClass="Input_text" Width="99%" MaxLength="300" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr id="listType" runat="Server">
                <th class="RB_L" width="15%">
                    <asp:Label ID="Label4" runat="server" Text="Hiển thị"></asp:Label>
                </th>
                <td class="B_L">
                    <asp:CheckBoxList ID="cb_list" CssClass="Css_cblist" runat="server" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr id="div_Area" runat="Server">
                <th class="RB_L" width="15%">
                    <asp:Label ID="Label2" runat="server" Text="Khu vực"></asp:Label>
                </th>
                <td class="B_L">
                    <asp:DropDownList ID="ddl_area" AutoPostBack="true" runat="server" 
                        onselectedindexchanged="ddl_area_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddl_territory" runat="server" Style="margin-left:10px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th class="RB_L" width="15%">
                    <asp:Label ID="Label120" runat="server" Text="Trạng thái"></asp:Label>
                </th>
                <td class="B_L">
                    <asp:DropDownList ID="ddlNewsStatus" runat="server">
                        <asp:ListItem Value="0">Ẩn</asp:ListItem>
                        <asp:ListItem Value="1" Selected="True">Hiện</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th class="RB_L" width="15%">
                    <asp:Label ID="Label6" runat="server" Text="Thứ tự"></asp:Label>
                </th>
                <td class="B_L">
                    <asp:TextBox ID="txtSortOrder" runat="server" Width="50px" CssClass="Input_text"
                        Text="100"></asp:TextBox>
                </td>
            </tr>
            <tr id="sn_like" runat="Server" visible="false">
                <th class="RB_L" width="15%">
                    <asp:Label ID="Label5" runat="server" Text="Like"></asp:Label>
                </th>
                <td class="B_L">
                    <asp:Label ID="lbl_like" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
       
        <%----%>
       <div id="divmeta" runat="Server">
            <pqt:TextBoxMultiLanguage ID="txt_title_tag" TextWidth="95%" Title="Meta Title" TitleWidth="15%"
                        runat="server" />
            <pqt:TextBoxMultiLanguage ID="txt_key_tag" TextWidth="95%" Title="Keyword tag" TitleWidth="15%"
                runat="server" />
            <pqt:TextBoxMultiLanguage ID="txt_des_tag" TextWidth="95%" Title="Description tag"
                TitleWidth="15%" runat="server" />
        </div>
        <div id="styleMota" runat="Server">
            <pqt:TextBoxMultiLanguage ID="tbmlSubContent" TextHeight="200px" Title="Mô tả"
            TextWidth="95%" TitleWidth="15%" TextMode="MultiLine" runat="server" />

            <%--<pqt:FCKMultiLanguage ID="fckmlMetaDescription" TextWidth="95%" TextHeight="300px" Title="Mô tả"
            TitleWidth="15%" runat="server" />--%>
             <Nck:ckeditor  ID="fckmlMetaDescription_CK" runat="server"  TextHeight="300px" Title="Mô tả" TextWidth="95%"
                TitleWidth="15%"/>
        </div>

        <div id="divnoidung" runat="Server">
            <%--<pqt:FCKMultiLanguage ID="tbmlContent" TextHeight="600px" Title="Nội dung" TextWidth="95%"
                TitleWidth="15%" TextMode="MultiLine" runat="server" />--%>
                <Nck:ckeditor  ID="tbmlContent_CK" runat="server"  TextHeight="600px" Title="Nội Dung" TextWidth="95%"
                TitleWidth="15%"/>
        </div>
        <div style="display: none;">
            <pqt:TextBoxMultiLanguage ID="tbmlComment" TextHeight="100px" Title="Chú thích" TextWidth="95%"
                TitleWidth="15%" TextMode="MultiLine" runat="server" />
        </div>
    </div>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td class="Line1">
            </td>
        </tr>
    </table>
    <div id="divImageProduct" style="display: none; background-color: Gray; padding: 2px">
        <div style="padding: 3px; background-color: White">
            <asp:Image ID="imgProduct" runat="server" Width="300px" />
        </div>
    </div>
</div>
<script type="text/javascript">
    $("#ctl00_MainContent_ctl00_ctl00_tbmlTitle_rptText_ctl00_txtName").change(function () {
        $("#ctl00_MainContent_ctl00_ctl00_txt_title_tag_rptText_ctl00_txtName").val($(this).val());
        $("#ctl00_MainContent_ctl00_ctl00_txt_key_tag_rptText_ctl00_txtName").val($(this).val());
        $("#ctl00_MainContent_ctl00_ctl00_txt_des_tag_rptText_ctl00_txtName").val($(this).val());

    });

</script>