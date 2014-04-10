using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using SchneiderMilkManagement.Services.DataContract.DataContractObjects;
using System.Xml.Serialization;

namespace SchneiderMilkManagement.Services.Response
{
    public class InsertProfileDataResponse : ResponseBase.Response
    {         
        [XmlElement("Profile")]
        public Profile Profile { get; set; }
    }
}