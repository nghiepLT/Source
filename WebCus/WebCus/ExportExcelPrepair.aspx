<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" ValidateRequest="false"  CodeBehind="ExportExcelPrepair.aspx.cs"
    Inherits="MainProject.ExportExcelPrepair" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divData" runat="Server"></div>
    <asp:HiddenField ID="hdnData" runat="server" />
    <div style="display:none;">
    <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
    </div>
    <script type="text/javascript">
        window.onload = function () {
            TableName_DataExport = getCookie('TableName_DataExport');
            obj = window.opener.document.getElementById(TableName_DataExport)
            var hdnData = document.getElementById('<%= hdnData.ClientID%>');
            hdnData.value = obj.innerHTML;

            var btnExport = document.getElementById('<%=btnExport.ClientID %>');
            btnExport.click();

        }

        function OpenExportEnd() {
            var sOption = "toolbar=yes,location=no,directories=yes,menubar=yes,";
            sOption += "scrollbars=yes,width=150,height=100,left=100,top=25";
            window.open("ExportExcel.aspx", "_blank", sOption);
            window.close();
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

//        alert(getCookie('ExportData'));

    </script>
    </form>
</body>
</html>
