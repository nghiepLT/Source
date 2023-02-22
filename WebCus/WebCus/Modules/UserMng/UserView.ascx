<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="UserView.ascx.cs"
    Inherits="UserMng.UserView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript">

    function CheckValidate() {
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

</script>
<div class="page-title">
    <h2 class="icon-title">
        <span>Cập nhật thông tin</span>
    </h2>
</div>
<br/>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tbody>
        <tr>
            <td class="C">
            </td>
            <td class="R">
                <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn-1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </tbody>
</table>
<div class="TboardBox">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="4" class="Line2">
            </td>
        </tr>
        <tr>
         <th width="20%" class="RB_L">
                <span class="txt9_R">*</span>
                <asp:Label ID="Label2" runat="server" Text="LoginID"></asp:Label>
            </th>
            <td class="B_L" >
                <asp:TextBox ID="txt_loginid" runat="server" CssClass="Input_text" Width="90%" ReadOnly="true"></asp:TextBox>
            </td>
           
           
            <th width="20%" class="RB_L">
                <span class="txt9_R">*</span>
                <asp:Label ID="Label1" runat="server" Text="FullName"></asp:Label>
            </th>
            <td class="B_L">
                <asp:TextBox ID="txtUsername" runat="server" CssClass="Input_text" Width="90%" ReadOnly="true"></asp:TextBox>
            </td>
           
           
           
        </tr>
        
        <tr >
            <th width="20%" class="RB_L">
                <span class="txt9_R">*</span>
                <asp:Label ID="Label7" runat="server" Text="Password"></asp:Label>
            </th>
            <td class="B_L">
                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="Input_text"
                    Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                <span class="txt9_R">*</span>
                <asp:Label ID="Label9" runat="server" Text="Confirm Password"></asp:Label>
            </th>
            <td class="B_L ">
                <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" CssClass="Input_text"
                    Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none;">
            <th width="20%" class="RB_L">
                <span class="txt9_R">*</span>
                <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label>
            </th>
            <td class="B_L ">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="Input_text" Width="80%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                <asp:Label ID="Label5" runat="server" Text="Address"></asp:Label>
            </th>
            <td class="B_L ">
                <asp:TextBox ID="txtAddress" runat="server" CssClass="Input_text" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none;">
            <th width="20%" class="RB_L">
                <asp:Label ID="Label15" runat="server" Text="Tel"></asp:Label>
            </th>
            <td class="B_L ">
                <asp:TextBox ID="txtTel" runat="server" CssClass="Input_text" Width="80%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                <asp:Label ID="Label10" runat="server" Text="One contact"></asp:Label>
            </th>
            <td class="B_L ">
                <asp:TextBox ID="txtFax" runat="server" CssClass="Input_text" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none;">
            <th width="20%" class="RB_L">
                <asp:Label ID="Label12" runat="server" Text="IsExpire"></asp:Label>
            </th>
            <td class="B_L ">
                <asp:Label ID="lblIsExpire" runat="server" Text="Label"></asp:Label>
            </td>
            <th width="20%" class="RB_L">
                <asp:Label ID="Label13" runat="server" Text="ExprireDate (MM-dd-yyyy)"></asp:Label>
            </th>
            <td class="B_L ">
                <asp:Label ID="lblExpireDate" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="Line1" height="1">
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tbttNhanvien"
        runat="server">
        <tr>
            <td colspan="4" class="Line2" >
          
            </td>
        </tr>
         <tr>
            <td colspan="4" style="margin-top:10px; text-align:center; background:url(images/common/bkg-btn-blue.gif) repeat-x scroll 0 top #3A8FCE;color:#fff;padding:10px;" >
            THÔNG TIN NHÂN VIÊN
            </td>
        </tr>
        <tr id="tr_hoten" runat="server">
            <th width="20%" class="RB_L">
                Họ tên
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_hoten" runat="server" CssClass="Input_text" Width="90%" ReadOnly="true"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Giới tính
            </th>
            <td class="B_L" width="30%">
                <asp:DropDownList ID="dr_gioitinh" runat="server" CssClass="demo-default" Width="50%" Enabled="false">
                    <asp:ListItem Text="--Chọn giới tính--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Nam" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Nữ" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="tr1" runat="server">
            <th width="20%" class="RB_L">
                Ngày Sinh
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_ngaysinh" runat="server" CssClass="Input_text" Width="50%" ReadOnly="true"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Nơi Sinh
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_noisinh" runat="server" CssClass="Input_text" Width="90%" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr2" runat="server">
            <th width="20%" class="RB_L">
                Số CMND
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_socmnd" runat="server" ReadOnly="true" CssClass="Input_text" Width="90%" onkeypress="return ValidateKeypress(/\d/,event);"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Ngày Cấp CMND
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_ngaycapcmnd" ReadOnly="true" runat="server" CssClass="Input_text" Width="25%"></asp:TextBox>
                Nơi Cấp :  <asp:TextBox ID="txt_noicapcmnd" ReadOnly="true" runat="server" CssClass="Input_text" Width="44.5%"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr3" runat="server">
            <th width="20%" class="RB_L">
                Đ/c Tạm Trú
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_dctamtru" runat="server" ReadOnly="true" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Đ/c Thường Trú
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_dcthuongtru" runat="server" ReadOnly="true" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr4" runat="server">
            <th width="20%" class="RB_L">
                Email
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_email" runat="server" ReadOnly="true" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Số Điện Thoại
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_sdt" runat="server" ReadOnly="true" CssClass="Input_text" Width="90%" onkeypress="return ValidateKeypress(/\d/,event);"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr5" runat="server">
            <th width="20%" class="RB_L">
                Trình Độ
            </th>
            <td class="B_L" width="30%">
                <asp:DropDownList ID="dr_trinhdonhanvien" Enabled="false" runat="server" CssClass="demo-default"
                    Width="92.5%">
                    <asp:ListItem Text="--Chọn Trình Độ--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="THPT" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Cao Đẳng" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Đại Học" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Sau Đại Học" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <th width="20%" class="RB_L">
                Chuyên Môn
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_chuyenmon" runat="server" ReadOnly="true" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr6" runat="server">
            <th width="20%" class="RB_L">
                Kinh Nghiệm
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_kinhnghiem" runat="server" ReadOnly="true" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Mã số thuế
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_masothue" runat="server" ReadOnly="true" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr7" runat="server">
            <th width="20%" class="RB_L">
                TK Ngân hàng
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_tknganhang" runat="server" ReadOnly="true" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Số TK Ngân Hàng
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txtsotknganhang" runat="server" ReadOnly="true" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr10" runat="server">
            <th width="20%" class="RB_L">
                Dân tộc
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_dantoc" runat="server" ReadOnly="true" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Tôn giáo
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_tongiao" runat="server" ReadOnly="true" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr8" runat="server">
            <th width="20%" class="RB_L">
                Nguyên Quán
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_nguyenQuan" runat="server" ReadOnly="true" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
             <th class="RB_L" width="15%">
           Tình trạng hôn nhân
            </th>
             <td  class="B_L">
                <asp:TextBox ID="txt_tinhtranghonnhan" runat="server" ReadOnly="true" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
           
        </tr>
        <tr>
            <th class="RB_L" width="15%">
                <asp:Label ID="Label16" runat="server" Text="Ảnh NV"></asp:Label>
            </th>
            <td class="B_L" colspan="3">
                <table onmouseover="return ShowImage(event, 1);" onmouseout="return ShowImage(event, 0);">
                    <tr>
                        <td>
                           
                             <asp:Image ID="img_user" runat="server" width="150px" height="160px"  />
                        </td>
                    </tr>
                </table>                
            </td>
           <%-- <th width="20%" class="RB_L">
                Ghi chú
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_ghichunhanvien" ReadOnly="true" runat="server" CssClass="Input_text" Width="90%" TextMode="MultiLine"></asp:TextBox>
            </td>--%>
        </tr>
        
        <%--<tr>
            <td class="B_L" colspan="4" style="margin-top:10px; text-align:center;">
                <asp:Button ID="btn_capnhatnhanvien" Width="100px" CssClass="btn-1" Text="Cập nhật"
                    runat="server" OnClick="btnSave_Click" OnClientClick="return verifyupdate();" />
            </td>
        </tr>--%>
    </table>
</div>
