using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using SchneiderMilkManagement.Services.DataContract.DataContractObjects;
using System.Xml.Serialization;

namespace SchneiderMilkManagement.Services.Response
{
    public class MilkPropertyResponse : ResponseBase.Response
    {
        [XmlArray("MilkProperties")]
        [XmlArrayItem("MilkProperty")]
        public MilkPropertyDco[] objMilkProperties { get; set; }
    }
}