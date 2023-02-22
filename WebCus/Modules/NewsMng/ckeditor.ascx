<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ckeditor.ascx.cs" Inherits="NewsMng.ckeditor" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<%--<CKEditor:CKEditorControl ID="CKEditorControl1" BasePath="Include/ObjCK/ckeditor/" runat="server"></CKEditor:CKEditorControl>--%>

<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <asp:Repeater ID="rptText" runat="server">
        <ItemTemplate>
            <tr>
                <th class="RB_L" width='<%#TitleWidth %>'>
                    <%#Title + " " + Eval("Name") %>
                </td>
                <td class="B_L" colspan="3">
                    
                  
                    <CKEditor:CKEditorControl ID="txtName" Width='<%# TextWidth %>' Height='<%# TextHeight %>' Text='<%#GetText(Eval("LanguageID")) %>' BasePath="Include/ObjCK/ckeditor/" runat="server"></CKEditor:CKEditorControl>

                    <asp:HiddenField ID="hdnLangID" Value='<%#Eval("LanguageID") %>' runat="server" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>