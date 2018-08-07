using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net.Mime;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Windows.Forms;
using HelpMe.BusinessAccess;
using HelpMe.Entities;
using HelpMeDatabaseConfiguration.Configurations;

namespace HelpMeSync
{
    public static class ApplicationHelper
    {
        public static class DatabaseHelper
        {
            #region Database_Properties
            public static bool IsConnectionExists(string ConnString)
            {
                try
                {
                    HelpMeDatabaseProvider = BeanHelper.DBHelper.ProviderName;
                    HelpMeConnection = new SqlConnection(ConnString);

                    try
                    {
                        HelpMeConnection.Open();
                        HelpMeConnection.Close();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = ex.Message;
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    return false;
                }
            }
            public static string ErrorMessage { get; set; }
            public static string HelpMeConnectionString { get; set; }
            public static string HelpMeDatabaseProvider { get; set; }
            public static SqlConnection HelpMeConnection { get; set; }
            static List<string> _DBConnections;
            public static List<string> HelpMeDBConnections
            {
                get
                {
                    if (_DBConnections == null)
                        return _DBConnections = new List<string>();

                    return _DBConnections;
                }
                set
                {
                    _DBConnections = value;
                }
            }
            #endregion
        }

        public static class EmailHelper
        {
            #region Email_Properties
            public static SmtpClient SmtpServer;
            static string strServer;
            public static string Server
            {
                set
                {
                    strServer = value;
                    SmtpServer = new SmtpClient(value);
                }
                get { return strServer; }
            }
            public static string SendTo { get; set; }
            public static string SendFrom { get; set; }
            public static string Subject { get; set; }
            public static int Port { get; set; }
            public static string LoginId { get; set; }
            public static string Password { get; set; }
            public static Boolean SSL { get; set; }
            public static Boolean Async { get; set; }
            public static string LogoPath { get; set; }
            public static string DatabaseVersion { get; set; }
            #endregion

            #region Functions
            public static string SendTestEMail()
            {
                try
                {
                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress(SendFrom);

                    mail.To.Clear();

                    string[] strMailTo = SendTo.Split(';');

                    foreach (string sMailTo in strMailTo)
                        mail.To.Add(sMailTo);

                    mail.Subject = Subject;
                    mail.IsBodyHtml = true;
                    mail.Body = GetTestMailBody();

                    AlternateView av = AlternateView.CreateAlternateViewFromString(mail.Body, null, MediaTypeNames.Text.Html);
                    LinkedResource lr = new LinkedResource(LogoPath, MediaTypeNames.Image.Jpeg);
                    lr.ContentId = "imglogo";
                    av.LinkedResources.Add(lr);
                    mail.AlternateViews.Add(av);

                    SmtpServer.Port = Convert.ToInt32(Port);
                    SmtpServer.Credentials = new System.Net.NetworkCredential(LoginId, Password);
                    SmtpServer.EnableSsl = SSL;

                    string userState = "test message1";

                    if (Async)
                    {
                        SmtpServer.SendAsync(mail, userState);
                    }
                    else
                    {
                        SmtpServer.Send(mail);
                    }

                    return "Succeed";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            static string GetTestMailBody()
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<html> <head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8'><title>Quick HelpMe</title><style type='text/css'> .style1 { width: 50%; height: 50px; }.style2 { height: 50px; } </style><style type='text/css'> body { margin: 0px; padding: 0px;font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 11px; }.img_left { float: left; } .img_right{ float: right;} .table { border: 1px solid #727d8d; border-collapse: collapse; }.table td p { margin: 0px 0px 10px 0px; }.table td { padding: 5px 8px; font: 11px Verdana, Arial, Helvetica, sans-serif; color: #4f4f4f; line-height: 18px; border: 1px solid #97a4b7;background: #FFFFFF; }.table th { background: #727d8d; font-size: 11px; padding: 5px 8px; border-bottom: none; border: 1px solid #97a4b7; color: #ffffff; font-weight: bold; }.table td input.txtbox { border: solid 1px #727d8d; font-weight: normal; text-align: left; color: #000000; }.table a { color: #000000; } .table a:hover{ text-decoration: underline; }.table td .select{ border: solid 1px #727d8d; font-weight: normal; text-align: left; color: #cccccc; } </style> </head><body> <table width='98%' border='0' align='center' cellpadding='0' cellspacing='0' class='table'><tr> <td height='55' align='left' valign='top'><table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td width='50%' align='left' valign='middle' style='font-family: Verdana; font-size: 20px;font-weight: 300; color: White; background-color: #708090' class='style1'> Quick HelpMe 2.0</td><td width='50%' align='left' valign='middle' class='style2'> <img src=\"cid:imglogo\" /> </td></tr> </table> </td> </tr>");
                sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Test Message :</b></p> <p>You have successfully recieved an Email From Quick HelpMe...</p> </td></tr>");
                sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Date :</b></p> <p>" + System.DateTime.Now.ToString() + "</p> </td></tr>");
                sb.Append("</table></body> </html>");

                return sb.ToString();
            }
            public static void SendEMail()
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(ApplicationHelper.EmailHelper.SendFrom);

                    mail.To.Clear();
                    string[] strMailTo = ApplicationHelper.EmailHelper.SendTo.Split(';');
                    foreach (string sMailTo in strMailTo)
                        mail.To.Add(sMailTo);

                    mail.Subject = Subject;
                    mail.IsBodyHtml = true;
                    mail.Body = GetMailBody();

                    AlternateView av = AlternateView.CreateAlternateViewFromString(mail.Body, null, MediaTypeNames.Text.Html);
                    LinkedResource lr = new LinkedResource(LogoPath, MediaTypeNames.Image.Jpeg);
                    lr.ContentId = "imglogo";
                    av.LinkedResources.Add(lr);
                    mail.AlternateViews.Add(av);

                    SmtpServer.Port = ApplicationHelper.EmailHelper.Port;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(ApplicationHelper.EmailHelper.LoginId, ApplicationHelper.EmailHelper.Password);
                    SmtpServer.EnableSsl = ApplicationHelper.EmailHelper.SSL;

                    string userState = "test message1";

                    if (ApplicationHelper.EmailHelper.Async)
                        SmtpServer.SendAsync(mail, userState);
                    else
                        SmtpServer.Send(mail);
                }
                catch { }
            }
            static string GetMailBody()
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<html> <head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8'><title>Quick HelpMe</title><style type='text/css'> .style1 { width: 50%; height: 50px; }.style2 { height: 50px; } </style><style type='text/css'> body { margin: 0px; padding: 0px;font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 11px; }.img_left { float: left; } .img_right{ float: right;} .table { border: 1px solid #727d8d; border-collapse: collapse; }.table td p { margin: 0px 0px 10px 0px; }.table td { padding: 5px 8px; font: 11px Verdana, Arial, Helvetica, sans-serif; color: #4f4f4f; line-height: 18px; border: 1px solid #97a4b7;background: #FFFFFF; }.table th { background: #727d8d; font-size: 11px; padding: 5px 8px; border-bottom: none; border: 1px solid #97a4b7; color: #ffffff; font-weight: bold; }.table td input.txtbox { border: solid 1px #727d8d; font-weight: normal; text-align: left; color: #000000; }.table a { color: #000000; } .table a:hover{ text-decoration: underline; }.table td .select{ border: solid 1px #727d8d; font-weight: normal; text-align: left; color: #cccccc; } </style> </head><body> <table width='98%' border='0' align='center' cellpadding='0' cellspacing='0' class='table'><tr> <td height='55' align='left' valign='top'><table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td width='50%' align='left' valign='middle' style='font-family: Verdana; font-size: 20px;font-weight: 300; color: White; background-color: #708090' class='style1'> Quick HelpMe 2.0.002 Error</td><td width='50%' align='left' valign='middle' class='style2'> <img src=\"cid:imglogo\" /> </td></tr> </table> </td> </tr>");
                sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Form\\Class Name :</b></p> <p>" + ErrorHelper.HelpMeErrors.FormName + "</p> </td></tr>");
                sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Event\\Function Name :</b></p> <p>" + ErrorHelper.HelpMeErrors.FunctionName + "</p> </td></tr>");
                sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Path :</b></p> <p>" + ErrorHelper.HelpMeErrors.FilePath + "</p> </td></tr>");
                sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Error Message :</b></p> <p>" + ErrorHelper.HelpMeErrors.ErrorMessage + "</p> </td></tr>");
                sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Error Source :</b></p> <p>" + ErrorHelper.HelpMeErrors.ErrorSource + "</p> </td></tr>");
                sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Error Stack :</b></p> <p>" + ErrorHelper.HelpMeErrors.ErrorStack + "</p> </td></tr>");
                sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Error InnerException :</b></p> <p>" + ErrorHelper.HelpMeErrors.ErrorInnerEx + "</p> </td></tr>");
                sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Error Data :</b></p> <p>" + ErrorHelper.HelpMeErrors.ErrorData + "</p> </td></tr>");
                sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Date :</b></p> <p>" + ErrorHelper.HelpMeErrors.ErrorDate.ToString() + "</p> </td></tr>");
                sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>DataBase Version :</b></p> <p>" + DatabaseVersion + "</p> </td></tr>");
                sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Machine Name :</b></p> <p>" + Environment.MachineName + "</p> </td></tr>");
                sb.Append("</table></body> </html>");

                return sb.ToString();
            }
            #endregion
        }

        public static class ErrorHelper
        {
            #region Error_Log_Settings
            public static HelpMeErrors HelpMeErrors { get; set; }
            public static void ShowError(Exception ex, string strFormName, string strFunctionName, string strFilePath, string ErrorMessage, string strCaption)
            {
                if (HelpMeErrors == null)
                {
                    HelpMeErrors = new HelpMeErrors();
                }

                HelpMeErrors.ErrorData = ex.Data.ToString();

                if (ex.InnerException == null)
                {
                    HelpMeErrors.ErrorInnerEx = string.Empty;
                }
                else
                {
                    HelpMeErrors.ErrorInnerEx = ex.InnerException.ToString();
                }
                HelpMeErrors.ErrorMessage = ex.Message.Replace("\n", "");
                HelpMeErrors.ErrorSource = ex.Source;
                HelpMeErrors.ErrorStack = ex.StackTrace;

                HelpMeErrors.ErrorDate = System.DateTime.Now;
                HelpMeErrors.FilePath = strFilePath;
                HelpMeErrors.FormName = strFormName;
                HelpMeErrors.FunctionName = strFunctionName;
                HelpMeErrors.IsSent = false;

                #region Log Error in Event Viewer

                try
                {
                    EventLog el = new EventLog("HelpMe Error Log");
                    el.Source = "HelpMe 2.0.002";
                    el.WriteEntry(GetErrorString(), EventLogEntryType.Error);
                }
                catch { }

                #endregion

                #region Log Error in Database
                if (!BeanHelper.HelpMeErrorsBean.IsExists(ex.StackTrace))
                {
                    Insert();

                    Thread t = new Thread(EmailHelper.SendEMail);
                    t.Start();
                }
                #endregion
                //MessageBox.Show(HelpMeErrors.ErrorMessage, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            public static void ShowError(Exception ex, string strFormName, string strFunctionName, string strFilePath, string ErrorMessage, string strCaption, IntPtr nWinHandle)
            {
                if (HelpMeErrors == null)
                {
                    HelpMeErrors = new HelpMeErrors();
                }

                HelpMeErrors.ErrorData = ex.Data.ToString();

                if (ex.InnerException == null)
                {
                    HelpMeErrors.ErrorInnerEx = string.Empty;
                }
                else
                {
                    HelpMeErrors.ErrorInnerEx = ex.InnerException.ToString();
                }
                HelpMeErrors.ErrorMessage = ex.Message;
                HelpMeErrors.ErrorSource = ex.Source;
                HelpMeErrors.ErrorStack = ex.StackTrace;

                HelpMeErrors.ErrorDate = System.DateTime.Now;
                HelpMeErrors.FilePath = strFilePath;
                HelpMeErrors.FormName = strFormName;
                HelpMeErrors.FunctionName = strFunctionName;
                HelpMeErrors.IsSent = false;

                #region Log Error in Event Viewer

                try
                {
                    EventLog el = new EventLog("HelpMe Error Log");
                    el.Source = "HelpMe 2.0.002";
                    el.WriteEntry(GetErrorString(), EventLogEntryType.Error);
                }
                catch { }

                #endregion

                #region Log Error in Database

                Insert();

                #endregion
                //MessageBox.Show(new WindowWrapper(nWinHandle), ErrorMessage, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            static string GetErrorString()
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("Form\\Class Name : " + HelpMeErrors.FormName + "\n\n");
                sb.Append("Event\\Function Name : " + HelpMeErrors.FunctionName + "\n\n");
                sb.Append("Path : " + HelpMeErrors.FilePath + "\n\n");
                sb.Append("Error Message : " + HelpMeErrors.ErrorMessage + "\n\n");
                sb.Append("Error Source : " + HelpMeErrors.ErrorSource + "\n\n");
                sb.Append("Error Stack : " + HelpMeErrors.ErrorStack + "\n\n");
                sb.Append("Error InnerException : " + HelpMeErrors.ErrorInnerEx + "\n\n");
                sb.Append("Error Data : " + HelpMeErrors.ErrorData + "\n\n");
                sb.Append("Date : " + HelpMeErrors.ErrorDate.ToString() + "\n\n");

                return sb.ToString();
            }
            static void Insert()
            {
                BeanHelper.HelpMeErrorsBean.HelpMeErrors = HelpMeErrors;
                BeanHelper.HelpMeErrorsBean.ExecuteData("I");
            }
            #endregion
        }

        public static class CommonHelper
        {
            #region Variables
            public static string MessageTitle { get { return Properties.Settings.Default.MessageTitle; } }
            public static int ActiveCmpId { get; set; }
            public static string ActiveCmpName { get; set; }
            public static Int64 ActiveLoginId { get; set; }
            public static string ActiveLoginName { get; set; }
            #endregion

            #region Functions
            public static bool IsNumeric(string s) { try { Double.Parse(s); return true; } catch { return false; } }
            public static string SendSMS(string SMSAPI, string SMSUserName, string SMSPassword, string SMSSenderId, string PhoneNo, string Message, ref string sErr)
            {
                string result = string.Empty;
                string sQry = SMSAPI;
                Message = Message.Replace("&", string.Empty);

                sQry = sQry.Replace("[User]", SMSUserName);
                sQry = sQry.Replace("[Password]", SMSPassword);
                sQry = sQry.Replace("[Mobile]", PhoneNo);
                sQry = sQry.Replace("[SenderId]", SMSSenderId);
                sQry = sQry.Replace("[SMS]", Message);

                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(sQry);

                    // Defines Web Request Object to be posted.
                    httpWebRequest.ContentType = "application/json; charset=utf-8";
                    httpWebRequest.Accept = "application/json, text/javascript, */*";
                    httpWebRequest.Method = "POST";

                    // Posts JSON Data to web service.
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        //streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    // here we are getting Response from the web srevice on posted JSON data.
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    // We are reading response from here.
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                        sErr = result;
                    }
                }
                catch (Exception ex)
                {
                    sErr = ex.Message + ", " + sQry;
                    //MessageBox.Show(ex.Message);
                }
                return result;
            }
            public static string CheckCreditBalance(string SMSAPI, string SMSUserName, string SMSPassword, ref string sErr)
            {
                string result = string.Empty;
                string sQry = SMSAPI;

                sQry = sQry.Replace("[User]", SMSUserName);
                sQry = sQry.Replace("[Password]", SMSPassword);

                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(sQry);

                    // Defines Web Request Object to be posted.
                    httpWebRequest.ContentType = "application/json; charset=utf-8";
                    httpWebRequest.Accept = "application/json, text/javascript, */*";
                    httpWebRequest.Method = "POST";

                    // Posts JSON Data to web service.
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        //streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    // here we are getting Response from the web srevice on posted JSON data.
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    // We are reading response from here.
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    sErr = ex.Message;
                }
                return result;
            }
            #endregion
        }

        public class WindowWrapper : System.Windows.Forms.IWin32Window
        {
            public WindowWrapper(IntPtr handle)
            {
                Handle = handle;
            }
            private IntPtr _handle;
            public IntPtr Handle
            {
                get { return _handle; }
                set { _handle = value; }
            }
        }
    }
}