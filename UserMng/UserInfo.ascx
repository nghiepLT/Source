﻿<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="UserInfo.ascx.cs"
    Inherits="UserMng.UserInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<script type="text/javascript">
    function CheckValidate() {
        if (document.getElementById('<%=txtLoginID.ClientID %>').value == "") {
            alert('Input UserID');
            document.getElementById('<%=txtLoginID.ClientID %>').focus();
            return false;
        }

        if (document.getElementById('<%=txtUsername.ClientID %>').value == "") {
            alert('Input User name');
            document.getElementById('<%=txtUsername.ClientID %>').focus();
            return false;
        }
        var txtPassword = document.getElementById('<%=txtPassword.ClientID %>');
        if (txtPassword.value == "") {
            alert('Input Password');
            txtPassword.focus();
            return false;
        }

        var txtConfirmPassword = document.getElementById('<%=txtConfirmPassword.ClientID %>');
        if (txtConfirmPassword.value == "") {
            alert('Input Confirm Password');
            txtConfirmPassword.value = '';
            txtConfirmPassword.focus();
            return false;
        }

        if (txtPassword.value != txtConfirmPassword.value) {
            alert('Confirm Password not conrect');
            txtConfirmPassword.focus();
            return false;
        }

        var txtEmail = document.getElementById('<%=txtEmail.ClientID %>');
        if (txtEmail.value == "") {
            alert('Input Email');
            txtEmail.focus();
            return false;
        }
        if (!txtEmail.value.isemail()) {
            alert('Email invalid');
            txtEmail.focus();
            return false;
        }


        return true;
    }
    function ShowPopupMapLink(IDUser) {
        WindowOpen('RenderPopup.aspx?smid=UserMng&renderPage=UserControl.ascx&md=UserMapLink&id=' + IDUser, 'UserMapLink', 950, 550, 'yes');
        return false;
    }
</script>

<div class="page-title">
    <h2 class="icon-title">
        <span>Cập nhật thông tin admin</span>
    </h2>
</div>
</br>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tbody>
        <tr>
            <td class="C">
            </td>
            <td class="R">
                <asp:Button ID="btnInsert" runat="server" Text="Thêm mới" CssClass="btn-1" OnClick="btnInsert_Click" />
                &nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn-1" OnClick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="Xóa" CssClass="btn-1" OnClick="btnDelete_Click" />
            </td>
        </tr>
    </tbody>
</table>
<div class="TboardBox">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tbttkt" runat="server">
        <tr>
            <td colspan="4" class="Line2">
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
                <span class="txt9_R">*</span>
                <asp:Label ID="Label2" runat="server" Text="LoginID"></asp:Label>
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txtLoginID" runat="server" ReadOnly="true" CssClass="Input_text"
                    Width="80%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                <span class="txt9_R">*</span>
                <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txtUsername" runat="server" CssClass="Input_text" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
                <span class="txt9_R">*</span>
                <asp:Label ID="Label7" runat="server" Text="Password"></asp:Label>
            </th>
            <td class="B_L">
                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="Input_text"
                    Width="80%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                <span class="txt9_R">*</span>
                <asp:Label ID="Label9" runat="server" Text="Confirm Password"></asp:Label>
            </th>
            <td class="B_L">
                <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" CssClass="Input_text"
                    Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
                <span class="txt9_R">*</span>
                <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label>
            </th>
            <td class="B_L">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="Input_text" Width="80%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                <asp:Label ID="Label5" runat="server" Text="Address"></asp:Label>
            </th>
            <td class="B_L">
                <asp:TextBox ID="txtAddress" runat="server" CssClass="Input_text" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
                <asp:Label ID="Label15" runat="server" Text="Tel"></asp:Label>
            </th>
            <td class="B_L">
                <asp:TextBox ID="txtTel" runat="server" CssClass="Input_text" Width="80%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                <asp:Label ID="Label10" runat="server" Text="Fax"></asp:Label>
            </th>
            <td class="B_L">
                <asp:TextBox ID="txtFax" runat="server" CssClass="Input_text" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none;">
            <th width="20%" class="RB_L">
                <asp:Label ID="Label12" runat="server" Text="IsExpire"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:RadioButton ID="rdoIsExpireY" Text="Yes" GroupName="IsExpire" runat="server" />
                <asp:RadioButton ID="rdoIsExpireN" Text="No" Checked="true" GroupName="IsExpire"
                    runat="server" />
            </td>
            <th width="20%" class="RB_L">
                <asp:Label ID="Label13" runat="server" Text="ExprireDate (MM-dd-yyyy)"></asp:Label>
            </th>
            <td class="B_L">
                <asp:TextBox ID="txtExpireDate" runat="server" ReadOnly="true" CssClass="Input_text"
                    Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
                Active
            </th>
            <td class="RB_L">
                <asp:RadioButton ID="rdoActiveYes" Text="Yes" GroupName="Active" runat="server" />
                <asp:RadioButton ID="rdoActiveNo" Text="No" Checked="true" GroupName="Active"
                    runat="server" />
            </td>
            <th width="20%" class="RB_L">
                <asp:Label ID="Label11" runat="server" Text="Remark"></asp:Label>
            </th>
            <td class="B_L">
                <asp:TextBox ID="txtRemark" TextMode="MultiLine" Height="50px" runat="server" CssClass="Input_text"
                    Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr id="trUserType" runat="server">
            <th width="20%" class="RB_L">
                <asp:Label ID="Label6" runat="server" Text="UserType"></asp:Label>
            </th>
            <td class="B_L" colspan="3">
                <asp:DropDownList ID="ddlUserType" runat="server" 
                    onselectedindexchanged="ddlUserType_SelectedIndexChanged" AutoPostBack="true">
                   <%-- <asp:ListItem Text="SupperAdmin" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Admin" Selected="True" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Employee" Value="3"></asp:ListItem>
                    <asp:ListItem Text="User" Value="5"></asp:ListItem>--%>
                      <asp:ListItem Text="SupperAdmin" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Admin" Value="2"></asp:ListItem>
                      <asp:ListItem Text="Group Admin" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Employee" Selected="True" Value="4"></asp:ListItem>
                    <asp:ListItem Text="User" Value="5"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>        
        <tr id="tr_parentuser" runat="server">
          <th width="20%" class="RB_L">
                <asp:Label ID="Label8" runat="server" Text="Trực Thuộc Phòng"></asp:Label>
            </th>
             <td class="B_L" colspan="3">
                <asp:DropDownList ID="dr_parent" runat="server">                   
                </asp:DropDownList>
            </td>
            
        </tr>
        <tr id="tr_lkns" runat="server">
        <th  width="20%" class="RB_L">
            Liên Kết Nhân Sự
            </th>
            <td>
            <asp:DropDownList ID="dr_dsnhanvien" runat="server" 
                    onselectedindexchanged="dr_dsnhanvien_SelectedIndexChanged" AutoPostBack="true">                   
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="tr_cty" runat="server">
          <th width="20%" class="RB_L">
                <asp:Label ID="Label4" runat="server" Text="Trực Thuộc Cty"></asp:Label>
            </th>
             <td class="B_L" colspan="3">
                <asp:DropDownList ID="dr_cty" runat="server">
                          <asp:ListItem Text="Nguyên Kim" Value="1" Selected="true"></asp:ListItem>
                          <asp:ListItem Text="Chính Nhân" Value="2"></asp:ListItem>
                          <asp:ListItem Text="SMC" Value="3"></asp:ListItem>         
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0">
    
    <tr class="trLabelFilter1">       
        <td>
            Tìm theo
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr class="trLabelFilter1">    
        <td>
            <asp:DropDownList ID="dropSearchtype" CssClass="select" Width="150px" runat="server"
                OnSelectedIndexChanged="dropSearchtype_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="Chọn tìm theo" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Phòng Ban" Value="1"></asp:ListItem>
                <asp:ListItem Text="Tên Nhân Viên" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="dr_dspb" CssClass="select" Width="150px" runat="server" 
                onselectedindexchanged="dr_dspb_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:TextBox ID="txtSearch" CssClass="Input_text" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnSearch" CssClass="btn-1" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" />
            
        </td>
    </tr>
</table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="15px">
            </td>
        </tr>
        <tr>
            <td class="Line2">
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvUser" runat="server" Width="100%" AutoGenerateColumns="False"
                    AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row" CellSpacing="1"
                    CellPadding="0" BorderWidth="0px" OnRowCommand="gvUser_RowCommand">
                    <Columns>
                    <asp:TemplateField HeaderText="STT">
                            <ItemTemplate>
                                &nbsp
                                <%# Container.DisplayIndex+1%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_L" Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UserID" Visible="false">
                            <ItemTemplate>
                                &nbsp
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("UserID") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="8%" CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LoginID">
                            <ItemTemplate>
                                &nbsp;
                                <asp:LinkButton ID="lnkLink" runat="server" Text='<%# Eval("LoginID")%>' CommandArgument='<%# Eval("UserID") %>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" Width="17%" />
                            <ItemStyle CssClass="RB_L" Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Username">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText1" runat="server" Text='<%# Eval("UserName")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" Width="20%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Cty">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText1" runat="server" Text='<%# Eval("UserLike")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" Width="20%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Chucvu">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText1" runat="server" Text='<%# Eval("UserType")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" Width="20%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Phongban">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText1" runat="server" Text='<%# Eval("ParentID")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" Width="20%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Email")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tel">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText2" runat="server" Text='<%# Eval("Tel")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MapLink">
                            <ItemTemplate>
                                &nbsp;
                                <a onclick='ShowPopupMapLink(<%#Eval("UserID") %>);'>
                                    <%# Eval("LoginID")%>
                                </a>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
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
    <div class="panigation" id="pager_div" runat="server">
            <center>
                <div>
                    <asp:LinkButton ID="lnkFirst" Style="padding: 1px 3px; margin: 1px; background: #ccc;
                        border: solid 1px #666; font: 8pt tahoma;" OnClick="lnkFirst_Click" runat="server">
                    <<
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkPrevious" Style="padding: 1px 3px; margin: 1px; background: #ccc;
                        border: solid 1px #666; font: 8pt tahoma;" OnClick="lnkPrevious_Click" runat="server">
                    <
                    </asp:LinkButton>
                    <asp:Repeater ID="rptPages" runat="server" OnItemCommand="rptPages_ItemCommand1"
                        OnItemDataBound="RepeaterPaging_ItemDataBound">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnPage" Style="padding: 1px 3px; margin: 1px; border: solid 1px #666;
                                font: 8pt tahoma;" CommandName="Page" CommandArgument='<%#Eval("PageIndex") %>'
                                runat="server"><%# Eval("PageText")%>

                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:LinkButton ID="lnkNext" Style="padding: 1px 3px; margin: 1px; background: #ccc;
                        border: solid 1px #666; font: 8pt tahoma;" OnClick="lnkNext_Click" runat="server">
                    >
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkLast" Style="padding: 1px 3px; margin: 1px; background: #ccc;
                        border: solid 1px #666; font: 8pt tahoma;" OnClick="lnkLast_Click" runat="server">
                    >>
                    </asp:LinkButton>
                </div>
            </center>
        </div>
</div>
