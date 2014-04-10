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

public partial class AddEditProperty : BasePage
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
        try
        {
            (this.Master as MasterPages_MasterPage).DeselectAllMenuItems();
            (this.Master as MasterPages_MasterPage).SelectMenuItem("ancMilkProperty");

            if (!IsPostBack)
            {
                BindProfile();
                lblErr.Text = "";
                if (Mode == "Add")
                {
                    lblTitle.Text = "Add Property";
                }
                else if (Mode == PageMode.Edit.ToString())
                {
                    BindPropertyDetails();
                    ddlProfileType.Enabled = false;
                    lblTitle.Text = "Edit Property";
                }               
            }

        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "Page_Load", "AddEditProperty.aspx.cs");
        }
    }

    /// <summary>
    /// Page validation
    /// </summary>
    /// <returns>bool</returns>
    private bool ValidatePage()
    {
        bool Status = false;
        string ErrorMessage = "";
        string ProfileType = ddlProfileType.SelectedValue;
        string Name = txtName.Text.Trim();       
        if (ProfileType == "-1")
        {
            ErrorMessage += " Profile Type,";
        }
        if (Name == "")
        {
            ErrorMessage += " Name,";
        }        
        if (ErrorMessage != "")
        {
            Status = true;
            lblErr.Text = "Please enter these values : " + ErrorMessage.Substring(0, ErrorMessage.Length - 1) + ".";
        }        
        return Status;
    }
    /// <summary>
    /// Bind Property Details
    /// </summary>
    void BindPropertyDetails()
    {
        try
        {
            Property objProperty = new Property();
            objProperty = (new PropertyFacade()).SelById(PropertyId);  
            txtName.Text = CommonFunctions.GetStringValue(objProperty.Name);
            ddlProfileType.SelectedValue = CommonFunctions.GetStringValue( objProperty.ProfileId);
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "BindPropertyDetails", "AddEditProperty.aspx.cs");
        }
    }

    /// <summary>
    /// SaveProperty
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkSaveDetails_Click(object sender, EventArgs e)
    {
        if (!ValidatePage())
        {
            SaveProperty();
        }
    }

    /// <summary>
    /// BindProfile
    /// </summary>
    private void BindProfile()
    {
        try
        {
            IList<Profile> objProfileList = new PropertyFacade().SelAllProfileForDropDown();
            if (objProfileList != null)
            {
                ddlProfileType.DataSource = objProfileList;
                ddlProfileType.DataTextField = "Name";
                ddlProfileType.DataValueField = "ProfileId";
                ddlProfileType.DataBind();
                ddlProfileType.Items.Insert(0, new ListItem("-Select-", "-1"));
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "BindProfile", "AddEditProperty.aspx.cs");
        }
    }

    /// <summary>
    /// SaveProperty
    /// </summary>
    public void SaveProperty()
    {
        try
        {            
            Property objProperty = new Property();
            objProperty.CreatedBy = 1;
            objProperty.ModifiedBy = 1;
            objProperty.Name = txtName.Text.Trim();
            objProperty.ProfileId = CommonFunctions.getIntValue( ddlProfileType.SelectedValue);

            if (Mode == PageMode.Add.ToString())
            {
                int retval = (new PropertyFacade().InsertProperty(objProperty));
                if (retval > 0)
                {
                    pnlForm.Visible = false;
                    pnlConfirmation.Visible = true;
                    ancSaveDetails.HRef = "AddEditProperty.aspx?mode=Edit&ID=" + PropertyId;
                    lblConfirmationMessage.Text = "Property added successfully.";
                }
                else
                {
                    lblErr.Text = "Property Name already exists.";
                }
            }
            else
            {
                objProperty.PropertyId = PropertyId;
                objProperty.ModifiedBy = LoginId;
                int retval = (new PropertyFacade().UpdateProperty(objProperty));
                if (retval > 0)
                {
                    pnlForm.Visible = false;
                    pnlConfirmation.Visible = true;
                    ancSaveDetails.HRef = "AddEditProperty.aspx?mode=Edit&ID=" + PropertyId;
                    lblConfirmationMessage.Text = "Property updated successfully.";
                }
                else
                {
                    lblErr.Text = "Property Name already exists.";
                }
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "SaveProperty", "AddEditProperty.aspx.cs");
        }

    }

    /// <summary>
    /// Edit The Property
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("AddEditProperty.aspx?Mode=Edit&ID=" + Convert.ToString(PropertyId));
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "btnEdit_Click", "AddEditProperty.aspx.cs");
        }
    }
}