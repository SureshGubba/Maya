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

public partial class AddEditMilkProperty : BasePage
{
    #region ["Properties"]

    int _MilkPropertyId = 0;
    public int MilkPropertyId
    {
        get
        {
            if (Request.QueryString["ID"] != null)
            {
                _MilkPropertyId = Convert.ToInt16(Request.QueryString["ID"]);
            }
            else if (ViewState["MilkPropertyId"] != null)
            {
                _MilkPropertyId = Convert.ToInt16(ViewState["MilkPropertyId"]);
            }
            return _MilkPropertyId;
        }
        set { ViewState["MilkPropertyId"] = value; }
    }

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ddlPropertyName.Items.Insert(0, new ListItem("-Select-", "-1"));
                BindProfile();             
                ddlPropertyName.Focus();
                lblErr.Text = "";
                if (Mode == "Add")
                {
                    chkIsActive.Checked = true;
                    lblTitle.Text = "Add Milk Property";
                }
                else if (Mode == PageMode.Edit.ToString())
                {
                    BindMilkPropertyDetails();
                    lblTitle.Text = "Edit Milk Property";
                }
            }

             if (Mode == PageMode.Edit.ToString())
            {
                ddlProfileType.Enabled = false;
                ddlPropertyName.Enabled = false;
            }
        }
        catch(Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "Page_Load", "AddEditMilkProperty.aspx.cs"); 
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
        string ProfileType = ddlProfileType.SelectedValue;
        string PropertyName = ddlPropertyName.SelectedValue;
        string PropertyType = ddlPropertyType.SelectedValue;
        string Unit = txtUnit.Text.Trim();
        string MinValue = txtMinvalue.Text.Trim();
        string MaxValue = txtMaxValue.Text.Trim();
        string PollingTime = txtPollingTime.Text.Trim();
        string SmsPollingTime = txtSmsPollingTime.Text.Trim();
        if(ProfileType == "-1")
        {
            ErrorMessage += " Profile Type,";
        }
        if(PropertyName == "-1")
        {
            ErrorMessage += " Property Name,";
        }
        if(PropertyType == "-1")
        {
            ErrorMessage += " Property Type,";
        }
        if(Unit == "")
        {
             ErrorMessage += " Unit,";
        }
        if(MinValue == "")
        {
             ErrorMessage += " Min Value,";
        }
        if(MaxValue == "")
        {
             ErrorMessage += " Max Value,";
        }
        if(PollingTime == "")
        {
             ErrorMessage += " Polling Time,";
        }
        if(SmsPollingTime == "")
        {
             ErrorMessage += " SMS Polling Time,";
        }
        if(ErrorMessage != "")
        {
            Status = true;
            lblErr.Text = "Please enter these values : " + ErrorMessage.Substring(0,ErrorMessage.Length - 1) + ".";
        }
        else
        {
            if(CommonFunctions.getIntValue( MinValue) > CommonFunctions.getIntValue( MaxValue) )
            {
                Status = true;
                lblErr.Text = "Min value must be less than max value.";
            }
        }
        return Status;
    }
    /// <summary>
    /// Bind MilkProperty Details
    /// </summary>
    void BindMilkPropertyDetails()
    {
        try
        {
            MilkProperty objMilkProperty = new MilkProperty();
            objMilkProperty = (new MilkPropertyBF()).SelById(MilkPropertyId);            
            BindProperty();
            if (ddlPropertyName.Items.FindByValue(CommonFunctions.GetStringValue( objMilkProperty.PropertyId)) != null)
                ddlPropertyName.Items.FindByValue(CommonFunctions.GetStringValue(objMilkProperty.PropertyId)).Selected = true;
            if (ddlProfileType.Items.FindByValue(CommonFunctions.GetStringValue(objMilkProperty.ProfileId)) != null)
                ddlProfileType.Items.FindByValue(CommonFunctions.GetStringValue(objMilkProperty.ProfileId)).Selected = true;
            if (ddlPropertyType.Items.FindByValue(objMilkProperty.PropertyType) != null)
                ddlPropertyType.Items.FindByValue(objMilkProperty.PropertyType).Selected = true;    
            chkIsActive.Checked = objMilkProperty.IsActive;
            chkAllowDelete.Checked = objMilkProperty.AllowDelete;
            chkIsSMSRequired.Checked = objMilkProperty.IsSMSRequired;
            txtMaxValue.Text = CommonFunctions.GetStringValue(objMilkProperty.MaxValue);
            txtMinvalue.Text = CommonFunctions.GetStringValue(objMilkProperty.MinValue);
            txtPollingTime.Text = CommonFunctions.GetStringValue(objMilkProperty.PollingTime);
            txtSmsPollingTime.Text = CommonFunctions.GetStringValue(objMilkProperty.SmsPollingTime);      
            txtUnit.Text = objMilkProperty.Unit;
            txtPortAddress.Text = objMilkProperty.PortAddress;
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "BindMilkPropertyDetails", "AddEditMilkProperty.aspx.cs"); 
        }
    }

    /// <summary>
    /// Bind Profile
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
            CommonFunctions.ErrorLog(ex, ex.Message, "BindProfile", "AddEditMilkProperty.aspx.cs"); 
        }
    }

    /// <summary>
    /// Bind Property
    /// </summary>
    private void BindProperty()
    {
        try
        {
            IList<Property> objPropertyList = new PropertyFacade().SelAll();
            if (objPropertyList != null)
            {
                ddlPropertyName.DataSource = objPropertyList;
                ddlPropertyName.DataTextField = "Name";
                ddlPropertyName.DataValueField = "PropertyId";
                ddlPropertyName.DataBind();
                ddlPropertyName.Items.Insert(0, new ListItem("-Select-", "-1"));
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "BindProperty", "AddEditMilkProperty.aspx.cs");
        }
    }

    /// <summary>
    /// Bind Property OnChange
    /// </summary>
    private void BindPropertyOnChange()
    {
        try
        {
            IList<Property> objPropertyList = new PropertyFacade().SelAllByProfile(CommonFunctions.getIntValue(ddlProfileType.SelectedValue));
            if (objPropertyList != null)
            {
                ddlPropertyName.DataSource = objPropertyList;
                ddlPropertyName.DataTextField = "Name";
                ddlPropertyName.DataValueField = "PropertyId";
                ddlPropertyName.DataBind();
                ddlPropertyName.Items.Insert(0, new ListItem("-Select-", "-1"));
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "BindPropertyOnChange", "AddEditMilkProperty.aspx.cs");
        }
    }

    /// <summary>
    /// Bind Assigned Property
    /// </summary>
    private void BindPropertyAssigned()
    {
        try
        {
            IList<Property> objPropertyList = new PropertyFacade().SelAllAssignedPropertyByProfile(CommonFunctions.getIntValue(ddlProfileType.SelectedValue));
            if (objPropertyList != null)
            {
                ddlPropertyName.DataSource = objPropertyList;
                ddlPropertyName.DataTextField = "Name";
                ddlPropertyName.DataValueField = "PropertyId";
                ddlPropertyName.DataBind();
                ddlPropertyName.Items.Insert(0, new ListItem("-Select-", "-1"));
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "BindPropertyAssigned", "AddEditMilkProperty.aspx.cs");
        }
    }

    /// <summary>
    /// Bind Property OnChange ProfileType 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlProfileType_Selection(object sender, EventArgs e)
    {
        try
        {
            ddlPropertyName.Items.Clear();
            if (ddlProfileType.SelectedIndex > 0)
            {
                BindPropertyOnChange();
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "ddlProfileType_Selection", "AddEditMilkProperty.aspx.cs");
        }
    }
    /// <summary>
    /// SaveMilkProperty
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkSaveDetails_Click(object sender, EventArgs e)
    {
        if (!ValidatePage())
        {
            SaveMilkProperty();
        }
    }

    /// <summary>
    /// SaveMilkProperty
    /// </summary>
    public void SaveMilkProperty()
    {
        try
        {            
            MilkProperty objMilkProperty = new MilkProperty();
            objMilkProperty.CreatedBy = 1;
            objMilkProperty.ModifiedBy = 1;
            objMilkProperty.PropertyId = CommonFunctions.getIntValue( ddlPropertyName.SelectedValue);
            objMilkProperty.ProfileId = CommonFunctions.getIntValue( ddlProfileType.SelectedValue);
            objMilkProperty.ProfileType = ddlProfileType.SelectedValue;
            objMilkProperty.PropertyType = ddlPropertyType.SelectedValue;
            objMilkProperty.PropertyName = ddlPropertyName.SelectedValue;
            objMilkProperty.MinValue = CommonFunctions.getDecimalValue(txtMinvalue.Text.Trim());
            objMilkProperty.MaxValue = CommonFunctions.getDecimalValue(txtMaxValue.Text.Trim());
            objMilkProperty.Address = "";
            objMilkProperty.PollingTime = CommonFunctions.getDoubleValue(txtPollingTime.Text.Trim());
            objMilkProperty.SmsPollingTime = CommonFunctions.getIntValue(txtSmsPollingTime.Text.Trim());
            objMilkProperty.Unit = txtUnit.Text.Trim();
            objMilkProperty.IsActive = chkIsActive.Checked;
            objMilkProperty.AllowDelete = chkAllowDelete.Checked;
            objMilkProperty.IsSMSRequired = chkIsSMSRequired.Checked;
            if (txtPortAddress.Text.Trim() != "")
                objMilkProperty.PortAddress = txtPortAddress.Text.Trim();
            else
                objMilkProperty.PortAddress = string.Empty;

            if (Mode == PageMode.Add.ToString())
            {
                int retval = (new MilkPropertyBF().InsertMilkProperty(objMilkProperty));
                if (retval > 0)
                {
                    pnlForm.Visible = false;
                    pnlConfirmation.Visible = true;
                    ancSaveDetails.HRef = "AddEditMilkProperty.aspx?mode=Edit&ID=" + MilkPropertyId;
                    lblConfirmationMessage.Text = "Milk property added successfully.";
                }
                else
                {
                    lblErr.Text = "Property Name already exists with the Profile type";
                }
            }
            else
            {
                objMilkProperty.MilkPropertyId = MilkPropertyId;
                objMilkProperty.ModifiedBy = LoginId;
                int retval = (new MilkPropertyBF().UpdateMilkProperty(objMilkProperty));
                if (retval > 0)
                {
                    pnlForm.Visible = false;
                    pnlConfirmation.Visible = true;
                    ancSaveDetails.HRef = "AddEditMilkProperty.aspx?mode=Edit&ID=" + MilkPropertyId;
                    lblConfirmationMessage.Text = "Milk property updated successfully.";
                }
                else
                {
                    lblErr.Text = "Property Name already exists with the Profile type";
                }
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "SaveMilkProperty", "AddEditMilkProperty.aspx.cs");
        }
    }
    /// <summary>
    /// Edit The MilkProperty
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("AddEditMilkProperty.aspx?Mode=Edit&ID=" + Convert.ToString(MilkPropertyId));
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "btnEdit_Click", "AddEditMilkProperty.aspx.cs");
        }
    }
}