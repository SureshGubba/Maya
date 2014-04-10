using System;
using System.Collections.Generic;
using System.Text;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects.BusinessRules;

namespace SchneiderMilkManagement.BusinessLayer.BusinessObjects
{
    public class Property : BusinessObject
    {
        public Property()
        {
            AddRule(new ValidateId("ProfileId"));            
            AddRule(new ValidateRequired("Name"));            
        }
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int ProfileId { get; set; }
        public string ProfileName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int UIOrder { get; set; }
    }
}
