using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;

namespace SchneiderMilkManagement.DataLayer.DataObjects
{
    public class UserDao
    {
        #region [Member parameters]
        IList<User> ObjUsers;
        User ObjUser;
        DataTable dt;
        DataRow dr;
        int intReturn;

        #endregion

        #region [Insert Method]
        /// <summary>
        /// Insert Into User
        /// </summary>
        /// <param name="ObjUser">ObjUser</param>
        /// <returns>int </returns>
 
        public int Insert(User ObjUser)
        {
            try
            {
                DbParam[] param = new DbParam[6];

                param[0] = new DbParam("@FirstName", ObjUser.FirstName, SqlDbType.VarChar);
                param[1] = new DbParam("@LastName", ObjUser.LastName, SqlDbType.VarChar);
                param[2] = new DbParam("@UserName", ObjUser.Username, SqlDbType.VarChar);
                param[3] = new DbParam("@IsSuperAdmin", ObjUser.IsSuperAdmin, SqlDbType.Bit);
                param[4] = new DbParam("@Password", ObjUser.Password, SqlDbType.VarChar);
                param[5] = new DbParam("@Email", ObjUser.Email, SqlDbType.VarChar);

                intReturn = Db.Insert("SP_tblUser_Ins", param, true);
                ObjUser.UserId = intReturn;
                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "Insert", "UserDao");
            }
            return intReturn;

        }

        #endregion

        #region [Update Method]
        /// <summary>
        /// Upadte The User
        /// </summary>
        /// <param name="ObjUser">ObjUser</param>
        /// <returns>int</returns>
        public int Update(User ObjUser)
        {
            try
            {
                DbParam[] param = new DbParam[5];

                param[0] = new DbParam("@UserId", ObjUser.UserId, SqlDbType.Int);
                param[1] = new DbParam("@FirstName", ObjUser.FirstName, SqlDbType.VarChar);
                param[2] = new DbParam("@LastName", ObjUser.LastName, SqlDbType.VarChar);
                param[3] = new DbParam("@Email", ObjUser.Email, SqlDbType.VarChar);
                param[4] = new DbParam("@UserName", ObjUser.Username, SqlDbType.VarChar);

                intReturn = Db.Insert("SP_tblUser_Upd", param, true);

                
            }
            catch (Exception ex) 
            {

                Db.ErrorLog(ex, ex.Message, "Update", "UserDao");
            }
            return intReturn;
        }

        /// <summary>
        /// Update The Mobile
        /// </summary>
        /// <param name="ObjUser">ObjUser</param>
        /// <returns>int</returns>
        public int UpdateMobile(User ObjUser)
        {
            try
            {
                DbParam[] param = new DbParam[2];
                param[0] = new DbParam("@UserId", ObjUser.UserId, SqlDbType.Int);
                param[1] = new DbParam("@MobileNo", ObjUser.MobileNo, SqlDbType.VarChar);
                intReturn = Db.Insert("SP_tblUser_UpdateMobile", param, true);

                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "UpdateMobile", "UserDao");
            }
            return intReturn;
        }

        #endregion

        #region [Select Methods]
        /// <summary>
        /// Select User By Paging
        /// </summary>
        /// <param name="SortExpression">SortExpression</param>
        /// <param name="SearchString">SearchString</param>
        /// <param name="maximumRows">maximumRows</param>
        /// <param name="startRowIndex">startRowIndex</param>
        /// <returns>IList<User></returns>
        public IList<User> SelAllByPaging(string SortExpression, string SearchString, int maximumRows, int startRowIndex)
        {
            try
            {
                DbParam[] param = new DbParam[4];

                param[0] = new DbParam("@SortBy", SortExpression, SqlDbType.VarChar);
                param[1] = new DbParam("@SearchString", SearchString, SqlDbType.VarChar);
                param[2] = new DbParam("@maximumRows", maximumRows, SqlDbType.Int);
                param[3] = new DbParam("@startRowIndex", startRowIndex, SqlDbType.Int);

                dt = Db.GetDataTable("sp_tblUsers_SELAllByPaging", param);

                if (dt != null)
                {
                    ObjUsers = new List<User>();

                    foreach (DataRow row in dt.Rows)
                    {
                        ObjUsers.Add(GetObject(row));
                    }
                }

                
            }
            catch (Exception ex) 
            {
                Db.ErrorLog(ex, ex.Message, "SelAllByPaging", "UserDao");
            }
            return ObjUsers;
        }


        /// <summary>
        /// Select Count Of User By SearchString
        /// </summary>
        /// <param name="sortExpression">sortExpression</param>
        /// <param name="SearchString">SearchString</param>
        /// <returns>int</returns>
        public int SelCount(string sortExpression, string SearchString)
        {
            int UserCount = 0;
            try
            {
                DbParam[] param = new DbParam[1];
                
                object obj;

                param[0] = new DbParam("@SearchString", SearchString, SqlDbType.VarChar);

                obj = Db.GetScalar("sp_tblUsers_SELCount_Search", param);

                if (obj != null)
                {
                    UserCount = Db.ToInteger(obj);
                }
               
            }
            catch (Exception ex) 
            {
                Db.ErrorLog(ex, ex.Message, "SelCount", "UserDao");
            }
            return UserCount;
        }

        /// <summary>
        /// Select User By UserName
        /// </summary>
        /// <param name="UserName">UserName</param>
        /// <returns>User</returns>
        public User GetUserByUserName(String UserName)
        {
            try
            {
                String[] Password = new String[2];
                DbParam[] param = new DbParam[1];

                param[0] = new DbParam("@UserName", UserName, SqlDbType.VarChar);

                dr = Db.GetDataRow("SP_tblUser_GetUserByUserName", param);
                ObjUser = new User();
                if (dr != null)
                {
                    ObjUser.UserId = Db.ToInteger(dr["UserId"]);
                    ObjUser.FirstName = Db.ToString(dr["FirstName"]);
                    ObjUser.LastName = Db.ToString(dr["LastName"]);
                    ObjUser.Username = Db.ToString(dr["UserName"]);
                    ObjUser.IsSuperAdmin = Db.ToBoolean(dr["IsSuperAdmin"]);
                    ObjUser.Role = Db.ToString(dr["Role"]);
                    ObjUser.Email = Db.ToString(dr["Email"]);
                    ObjUser.Password = Db.ToString(dr["Password"]);
                }
              
            }
            catch (Exception ex) 
            {
                Db.ErrorLog(ex, ex.Message, "GetUserByUserName", "UserDao");
            }
            return ObjUser;
        }

        /// <summary>
        /// Check Login By UserName And Password
        /// </summary>
        /// <param name="UserName">UserName</param>
        /// <param name="Password">Password</param>
        /// <returns>User</returns>
        public User CheckLogin(String UserName, String Password)
        {
            try
            {
                DbParam[] param = new DbParam[2];

                param[0] = new DbParam("@UserName", UserName, SqlDbType.VarChar);
                param[1] = new DbParam("@Password", Password, SqlDbType.VarChar);

                dt = new DataTable();
                dt = Db.GetDataTable("SP_tblUser_CheckLogin", param);
                ObjUser = new User();

                if (dt != null)
                {
                    ObjUser.UserId = Db.ToInteger(dt.Rows[0]["UserId"]);
                    ObjUser.FirstName = Db.ToString(dt.Rows[0]["FirstName"]);
                    ObjUser.LastName = Db.ToString(dt.Rows[0]["LastName"]);
                    ObjUser.Username = Db.ToString(dt.Rows[0]["UserName"]);
                    ObjUser.IsSuperAdmin = Db.ToBoolean(dt.Rows[0]["IsSuperAdmin"]);
                    ObjUser.Role = Db.ToString(dt.Rows[0]["Role"]);
                    ObjUser.Password = Db.ToString(dt.Rows[0]["Password"]);
                    ObjUser.Email = Db.ToString(dt.Rows[0]["Email"]);
                }
               
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "CheckLogin", "UserDao");
            }
            return ObjUser;
        }

        /// <summary>
        /// Select User By UserId
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns>User</returns>
        public User SelByUserId(int UserId)
        {
            try
            {
                DbParam[] param = new DbParam[1];

                param[0] = new DbParam("@UserId", UserId, SqlDbType.Int);

                dt = new DataTable();
                dt = Db.GetDataTable("sp_tblUser_SelById", param);
                ObjUser = new User();

                if (dt != null)
                {
                    ObjUser.UserId = Db.ToInteger(dt.Rows[0]["UserId"]);
                    ObjUser.FirstName = Db.ToString(dt.Rows[0]["FirstName"]);
                    ObjUser.LastName = Db.ToString(dt.Rows[0]["LastName"]);
                    ObjUser.Username = Db.ToString(dt.Rows[0]["UserName"]);
                    ObjUser.IsSuperAdmin = Db.ToBoolean(dt.Rows[0]["IsSuperAdmin"]);
                    ObjUser.Role = Db.ToString(dt.Rows[0]["Role"]);
                    ObjUser.Email = Db.ToString(dt.Rows[0]["Email"]);
                    ObjUser.MobileNo = Db.ToString(dt.Rows[0]["MobileNo"]);
                }
               
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelByUserId", "UserDao");
            }
            return ObjUser;

        }

        /// <summary>
        /// Change The Password Of User By UserName
        /// </summary>
        /// <param name="UserName">UserName</param>
        /// <param name="OldPassword">OldPassword</param>
        /// <param name="NewPassword">NewPassword</param>
        /// <returns>int</returns>
        public int ChangePassword(String UserName, String OldPassword, String NewPassword)
        {
            DbParam[] param = new DbParam[3];


            param[0] = new DbParam("@UserName", UserName, SqlDbType.VarChar);
            param[1] = new DbParam("@OldPassword", OldPassword, SqlDbType.VarChar);
            param[2] = new DbParam("@NewPassword", NewPassword, SqlDbType.VarChar);

            intReturn = Db.Update("SP_tblUser_ChangePassword_New", param, true);

            return intReturn;
        }

        #endregion

        #region [Delete Method]
        /// <summary>
        /// Delete The User By UserId
        /// </summary>
        /// <param name="Ids">Ids</param>
        /// <returns>int</returns>
        public int DeleteWithArray(String Ids)
        {
            try
            {
                DbParam[] param = new DbParam[1];
                param[0] = new DbParam("@Ids", Ids, SqlDbType.VarChar);

                intReturn = Db.Update("sp_tblUsers_DELWithArray", param);

                
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "DeleteWithArray", "UserDao");
            }
            return intReturn;
        }


        #endregion

        #region [Mapper]
        /// <summary>
        /// Get The User
        /// </summary>
        /// <param name="dr">dr</param>
        /// <returns>User</returns>
        User GetObject(DataRow dr)
        {
            ObjUser = new User();

            ObjUser.UserId = Db.ToInteger(dr["UserId"]);
            ObjUser.FirstName = Db.ToString(dr["FirstName"]);
            ObjUser.LastName = Db.ToString(dr["LastName"]);
            ObjUser.Username = Db.ToString(dr["UserName"]);
            ObjUser.IsSuperAdmin = Db.ToBoolean(dr["IsSuperAdmin"]);
            ObjUser.Role = Db.ToString(dr["Role"]);
            ObjUser.Password = Db.ToString(dr["Password"]);
            ObjUser.Email = Db.ToString(dr["Email"]);

            return ObjUser;
        }

        #endregion

    }
}
