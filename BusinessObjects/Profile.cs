using System;
using System.Collections.Generic;
using System.Text;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects.BusinessRules;

namespace SchneiderMilkManagement.BusinessLayer.BusinessObjects
{
    public class Profile : BusinessObject
    {
        public Profile()
        {
            AddRule(new ValidateId("ProfileId"));
            AddRule(new ValidateRequired("Name"));
            AddRule(new ValidateRequired("Value"));
            AddRule(new ValidateLength("Name", 0, 100));
            AddRule(new ValidateLength("Value", 0, 100));               
        }
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }       
    }
}
