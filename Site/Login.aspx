<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login"
    Debug="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Schneider Maya - Login Page</title>
    <link href="App_Themes/Default/CSS/StyleSheet2.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="container">
            <div class="header">
                <div class="headerleft">
                   <img src="App_Themes/Default/Images/SchneiderElectricLogo.png" height="75"     alt="" />
                   <h1>Remote Monitoring of BMC</h1>
                </div>
                <div class="headerright">
                </div>
            </div>
            <div class="menu">
            </div>
            <div class="contentmain">
            </div>
            <div class="contenttop">
            </div>
            <div class="contentmiddle">
                <div id="divErrMsg" style="color: Red; float: left; line-height: 20px; padding-left: 12px;
                    text-align: center;width:100%;">
                    <asp:Label ID="lblErrMsg" runat="server" Style="display: none;" CssClass="loginErr"></asp:Label></div>
                <div class="loginbg" id="login">
                    <div class="loginbox">
                        <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false"
                            OnAuthenticate="LoginUser_Authenticate">
                            <LayoutTemplate>
                                <div class="loginrow">
                                    <div class="loginform">
                                        <asp:TextBox ID="UserName" runat="server" CssClass="logininput"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="loginrow loggap">
                                    <div class="loginform">
                                        <asp:TextBox ID="Password" runat="server" CssClass="logininput" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="loginrow loggap">
                                    <span class="fl">
                                        <asp:CheckBox ID="RememberMe" runat="server" />
                                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Remember me</asp:Label></span>
                                    <span class="fr">
                                        <asp:LinkButton ID="LoginButton" runat="server" Text="" CommandName="Login" OnClientClick="return ValidateLogin();"
                                            CssClass="loginbutton"> 
                                        </asp:LinkButton>
                                    </span>
                                </div>
                            </LayoutTemplate>
                        </asp:Login>
                    </div>
                </div>
            </div>
            <div class="contentbottom">
                &nbsp;</div>
        </div>
    </div>
    <div class="fottermian">
        <div class="fotter">
            <div class="fotterleft">
                Copyright &copy; Schneider Electric. All rights reserved.</div>      
                    
        </div>
    </div>
    </form>
</body>
<script type="text/javascript">

    function ValidateLogin()
    {
        var isValid = true;
        if (document.getElementById('<%= ((TextBox)LoginUser.FindControl("UserName")).ClientID %>').value.replace(/^\s+|\s+$/, '') == "")
        {

            document.getElementById('<%= lblErrMsg.ClientID %>').innerHTML = " Please enter Username ";
            isValid = false;
        }
        if (document.getElementById('<%= ((TextBox)LoginUser.FindControl("Password")).ClientID %>').value.replace(/^\s+|\s+$/, '') == "")
        {

            if (document.getElementById('<%= lblErrMsg.ClientID %>').innerHTML == " Please enter Username ")
            {

                document.getElementById('<%= lblErrMsg.ClientID %>').innerHTML += " and Password.";
                isValid = false;
            } else
            {
                document.getElementById('<%= lblErrMsg.ClientID %>').innerHTML = " Please enter Password.";
                isValid = false;
            }
        }
        if (isValid == false)
            document.getElementById('<%= lblErrMsg.ClientID %>').style.display = "block";
        return isValid;
    }
   
</script>
</html>
