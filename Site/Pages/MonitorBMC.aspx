<%@ Page Title="Monitor BMC" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="MonitorBMC.aspx.cs" Inherits="MonitorBMC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/ChangePassword.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pn1" runat="server">
        <div class="contentmain">
            <div class="contenttop">
                <h1>
                    <asp:Label ID="lblTitle" runat="server">Monitor BMC</asp:Label>
                </h1>
            </div>
            <div class="contentmiddle">
                <div class="pageheight">
                    &nbsp;
                </div>
                <div class="gridmain">
                    <asp:GridView GridLines="none" ID="gvBMCMonitorData" ShowHeader="true" runat="server"
                        AutoGenerateColumns="false" PageSize="10" CssClass="">
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
                            <asp:TemplateField SortExpression="ParameterName">
                                <HeaderStyle Width="150px" CssClass="fll" />
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lnkParameterName" CausesValidation="False" Visible="true" runat="server"
                                        CommandName="Sort" CommandArgument="ParameterName">
                                         Parameter Name</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblParameterName" runat="server" ToolTip='<%# Eval("ParameterName") %>' 
                                         ForeColor='<%# GetCustomizedStyleForUIParameter(Eval("ParameterName").ToString(),Convert.ToDouble(Eval("CaptruredValue")),Convert.ToDouble(Eval("MinValue")),Convert.ToDouble(Eval("MaxValue"))) %>'
                                        Text='<%# Eval("ParameterName").ToString() %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:TemplateField SortExpression="CapturedValue">
                                <HeaderStyle Width="150px" CssClass="fll" />
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lnkCapturedValue" CausesValidation="False" Visible="true" runat="server"
                                        CommandName="Sort" CommandArgument="CapturedValue">
                                         CapturedValue</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCapturedValue"  ForeColor='<%# GetCustomizedStyleForUIParameter(Eval("ParameterName").ToString(),Convert.ToDouble(Eval("CaptruredValue")),Convert.ToDouble(Eval("MinValue")),Convert.ToDouble(Eval("MaxValue"))) %>'  runat="server" ToolTip='<%# Eval("CaptruredValue") %>'
                                         Text='<%# GetCustomizedParameterForUI(Eval("ParameterName").ToString(),Convert.ToDecimal(Eval("CaptruredValue"))) %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField>
                             <HeaderStyle Width="100px" CssClass="fll" />
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lnkStatus" CausesValidation="False" Visible="true" runat="server"
                                        CommandName="Sort" CommandArgument="Date">
                                         Status</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="imgStatus" runat="server" CssClass="label" ToolTip='<%# GetStatusToolTip(Convert.ToDouble(Eval("MinValue")),Convert.ToDouble(Eval("MaxValue")),Convert.ToDouble(Eval("CaptruredValue"))) %>'
                                     src='<%# GetStatusImage(Convert.ToDouble(Eval("MinValue")),Convert.ToDouble(Eval("MaxValue")),Convert.ToDouble(Eval("CaptruredValue")),Convert.ToString(Eval("PropertyType")))  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField SortExpression="Date">
                                <HeaderStyle Width="100px" CssClass="fll" />
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lnkDate" CausesValidation="False" Visible="true" runat="server"
                                        CommandName="Sort" CommandArgument="Date">
                                         Date</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" ToolTip='<%# Eval("Date") %>' Text='<%# Eval("Date").ToString() %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="contentbottom">
            </div>
        </div>
    </asp:Panel>
</asp:Content>
