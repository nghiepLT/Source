function Submit_Button(e, idBtn) {
    var keynum;
    if (window.event) // IE
        keynum = e.keyCode;
    else if (e.which) // Netscape/Firefox/Opera
        keynum = e.which;
    if (keynum == 13) {
        var obj = document.getElementById(idBtn);
        window.location.href = obj.href;
        return false;
    }

}

function Check_Enter(e) {
    var keynum;
    if (window.event) // IE
        keynum = e.keyCode;
    else if (e.which) // Netscape/Firefox/Opera
        keynum = e.which;
    if (keynum == 13) {
        return true;
    }
    return false;

}

function addto_facebook(strUrl, strTitle) {
    window.open('http://www.facebook.com/share.php?u=' + strUrl + '&t=' + strTitle, '', 'location=1,status=1,scrollbars=1,width=700,height=400');
}

function addto_linkhay(strUrl) {
    window.open('http://linkhay.com/submit?link_url=' + strUrl);
}
function addto_twitter(strUrl) {
    window.open('http://twitter.com/home?status=' + strUrl);
}
function addto_google(strUrl, strTitle) {
            window.open('http://www.google.com/bookmarks/mark?op=edit&bkmk=' + strUrl + '&title=' + strTitle + '&annotation=' + strTitle);
        }
function addto_buzz(strUrl) {
    window.open('http://buzz.yahoo.com/buzz?publisherurn=VOUCHER.VN&targetUrl=' + strUrl);
}
function addto_zing(strUrl, strTitle, strImage) {
    window.open('http://link.apps.zing.vn/share?u=' + strUrl + '&images=' + strImage + '&t=' + strTitle + '&desc=' + strTitle + '&media=&width=0&height=0');
}
function addto_linkedin(strUrl, strTitle) {
    window.open('http://www.linkedin.com/shareArticle?mini=true&url=' + strUrl + '&title=' + strTitle + '&ro=false&summary=&source=VOUCHER.VN');
} 

function ExporttoExcel_withChart(p_id, p_excelName, p_img, p_width, p_height) {
    var objData = document.getElementById(p_id);
    var form = document.createElement("form");
    form.setAttribute("method", "post");
    form.setAttribute("action", "ExportExcelCSharp.aspx");
    form.setAttribute("target", "formresult");

    var hiddenField = document.createElement("input");
    hiddenField.setAttribute("name", "ExportData");
    hiddenField.setAttribute("value", objData.innerHTML);
    form.appendChild(hiddenField);

    var hiddenField_Name = document.createElement("input");
    hiddenField_Name.setAttribute("name", "ReportName");
    hiddenField_Name.setAttribute("value", p_excelName);
    form.appendChild(hiddenField_Name);

    //image
    var hiddenField_Image = document.createElement("input");
    hiddenField_Image.setAttribute("name", "strFileimage");
    hiddenField_Image.setAttribute("value", p_img);
    form.appendChild(hiddenField_Image);

    //alert(p_height); 
    //alert(p_width);
    //width
    var hiddenField_width = document.createElement("input");
    hiddenField_width.setAttribute("name", "width");
    hiddenField_width.setAttribute("value", p_width);
    form.appendChild(hiddenField_width);

    //height
    var hiddenField_height = document.createElement("input");
    hiddenField_height.setAttribute("name", "height");
    hiddenField_height.setAttribute("value", p_height);
    form.appendChild(hiddenField_height);

    document.body.appendChild(form);
    // creating the 'formresult' window with custom features prior to submitting the form 

    var sOption = "toolbar=yes,location=no,directories=yes,menubar=yes,";
    sOption += "scrollbars=yes,width=100,height=100,left=100,top=25";
    window.open("ExportExcelCSharp.aspx", "", sOption);
    form.submit();
    return false;
}

function ExporttoExcel(p_id, p_excelName) {
    var objData = document.getElementById(p_id);

    var form = document.createElement("form");
    form.setAttribute("method", "post");
    form.setAttribute("action", "ExportExcel.aspx");
    form.setAttribute("target", "formresult");

    var hiddenField = document.createElement("input");
    hiddenField.setAttribute("name", "ExportData");
    hiddenField.setAttribute("value", objData.innerHTML);
    form.appendChild(hiddenField);

    var hiddenField_Name = document.createElement("input");
    hiddenField_Name.setAttribute("name", "ReportName");
    hiddenField_Name.setAttribute("value", p_excelName);
    form.appendChild(hiddenField_Name);

    document.body.appendChild(form);
    // creating the 'formresult' window with custom features prior to submitting the form 
    
    var sOption = "toolbar=yes,location=no,directories=yes,menubar=yes,";
    sOption += "scrollbars=yes,width=100,height=100,left=100,top=25";
    window.open("ExportExcel.aspx", "", sOption);
    form.submit();
    return false;
}

function ExporttoExcel_withTitle(p_id, p_idTitle, p_excelName) {

    var objData = document.getElementById(p_id);
    var objDataTitle = document.getElementById(p_idTitle);    

    var form = document.createElement("form");
    form.setAttribute("method", "post");
    form.setAttribute("action", "ExportExcel.aspx");
    form.setAttribute("target", "formresult");

    var hiddenField = document.createElement("input");
    hiddenField.setAttribute("name", "ExportData");
    hiddenField.setAttribute("value", objData.innerHTML);
    form.appendChild(hiddenField);

    var hiddenField_Name = document.createElement("input");
    hiddenField_Name.setAttribute("name", "ReportName");
    hiddenField_Name.setAttribute("value", p_excelName);
    form.appendChild(hiddenField_Name);

    var hiddenField_Title = document.createElement("input");
    hiddenField_Title.setAttribute("name", "ExportDataTitle");
    hiddenField_Title.setAttribute("value", objDataTitle.innerHTML);
    form.appendChild(hiddenField_Title);

    document.body.appendChild(form);
    // creating the 'formresult' window with custom features prior to submitting the form 

    var sOption = "toolbar=yes,location=no,directories=yes,menubar=yes,";
    sOption += "scrollbars=yes,width=100,height=100,left=100,top=25";
    window.open("ExportExcel.aspx", "", sOption);
    form.submit();
    return false;
}

function ExporttoExcel_withImage(p_id, p_excelName,p_img) {
    var objData = document.getElementById(p_id);

    var form = document.createElement("form");
    form.setAttribute("method", "post");
    form.setAttribute("action", "ExportExcel_withImage.aspx");
    form.setAttribute("target", "formresult");

    var hiddenField = document.createElement("input");
    hiddenField.setAttribute("name", "ExportData");
    hiddenField.setAttribute("value", objData.innerHTML);
    form.appendChild(hiddenField);

    var hiddenField_Name = document.createElement("input");
    hiddenField_Name.setAttribute("name", "ReportName");
    hiddenField_Name.setAttribute("value", p_excelName);
    form.appendChild(hiddenField_Name);

    //image
    var hiddenField_Image = document.createElement("input");
    hiddenField_Image.setAttribute("name", "strFileimage");
    hiddenField_Image.setAttribute("value", p_img);
    form.appendChild(hiddenField_Image);

    document.body.appendChild(form);
    // creating the 'formresult' window with custom features prior to submitting the form 

    var sOption = "toolbar=yes,location=no,directories=yes,menubar=yes,";
    sOption += "scrollbars=yes,width=100,height=100,left=100,top=25";
    window.open("ExportExcel_withImage.aspx", "", sOption);
    form.submit();
    return false;
}

function ExporttoExcel1(p_id) {
    var sOption = "toolbar=yes,location=no,directories=yes,menubar=yes,";
    sOption += "scrollbars=yes,width=100,height=100,left=100,top=25";

    window.open("WebForm3.aspx", "", sOption);
    return;
    var sWinHTML = document.getElementById(p_id).innerHTML;
    var winprint = window.open("ExportExcelPrepair.aspx", "", sOption);
    winprint.document.open();
    winprint.document.write('<html><head>')

    winprint.document.write('<meta http-equiv="Content-Type" content="application/vnd.ms-excel">')
    winprint.document.write('<meta http-equiv="Content-disposition": attachment; filename="file.xls">')
    winprint.document.write('</head><body  onload="self.close();">')

    winprint.document.write(sWinHTML);
    winprint.document.write('</body></html>');
    winprint.document.close();
    winprint.focus();
}

//function PrintReport(p_id) {
//    var objData = document.getElementById(p_id);
//    var objTitle = document.getElementById('printTitle');
//    var objHeader = document.getElementById('printHeader');

//    var form = document.createElement("form");
//    form.setAttribute("method", "post");
//    form.setAttribute("action", "PrintReport.aspx");
//    form.setAttribute("target", "formresult");

//    var hiddenField = document.createElement("input");
//    hiddenField.setAttribute("name", "ExportData");
//    hiddenField.setAttribute("value", objData.innerHTML);
//    form.appendChild(hiddenField);

//    if (objTitle != null) {
//        var hiddenField = document.createElement("input");
//        hiddenField.setAttribute("name", "TitleReport");
//        hiddenField.setAttribute("value", objTitle.innerHTML);
//        form.appendChild(hiddenField);
//    }

//    if (objHeader != null) {
//        var hiddenField = document.createElement("input");
//        hiddenField.setAttribute("name", "HeaderReport");
//        hiddenField.setAttribute("value", printHeader.innerHTML);
//        form.appendChild(hiddenField);
//    }

//    

////    var hiddenField_Name = document.createElement("input");
////    hiddenField_Name.setAttribute("name", "ReportName");
////    hiddenField_Name.setAttribute("value", p_excelName);
////    form.appendChild(hiddenField_Name);

//    document.body.appendChild(form);
//    // creating the 'formresult' window with custom features prior to submitting the form 

//    var sOption = "toolbar=yes,location=no,directories=yes,menubar=yes,";
//    sOption += "scrollbars=yes,width=100,height=100,left=100,top=25";
//    window.open("PrintReport.aspx", "", sOption);
//    form.submit();
//    return false;
//}

function PrintPreview(p_idData) {
    var sOption = "toolbar=no,location=no,directories=no,menubar=no,";
    sOption += "scrollbars=yes,width=1000,height=600,left=100,top=25";
    window.open("PrintReport.aspx?id=" + p_idData, "", sOption);
    
    return false;
}

function PrintPreview_withTitle(p_idData, lbTitle) {
    var sOption = "toolbar=no,location=no,directories=no,menubar=no,";
    sOption += "scrollbars=yes,width=1000,height=600,left=100,top=25";
    window.open("PrintReport_withTitle.aspx?id=" + p_idData + "&title=" + lbTitle, "", sOption);

    return false;
}

function encode(p_value) {
    return encodeURIComponent(p_value);
}

function decode(p_value) {
    return decodeURIComponent(p_value.replace(/\+/g, " "));
}


function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}

function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}

function writeCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    } else {
        var expires = "";
    }

    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function numberFormat(number, decimals, dec_point, thousands_sep) {
    var n = number, prec = decimals;
    n = !isFinite(+n) ? 0 : +n;
    prec = !isFinite(+prec) ? 0 : Math.abs(prec);
    var sep = (typeof thousands_sep == "undefined") ? '.' : thousands_sep;
    var dec = (typeof dec_point == "undefined") ? ',' : dec_point;
    var s = (prec > 0) ? n.toFixed(prec) : Math.round(n).toFixed(prec); //fix for IE parseFloat(0.55).toFixed(0) = 0;
    var abs = Math.abs(n).toFixed(prec);
    var _, i;
    if (abs >= 1000) {
        _ = abs.split(/\D/);
        i = _[0].length % 3 || 3;
        _[0] = s.slice(0, i + (n < 0)) +
				  _[0].slice(i).replace(/(\d{3})/g, sep + '$1');
        s = _.join(dec);
    } else {
        s = s.replace(',', dec);
    }
    return s;
}

function string_to_slug(str) {
    str = str.replace(/^\s+|\s+$/g, ''); // trim
    str = str.toLowerCase();

    // remove accents, swap ñ for n, etc
    var from = "àáäâèéëêìíïîòóöôùúüûñç·/_,:;";
    var to = "aaaaeeeeiiiioooouuuunc------";
    for (var i = 0, l = from.length ; i < l ; i++) {
        str = str.replace(new RegExp(from.charAt(i), 'g'), to.charAt(i));
    }

    str = str.replace(/[^a-z0-9 -]/g, '') // remove invalid chars
      .replace(/\s+/g, '-') // collapse whitespace and replace by -
      .replace(/-+/g, '-'); // collapse dashes

    return str;
}