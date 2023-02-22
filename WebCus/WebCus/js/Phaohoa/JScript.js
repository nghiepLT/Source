var bgBegin = 22;
var bgEnd = 31;
var bgTetId = 31;
var bgTetUrl = '/banner/';
document.body.style.backgroundImage = 'url(' + bgTetUrl + bgTetId + '.jpg)';
document.body.style.backgroundAttachment = 'fixed';
document.body.style.backgroundSize = 'cover';
setInterval(function () {
    if (bgTetId == bgEnd) bgTetId = bgBegin;
    document.body.style.backgroundImage = 'url(' + bgTetUrl + bgTetId + '.jpg)';
    document.body.style.backgroundAttachment = 'fixed';
    document.body.style.backgroundSize = 'cover';
    bgTetId++;
}, 15000);