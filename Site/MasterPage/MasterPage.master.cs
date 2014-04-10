using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchneiderMilkManagement;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using SchneiderMilkManagement.BusinessLayer.BusinessFacade;
using System.Web.UI.HtmlControls;

public partial class MasterPages_MasterPage : System.Web.UI.MasterPage
{
    public string PageName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            litUser.Text = Session["UserName"].ToString();
           
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "Page_Load", "MasterPage.master.cs");
        }
    }
    protected void ancLogout_click(object sender, EventArgs e)
    {
        try
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("../Login.aspx");
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "ancLogout_click", "MasterPage.master.cs");
        }
    }

    public  void DeselectAllMenuItems()
    {
        ((HtmlAnchor)this.FindControl("ancMilkProperty")).Attributes["class"] = ""; ;
        ((HtmlAnchor)this.FindControl("ancManageProperty")).Attributes["class"] = ""; ;
        ((HtmlAnchor)this.FindControl("ancManageMobile")).Attributes["class"] = ""; ;
        ((HtmlAnchor)this.FindControl("ancViewLogs")).Attributes["class"] = "";
        ((HtmlAnchor)this.FindControl("ancMonitorBMC")).Attributes["class"] = "";
        ((HtmlAnchor)this.FindControl("ancViewGraphs")).Attributes["class"] = "";
        ((HtmlAnchor)this.FindControl("ancConfigurationHistory")).Attributes["class"] = "";
        
        if ((Session["UserRole"]!=null) && (Session["UserRole"].ToString() != "Administrator"))
        {
            ((HtmlAnchor)this.FindControl("ancMilkProperty")).Visible = false;
            ((HtmlAnchor)this.FindControl("ancManageProperty")).Visible = false;
       
        }
    }

    public void SelectMenuItem(string menuItemName)
    {
        HtmlAnchor menuItem = (HtmlAnchor)this.FindControl(menuItemName);
        menuItem.Attributes["class"] = "menusel";
    }
}
