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

public partial class ManageProperty : BaseGridPage
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
            (this.Master as MasterPages_MasterPage).DeselectAllMenuItems();
            (this.Master as MasterPages_MasterPage).SelectMenuItem("ancManageProperty");

            GvItems = gvProperty;
            OdsItems = odsPrperty;
            LnkCheckAll = lnkCheckAll;
            LnkClearAll = lnkClearAll;
            LnkDeleteChecked = lnkDelete;
            SpanGridOptions = spanGridOptions;
            LblError = lblErrMSG;
            ObjIFacade = new PropertyFacade();
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "OnInit", "ManageProperty.aspx.cs");
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
                SortColumn = "Name";
                SearchString = " 1=1 ";
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "OnLoad", "ManageProperty.aspx.cs");
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
                returnValue = new PropertyFacade().DeleteWithArray(Convert.ToString(e.CommandArgument));
                if (returnValue > 0)
                {
                    lblErrMSG.Text = "Selected item(s) has been deleted successfully.";
                    gvProperty.DataBind();
                }
                else
                {
                    lblErrMSG.Text = "Error while deleting Milk Property.";
                }
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "DeleteMilkProperty", "ManageProperty.aspx.cs");
        }
    }

    /// <summary>
    /// Search By ProfileName
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            SearchString = "";
            if (txtTitle.Text.Length > 0)
                SearchString = " Or Name like '" + txtTitle.Text.Trim().Replace("'", "''") + "%' Or ProfileName like '" + txtTitle.Text.Trim().Replace("'", "''") + "%'";
            if (SearchString.Length == 0)
                SearchString = " Or 1=1 ";

            hidSearchString.Value = SearchString.Substring(3);
            gvProperty.DataBind();
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "btnSearch_Click", "ManageProperty.aspx.cs");
        }
    }
}


