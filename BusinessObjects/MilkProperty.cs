using System;
using System.Collections.Generic;
using System.Text;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects.BusinessRules;

namespace SchneiderMilkManagement.BusinessLayer.BusinessObjects
{
    public class MilkProperty : BusinessObject
    {
        public MilkProperty()
        {
            AddRule(new ValidateId("MilkPropertyId"));
            AddRule(new ValidateId("PropertyId"));
            AddRule(new ValidateId("ProfileId"));
            AddRule(new ValidateRequired("ProfileType"));
            AddRule(new ValidateRequired("PropertyName"));
            AddRule(new ValidateRequired("PropertyType"));
            AddRule(new ValidateRequired("Unit"));
            AddRule(new ValidateRequired("MinValue"));
            AddRule(new ValidateLength("MinValue",0,10));
            AddRule(new ValidateRequired("MaxValue"));
            AddRule(new ValidateLength("MaxValue",0,10));
            AddRule(new ValidateRequired("SmsPollingTime"));            
        }
        public int MilkPropertyId { get; set; }
        public int PropertyId { get; set; }
        public int ProfileId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public string Unit { get; set; }
        public string PortAddress { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public string ProfileType { get; set; }
        public string Address { get; set; }
        public double PollingTime { get; set; }
        public int SmsPollingTime { get; set; }
        public Boolean IsSMSRequired { get; set; }
        public bool AllowDelete { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }        
    }
}
