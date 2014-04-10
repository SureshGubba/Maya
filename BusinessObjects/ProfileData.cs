using System;
using System.Collections.Generic;
using System.Text;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects.BusinessRules;

namespace SchneiderMilkManagement.BusinessLayer.BusinessObjects
{
    public class ProfileData : BusinessObject
    {
        public ProfileData()
        {           
            AddRule(new ValidateRequired("ProfileName"));            
        }
        public int ProfileDataId { get; set; }
        public string ProfileName { get; set; }
        public int ProfileId { get; set; }
        public ProfileParameter[] ProfileParameters { get; set; }
        public string ParameterName { get; set; }
        public double CapturedValue { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public DateTime Date { get; set; }
    }
}
