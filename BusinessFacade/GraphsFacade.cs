using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using SchneiderMilkManagement.DataLayer.DataObjects;

namespace SchneiderMilkManagement.BusinessLayer.BusinessFacade
{
    public class GraphsFacade : IFacade
    {
        public List<ProfileData> SelectDataForGraph(string ParameterName)
        {
            List<ProfileData> objProfileDataList = null;
            try
            {
                DbParam[] param = new DbParam[1];

                ParameterName = ParameterName.Replace("'", "''");
                param[0] = new DbParam("@ParameterName", ParameterName, SqlDbType.VarChar);

                var dt = Db.GetDataTable("PROC_tblProfileData_ForTrendGraphs", param);

                if (dt != null)
                {
                    objProfileDataList = new List<ProfileData>();
                    ProfileData objProfileData = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        objProfileData = new ProfileData();
                        objProfileData.ProfileId = Db.ToInteger(row["ProfileId"]);
                        objProfileData.ProfileDataId = Db.ToInteger(row["ProfileDataId"]);
                        objProfileData.ProfileName = Db.ToString(row["ProfileName"]);
                        objProfileData.ParameterName = Db.ToString(row["ParameterName"]);
                        objProfileData.CapturedValue = Db.ToDouble(row["CapturedValue"]);
                        objProfileData.MinValue = Db.ToDouble(row["MinValue"]);
                        objProfileData.MaxValue = Db.ToDouble(row["MaxValue"]);
                        objProfileData.Date = Db.ToDateTime(row["Date"]);
                        objProfileDataList.Add(objProfileData);
                    }
                }
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllByPaging", "ProfileDataDao");
                throw;
            }
            return objProfileDataList;
        }

        /// <summary>
        /// Delete The MilkProperty By MilkPropertyIds
        /// </summary>
        /// <param name="MilkPropertyIds">MilkPropertyIds</param>
        /// <returns>int</returns>
        public override int DeleteWithArray(string MilkPropertyIds)
        {
            int retValue = 0;
            
            return retValue;
        }
    }
}