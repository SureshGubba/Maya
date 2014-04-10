using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;

namespace SchneiderMilkManagement.DataLayer.DataObjects
{

    public class AuditRecord
    {
        public String ColumnName{get;set;}
        public String OldValue{get;set;}
        public String NewValue {get;set;}
       public String ModifiedOn {get;set;}
       public String ModifiedBy {get;set;}
       public String ID {get;set;}
       public String ActionType{get;set;}
       public String BatchID { get; set; }
    }

    public class MilkPropertyAuditDao
    {
          private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

   
        #region [Insert Methods]
        /// <summary>
        /// Insert into MilkProperty
        /// </summary>
        /// <param name="objMilkProperty">objMilkProperty</param>
        /// <returns>int</returns>
        public void StoreAuditInformationForMilkProperty(MilkProperty objMilkProperty)
        {
            try
            {
                var modifiedBy = objMilkProperty.ModifiedBy;
                var batchID = Guid.NewGuid();
                var auditInsertstoredProcedure = "SP_tblMilkProperties_Audit_Insert";
                var InsertAction = "Inserted";
                var UpdatedAction = "Updated";
                var DeletedAction = "Deleted";

                var  auditRecords = new List<AuditRecord>();

                      auditRecords.Add( new AuditRecord(){ColumnName = "MilkPropertyId"});
                      auditRecords.Add( new AuditRecord(){ColumnName = "ProfileId"});
                      auditRecords.Add( new AuditRecord(){ColumnName = "Unit"});
                      auditRecords.Add( new AuditRecord(){ColumnName = "PortAddress"});
                      auditRecords.Add( new AuditRecord(){ColumnName = "MinValue"});
                      auditRecords.Add( new AuditRecord(){ColumnName = "MaxValue"});
                      auditRecords.Add( new AuditRecord(){ColumnName = "PollingTime"});
                      auditRecords.Add( new AuditRecord(){ColumnName = "SmsPollingTime"});
                      auditRecords.Add( new AuditRecord(){ColumnName = "IsSMSRequired"});
                      auditRecords.Add( new AuditRecord(){ColumnName = "AllowDelete"});
                      auditRecords.Add( new AuditRecord(){ColumnName = "CreatedBy"});
                      auditRecords.Add( new AuditRecord(){ColumnName = "ModifiedBy"});
                      auditRecords.Add(new AuditRecord() { ColumnName = "IsActive" });
                      auditRecords.Add(new AuditRecord() { ColumnName = "PropertyType" });
                
             
                var connection = new SqlConnection(connectionString);
                connection.Open();
                var command = new SqlCommand();

                var actionType = "";
                command.Connection = connection;
                command.CommandText = "Select count(1)  FROM [dbo].[tblMilkProperties] Where MilkPropertyId="+objMilkProperty.MilkPropertyId;
                command.CommandType = CommandType.Text;

                Int32 retValue = (Int32)command.ExecuteScalar();
                actionType = (retValue == 0) ? InsertAction : UpdatedAction;


                    auditRecords.Where(x => x.ColumnName == "AllowDelete").FirstOrDefault().NewValue = objMilkProperty.AllowDelete.ToString();
                    auditRecords.Where(x => x.ColumnName == "CreatedBy").FirstOrDefault().NewValue = objMilkProperty.CreatedBy.ToString();
                    auditRecords.Where(x => x.ColumnName == "IsActive").FirstOrDefault().NewValue = objMilkProperty.IsActive.ToString();
                    auditRecords.Where(x => x.ColumnName == "IsSMSRequired").FirstOrDefault().NewValue = objMilkProperty.IsSMSRequired.ToString();
                    auditRecords.Where(x => x.ColumnName == "MaxValue").FirstOrDefault().NewValue = objMilkProperty.MaxValue.ToString();
                    auditRecords.Where(x => x.ColumnName == "MilkPropertyId").FirstOrDefault().NewValue = objMilkProperty.MilkPropertyId.ToString();
                    auditRecords.Where(x => x.ColumnName == "MinValue").FirstOrDefault().NewValue = objMilkProperty.MinValue.ToString();
                    auditRecords.Where(x => x.ColumnName == "ModifiedBy").FirstOrDefault().NewValue = objMilkProperty.ModifiedBy.ToString();
                    auditRecords.Where(x => x.ColumnName == "PollingTime").FirstOrDefault().NewValue = objMilkProperty.PollingTime.ToString();
                    auditRecords.Where(x => x.ColumnName == "PortAddress").FirstOrDefault().NewValue = objMilkProperty.PortAddress;
                    auditRecords.Where(x => x.ColumnName == "ProfileId").FirstOrDefault().NewValue = objMilkProperty.ProfileId.ToString();
                    auditRecords.Where(x => x.ColumnName == "SmsPollingTime").FirstOrDefault().NewValue = objMilkProperty.SmsPollingTime.ToString();
                    auditRecords.Where(x => x.ColumnName == "Unit").FirstOrDefault().NewValue = objMilkProperty.Unit;
                    auditRecords.Where(x => x.ColumnName == "PropertyType").FirstOrDefault().NewValue = objMilkProperty.PropertyType.ToString();                    
              


                if (actionType == UpdatedAction)
                {
                    command.CommandText = @"SELECT [MilkPropertyId],[ProfileId],[PropertyId],[PropertyName1],[PropertyType],[Unit],[ProfileType1],[AllowDelete],[IsActive]
                                            ,[CreatedOn],[CreatedBy],[ModifiedOn],[ModifiedBy],[MinValue],[MaxValue],[PortAddress] ,[Address],[PollingTime],[SmsPollingTime],[IsSMSRequired]
                                          FROM [dbo].[tblMilkProperties]" +" Where MilkPropertyId=" + objMilkProperty.MilkPropertyId;
                    command.CommandType = CommandType.Text;

                    var previousData = new DataTable();
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(previousData);

                    if (previousData.Rows.Count > 0)
                    {
                        auditRecords.Where(x => x.ColumnName == "AllowDelete").FirstOrDefault().OldValue = previousData.Rows[0]["AllowDelete"].ToString();
                        auditRecords.Where(x => x.ColumnName == "CreatedBy").FirstOrDefault().OldValue = previousData.Rows[0]["CreatedBy"].ToString();
                        auditRecords.Where(x => x.ColumnName == "IsActive").FirstOrDefault().OldValue = previousData.Rows[0]["IsActive"].ToString();
                        auditRecords.Where(x => x.ColumnName == "IsSMSRequired").FirstOrDefault().OldValue = previousData.Rows[0]["IsSMSRequired"].ToString();
                        auditRecords.Where(x => x.ColumnName == "MaxValue").FirstOrDefault().OldValue = previousData.Rows[0]["MaxValue"].ToString();
                        auditRecords.Where(x => x.ColumnName == "MilkPropertyId").FirstOrDefault().OldValue = previousData.Rows[0]["MilkPropertyId"].ToString();
                        auditRecords.Where(x => x.ColumnName == "MinValue").FirstOrDefault().OldValue = previousData.Rows[0]["MinValue"].ToString();
                        auditRecords.Where(x => x.ColumnName == "ModifiedBy").FirstOrDefault().OldValue = previousData.Rows[0]["ModifiedBy"].ToString();
                        auditRecords.Where(x => x.ColumnName == "PollingTime").FirstOrDefault().OldValue = previousData.Rows[0]["PollingTime"].ToString();
                        auditRecords.Where(x => x.ColumnName == "PortAddress").FirstOrDefault().OldValue = previousData.Rows[0]["PortAddress"].ToString();
                        auditRecords.Where(x => x.ColumnName == "ProfileId").FirstOrDefault().OldValue = previousData.Rows[0]["ProfileId"].ToString();
                        auditRecords.Where(x => x.ColumnName == "SmsPollingTime").FirstOrDefault().OldValue = previousData.Rows[0]["SmsPollingTime"].ToString();
                        auditRecords.Where(x => x.ColumnName == "Unit").FirstOrDefault().OldValue = previousData.Rows[0]["Unit"].ToString();
                        auditRecords.Where(x => x.ColumnName == "PropertyType").FirstOrDefault().OldValue = previousData.Rows[0]["PropertyType"].ToString();
                    }
                }
                 auditRecords.ToList().ForEach(x =>             
                 {
                     if ((x.OldValue==null) || (x.OldValue.ToString() != x.NewValue.ToString()))
                     {
                            command = new SqlCommand();
                            command.CommandText = auditInsertstoredProcedure;
                            command.CommandType = CommandType.StoredProcedure;
                            command.Connection = connection;

                            command.Parameters.AddWithValue("@ColumnName",x.ColumnName);
                            command.Parameters.AddWithValue("@OldValue",x.OldValue??String.Empty);
                            command.Parameters.AddWithValue("@NewValue", x.NewValue ?? String.Empty);
                            command.Parameters.AddWithValue("@ModifiedOn",DateTime.Now);
                            command.Parameters.AddWithValue("@ModifiedBy",  objMilkProperty.ModifiedBy);
                            command.Parameters.AddWithValue("@ID",Guid.NewGuid());
                            command.Parameters.AddWithValue("@ActionType",actionType);
                            command.Parameters.AddWithValue("@BatchID", batchID);
                            command.ExecuteNonQuery();
                     }
                });
          
        }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "InsertMilkProperty", "MilkPropertyDao"); 
            }
           
        }

        #endregion

   
    }
}
