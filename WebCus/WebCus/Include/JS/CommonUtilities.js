//=======================================================================
// Name     : FileUploadManager
// Author   : TienPQ
// Date     : 2007/11/05
//=======================================================================
function FileUploadManager(objID, codeBase, width, height, classID) {
    if (width == "") width = 400;
    if (height == "") height = 300;

    document.write('<table style="width:' + width + '"><tr><td align="right">');

    document.write('<input type="button" onclick="return ' + objID + '_AddFile();" value="Add Files" />');
    document.write('<input type="button" onclick="return ' + objID + '_AddFolder();" value="Add Folder" />');
    document.write('<input type="button" onclick="return ' + objID + '_RemovewFile();" value="RemoveFile" />');
    document.write('<input type="button" onclick="return ' + objID + '_RemoveAll();" value="RemoveAll" />');
    
    document.write('</td></tr><tr><td>');

    document.write("<OBJECT id='" + objID + "' codeBase='" + codeBase + "' width='" + width + "' height='" + height + "' classid='" + classID + "'>");
    document.write("<param name='UploadURL' value='http://vnshare.vn/FileUpload/DirectoryUpload.aspx'>");
    document.write("<param name='SerialKey' value=''>");
    document.write("</OBJECT>");

    document.write('</td></tr></table>');

    document.write('<script type="text/javascript">');
    
    document.write('function ' + objID + '_AddFile() {');
    document.write("var obj = document.getElementById('" + objID + "');");
    document.write(' if (obj != null) {obj.ShowAddFilesDialog();}}');

    document.write('function ' + objID + '_AddFolder() {');
    document.write("var obj = document.getElementById('" + objID + "');");
    document.write(' if (obj != null) {obj.ShowAddFolderDialog();}}');

    document.write('function ' + objID + '_RemovewFile() {');
    document.write("var obj = document.getElementById('" + objID + "');");
    document.write(' if (obj != null) {obj.RemoveSelectedFiles();}}');


    document.write('function ' + objID + '_RemoveAll() {');
    document.write("var obj = document.getElementById('" + objID + "');");
    document.write(' if (obj != null) {obj.RemoveAllFiles();}}');

    document.write('</script>');
}

function AddParamPowUpload(p_objID, p_isUploadStruct, p_saveFilePath) {

//    window.open('/FileUpload/FileUploadMonitor.aspx?FileUploaderID=' + p_objID);
    
    var powUpload = document.getElementById(p_objID)
    powUpload.RemoveAllFormItems()
    for (i = 0; i < powUpload.FileCount; i++) {
        var file = powUpload.GetItem(i);
        var filename = file.FullName
        powUpload.AddFormItem("CreationTime_" + i, file.CreationTime);
        powUpload.AddFormItem("LastAccessTime_" + i, file.LastAccessTime);
        powUpload.AddFormItem("LastWriteTime_" + i, file.LastWriteTime);
        powUpload.AddFormItem("SelectedPath_" + i, file.SelectedPath);
    }
    powUpload.AddFormItem("UploadStructure", p_isUploadStruct);
    powUpload.AddFormItem("SaveFilePath", p_saveFilePath);
}

function FileUploadMonitor(objID, codeBase, width, height, classID) {
    document.write("<OBJECT id='" + objID + "' codeBase='" + codeBase + "' width='" + width + "' height='" + height + "' classid='" + classID + "' VIEWASTEXT></OBJECT>");
}



function getX() {
    if (document.all) return (window.document.body.clientWidth + 10)
    else if (document.getElementById) return window.top.outerWidth;
    else return window.top.outerWidth - 12;
}

function getY() {
    if (document.all) return (window.top.document.body.clientHeight + 29)
    else if (document.getElementById) return window.top.outerHeight;
    else return window.top.outerHeight - 31;
}


function getWidth(idObj) {
    var obj = document.getElementById(idObj);
    return obj != null ? obj.offsetWidth : 100;
}

function getHeight(idObj) {
    var obj = document.getElementById(idObj);
    return obj != null ? obj.offsetHeight : 100;
}

function CenterWindow() {
    var width = window.screen.availWidth;
    var height = window.screen.availHeight;
    var x = window.getX();
    var y = window.getY();

    window.moveTo((width - x) / 2, (height - y) / 2);

    window.focus();
}

function WindowOpen(url, title, width, height, scrollbars) {
    var screenWidth = window.screen.availWidth;
    var screenHeight = window.screen.availHeight;

    var top = (screenHeight - height - 20) / 2;
    var left = (screenWidth - width) / 2;

    title = window.open(url, title, 'top = ' + top + ', left = ' + left + ',width=' + width + ', height=' + height + ', resizable=no, status=yes, toolbar=no, menubar=no, scrollbars=' + scrollbars);
    title.focus();
}

function WindowShowModalDialog(url, title, width, height) {
    var screenWidth = window.screen.availWidth;
    var screenHeight = window.screen.availHeight;

    var top = (screenHeight - height - 20) / 2;
    var left = (screenWidth - width) / 2;

    window.showModalDialog(url, title, 'dialogWidth=' + width + 'px;dialogHeight=' + height + 'px;center:Yes;help:No;resizable:No;status:No;');
}


function WindowOpenResize(p_url, p_title, p_top, p_left, p_width, p_height, p_resize, p_scrollbars) {
    if (p_top == '') {
        var screenHeight = window.screen.availHeight;
        var top = (screenHeight - height - 20) / 2;
    }
    else
        top = p_top;

    if (p_left == '') {
        var screenWidth = window.screen.availWidth;
        var left = (screenWidth - width) / 2;
    }
    else
        left = p_left;

    title = window.open(p_url, p_title, 'top = ' + top + ', left = ' + left + ',width=' + p_width + ', height=' + p_height + ', resizable=' + p_resize + ', status=yes, toolbar=no, menubar=no, scrollbars=' + p_scrollbars);
    title.focus();
}

function WindowResize(width, height) {
    var screenWidth = window.screen.availWidth;
    var screenHeight = window.screen.availHeight;

    var top = (screenHeight - height - 20) / 2;
    var left = (screenWidth - width) / 2;

    window.resizeTo(width, height);
    window.moveTo(left, top);
    window.focus();
}

function PageMoveValid(p_movePageControl, p_totalPage) {
    if (p_movePageControl.value.length == 0)
        return false;

    if ((p_movePageControl.value > p_totalPage) ||
		p_movePageControl.value == 0 ||
		!p_movePageControl.value.trim().isnumber())
        return false;

    return true;
}

function SetFocusOnEnter(e, btn) {
    var keynum;
    if (window.event) // IE
        keynum = e.keyCode;
    else if (e.which) // Netscape/Firefox/Opera
        keynum = e.which;
    if (keynum == 13) {
        document.getElementById(btn).click();
        return false;
    }
}


function addLoadEvent(func) {
    var oldonload = window.onload;
    if (typeof window.onload != 'function') {
        window.onload = func;
    } else {
        window.onload = function() {
            if (oldonload) {
                oldonload();
            }
            func();
        }
    }
}

function flashObject(file, width, height) {
    document.write("<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0' width='" + width + "' height='" + height + "'>");
    document.write("<param name=movie value='" + file + "'>");
    document.write("<param name=quality value=high>");
    document.write("<param name='wmode' value='Transparent' />");
    document.write("<embed src='" + file + "' quality=high pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash' wmode='Transparent' type='application/x-shockwave-flash' width='" + width + "' height='" + height + "'>");
    document.write("</embed></object>");
}

 function LibSearch(scon) {
            var AdMyDomain = 'giacongcokhi.com.vn';
            if (scon != '') {
                var Location = 'SearchResult.aspx';
                Location += '?domains=' + AdMyDomain;
                Location += '&q=' + scon;
                Location += '&sitesearch=' + AdMyDomain;
                Location += '&sa=Google+Search';
                Location += '&client=pub-0950933637091935';
                Location += '&forid=sbi';
                Location += '&ie=UTF-8';
                Location += '&oe=UTF-8';
                Location += '&cof=GALT%3A%23008000%3BGL%3A1%3BDIV%3A%23336699%3BVLC%3A663399%3BAH%3Acenter%3BBGC%3AFFFFFF%3BLBGC%3A336699%3BALC%3A0000FF%3BLC%3A0000FF%3BT%3A000000%3BGFNT%3A0000FF%3BGIMP%3A0000FF%3BFORID%3A11';
                Location += '&hl=vi';
                window.location = Location;
            }
        }


        function SetFocus(e) {
            var keynum;
            if (window.event) // IE
                keynum = e.keyCode;
            else if (e.which) // Netscape/Firefox/Opera
                keynum = e.which;

            if (keynum == 13) {
                document.getElementById("btnSearch").click();
                return false;
            }
        }

        function SetSelectedStyleMenu(id) {
            var menu = document.getElementById(id);
            if (id == 'menuHome')
                menu.className = "first Selected";
            else
                menu.className = "selected";
        }

        function NavFirstPage(currentPage) {
            document.getElementById('navCurrentPage').innerText = currentPage;

        }

 function correctPNG() // correctly handle PNG transparency in Win IE 5.5 & 6.
        {
            var arVersion = navigator.appVersion.split("MSIE")
            var version = parseFloat(arVersion[1])
            if ((version >= 5.5) && (document.body.filters)) {
                for (var i = 0; i < document.images.length; i++) {
                    var img = document.images[i]
                    var imgName = img.src.toUpperCase()
                    if (imgName.substring(imgName.length - 3, imgName.length) == "PNG") {
                        var imgID = (img.id) ? "id='" + img.id + "' " : ""
                        var imgClass = (img.className) ? "class='" + img.className + "' " : ""
                        var imgTitle = (img.title) ? "title='" + img.title + "' " : "title='" + img.alt + "' "
                        var imgStyle = "display:inline-block;" + img.style.cssText
                        if (img.align == "left") imgStyle = "float:left;" + imgStyle
                        if (img.align == "right") imgStyle = "float:right;" + imgStyle
                        if (img.parentElement.href) imgStyle = "cursor:hand;" + imgStyle
                        var strNewHTML = "<span " + imgID + imgClass + imgTitle
            + " style=\"" + "width:" + img.width + "px; height:" + img.height + "px;" + imgStyle + ";"
            + "filter:progid:DXImageTransform.Microsoft.AlphaImageLoader"
            + "(src=\'" + img.src + "\', sizingMethod='scale');\"></span>"
                        img.outerHTML = strNewHTML
                        i = i - 1
                    }
                }
            }
        }
        
function OpenWebLink(obj) {
        if(obj.value != '0')
            window.open(obj.value)
}

function readCookie(p_name) {
    var nameEQ = p_name + "=";
    var ca = document.cookie.split(';');
    for(var i=0;i < ca.length;i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') c = c.substring(1,c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
    }
    return null;
}    

     
function createCookie(name,value,days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime()+(days*24*60*60*1000));
        var expires = "; expires="+date.toGMTString();
    }
    else var expires = "";
    document.cookie = name+"="+value+expires+"; path=/";
}
    
function getObj(id)    
{
    var obj = document.getElementById(id);
    return obj;
}
            
            