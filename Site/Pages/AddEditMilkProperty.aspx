<%@ Page Title="SchneiderMilkManagement - Add/Edit Milk Properties" Language="C#"
    MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="AddEditMilkProperty.aspx.cs"
    Inherits="AddEditMilkProperty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/CommonFunctions.js" type="text/javascript"></script>
    <script src="../js/AddEditMilkProperty.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pn1" runat="server" DefaultButton="lnkSaveDetails">
        <div class="contentmain">
            <div class="contenttop">
                <h1>
                    <asp:Label ID="lblTitle" runat="server"> Add / Edit Milk Property details</asp:Label>
                </h1>
            </div>
            <div class="contentmiddle">
                <div class="innermain">
                    <div class="breadcrum">
                        <a id="lnkMilkProperty" href="ManageMilkProperty.aspx">Configuration</a>
                        &#187; Add / Edit Milk Property details
                    </div>
                    <div class="mand">
                        (<span>*</span>) indicates mandatory fields
                    </div>
                    <asp:Panel ID="pnlForm" runat="server">
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
                                        <asp:DropDownList ID="ddlProfileType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProfileType_Selection">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="formfield  fr">
                                    <span class="formlabel"><span class="mandatory">*&nbsp;</span>Property Name <b>:</b></span><span
                                        class="formcontrol">
                                        <asp:DropDownList ID="ddlPropertyName" runat="server">
                                        </asp:DropDownList>
                                        <br />
                                    </span>
                                </div>
                            </div>
                            <div class="formrow">
                                <div class="formfield">
                                    <span class="formlabel"><span class="mandatory">*&nbsp;</span>Property Type <b>:</b></span><span
                                        class="formcontrol">
                                        <asp:DropDownList ID="ddlPropertyType" runat="server">
                                            <asp:ListItem Text="Select " Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Value" Value="Value"></asp:ListItem>
                                            <asp:ListItem Text="Range" Value="Range"></asp:ListItem>
                                            <asp:ListItem Text="Boolean" Value="Boolean"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </div>
                                <div class="formfield fr">
                                    <span class="formlabel"><span class="mandatory">*&nbsp;</span>Unit<b>:</b> </span>
                                    <span class="formcontrol">
                                        <asp:TextBox ID="txtUnit" runat="server"></asp:TextBox>
                                    </span>
                                </div>
                            </div>
                            <div class="formrow">
                                <div class="formfield">
                                    <span class="formlabel"><span class="mandatory">*&nbsp;</span>Min Value <b>:</b></span><span
                                        class="formcontrol">
                                        <asp:TextBox ID="txtMinvalue" MaxLength="10" runat="server" CssClass="regform1" AutoCompleteType="None"
                                            onkeypress="return AllowFloat(this,event);"></asp:TextBox>
                                    </span>
                                </div>
                                <div class="formfield fr">
                                    <span class="formlabel"><span class="mandatory">*&nbsp;</span>Max Value <b>:</b></span><span
                                        class="formcontrol">
                                        <asp:TextBox ID="txtMaxValue" MaxLength="10" runat="server" CssClass="regform1" AutoCompleteType="None"
                                            onkeypress="return AllowFloat(this,event);"></asp:TextBox>
                                    </span>
                                </div>
                            </div>
                            <div class="formrow">
                                <div class="formfield">
                                    <span class="formlabel">Property Address <b>:</b></span><span class="formcontrol">
                                        <asp:TextBox ID="txtPortAddress" MaxLength="20" runat="server" CssClass="regform1"
                                            AutoCompleteType="None"></asp:TextBox>
                                    </span>
                                </div>
                                <div class="formfield fr">
                                    <span class="formlabel">Allow Delete <b>:</b></span><span><asp:CheckBox ID="chkAllowDelete"
                                        runat="server" Style="margin-top: 6px; float: left;" Checked="true" /></span>
                                    <span class="formlabel">Is Active <b>:</b></span><span><asp:CheckBox ID="chkIsActive"
                                        runat="server" Style="margin-top: 6px; float: left;" Checked="true" /></span>
                                    <span class="formlabel">Is SMS Required <b>:</b></span><span><asp:CheckBox ID="chkIsSMSRequired"
                                        runat="server" Style="margin-top: 6px; float: left;" Checked="true" /></span>
                                </div>
                            </div>
                            <div class="formrow">
                                <div class="formfield">
                                    <span class="formlabel"><span class="mandatory">*&nbsp;</span>SMS Polling Time <b>:</b></span><span
                                        class="formcontrol">
                                        <asp:TextBox ID="txtSmsPollingTime" MaxLength="10" runat="server" CssClass="regform1"
                                            onkeypress="return AllowNumeric(event);" AutoCompleteType="None" ondrag="return false;"
                                            ondrop="return false;"></asp:TextBox>
                                    </span>
                                </div>
                                <div class="formfield fr">
                                    <span class="formlabel">Polling Time (Minutes) <b>:</b></span><span class="formcontrol">
                                        <asp:TextBox ID="txtPollingTime" MaxLength="5" runat="server" CssClass="regform1"
                                            AutoCompleteType="None" onkeypress="return AllowFloat(this,event);"></asp:TextBox>
                                    </span>
                                </div>
                            </div>
                            <div class="bottombuttons" style="width: 400px !important">
                                <asp:LinkButton ID="lnkSaveDetails" runat="server" OnClick="lnkSaveDetails_Click"
                                    OnClientClick="return ValidateFields();"><img src="../App_Themes/Default/Images/save.png" alt="" /></asp:LinkButton>
                                <a runat="server" id="anccancel" class="" href="ManageMilkProperty.aspx">
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
                                    <img src="../App_Themes/Default/Images/edit.png" alt="" /></a> <a href="ManageMilkProperty.aspx"
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
        var ddlPropertyName = '<%=ddlPropertyName.ClientID %>';
        var ddlPropertyType = '<%=ddlPropertyType.ClientID %>';
        var txtUnit = '<%=txtUnit.ClientID %>';
        var txtMinvalue = '<%=txtMinvalue.ClientID %>';
        var txtMaxValue = '<%=txtMaxValue.ClientID %>';
        var txtPollingTime = '<%=txtPollingTime.ClientID %>';
        var txtSmsPollingTime = '<%=txtSmsPollingTime.ClientID %>';
        var txtMaxValue = '<%=txtMaxValue.ClientID %>';
        var txtPollingTime = '<%=txtPollingTime.ClientID %>';
    </script>
</asp:Content>
