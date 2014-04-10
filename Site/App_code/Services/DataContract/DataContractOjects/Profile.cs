using System;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SchneiderMilkManagement.Services.DataContract.DataContractObjects
{
    [XmlRoot("Profile")]
    public class Profile
    {
        [XmlElement]
        public string ProfileName { get; set; }
        [XmlElement]    
        public ProfileParameter[] ProfileParameter { get; set; }
    }
}
