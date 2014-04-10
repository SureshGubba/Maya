using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using SchneiderMilkManagement.DataLayer.DataObjects;
using SchneiderMilkManagement.BusinessLayer.BusinessFacade;
using SchneiderMilkManagement;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session.Clear();
                Session.Abandon();
                LoadCookie();
                form1.DefaultButton = ((LinkButton)LoginUser.FindControl("LoginButton")).UniqueID;
                form1.DefaultFocus = ((TextBox)LoginUser.FindControl("UserName")).UniqueID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ErrorLog(ex, ex.Message, "Page_Load", "Login.aspx.cs");
            }
        }
    }

    /// <summary>
    /// Check Login Details Of User
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            User objuser = new User();
            objuser = (new UserFacade()).CheckLogin(Page.Request.Form["txtUsername"].ToString(), Page.Request.Form["txtPassword"].ToString());

            if (objuser.UserId != 0)
            {
                Session["UserId"] = objuser.UserId;
                Session["UserName"] = objuser.Username;
                Session["UserRole"] = objuser.Role;

                if (LoginUser.RememberMeSet)
                {
                    SaveLoginCookie();
                }
                else
                {
                    deleteLoginCookie();
                }
                Response.Redirect("~/Pages/MonitorBMC.aspx");
            }
            else
            {
                lblErrMsg.Style.Add("display", "block");
                lblErrMsg.Text = " You have provided invalid credentials. Please try again.";
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "btnLogin_Click", "Login.aspx.cs");
        }
    }

    protected void lnkSend_OnClick(object sender, EventArgs e)
    {

    }

    #region Cookies

    //'---------------------------------------------------------------------------------
    //' Procedure Name - SaveLoginCookie
    //' Procedure Type - User Defined Function
    //' Return Type - Void
    //' Parameters - Void
    //' Description - This is the User Defined Function for Saving LoginCookoie
    //'---------------------------------------------------------------------------------
   /// <summary>
    /// Save Login Cookie
   /// </summary>
    private void SaveLoginCookie()
    {
        try
        {
            HttpCookie cookie = new HttpCookie("CMSAdmin", CommonFunctions.ToStringForInput(LoginUser.UserName));
            cookie.Expires = DateTime.Now.AddDays(14);
            Response.Cookies.Add(cookie);
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "SaveLoginCookie", "Login.aspx.cs");
        }
    }

    ////'---------------------------------------------------------------------------------
    ////' Procedure Name - DeleteLoginCookie
    ////' Procedure Type - User Defined Function
    ////' Return Type - Void
    ////' Parameters - Void
    ////' Description - This is the User Defined Function for Saving LoginCookoie
    ////'---------------------------------------------------------------------------------
  /// <summary>
    /// Delete Login Cookie
  /// </summary>
    private void deleteLoginCookie()
    {
        try
        {
            HttpCookie cookie = Request.Cookies["CMSAdmin"];
            if (cookie == null)
            {

            }
            else
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "deleteLoginCookie", "Login.aspx.cs");
        }

    }

    //'---------------------------------------------------------------------------------
    //' Procedure Name - LoadCookie
    //' Procedure Type - User Defined Function
    //' Return Type - Void
    //' Parameters - Void
    //' Description - This is the User Defined Function for Loading Cookie
    //'---------------------------------------------------------------------------------
    /// <summary>
    /// Load Cookie
    /// </summary>
    private void LoadCookie()
    {
        try
        {
            HttpCookie cookie = Request.Cookies["CMSAdmin"];
            if (Request.Cookies["CMSAdmin"] == null)
            {
                LoginUser.UserName = "";
            }
            else
            {
                LoginUser.UserName = CommonFunctions.ToStringForInput(cookie.Value.ToString());// Change
                ((TextBox)LoginUser.FindControl("UserName")).Text = CommonFunctions.ToStringForInput(cookie.Value.ToString());
                LoginUser.RememberMeSet = true;
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "LoadCookie", "Login.aspx.cs");
        }
    }

    /// <summary>
    /// Authenticate The User
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
    {
        Loginuser();
    }

    /// <summary>
    /// Check Login
    /// </summary>
    private void Loginuser()
    {
        User objuser = new User();
        objuser = (new UserFacade()).CheckLogin(LoginUser.UserName, LoginUser.Password);

        if (objuser != null && objuser.UserId != 0)
        {
            Session["UserId"] = objuser.UserId;
            Session["UserName"] = objuser.Username;
            Session["UserRole"] = objuser.Role;

            if (LoginUser.RememberMeSet)
            {
                SaveLoginCookie();
            }
            else
            {
                deleteLoginCookie();
            }
            Response.Redirect("~/Pages/MonitorBMC.aspx");
        }
        else
        {
            lblErrMsg.Style.Add("display", "block");
            lblErrMsg.Text = " You have provided invalid credentials. Please try again.";
        }
    }
    #endregion
}