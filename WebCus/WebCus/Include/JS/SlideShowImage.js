var aImages = null;

var aSize = new Array(270, 177)
var iDisplay = 3000
var oTimer = null
var iCurrent = 0
var sSource = ""

function doDisplay() {
    clearTimeout(oTimer)
    if (sSource != "") {
        if (document.images.slideShow.filters) {
            document.images.slideShow.filters[0].Stop()
            document.images.slideShow.filters[0].Apply()
            document.images.slideShow.filters.revealTrans.transition = 23
        }
        document.images.slideShow.src = sSource
        if (document.images.slideShow.filters) {
            document.images.slideShow.filters[0].Play()
            document.images.slideShow.filters.revealTrans.transition = 23
        }
    }
}
function doReadyImage() {
    sSource = this.src
    if (oTimer == null) doDisplay()
}
function doErrorDisplay() {
    clearTimeout(oTimer)
    doLoad()
}
function doLoad() {
    clearTimeout(oTimer)
    var img = new Image()
    img.onload = doReadyImage
    img.onerror = doErrorDisplay
    sSource = ""
    iCurrent++
    if (iCurrent == aImages.length) iCurrent = 0
    oTimer = setTimeout("oTimer=null;doDisplay()", iDisplay)
    img.src = aImages[iCurrent]
}
function quickNextLoad() {
    clearTimeout(oTimer)
    oTimer = null
    var img = new Image()
    img.onload = doReadyImage
    img.onerror = doErrorDisplay
    img.src = aImages[iCurrent]
}
function quickPrevLoad() {
    clearTimeout(oTimer)
    oTimer = null
    var img = new Image()
    img.onload = doReadyImage
    img.onerror = doErrorDisplay
    iCurrent -= 1
    if (iCurrent < 0) iCurrent = aImages.length - 1
    iCurrent -= 1
    if (iCurrent < 0) iCurrent = aImages.length - 1
    img.src = aImages[iCurrent]
}

function WriteImageSlideShow(arr) {
    aImages = arr;
    document.write("<IMG NAME=slideShow style=\"border: 0px solid #FF9900; padding-left: 0; padding-right: 0; padding-top: 0; padding-bottom: 0\" SRC=\"" + aImages[iCurrent] + "\" ONERROR=\"doErrorDisplay(); doLoad()\" ONLOAD=\"doLoad()\" WIDTH=\"600px" + aSize[0] + "\" HEIGHT=\"250px" + aSize[1] + "\" STYLE=\"filter: revealTrans(TRANSITION=23)\">")
}


                                                   