using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;

namespace SchneiderMilkManagement.DataLayer.DataObjects
{
    public class SyncLogDao
    {
        #region [Member parameters]

        IList<SyncLog> objSyncLogs;
        SyncLog objSyncLog;
        DataTable dt;
        DataRow dr;
        int intReturn;

        #endregion

        #region [Select Methods]
        /// <summary>
        /// Select SyncLog By SyncLogId
        /// </summary>
        /// <param name="SyncLogId">SyncLogId</param>
        /// <returns>SyncLog</returns>
        public SyncLog SelById(int SyncLogId)
        {
            try
            {
                DbParam[] param = new DbParam[1];

                param[0] = new DbParam("@SyncLogId", SyncLogId, SqlDbType.Int);
                dr = Db.GetDataRow("Sp_tblSyncLog_SelById", param);

                if (dr != null)
                    objSyncLog = GetObject(dr);

                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelById", "SyncLogDao");
            }
            return objSyncLog;
        }

        /// <summary>
        /// Select All SyncLog
        /// </summary>
        /// <returns>IList<SyncLog></returns>
        public IList<SyncLog> SelAll()
        {
            try
            {
                dt = Db.GetDataTable("Sp_tblSyncLog_SelALL", null);

                if (dt != null)
                {
                    objSyncLogs = new List<SyncLog>();

                    foreach (DataRow row in dt.Rows)
                        objSyncLogs.Add(GetObject(row));
                }

               
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAll", "SyncLogDao");
            }
            return objSyncLogs;
        }

        #endregion

        #region [Insert Methods]
        /// <summary>
        /// Insert Into SyncLog
        /// </summary>
        /// <param name="objSyncLog">objSyncLog</param>
        /// <returns>int </returns>
        public int InsertSyncLog(SyncLog objSyncLog)
        {
            try
            {
                DbParam[] param = new DbParam[3];

                param[0] = new DbParam("@Module", objSyncLog.Module, SqlDbType.VarChar);
                param[1] = new DbParam("@EntityId", objSyncLog.EntityId, SqlDbType.Int);
                param[2] = new DbParam("@LastSyncDate", objSyncLog.LastSyncDate, SqlDbType.DateTime);

                intReturn = Db.Insert("SP_tblSyncLog_INS", param, true);
               
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "InsertSyncLog", "SyncLogDao");
            }
            return intReturn;
        }

        #endregion


        #region [Mapper]
        /// <summary>
        ///  Get SyncLog
        /// </summary>
        /// <param name="dr">dr</param>
        /// <returns>SyncLog</returns>
        SyncLog GetObject(DataRow dr)
        {
            try
            {
                objSyncLog = new SyncLog();

                objSyncLog.SyncLogId = Db.ToInteger(dr["SyncLogId"]);
                objSyncLog.Module = Db.ToString(dr["Module"]);
                objSyncLog.EntityId = Db.ToInteger(dr["EntityId"]);
                objSyncLog.LastSyncDate = Db.ToDateTime(dr["LastSyncDate"]);
                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "GetObject", "SyncLogDao");
            }
            return objSyncLog;
        }
        #endregion

  

    }
}
