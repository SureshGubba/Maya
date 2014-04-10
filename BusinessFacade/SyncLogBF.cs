using System;
using System.Collections.Generic;
using System.Text;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using SchneiderMilkManagement.DataLayer.DataObjects;

namespace SchneiderMilkManagement.BusinessLayer.BusinessFacade
{
    public class SyncLogBF
    {
        #region [Select Methods]

        /// <summary>
        /// Select SyncLog By SyncLogId  
        /// </summary>
        /// <param name="SyncLogId">SyncLogId</param>
        /// <returns>SyncLog</returns>
        public SyncLog SelById(int SyncLogId)
        {

            SyncLog objSyncLog = null;
            try
            {
                objSyncLog =new SyncLogDao().SelById(SyncLogId);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelById", "SyncLogBF");
            }
            return objSyncLog;
            
        }

        /// <summary>
        /// Select All SyncLog
        /// </summary>
        /// <returns>IList<SyncLog></returns>
        public IList<SyncLog> SelAll()
        {

            IList<SyncLog> objSyncLogList = null;
            try
            {
                objSyncLogList =new SyncLogDao().SelAll();
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelById", "SyncLogBF");
            }
            return objSyncLogList;
            
        }

        #endregion

        #region [Insert]
        /// <summary>
        /// Insert Into SyncLog
        /// </summary>
        /// <param name="objSyncLog">objSyncLog</param>
        /// <returns>int</returns>
        public int InsertSyncLog(SyncLog objSyncLog)
        {
            int retValue = 0;
            try
            {
                retValue = new SyncLogDao().InsertSyncLog(objSyncLog);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "InsertSyncLog", "SyncLogBF");
            }
            return retValue;
            
        }
      
        #endregion
    }
}
