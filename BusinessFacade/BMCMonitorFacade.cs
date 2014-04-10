using System;
using System.Collections.Generic;
using System.Text;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using SchneiderMilkManagement.DataLayer.DataObjects;

namespace SchneiderMilkManagement.BusinessLayer.BusinessFacade
{
    public class BMCMonitorFacade : IFacade
    {
        /// <summary>
        /// Insert User
        /// </summary>
        /// <param name="ObjUser">ObjUser</param>
        /// <returns>int</returns>
        public IList<BMCLatestParameter> SelectCurrentProfileParametersOfBMC()
        {
            try
            {
                return new BMCMonitorDao().SelectCurrentProfileParametersOfBMC();
        
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelectCurrentProfileParametersOfBMC", "BMCMonitorFacade");
                throw;
            }
            return null;
            
        }


        public override int DeleteWithArray(string IdStr)
        {
            throw new NotImplementedException();
        }
    }
}