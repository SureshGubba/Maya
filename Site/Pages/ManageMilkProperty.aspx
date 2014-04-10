<%@ Page Title="Configure Milk Properties" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="ManageMilkProperty.aspx.cs" Inherits="ManageMilkProperty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../App_Themes/Default/CSS/popup.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnl" runat="server" DefaultButton="btnSearch">
        <asp:UpdatePanel ID="udpCountry" runat="server">
            <ContentTemplate>
                <div class="contentmain">
                    <div class="contenttop">
                        <h1>
                            Configure Milk Properties
                        </h1>
                    </div>
                    <div class="contentmiddle">
                        <div class="innermain">
                            <div class="breadcrum" style="display: none;">
                                <a id="lnkDashboard" href="dashboard.aspx">Dashboard</a> &#187; Configure Milk Properties
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
                                                <asp:Button runat="server" ID="btnSearch" CssClass="searchbuttom" OnClick="btnSearch_Click">
                                                </asp:Button>
                                                <asp:Button runat="server" ID="btnReset" CssClass="clearbuttom" OnClientClick="return  ClearSearchField();">
                                                </asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="errormgs">
                                    <asp:Label ID="lblErrMSG" runat="server" Text="" ForeColor="Red" Font-Size="12"></asp:Label>
                                    <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red">&nbsp;</asp:Label>
                                </div>
                                <div class="gridmain">
                                    <asp:GridView GridLines="none" ID="gvMilkPrperties" ShowHeader="true" runat="server"
                                        AutoGenerateColumns="false" PageSize="10" AllowSorting="true" CssClass="" DataKeyNames="MilkPropertyId"
                                        AllowPaging="true" DataSourceID="odsMilkPrperties">
                                        <RowStyle CssClass="gridrow" />
                                        <PagerStyle CssClass="pagestyle" HorizontalAlign="Right" />
                                        <AlternatingRowStyle CssClass="gridrow" />
                                        <HeaderStyle CssClass="gridheadernew" />
                                        <FooterStyle CssClass="gridbottom" />
                                        <EmptyDataTemplate>
                                            <h3 class="error aligncenter">
                                                No records available.</h3>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="25px" HorizontalAlign="Center" />
                                                <ItemStyle Width="25px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDel" runat="server" name="chkDel" Enabled='<%# Convert.ToBoolean(Eval("AllowDelete"))? true:false %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="PropertyName">
                                                <HeaderStyle Width="150px" CssClass="fll" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkPropertyName" CausesValidation="False" Visible="true" runat="server"
                                                        CommandName="Sort" CommandArgument="PropertyName">
                                         Property</asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPropertyName" runat="server" ToolTip='<%# Eval("PropertyName") %>'
                                                        Text='<%# Eval("PropertyName").ToString() %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField SortExpression="MinValue">
                                                <HeaderStyle Width="150px" CssClass="fll" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkMinVal" CausesValidation="False" Visible="true" runat="server"
                                                        CommandName="Sort" CommandArgument="MinValue">
                                        Min Value</asp:LinkButton></HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("MinValue")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="MaxValue">
                                                <HeaderStyle Width="150px" CssClass="fll" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkMaxValue" CausesValidation="False" Visible="true" runat="server"
                                                        CommandName="Sort" CommandArgument="MaxValue">
                                        Max Value</asp:LinkButton></HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("MaxValue")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="IsActive">
                                                <HeaderStyle Width="100px" CssClass="fll" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkIsActive" CausesValidation="False" Visible="true" runat="server"
                                                        CommandName="Sort" CommandArgument="MaxValue">
                                        IsActive</asp:LinkButton></HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToBoolean(Eval("IsActive")) ?"Active":"InActive"%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    Edit</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="LbtDealItem" ToolTip="Edit Milk Property"
                                                        PostBackUrl='<%# "~/Pages/AddEditMilkProperty.aspx?mode=Edit&ID=" + Eval("MilkPropertyId")%>'><img alt="" src="../App_Themes/Default/Images/icon_edit.gif" /> 
                                                    </asp:LinkButton></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div style="width: 100%; padding-left: 20px; clear: both;">
                            <asp:LinkButton ID="lnkAddNew" Font-Size="12px" ForeColor="#065C8E" PostBackUrl="AddEditMilkProperty.aspx?mode=add"
                                runat="server">Add New</asp:LinkButton>
                            <span runat="server" id="spanGridOptions" visible="false">&nbsp;|&nbsp;
                                <asp:LinkButton runat="server" ID="lnkCheckAll" CssClass="gridfooterlinks" Text="Check All"></asp:LinkButton>&nbsp;|&nbsp;
                                <asp:LinkButton runat="server" ID="lnkClearAll" CssClass="gridfooterlinks" Text="Clear All"></asp:LinkButton>&nbsp;|&nbsp;
                                <asp:LinkButton runat="server" ID="lnkDelete" CssClass="gridfooterlinks" Text="Delete Checked"></asp:LinkButton></span>
                        </div>
                    </div>
                    <div class="contentbottom">
                    </div>
                    <asp:ObjectDataSource ID="odsMilkPrperties" runat="server" DataObjectTypeName="MilkProperty"
                        SelectCountMethod="GetMilkPropertyCount" EnablePaging="true" SelectMethod="SelAllByPaging"
                        TypeName="SchneiderMilkManagement.BusinessLayer.BusinessFacade.MilkPropertyBF">
                        <SelectParameters>
                            <asp:Parameter Direction="Input" Name="SortBy" DefaultValue="CreatedOn Desc" Type="String" />
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
        function ClearSearchField()
        {
            document.getElementById('<%= txtTitle.ClientID %>').value = "";
        }
    </script>
</asp:Content>
