<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FCKMultiLanguage.ascx.cs"
    Inherits="NewsMng.FCKMultiLanguage" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <asp:Repeater ID="rptText" runat="server">
        <ItemTemplate>
            <tr>
                <th class="RB_L" width='<%#TitleWidth %>'>
                    <%#Title + " " + Eval("Name") %>
                </td>
                <td class="B_L" colspan="3">
                    <fckeditorv2:fckeditor ID="txtName" BasePath="/Include/Object/fckeditor/" ToolbarSet='<%#this.ToolbarSet %>'
                    Width='<%# TextWidth %>' Height='<%# TextHeight %>' Value='<%#GetText(Eval("LanguageID")) %>' runat="server">
                    </fckeditorv2:fckeditor>
                    <asp:HiddenField ID="hdnLangID" Value='<%#Eval("LanguageID") %>' runat="server" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
