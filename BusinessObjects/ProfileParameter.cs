using System;
using System.Collections.Generic;
using System.Text;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects.BusinessRules;

namespace SchneiderMilkManagement.BusinessLayer.BusinessObjects
{
    public class ProfileParameter : BusinessObject
    {
        public ProfileParameter()
        {
            AddRule(new ValidateRequired("ParameterName"));
            AddRule(new ValidateRequired("CaptruredValue"));
            AddRule(new ValidateRequired("MinValue"));
            AddRule(new ValidateRequired("MaxValue"));
            AddRule(new ValidateRequired("Date"));
            AddRule(new ValidateLength("MinValue", 0, 10));
            AddRule(new ValidateLength("MaxValue", 0, 10));            
        }
        public string ParameterName { get; set; }
        public double CaptruredValue { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public DateTime Date { get; set; }
    }
}
