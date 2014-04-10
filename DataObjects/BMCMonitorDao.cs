using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using System.Data.Linq;

namespace SchneiderMilkManagement.DataLayer.DataObjects
{
    public class BMCMonitorUIConversion
    {
        public String CapacityInLiters { get; set; }
        public String CapacityInCMS { get; set; }
        public Boolean IsInrange { get; set; }
    }

    public class BMCMonitorDao
    {

        public static DataTable CapacityLookUpTable { get; set; }

        #region [Member parameters]

        IList<BMCLatestParameter> objProfileParametersLatest;
        BMCLatestParameter objProfileParameter;
       
        #endregion

        #region [Select Methods]
       
        
        /// <summary>
        /// Select Current ProfileParameters
        /// </summary>
        /// <param name="SortBy">SortBy</param>
        /// <param name="SearchString">SearchString</param>
        /// <param name="maximumRows">maximumRows</param>
        /// <param name="startRowIndex">startRowIndex</param>
        /// <returns>IList<MilkProperty</returns>
        public IList<BMCLatestParameter> SelectCurrentProfileParametersOfBMC()
        {
            try
            {
                var allProperties = new PropertyDao().SelAll();
                var dataset = Db.GetDataSet("SP_tblProfileData_SelCurrentData",null);
                if (dataset != null)
                {
                    objProfileParametersLatest = new List<BMCLatestParameter>();
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        var bmcLatestParameter = GetObject(row, dataset.Tables[1]);
                        var paramdata = allProperties.Where(x => x.Name.Equals(bmcLatestParameter.ParameterName,StringComparison.InvariantCultureIgnoreCase)).ToList()[0];
                        bmcLatestParameter.UIOrder = (paramdata == null) ? Int32.MaxValue : paramdata.UIOrder;
                        objProfileParametersLatest.Add(bmcLatestParameter);
                    }
                }                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelectCurrentProfileParametersOfBMC", "BMCMonitorDao"); 
            }
            return objProfileParametersLatest;
        }

        public BMCMonitorUIConversion GetCapacityinLitersFromMM(decimal capacityInCM)
        {
            var bmcMonitorUIConversion = new BMCMonitorUIConversion();
            bmcMonitorUIConversion.IsInrange = true;
            
            try
            {
                bmcMonitorUIConversion.CapacityInCMS = capacityInCM.ToString();

                if (CapacityLookUpTable == null)
                {
                    CapacityLookUpTable = Db.GetDataTable("SP_tblBMCCapacityLookup", null);
                }
                decimal    capacityinMM =(capacityInCM * 10);

                if (CapacityLookUpTable != null)
              {
                  var maxValueRow = CapacityLookUpTable.AsEnumerable().OrderByDescending(x => x.Field<decimal>("CapacityinCMs")).FirstOrDefault();
                  var minValueRow = CapacityLookUpTable.AsEnumerable().OrderBy(x => x.Field<decimal>("CapacityinCMs")).FirstOrDefault();
                 var data = CapacityLookUpTable.AsEnumerable().Where(x => x.Field<decimal>("CapacityinCMs") == capacityinMM).FirstOrDefault();


                 if (capacityinMM > maxValueRow.Field<decimal>("CapacityinCMs"))
                  {
                      bmcMonitorUIConversion.CapacityInLiters = " > " + maxValueRow.Field<decimal>("CapacityinLiters");
                      bmcMonitorUIConversion.IsInrange = false;
                  }
                 else if (capacityinMM < minValueRow.Field<decimal>("CapacityinCMs"))
                  {
                      bmcMonitorUIConversion.CapacityInLiters = " < " + minValueRow.Field<decimal>("CapacityinLiters"); ;
                      bmcMonitorUIConversion.IsInrange = false;
                  }
                 else  if (data != null)
                 {
                     bmcMonitorUIConversion.CapacityInLiters = data.Field<decimal>("CapacityinLiters").ToString();
                 }
                 else
                 {
                     var lowerEndRow = CapacityLookUpTable.AsEnumerable().Where(x => x.Field<decimal>("CapacityinCMs") < capacityinMM).OrderByDescending(x => x.Field<decimal>("CapacityinCMs")).FirstOrDefault();
                     var highEndRow = CapacityLookUpTable.AsEnumerable().Where(x => x.Field<decimal>("CapacityinCMs") > capacityinMM).OrderBy(x => x.Field<decimal>("CapacityinCMs")).FirstOrDefault();
                     bmcMonitorUIConversion.CapacityInLiters = ((lowerEndRow.Field<decimal>("CapacityinLiters") + highEndRow.Field<decimal>("CapacityinLiters")) / 2).ToString();
                 }                
                  
              }
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelectCurrentProfileParametersOfBMC", "BMCMonitorDao");
            }
            return bmcMonitorUIConversion;

        }
      

        #endregion

      
        #region [Mapper]
        /// <summary>
        /// Get the MilkProperty
        /// </summary>
        /// <param name="dr">dr</param>
        /// <returns>MilkProperty</returns>
        BMCLatestParameter GetObject(DataRow dr,DataTable minMaxData)
        {
            try
            {

                objProfileParameter = new BMCLatestParameter();

                var minMaxPropertyRow = minMaxData.AsEnumerable().Where(x => x.Field<String>("PropertyName") == Db.ToString(dr["ParameterName"])).FirstOrDefault();
                if (minMaxPropertyRow != null)
                {
                    objProfileParameter.MinValue = Db.ToDouble(minMaxPropertyRow.Field<decimal>("MinValue"));
                    objProfileParameter.MaxValue = Db.ToDouble(minMaxPropertyRow.Field<decimal>("MaxValue"));
                    objProfileParameter.PropertyType = Db.ToString(minMaxPropertyRow.Field<string>("PropertyType"));          
                }
                else
                {
                    objProfileParameter.MinValue = Db.ToDouble(dr["MinValue"]);
                    objProfileParameter.MinValue = Db.ToDouble(dr["MaxValue"]);
                }

                objProfileParameter.Date=Db.ToDateTime(dr["Date"]);
                objProfileParameter.ParameterName= Db.ToString(dr["ParameterName"]);
                objProfileParameter.CaptruredValue= Db.ToDouble(dr["CapturedValue"]);
               
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "GetObject", "BMCMonitorDao"); 
            }
            return objProfileParameter;
        }
        #endregion

    }
}
