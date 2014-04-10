using System;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SchneiderMilkManagement.Services.DataContract.DataContractObjects
{
    [XmlRoot("ProfileParameter")]
    public class ProfileParameter
    {
        [XmlElement]
        public string ParameterName { get; set; }
        [XmlElement]
        public double CaptruredValue { get; set; }
        [XmlElement]
        public double MinValue { get; set; }
        [XmlElement]
        public double MaxValue { get; set; }
        [XmlElement]
        public DateTime Date { get; set; }
    }
}
