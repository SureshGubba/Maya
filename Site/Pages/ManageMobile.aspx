<%@ Page Title="Add Mobile Number" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="ManageMobile.aspx.cs" Inherits="ManageMobile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/CommonFunctions.js" type="text/javascript"></script>
    <script src="../js/ManageMobile.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pn1" runat="server" DefaultButton="lnkSaveDetails">
        <div class="contentmain">
            <div class="contenttop">
                <h1>
                    <asp:Label ID="lblTitle" runat="server">Add Mobile No</asp:Label>
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
                                <span class="formlabel"><span class="mandatory">*&nbsp;</span>Mobile No <b>:</b></span><span
                                    class="formcontrol">
                                    <asp:TextBox ID="txtMobile" MaxLength="10" runat="server" CssClass="regform1" onkeypress="return AllowNumeric(event);" AutoCompleteType="None"></asp:TextBox>
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
    </asp:Panel>
    <script type="text/javascript">
        var lblErr = '<%=lblErr.ClientID %>';
        var txtMobile = '<%=txtMobile.ClientID %>';         
        </script>
   </asp:Content>
