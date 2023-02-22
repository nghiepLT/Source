<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebLink.ascx.cs" Inherits="UserMng.WebLink" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>

<script type="text/javascript">

    function CheckValidate() {



        return true;
    }

    function CheckDelete() {

    }
    
</script>

<table border="0" cellspacing="0" cellpadding="0" width="100%">
    <tr>
        <td style="font-size:1pt;">
            <img src="Images/Other/corner_Top_Left.gif" />
        </td>
        <td style="background-image: url('Images/Other/bar_Top.gif');">
        </td>
        <td style="font-size:1pt;">
            <img src="Images/Other/corner_Top_right.gif" />
        </td>
    </tr>
    <tr>
        <td style="background-image: url('Images/Other/bar_Left.gif');" width='5'>
        </td>
        <td class="R">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="pdL5 L">
                        <img src="/images/Icon/bullet_Tit.gif" />
                        <b>
                            <asp:Label ID="Label4" runat="server" Text="Link Website"></asp:Label>
                        </b>
                    </td>
                    <td class="R">
                        <b>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" Style="background: url('Images/Icon/delete.gif') no-repeat left;
                                padding-bottom: 3px; padding-left: 25px; padding-right: 15px" OnClientClick="return CheckDelete();"
                                OnClick="btnDelete_Click" />
                        </b><b>
                            <asp:Button ID="btnInsert" runat="server" Text="Insert" Style="background: url('Images/Icon/btn_modify.gif') no-repeat left;
                                padding-bottom: 3px; padding-left: 25px; padding-right: 15px" OnClientClick="return CheckValidate();"
                                OnClick="btnInsert_Click" />
                        </b><b>
                            <asp:Button ID="btnSave" runat="server" Text="Save" Style="background: url('Images/Icon/save.gif') no-repeat left;
                                padding-bottom: 3px; padding-left: 25px; padding-right: 15px" OnClientClick="return CheckValidate();"
                                OnClick="btnSave_Click" />
                        </b>
                    </td>
                </tr>
            </table>
        </td>
        <td style="background-image: url('Images/Other/bar_Right.gif');" width='5'>
        </td>
    </tr>
    <tr>
        <td style="font-size:1pt;">
            <img src="Images/Other/corner_Bottom_Left.gif" />
        </td>
        <td style="background-image: url('Images/Other/bar_Bottom.gif');">
        </td>
        <td style="font-size:1pt;">
            <img src="Images/Other/corner_Bottom_Right.gif" />
        </td>
    </tr>
</table>
<br />
<table border="0" cellspacing="0" cellpadding="0" width="100%">
    <tr>
        <td style="font-size:1pt;">
            <img src="Images/Other/corner_Top_Left.gif" />
        </td>
        <td style="background-image: url('Images/Other/bar_Top.gif');">
        </td>
        <td style="font-size:1pt;">
            <img src="Images/Other/corner_Top_right.gif" />
        </td>
    </tr>
    <tr>
        <td style="background-image: url('Images/Other/bar_Left.gif');" width='5'>
        </td>
        <td class="R">
            <table width="100%">
                <tr>
                    <td width="20%" class="T">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="pdL5 L ">
                                        <asp:TreeView ID="treeWebLink" runat="server" ShowLines="true" 
                                            onselectednodechanged="treeWebLink_SelectedNodeChanged" >
                                            <SelectedNodeStyle CssClass="Link02" />
                                            <NodeStyle Font-Bold="true" />
                                        </asp:TreeView>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="T">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th class="RB_L" width="20%">
                                    <asp:Label ID="Label8" runat="server" Text="IsView"></asp:Label>
                                </td>
                                <td class="B_L" width="30%" style="width: 80%">
                                    <asp:RadioButton ID="rdoIsViewY" runat="server" GroupName="IsView" Text="Yes" 
                                        Checked="True" />
                                    &nbsp;&nbsp;
                                    <asp:RadioButton ID="rdoIsViewN" runat="server" GroupName="IsView" Text="No" />
                                &nbsp;
                                    <asp:Label ID="lblID" runat="server" Text="" style="display:none"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th class="RB_L" width="20%">
                                    <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
                                </td>
                                <td class="B_L" width="30%" style="width: 80%">
                                    <asp:TextBox ID="txtName" runat="server" Width="95%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th class="RB_L" width="20%">
                                    <asp:Label ID="Label2" runat="server" Text="URL"></asp:Label>
                                </td>
                                <td class="B_L" width="30%" style="width: 80%">
                                    <asp:TextBox ID="txtUrl" runat="server" Width="95%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td style="background-image: url('Images/Other/bar_Right.gif');" width='5'>
        </td>
    </tr>
    <tr>
        <td style="font-size:1pt;">
            <img src="Images/Other/corner_Bottom_Left.gif" />
        </td>
        <td style="background-image: url('Images/Other/bar_Bottom.gif');">
        </td>
        <td style="font-size:1pt;">
            <img src="Images/Other/corner_Bottom_Right.gif" />
        </td>
    </tr>
</table>
<div id="divImageProduct" style="display: none; background-color: Gray; padding: 2px">
    <div style="padding: 3px; background-color: White">
        <asp:Image ID="imgProduct" runat="server" Width="300px" Height="300px" />
    </div>
</div>
<%--<table border="0" cellspacing="0" cellpadding="0" width='100%'>
                <tr>
                    <td style="font-size: 1pt;">
                        <img src="Images/Other/corner_Top_Left.gif" />
                    </td>
                    <td style="background-image: url('Images/Other/bar_Top.gif');">
                    </td>
                    <td style="font-size: 1pt;">
                        <img src="Images/Other/corner_Top_right.gif" />
                    </td>
                </tr>
                <tr>
                    <td style="background-image: url('Images/Other/bar_Left.gif');" width='5'>
                    </td>
                    <td class="R">
                        
                    </td>
                    <td style="background-image: url('Images/Other/bar_Right.gif');" width='5'>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 1pt;">
                        <img src="Images/Other/corner_Bottom_Left.gif" />
                    </td>
                    <td style="background-image: url('Images/Other/bar_Bottom.gif');">
                    </td>
                    <td style="font-size: 1pt;">
                        <img src="Images/Other/corner_Bottom_Right.gif" />
                    </td>
                </tr>
            </table>--%>