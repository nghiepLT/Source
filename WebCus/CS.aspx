<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CS.aspx.cs" Inherits="CS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='http://jquery-star-rating-plugin.googlecode.com/svn/trunk/jquery.rating.js'
        type="text/javascript"></script>
    <link rel="Stylesheet" href="http://jquery-star-rating-plugin.googlecode.com/svn/trunk/jquery.rating.css" />
    <script type="text/javascript">
        $(function () {
            GetRatings();
        });
        function GetRatings() {
            $.ajax({
                type: "POST",
                url: "CS.aspx/GetRating",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = eval(response.d)[0];
                    if (result.Average > 0) {
                        $('.rating_star').eq(result.Average - 1).attr("checked", "checked");
                        $("#rating").html("Average Rating: " + result.Average + " Total Rating:" + result.Total);
                    }
                    ApplyPlugin();
                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
        }
        function ApplyPlugin() {
            $('.rating_star').rating({
                callback: function (value, link) {
                    $.ajax({
                        type: "POST",
                        url: "CS.aspx/Rate",
                        data: "{rating: " + value + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            GetRatings();
                            alert("Your rating has been saved.");
                        },
                        failure: function (response) {
                            alert('There was an error.');
                        }
                    });
                }
            });
        }
    </script>
    <div>
        <input name="rating_star" type="radio" class="rating_star" value="1" />
        <input name="rating_star" type="radio" class="rating_star" value="2" />
        <input name="rating_star" type="radio" class="rating_star" value="3" />
        <input name="rating_star" type="radio" class="rating_star" value="4" />
        <input name="rating_star" type="radio" class="rating_star" value="5" />
    </div>
    <br />
    <hr />
    <span id="rating"></span>
    </form>
</body>
</html>
