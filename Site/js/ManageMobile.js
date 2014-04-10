function ValidateFields() {
            var msg = "";
            document.getElementById(lblErr).innerHTML = "&nbsp;";
            if (document.getElementById(txtMobile).value.replace(/^\s+|\s+$/, '') == "")
                msg += " Mobile No,"
            else {
                if (document.getElementById(txtMobile).value.replace(/^\s+|\s+$/, '').length != 10)
                    msg += " Mobile No,"  
            }       
            if (msg != '') {
                document.getElementById(lblErr).innerHTML = "The Following field(s) have invalid values :" + msg.substring(0, msg.length - 1) + ".";
                window.location = "#";
                return false;
            }
        }