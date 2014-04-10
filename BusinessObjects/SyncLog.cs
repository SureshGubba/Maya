using System;
using System.Collections.Generic;
using System.Text;

namespace SchneiderMilkManagement.BusinessLayer.BusinessObjects
{
    public class SyncLog
    {
        public int SyncLogId { get; set; }
        public string Module { get; set; }
        public int EntityId { get; set; }
        public DateTime LastSyncDate { get; set; }
    }
}
