using System;
using System.Collections.Generic;
using System.Text;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using SchneiderMilkManagement.DataLayer.DataObjects;

namespace SchneiderMilkManagement.BusinessLayer.BusinessFacade
{
    public abstract class IFacade
    {
        public abstract int DeleteWithArray(string IdStr);
    }
}
