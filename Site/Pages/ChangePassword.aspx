<%@ Page Title="SchneiderMilkManagement - Change Password" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Pages_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <script src="../js/ChangePassword.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pn1" runat="server">
        <div class="contentmain">
            <div class="contenttop">
                <h1>
                    <asp:Label ID="lblTitle" runat="server"> Change Password</asp:Label>
                </h1>
            </div>
            <div class="contentmiddle">
                <div class="innermain">
                    <div class="mand">
                        (<span>*</span>) indicates mandatory fields
                    </div>
                    <div class="formmain">
                        <div class="formrow">
                            <div class="center">
                                <asp:Label runat="server" ID="lblErr" CssClass="error" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="formrow">
                            <div class="formfield">
                                <span class="formlabel"><span class="mandatory">*&nbsp;</span>Old Password :</span><span
                                    class="formcontrol">
                                    <asp:TextBox ID="txtOldPwd" runat="server" TextMode="Password" CssClass="regform1"
                                        AutoCompleteType="None"></asp:TextBox>
                                </span>
                            </div>
                            <div class="formfield">
                                <span class="formlabel"><span class="mandatory">*&nbsp;</span>New Password :</span><span
                                    class="formcontrol">
                                    <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password" CssClass="regform1"
                                        AutoCompleteType="None"></asp:TextBox>
                                </span>
                            </div>
                            <div class="formfield">
                                <span class="formlabel"><span class="mandatory">*&nbsp;</span>Confirm Password :</span><span
                                    class="formcontrol">
                                    <asp:TextBox ID="txtCPwd" runat="server" TextMode="Password" CssClass="regform1"
                                        AutoCompleteType="None"></asp:TextBox>
                                </span>
                            </div>
                        </div>
                        <div class="bottombuttons" style="width: 400px !important">
                            <asp:LinkButton ID="lnkSaveDetails" runat="server" OnClick="lnkSaveDetails_Click"
                                OnClientClick="return ValidateFields();"><img src="../App_Themes/Default/Images/save.png" alt="" /></asp:LinkButton>
                            <a runat="server" id="anccancel" class="" href="MonitorBMC.aspx">
                                <img src="../App_Themes/Default/Images/back.png" alt="" /></a>
                        </div>
                    </div>
                </div>
                <div class="pageheight">
                    &nbsp;
                </div>
            </div>
            <div class="contentbottom">
            </div>
        </div>
        <script type="text/javascript">
            var lblErr = '<%=lblErr.ClientID %>';
            var txtOldPwd = '<%=txtOldPwd.ClientID %>';
            var txtNewPwd = '<%=txtNewPwd.ClientID %>';
            var txtCPwd = '<%=txtCPwd.ClientID %>';
        </script>
    </asp:Panel>   
</asp:Content>
