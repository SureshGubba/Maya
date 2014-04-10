using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Services;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using SchneiderMilkManagement.BusinessLayer.BusinessFacade;
using SchneiderMilkManagement.Services.DataContract.DataContractObjects;
using SchneiderMilkManagement.Services.DataContract.DataContractMapper;
using SchneiderMilkManagement.Services.Response;
using SchneiderMilkManagement.Services.ResponseBase;
using SchneiderMilkManagement;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    public WebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// Get MilkProperties By LastSyncDate
    /// </summary>
    /// <param name="LastSyncDate">LastSyncDate</param>
    /// <returns>MilkPropertyResponse</returns>
    [WebMethod]
    public MilkPropertyResponse GetMilkProperties(string LastSyncDate = "")
    {
        MilkPropertyResponse objMilkPropertyResponse = new MilkPropertyResponse();
        try
        {
            DateTime ServerTime;
            if (LastSyncDate == "")
            {
                ServerTime = Convert.ToDateTime("1990/1/1 00:00");
            }
            else
            {
                ServerTime = Convert.ToDateTime(LastSyncDate);
            }            
            IList<MilkProperty> objMilkProperties = new MilkPropertyBF().SelAllActiveByLastSyncDate(ServerTime);

            if (objMilkProperties != null)
            {
                User objUser = new UserFacade().SelByUserId(1);
                objMilkPropertyResponse.objMilkProperties = Mapper.ToDataContractObjects(objMilkProperties);
                objMilkPropertyResponse.Count = objMilkProperties.Count;
                objMilkPropertyResponse.Message = "success";
                objMilkPropertyResponse.Status = Status.Success;
                objMilkPropertyResponse.ServerTime = DateTime.Now.ToString();
                if (objUser != null)
                    objMilkPropertyResponse.MobileNo = objUser.MobileNo;
            }
            else
            {
                objMilkPropertyResponse.Count = 0;
                objMilkPropertyResponse.Message = "failure";
                objMilkPropertyResponse.Status = Status.Failure;
                objMilkPropertyResponse.ServerTime = DateTime.Now.ToString();
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "GetMilkProperties", "WebService.cs");
        }
        return objMilkPropertyResponse;
    }

    /// <summary>
    /// Insert Profile Data
    /// </summary>
    /// <param name="Profiles">Profiles</param>
    /// <returns>InsertProfileDataResponse</returns>
    [WebMethod]
    public InsertProfileDataResponse InsertProfileData(Profiles Profiles)
    {
        InsertProfileDataResponse objInsertProfileDataResponse = new InsertProfileDataResponse();
        try
        {            
            bool result = new ProfileDataFacade().InsertProfile(Mapper.FromDataContractObjects(Profiles.Profile));
            if (result == true)
            {
                objInsertProfileDataResponse.Message = "success";
                objInsertProfileDataResponse.Status = Status.Success;
                objInsertProfileDataResponse.ServerTime = DateTime.Now.ToString();
            }
            else
            {
                objInsertProfileDataResponse.Message = "failure";
                objInsertProfileDataResponse.Status = Status.Failure;
                objInsertProfileDataResponse.ServerTime = DateTime.Now.ToString();
            }
        }
        catch (Exception ex)
        {
            CommonFunctions.ErrorLog(ex, ex.Message, "InsertProfileData", "WebService.cs");
        }
        return objInsertProfileDataResponse;
    }
}