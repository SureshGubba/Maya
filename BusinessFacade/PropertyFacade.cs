using System;
using System.Collections.Generic;
using System.Text;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using SchneiderMilkManagement.DataLayer.DataObjects;

namespace SchneiderMilkManagement.BusinessLayer.BusinessFacade
{
    public class PropertyFacade :IFacade
    {
        #region [Select Methods]
        /// <summary>
        /// Select Property By PropertyId 
        /// </summary>
        /// <param name="PropertyId">PropertyId</param>
        /// <returns>Property</returns>
        public Property SelById(int PropertyId)
        {
            return new PropertyDao().SelById(PropertyId);
        }

        /// <summary>
        /// Select All Property
        /// </summary>
        /// <returns>IList<Property> </returns>
        public IList<Property> SelAll()
        {
            return new PropertyDao().SelAll();
        }

        /// <summary>
        /// Select All Property By ProfileId
        /// </summary>
        /// <param name="ProfileId">ProfileId</param>
        /// <returns>IList<Property></returns>
        public IList<Property> SelAllByProfile(int ProfileId)
        {
            return new PropertyDao().SelAllByProfile(ProfileId);
        }

        /// <summary>
        /// Select All Assigned Property By ProfileId
        /// </summary>
        /// <param name="ProfileId">ProfileId</param>
        /// <returns>IList<Property></returns>
        public IList<Property> SelAllAssignedPropertyByProfile(int ProfileId)
        {
            return new PropertyDao().SelAllAssignedPropertyByProfile(ProfileId);
        }

        //public IList<Property> SelAllActive()
        //{
        //    return new PropertyDao().SelAllActive();
        //}

        /// <summary>
        /// Select All Property By Paging
        /// </summary>
        /// <param name="SortBy">SortBy</param>
        /// <param name="SearchString">SearchString</param>
        /// <param name="maximumRows">maximumRows</param>
        /// <param name="startRowIndex">startRowIndex</param>
        /// <returns>IList<Property></returns>
        public IList<Property> SelAllByPaging(string SortBy, string SearchString, int maximumRows, int startRowIndex)
        {
            return new PropertyDao().SelAllByPaging(SortBy, SearchString, maximumRows, startRowIndex);
        }

        /// <summary>
        /// Get Count Of Property By SearchString
        /// </summary>
        /// <param name="SortBy">SortBy</param>
        /// <param name="SearchString">SearchString</param>
        /// <returns>int</returns>
        public int GetPropertyCount(string SortBy, string SearchString)
        {
            return new PropertyDao().GetPropertyCount(SearchString);
        }

        //public IList<Property> SelAllActiveByLastSyncDate(DateTime LastSyncDate)
        //{
        //    return new PropertyDao().SelAllActiveByLastSyncDate(LastSyncDate);
        //}
        #endregion

        #region [Insert/Update/Delete]
        /// <summary>
        /// Insert Into Property
        /// </summary>
        /// <param name="objProperty">objProperty</param>
        /// <returns>int</returns>
        public int InsertProperty(Property objProperty)
        {
            int retValue = 0;
            try
            {
                retValue = new PropertyDao().InsertProperty(objProperty);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "InsertProperty", "PropertyFacade");
                throw;
            }
            return retValue;
            
        }

        /// <summary>
        /// Upadte The Property
        /// </summary>
        /// <param name="objProperty">objProperty</param>
        /// <returns>int</returns>
        public int UpdateProperty(Property objProperty)
        {
            int retValue = 0;
            try
            {
                retValue = new PropertyDao().UpdateProperty(objProperty);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "UpdateProperty", "PropertyFacade");
            }
            return retValue;
            
        }

        /// <summary>
        /// Delete Property By PropertyIds
        /// </summary>
        /// <param name="PropertyIds">PropertyIds</param>
        /// <returns>int</returns>
        public override int DeleteWithArray(string PropertyIds)
        {
           
            int retValue = 0;
            try
            {
                retValue = new PropertyDao().DeletePropertysId(PropertyIds);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "UpdateProperty", "PropertyFacade");
            }
            return retValue;
        }
        #endregion

        #region profile drop down
        /// <summary>
        /// Select All Profile For Drop Down
        /// </summary>
        /// <returns>IList<Profile></returns>
        public IList<Profile> SelAllProfileForDropDown()
        {
            IList<Profile> objProfileList = null;
            try
            {
                objProfileList=new PropertyDao().SelAllProfileForDropDown();
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllProfileForDropDown", "PropertyFacade");
            }
            return objProfileList;
        }
        #endregion
    }
}
