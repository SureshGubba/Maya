using System;
using System.Collections.Generic;
using System.Text;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;
using SchneiderMilkManagement.DataLayer.DataObjects;

namespace SchneiderMilkManagement.BusinessLayer.BusinessFacade
{
    public class UserFacade : IFacade
    {
        /// <summary>
        /// Insert User
        /// </summary>
        /// <param name="ObjUser">ObjUser</param>
        /// <returns>int</returns>
        public int Insert(User ObjUser)
        {
            int retValue = 0;
            try
            {
                retValue =  (new UserDao()).Insert(ObjUser);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "Insert", "UserFacade");
            }
            return retValue;
            
        }

        /// <summary>
        /// Upadte The User
        /// </summary>
        /// <param name="ObjUser">ObjUser</param>
        /// <returns>int</returns>
        public int Update(User ObjUser)
        {
            int retValue = 0;
            try
            {
                retValue =  (new UserDao()).Update(ObjUser);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "Update", "UserFacade");
            }
            return retValue;
            
        }

        /// <summary>
        /// Update The Mobile
        /// </summary>
        /// <param name="ObjUser">ObjUser</param>
        /// <returns>int</returns>
        public int UpdateMobile(User ObjUser)
        {
            int retValue = 0;
            try
            {
                retValue = (new UserDao()).UpdateMobile(ObjUser);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "UpdateMobile", "UserFacade");
            }
            return retValue;
           
        }

        /// <summary>
        /// Select All User By Paging
        /// </summary>
        /// <param name="SortBy">SortBy</param>
        /// <param name="SearchString">SearchString</param>
        /// <param name="maximumRows">maximumRows</param>
        /// <param name="startRowIndex">startRowIndex</param>
        /// <returns>IList<User></returns>
        public IList<User> SelAllByPaging(string SortBy, string SearchString, int maximumRows, int startRowIndex)
        {
            IList<User> objUserList = null;
            try
            {
                objUserList=(new UserDao()).SelAllByPaging(SortBy, SearchString, maximumRows, startRowIndex);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelAllByPaging", "UserFacade");
            }
            return objUserList;
        }

        /// <summary>
        /// Select Count Of User By SearchString
        /// </summary>
        /// <param name="SortBy">SortBy</param>
        /// <param name="SearchString">SearchString</param>
        /// <returns>int</returns>
        public int SelCount(string SortBy, string SearchString)
        {
            int retValue = 0;
            try
            {
                retValue = (new UserDao()).SelCount(SortBy, SearchString);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "SelCount", "UserFacade");
            }
            return retValue;
            
        }

        /// <summary>
        /// Get User By UserName
        /// </summary>
        /// <param name="UserName">UserName</param>
        /// <returns>User</returns>
        public User GetUserByUserName(String UserName)
        {
            User objUser= null;
            try
            {
                objUser = (new UserDao()).GetUserByUserName(UserName);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "GetUserByUserName", "UserFacade");
            }
            return objUser;
            
        }

        /// <summary>
        /// Select User By UserId
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns>User</returns>
        public User SelByUserId(int UserId)
        {
            User objUser = null;
            try
            {
                objUser = (new UserDao()).SelByUserId(UserId);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "GetUserByUserName", "UserFacade");
            }
            return objUser;
            
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
            int retValue = 0;
            try
            {
                retValue = (new UserDao()).ChangePassword(UserName, OldPassword, NewPassword);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "ChangePassword", "UserFacade");
            }
            return retValue;
            
        }

        /// <summary>
        /// Check User Login By UserName And Password
        /// </summary>
        /// <param name="UserName">UserName</param>
        /// <param name="Password">Password</param>
        /// <returns>User</returns>
        public User CheckLogin(String UserName, String Password)
        {
            User objUser = null;
            try
            {
                objUser = (new UserDao()).CheckLogin(UserName, Password);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "CheckLogin", "UserFacade");
            }
            return objUser;
            
        }

        /// <summary>
        /// Delete User By UserIds
        /// </summary>
        /// <param name="Ids">Ids</param>
        /// <returns>int</returns>
        public override int DeleteWithArray(String Ids)
        {
            int retValue = 0;
            try
            {
                retValue = (new UserDao()).DeleteWithArray(Ids);
            }
            catch (Exception ex)
            {
                Db.ErrorLog(ex, ex.Message, "DeleteWithArray", "UserFacade");
            }
            return retValue;
            
        }
    }
}
