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

public partial class ManageMilkProperty : BaseGridPage
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
            (this.Master as MasterPages_MasterPage).DeselectAllMenuItems();
            (this.Master as MasterPages_MasterPage).SelectMenuItem("ancMilkProperty");

            GvItems = gvMilkPrperties;
            OdsItems = odsMilkPrperties;
            LnkCheckAll = lnkCheckAll;
            LnkClearAll = lnkClearAll;
            LnkDeleteChecked = lnkDelete;
            SpanGridOptions = spanGridOptions;
            LblError = lblErrMSG;
            ObjIFacade = new MilkPropertyBF();
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "OnInit", "ManageMilkProperty.aspx.cs");
        }
        base.OnInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                SortOrder = "ASC";
                SortColumn = "PropertyName";
                SearchString = " 1=1 ";
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "OnLoad", "ManageMilkProperty.aspx.cs");
        }
        base.OnLoad(e);
    }

    /// <summary>
    /// Delete The MilkProperty
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeleteMilkProperty(object sender, CommandEventArgs e)
    {
        try
        {
            int returnValue = 0;
            if (e.CommandName == "del")
            {
                returnValue = new MilkPropertyBF().DeleteWithArray(Convert.ToString(e.CommandArgument));
                if (returnValue > 0)
                {
                    lblErrMSG.Text = "Selected item(s) has been deleted successfully.";
                    gvMilkPrperties.DataBind();
                }
                else
                {
                    lblErrMSG.Text = "Error while deleting Milk Property.";
                }
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "DeleteMilkProperty", "ManageMilkProperty.aspx.cs");
        }
    }

    /// <summary>
    /// Search The PropertyName
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            SearchString = "";
            if (txtTitle.Text.Length > 0)
                SearchString = " Or PropertyName like '" + txtTitle.Text.Trim().Replace("'", "''") + "%' Or ProfileType like '" + txtTitle.Text.Trim().Replace("'", "''") + "%'";
            if (SearchString.Length == 0)
                SearchString = " Or 1=1 ";

            hidSearchString.Value = SearchString.Substring(3);
            gvMilkPrperties.DataBind();
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "btnSearch_Click", "ManageMilkProperty.aspx.cs");
        }
    }
}


