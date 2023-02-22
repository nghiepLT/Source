<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TextBoxMultiLanguage.ascx.cs"
    Inherits="NewsMng.TextBoxMultiLanguage" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <asp:Repeater ID="rptText" runat="server">
        <ItemTemplate>
            <tr>
                <th class="RB_L" width='<%#TitleWidth %>'>
                    <%#Title + " " + Eval("Name") %>
                </td>
                <td class="B_L" colspan="3">
                    <asp:TextBox ID="txtName" CssClass="Input_text" runat="server" TextMode='<%# TextMode %>' MaxLength='<%# MaxLength %>' 
                    Width='<%# TextWidth %>' Height='<%# TextHeight %>' Text='<%#GetText(Eval("LanguageID")) %>'></asp:TextBox>
                    <asp:HiddenField ID="hdnLangID" Value='<%#Eval("LanguageID") %>'  runat="server" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
