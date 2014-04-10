using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using SchneiderMilkManagement.DataLayer.DataObjects;
using SchneiderMilkManagement.BusinessLayer.BusinessFacade;
using SchneiderMilkManagement;
using System.Web.UI.HtmlControls;

public partial class ManageMobile : BasePage
{
    #region ["Properties"]

    int _PropertyId = 0;
    public int PropertyId
    {
        get
        {
            if (Request.QueryString["ID"] != null)
            {
                _PropertyId = Convert.ToInt16(Request.QueryString["ID"]);
            }
            else if (ViewState["PropertyId"] != null)
            {
                _PropertyId = Convert.ToInt16(ViewState["PropertyId"]);
            }
            return _PropertyId;
        }
        set { ViewState["PropertyId"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        (this.Master as MasterPages_MasterPage).DeselectAllMenuItems();
        (this.Master as MasterPages_MasterPage).SelectMenuItem("ancManageMobile");

        
        try
        {
            if (!IsPostBack)
            {
                BindMobile();
                lblErr.Text = "";
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "Page_Load", "ManageMobile.aspx.cs");
        }
    }

    /// <summary>
    /// Page Validation
    /// </summary>
    /// <returns>bool</returns>
    private bool ValidatePage()
    {
        bool Status = false;
        string ErrorMessage = "";
        string Mobile = txtMobile.Text.Trim();
        if (Mobile == "")
        {
            ErrorMessage += " Mobile,";
        }
        else if (Mobile.Length != 10)
        {
            ErrorMessage += " Mobile,";
        }
        if (ErrorMessage != "")
        {
            Status = true;
            lblErr.Text = "The Following field(s) have invalid values : " + ErrorMessage.Substring(0, ErrorMessage.Length - 1) + ".";
        }
        return Status;
    }
    /// <summary>
    /// Save Mobile Details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkSaveDetails_Click(object sender, EventArgs e)
    {
        if (!ValidatePage())
        {
            SaveMobile();
        }
    }

    /// <summary>
    /// BindMobile
    /// </summary>
    private void BindMobile()
    {
        try
        {
            int UserId = 0;
            if (Session["UserId"] != null)
                UserId = CommonFunctions.getIntValue(Session["UserId"]);
            User objUser = new UserFacade().SelByUserId(UserId);
            if (objUser != null)
            {
                txtMobile.Text = objUser.MobileNo;
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "BindMobile", "ManageMobile.aspx.cs");
        }
    }

    /// <summary>
    /// SaveMobile
    /// </summary>
    public void SaveMobile()
    {
        try
        {
            int UserId = 0;
            if (Session["UserId"] != null)
                UserId = CommonFunctions.getIntValue(Session["UserId"]);
            User objUser = new User();
            objUser.MobileNo = txtMobile.Text.Trim();
            objUser.UserId = UserId;
            int retval = (new UserFacade().UpdateMobile(objUser));
            if (retval > 0)
            {
                lblErr.Text = "Mobile No updated successfully.";
            }
            else
            {
                lblErr.Text = "Updation Failed.";
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "SaveMobile", "ManageMobile.aspx.cs");
        }
    }   
}