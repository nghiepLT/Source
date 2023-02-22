<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="FileMng.ascx.cs"
    Inherits="UserMng.FileMng" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link rel="stylesheet" type="text/css" href="/js/context_menu/jqcontextmenu.css" />
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
<script type="text/javascript" src="/js/context_menu/jqcontextmenu.js">

    /***********************************************
    * jQuery Context Menu- (c) Dynamic Drive DHTML code library (www.dynamicdrive.com)
    * This notice MUST stay intact for legal use
    * Visit Dynamic Drive at http://www.dynamicdrive.com/ for this script and 100s more
    ***********************************************/

</script>
<script type="text/javascript">

    jQuery(document).ready(function ($) {
        $('#area_file').addcontextmenu('contextmenu2')
        $('.class_folder').addcontextmenu('contextmenu1')
        $('.class_file').addcontextmenu('contextmenu3')
    })

    function SetFocus_Create_Folder(e, p_obj) {
        if (!e) { var e = window.event; }
        var keynum;
        if (window.event) // IE
            keynum = e.keyCode;
        else if (e.which) // Netscape/Firefox/Opera
            keynum = e.which;
        if (keynum == 13) {
            Create_Folder(p_obj);
            e.preventDefault();
            return false;
        }
    }

</script>
<script type="text/javascript">

    function CheckDelete() {
        var hdfNodeValue = document.getElementById('<%=hdfNodeValue.ClientID %>');
        if (hdfNodeValue.value != '') {
            var valueNode = hdfNodeValue.value;
            var isFolder = valueNode.substring(valueNode.length - 3, valueNode.length - 2);
            var isHaveFile = valueNode.substring(valueNode.length - 1, valueNode.length);
            if (isFolder == 1 && isHaveFile == 1) {
                alert('Thư mục này có chứa file, bạn không thẻ xóa cả thư mục');
                return false;
                //                return confirm('Thư mục này có chứa file, bạn có chắc xóa cả thư mục không?');
            }
            else {
                hdfNodeValue.value = isFolder;
                return confirm('Bạn có chắc xóa không')
            }
        }
        else {
            alert('Vui lòng chọn file cần xóa');
            return false;
        }

    }

    function CheckCreateFolder() {
        var hdfNodeValue = document.getElementById('<%=hdfNodeValue.ClientID %>');
        if (hdfNodeValue.value != '') {
            var valueNode = hdfNodeValue.value;
            var isFolder = valueNode.substring(valueNode.length - 3, valueNode.length - 2);
            if (isFolder == 0) {
                alert('Vui lòng chọn thư mục cần tạo thư mục con');
                return false;
            }
            else {
                var folderName = prompt("Nhập tên thư mục", "New Folder");
                hdfNodeValue.value = folderName;
                return true;
            }
        }
        else {
            alert('Vui lòng chọn thư mục cần tạo thư mục con');
            return false;
        }

    }

</script>
<div class="page-title">
    <h2 class="icon-title">
        <span>Quản lý file</span>
    </h2>
</div>
<br>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tbody>
        <tr>
            <td>
                &nbsp;
            </td>
            <td class="R">
                <asp:Button ID="btnDelete" runat="server" Visible="false" CssClass="btn-1" Text="Delete"
                    OnClick="btnDelete_Click" OnClientClick="return CheckDelete();" />
                &nbsp;
                <asp:Button ID="btnNewFolder" runat="server" Text="New Folder" Visible="false" OnClick="btnNewFolder_Click"
                    OnClientClick="return CheckCreateFolder();" />
            </td>
        </tr>
    </tbody>
</table>
<div class="TboardBox">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="2" class="Line2">
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
                <div class="TreeFile over" style="min-height: 400px; max-width: 220px; overflow: hidden;">
                    <asp:TreeView ID="treeFile" runat="server" OnSelectedNodeChanged="treeFile_SelectedNodeChanged"
                        ShowLines="True">
                        <NodeStyle HorizontalPadding="5px" />
                        <SelectedNodeStyle CssClass="NodeSelected" />
                    </asp:TreeView>
                    <asp:HiddenField ID="hdfNodeValue" runat="server" />
                </div>
            </th>
            <td class="setup02 pd5">
                <div id="area_file" style="height: 400px; overflow: auto;">
                    <asp:Repeater ID="rpt_file" runat="server">
                        <ItemTemplate>
                            <div class='<%#Eval("Is_Folder").ToString()=="1"?"class_folder":"class_file" %> Folder_item hover1'
                                id='file_folder_<%#Container.ItemIndex %>' ondblclick="Open_Folder('<%#Eval("Path").ToString().Replace("\\","/") %>', '<%#Eval("Is_Folder") %>');">
                                <img src='<%#Get_Url_Thumb(Eval("Name"),0, Eval("Is_Folder"))%>' alt='<%#Eval("Name") %>'
                                    title='<%#Eval("Name") %>' width="48px" height="48px" />
                                <div>
                                    <%#Eval("Name") %>
                                </div>
                                <span class="fi_ico_del">
                                    <img src="/images/icon/delete_s.gif" alt="Delete" title='Click để xóa' onclick="return Delete_Item('<%#Eval("Path").ToString().Replace("\\","/") %>', '<%#Eval("Is_Folder") %>', <%#Container.ItemIndex %>, 1)" />
                                </span>
                                <input type="hidden" id='hdn_is_folder_<%#Container.ItemIndex %>' value='<%#Eval("Is_Folder")%>' />
                                <input type="hidden" id='hdn_path_<%#Container.ItemIndex %>' value='<%#Eval("Path").ToString().Replace("\\","/") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="Line1">
            </td>
        </tr>
    </table>
    <div style="display: none;">
        <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_Click" />
        <br />
        <br />
        <asp:Button ID="btnRename" runat="server" Text="Rename" OnClick="btnRename_Click" />
    </div>
</div>
<ul id="contextmenu2" class="jqcontextmenu">
    <li onclick="return New_Folder();"><span>
        <img src="/Images/Icon/IcoFileExt/folder.gif" alt="New Folder" /></span>New Folder</li>
</ul>
<ul id="contextmenu1" class="jqcontextmenu">
    <li onclick="return Right_Click(1);"><span>
        <img src="/Images/Icon/IcoFileExt/icon_folderopen.gif" alt="Open folder" /></span>
        Open Folder</li>
    <li onclick="return Right_Click(2);"><span>
        <img src="/Images/Icon/btn_cancel.gif" alt="Delete" /></span>Delete</li>
    <li onclick="return Right_Click(3);"><span>
        <img src="/Images/Icon/icon-arrow.png" alt="View link" /></span>View link</li>
    <li onclick="return New_Folder();"><span>
        <img src="/Images/Icon/IcoFileExt/folder.gif" alt="New Folder" /></span>New Folder</li>
</ul>
<ul id="contextmenu3" class="jqcontextmenu">
    <li onclick="return Right_Click(2);"><span>
        <img src="/Images/Icon/btn_cancel.gif" alt="Delete" /></span>Delete</li>
    <li onclick="return Right_Click(3);"><span>
        <img src="/Images/Icon/icon-arrow.png" alt="View link" /></span>View link</li>
    <li onclick="return New_Folder();"><span>
        <img src="/Images/Icon/IcoFileExt/folder.gif" alt="New Folder" /></span>New Folder</li>
</ul>
<script type="text/javascript">

    function Right_Click(p_type) {

        var path = $('#' + click_obj.id.replace('file_folder_', 'hdn_path_')).val();
        var is_folder = $('#' + click_obj.id.replace('file_folder_', 'hdn_is_folder_')).val();
        var p_index = click_obj.id.replace('file_folder_', '');

        if (p_type == 1)
            Open_Folder(path, is_folder);
        else if (p_type == 2)
            Delete_Item(path);
        else if (p_type == 3)
            View_Link(path);

    }

    function View_Link(p_path) {
        $.ajax({
            type: "POST",
            url: "/Login.aspx/View_Link",
            data: "{p_path:'" + p_path.toString() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response.d)
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('Err:View_Link');
            }
        });
    }

    function copyToClipboardCrossbrowser(p_value) {

        if (window.clipboardData && clipboardData.setData) {
            clipboardData.setData("Text", p_value);
        }
        else {
            // You have to sign the code to enable this or allow the action in about:config by changing
            //user_pref("signed.applets.codebase_principal_support", true);
            netscape.security.PrivilegeManager.enablePrivilege('UniversalXPConnect');

            var clip = Components.classes["@mozilla.org/widget/clipboard;1"].createInstance(Components.interfaces.nsIClipboard);
            if (!clip) return;

            // create a transferable

            var trans = Components.classes["@mozilla.org/widget/transferable;1"].createInstance(Components.interfaces.nsITransferable);
            if (!trans) return;

            // specify the data we wish to handle. Plaintext in this case.
            trans.addDataFlavor('text/unicode');

            // To get the data from the transferable we need two new objects
            var str = new Object();
            var len = new Object();

            var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);

            str.data = p_value;

            trans.setTransferData("text/unicode", str, str.data.length * 2);

            var clipid = Components.interfaces.nsIClipboard;
            if (!clip) return false;
            clip.setData(trans, null, clipid.kGlobalClipboard);
        }
    }

    function New_Folder() {
        var html_new_Folder = '<div id="file_folder_new" class="Folder_item hover1">'
        html_new_Folder += '<img width="48px" height="48px" title="domain" alt="domain" src="/Images/icon/Icon_Big/icon_folder.png">'
        html_new_Folder += '<div><input id="txt_new_folder" onblur="Create_Folder(this)" onKeyPress="return SetFocus_Create_Folder(event, this)" type="text" value="New Folder"/> </div></div>';
        $('#area_file').append(html_new_Folder);
        $('#txt_new_folder').focus();
        return false;

    }

    function Delete_Item(p_path, p_is_folder, p_index, p_re_load) {
        if (confirm('Bạn muốn xóa mục này?')) {
            $.ajax({
                type: "POST",
                url: "/Login.aspx/Delete_File_Or_Folder",
                data: "{p_path:'" + p_path.toString() + "',p_Is_Folder:'" + p_is_folder + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.indexOf('Err') == -1) {
                        $('#file_folder_' + p_index).hide();
                        if (p_re_load == 1)
                            window.location.href = window.location.href;
                    }
                    else {
                        alert(response.d);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Delete');
                }
            });

        }

    }

    function Create_Folder(p_obj) {
        $.ajax({
            type: "POST",
            url: "/Login.aspx/Create_Folder",
            data: "{p_name:'" + p_obj.value + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d.indexOf('Err') == -1) {
                    window.location.href = window.location.href;
                }
                else {
                    alert(response.d);
                    window.location.href = window.location.href;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('Create');
            }
        });

    }

    function Open_Folder(p_path, p_is_folder) {
        if (p_is_folder == '1') {
            $.ajax({
                type: "POST",
                url: "/Login.aspx/Open_Folder",
                data: "{p_path:'" + p_path.toString() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.indexOf('Err') == -1) {
                        window.location.href = window.location.href;
                    }
                    else {
                        alert(response.d);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Err:Open');
                }
            });

        }

    }

</script>
