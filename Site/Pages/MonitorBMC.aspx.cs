using System;
using System.Collections.Generic;
using System.Drawing;
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

public partial class MonitorBMC : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            (this.Master as MasterPages_MasterPage).DeselectAllMenuItems();
            (this.Master as MasterPages_MasterPage).SelectMenuItem("ancMonitorBMC");
            LoadBMCMonitorData();
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "Page_Load", "MonitorBMC.aspx.cs");
        }
    }

    private void LoadBMCMonitorData()
    {
        var data = new BMCMonitorDao().SelectCurrentProfileParametersOfBMC();
        gvBMCMonitorData.DataSource = data.OrderBy(x => x.UIOrder);
        gvBMCMonitorData.DataBind();
    }

    public String GetStatusImage(double MinValue, double MaxValue, double CapturedValue, string dataType)
{

    string BasePAth = "../App_Themes/Default/Images/";
    string OkImage = BasePAth + "OK_Grid.png";
    String AlertImage = BasePAth + "Alert_Grid.png";

    string statusImage = OkImage;


    if (dataType.Equals("Boolean", StringComparison.InvariantCultureIgnoreCase))
    {
        if (CapturedValue < 0 || CapturedValue > 1)
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

  public String GetStatusToolTip(double MinValue, double MaxValue, double CapturedValue)
  {
      return String.Format("Min:{0} Max:{1}",MinValue,MaxValue); ;
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
          if(CapturedValue  ==1)
          UIValue = String.Format("{0}", "Closed");

          if(CapturedValue  ==0)
          UIValue = String.Format("{0}", "Open");
      }
     
      return UIValue;
  }

  public System.Drawing.Color GetCustomizedStyleForUIParameter(string parameterName, double CapturedValue,double MinValue,double MaxValue)
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

}