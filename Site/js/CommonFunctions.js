function CheckAll(gridId, rowCount) {
   
   $('#'+gridId).find('input[type="checkbox"]').each(function(){
       if(!($(this).attr("disabled")))
            $(this).attr("checked",true);
       });

    return false;
}

function ClearAll(gridId, rowCount) {
  
  $('#'+gridId).find('input[type="checkbox"]').each(function(){
       if(!($(this).attr("disabled")))
            $(this).attr("checked",false);
       });

    return false;
}

function RemoveHTML(strText) {
    var regEx = /<[^>]*>/g;
    return strText.replace(regEx, "");
}

function Check_Delete(gridId,  rowCount, lblErrMsg) {
    document.getElementById(lblErrMsg).innerHTML = "";

    if ($('#'+gridId).find('input:checked').length == 0) {
        document.getElementById(lblErrMsg).innerHTML = "Select at least one record.";
        window.location = "#";
        return false;

    } else {
        return confirm("Are you sure to delete selected item(s)");
    }
}

function ShowdeleteConfirmation() {
    var lblErrMsg = document.getElementById(lblErrMsg);
    lblErrMsg.innerHTML = 'Successfully deleted the selected items.';
    return false;
}


function AllowDateFormat(event) {
    var keyval = window.event ? window.event.keyCode : event.which;
    if (!((keyval >= 48) && (keyval <= 57) || /*slah*/ keyval == 47)) {
        if (!window.XMLHttpRequest) {
            window.event.keyCode = null;
        } else if (navigator.userAgent.toLowerCase().indexOf("msie") > -1) {
            window.event.keyCode = null;
        } else {
            if (!((keyval == 0) || /*back space*/ (keyval <= 8) || /*tab*/ (keyval == 9) || /*minus*/ (keyval == 45))) {
                event.preventDefault();
            }
        }
    }
}

function IncludeAnd(strErr) {
    var lastindex = 0;
    if (strErr.indexOf(",") > -1) {
        lastindex = strErr.lastIndexOf(",")
        var strcheckstringwithcoma = "";
        var strcheckstringwithand = "";
        strcheckstring = strErr.substring(lastindex, parseInt(strErr.length));
        strcheckstringwithand = strcheckstring.replace(",", " and")
        strErr = strErr.replace(strcheckstring, strcheckstringwithand);
    }
    return strErr;
}

function ChkValidEmail(object) {
    var myRegxp = /\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/

    return myRegxp.test(object);
}

function AllowNumeric(event) {
    if (navigator.appName == "Microsoft Internet Explorer") {
        if (!((window.event.keyCode >= 48) && (window.event.keyCode <= 57) || (window.event.keyCode == 45))) {
            window.event.keyCode = null;
        }
    } else {
        if (!((event.which >= 48) && (event.which <= 57) || (event.which == 8) || (event.keyCode == 9))) {
            event.preventDefault();
        }
    }
}
function AllowFloat(obj, event)
        {
            if (event.which == 47 || event.which == 58 || event.which == 59)
            {
                event.preventDefault();
            }
            if ((event.which < 46 || event.which > 59) && event.which != 8)
            {
                event.preventDefault();
            } // prevent if not number/dot

            if (event.which == 46 && $(this).val().indexOf('.') != -1)
            {
                event.preventDefault();
            } // prevent if already dot
        }

function AllowPhoneNumeric(event) {
    if (navigator.appName == "Microsoft Internet Explorer") {
        if (!((window.event.keyCode >= 48) && (window.event.keyCode <= 57) || (window.event.keyCode == 32) || (window.event.keyCode == 45) || (window.event.keyCode == 43) || (window.event.keyCode == 109))) {
            window.event.keyCode = null;
        }
    } else {
        if (!((event.which >= 48) && (event.which <= 57) || (event.which == 8) || (event.keyCode == 9) || (event.which == 43) || (event.which == 32))) {
            event.preventDefault();
        }
    }
}

//check for valid date format like MM/dd/yyyy
var dtCh = "/";
var minYear = 1800;
var maxYear = 2500;

function stripCharsInBag(s, bag) {
    var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary(year) {
    // February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
}

function DaysArray(n) {
    for (var i = 1; i <= n; i++) {
        this[i] = 31
        if (i == 4 || i == 6 || i == 9 || i == 11) {
            this[i] = 30
        }
        if (i == 2) {
            this[i] = 29
        }
    }
    return this
}

function isDate(dtStr) {
    var daysInMonth = DaysArray(12)
    var pos1 = dtStr.indexOf(dtCh)
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
    var strMonth = dtStr.substring(0, pos1)
    var strDay = dtStr.substring(pos1 + 1, pos2)
    var strYear = dtStr.substring(pos2 + 1)
    strYr = strYear
    if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
    if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
    for (var i = 1; i <= 3; i++) {
        if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
    }
    month = parseInt(strMonth)
    day = parseInt(strDay)
    year = parseInt(strYr)
    if (pos1 == -1 || pos2 == -1) {
        alert("The date format should be : mm/dd/yyyy")
        return false
    }
    if (strMonth.length < 1 || month < 1 || month > 12) {
        alert("Please enter a valid month")
        return false
    }
    if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
        alert("Please enter a valid day")
        return false
    }
    if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
        alert("Please enter a valid 4 digit year between " + minYear + " and " + maxYear)
        return false
    }
    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
        alert("Please enter a valid date")
        return false
    }
    //return true
}

function validateImageFormat(fileuploadid) {
    var extensions = new Array("png", "jpeg", "jpg", "gif", "bmp");

    var image_file = fileuploadid;
    var image_length = image_file.value.length;
    var pos = image_file.value.indexOf('.') + 1;
    var ext = image_file.value.substring(pos, image_length);
    var final_ext = ext.toLowerCase();
    for (i = 0; i < extensions.length; i++)
    if (extensions[i] == final_ext) {
        return true;
    }
    return false;
}

function RemoveHTMLTags(id) {
   
    var str = document.getElementById(id).value;
    document.getElementById(id).value = str.replace(/<\/?[^>]+(>|$)/g, "");
}

function isURL(s) {
    var regexp = /http:\/\/[A-Za-z0-9\.-]{3,}\.[A-Za-z]{3}/;
    return regexp.test(s);
}

function isCheckURL(s) {
    if (s.indexof("http://") >= 0 && s.indexof("ftp://") >= 0 && s.indexof("mailto:") >= 0 && s.length > 8) {
        return false
    } else {
        return true
    }
}

function url_validate(userUrl) {
    var RegExp = "/^(([w]+:)?//)?(([dw]| [a-fA-fd]{2 2})+(:([dw]| [a-fA-fd]{2 2})+)?@)?([dw][-dw]{0 253}[dw].)+[w]{2 4}(:[d]+)?(/([-+_~.dw]| [a-fA-fd]{2 2})*)*(?(&?([-+_~.dw]| [a-fA-fd]{2 2})=?)*)?(#([-+_~.dw]| [a-fA-fd]{2 2})*)?$/";
    var regUrl = /^(((ht|f){1}(tp:[/][/]){1})|((www.){1}))[-a-zA-Z0-9@:%_\+.~#?&//=]+$/;
    if (regUrl.test(userUrl) == false) {
        return false;
    } else {
        return true;
    }
}

function validatelimit(obj, maxchar, lbl) {

    if (this.id) obj = this;

    var remaningChar = maxchar - obj.value.length;
    document.getElementById(lbl).innerHTML = remaningChar;
    if (remaningChar <= 0) {
        obj.value = obj.value.substring(maxchar, 0);
        alert('Character Limit exceeds!');
        return false;

    } else {
        return true;
    }
}

function isUrl(s) {
	var regexp = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/
	return regexp.test(s);
}

function ValidateWebAddress(incomingString) {
    var companyUrl = incomingString;
    var RegExp = /^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$/;
    if (RegExp.test(companyUrl)) {
        return true;
    } else {
        return false;
    }
}


function AllowLatandLon(event) {
    if (navigator.appName == "Microsoft Internet Explorer") {
        if (!((window.event.keyCode >= 48) && (window.event.keyCode <= 57)) && !((window.event.keyCode == 45) || (window.event.keyCode == 46))) {
            window.event.keyCode = null;
        }

    } else {
        if (!((event.which >= 48) && (event.which <= 57) || (event.which == 8) || (event.keyCode == 9)) && !((event.which == 45) || (event.which == 46))) {
            event.preventDefault();
        }

    }
}

function checkdate(input) {
    var validformat = /^\d{2}\-\d{2}\-\d{4}$/
    if (!validformat.test(input.value))
        return false;
    else {
        var monthfield = input.value.split("-")[1]
        var dayfield = input.value.split("-")[0]
        var yearfield = input.value.split("-")[2]
        var dayobj = new Date(yearfield, monthfield - 1, dayfield);
        if ((dayobj.getMonth() + 1 != monthfield) || (dayobj.getDate() != dayfield) || (dayobj.getFullYear() != yearfield))
            return false;
        else
            return true;
    }
}

 function allowAlphanumeric(e) 
         { 
            var k; 
            document.all ? k=e.keycode : k=e.which;
            return((k>47 && k<58)||(k>64 && k<91)||(k>96 && k<123)||k==0 || k == 8||k==32); 
         } 

function getQuerystring(key, default_) {
    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null)
        return default_;
    else
        return qs[1];
}