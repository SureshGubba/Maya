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

public partial class Pages_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            (this.Master as MasterPages_MasterPage).DeselectAllMenuItems();
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "Page_Load", "ChangePassword.aspx.cs");
        }
    }

    /// <summary>
    /// Page validation
    /// </summary>
    /// <returns></returns>
    private bool ValidatePage()
    {
        bool Status = false;
        string ErrorMessage = "";
        string OldPassword = txtOldPwd.Text.Trim();
        string NewPassword = txtNewPwd.Text.Trim();
        string ConfirmPassword = txtCPwd.Text.Trim();
        if (OldPassword == "")
        {
            ErrorMessage += " Old Password,";
        }
        if (NewPassword == "")
        {
            ErrorMessage += " New Password,";
        }
        if (ConfirmPassword == "")
        {
            ErrorMessage += " Confirm Password,";
        }
        if (ErrorMessage != "")
        {
            Status = true;
            lblErr.Text = "The Following field(s) have invalid values : " + ErrorMessage.Substring(0, ErrorMessage.Length - 1) + ".";
        }
        else
        {
            if (NewPassword != ConfirmPassword)
            {
                Status = true;
                lblErr.Text = " New Password and Confirm Password should match.";
            }
        }
        return Status;
    }
    /// <summary>
    /// Upadate The Password
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkSaveDetails_Click(object sender, EventArgs e)
    {
        if (!ValidatePage())
        {
            try
            {
                String UserName = CommonFunctions.GetStringValue(Session["UserName"]);
                String OldPwd = txtOldPwd.Text.Trim();
                String CPwd = txtCPwd.Text.Trim();
                int retVal = (new UserFacade()).ChangePassword(UserName, OldPwd, CPwd);
                if (retVal == 1)
                    lblErr.Text = "Successfully Updated";
                else if (retVal == -1)
                    lblErr.Text = "Invalid Old Password";
                else if (retVal == -2)
                    lblErr.Text = "Old Password and New Password should not be same";
                else
                    lblErr.Text = "Error while updating";
            }
            catch (Exception ex)
            {
                CommonFunctions.ErrorLog(ex, ex.Message, "lnkSaveDetails_Click", "ChangePassword.aspx.cs");
            }
        }
    }
}