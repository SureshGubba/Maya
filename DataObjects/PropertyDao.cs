using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;

namespace SchneiderMilkManagement.DataLayer.DataObjects
{
    public class PropertyDao
    {
        #region [Member parameters]

        IList<Property> objProperties;
        Property objProperty;
        DataTable dt;
        DataRow dr;
        int intReturn;

        #endregion

        #region [Select Methods]
        /// <summary>
        /// Select Property By PropertyId
        /// </summary>
        /// <param name="PropertyId">PropertyId</param>
        /// <returns>Property</returns>
        public Property SelById(int PropertyId)
        {
            try
            {
                DbParam[] param = new DbParam[1];

                param[0] = new DbParam("@PropertyId", PropertyId, SqlDbType.Int);
                dr = Db.GetDataRow("SP_tblProperties_SelById", param);

                if (dr != null)
                    objProperty = GetObject(dr);

                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelById", "PropertyDao");
            }
            return objProperty;
        }


        /// <summary>
        /// Select All Property
        /// </summary>
        /// <returns>IList<Property></returns>
        public IList<Property> SelAll()
        {
            IList<Property> objPropertyList = null;
            Property objProperty = null;
            try
            {
                dt = Db.GetDataTable("SP_tblProperties_SelAll", null);
                if (dt != null)
                {
                    objPropertyList = new List<Property>();
                    foreach (DataRow row in dt.Rows)
                    {
                        objProperty = new Property();
                        objProperty.PropertyId = Db.ToInteger(row["PropertyId"]);
                        objProperty.ProfileId = Db.ToInteger(row["ProfileId"]);
                        objProperty.Name = Db.ToString(row["Name"]);
                        objProperty.Value = Db.ToString(row["Value"]);
                        objProperty.UIOrder = Db.ToInteger(row["UIOrder"]);
                        objPropertyList.Add(objProperty);
                    }

                }
                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAll", "PropertyDao");
            }
            return objPropertyList;
        }

        /// <summary>
        /// Select All Property By ProfileId
        /// </summary>
        /// <param name="ProfileId">ProfileId</param>
        /// <returns>IList<Property></returns>
        public IList<Property> SelAllByProfile(int ProfileId)
        {
            IList<Property> objPropertyList = null;
            Property objProperty = null;
            try
            {
                DbParam[] param = new DbParam[1];

                param[0] = new DbParam("@ProfileId", ProfileId, SqlDbType.Int);
                dt = Db.GetDataTable("SP_tblProperties_SelAllByProfileId", param);
                if (dt != null)
                {
                    objPropertyList = new List<Property>();
                    foreach (DataRow row in dt.Rows)
                    {
                        objProperty = new Property();
                        objProperty.PropertyId = Db.ToInteger(row["PropertyId"]);
                        objProperty.ProfileId = Db.ToInteger(row["ProfileId"]);
                        objProperty.Name = Db.ToString(row["Name"]);
                        objProperty.Value = Db.ToString(row["Value"]);
                        objPropertyList.Add(objProperty);
                    }

                }
                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllByProfile", "PropertyDao");
            }
            return objPropertyList;
        }

        /// <summary>
        /// Select All Assigned Property By ProfileId
        /// </summary>
        /// <param name="ProfileId">ProfileId</param>
        /// <returns>IList<Property></returns>
        public IList<Property> SelAllAssignedPropertyByProfile(int ProfileId)
        {
            IList<Property> objPropertyList = null;
            Property objProperty = null;
            try
            {
                DbParam[] param = new DbParam[1];

                param[0] = new DbParam("@ProfileId", ProfileId, SqlDbType.Int);
                dt = Db.GetDataTable("SP_tblProperties_SelAllAssignedPropertyByProfileId", param);
                if (dt != null)
                {
                    objPropertyList = new List<Property>();
                    foreach (DataRow row in dt.Rows)
                    {
                        objProperty = new Property();
                        objProperty.PropertyId = Db.ToInteger(row["PropertyId"]);
                        objProperty.ProfileId = Db.ToInteger(row["ProfileId"]);
                        objProperty.Name = Db.ToString(row["Name"]);
                        objProperty.Value = Db.ToString(row["Value"]);
                        objPropertyList.Add(objProperty);
                    }

                }
               
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllAssignedPropertyByProfile", "PropertyDao");
            }
            return objPropertyList;
        }

        //public IList<MilkProperty> SelAllActive()
        //{
        //    try
        //    {
        //        dt = Db.GetDataTable("SP_tblMilkProperties_SelAllActive", null);

        //        if (dt != null)
        //        {
        //            objMilkPropertys = new List<MilkProperty>();

        //            foreach (DataRow row in dt.Rows)
        //                objMilkPropertys.Add(GetObject(row));
        //        }

        //        return objMilkPropertys;
        //    }
        //    catch (Exception ex)
        //    {
        //        // CommonFunctions.LogError(ex, WINIT.ErrorLog.LogSeverity.Error);
        //        throw ex;
        //    }
        //}

        //public IList<MilkProperty> SelAllActiveByLastSyncDate(DateTime LastSyncDate)
        //{
        //    try
        //    {
        //        DbParam[] param = new DbParam[1];

        //        param[0] = new DbParam("@LastSyncDate", LastSyncDate, SqlDbType.DateTime);
        //        dt = Db.GetDataTable("SP_tblMilkProperties_SelAllByLastSyncDate", param);

        //        if (dt != null)
        //        {
        //            objMilkPropertys = new List<MilkProperty>();

        //            foreach (DataRow row in dt.Rows)
        //                objMilkPropertys.Add(GetObject(row));
        //        }

        //        return objMilkPropertys;
        //    }
        //    catch (Exception ex)
        //    {
        //        // CommonFunctions.LogError(ex, WINIT.ErrorLog.LogSeverity.Error);
        //        throw ex;
        //    }
        //}


        /// <summary>
        /// Select All Property By Paging
        /// </summary>
        /// <param name="SortBy">SortBy</param>
        /// <param name="SearchString">SearchString</param>
        /// <param name="maximumRows">maximumRows</param>
        /// <param name="startRowIndex">startRowIndex</param>
        /// <returns>IList<Property> </returns>
        public IList<Property> SelAllByPaging(string SortBy, string SearchString, int maximumRows, int startRowIndex)
        {
            try
            {
                DbParam[] param = new DbParam[4];

                param[0] = new DbParam("@SortBy", SortBy, SqlDbType.VarChar);
                param[1] = new DbParam("@SearchString", SearchString, SqlDbType.VarChar);
                param[2] = new DbParam("@maximumRows", maximumRows, SqlDbType.Int);
                param[3] = new DbParam("@StartIndex", startRowIndex, SqlDbType.Int);

                dt = Db.GetDataTable("PROC_tblProperties_SelSllByPaging", param);

                if (dt != null)
                {
                    objProperties = new List<Property>();

                    foreach (DataRow row in dt.Rows)
                        objProperties.Add(GetObject(row));
                }

                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllByPaging", "PropertyDao");
            }
            return objProperties;
        }


        /// <summary>
        /// Get Count Of Property By SearchString
        /// </summary>
        /// <param name="SearchString">SearchString</param>
        /// <returns>int</returns>
        public int GetPropertyCount(string SearchString)
        {
            int PropertyCount = 0;
            try
            {
                DbParam[] param = new DbParam[1];
                
                object obj;

                param[0] = new DbParam("@SearchString", SearchString, SqlDbType.VarChar);

                obj = Db.GetScalar("PROC_tblProperties_SelCount", param);

                if (obj != null)
                {
                    PropertyCount = Db.ToInteger(obj);
                }
              
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "GetPropertyCount", "PropertyDao");
            }
            return PropertyCount;
        }

        #endregion

        #region [Insert Methods]
        /// <summary>
        ///  Insert Into Property
        /// </summary>
        /// <param name="objProperty">objProperty</param>
        /// <returns>int</returns>
        public int InsertProperty(Property objProperty)
        {
            try
            {
                DbParam[] param = new DbParam[4];

                param[0] = new DbParam("@Name", objProperty.Name, SqlDbType.VarChar);
                param[1] = new DbParam("@Value", "", SqlDbType.VarChar);
                param[2] = new DbParam("@ProfileId", objProperty.ProfileId, SqlDbType.Int);
                param[3] = new DbParam("@CreatedBy", objProperty.CreatedBy, SqlDbType.Int);
                
                intReturn = Db.Insert("SP_tblProperties_Ins", param, true);
                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "InsertProperty", "PropertyDao");
            }
            return intReturn;
        }

        #endregion

        #region [Update Methods]
        /// <summary>
        /// Update the Property
        /// </summary>
        /// <param name="objProperty">objProperty</param>
        /// <returns>int</returns>
        public int UpdateProperty(Property objProperty)
        {
            try
            {
                DbParam[] param = new DbParam[5];

                param[0] = new DbParam("@PropertyId", objProperty.PropertyId, SqlDbType.Int);
                param[1] = new DbParam("@Name", objProperty.Name, SqlDbType.VarChar);
                param[2] = new DbParam("@Value", "", SqlDbType.VarChar);
                param[3] = new DbParam("@ProfileId", objProperty.ProfileId, SqlDbType.Int);
                param[4] = new DbParam("@ModifiedBy", objProperty.ModifiedBy, SqlDbType.Int);
                intReturn = Db.Insert("SP_tblProperties_Upd", param, true);

                return intReturn;
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "UpdateProperty", "PropertyDao");
            }
            return intReturn;
        }

        #endregion

        #region [Delete Methods]
        /// <summary>
        /// Delete The Property By PropertyIds
        /// </summary>
        /// <param name="PropertyIds">PropertyIds</param>
        /// <returns>int</returns>
        public int DeletePropertysId(string PropertyIds)
        {
            int retval = 0;
            try
            {
                objProperties = new List<Property>();
                DbParam[] param = new DbParam[1];
                param[0] = new DbParam("@PropertyIds", PropertyIds, SqlDbType.VarChar);
                retval = Db.Update("sp_tblProperty_DelwithArray", param);
                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "DeletePropertysId", "PropertyDao");
            }
            return retval;
        }

        #endregion

        #region profile drop down
        /// <summary>
        /// Select All Profile For DropDown
        /// </summary>
        /// <returns>IList<Profile></returns>
        public IList<Profile> SelAllProfileForDropDown()
        {
            IList<Profile> objProfileList = null;
            Profile objProfile = null;
            try
            {
                dt = Db.GetDataTable("SP_tblProfile_SelAllForDropDown", null);

                if (dt != null)
                {
                    objProfileList = new List<Profile>();
                    foreach (DataRow row in dt.Rows)
                    {
                        objProfile = new Profile();
                        objProfile.ProfileId = Db.ToInteger(row["ProfileId"]);
                        objProfile.Name = Db.ToString(row["Name"]);
                        objProfile.Value = Db.ToString(row["Value"]);
                        objProfileList.Add(objProfile);
                    }
                        
                }

               
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllProfileForDropDown", "PropertyDao");
            }
            return objProfileList;
        }
        #endregion
        #region [Mapper]
        /// <summary>
        /// Get Property
        /// </summary>
        /// <param name="dr">dr</param>
        /// <returns>Property</returns>
        Property GetObject(DataRow dr)
        {
            try
            {
                objProperty = new Property();

                objProperty.PropertyId = Db.ToInteger(dr["PropertyId"]);
                objProperty.Name = Db.ToString(dr["Name"]);
                objProperty.Value = Db.ToString(dr["Value"]);
                objProperty.ProfileId = Db.ToInteger(dr["ProfileId"]);
                objProperty.ProfileName = Db.ToString(dr["ProfileName"]);
                objProperty.CreatedBy = Db.ToInteger(dr["CreatedBy"]);
                objProperty.CreatedOn = Db.ToDateTime(dr["CreatedOn"]);
                objProperty.ModifiedBy = Db.ToInteger(dr["ModifiedBy"]);
                objProperty.ModifiedOn = Db.ToDateTime(dr["ModifiedOn"]);
                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "GetObject", "PropertyDao");
            }
            return objProperty;
        }
        #endregion

    }
}
