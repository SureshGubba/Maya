<%@ Page Title=" View Logs" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="ViewLogs.aspx.cs" Inherits="ViewLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../App_Themes/Default/CSS/popup.css" rel="stylesheet" type="text/css" />
    <link href="../js/calendar-blue.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../js/calendar-en.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtFromDate.ClientID %>").dynDateTime({
                showsTime: true,
                ifFormat: "%Y/%m/%d %H:%M",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
            $("#<%=txtToDate.ClientID %>").dynDateTime({
                showsTime: true,
                ifFormat: "%Y/%m/%d %H:%M",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
    </script>
 
   

    <asp:Panel ID="pnl" runat="server" DefaultButton="btnSearch">
        <asp:UpdatePanel ID="udpCountry" runat="server">

            <ContentTemplate>

                <div class="contentmain" runat="server" style="margin-right: 10px;margin-left: 10px;margin-bottom: -50px;">
                    <table>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td><div>From Date</div></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtFromDate" Height="25px" Enabled="False"></asp:TextBox>
                                <img src="../Images/calender.png" alt="Calendar" height="20" style="margin-bottom:-5px;margin-left:1px;" /></td>
                            <td ><div style="margin-left:20px;" >To Date</div></td>
                            <td>
                                <asp:TextBox ID="txtToDate" runat="server" Height="25px" Enabled="False"></asp:TextBox>
                                <img src="../Images/calender.png" alt="Calendar" height="20" style="margin-bottom:-5px;margin-left:1px;" /></td>
                            <td></td>
                            <td>
                           <asp:ImageButton runat="server" ID="btnExportToExcel" OnClick="btnExportToExcel_Click" AlternateText="Export To Excel" Height="40px" ImageUrl="~/Images/exporttoexcel.jpg" ToolTip="Export To Excel" OnClientClick="ShowProgress();"   />
                        
                              <div class="loading" align="center" runat="server">
    Loading. Please wait.<br />
    <br />
    <img src="../App_Themes/Default/Images/loading.gif" alt="" id="LoadingImage" />
</div>
                                    <%-- <asp:Button runat="server" Text="Export To Excel" ID="btnExportToExcel"  OnClick="btnExportToExcel_Click" AlternateText="Export To Excel"    />--%>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="errormgs">
                    <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red" Font-Size="12"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text="" ForeColor="Red">&nbsp;</asp:Label>
                </div>

                <div class="contentmain">
                    <div class="contenttop">
                        <h1>View Logs
                        </h1>
                    </div>
                    <div class="contentmiddle">
                        <div class="innermain">
                            <div class="breadcrum" style="display: none;">
                                <a id="lnkDashboard" href="dashboard.aspx">Dashboard</a> &#187; View Logs
                            </div>
                            <div class="gridconatiner">
                                <div class="search">
                                    <div class="searchm">
                                        <div class="formfield1">
                                            <div class="formlabel1">
                                                Search :
                                            </div>
                                            <div class="seacontrol">
                                                <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox>
                                            </div>
                                            <div class="butm">
                                                <asp:Button runat="server" ID="btnSearch" CssClass="searchbuttom" OnClick="btnSearch_Click"></asp:Button>
                                                <asp:Button runat="server" ID="btnReset" CssClass="clearbuttom" OnClientClick="return  ClearSearchField();"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="errormgs">
                                    <asp:Label ID="lblErrMSG" runat="server" Text="" ForeColor="Red" Font-Size="12"></asp:Label>
                                    <asp:Label ID="lblInformation" runat="server" ForeColor="Blue" Font-Size="10pt"></asp:Label>
                                </div>
                                <div class="gridmain">
                                    <asp:GridView GridLines="none" ID="gvProfileData" ShowHeader="true" runat="server"
                                        AutoGenerateColumns="false" PageSize="10" AllowSorting="true" CssClass="" DataKeyNames="ProfileDataId"
                                        AllowPaging="true" DataSourceID="odsProfileData">
                                        <RowStyle CssClass="gridrow" />
                                        <PagerStyle CssClass="pagestyle" HorizontalAlign="Right" />
                                        <AlternatingRowStyle CssClass="gridrow" />
                                        <HeaderStyle CssClass="gridheadernew" />
                                        <FooterStyle CssClass="gridbottom" />
                                        <EmptyDataTemplate>
                                            <h3 class="error aligncenter">No records available.</h3>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField SortExpression="ParameterName">
                                                <HeaderStyle Width="150px" CssClass="fll" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkParameterName" CausesValidation="False" Visible="true" runat="server"
                                                        CommandName="Sort" CommandArgument="ParameterName">
                                         Parameter Name</asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParameterName" runat="server" ToolTip='<%# Eval("ParameterName") %>'
                                                        Text='<%# Eval("ParameterName").ToString() %>'
                                                        ForeColor='<%# GetCustomizedStyleForUIParameter(Eval("ParameterName").ToString(),Convert.ToDouble(Eval("CapturedValue")),Convert.ToDouble(Eval("MinValue")),Convert.ToDouble(Eval("MaxValue"))) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="MinValue">
                                                <HeaderStyle Width="150px" CssClass="fll" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkMinValue" CausesValidation="False" Visible="true" runat="server"
                                                        CommandName="Sort" CommandArgument="MinValue">
                                         MinValue</asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMinValue" runat="server" ToolTip='<%# Eval("MinValue") %>'
                                                        Text='<%# GetCustomizedMinValue(Convert.ToString(Eval("ParameterName").ToString()),Convert.ToDouble(Eval("MinValue").ToString())) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="MaxValue">
                                                <HeaderStyle Width="150px" CssClass="fll" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkMaxValue" CausesValidation="False" Visible="true" runat="server"
                                                        CommandName="Sort" CommandArgument="MaxValue">
                                         MaxValue</asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProfileData" runat="server" ToolTip='<%# Eval("MaxValue") %>'
                                                        Text='<%# GetCustomizedMaxValue(Eval("ParameterName").ToString(),Convert.ToDouble(Eval("MaxValue"))) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="CapturedValue">
                                                <HeaderStyle Width="150px" CssClass="fll" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkCapturedValue" CausesValidation="False" Visible="true" runat="server"
                                                        CommandName="Sort" CommandArgument="CapturedValue">
                                         CapturedValue</asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCapturedValue"
                                                        ForeColor='<%# GetCustomizedStyleForUIParameter(Eval("ParameterName").ToString(),Convert.ToDouble(Eval("CapturedValue")),Convert.ToDouble(Eval("MinValue")),Convert.ToDouble(Eval("MaxValue"))) %>'
                                                        runat="server" ToolTip='<%# Eval("CapturedValue") %>'
                                                        Text='<%# GetCustomizedParameterForUI(Eval("ParameterName").ToString(),Convert.ToDecimal(Eval("CapturedValue"))) %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderStyle Width="100px" CssClass="fll" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkStatus" CausesValidation="False" Visible="true" runat="server"
                                                        CommandName="Sort" CommandArgument="Date">
                                         Status</asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Image ID="imgStatus" runat="server" CssClass="label" ToolTip='<%# GetStatusToolTip(Convert.ToDouble(Eval("MinValue")),Convert.ToDouble(Eval("MaxValue")),Convert.ToDouble(Eval("CapturedValue"))) %>'
                                                        src='<%# GetStatusImage(Convert.ToDouble(Eval("MinValue")),Convert.ToDouble(Eval("MaxValue")),Convert.ToDouble(Eval("CapturedValue")),Convert.ToString(Eval("ParameterName")))  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Date">
                                                <HeaderStyle Width="150px" CssClass="fll" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkDate" CausesValidation="False" Visible="true" runat="server"
                                                        CommandName="Sort" CommandArgument="Date">
                                         Date</asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" ToolTip='<%# Eval("Date") %>'
                                                        Text='<%# Eval("Date").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div style="width: 100%; padding-left: 20px; clear: both; display: none;">
                            <asp:LinkButton ID="lnkAddNew" Font-Size="12px" ForeColor="#065C8E" PostBackUrl="AddEditProfileData.aspx?mode=add"
                                runat="server">Add New</asp:LinkButton>
                            <span runat="server" id="spanGridOptions" visible="false">&nbsp;|&nbsp;
                                <asp:LinkButton runat="server" ID="lnkCheckAll" CssClass="gridfooterlinks" Text="Check All"></asp:LinkButton>&nbsp;|&nbsp;
                                <asp:LinkButton runat="server" ID="lnkClearAll" CssClass="gridfooterlinks" Text="Clear All"></asp:LinkButton>&nbsp;|&nbsp;
                                <asp:LinkButton runat="server" ID="lnkDelete" CssClass="gridfooterlinks" Text="Delete Checked"></asp:LinkButton></span>
                        </div>
                    </div>
                    <div class="contentbottom">
                    </div>
                    <asp:ObjectDataSource ID="odsProfileData" runat="server" DataObjectTypeName="ProfileData"
                        SelectCountMethod="GetProfileDataCount" EnablePaging="true" SelectMethod="SelAllByPaging"
                        TypeName="SchneiderMilkManagement.BusinessLayer.BusinessFacade.ProfileDataFacade">
                        <SelectParameters>
                            <asp:Parameter Direction="Input" Name="SortBy" DefaultValue="ProfileDataId Desc" Type="String" />
                            <asp:ControlParameter DefaultValue="1=1" PropertyName="Value" Type="String" Name="SearchString"
                                ControlID="hidSearchString" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <input type="hidden" runat="server" value="" id="hndSearchValue" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:HiddenField runat="server" ID="hidStartIndex" Value="0" />
    <asp:HiddenField runat="server" ID="hidSearchString" Value=" 1=1" />
    <script src="../js/CommonFunctions.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ClearSearchField() {
            document.getElementById('<%= txtTitle.ClientID %>').value = "";
        }
        
       
        function ShowProgress() {
      
               
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
        function GotoDownloadPage(openerFile)
        {
             window.open(openerFile);
        }
    
    </script>
</asp:Content>
