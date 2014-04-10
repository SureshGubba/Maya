using System;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SchneiderMilkManagement.Services.DataContract.DataContractObjects
{
    [XmlRoot("Profiles")]
    public class Profiles
    {        
        [XmlElement]
        public Profile[] Profile { get; set; }
    }
}
