using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;

namespace SchneiderMilkManagement.DataLayer.DataObjects
{
    public class MilkPropertyDao
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
        /// Select By Milk Property Id
        /// </summary>
        /// <param name="MilkPropertyId">MilkPropertyId</param>
        /// <returns>MilkProperty</returns>
        public MilkProperty SelById(int MilkPropertyId)
        {
            try
            {
                DbParam[] param = new DbParam[1];

                param[0] = new DbParam("@MilkPropertyId", MilkPropertyId, SqlDbType.Int);
                dr = Db.GetDataRow("SP_tblMilkProperties_SelById", param);

                if (dr != null)
                    objMilkProperty = GetObject(dr);
                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelById", "MilkPropertyDao");               
            }
            return objMilkProperty;
        }

        public IList<MilkProperty> SelAll()
        {
            try
            {
                dt = Db.GetDataTable("SP_tblMilkProperties_SelAll", null);
                if (dt != null)
                {
                    objMilkPropertys = new List<MilkProperty>();

                    foreach (DataRow row in dt.Rows)
                        objMilkPropertys.Add(GetObject(row));
                }               
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAll", "MilkPropertyDao"); 
            }
            return objMilkPropertys;
        }
        /// <summary>
        /// Select All Active MilkProperty
        /// </summary>
        /// <returns> IList<MilkProperty> </returns>
        public IList<MilkProperty> SelAllActive()
        {
            try
            {
                dt = Db.GetDataTable("SP_tblMilkProperties_SelAllActive", null);

                if (dt != null)
                {
                    objMilkPropertys = new List<MilkProperty>();

                    foreach (DataRow row in dt.Rows)
                        objMilkPropertys.Add(GetObject(row));
                }
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllActive", "MilkPropertyDao"); 
            }
            return objMilkPropertys;
        }
        /// <summary>
        /// Select All Active MilkProperty By LastSyncDate
        /// </summary>
        /// <param name="LastSyncDate">LastSyncDate</param>
        /// <returns>IList<MilkProperty> </returns>
        public IList<MilkProperty> SelAllActiveByLastSyncDate(DateTime LastSyncDate)
        {
            try
            {
                DbParam[] param = new DbParam[1];
                param[0] = new DbParam("@LastSyncDate", LastSyncDate, SqlDbType.DateTime);
                dt = Db.GetDataTable("SP_tblMilkProperties_SelAllByLastSyncDate", param);
                if (dt != null)
                {
                    objMilkPropertys = new List<MilkProperty>();

                    foreach (DataRow row in dt.Rows)
                        objMilkPropertys.Add(GetObject(row));
                }                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllActiveByLastSyncDate", "MilkPropertyDao"); 
            }
            return objMilkPropertys;
        }
        /// <summary>
        /// Select All MilkProperty By Paging
        /// </summary>
        /// <param name="SortBy">SortBy</param>
        /// <param name="SearchString">SearchString</param>
        /// <param name="maximumRows">maximumRows</param>
        /// <param name="startRowIndex">startRowIndex</param>
        /// <returns>IList<MilkProperty</returns>
        public IList<MilkProperty> SelAllByPaging(string SortBy, string SearchString, int maximumRows, int startRowIndex)
        {
            try
            {
                DbParam[] param = new DbParam[4];
                param[0] = new DbParam("@SortBy", SortBy, SqlDbType.VarChar);
                param[1] = new DbParam("@SearchString", SearchString, SqlDbType.VarChar);
                param[2] = new DbParam("@maximumRows", maximumRows, SqlDbType.Int);
                param[3] = new DbParam("@StartIndex", startRowIndex, SqlDbType.Int);
                dt = Db.GetDataTable("PROC_tblMilkProperties_SelSllByPaging", param);
                if (dt != null)
                {
                    objMilkPropertys = new List<MilkProperty>();
                    foreach (DataRow row in dt.Rows)
                        objMilkPropertys.Add(GetObject(row));
                }                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllByPaging", "MilkPropertyDao"); 
            }
            return objMilkPropertys;
        }

        /// <summary>
        /// Get Count of MilkProperty
        /// </summary>
        /// <param name="SearchString">SearchString</param>
        /// <returns>int</returns>
        public int GetMilkPropertyCount(string SearchString)
        {
            int MilkPropertyCount = 0;
            try
            {
                DbParam[] param = new DbParam[1];                
                object obj;
                param[0] = new DbParam("@SearchString", SearchString, SqlDbType.VarChar);
                obj = Db.GetScalar("PROC_tblMilkProperties_SelCount", param);
                if (obj != null)
                {
                    MilkPropertyCount = Db.ToInteger(obj);
                }                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "GetMilkPropertyCount", "MilkPropertyDao"); 
            }
            return MilkPropertyCount;
        }

        #endregion

        #region [Insert Methods]
        /// <summary>
        /// Insert into MilkProperty
        /// </summary>
        /// <param name="objMilkProperty">objMilkProperty</param>
        /// <returns>int</returns>
        public int InsertMilkProperty(MilkProperty objMilkProperty)
        {
            try
            {
                DbParam[] param = new DbParam[14];
                param[0] = new DbParam("@PropertyId", objMilkProperty.PropertyId, SqlDbType.Int);
                param[1] = new DbParam("@PropertyType", objMilkProperty.PropertyType, SqlDbType.VarChar);
                param[2] = new DbParam("@Unit", objMilkProperty.Unit, SqlDbType.VarChar);
                param[3] = new DbParam("@ProfileId", objMilkProperty.ProfileId, SqlDbType.Int);
                param[4] = new DbParam("@AllowDelete", objMilkProperty.AllowDelete, SqlDbType.Bit);
                param[5] = new DbParam("@IsActive", objMilkProperty.IsActive, SqlDbType.Bit);
                param[6] = new DbParam("@CreatedBy", objMilkProperty.CreatedBy, SqlDbType.Int);
                param[7] = new DbParam("@MinValue", objMilkProperty.MinValue, SqlDbType.Decimal);
                param[8] = new DbParam("@MaxValue", objMilkProperty.MaxValue, SqlDbType.Decimal);
                param[9] = new DbParam("@PortAddress", objMilkProperty.PortAddress, SqlDbType.VarChar);
                param[10] = new DbParam("@Address", objMilkProperty.Address, SqlDbType.VarChar);
                param[11] = new DbParam("@PollingTime", objMilkProperty.PollingTime, SqlDbType.Float);
                param[12] = new DbParam("@SmsPollingTime", objMilkProperty.SmsPollingTime, SqlDbType.Int);
                param[13] = new DbParam("@IsSMSRequired", objMilkProperty.IsSMSRequired, SqlDbType.Bit);
                intReturn = Db.Insert("SP_tblMilkProperties_Ins", param, true);
                new  MilkPropertyAuditDao().StoreAuditInformationForMilkProperty(objMilkProperty);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "InsertMilkProperty", "MilkPropertyDao"); 
            }
            return intReturn;
        }

        #endregion

        #region [Update Methods]
        /// <summary>
        /// Update the MilkProperty
        /// </summary>
        /// <param name="objMilkProperty">objMilkProperty</param>
        /// <returns>int</returns>
        public int UpdateMilkProperty(MilkProperty objMilkProperty)
        {
            try
            {
                new MilkPropertyAuditDao().StoreAuditInformationForMilkProperty(objMilkProperty);

                DbParam[] param = new DbParam[15];
                param[0] = new DbParam("@PropertyId", objMilkProperty.PropertyId, SqlDbType.Int);
                param[1] = new DbParam("@PropertyType", objMilkProperty.PropertyType, SqlDbType.VarChar);
                param[2] = new DbParam("@Unit", objMilkProperty.Unit, SqlDbType.VarChar);
                param[3] = new DbParam("@ProfileId", objMilkProperty.ProfileId, SqlDbType.Int);
                param[4] = new DbParam("@AllowDelete", objMilkProperty.AllowDelete, SqlDbType.Bit);
                param[5] = new DbParam("@IsActive", objMilkProperty.IsActive, SqlDbType.Bit);
                param[6] = new DbParam("@CreatedBy", objMilkProperty.CreatedBy, SqlDbType.Int);
                param[7] = new DbParam("@MilkPropertyId", objMilkProperty.MilkPropertyId, SqlDbType.Int);
                param[8] = new DbParam("@MinValue", objMilkProperty.MinValue, SqlDbType.Decimal);
                param[9] = new DbParam("@MaxValue", objMilkProperty.MaxValue, SqlDbType.Decimal);
                param[10] = new DbParam("@PortAddress", objMilkProperty.PortAddress, SqlDbType.VarChar);
                param[11] = new DbParam("@Address", objMilkProperty.Address, SqlDbType.VarChar);
                param[12] = new DbParam("@PollingTime", objMilkProperty.PollingTime, SqlDbType.Float);
                param[13] = new DbParam("@SmsPollingTime", objMilkProperty.SmsPollingTime, SqlDbType.Int);
                param[14] = new DbParam("@IsSMSRequired", objMilkProperty.IsSMSRequired, SqlDbType.Bit);
                intReturn = Db.Insert("SP_tblMilkProperties_Upd", param, true);                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "UpdateMilkProperty", "MilkPropertyDao"); 
            }
            return intReturn;
        }

        #endregion

        #region [Delete Methods]
        /// <summary>
        /// Delete the MilkProperty By MilkPropertyIds
        /// </summary>
        /// <param name="MilkPropertyIds">MilkPropertyIds</param>
        /// <returns>int</returns>
        public int DeleteMilkPropertysId(string MilkPropertyIds)
        {
            int retval = 0;
            try
            {
                List<MilkProperty> objIMilkProperty = new List<MilkProperty>();
                DbParam[] param = new DbParam[1];
                param[0] = new DbParam("@MilkPropertyIds", MilkPropertyIds, SqlDbType.VarChar);
                retval = Db.Update("sp_tblMilkProperty_DelwithArray", param);                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "DeleteMilkPropertysId", "MilkPropertyDao"); 
            }
            return retval;
        }

        #endregion

        #region [Mapper]
        /// <summary>
        /// Get the MilkProperty
        /// </summary>
        /// <param name="dr">dr</param>
        /// <returns>MilkProperty</returns>
        MilkProperty GetObject(DataRow dr)
        {
            try
            {
                objMilkProperty = new MilkProperty();

                objMilkProperty.MilkPropertyId = Db.ToInteger(dr["MilkPropertyId"]);
                objMilkProperty.ProfileId = Db.ToInteger(dr["ProfileId"]);
                objMilkProperty.PropertyId = Db.ToInteger(dr["PropertyId"]);
                objMilkProperty.PropertyName = Db.ToString(dr["PropertyName"]);
                objMilkProperty.PropertyType = Db.ToString(dr["PropertyType"]);
                objMilkProperty.Unit = Db.ToString(dr["Unit"]);
                objMilkProperty.PortAddress = Db.ToString(dr["PortAddress"]);
                objMilkProperty.MinValue = Db.ToDecimal(dr["MinValue"]);
                objMilkProperty.MaxValue = Db.ToDecimal(dr["MaxValue"]);
                objMilkProperty.ProfileType = Db.ToString(dr["ProfileType"]);
                objMilkProperty.Address = Db.ToString(dr["Address"]);
                objMilkProperty.AllowDelete = Db.ToBoolean(dr["AllowDelete"]);
                objMilkProperty.IsActive = Db.ToBoolean(dr["IsActive"]);
                objMilkProperty.CreatedBy = Db.ToInteger(dr["CreatedBy"]);
                objMilkProperty.CreatedOn = Db.ToDateTime(dr["CreatedOn"]);
                objMilkProperty.ModifiedBy = Db.ToInteger(dr["ModifiedBy"]);
                objMilkProperty.ModifiedOn = Db.ToDateTime(dr["ModifiedOn"]);
                objMilkProperty.PollingTime = Db.ToDouble(dr["PollingTime"]);
                objMilkProperty.SmsPollingTime = Db.ToInteger(dr["SmsPollingTime"]);
                objMilkProperty.IsSMSRequired = Db.ToBoolean(dr["IsSMSRequired"]);                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "GetObject", "MilkPropertyDao"); 
            }
            return objMilkProperty;
        }
        #endregion

    }
}
