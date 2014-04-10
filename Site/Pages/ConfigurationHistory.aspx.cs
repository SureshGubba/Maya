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
using System.Data;
using System.Web.UI.DataVisualization.Charting;

public partial class ConfigurationHistory : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
          
            LblError = lblErrMSG;
            if (!IsPostBack)
            {
                LoadDistinctMilkProperties();
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "OnInit", "ViewLogs.aspx.cs");
        }
        base.OnInit(e);
    }

    private void LoadDistinctMilkProperties()
    {
        var MilkPropertyBF = new MilkPropertyBF();
        var Properties=MilkPropertyBF.SelAllActive();

        cmbParameters.Items.Add(new ListItem("Select", "Select"));
        cmbParameters.Items.Add(new ListItem("All", "All"));

        foreach (var property in Properties)
        {
            cmbParameters.Items.Add(new  ListItem( property.PropertyName, property.PropertyName));
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        try
        {
            (this.Master as MasterPages_MasterPage).DeselectAllMenuItems();
            (this.Master as MasterPages_MasterPage).SelectMenuItem("ancConfigurationHistory");

        
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "OnLoad", "ViewLogs.aspx.cs");
        }
        base.OnLoad(e);
    }


    protected void cmbParameters_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbParameters.SelectedItem.Value == "Select") return;

        var selectedParameter = cmbParameters.SelectedItem.Value;

        var graphData = new GraphsFacade().SelectDataForGraph(selectedParameter);
        BindDataToGraph(graphData,selectedParameter);
    }

    private void BindDataToGraph(List<ProfileData> graphData, String selectedParameters)
    {
        if (graphData == null)
            lblErrMSG.Text = "No Data Found";

        crtParameters.DataSource = graphData;
        crtParameters.Legends.Add("QualityParameters").Title = "QualityParameters";

        var ParametersList = new List<String>();

        if(selectedParameters=="All")
        {
            foreach (MilkProperty item in new MilkPropertyBF().SelAllActive())
           {
               ParametersList.Add(item.PropertyName);
           }
        }
        else
        {
            ParametersList.Add(selectedParameters);
        }

        ParametersList.ForEach(x=>
            {
                var series = new Series { Name = x, ChartType = SeriesChartType.Line, IsValueShownAsLabel = true };
                crtParameters.Series.Add(series);
                var dataForSIngleParameter =graphData.FindAll(y=>y.ParameterName==x);
                 dataForSIngleParameter.Reverse();
                foreach (ProfileData profileData in dataForSIngleParameter )
                {
                    series.Points.AddXY(profileData.Date.ToString(), profileData.CapturedValue);
                }
            });
      
        crtParameters.ChartAreas["ChartArea1"].AxisX.Title = "Quality Parameter Captured Date";
        crtParameters.ChartAreas["ChartArea1"].AxisY.Title = "Quality Parameter Value";
        crtParameters.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        crtParameters.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = false;
        crtParameters.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
       
           
      
        crtParameters.DataBind();
    }
}


