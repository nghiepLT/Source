﻿<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="UserMapLink.ascx.cs"
    Inherits="UserMng.UserMapLink" %>
<%@ Register Src="TextBoxMultiLanguage.ascx" TagName="TextBoxMultiLanguage" TagPrefix="pqt" %>
<div class="page-title">
    <h2 class="icon-title">
        <span>Quản lý phân quyền    </span>
    </h2> 
</div>
<asp:HiddenField ID="hdnCategoryIDs" Value="" runat="server" />
<script language="javascript" type="text/javascript">
    function OnTreeClick(evt) {
        var src = window.event != window.undefined ? window.event.srcElement : evt.target;
        var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
        if (isChkBoxClick) {
            var parentTable = GetParentByTagName("table", src);
            var nxtSibling = parentTable.nextSibling;
            if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
            {
                if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                {
                    //check or uncheck children at all levels
                    CheckUncheckChildren(parentTable.nextSibling, src.checked);
                }
            }
            //check or uncheck parents at all levels
            CheckUncheckParents(src, src.checked);
        }
    }

    function CheckUncheckChildren(childContainer, check) {
        var childChkBoxes = childContainer.getElementsByTagName("input");
        var childChkBoxCount = childChkBoxes.length;
        for (var i = 0; i < childChkBoxCount; i++) {
            childChkBoxes[i].checked = check;
        }
    }

    function CheckUncheckParents(srcChild, check) {
        var parentDiv = GetParentByTagName("div", srcChild);
        var parentNodeTable = parentDiv.previousSibling;

        if (parentNodeTable) {
            var checkUncheckSwitch;

            if (check) //checkbox checked
            {
                var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                if (isAllSiblingsChecked)
                    checkUncheckSwitch = true;
                else
                    return; //do not need to check parent if any(one or more) child not checked
            }
            else //checkbox unchecked
            {
                checkUncheckSwitch = false;
            }

            var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
            if (inpElemsInParentTable.length > 0) {
                var parentNodeChkBox = inpElemsInParentTable[0];
                parentNodeChkBox.checked = checkUncheckSwitch;
                //do the same recursively
                CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
            }
        }
    }

    function AreAllSiblingsChecked(chkBox) {
        var parentDiv = GetParentByTagName("div", chkBox);
        var childCount = parentDiv.childNodes.length;
        for (var i = 0; i < childCount; i++) {
            if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
            {
                if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                    var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                    //if any of sibling nodes are not checked, return false
                    if (!prevChkBox.checked) {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    //utility function to get the container of an element by tagname
    function GetParentByTagName(parentTagName, childElementObj) {
        var parent = childElementObj.parentNode;
        while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
            parent = parent.parentNode;
        }
        return parent;
    }

</script>

<div class="TboardBox">
<%--<div id="div_mng" runat="server">
    <div class="Line2" style="margin-top: 5px;">
    </div>
    <asp:Button ID="Button1" runat="server" Text="Lưu" CssClass="btn-1" OnClick="btnSave_Click"/>
    <div class="Line2" style="margin-top: 5px;">
    </div>
    <div class="clearfix" style="margin-left:10px;padding-bottom:20px;">
        
    </div>
    <div class="Line2">
    </div>
</div>--%>
    <!-- -->
    <div class="Line2" style="margin-top: 5px;">
    </div>
    <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn-1" OnClick="btnSave_Click"/>
    <div class="Line2" style="margin-top: 5px;">
    </div>
    <div class="clearfix" style="margin-left:10px;padding-bottom:20px;">
        <asp:TreeView ID="treeMenuAdmin" EnableClientScript="true" AfterClientCheck="CheckChildNodes();" 
         onclick="OnTreeClick(event)"
        runat="server" ShowLines="true" ShowCheckBoxes="All"
        ExpandDepth="0" PopulateNodesFromClient="true" ShowExpandCollapse="true">
            <NodeStyle CssClass="pdL5" />
        </asp:TreeView>
    </div>
    <div class="Line2">
    </div>
</div>
