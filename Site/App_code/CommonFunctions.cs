using System;
using System.Web;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Collections;
using System.Security.Cryptography;
using System.Xml;

using System.Xml.Serialization;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects;

namespace SchneiderMilkManagement
{
    public static class CommonFunctions
    {
        public static void ErrorLog(Exception ex, string message, string funcitonName, string moduleName)
        {
            try
            {
                (new WINIT.ErrorLog.ErrorLog()).Log(ex, message, funcitonName, moduleName);
            }
            catch
            {

            }
        }
        
        #region [Type casting]

        /// <summary>
        /// Procedure Name - getIntValue
        /// Procedure Type - User Defined Function
        /// Return Type - Int32
        /// Parameters - objTest: Object
        /// Description - This is the User Defined Function for retreiving integer value
        /// </summary>
        /// <param name="objTest">objTest</param>
        /// <returns>Int Value</returns>
        /// 

        public static Int32 getIntValue(object objTest)
        {
            Int32 functionReturnValue = default(Int32);
            try
            {
                if (((objTest != null)) && ((!object.ReferenceEquals(objTest, System.DBNull.Value))))
                {
                    functionReturnValue = Convert.ToInt32(objTest);
                }
                else if (((objTest == null)) || ((object.ReferenceEquals(objTest, System.DBNull.Value))))
                {
                    functionReturnValue = 0;
                }

            }
            catch
            {
                functionReturnValue = 0;
            }

            return functionReturnValue;

        }

        public static string GetFormattedDouble(object obj)
        {
            try
            {
                if (obj != null && (!ReferenceEquals(obj, DBNull.Value)) && obj.ToString() != "")
                {
                    //return string.Format("{0:#,###,###.##}", Math.Round(Convert.ToDouble(obj.ToString()), 2).ToString());
                    string value = obj.ToString();
                    string formattedValue = "---";

                    if (value.Length == 0 || double.Parse(value) == 0)
                    {
                        formattedValue = "---";
                    }
                    else
                    {
                        double amount = double.Parse(value);
                        string fomrat = "{0:0,0}";

                        if (amount > Math.Floor(amount))
                        {
                            fomrat = "{0:0,0.00}";
                        }

                        if (float.Parse(value) < 0)
                        {
                            formattedValue = "<span style='color: red;'>(" + String.Format(fomrat, (0 - double.Parse(value))) + ")</span>";
                        }
                        else
                        {
                            formattedValue = String.Format(fomrat, double.Parse(value));
                        }
                    }
                    return formattedValue;
                }
            }
            catch { }
            return "0.00";
        }

        public static string GetStringValue(object objTest)
        {
            try
            {
                return (objTest != null) && (!object.ReferenceEquals(objTest, System.DBNull.Value)) ? objTest.ToString() : "";
            }
            catch { }
            return "";
        }

        public static string GetSubstring(object objTest, int length)
        {
            string strValue = "";
            if ((!object.ReferenceEquals(objTest, System.DBNull.Value)))
            {
                strValue = Convert.ToString(objTest);

                if (strValue.Length > length)
                {
                    strValue = strValue.Substring(0, (length > 3) ? length - 3 : length) + "...";
                }
            }

            return strValue;
        }

        /// <summary>
        /// Procedure Name - getDoubleValue
        /// Procedure Type - User Defined Function
        /// Return Type - Double
        /// Parameters - objTest: Object
        /// Description - This is the User Defined Function for retreiving double value
        /// </summary>
        /// <param name="objTest"></param>
        /// <returns>Double Value</returns>
        public static double getDoubleValue(object objTest)
        {
            double functionReturnValue = 0;
            try
            {

                if ((!object.ReferenceEquals(objTest, System.DBNull.Value)))
                {
                    functionReturnValue = Convert.ToDouble(objTest);
                }
                else if ((object.ReferenceEquals(objTest, System.DBNull.Value)))
                {
                    functionReturnValue = 0.0;
                }

            }
            catch
            {
                functionReturnValue = 0.0;
            }

            return functionReturnValue;

        }

        /// <summary>
        /// Procedure Name - getDecimalValue
        /// Procedure Type - User Defined Function
        /// Return Type - Decimal
        /// Parameters - objTest: Object
        /// Description - This is the User Defined Function for retreiving Decimal value
        /// </summary>
        /// <param name="objTest"></param>
        /// <returns></returns>
        public static decimal getDecimalValue(object objTest)
        {
            decimal functionReturnValue = default(decimal);
            if ((!object.ReferenceEquals(objTest, System.DBNull.Value)))
            {
                functionReturnValue = Convert.ToDecimal(objTest);
            }
            else if ((object.ReferenceEquals(objTest, System.DBNull.Value)))
            {
                functionReturnValue = (decimal)0.0;
            }
            return functionReturnValue;
        }

        /// <summary>
        /// Gets the boolean 
        /// </summary>
        /// <param name="objStatus"></param>
        /// <returns>Boolean</returns>
        public static bool getBooleanValue(object objStatus)
        {
            bool returnVal = false;

            if ((!object.ReferenceEquals(objStatus, System.DBNull.Value)))
            {
                if (objStatus.ToString().ToLower() == "true" || objStatus.ToString().ToLower() == "1" || objStatus.ToString().ToLower() == "y" || objStatus.ToString().ToLower() == "active")
                {
                    returnVal = true;
                }

            }

            return returnVal;
        }

        /// <summary>
        /// Get the date in MMDDYYYY format
        /// </summary>
        /// <param name="strDate">Date as string</param>
        /// <returns>Date String</returns>
        public static string getDateTimeDDMMYYY(string strDate)
        {
            string strMMDDyyyy = string.Empty;
            if (string.IsNullOrEmpty(strDate) == false)
            {

                string[] arrDate = strDate.Split(new char[] { '.', '/' });
                if (arrDate.Length == 3)
                {
                    //DateTime dtDate = new DateTime(Convert.ToInt32(arrDate[2].ToString()), Convert.ToInt32(arrDate[0].ToString()), Convert.ToInt32(arrDate[1].ToString()));
                    strMMDDyyyy = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                }
                else if ((arrDate.Length == 2) || (arrDate.Length == 1))
                {
                    strMMDDyyyy = "01/01/1900";
                }

            }
            else if (string.IsNullOrEmpty(strDate) == true)
            {
                strMMDDyyyy = "01/01/1900";
            }
            return strMMDDyyyy;

        }
        #endregion

        public static Boolean SendEmailWithTemplate(string toEmail, string fromEmail, string emailSubject, string emailBody, string strCC, string replyTo, string sendersName, string strBCC)
        {
            try
            {
                string strBody = emailBody;
                MailMessage mailMsg = new MailMessage();
                SmtpClient client = new SmtpClient();
                if (sendersName.Length > 0)
                {
                    mailMsg.From = new MailAddress(fromEmail, sendersName);
                }
                else
                {
                    mailMsg.From = new MailAddress(fromEmail);
                }
                mailMsg.To.Add(toEmail);
                if (strCC != "")
                {
                    mailMsg.CC.Add(strCC);
                }
                if (strBCC != "")
                {
                    mailMsg.Bcc.Add(strBCC);
                }

                mailMsg.Subject = emailSubject;
                mailMsg.Body = emailBody;
                mailMsg.IsBodyHtml = true;
                mailMsg.Priority = MailPriority.Normal;
                client.Host = ConfigurationManager.AppSettings["SMTPServer"];

                string smtphost = ConfigurationManager.AppSettings["SMTPServer"].ToString();
                int smtpport = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPORT"]);
                string smtpuser = ConfigurationManager.AppSettings["FROMEMAIL"].ToString();
                string smtppwd = ConfigurationManager.AppSettings["FROMPWD"].ToString();

                client.Send(mailMsg);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static bool SendEmail(string fromEmail, string toEmail, string subject, string message, bool isHTML, ref string response, string cc, IList<Attachment> attachments)
        {
            string smtphost = ConfigurationManager.AppSettings["SMTPServer"].ToString();
            int smtpport = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPORT"]);
            string smtpuser = ConfigurationManager.AppSettings["FROMEMAIL"].ToString();
            string smtppwd = ConfigurationManager.AppSettings["FROMPWD"].ToString();            
            MailMessage msg = new MailMessage(fromEmail, toEmail);
            SmtpClient MarsMail = new SmtpClient();
            MarsMail.Host = smtphost;
            MarsMail.Port = smtpport;
            MarsMail.Credentials = new System.Net.NetworkCredential(smtpuser, smtppwd);

            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = isHTML;

            if (attachments != null)
            {
                if (attachments.Count > 0)
                {
                    foreach (Attachment attachment in attachments)
                    {
                        msg.Attachments.Add(attachment);
                    }
                }
            }

            if (cc.Length > 0)
            {
                msg.CC.Add(new MailAddress(cc));
            }

            try
            {
                MarsMail.Send(msg);
            }
            catch (Exception exception)
            {
                response = exception.Message;
                return false;
            }
            return true;
        }

        public static string GetEmailTemplateContent(string strEmailFilePath)
        {
            StreamReader srFile = null;
            string strEmailBody = "";
            try
            {
                srFile = File.OpenText(HttpContext.Current.Server.MapPath(strEmailFilePath));
                strEmailBody = srFile.ReadToEnd();
            }
            catch
            {

            }
            finally
            {
                if (srFile != null)
                    srFile.Close();
            }
            return strEmailBody;
        }

        public static string GetPromoCode(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }

        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }

        public static string ToStringForDB(string input)
        {
            return string.IsNullOrEmpty(input) ? "" : HttpUtility.HtmlEncode(input);
        }

        public static string ToStringForInput(string input)
        {
            return string.IsNullOrEmpty(input) ? "" : HttpUtility.HtmlDecode(input);
        }
    }
}
