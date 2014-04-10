function ValidateFields() {
    var msg = "";
    document.getElementById(lblErr).innerHTML = "&nbsp;";
    if (document.getElementById(ddlProfileType).value == "-1")
        msg += " Profile Type,"
    if (document.getElementById(txtName).value.replace(/^\s+|\s+$/, '') == "")
        msg += " Name,"
    if (msg != '') {
        document.getElementById(lblErr).innerHTML = "Please enter these values :" + msg.substring(0, msg.length - 1) + ".";
        window.location = "#";
        return false;
    }
}