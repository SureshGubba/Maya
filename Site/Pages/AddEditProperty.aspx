<%@ Page Title="SchneiderMilkManagement - Add/Edit Properties" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="AddEditProperty.aspx.cs" Inherits="AddEditProperty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/CommonFunctions.js" type="text/javascript"></script>
    <script src="../js/AddEditProperty.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pn1" runat="server" DefaultButton="lnkSaveDetails">
        <div class="contentmain">
            <div class="contenttop">
                <h1>
                    <asp:Label ID="lblTitle" runat="server"> Add / Edit Property details</asp:Label>
                </h1>
            </div>
            <div class="contentmiddle">
                <div class="innermain">
                    <div class="breadcrum">
                        <a id="lnkProperty" href="ManageProperty.aspx">Manage Properties</a> &#187; Add
                        / Edit Property details
                    </div>
                    <div class="mand">
                        (<span>*</span>) indicates mandatory fields
                    </div>
                    <asp:Panel ID="pnlForm" runat="server" >
                    <div class="formmain">
                        <div class="formrow">
                            <div class="center">
                                <asp:Label runat="server" ID="lblErr" CssClass="error" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="formrow">
                            <div class="formfield">
                                <div class="formlabel">
                                    <span>*</span> Profile Type :</div>
                                <div class="formcontrol">
                                    <asp:DropDownList ID="ddlProfileType" runat="server">                                        
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="formfield fr">
                                <span class="formlabel"><span class="mandatory">*&nbsp;</span>Name <b>:</b></span><span
                                    class="formcontrol">
                                    <asp:TextBox ID="txtName" MaxLength="100" runat="server" CssClass="regform1" AutoCompleteType="None"></asp:TextBox>
                                </span>
                            </div>                            
                        </div>
                        <div class="bottombuttons" style="width: 400px !important">
                            <asp:LinkButton ID="lnkSaveDetails" runat="server" OnClick="lnkSaveDetails_Click"
                                OnClientClick="return ValidateFields();"><img src="../App_Themes/Default/Images/save.png" alt="" /></asp:LinkButton>
                            <a runat="server" id="anccancel" class="" href="ManageProperty.aspx">
                                <img src="../App_Themes/Default/Images/back.png" alt="" /></a>
                        </div>
                    </div>
                    </asp:Panel>
                     <asp:Panel ID="pnlConfirmation" runat="server" Visible="false">
                        <div class="formmain">
                            <div class="formrow">
                                <h3 style="text-align: center;">
                                    <asp:Label ID="lblConfirmationMessage" runat="server" Text="Successfully saved." />
                                </h3>
                            </div>
                            <div class="bottombuttons" style="width: 400px !important;padding:10px 0 0 320px;">
                                <a onclick="return ValidateFields();" runat="server"  id="ancSaveDetails" href="javascript:void(0);">
                                    <img src="../App_Themes/Default/Images/edit.png" alt="" /></a> <a href="ManageProperty.aspx"
                                        id="ancBack" class="">
                                        <img src="../App_Themes/Default/Images/back.png" alt="" /></a>
                            </div>
                        </div>
                    </asp:Panel>
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
         var ddlProfileType = '<%=ddlProfileType.ClientID %>';
         var txtName = '<%=txtName.ClientID %>';         
        </script>
</asp:Content>
