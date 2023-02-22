<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="PoinKm.ascx.cs"
    Inherits="UserMng.PoinKm" %>
<%@ Register Src="TextBoxMultiLanguage.ascx" TagName="TextBoxMultiLanguage" TagPrefix="pqt" %>

<script type="text/javascript">
    function CheckValidate() {
       
        if (document.getElementById('<%=txt_areades.ClientID %>').value == "") {
            alert('Nhập Tên Khu Vực');
            document.getElementById('<%=txt_areades.ClientID %>').focus();
            return false;
        }
       
        var Usertype = document.getElementById('<%=dr_khoangcach.ClientID %>');
        var idselect = Usertype.options[Usertype.selectedIndex].value;
        if (idselect == "-1") {
            alert('Chọn khoảng cách !');
            Usertype.focus();
            return false;
        }
        return true;
    }
   </script>
<div class="page-title">
    <h2 class="icon-title">
        <span>Thêm Mới Khu Vực Xữ Lý</span>
    </h2>
</div>
<asp:HiddenField ID="hdnCategoryIDs" Value="" runat="server" />
<script language="javascript" type="text/javascript">
    function OnTreeClick(evt) {
        var src = window.event != window.undefined ? window.event.srcElement : evt.target;
        var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
        if (isChkBoxClick) {
            var parentTable = GetParentByTagName("table", src);
            var nxtSibling = parentTable.nextSibling;
            if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
            {
                if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                {
                    //check or uncheck children at all levels
                    CheckUncheckChildren(parentTable.nextSibling, src.checked);
                }
            }
            //check or uncheck parents at all levels
            CheckUncheckParents(src, src.checked);
        }
    }

    function CheckUncheckChildren(childContainer, check) {
        var childChkBoxes = childContainer.getElementsByTagName("input");
        var childChkBoxCount = childChkBoxes.length;
        for (var i = 0; i < childChkBoxCount; i++) {
            childChkBoxes[i].checked = check;
        }
    }

    function CheckUncheckParents(srcChild, check) {
        var parentDiv = GetParentByTagName("div", srcChild);
        var parentNodeTable = parentDiv.previousSibling;

        if (parentNodeTable) {
            var checkUncheckSwitch;

            if (check) //checkbox checked
            {
                var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                if (isAllSiblingsChecked)
                    checkUncheckSwitch = true;
                else
                    return; //do not need to check parent if any(one or more) child not checked
            }
            else //checkbox unchecked
            {
                checkUncheckSwitch = false;
            }

            var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
            if (inpElemsInParentTable.length > 0) {
                var parentNodeChkBox = inpElemsInParentTable[0];
                parentNodeChkBox.checked = checkUncheckSwitch;
                //do the same recursively
                CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
            }
        }
    }

    function AreAllSiblingsChecked(chkBox) {
        var parentDiv = GetParentByTagName("div", chkBox);
        var childCount = parentDiv.childNodes.length;
        for (var i = 0; i < childCount; i++) {
            if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
            {
                if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                    var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                    //if any of sibling nodes are not checked, return false
                    if (!prevChkBox.checked) {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    //utility function to get the container of an element by tagname
    function GetParentByTagName(parentTagName, childElementObj) {
        var parent = childElementObj.parentNode;
        while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
            parent = parent.parentNode;
        }
        return parent;
    }

</script>

<div class="TboardBox">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tbody>
            <tr>
                <td class="C">
                </td>
                <td class="R">
                    <asp:Button ID="btnInsertBanner" runat="server" Text="Tạo mới" CssClass="btn-1" OnClick="btnInsertBanner_Click" />
                    &nbsp;
                    <asp:Button ID="btnSaveBanner" runat="server" Text="Lưu" CssClass="btn-1" OnClick="btnSaveBanner_Click"
                        OnClientClick="return CheckValidate();" />
                    &nbsp;
                    <asp:Button Visible="false" ID="btnDeleteBanner" runat="server" Text="Xóa" CssClass="btn-1" OnClick="btnDeleteBanner_Click"
                        OnClientClick="return ConfirmDeleteBanner();" />
                </td>
            </tr>
        </tbody>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="4" height="5px">
            </td>
        </tr>
        <tr>
            <td colspan="4" class="Line2">
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="lbl_name" runat="server" Text="Khoảng cách (km)"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:DropDownList ID="dr_khoangcach" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label1" runat="server" Text="Tên Khu vực"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:TextBox ID="txt_areades" CssClass="Input_text" Width="99%" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label2" runat="server" Text="Ghi chú"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:TextBox ID="txt_Sort" CssClass="Input_text" Width="99%" runat="server"></asp:TextBox>
            </td>
            <%--<th width="15%" class="RB_L">
                <asp:Label ID="Label1" runat="server" Text="Trạng thái"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>--%>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="4" class="Line1">
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="Line2">
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvBanner" runat="server" Width="100%" AutoGenerateColumns="False"
                    AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row" CellSpacing="1"
                    CellPadding="0" BorderWidth="0px" OnRowCommand="gvBanner_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="STT">
                            <ItemTemplate>
                                &nbsp
                                <%# Container.DisplayIndex+1%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_L" Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tên khu vực">
                            <ItemTemplate>
                                <%#Eval("AreaName")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Điểm Đoạn Đường" Visible="false">
                            <ItemTemplate>
                                <%#Eval("AreaDescription")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C"/>
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Thứ tự">
                            <ItemTemplate>
                                <%#Eval("SortOrder")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C"/>
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>--%>
                       <%-- <asp:TemplateField HeaderText="Trạng thái">
                            <ItemTemplate>
                                <img src='/admincss/images/icon/<%# Get_Image_Status(Eval("Status"))%>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_C" Width="80px" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="" Visible="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" ImageUrl="/images/icon/modify.gif" ToolTip="Xem/sửa"
                                    CommandArgument='<%# Eval("TAreaID") %>'
                                    runat="server" CommandName="EditItem" />
                                &nbsp;
                                <asp:ImageButton ID="imgDelete" ImageUrl="/images/icon/delete.gif" ToolTip="Xóa"
                                    runat="server" CommandArgument='<%# Eval("TAreaID") %>' CommandName="DeleteItem"
                                    OnClientClick="return confirm('Bạn thật sự muốn xóa?')" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="B_C" Width="70px" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="Line1" height="1">
            </td>
        </tr>
    </table>
</div>
