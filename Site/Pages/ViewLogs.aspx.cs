using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using SchneiderMilkManagement.DataLayer.DataObjects;
using SchneiderMilkManagement.BusinessLayer.BusinessFacade;
using SchneiderMilkManagement;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Configuration;
using MyControls;
using System.Diagnostics;
using System.IO;

using System.Text;
using System.Data;
using System.Net;

public partial class ViewLogs : BaseGridPage
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
            GvItems = gvProfileData;
            OdsItems = odsProfileData;
            LnkCheckAll = lnkCheckAll;
            LnkClearAll = lnkClearAll;
            LnkDeleteChecked = lnkDelete;
            SpanGridOptions = spanGridOptions;
            LblError = lblErrMSG;
            ObjIFacade = new ProfileDataFacade();
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "OnInit", "ViewLogs.aspx.cs");
        }
        base.OnInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
        try
        {
            (this.Master as MasterPages_MasterPage).DeselectAllMenuItems();
            (this.Master as MasterPages_MasterPage).SelectMenuItem("ancViewLogs");

        
            if (!IsPostBack)
            {
                SortOrder = "ASC";
                SortColumn = "Name";
                SearchString = " 1=1 ";
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "OnLoad", "ViewLogs.aspx.cs");
        }
        base.OnLoad(e);
    }

    public String GetStatusToolTip(double MinValue, double MaxValue, double CapturedValue)
    {
        return String.Format("Min:{0} Max:{1}", MinValue, MaxValue); ;
    }

    public String GetCustomizedParameterForUI(string parameterName, decimal CapturedValue)
    {
        string UIValue = (CapturedValue != null) ? CapturedValue.ToString() : String.Empty;

        if (parameterName.Trim().Equals("Level(cm)", StringComparison.InvariantCultureIgnoreCase))
        {
            UIValue = String.Format("{0} Liters ({1} cms)", new BMCMonitorDao().GetCapacityinLitersFromMM(CapturedValue).CapacityInLiters, CapturedValue);
        }

        if (parameterName.Trim().Equals("LidStatus", StringComparison.InvariantCultureIgnoreCase))
        {
            if (CapturedValue == 1)
                UIValue = String.Format("{0}", "Closed");

            if (CapturedValue == 0)
                UIValue = String.Format("{0}", "Open");
        }

        return UIValue;
    }

    public System.Drawing.Color GetCustomizedStyleForUIParameter(string parameterName, double CapturedValue, double MinValue, double MaxValue)
    {
        System.Drawing.Color style = System.Drawing.Color.Black;

        if (CapturedValue < MinValue || CapturedValue > MaxValue)
            style = System.Drawing.Color.Red;

        if (parameterName.Trim().Equals("Level(cm)", StringComparison.InvariantCultureIgnoreCase))
        {
            var isInsideRange = new BMCMonitorDao().GetCapacityinLitersFromMM(Convert.ToDecimal(CapturedValue)).IsInrange;
            if (!isInsideRange)
            {
                style = System.Drawing.Color.Red;
            }

        }
        return style;

    }

    public string GetCustomizedMaxValue(string paramname,double maxvalue)
    {
        if (!paramname.Equals("LidStatus", StringComparison.InvariantCultureIgnoreCase))
        {
            return maxvalue.ToString();
        }

        return string.Empty;

    }

    public string GetCustomizedMinValue(string paramname, double minvalue)
    {
        if (!paramname.Equals("LidStatus", StringComparison.InvariantCultureIgnoreCase))
        {
            return minvalue.ToString();
        }

        return string.Empty;
    }

    public String GetStatusImage(double MinValue, double MaxValue, double CapturedValue,string ParameterName)
    {

        string BasePAth = "../App_Themes/Default/Images/";
        string OkImage = BasePAth + "OK_Grid.png";
        String AlertImage = BasePAth + "Alert_Grid.png";

        string statusImage = OkImage;
        
        string dataType=string.Empty;
        
        if (ParameterName.Equals("LidStatus",StringComparison.InvariantCultureIgnoreCase))
        {
           if(CapturedValue< 0 || CapturedValue > 1)
               statusImage = AlertImage;

           if (CapturedValue == 0)
               statusImage = AlertImage;
        }        
        else
        {
            if (CapturedValue < MinValue || CapturedValue > MaxValue)
                statusImage = AlertImage;
        }

        return statusImage;
    }

    /// <summary>
    /// Delete The MilkProfile Data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeleteMilkProfileData(object sender, CommandEventArgs e)
    {
        try
        {
            int returnValue = 0;
            if (e.CommandName == "del")
            {
                returnValue = new ProfileDataFacade().DeleteWithArray(Convert.ToString(e.CommandArgument));
                if (returnValue > 0)
                {
                    lblErrMSG.Text = "Selected item(s) has been deleted successfully.";
                    gvProfileData.DataBind();
                }
                else
                {
                    lblErrMSG.Text = "Error while deleting Milk ProfileData.";
                }
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "DeleteMilkProfileData", "ViewLogs.aspx.cs");
        }
    }

    /// <summary>
    /// Search By ParameterName
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            SearchString = "";
            if (txtTitle.Text.Length > 0)
                SearchString = " Or P.Name like '" + txtTitle.Text.Trim().Replace("'", "''") + "%' Or ParameterName like '" + txtTitle.Text.Trim().Replace("'", "''") + "%'";
            if (SearchString.Length == 0)
                SearchString = " Or 1=1 ";

            hidSearchString.Value = SearchString.Substring(3);
            gvProfileData.DataBind();
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "btnSearch_Click", "ViewLogs.aspx.cs");
        }
    }

    private Boolean CheckValidations()
    {
        DateTime validate;
        Boolean isValid = true;
        if(!DateTime.TryParse(txtFromDate.Text,out validate))
        {
            lblErrMSG.Text = "From Date is not in a valid format";
            isValid = false;
            return isValid;
        }

        if (!DateTime.TryParse(txtToDate.Text, out validate))
        {
            lblErrMSG.Text = "To Date is not in a valid format";
            isValid = false;
            return isValid;
        }

        if (DateTime.Parse(txtFromDate.Text) > DateTime.Parse(txtToDate.Text))
        {
            lblErrMSG.Text = "FromDate should be less than ToDate";
            isValid = false;
            return isValid;
        }

        return isValid;
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
     
        if (!CheckValidations())
            return;
      
        try
        {
           
            var excelData = new ProfileDataFacade().SelExcelData(txtFromDate.Text, txtToDate.Text);

            if (excelData.Rows.Count == 0)
            {
                lblErrMSG.Text = "There is no data to export between the given dates";
                return;
            }

            lblErrMSG.Text = "Exporting data to excel.Please wait....";
            var fileName = "ExportToExcel_"+Guid.NewGuid() + ".xlsx";
            var filePath = Server.MapPath(@"~/"+"/TempReports/") + fileName;
            SpreadsheetService spreadobj = new SpreadsheetService();
            spreadobj.CreateSpreadsheetWorkbook(filePath, excelData, txtFromDate.Text, txtToDate.Text);
            var openerFile = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + ResolveUrl("~/") + ("/TempReports/") + fileName;
           lblErrMSG.Text = "Finished exporting data to excel.";
           
           ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Download", "GotoDownloadPage('"+openerFile+"');", true);
          
        }
        catch (Exception ex)
        {
            
            Response.AddHeader("Refresh", "3; url=ViewLogs.aspx");
            CommonFunctions.ErrorLog(ex, ex.Message, "btnSearch_Click", "ViewLogs.aspx.cs");
            throw;
        }
    }

    protected void UploadDataTableToExcel(DataTable excelData, string filename)
    {
        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = string.Empty;
        foreach (DataColumn dtcol in excelData.Columns)
        {
            Response.Write(tab + dtcol.ColumnName);
            tab = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in excelData.Rows)
        {
            tab = "";
            for (int j = 0; j < excelData.Columns.Count; j++)
            {
                Response.Write(tab + Convert.ToString(dr[j]));
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
}


