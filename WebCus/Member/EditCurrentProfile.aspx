<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditCurrentProfile.aspx.cs" Inherits="WebCus.EditCurrentProfile" %>

<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<%@ Register Src="../ASCX/Content.ascx" TagName="Content" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <title>Thông tin thành viên</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function SetFocus(e) {
            var keynum;
            if (window.event) // IE
                keynum = e.keyCode;
            else if (e.which) // Netscape/Firefox/Opera
                keynum = e.which;
            if (keynum == 13) {
                var obj = document.getElementById('<%=btnEditProfile.ClientID %>');
                obj.click();
                return false;
            }

        }
        $(document).ready(function () {
            var hash = $(location).attr('hash');
            if (hash == '#xemDonHang')
            {
                $('#div_info').hide();
                $('#div_Order').show();
            }
        });

        $(document).ready(function () {
            $('#DoiPass').click(function () {
                $('#div_info').hide();
                $('#div_changepass').show();
            });

            $('#xemDonHang').click(function () {
                $('#div_info').hide();
                $('#div_Order').show();
            });

            $('#thongtin').click(function () {
                $('#div_info').show();
                $('#div_changepass').hide();
            });


            $('#thongtin2').click(function () {
                $('#div_info').show();
                $('#div_Order').hide();
            });
        })
    </script>
    <script type="text/javascript">
        function validatePassConfig(oSrc, args) {
            var a = $('#<%=txt_passnew.ClientID %>').val();
            var b = args.Value;

            args.IsValid = (a == b);
        }
    </script>
    <!--  -->
   <div class="container">
          <h1 style="text-align: center; margin-bottom: 20px;"><%=this.ClientLanguageMsg("lngifon") %></h1>
        <div id="div_info" class="col-md-6 col-md-offset-3 col-xs-12">
             <div class="form-group">
                <label><span style="color: red;">*</span>      <%=this.ClientLanguageMsg("lngFullname") %>:</label>
                 <asp:TextBox ID="txtUserName"  CssClass="form-control"
                     runat="server"></asp:TextBox>
                 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtUserName"
                     SetFocusOnError="true" Display="None" ValidationGroup="reg" runat="server" ></asp:RequiredFieldValidator>
             </div>

            <div class="form-group">
                <label><%=this.ClientLanguageMsg("lngUser") %>:</label>
                <asp:Label ID="lbl_tendangnhap" runat="server" Text=""></asp:Label>
            </div>
            <div class="form-group">
                <label>Mật khẩu: ***</label> <label id="DoiPass" style="cursor: pointer;" class=""><%=this.ClientLanguageMsg("lngChangePass") %> </label>
            </div>
            <div class="form-group">
                <span id="xemDonHang" style="cursor: pointer;" class="btn btn-success"><%=this.ClientLanguageMsg("lngHistoryOrder") %> </span>
            </div>
            <div class="form-group">
                <label>Email:</label>
                <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
            </div>

            <div class="form-group">
                <label>    <span style="color: red;">*</span><%=this.ClientLanguageMsg("lngTel") %>:</label>
                <asp:TextBox ID="txtTel" CssClass="form-control" runat="server"></asp:TextBox>
            
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtTel"
                    SetFocusOnError="true" Display="None" ValidationGroup="reg" runat="server"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label><span style="color: red;">*</span> <%=this.ClientLanguageMsg("lngAddress") %>:</label>
                <asp:TextBox ID="txtAddress" CssClass="form-control"
                    runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtAddress"
                    SetFocusOnError="true" Display="None" ValidationGroup="reg" runat="server"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label><%=this.ClientLanguageMsg("lngGender") %>:</label>
                <asp:RadioButton ID="rdoGender_Male" runat="server" GroupName="Gender"
                    Checked="true" />
                &nbsp;
               <asp:RadioButton ID="rdoGender_Female" runat="server" GroupName="Gender" />
            </div>

            <div class="form-group">
                <label><span style="color: red;">*</span><%=this.ClientLanguageMsg("lngCodeSecret") %>:</label>
                <asp:TextBox ID="txtVerifyCode" CssClass="form-control"
                    runat="server"></asp:TextBox>

            </div>

            <div class="form-group">
                <asp:Image ID="Image2" runat="server" ImageUrl="/ImageValidator.aspx?code=0" Width="80px"
                    Height="20px" Style="padding-right: 10px; margin: 0;" />
                <asp:ImageButton ID="btnChangeImg" runat="server" ImageUrl="/Images/icon/refresh_icon.gif"
                    OnClientClick="return reloadImg();" Style="" />
            </div>

            <div class="form-group">
                <asp:ValidationSummary ShowSummary="false" ShowMessageBox="true" ValidationGroup="reg"
                    ID="ValidationSummary2" runat="server" />
                <asp:LinkButton ID="btnEditProfile"  OnClick="btnCreate_Click" ValidationGroup="reg"
                   CssClass="btn btn-info" runat="server">
                                        <%=this.ClientLanguageMsg("lngUpdate") %>
                </asp:LinkButton>

            </div>

        </div>
        <div class="col-md-6 col-md-offset-3 col-xs-12">
            <div id="div_changepass" style="display: none;">
                <div class="form-group">
                    <label>  <span style="color: red;">*</span><%=this.ClientLanguageMsg("lngOldPass") %>:</label>
                    <asp:TextBox ID="txt_passold" CssClass="form-control"
                        TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txt_passold"
                        SetFocusOnError="true" Display="None" ValidationGroup="Pass" runat="server" ></asp:RequiredFieldValidator>

                </div>

                 <div class="form-group">
                    <label>  <span style="color: red;">*</span> <%=this.ClientLanguageMsg("lngNewPass") %>:</label>
                     <asp:TextBox ID="txt_passnew" CssClass="form-control"
                         TextMode="Password" runat="server"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_passnew"
                         SetFocusOnError="true" Display="None" ValidationGroup="Pass" runat="server" ></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txt_passnew"
                         ValidationExpression="^[a-zA-Z0-9_]{6,20}$" runat="server" Display="None" SetFocusOnError="true"
                         ValidationGroup="Pass" >
                     </asp:RegularExpressionValidator>

                 </div>

                <div class="form-group">
                    <label><span style="color: red;">*</span><%=this.ClientLanguageMsg("lngPassConfirm") %>:</label>
                    <asp:TextBox ID="txt_passnewconfirm" CssClass="form-control"
                        TextMode="Password" runat="server"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidator1" Display="None" runat="server" ValidationGroup="Pass"
                        ControlToValidate="txt_passnewconfirm" 
                        ClientValidationFunction="validatePassConfig"></asp:CustomValidator>

                </div>

                <div class="form-group">
                    <label><span style="color: red;">*</span>   <%=this.ClientLanguageMsg("lngCodeSecret") %>:</label>
                    <asp:TextBox ID="txtVerifyCode02" CssClass="form-control"
                        runat="server"></asp:TextBox>

                </div>

                <div class="form-group">
                    <label></label>
                    <asp:Image ID="Image1" runat="server" ImageUrl="/ImageValidator.aspx?code=0" Width="80px"
                        Height="20px" Style="padding-right: 10px; margin: 0;" />
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="/Images/icon/refresh_icon.gif"
                        OnClientClick="return reloadImg();" Style="" />
                </div>

                <div class="form-group">
                    <asp:ValidationSummary ShowSummary="false" ShowMessageBox="true" ValidationGroup="Pass"
                        ID="ValidationSummary1" runat="server" />
                    <asp:LinkButton ID="btn_changepass"  OnClick="btn_changepass_Click" ValidationGroup="Pass"
                        CssClass="btn btn-info" runat="server">
                                        <%=this.ClientLanguageMsg("lngUpdate") %>
                    </asp:LinkButton>
                    <input id="thongtin" type="button" value="<%= this.ClientLanguageMsg("lngifon") %>" style="cursor: pointer;"
                        class="btn btn-danger" />
                </div>

            </div>

        </div>
      
         <div id="div_Order" class="col-md-12 col-xs-12" style="display:none;">
             
             <h3><%#this.ClientLanguageMsg("lngYourOrder") %></h3>
               <asp:Label ID="lblNoOrders" runat="server" ForeColor="Red" Visible="false"><%#this.ClientLanguageMsg("lngOrderEmpty") %></asp:Label>
             <div class="table-responsive">
             <asp:GridView ID="gvList" runat="server" CssClass="table " AutoGenerateColumns="false"
                  OnRowDataBound="gvList_RowDataBound" Width="100%">
                 <Columns>
                     <asp:TemplateField HeaderText="Mã Đơn Hàng">
                         <HeaderTemplate>
                             <%#this.ClientLanguageMsg("lngOrderID") %>
                         </HeaderTemplate>
                         <ItemTemplate>
                             MS<%# Eval("OrderID")%>
                         </ItemTemplate>
                         <HeaderStyle CssClass="RB_L" />
                         <ItemStyle Width="70px" CssClass="RB_L" />
                     </asp:TemplateField>
                 </Columns>

                 <Columns>
                     <asp:TemplateField HeaderText="Tổng Tiền">
                         <HeaderTemplate>
                             <%#this.ClientLanguageMsg("lngTotalAmountOrder") %>
                         </HeaderTemplate>
                         <ItemTemplate>
                             <%# Eval("Total", "{0:N0}")%>
                         </ItemTemplate>
                         <HeaderStyle CssClass="RB_L" />
                         <ItemStyle Width="70px" CssClass="RB_L" />
                     </asp:TemplateField>
                 </Columns>
                 <Columns>
                     <asp:TemplateField HeaderText="Người nhận/SĐT/Địa Chỉ">
                         <HeaderTemplate>
                            <%#this.ClientLanguageMsg("lngRecevie") %>
                         </HeaderTemplate>
                         <ItemTemplate>
                             <%# Eval("FirstName")%>/<%# Eval("Tel") %>/<%# Eval("Email") %>
                         </ItemTemplate>
                         <ItemStyle CssClass="RB_L" />
                         <HeaderStyle CssClass="RB_L" Width="120px" />
                     </asp:TemplateField>
                 </Columns>

                 <Columns>
                     <asp:TemplateField HeaderText="">
                         <HeaderTemplate>
                              <%#this.ClientLanguageMsg("lngProductName") %>
                         </HeaderTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label2" CssClass="text002" ToolTip='<%# Eval("Name")%>' Text='<%# WebCus.Utilis.TrimText(Eval("Name"),150) %>'
                                 runat="server"></asp:Label>
                         </ItemTemplate>
                         <ItemStyle CssClass="RB_L" />
                         <HeaderStyle CssClass="RB_L" />
                     </asp:TemplateField>
                 </Columns>
                 <Columns>
                     <asp:TemplateField HeaderText="">
                         <HeaderTemplate>
                              <%#this.ClientLanguageMsg("lngPrice") %>
                         </HeaderTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label6" CssClass="text002" Text='<%# Eval("Price","{0:N0}") %>' runat="server"></asp:Label>
                         </ItemTemplate>
                         <ItemStyle Width="70px" CssClass="RB_L" />
                         <HeaderStyle CssClass="RB_L" />
                     </asp:TemplateField>
                 </Columns>
                 <Columns>
                     <asp:TemplateField HeaderText="">
                         <HeaderTemplate>
                            <%#this.ClientLanguageMsg("lngQuatation") %>
                         </HeaderTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label9" CssClass="text002" Text='<%# Eval("Quantity","{0:N0}") %>'
                                 runat="server"></asp:Label>
                         </ItemTemplate>
                         <ItemStyle Width="30px" CssClass="RB_L" />
                         <HeaderStyle CssClass="RB_L" />
                     </asp:TemplateField>
                 </Columns>
                 <Columns>
                     <asp:TemplateField HeaderText="">
                         <HeaderTemplate>
                                                         <%#this.ClientLanguageMsg("lngStatus") %>

                                <br />
                                                         <%#this.ClientLanguageMsg("lngPaymentMethod") %>

                         </HeaderTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label4" CssClass="text002" Text='<%# ((WebCus.Transaction_Status)(Eval("Status"))).ToString() %>'
                                 runat="server"></asp:Label>
                             <br />
                             <asp:Label ID="Label7" CssClass="text002" Text='<%# ((WebCus.PaymentType)(Eval("PaymentMethod"))).ToString() %>'
                                 runat="server"></asp:Label>
                         </ItemTemplate>
                         <ItemStyle Width="60px" CssClass="RB_L" />
                         <HeaderStyle CssClass="RB_L" />
                     </asp:TemplateField>
                 </Columns>
                 <Columns>
                     <asp:TemplateField HeaderText="">
                         <HeaderTemplate>
                               <%#this.ClientLanguageMsg("lngDateBought") %>
                         </HeaderTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label3" CssClass="text002" Text='<%# Eval("RegDate","{0:yyyy-MM-dd}") %>'
                                 runat="server"></asp:Label>
                             <br />
                             <asp:Label ID="Label8" CssClass="text002" Text='<%# Eval("RegDate","{0:HH:mm}") %>'
                                 runat="server"></asp:Label>
                         </ItemTemplate>
                         <ItemStyle Width="70px" CssClass="RB_L" />
                         <HeaderStyle CssClass="RB_L" />
                     </asp:TemplateField>
                 </Columns>
                 <Columns>
                     <asp:TemplateField HeaderText="">
                         <HeaderTemplate>
                              <%#this.ClientLanguageMsg("lngRemark") %>
                         </HeaderTemplate>
                         <ItemTemplate>
                             <%# Eval("Note")%>
                         </ItemTemplate>
                         <ItemStyle Width="50px" CssClass="RB_C" />
                         <HeaderStyle CssClass="RB_L" />
                     </asp:TemplateField>
                 </Columns>
             </asp:GridView>
             </div>

              <div class="col-md-12 col-xs-12">
                  
                  <div class="pagination" style="margin-bottom:20px;float:right">
                      <pqt:PQTPager ID="PQTPager1" runat="server" OnPageIndexChanged="Pager_PageIndexChanged"
                          Width="410px" />
                  </div>
               
              </div>
              <input id="thongtin2" type="button" value="<%=this.ClientLanguageMsg("lngifon") %>" style="cursor: pointer; margin-bottom:20px;"
                      class="btn btn-danger" />
         </div>
    </div>
</asp:Content>
