using System;
using System.Collections.Generic;
using System.Text;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using SchneiderMilkManagement.DataLayer.DataObjects;

namespace SchneiderMilkManagement.BusinessLayer.BusinessFacade
{
    public class MilkPropertyBF :IFacade
    {
        #region [Select Methods]
        /// <summary>
        /// Select The MilkProperty By MilkPropertyId
        /// </summary>
        /// <param name="MilkPropertyId">MilkPropertyId</param>
        /// <returns>MilkProperty</returns>
        public MilkProperty SelById(int MilkPropertyId)
        {
            MilkProperty objMilkProperty = null;
            try
            {
                objMilkProperty= new MilkPropertyDao().SelById(MilkPropertyId);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelById", "MilkPropertyBF");
                throw;
            }
            return objMilkProperty;
        }

        /// <summary>
        /// Select All MilkProperty
        /// </summary>
        /// <returns>IList<MilkProperty> </returns>
        public IList<MilkProperty> SelAll()
        {
            IList<MilkProperty> objMilkPropertyList = null;
            try
            {
                objMilkPropertyList = new MilkPropertyDao().SelAll();
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAll", "MilkPropertyBF");
                throw;
            }
            return objMilkPropertyList;
            
        }

        /// <summary>
        /// Select All Active MilkProperty
        /// </summary>
        /// <returns>IList<MilkProperty></returns>
        public IList<MilkProperty> SelAllActive()
        {
            IList<MilkProperty> objMilkPropertyList = null;
            try
            {
                objMilkPropertyList =new MilkPropertyDao().SelAllActive();
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllActive", "MilkPropertyBF");
                throw;
            }
            return objMilkPropertyList;
            
        }


        /// <summary>
        /// Select MilkProperty By Paging
        /// </summary>
        /// <param name="SortBy">SortBy</param>
        /// <param name="SearchString">SearchString</param>
        /// <param name="maximumRows">maximumRows</param>
        /// <param name="startRowIndex">startRowIndex</param>
        /// <returns>IList<MilkProperty></returns>
        public IList<MilkProperty> SelAllByPaging(string SortBy, string SearchString, int maximumRows, int startRowIndex)
        {
            IList<MilkProperty> objMilkPropertyList = null;
            try
            {
                objMilkPropertyList = new MilkPropertyDao().SelAllByPaging(SortBy, SearchString, maximumRows, startRowIndex);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllActive", "MilkPropertyBF");
                throw;
            }
            return objMilkPropertyList;
           
        }


        /// <summary>
        /// Get Count Of MilkProperty By SearchString
        /// </summary>
        /// <param name="SortBy">SortBy</param>
        /// <param name="SearchString">SearchString</param>
        /// <returns>int</returns>
        public int GetMilkPropertyCount(string SortBy, string SearchString)
        {
            int retValue = 0;
            try
            {
                retValue =new MilkPropertyDao().GetMilkPropertyCount(SearchString);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "GetMilkPropertyCount", "MilkPropertyBF");
                throw;
            }
            return retValue;
            
        }


        /// <summary>
        /// Select All Active MilkProperty By LastSyncDate
        /// </summary>
        /// <param name="LastSyncDate">LastSyncDate</param>
        /// <returns>IList<MilkProperty></returns>
        public IList<MilkProperty> SelAllActiveByLastSyncDate(DateTime LastSyncDate)
        {
            IList<MilkProperty> objMilkPropertyList = null;
            try
            {
                objMilkPropertyList =new MilkPropertyDao().SelAllActiveByLastSyncDate(LastSyncDate);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllActive", "MilkPropertyBF");
                throw;
            }
            return objMilkPropertyList;
             
        }
        #endregion

        #region [Insert/Update/Delete]
        /// <summary>
        /// Insert Into  MilkProperty 
        /// </summary>
        /// <param name="objMilkProperty">objMilkProperty</param>
        /// <returns>int</returns>
        public int InsertMilkProperty(MilkProperty objMilkProperty)
        {
            int retValue = 0;
            try
            {
                retValue = new MilkPropertyDao().InsertMilkProperty(objMilkProperty);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "InsertMilkProperty", "MilkPropertyBF");
                throw;
            }
            return retValue;
        }


        /// <summary>
        /// Upadte The MilkProperty 
        /// </summary>
        /// <param name="objMilkProperty">objMilkProperty</param>
        /// <returns>int</returns>
        public int UpdateMilkProperty(MilkProperty objMilkProperty)
        {
            int retValue = 0;
            try
            {
                retValue = new MilkPropertyDao().UpdateMilkProperty(objMilkProperty);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "UpdateMilkProperty", "MilkPropertyBF");
                throw;
            }
            return retValue;
        }


        /// <summary>
        /// Delete The MilkProperty By MilkPropertyIds
        /// </summary>
        /// <param name="MilkPropertyIds">MilkPropertyIds</param>
        /// <returns>int</returns>
        public override int DeleteWithArray(string MilkPropertyIds)
        {
            int retValue = 0;
            try
            {
                retValue = new MilkPropertyDao().DeleteMilkPropertysId(MilkPropertyIds);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "DeleteWithArray", "MilkPropertyBF");
                throw;
            }
            return retValue;
        }
        #endregion
    }
}
