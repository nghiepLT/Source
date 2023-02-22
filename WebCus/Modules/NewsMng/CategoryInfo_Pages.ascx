<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryInfo_Pages.ascx.cs"
    Inherits="NewsMng.CategoryInfo_Pages" %>
<%@ Register Src="TextBoxMultiLanguage.ascx" TagName="TextBoxMultiLanguage" TagPrefix="pqt" %>
<%@ Register Src="FCKMultiLanguage.ascx" TagName="FCKMultiLanguage" TagPrefix="pqt" %>
<%@ Register Src="ckeditor.ascx" TagName="ckeditor" TagPrefix="Nck" %>
<script type="text/javascript" src="/Include/JS/ShowImage.js"></script>
<script type="text/javascript">

    function CheckValidate() {

        var check = false;
        var objArrFile = document.getElementsByTagName('input');
        for (i = 0; i < objArrFile.length; i++) {
            if (objArrFile[i].type == 'text') {
                if (objArrFile[i].id.toString().indexOf('txtName')>=0 && objArrFile[i].value != '')
                    check = true;
            }
        }
        if (!check) {
            alert('Nhập tiêu đề');
            return false;
        }

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
    function ShowImage(event, isShow) {

        if (isShow == 0) {
            document.getElementById("divImageProduct").style.display = 'none';
            return false;
        } else {
            PopupArea(event, "divImageProduct");
            return false;
        }

    }

    function ShowImageIcon(event, isShow) {

        if (isShow == 0) {
            document.getElementById("divImageIcon").style.display = 'none';
            return false;
        } else {
            PopupArea(event, "divImageIcon");
            return false;
        }

    }
</script>
<div class="page-title">
    <h2 class="icon-title">
        <span><%=this.UK == "Blogs"?"Danh mục Blogs":"Trang nội dung" %></span>
    </h2>
</div>
<br>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tbody>
        <tr>
            <td>&nbsp;
            </td>
            <td class="R">
                <asp:Button ID="btnDelete" runat="server" Text="Xóa" CssClass="btn-1" OnClientClick="return CheckDelete();"
                    OnClick="btnDelete_Click" />
                &nbsp;
                <asp:Button ID="btnInsert" runat="server" Text="Tạo mới" CssClass="btn-1" OnClick="btnInsert_Click" />
                   
                &nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn-1" OnClientClick="return CheckValidate();"
                    OnClick="btnSave_Click" />

                <a href="/RenderModule.aspx?smid=ProductMng&md=ProductControl.ascx&muid=ProductList" class="btn-1" id="pro_list" runat="Server" visible="false">Về danh sách
                </a>
            </td>
        </tr>
    </tbody>
</table>
<div class="TboardBox">
    <table width="100%">
        <tr>
            <td colspan="2" class="Line2"></td>
        </tr>
        <tr>
            <td width="20%" class="T" id="tdCategory" runat="server">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="pdL5 L ">
                            <asp:Panel ID="Panel1" Height="100%" Width="100%" ScrollBars="Auto" runat="server">
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
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <th class="RB_L">Hiển thị
                        </th>
                        <td class="B_L">
                            <asp:RadioButton ID="rdoIsViewY" runat="server" Checked="true" GroupName="IsView" Text="Yes" />
                            &nbsp;&nbsp;
                            <asp:RadioButton ID="rdoIsViewN" runat="server" GroupName="IsView" Text="No" />
                        </td>
                    </tr>
                    <tr id="tr_imgIcon" runat="Server" visible="false">
                        <th class="RB_L" width="120px">
                            <asp:Label ID="Label6" runat="server" Text="Icon Title"></asp:Label>
                        </th>
                        <td class="B_L">
                            <table>
                                <tr>
                                    <td onmouseover="return ShowImageIcon(event, 1);" onmouseout="return ShowImageIcon(event, 0);">
                                        <asp:Label ID="lbl_imgIcon" runat="server" Text="" ForeColor="BlueViolet" Style="cursor: pointer"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:FileUpload ID="fileIcon" runat="server" Width="300px" Height="24px" />
                        </td>
                    </tr>
                    <tr id="tr_img" runat="Server" visible="false">
                        <th class="RB_L" width="120px">
                            <asp:Label ID="Label1" runat="server" Text="Image"></asp:Label>
                        </th>
                        <td class="B_L">
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
                    <tr id="str_stt" runat="server">
                        <th class="RB_L" width="120px">
                            <asp:Label ID="Label5" runat="server" Text="Thứ tự"></asp:Label>
                        </th>
                        <td class="B_L">
                            <asp:TextBox ID="txtSortOrder" runat="server" Width="50px" CssClass="Input_text" ></asp:TextBox>
                             
                        </td>
                    </tr>
                    <tr id="sn_parent" runat="Server">
                        <th class="RB_L" width="120px">
                            <asp:Label ID="Label4" runat="server" Text="Parent"></asp:Label>
                        </th>
                        <td class="B_L">
                            <asp:DropDownList ID="ddl_parent" runat="server"  OnSelectedIndexChanged="change_sort">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <%--<tr id="sn_style" runat="Server" visible="false">
                        <th class="RB_L" width="120px">
                            <asp:Label ID="Label6" runat="server" Text="ParentID"></asp:Label>
                            <asp:Label ID="lblRootID" runat="server" Text=""></asp:Label>
                        </th>
                        <td class="B_L">
                            <asp:TextBox ID="txtParentID" runat="server" Width="50px" CssClass="Input_text"></asp:TextBox>
                        </td>
                    </tr>--%>
                </table>

                <pqt:TextBoxMultiLanguage ID="tbmlName" TextWidth="95%" Title="Tiêu đề" TitleWidth="120px"
                    runat="server" />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <th class="RB_L" width="120px">Alias URL
                        </th>
                        <td class="B_L">/<%=this.UK == "Blogs"?"blogs":"pages" %>/
                            <asp:TextBox ID="txtKeyWord" runat="server" Width="50%" Text="Keyword" CssClass="Input_text"></asp:TextBox>
                        </td>
                    </tr>
                     <tr id="tr_txtKeyWordad" runat="server">
                        <th class="RB_L" width="120px">Keywork
                        </th>
                        <td class="B_L">
                            <asp:TextBox ID="txtKeyWordad" runat="server" Width="50%" Text="" CssClass="Input_text"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div id="divmeta" runat="server">
                    <pqt:TextBoxMultiLanguage ID="txt_title_tag" TextWidth="95%" Title="Meta Title" TitleWidth="120px"
                        runat="server" />
                    <pqt:TextBoxMultiLanguage ID="txt_key_tag" TextWidth="95%" Title="Keyword tag" TitleWidth="120px"
                        runat="server" />
                    <pqt:TextBoxMultiLanguage ID="txt_des_tag" TextWidth="95%" Title="Description tag" TitleWidth="120px"
                        runat="server" />
                </div>
                <div id="styleMota" runat="Server">
                    <pqt:TextBoxMultiLanguage ID="tbmlMetaDescription" TextHeight="50px" Title="Mô tả"
                        TextWidth="95%" TitleWidth="120px" TextMode="MultiLine" runat="server" />
                    <%--<pqt:FCKMultiLanguage ID="fckmlMetaDescription" TextWidth="95%" TextHeight="300px" Title="Mô tả"
                        TitleWidth="120px" runat="server" />--%>
                          <Nck:ckeditor  ID="fckmlMetaDescription_CK" runat="server"  TextHeight="300px" Title="Nội Dung" TextWidth="95%"
                TitleWidth="15%"/>
                </div>
                <div id="div_editor" runat="Server" visible="true">
                    <%--<pqt:FCKMultiLanguage ID="fckmlDescription" TextWidth="95%" TextHeight="500px" Title="Nội dung"
                        TitleWidth="120px" runat="server" />--%>
                          <Nck:ckeditor  ID="fckmlDescription_CK" runat="server"  TextHeight="300px" Title="Nội Dung" TextWidth="95%"
                TitleWidth="15%"/>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="Line1"></td>
        </tr>
    </table>
    <div id="divImageProduct" style="display: none; background-color: Gray; padding: 2px">
        <div style="padding: 3px; background-color: White">
            <asp:Image ID="imgProduct" runat="server" Width="300px" />
        </div>
    </div>
    <div id="divImageIcon" style="display: none; background-color: Gray; padding: 2px">
        <div style="padding: 3px; background-color: White">
            <asp:Image ID="imgIcon" runat="server" Width="70" />
        </div>
    </div>
</div>

<script type="text/javascript">

    $("#MainContent_ctl00_ctl00_tbmlName_rptText_txtName_0").change(function () {
        $("#MainContent_ctl00_ctl00_txtKeyWord").val($(this).val().toSlug());
        $("#ctl00_MainContent_ctl00_ctl00_txt_title_tag_rptText_ctl00_txtName").val($(this).val());
        $("#ctl00_MainContent_ctl00_ctl00_txt_key_tag_rptText_ctl00_txtName").val($(this).val());
        $("#ctl00_MainContent_ctl00_ctl00_txt_des_tag_rptText_ctl00_txtName").val($(this).val());    
    });

</script>
<script type="text/javascript">

    $("#ctl00_MainContent_ctl00_ctl00_tbmlName_rptText_ctl00_txtName").change(function () {
        $("#ctl00_MainContent_ctl00_ctl00_txtKeyWord").val($(this).val().toSlug());
        $("#ctl00_MainContent_ctl00_ctl00_txt_title_tag_rptText_ctl00_txtName").val($(this).val());
        $("#ctl00_MainContent_ctl00_ctl00_txt_key_tag_rptText_ctl00_txtName").val($(this).val());
        $("#ctl00_MainContent_ctl00_ctl00_txt_des_tag_rptText_ctl00_txtName").val($(this).val());    
    });

</script>