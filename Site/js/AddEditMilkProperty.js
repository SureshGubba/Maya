function ValidateFields() {
    var msg = "";
    document.getElementById(lblErr).innerHTML = "&nbsp;";
    if (document.getElementById(ddlProfileType).value == "-1")
        msg += " Profile Type,"
    if (document.getElementById(ddlPropertyName).value == "-1")
        msg += " Property Name,"
    if (document.getElementById(ddlPropertyType).value == "")
        msg += " Property Type,"
    if (document.getElementById(txtUnit).value == "")
        msg += " Unit,"
    if (document.getElementById(txtMinvalue).value == "")
        msg += " Min Value,"
    if (document.getElementById(txtMaxValue).value == "")
        msg += " Max Value,"
    if (document.getElementById(txtPollingTime).value == "" || document.getElementById(txtPollingTime).value == 0
            || document.getElementById(txtPollingTime).value == ".")
        msg += " Polling Time,"
    if (document.getElementById(txtSmsPollingTime).value == "" || document.getElementById(txtSmsPollingTime).value == 0)
        msg += " SMS Polling Time,"
    if (msg != '') {
        document.getElementById(lblErr).innerHTML = "Please enter these values :" + msg.substring(0, msg.length - 1) + ".";
        window.location = "#";
        return false;
    }
    else {
        if (parseFloat(document.getElementById(txtMinvalue).value) > parseFloat(document.getElementById(txtMaxValue).value)) {
            document.getElementById(lblErr).innerHTML = "Min value must be less than max value.";
            window.location = "#";
            return false;
        }
    }
}