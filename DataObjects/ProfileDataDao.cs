using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;

namespace SchneiderMilkManagement.DataLayer.DataObjects
{
    public class ProfileDataDao
    {
        #region [Member parameters]

        IList<MilkProperty> objMilkPropertys;
        
        MilkProperty objMilkProperty;
        DataTable dt;
        DataRow dr;
        int intReturn;

        #endregion

        #region [Select Methods]
       
        /// <summary>
        /// Select All Profile Data By Paging
        /// </summary>
        /// <param name="SortBy">SortBy</param>
        /// <param name="SearchString">SearchString</param>
        /// <param name="maximumRows">maximumRows</param>
        /// <param name="startRowIndex">startRowIndex</param>
        /// <returns>IList<ProfileData></returns>
        public IList<ProfileData> SelAllByPaging(string SortBy, string SearchString, int maximumRows, int startRowIndex)
        {
            IList<ProfileData> objProfileDataList = null;
            try
            {
                DbParam[] param = new DbParam[4];

                param[0] = new DbParam("@SortBy", SortBy, SqlDbType.VarChar);
                param[1] = new DbParam("@SearchString", SearchString, SqlDbType.VarChar);
                param[2] = new DbParam("@maximumRows", maximumRows, SqlDbType.Int);
                param[3] = new DbParam("@StartIndex", startRowIndex, SqlDbType.Int);

                dt = Db.GetDataTable("PROC_tblProfileData_SelAllByPaging", param);

                if (dt != null)
                {
                    objProfileDataList = new List<ProfileData>();
                    ProfileData objProfileData = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        objProfileData = new ProfileData();
                        objProfileData.ProfileId = Db.ToInteger( row["ProfileId"]);
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
            }
            return objProfileDataList;
        }

        /// <summary>
        /// Get Profile Data Count By SearchString
        /// </summary>
        /// <param name="SearchString">SearchString</param>
        /// <returns>int</returns>
        public int GetProfileDataCount(string SearchString)
        {
            int ProfileDataCount = 0;
            try
            {
                DbParam[] param = new DbParam[1];                
                object obj;
                param[0] = new DbParam("@SearchString", SearchString, SqlDbType.VarChar);
                obj = Db.GetScalar("PROC_tblProfileData_SelCount", param);
                if (obj != null)
                {
                    ProfileDataCount = Db.ToInteger(obj);
                }               
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "GetProfileDataCount", "ProfileDataDao"); 
            }
            return ProfileDataCount;
        }

        #endregion

        #region [Insert Methods]
        /// <summary>
        /// Insert into Profile
        /// </summary>
        /// <param name="ArrProfileData">ArrProfileData</param>
        /// <returns>bool</returns>
        public bool InsertProfile(ProfileData[] ArrProfileData)
        {            
            DataTable dt = new DataTable();
            dt.Columns.Add("ProfileName");
            dt.Columns.Add("ParameterName");
            dt.Columns.Add("CaptruredValue");
            dt.Columns.Add("MinValue");
            dt.Columns.Add("MaxValue");
            dt.Columns.Add("Date"); 
            dt.Columns["ProfileName"].DataType = System.Type.GetType("System.String");
            dt.Columns["ParameterName"].DataType = System.Type.GetType("System.String");
            dt.Columns["CaptruredValue"].DataType = System.Type.GetType("System.Double");
            dt.Columns["MinValue"].DataType = System.Type.GetType("System.Double");
            dt.Columns["MaxValue"].DataType = System.Type.GetType("System.Double");
            dt.Columns["Date"].DataType = System.Type.GetType("System.String");
            DataRow dr = null;
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            foreach (ProfileData objProfileData in ArrProfileData)
            {
                foreach (ProfileParameter objProfileParameter in objProfileData.ProfileParameters)
                {
                    dr = dt.NewRow();
                    dr["ProfileName"] = Db.ToString(objProfileData.ProfileName);
                    dr["ParameterName"] = Db.ToString(objProfileParameter.ParameterName);
                    dr["CaptruredValue"] = Db.ToDouble(objProfileParameter.CaptruredValue);
                    dr["MinValue"] = Db.ToDouble(objProfileParameter.MinValue);
                    dr["MaxValue"] = Db.ToDouble(objProfileParameter.MaxValue);
                    dr["Date"] = Db.ToString(objProfileParameter.Date);
                    dt.Rows.Add(dr);
                }
            }
            bool retValue = false;
            try
            {
                DbParam[] param = new DbParam[6];                
                param[0] = new DbParam("@ProfileName", "", "ProfileName", SqlDbType.VarChar);
                param[1] = new DbParam("@ParameterName", "", "ParameterName", SqlDbType.VarChar);
                param[2] = new DbParam("@CapturedValue", "", "CaptruredValue", SqlDbType.Float);
                param[3] = new DbParam("@MinValue", "", "MinValue", SqlDbType.Float);
                param[4] = new DbParam("@MaxValue", "", "MaxValue", SqlDbType.Float);
                param[5] = new DbParam("@Date", "", "Date", SqlDbType.DateTime);
            //    intReturn = Db.Insert("SP_tblProfileData_Ins", param, true);
                retValue = Db.Update(ds, "SP_tblProfileData_Ins", "", "", param, true);                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "InsertProfile", "ProfileDataDao"); 
            }
            return retValue;
        }

        #endregion        

        public DataTable SelExcelData(String fromDate, String todate)
        {
            try
            {
                DbParam[] param = new DbParam[2];

                param[0] = new DbParam("@SearchStringFromDate", fromDate, SqlDbType.VarChar);
                param[1] = new DbParam("@SearchStringToDate", todate, SqlDbType.VarChar);
                var excelData = Db.GetDataTable("PROC_tblProfileData_GetDataForExportToExcel", param);
                return excelData;
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllByPaging", "ProfileDataDao");
                throw;
            }

            return null;
        }

      

    }
}
