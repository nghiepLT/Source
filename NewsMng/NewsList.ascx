<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsList.ascx.cs" Inherits="NewsMng.NewsList" %>
<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<script type="text/javascript">

    function ShowCategory() {
        WindowOpen('RenderPopup.aspx?smid=ProductMng&renderPage=SelectCategoryPopup.ascx', 'Category', 400, 450, 'no');
        return false;
    }
    
     function CheckMovePage() {
        var txtCurrentPage = document.getElementById("<%=txtCurrentPage.ClientID %>");
        var totalPage = <%=TotalPage %>;

        if (!PageMoveValid(txtCurrentPage, totalPage))
        {
	        alert('Invalid page number');
	        txtCurrentPage.focus();
	        return false;
        }
        return true;
    }
    
    function selectedTabOnLoad(){}

      function CheckAndConfirmDelete()
    {   
    
        var strValueIDChecked = '';
        
        var arrChkItem = document.getElementsByName('chkItem');
        
        for(var i=0; i< arrChkItem.length; i++)
        {
            var chkItem = arrChkItem[i];
            if(chkItem.checked == true)
            {
                strValueIDChecked += chkItem.value + '|';
            }
            
        }
        
        if(strValueIDChecked == '')
        {
            alert("Vui lòng chọn chuyên mục cần xóa");
            return false;
        }
        else
        {
            if(confirm('Bạn có chắc xóa các chuyên mục này không?'))
            {
                var hdnIDs = document.getElementById('<%=hdnIDs.ClientID %>');
                hdnIDs.value  = strValueIDChecked;
                return true;   
            }
            else
                return false;
        }
    }  

     function CheckAllItem(objcheckAll)
    {
        var obj = document.getElementsByName('chkItem');
        
        for(var i=0; i< obj.length; i++)
        {
            var chkItem = obj[i];
            chkItem.checked = objcheckAll.checked;
        }
    }

    function CheckActive()
    {   
    
        var strValueIDChecked = '';
        
        var arrChkItem = document.getElementsByName('chkItem');
        
        for(var i=0; i< arrChkItem.length; i++)
        {
            var chkItem = arrChkItem[i];
            if(chkItem.checked == true)
            {
                strValueIDChecked += chkItem.value + '|';
            }
            
        }
        
        if(strValueIDChecked == '')
        {
            alert("Vui lòng chọn tin cần cập nhật cho hiển thị tin");
            return false;
        }
        else
        {
            var hdnIDs = document.getElementById('<%=hdnIDs.ClientID %>');
            hdnIDs.value  = strValueIDChecked;
            return true;   
        
        }
    
    }  
    
    
    
</script>
<asp:HiddenField ID="hdnIDs" Value="" runat="server" />
<div class="page-title">
    <h2 class="icon-title">
        <span>Danh sách bài viết</span>
    </h2>
</div>
<br>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tbody>
        <tr>
            <td class="L">
                Tìm kiếm:
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Text="Chọn trạng thái" Value="2"></asp:ListItem>
                    <asp:ListItem Text="True" Value="1"></asp:ListItem>
                    <asp:ListItem Text="False" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlSearchType" Visible="false" runat="server">
                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Title" Value="1"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlCategory" Visible="true" runat="server">
                </asp:DropDownList>
                &nbsp;
                <asp:TextBox ID="txtSearchText" onkeydown="return SetFocusOnEnter(event, 'btnSearch');"
                    runat="server" CssClass="Input_text"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Tìm" OnClick="btnSearch_Click" CssClass="btn-1" />
            </td>
            <td class="R">
                <asp:Button ID="btnActive" runat="server" OnClientClick="return CheckActive();"
                    Text="Hiện tin" OnClick="btnActive_Click" CssClass="btn-1"></asp:Button>&nbsp;&nbsp;
                <asp:Button ID="btnDelete" runat="server" OnClientClick="return CheckAndConfirmDelete();"
                    Text="Xóa" OnClick="btnDelete_Click" CssClass="btn-1"></asp:Button>&nbsp;&nbsp;
                <asp:Button ID="btnInsert" runat="server" Text="Tạo mới" CssClass="btn-1" OnClick="btnInsert_Click" />
            </td>
        </tr>
    </tbody>
</table>
<div class="TboardBox">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="Line2">
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvUser" runat="server" Width="100%" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row"
                    CellSpacing="1" CellPadding="0" BorderWidth="0px" OnRowCommand="gvUser_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <HeaderTemplate>
                                <input id="chkAll" type="checkbox" onclick="CheckAllItem(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input id="chkItem" type="checkbox" name="chkItem" value='<%# Eval("NewsID")%>' />
                            </ItemTemplate>
                            <HeaderStyle Width="5%" CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_L" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="STT">
                            <ItemTemplate>
                                &nbsp
                                <%# Eval("Num")%>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_L" Width="8%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tiêu đề">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkLink" runat="server" CommandArgument='<%# Eval("NewsID") %>'>
                                <%# DBNull.Value == Eval("Title")? "":"&raquo;" %>&nbsp;
                                <%#Eval("Title")%>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="Author">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="Label1212" runat="server" Text='<%# Eval("Author")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" Width="17%" />
                            <ItemStyle CssClass="RB_L" Width="20%" />
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText="Nổi bật">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lbl_type" runat="server" Text='<%# BindTypeName(Eval("NewsID"))%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L"/>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Danh mục ">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblcate" runat="server" Text='<%# BindCateName(Eval("NewsCategoryID"))%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L"/>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Người tạo">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblRegUser" runat="server" Text='<%# this.MemberName(Eval("RegUser"))%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L"/>
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText="Bình luận">
                            <ItemTemplate>
                                &nbsp;
                                <%#this.SoBinhluan(Eval("NewsID"))%>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Like">
                            <ItemTemplate>
                                &nbsp;
                                <%#Eval("NewsLike","{0:N0}")%>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L"/>
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText="Ngày tạo">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="Label143" runat="server" Text='<%# Eval("RegDate", "{0: yyyy-MM-dd}")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center"/>
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Khu vực">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lbl_area" runat="server" Text='<%#GetAreaName(Eval("NewsID"))%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center"/>
                            <ItemStyle CssClass="B_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trạng thái">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="Label1421" runat="server" Text='<%#GetStatusName(Eval("NewsStatus"))%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center"/>
                            <ItemStyle CssClass="B_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thứ tự">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lbl_sortorder" runat="server" Text='<%#Eval("SortOrder")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="B_C"/>
                            <ItemStyle CssClass="B_C" />
                        </asp:TemplateField>
                        </Columns>
                        <Columns>
                            <asp:TemplateField HeaderText="Like">
                                <ItemTemplate>
                                    &nbsp;
                                    <%#Eval("NewsLike")%>
                                    &nbsp;
                                </ItemTemplate>
                                <HeaderStyle CssClass="B_C"/>
                                <ItemStyle CssClass="B_C" />
                            </asp:TemplateField>
                        </Columns>
                        <Columns>
                        <asp:TemplateField HeaderText="Comment">
                            <ItemTemplate>
                                <a onclick="WindowOpen('/RenderPopup.aspx?smid=UserMng&renderPage=Comment.ascx&id=<%# Eval("NewsID") %>&UK=<%=this.KeyWord %>', '', 900, 550, 'no');" style="cursor:pointer;"
                                    title="Danh sách bình luận">
                                    <img src="/images/icon/list02.gif" alt="">
                                </a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="B_C" />
                        </asp:TemplateField>
                        </Columns>
                    <RowStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="td_line2" height="1">
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="200px">
            </td>
            <td class="C">
                <pqt:PQTPager ID="PQTPager1" runat="server" OnPageIndexChanged="Pager_PageIndexChanged"
                    Width="410px" />
            </td>
            <td width="200px" class="R">
                <asp:TextBox ID="txtCurrentPage" runat="server" Width="30px" Style="text-align: right"
                    CssClass="Input_text"></asp:TextBox>
                /<asp:Label ID="lblTotalPage" runat="server" Text=""></asp:Label>
                <asp:Label ID="Label2" runat="server" Text=" Page"></asp:Label>
                &nbsp;&nbsp;
                <asp:Button ID="btnPageMove" runat="server" Text="Move" Font-Bold="true" OnClick="btnPageMove_Click"
                    OnClientClick="return CheckMovePage();" Style="background: url('Images/Icon/btn_move.gif') no-repeat left;
                    padding-bottom: 3px; padding-left: 22px;" />
            </td>
        </tr>
    </table>
</div>