using System;
using System.Collections.Generic;
using System.Text;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects.BusinessRules;

namespace SchneiderMilkManagement.BusinessLayer.BusinessObjects
{
    public class BMCLatestParameter : BusinessObject
    {
        public BMCLatestParameter()
        {
                     
        }
        public string ParameterName { get; set; }
        public double CaptruredValue { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public DateTime Date { get; set; }
        public int UIOrder { get; set; }
        public string PropertyType { get; set; }
    }
}
