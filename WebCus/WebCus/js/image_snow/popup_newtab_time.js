/*
+-------------------------------------------------------+
| Code Popup mở một tab mới với thời gian tùy chỉnh		|
| Coder: Endy Hoàng			     						|
| Website: itexpress.vn, itwebs.vn, itexpressvn.com     |
| Email: info@itexpressvn.com, skype: hoan.it           |
+-------------------------------------------------------+
*/
function addEvent(obj, eventName, func)
{
    if (obj.attachEvent)
    {
    obj.attachEvent("on" + eventName, func);
    }
    else if(obj.addEventListener)
    {
    obj.addEventListener(eventName, func, true);
    }
    else
    {
    obj["on" + eventName] = func;
    }
}
function Get_Cookie( name ) {
	
var start = document.cookie.indexOf( name + "=" );
var len = start + name.length + 1;
if ( ( !start ) &&
( name != document.cookie.substring( 0, name.length ) ) )
{
return null;
}
if ( start == -1 ) return null;
var end = document.cookie.indexOf( ";", len );
if ( end == -1 ) end = document.cookie.length;
return unescape( document.cookie.substring( len, end ) );
}
addEvent(window, "load", function(e)
{
	addEvent(document, "click", function(e)
	{
	  	
	   var height = screen.availHeight ;
		   var width = screen.availWidth;
				width= width-150;
				height= height-100;
		   var params = "left=10,top=10,location=1,toolbar=1,status=1,menubar=1,scrollbars=1,resizable=1,width="+width+", height="+height+"";
	   // -------------- pop 1 --------------------	
	   if(Get_Cookie("itexpressvn") != 1)
	   {	
			var w = window.open("https://itexpress.vn",'itexpressvn', params);
			if (w)
			{
				var today = new Date();
				var expires = 360000*2*1;//var expires = 3600000*24*1 = 1 ngày bạn (expires=1000 tương ứng với 1 giây);
				var expires_date = new Date(today.getTime() + (expires));
				document.cookie = "itexpressvn=1;expires=" + expires_date.toGMTString();
				w.blur();
				window.focus();
			}
		}
	    // -------------- end pop 1 --------------------
		// -------------- pop 2 --------------------	
	    //if (Get_Cookie("softfreevn") != 1) {
      //    var w = window.open("http://softfreevn.com", 'softfreevn', params);
      //    if (w) {
      //        var today = new Date();
      //        var expires = 360000 * 2 * 1;//var expires = 3600000*24*1 = 1 ngày bạn (expires=1000 tương ứng với 1 giây);
      //        var expires_date = new Date(today.getTime() + (expires));
      //        document.cookie = "softfreevn=1;expires=" + expires_date.toGMTString();
      //        w.blur();
      //        window.focus();
      //    }
      //}
	    // -------------- end pop 2 --------------------
		//Bạn có thể copy code trong khoảng "---pop---" và "--- end Pop---" tạo ra nhiều khối để mở cùng lúc nhiều trang khác nhau, và tất nhiên
		//bạn cũng có thể tùy chỉnh thời gian cho chúng
	});
});