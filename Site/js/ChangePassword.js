
function ValidateFields() {
    var msg = '', msg1 = '';
    document.getElementById(lblErr).innerHTML = "&nbsp;";
    if (document.getElementById(txtOldPwd).value.replace(/^\s+|\s+$/, '') == "")
        msg += " Old Password,"
    if (document.getElementById(txtNewPwd).value.replace(/^\s+|\s+$/, '') == "")
        msg += " New Password,"
    if (document.getElementById(txtCPwd).value.replace(/^\s+|\s+$/, '') == "")
        msg += " Confirm Password,"
    else if (document.getElementById(txtCPwd).value.replace(/^\s+|\s+$/, '') != document.getElementById(txtNewPwd).value.replace(/^\s+|\s+$/, ''))
        msg1 = " New Password and Confirm Password should match."
    if (msg != '') {
        document.getElementById(lblErr).innerHTML = "Please enter these values :" + msg.substring(0, msg.length - 1) + ".";
        window.location = "#";
        return false;
    }
    else if (msg1 != '') {
        document.getElementById(lblErr).innerHTML = msg1;
        window.location = "#";
        return false;
    }
}