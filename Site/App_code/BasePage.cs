using System;
using System.Data;
using System.Collections.Generic;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using System.Configuration;
namespace SchneiderMilkManagement
{
    public class BasePage : System.Web.UI.Page
    {
        private string _Mode = string.Empty;

        public string Mode
        {
            get
            {
                if (Context.Request.QueryString["mode"] != null)
                {
                    if (Context.Request.QueryString["mode"].ToLower() == "add")
                        _Mode = PageMode.Add.ToString();
                    else if (Context.Request.QueryString["mode"].ToLower() == "edit")
                        _Mode = PageMode.Edit.ToString();

                }
                return _Mode;
            }

        }

        public int LoginId
        {
            get
            {
                if (Session["UserId"] != null)
                {
                    return Convert.ToInt16(Convert.ToString(Session["UserId"]));
                }
                else
                {
                    return 0;
                }
            }
            set { Session["UserId"] = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }


        public BasePage()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}