using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using SchneiderMilkManagement.Services.DataContract;
using SchneiderMilkManagement.Services.ResponseBase;

namespace SchneiderMilkManagement.Services.ResponseBase
{
    public class Response
    {
        public Status Status = Status.Success;       
        public String Message { get; set; }
        public string CurrentTime { get; set; }      
        public String ServerTime { get; set; }      
        public int Count { get; set; }
        public string MobileNo { get; set; }
    }
}
