<%@ Page Title=" View Logs" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="true" CodeFile="ConfigurationHistory.aspx.cs" Inherits="ConfigurationHistory" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../App_Themes/Default/CSS/popup.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnl" runat="server">
        <asp:UpdatePanel ID="udpCountry" runat="server">
            <ContentTemplate>
                <div class="contentmain">
                    <div class="contenttop">
                        <h1>Trend Graphs
                        </h1>
                    </div>
                    <div class="contentmiddle">
                        <div class="innermain">
                            <div class="breadcrum" style="display: none;">
                                <a id="lnkDashboard" href="dashboard.aspx">Dashboard</a> &#187; Trend Graphs
                            </div>
                            <div class="gridconatiner">
                                <div class="errormgs">
                                    <asp:Label ID="lblErrMSG" runat="server" Text="" ForeColor="Red" Font-Size="12"></asp:Label>
                                    <asp:Label ID="LblError" runat="server" Text="" ForeColor="Red">&nbsp;</asp:Label>
                                </div>
                                <div>
                                    <div style="width: 316px">
                                        <label>Select Parameter</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="cmbParameters" OnSelectedIndexChanged="cmbParameters_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="gridmain">
                                        <asp:Chart ID="crtParameters" runat="server" Height="493px" Width="930px">
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1">                                                    
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="contentbottom">
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <script src="../js/CommonFunctions.js" type="text/javascript"></script>
    <script type="text/javascript">
       
    </script>
</asp:Content>
