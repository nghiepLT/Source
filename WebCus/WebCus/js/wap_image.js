function MM_findObj(n, d) { //v4.01

  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {

    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}

  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];

  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);

  if(!x && d.getElementById) x=d.getElementById(n); return x;

}

function MM_swapImage() { //v3.0

  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)

   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}

}

function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}


//--------------------------------------------------------------

var ppimgNW;
function popupImage(src, note, title, css, border) {
  if (border==null) border = 0;
  if (note==null) note = '';
  if (ppimgNW != null) ppimgNW.close();

  ppimgNW = window.open('','POPUPIMAGE','width=1,height=1');
  var doc = ppimgNW.document;
  doc.write('<html>');
  doc.write('<head>');

  if (title!=null) doc.write('<title>'+ title +'</title>');
  doc.write('<style> body {'+css+'} #ppImgText{'+ css +'} #ppImg{cursor:hand}</style></head>');
  doc.write('<body leftmargin="0" topmargin="' + border + '" onload="doResize();">');
  doc.write('<div align="center">');
  doc.write('<img src="' + src + '" id="ppImg" onclick="self.close();" title="Close">');
  doc.write('</div>');
  doc.write('<div style="height:1; width:' + border + '; font-size:4pt;">');
  doc.write('</div>');
  doc.write('<div id="ppImgText" align="center">');
  doc.write(note);
  doc.write('</div>');
  doc.write('</body>');
  doc.write('</html>');

  doc.write('<' + 'script>');
  doc.write('function doResize() {');
  doc.write('  var imgW = ppImg.width, imgH = ppImg.height;');     
  doc.write('  window.resizeTo(imgW + 8 +' + border*2 +', imgH + ppImgText.offsetHeight + 26 + '+ border*2 +');');

  doc.write('}');
  doc.write('doResize(); ');
  doc.write('</' + 'script>');


}