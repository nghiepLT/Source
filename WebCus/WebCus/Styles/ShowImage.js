var activeArea = null;
var winW = 0;
var winH = 0;
var clickX = 0;
var clickY = 0;
function GetWindowSize() {
    if (navigator.appName.indexOf("Microsoft") != -1) {
        winH = document.body.clientHeight;
        winW = document.body.clientWidth;
    } else {
        winH = window.innerHeight;
        winW = window.innerWidth;
    }
}
function GetMouseXY(e) {
    if (navigator.appName.indexOf("Microsoft") != -1) { // grab the x-y pos.s if browser is IE
        clickX = e.clientX + document.body.scrollLeft
        clickY = e.clientY + document.body.scrollTop
    } else {  // grab the x-y pos.s if browser is NS
        clickX = e.pageX
        clickY = e.pageY
    }
    // catch possible negative values in NS4
    if (clickX < 0) clickX = 0;
    if (clickY < 0) clickY = 0;
    return true
}

function AreaContains(parent, child) {
    while (child)
        if (parent == child) return true;
    else
        child = child.parentNode;

    return false;
}
function PopupAreaMouseOut(e) {
    if (!e)
        var e = window.event;
    var targ = e.relatedTarget ? e.relatedTarget : e.toElement;
    if (activeArea != null && !AreaContains(activeArea, targ)) {
        activeArea.style.display = 'none';
        activeArea = null;
    }
}

function PopupArea(e, idObj) {
    // get viewing size 
    GetWindowSize();
    GetMouseXY(e);

    // hide the old area
    if (activeArea != null) {
        activeArea.style.display = 'none';
    }

    // pop-up area
    var popupArea = document.getElementById(idObj);

    var Wwin = document.body.clientWidth;

    var a = $('#' + idObj).width() + clickX;
    var b = (a - Wwin - 10);
    popupArea.style.position = 'absolute';
    popupArea.style.display = 'inline';
    popupArea.style.zIndex = "9999";
    popupArea.style.top = clickY - 78 + 'px';

    if (b > 0) {
        //popupArea.style.left = clickX - b - 10 + 'px';
        popupArea.style.left = clickX - $('#' + idObj).width() - 10 + 'px';
    }
    else {
        popupArea.style.left = clickX + 10 + 'px';
    }
    
    //$('#' + idObj).css('right', e);
//    popupArea.style.left = clickX + 10 + 'px';

    popupArea.onmouseout = PopupAreaMouseOut;
    document.body.appendChild(popupArea);

    // keep the pop-up area
    activeArea = popupArea;
}