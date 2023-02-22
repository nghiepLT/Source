<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Accepwork.aspx.cs" Inherits="WebCus.Accepwork" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%--<script type="text/javascript">
    window.location.href = "/RenderModule.aspx?smid=NewsMng&md=NewsControl.ascx&muid=CategoryInfo01&UK=About&divnoidung=1";
    </script>--%>
   <%-- <script type="text/javascript">
        window.location.href = "/listcase";
    </script>--%>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
        
    <script type="text/javascript">
        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: ">>>Notice<<<",
                    buttons: {
                        Close: function () {
                            $(this).dialog('Ok Fine!');
                            window.location.href = "/listcase";
                        }
                    },
                    modal: true
                });
            });
        };

    </script>
    <div id="dialog" style="display: none">
    </div>
    
 
</asp:Content>
