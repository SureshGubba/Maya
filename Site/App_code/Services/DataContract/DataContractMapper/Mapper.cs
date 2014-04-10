using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SchneiderMilkManagement.Services.DataContract.DataContractObjects;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using System.Configuration;

namespace SchneiderMilkManagement.Services.DataContract.DataContractMapper
{
    public static class Mapper
    {
        #region [MilkProperty]
        /// <summary>
        /// To DataContractObjects
        /// </summary>
        /// <param name="objMilkProperty">objMilkProperty</param>
        /// <returns>MilkPropertyDco</returns>
        public static MilkPropertyDco ToDataContractObjects(MilkProperty objMilkProperty)
        {
            MilkPropertyDco MilkPropertyDco = new MilkPropertyDco();
            MilkPropertyDco.MilkPropertyId = objMilkProperty.MilkPropertyId;
            MilkPropertyDco.PropertyName = objMilkProperty.PropertyName;
            MilkPropertyDco.PropertyType = objMilkProperty.PropertyType;
            MilkPropertyDco.Unit = objMilkProperty.Unit ;
            MilkPropertyDco.PropertyAddress = objMilkProperty.PortAddress;
            MilkPropertyDco.Address = objMilkProperty.Address;
            MilkPropertyDco.PollingTime = objMilkProperty.PollingTime;
            MilkPropertyDco.SmsPollingTime = objMilkProperty.SmsPollingTime;
            MilkPropertyDco.IsSMSRequired = objMilkProperty.IsSMSRequired;
            MilkPropertyDco.MinValue = objMilkProperty.MinValue;
            MilkPropertyDco.MaxValue = objMilkProperty.MaxValue;
            MilkPropertyDco.ProfileType = objMilkProperty.ProfileType;
            MilkPropertyDco.AllowDelete = objMilkProperty.AllowDelete;
            MilkPropertyDco.IsActive = objMilkProperty.IsActive;
            MilkPropertyDco.CreatedOn = objMilkProperty.CreatedOn;
            MilkPropertyDco.CreatedBy = objMilkProperty.CreatedBy;
            MilkPropertyDco.ModifiedOn = objMilkProperty.ModifiedOn;
            MilkPropertyDco.ModifiedBy = objMilkProperty.ModifiedBy;
            return MilkPropertyDco;
        }

        /// <summary>
        /// FromDataContractObjects
        /// </summary>
        /// <param name="ArrProfile">ArrProfile</param>
        /// <returns>ProfileData</returns>
        public static ProfileData[] FromDataContractObjects(SchneiderMilkManagement.Services.DataContract.DataContractObjects.Profile[] ArrProfile)
        {
            ProfileData[] ArrProfileData = new ProfileData[ArrProfile.Length];
            int index = 0;
            foreach (SchneiderMilkManagement.Services.DataContract.DataContractObjects.Profile objProfile in ArrProfile)
            {
                ArrProfileData[index] = new ProfileData();
                ProfileData ProfileData = ArrProfileData[index];
                ProfileData.ProfileName = objProfile.ProfileName;
                int length = objProfile.ProfileParameter.Length;
                int i = 0;
                ProfileData.ProfileParameters = new SchneiderMilkManagement.BusinessLayer.BusinessObjects.ProfileParameter[length];
                foreach (SchneiderMilkManagement.Services.DataContract.DataContractObjects.ProfileParameter objProfileParameter in objProfile.ProfileParameter)
                {
                    ProfileData.ProfileParameters[i] = new BusinessLayer.BusinessObjects.ProfileParameter();
                    ProfileData.ProfileParameters[i].ParameterName = objProfileParameter.ParameterName;
                    ProfileData.ProfileParameters[i].CaptruredValue = objProfileParameter.CaptruredValue;
                    ProfileData.ProfileParameters[i].MinValue = objProfileParameter.MinValue;
                    ProfileData.ProfileParameters[i].MaxValue = objProfileParameter.MaxValue;
                    ProfileData.ProfileParameters[i].Date = objProfileParameter.Date;
                    i++;
                }
                index++;
            }
            return ArrProfileData;
        }

        /// <summary>
        /// ToDataContractObjects
        /// </summary>
        /// <param name="onjMilkProperties">onjMilkProperties</param>
        /// <returns>MilkPropertyDco</returns>
        public static MilkPropertyDco[] ToDataContractObjects(IEnumerable<MilkProperty> onjMilkProperties)
        {
            if (onjMilkProperties != null)
                return onjMilkProperties.Select(f => ToDataContractObjects(f)).ToArray();
            else
                return null;
        }

        #endregion
      
    }
}

