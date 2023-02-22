<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="UserTableWork.ascx.cs"
    Inherits="UserMng.UserTableWork" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
<br>
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
    <table width="100%" border="0" cellspacing="0" cellpadding="0"  id="tbttkt" runat="server">
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
                <asp:Label ID="Label10" runat="server" Text="Phone contact"></asp:Label>
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
            <td class="B_L">
                <asp:DropDownList ID="ddlUserType" runat="server"  >
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
              <th width="20%" class="RB_L">
                <asp:Label ID="Label8" runat="server" Text="Thuộc nhóm"></asp:Label>
            </th>
             <td class="B_L" colspan="3">
                <asp:DropDownList ID="dr_parent" runat="server">
                   
                    
                </asp:DropDownList>
            </td>
        </tr>
            <tr>
            <th width="20%" class="RB_L">
                <asp:Label ID="Label4" runat="server" Text="Level User"></asp:Label>
            </th>
            <td class="B_L">
                <asp:DropDownList ID="dr_levelUser" runat="server">
                   
                    <asp:ListItem Text="Level 1" Selected="True" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Level 2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Level 3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Level 4"  Value="4"></asp:ListItem>
                    <asp:ListItem Text="Level 5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Level 6" Value="6"></asp:ListItem>
                    <asp:ListItem Text="Level 7" Value="7"></asp:ListItem>
                    <asp:ListItem Text="Level 8" Value="8"></asp:ListItem>
                    <asp:ListItem Text="Level 9" Value="9"></asp:ListItem>
                    <asp:ListItem Text="Level 10" Value="10"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <th  width="20%" class="RB_L"> Nghỉ Phép: </th>
            <td class="B_L" colspan="3">
            <asp:RadioButton ID="rd_buoisang" runat="server" GroupName="nghiphep" Text="Nghỉ Phép Sáng" />
            <asp:RadioButton ID="rd_buoichieu" runat="server" GroupName="nghiphep" Text="Nghỉ Phép Chiều" />
            <asp:RadioButton ID="rd_phepngay" runat="server" GroupName="nghiphep" Text="Nghỉ Phép Ngày" />
           Số ngày nghỉ:  <asp:TextBox ID="txt_songaynghi" runat="server"></asp:TextBox> 
              Lý Do : <br /> <asp:TextBox ID="txt_lydonghi" runat="server"  TextMode="MultiLine"></asp:TextBox> 
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
                        <asp:TemplateField HeaderText="UserID"  Visible="false">
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
                        <asp:TemplateField HeaderText="Level" >
                            <ItemTemplate>
                                &nbsp;
                               <%#Eval("CompanyName")%>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" />
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
                                <asp:Label ID="lblResourceTel" runat="server" Text='<%# Eval("Tel")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone Contact">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText2" runat="server" Text='<%# Eval("Fax")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trạng Thái">
                            <ItemTemplate>
                                &nbsp;

                                <asp:Label ID="lblResource" runat="server" Text='<%# Eval("UserLike")%>' />
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
        <tr>
        <td>
        
        <asp:GridView ID="gr_kththong" runat="server" Width="100%" AutoGenerateColumns="False"
                    AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row" CellSpacing="1"
                    CellPadding="0" BorderWidth="0px" OnRowCommand="gvUser_RowCommand">
                    <Columns>
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
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" Width="10%" />
                            <ItemStyle CssClass="RB_L" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tên NV Kỹ Thuật">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText1" runat="server" Text='<%# Eval("UserName")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Level" >
                            <ItemTemplate>
                                &nbsp;
                                 <%#Eval("CompanyName")%>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle Width="8%" CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" >
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Email")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" Width="15%" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Adress">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("Address")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tel">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceTel" runat="server" Text='<%# Eval("Tel")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone Contact">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText2" runat="server" Text='<%# Eval("Fax")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trạng Thái">
                            <ItemTemplate>
                                &nbsp;
                                  <%--<div style='font-weight:bold; color:<%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Green" : (Eval("UserLike").ToString()=="1" ? "Red" : (Eval("UserLike").ToString()=="4" ? "Blue" :(Eval("UserLike").ToString()=="5" ? "Yellow" : "Black"))))%>' > 
                                    <asp:Label ID="Label4"  CssClass="text002" Text='<%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Sẵn Sàng" : "Đang Xữ Lý")%>'
                                    runat="server"></asp:Label>
                                    </div>--%>
                              <%--<div style='font-weight: bold; color: <%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Green" : (Eval("UserLike").ToString()=="1" ? "Red" : (Eval("UserLike").ToString()=="2" ? "Blue" :(Eval("UserLike").ToString()=="3" ? "orange" : "Black"))))%>'>
                            <asp:Label ID="Label4" CssClass="text002" Text='<%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Sẵn Sàng" :(Eval("UserLike").ToString()=="1" ? "Đang Xữ Lý" : (Eval("UserLike").ToString()=="2" ? "Nghỉ Buổi Sáng" :(Eval("UserLike").ToString()=="3" ? "Nghỉ Buổi Chiều" : "Nghỉ Phép Ngày") )))%>'
                                runat="server"></asp:Label>
                        </div>--%>
                        <div style='font-weight: bold; color: <%#string.Format("{0}", Eval("Gender").ToString()=="0" ? (Eval("UserLike").ToString()=="0" ? "Green" : "Red") : (Eval("Gender").ToString() =="400" ? "Black" : (Eval("Gender").ToString()=="200" ? "Blue" :(Eval("Gender").ToString()=="300" ? "orange" : "Green"))))%>'>
                            <asp:Label ID="Label4" CssClass="text002" Text='<%#string.Format("{0}", Eval("Gender").ToString()=="0" ? (Eval("UserLike").ToString()=="0" ? "Sẵn Sàng" : "Đang Xử Lý"):(Eval("Gender").ToString()=="400" ? "Nghỉ Phép Ngày" : (Eval("Gender").ToString()=="200" ? "Nghỉ Buổi Sáng" :(Eval("Gender").ToString()=="300" ? "Nghỉ Buổi Chiều" : "Sẵn Sàng"))))%>'
                                runat="server"></asp:Label>
                        </div>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="13%" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="MapLink">
                            <ItemTemplate>
                                &nbsp;
                                <a onclick='ShowPopupMapLink(<%#Eval("UserID") %>);'>
                                    <%# Eval("LoginID")%>
                                </a>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
                        </asp:TemplateField>--%>
                    </Columns>
                    <RowStyle HorizontalAlign="Center" BorderStyle="Double" BorderColor="Aqua"/>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:GridView>
        </td>
        </tr>
          <tr>
         <td style="background:#6092dc;padding:10px;font-weight:bold;color:#fff">
           Kỹ Thuật Sửa Chữa
         </td>
         </tr>
        <tr>
        <td>
        
        <asp:GridView ID="GR_ktsc" runat="server" Width="100%" AutoGenerateColumns="False"
                    AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row" CellSpacing="1"
                    CellPadding="0" BorderWidth="0px" OnRowCommand="gvUser_RowCommand">
                    <Columns>
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
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" Width="10%" />
                            <ItemStyle CssClass="RB_L" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tên NV Kỹ Thuật">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText1" runat="server" Text='<%# Eval("UserName")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Level" >
                            <ItemTemplate>
                                &nbsp;
                                 <%#Eval("CompanyName")%>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle Width="8%" CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" >
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Email")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" Width="15%" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Adress">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("Address")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tel">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceTel" runat="server" Text='<%# Eval("Tel")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone Contact">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText2" runat="server" Text='<%# Eval("Fax")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trạng Thái">
                            <ItemTemplate>
                                &nbsp;
                                  <%--<div style='font-weight:bold; color:<%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Green" : (Eval("UserLike").ToString()=="1" ? "Red" : (Eval("UserLike").ToString()=="4" ? "Blue" :(Eval("UserLike").ToString()=="5" ? "Yellow" : "Black"))))%>' > 
                                    <asp:Label ID="Label4"  CssClass="text002" Text='<%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Sẵn Sàng" : "Đang Xữ Lý")%>'
                                    runat="server"></asp:Label>
                                    </div>--%>
                              <%--<div style='font-weight: bold; color: <%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Green" : (Eval("UserLike").ToString()=="1" ? "Red" : (Eval("UserLike").ToString()=="2" ? "Blue" :(Eval("UserLike").ToString()=="3" ? "orange" : "Black"))))%>'>
                            <asp:Label ID="Label4" CssClass="text002" Text='<%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Sẵn Sàng" :(Eval("UserLike").ToString()=="1" ? "Đang Xữ Lý" : (Eval("UserLike").ToString()=="2" ? "Nghỉ Buổi Sáng" :(Eval("UserLike").ToString()=="3" ? "Nghỉ Buổi Chiều" : "Nghỉ Phép Ngày") )))%>'
                                runat="server"></asp:Label>
                        </div>--%>
                        <div style='font-weight: bold; color: <%#string.Format("{0}", Eval("Gender").ToString()=="0" ? (Eval("UserLike").ToString()=="0" ? "Green" : "Red") : (Eval("Gender").ToString() =="400" ? "Black" : (Eval("Gender").ToString()=="200" ? "Blue" :(Eval("Gender").ToString()=="300" ? "orange" : "Green"))))%>'>
                            <asp:Label ID="Label4" CssClass="text002" Text='<%#string.Format("{0}", Eval("Gender").ToString()=="0" ? (Eval("UserLike").ToString()=="0" ? "Sẵn Sàng" : "Đang Xử Lý"):(Eval("Gender").ToString()=="400" ? "Nghỉ Phép Ngày" : (Eval("Gender").ToString()=="200" ? "Nghỉ Buổi Sáng" :(Eval("Gender").ToString()=="300" ? "Nghỉ Buổi Chiều" : "Sẵn Sàng"))))%>'
                                runat="server"></asp:Label>
                        </div>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="13%" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="MapLink">
                            <ItemTemplate>
                                &nbsp;
                                <a onclick='ShowPopupMapLink(<%#Eval("UserID") %>);'>
                                    <%# Eval("LoginID")%>
                                </a>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
                        </asp:TemplateField>--%>
                    </Columns>
                    <RowStyle HorizontalAlign="Center" BorderStyle="Double" BorderColor="Aqua"/>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:GridView>
        </td>
        </tr>
        <tr>
         <td style="background:#6092dc;padding:10px;font-weight:bold;color:#fff">
           Kỹ Thuật Showroom
         </td>
         </tr>

        <tr>
        <td>
        
        <asp:GridView ID="gr_ktshowroom" runat="server" Width="100%" AutoGenerateColumns="False"
                    AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row" CellSpacing="1"
                    CellPadding="0" BorderWidth="0px" OnRowCommand="gvUser_RowCommand">
                    <Columns>
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
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" Width="10%" />
                            <ItemStyle CssClass="RB_L" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tên NV Kỹ Thuật">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText1" runat="server" Text='<%# Eval("UserName")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Level" >
                            <ItemTemplate>
                                &nbsp;
                                 <%#Eval("CompanyName")%>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle Width="8%" CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" >
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Email")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" Width="15%" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Adress">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("Address")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tel">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceTel" runat="server" Text='<%# Eval("Tel")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone Contact">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText2" runat="server" Text='<%# Eval("Fax")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trạng Thái">
                            <ItemTemplate>
                                &nbsp;
                                  <%--<div style='font-weight:bold; color:<%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Green" : (Eval("UserLike").ToString()=="1" ? "Red" : (Eval("UserLike").ToString()=="4" ? "Blue" :(Eval("UserLike").ToString()=="5" ? "Yellow" : "Black"))))%>' > 
                                    <asp:Label ID="Label4"  CssClass="text002" Text='<%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Sẵn Sàng" : "Đang Xữ Lý")%>'
                                    runat="server"></asp:Label>
                                    </div>--%>
                              <%--<div style='font-weight: bold; color: <%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Green" : (Eval("UserLike").ToString()=="1" ? "Red" : (Eval("UserLike").ToString()=="2" ? "Blue" :(Eval("UserLike").ToString()=="3" ? "orange" : "Black"))))%>'>
                            <asp:Label ID="Label4" CssClass="text002" Text='<%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Sẵn Sàng" :(Eval("UserLike").ToString()=="1" ? "Đang Xữ Lý" : (Eval("UserLike").ToString()=="2" ? "Nghỉ Buổi Sáng" :(Eval("UserLike").ToString()=="3" ? "Nghỉ Buổi Chiều" : "Nghỉ Phép Ngày") )))%>'
                                runat="server"></asp:Label>
                        </div>--%>
                        <div style='font-weight: bold; color: <%#string.Format("{0}", Eval("Gender").ToString()=="0" ? (Eval("UserLike").ToString()=="0" ? "Green" : "Red") : (Eval("Gender").ToString() =="400" ? "Black" : (Eval("Gender").ToString()=="200" ? "Blue" :(Eval("Gender").ToString()=="300" ? "orange" : "Green"))))%>'>
                            <asp:Label ID="Label4" CssClass="text002" Text='<%#string.Format("{0}", Eval("Gender").ToString()=="0" ? (Eval("UserLike").ToString()=="0" ? "Sẵn Sàng" : "Đang Xử Lý"):(Eval("Gender").ToString()=="400" ? "Nghỉ Phép Ngày" : (Eval("Gender").ToString()=="200" ? "Nghỉ Buổi Sáng" :(Eval("Gender").ToString()=="300" ? "Nghỉ Buổi Chiều" : "Sẵn Sàng"))))%>'
                                runat="server"></asp:Label>
                        </div>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="13%" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="MapLink">
                            <ItemTemplate>
                                &nbsp;
                                <a onclick='ShowPopupMapLink(<%#Eval("UserID") %>);'>
                                    <%# Eval("LoginID")%>
                                </a>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
                        </asp:TemplateField>--%>
                    </Columns>
                    <RowStyle HorizontalAlign="Center" BorderStyle="Double" BorderColor="Aqua"/>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:GridView>
        </td>
        </tr>

        <tr>
         <td style="background:#6092dc;padding:10px;font-weight:bold;color:#fff">
           Kỹ Thuật Bảo Hành
         </td>
         </tr>

        <tr>
        <td>
        
        <asp:GridView ID="gr_ktbh" runat="server" Width="100%" AutoGenerateColumns="False"
                    AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row" CellSpacing="1"
                    CellPadding="0" BorderWidth="0px" OnRowCommand="gvUser_RowCommand">
                    <Columns>
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
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" Width="10%" />
                            <ItemStyle CssClass="RB_L" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tên NV Kỹ Thuật">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText1" runat="server" Text='<%# Eval("UserName")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Level" >
                            <ItemTemplate>
                                &nbsp;
                                 <%#Eval("CompanyName")%>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle Width="8%" CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" >
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Email")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" Width="15%" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Adress">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("Address")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tel">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceTel" runat="server" Text='<%# Eval("Tel")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone Contact">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText2" runat="server" Text='<%# Eval("Fax")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trạng Thái">
                            <ItemTemplate>
                                &nbsp;
                                  <%--<div style='font-weight:bold; color:<%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Green" : (Eval("UserLike").ToString()=="1" ? "Red" : (Eval("UserLike").ToString()=="4" ? "Blue" :(Eval("UserLike").ToString()=="5" ? "Yellow" : "Black"))))%>' > 
                                    <asp:Label ID="Label4"  CssClass="text002" Text='<%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Sẵn Sàng" : "Đang Xữ Lý")%>'
                                    runat="server"></asp:Label>
                                    </div>--%>
                              <%--<div style='font-weight: bold; color: <%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Green" : (Eval("UserLike").ToString()=="1" ? "Red" : (Eval("UserLike").ToString()=="2" ? "Blue" :(Eval("UserLike").ToString()=="3" ? "orange" : "Black"))))%>'>
                            <asp:Label ID="Label4" CssClass="text002" Text='<%#string.Format("{0}", Eval("UserLike").ToString()=="0" ? "Sẵn Sàng" :(Eval("UserLike").ToString()=="1" ? "Đang Xữ Lý" : (Eval("UserLike").ToString()=="2" ? "Nghỉ Buổi Sáng" :(Eval("UserLike").ToString()=="3" ? "Nghỉ Buổi Chiều" : "Nghỉ Phép Ngày") )))%>'
                                runat="server"></asp:Label>
                        </div>--%>
                        <div style='font-weight: bold; color: <%#string.Format("{0}", Eval("Gender").ToString()=="0" ? (Eval("UserLike").ToString()=="0" ? "Green" : "Red") : (Eval("Gender").ToString() =="400" ? "Black" : (Eval("Gender").ToString()=="200" ? "Blue" :(Eval("Gender").ToString()=="300" ? "orange" : "Green"))))%>'>
                            <asp:Label ID="Label4" CssClass="text002" Text='<%#string.Format("{0}", Eval("Gender").ToString()=="0" ? (Eval("UserLike").ToString()=="0" ? "Sẵn Sàng" : "Đang Xử Lý"):(Eval("Gender").ToString()=="400" ? "Nghỉ Phép Ngày" : (Eval("Gender").ToString()=="200" ? "Nghỉ Buổi Sáng" :(Eval("Gender").ToString()=="300" ? "Nghỉ Buổi Chiều" : "Sẵn Sàng"))))%>'
                                runat="server"></asp:Label>
                        </div>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="13%" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="MapLink">
                            <ItemTemplate>
                                &nbsp;
                                <a onclick='ShowPopupMapLink(<%#Eval("UserID") %>);'>
                                    <%# Eval("LoginID")%>
                                </a>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="10%" />
                        </asp:TemplateField>--%>
                    </Columns>
                    <RowStyle HorizontalAlign="Center" BorderStyle="Double" BorderColor="Aqua"/>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:GridView>
        </td>
        </tr>
    </table>
</div>
