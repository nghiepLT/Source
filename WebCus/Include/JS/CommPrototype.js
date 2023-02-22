/*--------------------------------------------------------------------------------------*/
//  업무구분          :   공통
//  세부업무 구분   :    자바스크립트 프로토타입
//  프로그래명       :   CommPrototype.js
//  개발자            :    강성모
//  개발일            :    2007. 06. 26
//  개발내용         :    자주 사용하는 자바스크립트 프로토타입 정의
/*--------------------------------------------------------------------------------------*/

String.prototype.isEng = function() {
    if (this.search(/^[^A-Za-z]/) == -1)
        return true;
    else
        return false;
}
/* 영문,숫자 입력체크 */
String.prototype.isEngNum = function() {
    if (this.search(/[^A-Za-z0-9]/) == -1)
        return true;
    else
        return false;
}
/* 공백제거 */
String.prototype.trim = function() {
    return this.replace(/(^\s*)|(\s*$)|($\s*)/g, "");
}
/* 아이디 유효성 체크 */
String.prototype.isid = function() {
    if (this.search(/[^A-Za-z0-9_-]/) == -1)
        return true;
    else
        return false;
}
/* 영문 체크 */
String.prototype.isalpha = function() {
    if (this.search(/[^A-Za-z]/) == -1)
        return true;
    else
        return false;
}
/* 숫자체크 */
String.prototype.isnumber = function() {
    if (this.search(/[^0-9]/) == -1)
        return true;
    else
        return false;
}

String.prototype.isDecimal = function() {
    if (this.search(/^(\\+|-)?[0-9][0-9]*(\\.[0-9]*)?$/) == -1)
        return true;
    else
        return false;
}

/* E-Mail 체크 */
String.prototype.isemail = function() {
    var flag, md, pd, i;
    var str;

    if ((md = this.indexOf("@")) < 0)
        return false;
    else if (md == 0)
        return false;
    else if (this.substring(0, md).search(/[^.A-Za-z0-9_-]/) != -1)
        return false;
    else if ((pd = this.indexOf(".")) < 0)
        return false;
    else if ((pd + 1) == this.length || (pd - 1) == md)
        return false;
    else if (this.substring(md + 1, this.length).search(/[^.A-Za-z0-9_-]/) != -1)
        return false;
    else
        return true;
}
/* 한글자리 수 */
String.prototype.korlen = function() {
    var temp;
    var set = 0;
    var mycount = 0;

    for (k = 0; k < this.length; k++) {
        temp = this.charAt(k);

        if (escape(temp).length > 4) {
            mycount += 2;
        }
        else mycount++;
    }

    return mycount;
}
/* 주민등록번호 휴요성 체크 */
String.prototype.isssn = function() {

    var first = new Array(6);
    var second = new Array(7);
    var total = 0;
    var tmp = 0;

    if (this.length != 13)
        return false;
    else {
        for (i = 1; i < 7; i++)
            first[i] = this.substring(i - 1, i);

        for (i = 1; i < 8; i++)
            second[i] = this.substring(6 + i - 1, i + 6);

        for (i = 1; i < 7; i++) {
            if (i < 3)
                tmp = Number(second[i]) * (i + 7);
            else if (i >= 3)
                tmp = Number(second[i]) * ((i + 9) % 10);

            total = total + Number(first[i]) * (i + 1) + tmp;
        }

        if (Number(second[7]) != ((11 - (total % 11)) % 10))
            return false;
    }
    return true;
}
/* 바이트 수 반환 */
String.prototype.bytes = function() {
    var str = this;
    var l = 0;
    for (var i = 0; i < str.length; i++) l += (str.charCodeAt(i) > 128) ? 2 : 1;
    return l;
}
/* Replace */
String.prototype.replaceAll = function(oldValue, newValue) {

    var retValue = this;

    while (retValue.indexOf(oldValue) >= 0) {
        retValue = retValue.replace(oldValue, newValue);
    }

    return retValue;
}

String.prototype.divDecimal = function(index) {
    var value = this;
    var pos = String(value).indexOf('.');

    if (pos > 0 && String(value).length > pos + index) {
        value = String(value).substring(0, pos + index);
    }

    return value;
}

String.prototype.toSlug = function () {
    str = this.toString();
    if (typeof str === "undefined") {
        return "";
    }
    str = str.replace(/^\s+|\s+$/g, ''); // trim
    str = str.toLowerCase();

    // remove accents, swap ñ for n, etc
    var fromLower = "aáàảãạăắằẳẵặâấầẩẫậđeéèẻẽẹêếềểễệiíìỉĩịoóòỏõọôốồổỗộơớờởỡợuúùủũụưứừửữựyýỳỷỹỵ·/_,:;";
    var fromUpper = "AÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬĐEÉÈẺẼẸÊẾỀỂỄỆIÍÌỈĨỊOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢUÚÙỦŨỤƯỨỪỬỮỰYÝỲỶỸỴ·/_,:;";
    var to = "aaaaaaaaaaaaaaaaaadeeeeeeeeeeeeiiiiiioooooooooooooooooouuuuuuuuuuuuyyyyyy------";

    var fromLength = fromLower.length; // fromUpper and from Lower have the same lengthen.

    for (var i = 0, l = fromLength; i < l; i++) {
        str = str.replace(new RegExp(fromLower.charAt(i), 'g'), to.charAt(i));
        str = str.replace(new RegExp(fromUpper.charAt(i), 'g'), to.charAt(i));
    }

    str = str.replace(/[^a-z0-9 -]/g, '')// remove invalid chars
		.replace(/\s+/g, '-')// collapse whitespace and replace by -
		.replace(/-+/g, '-'); // collapse dashes

    return str;
}
