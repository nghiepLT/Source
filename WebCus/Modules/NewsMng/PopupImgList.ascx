<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopupImgList.ascx.cs" Inherits="NewsMng.PopupImgList" %>
<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<script type="text/javascript">
    function Change_Order(p_obj) {
        $.ajax({
            type: "POST",
            url: "/Login.aspx/Change_Order_MapImg",
            data: "{MapAllID:'" + p_obj.id.replace('Map_', '') + "',order:'" + p_obj.value + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //                alert('Bind_Area_Regis_Form:' + textStatus + ":" + errorThrown); 
            }
        });
    }
</script>
<div class="page-title">
    <h2 class="icon-title">
        <span>Hình ảnh</span>
    </h2>
</div>
<div class="clearfix" style="width:1000px;margin:0 auto">
    <asp:Repeater ID="rpt_img" runat="server">
        <ItemTemplate>
            <div style="float:left;width:200px;">
                <asp:FileUpload ID="FileUpload1" CssClass="css_txt" Width="200px" Height="24px" runat="server" />
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div class="Line2" style="margin:10px 0px;">
    <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn-1" OnClick="btnSave_Click"/>
</div>
<div class="TboardBox">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="Line2">
            </td>
        </tr>
    </table>
</div>
      
            <div class="clearfix" style="width:1000px;margin:0 auto;">
                <asp:Repeater ID="rpt_listImg" runat="server" OnItemCommand="rpt_listImg_OnItemCommand">
                    <ItemTemplate>
                        <div style="float:left;width:160px;height:130px;position:relative;margin:5px 0;">
                            <img src='<%#GetImagePathImg(Eval("MapID"),3) %>' alt="" width="160px" height="100px"/>
                            <div style="position:absolute;right:0;top:0;z-index:1000;">
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("MapAllID") %>' CommandName="delete" OnClientClick="return confirm('Bạn thật sự muốn xóa?')">
                                    <img src="/Images/Icon/delete_s.gif" alt=""/>
                                </asp:LinkButton>
                            </div>
                            <div>
                                <input type="text" id='Map_<%#Eval("MapAllID") %>' value='<%# Eval("thu_tu")!=null ? Eval("thu_tu") : 100%>'
                                class="Input_text" style="width: 100%;height:20px;text-align:center;padding:3px 0;" onchange="Change_Order(this)" />
                            </div>
                        </div>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <div style="float:left;width:160px;height:130px;position:relative;margin:5px;">
                            <img src='<%#GetImagePathImg(Eval("MapID"),3) %>' alt="" width="160px" height="100px"/>
                            <div style="position:absolute;right:0;top:0;z-index:1000;">
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("MapAllID") %>' CommandName="delete" OnClientClick="return confirm('Bạn thật sự muốn xóa?')">
                                    <img src="/Images/Icon/delete_s.gif" alt=""/>
                                </asp:LinkButton>
                            </div>
                            <div>
                                <input type="text" id='Map_<%#Eval("MapAllID") %>' value='<%# Eval("thu_tu")!=null ? Eval("thu_tu") : 100%>'
                                class="Input_text" style="width: 100%;height:20px;text-align:center;padding:3px 0;" onchange="Change_Order(this)" />
                            </div>
                        </div>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </div>
      
       
    
