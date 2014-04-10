using System;
using System.Collections.Generic;
using System.Text;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using SchneiderMilkManagement.DataLayer.DataObjects;
using System.Data;

namespace SchneiderMilkManagement.BusinessLayer.BusinessFacade
{
    public class ProfileDataFacade :IFacade
    {
        #region [Select Methods]
       
        /// <summary>
        /// Select ProfileData By Paging
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
                objProfileDataList = new ProfileDataDao().SelAllByPaging(SortBy, SearchString, maximumRows, startRowIndex);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllByPaging", "ProfileDataFacade");
                throw;
            }
            return objProfileDataList;
            
        }

        /// <summary>
        ///  Select Profile Data Count By SearchString
        /// </summary>
        /// <param name="SortBy">SortBy</param>
        /// <param name="SearchString">SearchString</param>
        /// <returns>int</returns>
        public int GetProfileDataCount(string SortBy, string SearchString)
        {
            int retValue = 0;
            try
            {
               retValue=new ProfileDataDao().GetProfileDataCount(SearchString);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "GetProfileDataCount", "ProfileDataFacade");
                throw;
            }
            return retValue;
        }
                
        #endregion

        #region [Insert/Update/Delete]
        /// <summary>
        /// Insert Into Profile
        /// </summary>
        /// <param name="ArrProfileData">ArrProfileData</param>
        /// <returns>bool</returns>
        public bool InsertProfile(ProfileData[] ArrProfileData)
        {
            bool ret = false;
            try
            {
                ret= new ProfileDataDao().InsertProfile(ArrProfileData);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "InsertProfile", "ProfileDataFacade");
                throw;
            }
            return ret;
        }

        
        /// <summary>
        /// Delete MilkPropery By MilkPropertyIds
        /// </summary>
        /// <param name="MilkPropertyIds">MilkPropertyIds</param>
        /// <returns>int</returns>
        public override int DeleteWithArray(string MilkPropertyIds)
        {

            int retValue = 0;            
            return retValue;
            
        }
        #endregion

        public DataTable SelExcelData(string fromDate, string toDate)
        {
            try
            {
                return new ProfileDataDao().SelExcelData(fromDate, toDate);

            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllByPaging", "ProfileDataFacade");
                throw;
            }

            return null;
        }
    }
}
