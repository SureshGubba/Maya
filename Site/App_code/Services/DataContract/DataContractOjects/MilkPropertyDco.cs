using System;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SchneiderMilkManagement.Services.DataContract.DataContractObjects
{
    public class MilkPropertyDco
    {
        public int MilkPropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public string Unit { get; set; }
        public string PropertyAddress { get; set; }
        public string Address { get; set; }
        public double PollingTime { get; set; }
        public int SmsPollingTime { get; set; }
        public bool IsSMSRequired { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public string ProfileType { get; set; }
        public bool AllowDelete { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
    }
}