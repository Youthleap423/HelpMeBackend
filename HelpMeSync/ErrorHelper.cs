using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net.Mail;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Threading;
using System.Net.Mime;
using HelpMe.BusinessAccess;
using HelpMe.Entities;

namespace HelpMe.Error
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("VFPAndDotNet.HelpMeEMailHelper")]
    public class ErrorHelper
    {
        public ErrorHelper() { }

        #region Properties
        public HelpMeErrors HelpMeErrors
        {
            get;
            set;
        }

        public SmtpClient SmtpServer;

        string strServer;
        public string Server
        {
            set
            {
                strServer = value;
                SmtpServer = new SmtpClient(value);
            }
            get { return strServer; }
        }

        public string SendTo
        {
            get;
            set;
        }

        public string SendFrom
        {
            get;
            set;
        }

        public string Subject
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        public string LoginId
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public Boolean SSL
        {
            get;
            set;
        }

        public Boolean Async
        {
            get;
            set;
        }

        public string LogoPath
        { get; set; }

        public string DatabaseVersion
        { get; set; }

        #endregion

        #region Functions
        void SendEMail()
        {
            try
            {
                DataSet ds = new DataSet();

                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + System.IO.Path.DirectorySeparatorChar + "HelpMeprofilesettings.xml"))
                {
                    ds.ReadXml(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + System.IO.Path.DirectorySeparatorChar + "HelpMeprofilesettings.xml");
                }

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(ds.Tables[0].Rows[0]["sender"].ToString());

                mail.To.Clear();

                string[] strMailTo = ds.Tables[0].Rows[0]["receiver"].ToString().Split(';');

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

                SmtpServer.Port = Convert.ToInt32(ds.Tables[0].Rows[0]["port"].ToString());
                SmtpServer.Credentials = new System.Net.NetworkCredential(ds.Tables[0].Rows[0]["Id"].ToString(), new HelpMeDatabaseConfiguration.Configurations.DatabaseSecurity().DecryptString(ds.Tables[0].Rows[0]["pass"].ToString()));
                SmtpServer.EnableSsl = SSL;

                string userState = "test message1";

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["async"].ToString()) == true)
                {
                    SmtpServer.SendAsync(mail, userState);
                }
                else
                {
                    SmtpServer.Send(mail);
                }
            }
            catch { }
        }
        public string SendTestEMail()
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
        string GetMailBody()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<html> <head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8'><title>HelpMe</title><style type='text/css'> .style1 { width: 50%; height: 50px; }.style2 { height: 50px; } </style><style type='text/css'> body { margin: 0px; padding: 0px;font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 11px; }.img_left { float: left; } .img_right{ float: right;} .table { border: 1px solid #727d8d; border-collapse: collapse; }.table td p { margin: 0px 0px 10px 0px; }.table td { padding: 5px 8px; font: 11px Verdana, Arial, Helvetica, sans-serif; color: #4f4f4f; line-height: 18px; border: 1px solid #97a4b7;background: #FFFFFF; }.table th { background: #727d8d; font-size: 11px; padding: 5px 8px; border-bottom: none; border: 1px solid #97a4b7; color: #ffffff; font-weight: bold; }.table td input.txtbox { border: solid 1px #727d8d; font-weight: normal; text-align: left; color: #000000; }.table a { color: #000000; } .table a:hover{ text-decoration: underline; }.table td .select{ border: solid 1px #727d8d; font-weight: normal; text-align: left; color: #cccccc; } </style> </head><body> <table width='98%' border='0' align='center' cellpadding='0' cellspacing='0' class='table'><tr> <td height='55' align='left' valign='top'><table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td width='50%' align='left' valign='middle' style='font-family: Verdana; font-size: 20px;font-weight: 300; color: White; background-color: #708090' class='style1'> HelpMe 2.0.002 Error</td><td width='50%' align='left' valign='middle' class='style2'> <img src=\"cid:imglogo\" /> </td></tr> </table> </td> </tr>");
            sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Form\\Class Name :</b></p> <p>" + HelpMeErrors.FormName + "</p> </td></tr>");
            sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Event\\Function Name :</b></p> <p>" + HelpMeErrors.FunctionName + "</p> </td></tr>");
            sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Path :</b></p> <p>" + HelpMeErrors.FilePath + "</p> </td></tr>");
            sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Error Message :</b></p> <p>" + HelpMeErrors.ErrorMessage + "</p> </td></tr>");
            sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Error Source :</b></p> <p>" + HelpMeErrors.ErrorSource + "</p> </td></tr>");
            sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Error Stack :</b></p> <p>" + HelpMeErrors.ErrorStack + "</p> </td></tr>");
            sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Error InnerException :</b></p> <p>" + HelpMeErrors.ErrorInnerEx + "</p> </td></tr>");
            sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Error Data :</b></p> <p>" + HelpMeErrors.ErrorData + "</p> </td></tr>");
            sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Date :</b></p> <p>" + HelpMeErrors.ErrorDate.ToString() + "</p> </td></tr>");
            sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>DataBase Version :</b></p> <p>" + DatabaseVersion + "</p> </td></tr>");
            sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Machine Name :</b></p> <p>" + Environment.MachineName + "</p> </td></tr>");
            sb.Append("</table></body> </html>");

            return sb.ToString();
        }
        string GetTestMailBody()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<html> <head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8'><title>HelpMe</title><style type='text/css'> .style1 { width: 50%; height: 50px; }.style2 { height: 50px; } </style><style type='text/css'> body { margin: 0px; padding: 0px;font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 11px; }.img_left { float: left; } .img_right{ float: right;} .table { border: 1px solid #727d8d; border-collapse: collapse; }.table td p { margin: 0px 0px 10px 0px; }.table td { padding: 5px 8px; font: 11px Verdana, Arial, Helvetica, sans-serif; color: #4f4f4f; line-height: 18px; border: 1px solid #97a4b7;background: #FFFFFF; }.table th { background: #727d8d; font-size: 11px; padding: 5px 8px; border-bottom: none; border: 1px solid #97a4b7; color: #ffffff; font-weight: bold; }.table td input.txtbox { border: solid 1px #727d8d; font-weight: normal; text-align: left; color: #000000; }.table a { color: #000000; } .table a:hover{ text-decoration: underline; }.table td .select{ border: solid 1px #727d8d; font-weight: normal; text-align: left; color: #cccccc; } </style> </head><body> <table width='98%' border='0' align='center' cellpadding='0' cellspacing='0' class='table'><tr> <td height='55' align='left' valign='top'><table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td width='50%' align='left' valign='middle' style='font-family: Verdana; font-size: 20px;font-weight: 300; color: White; background-color: #708090' class='style1'> HelpMe 2.0</td><td width='50%' align='left' valign='middle' class='style2'> <img src=\"cid:imglogo\" /> </td></tr> </table> </td> </tr>");
            sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Test Message :</b></p> <p>You have successfully recieved an Email From HelpMe...</p> </td></tr>");
            sb.Append("<tr> <td height='55' align='left' valign='top'> <p style=\"font-size:14px;\"> <b>Date :</b></p> <p>" + System.DateTime.Now.ToString() + "</p> </td></tr>");
            sb.Append("</table></body> </html>");

            return sb.ToString();
        }
        string GetErrorString()
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
        void Insert()
        {
            BeanHelper.HelpMeErrorsBean.HelpMeErrors = HelpMeErrors;
            BeanHelper.HelpMeErrorsBean.ExecuteData("I");
        }

        public void ShowError(Exception ex, string strFormName, string strFunctionName, string strFilePath, string ErrorMessage, string strCaption)
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

                Thread t = new Thread(SendEMail);
                t.Start();
            }
            #endregion

            MessageBox.Show(HelpMeErrors.ErrorMessage, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void ShowError(Exception ex, string strFormName, string strFunctionName, string strFilePath, string ErrorMessage, string strCaption, IntPtr nWinHandle)
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

            MessageBox.Show(new WindowWrapper(nWinHandle), ErrorMessage, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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