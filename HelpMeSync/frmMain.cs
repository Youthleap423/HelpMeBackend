using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using HelpMe.BusinessAccess;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Web.Script.Serialization;

namespace HelpMeSync
{
    public partial class frmMain : Form
    {
        #region Variables
        MailMessage _Message;
        public MailMessage Message
        {
            get
            {
                if (_Message == null)
                    return _Message = new MailMessage();

                return _Message;
            }
            set
            {
                _Message = new MailMessage();
            }
        }

        SmtpClient SmtpServer;
        #endregion

        #region Android Variables
        string Android_ApplicationId = string.Empty;
        string Android_SenderId = string.Empty;
        #endregion

        #region IOS Variables
        int IOS_Port = 0;
        string IOS_Hostname = string.Empty;
        string IOS_CertificatePath = string.Empty;
        string IOS_Password = string.Empty;
        #endregion

        #region Events
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            #region Database Settings
            ApplicationHelper.DatabaseHelper.HelpMeConnectionString = string.Empty;
            HelpMeDatabaseConfiguration.Configurations.DatabaseSecurity dbSecurity = null;

            string FilePath = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + "DBConfig.HelpMe";
            if (File.Exists(FilePath))
            {
                dbSecurity = new HelpMeDatabaseConfiguration.Configurations.DatabaseSecurity();
                ApplicationHelper.DatabaseHelper.HelpMeConnectionString = dbSecurity.DecryptFiletoString();
                ApplicationHelper.DatabaseHelper.HelpMeDatabaseProvider = dbSecurity.ProviderName;

                BeanHelper.DBHelper.ConnectionString = ApplicationHelper.DatabaseHelper.HelpMeConnectionString;
                BeanHelper.DBHelper.ProviderName = ApplicationHelper.DatabaseHelper.HelpMeDatabaseProvider;
            }
            #endregion

            Android_ApplicationId = "AAAA1P9IXqo:APA91bESNkpeKW4y6PB0bbsUXOibHOPGyGEfEDZTblvu0LdkcDokcM-MZaPX5DtnTOGU8ogVh-dgkNYpjKA2jDhDy04U9rLHajBvJDL7oWoPqAIAz5_s4fTJoakxnSmtlXgx2_GXOzfB";
            Android_SenderId = "914815999658";

            IOS_Port = 2195;
            IOS_Hostname = "gateway.push.apple.com";
            IOS_CertificatePath = Application.StartupPath + "\\IOSCertificate\\iphone_pro.p12";
            IOS_Password = "123456";

            //string TokenId = "fwBflGr6fBc:APA91bFNfRqq1MqCYjM3BNViRkPwLleqjjWjp9Cn3BQcBRhfEfzMcylPvWOWnKE83NM0cmPm9Ma8D9VrOBPTf6uwn988euyGYLm_Jr297WE7n-3XdrcR_-l50hCFXPIaMzpadVZG9Gy4";
            //SendAndroidNotification(TokenId, "HelpMe", "Hello HelpMe ! Message");
            //SendAndroidNotification(TokenId, "HelpMe", "Hello HelpMe ! Message");
            Scheduled_Execution();
        }
        #endregion

        #region Functions
        void Scheduled_Execution()
        {
            if (!string.IsNullOrEmpty(ApplicationHelper.DatabaseHelper.HelpMeConnectionString) && ApplicationHelper.DatabaseHelper.IsConnectionExists(ApplicationHelper.DatabaseHelper.HelpMeConnectionString))
            {
                SendNotifications();
            }
            this.Close();
        }

//        void SetNotifications()
//        {
//            try
//            {
//                string sQry = string.Empty;
//                BeanHelper.DBHelper = new HelpMe.DBAccess.DBHelper(ApplicationHelper.DatabaseHelper.HelpMeConnectionString, ApplicationHelper.DatabaseHelper.HelpMeDatabaseProvider);
                                
//                #region Vet Appointment Confirmation
//                sQry = @"Insert Into tblNotification
//                        (ClientId, VetId, AppHeading, Title, Remarks, AppIconPath, ImagePath, NotificationType, IsSent, CreatedOn, JobPostId)
//                        Select ClientId, Null, 'AniVet Hub', 'Appointment Confirmation', 'Appointment is due on ' + Replace(Convert(Varchar(11), AppointmentDate, 113), ' ', '-') + ' ' + Convert(Varchar(8), FromTime) + '.
//                            Appointment No. : APP0108', 'http://admin.HelpMe.com/WebService/DefaultPics/AppIcon.png', '', 9, 0, dbo.GetCurrentDate(), JobPostId
//                        From tblVetAppointment VApp
//                        Where AppointmentStatus = 1 And DateAdd(Minute, -30, Convert(DateTime, Convert(Varchar(11), AppointmentDate, 113) + ' ' + Convert(Varchar(8), FromTime))) <= dbo.GetCurrentDate()
//	                        And Not Exists (Select 1 From tblNotification VNT Where VNT.NotificationType = 9 And VNT.JobPostId = VApp.JobPostId And VNT.ClientId Is Not Null)
//
//                        Union 
//
//                        Select Null, VetId, 'AniVet Hub', 'Appointment Confirmation', 'Appointment is due on ' + Replace(Convert(Varchar(11), AppointmentDate, 113), ' ', '-') + ' ' + Convert(Varchar(8), FromTime) + '.
//                            Appointment No. : APP0108', 'http://admin.HelpMe.com/WebService/DefaultPics/AppIcon.png', '', 10, 0, dbo.GetCurrentDate(), JobPostId
//                        From tblVetAppointment VApp
//                        Where AppointmentStatus = 1 And DateAdd(Minute, -30, Convert(DateTime, Convert(Varchar(11), AppointmentDate, 113) + ' ' + Convert(Varchar(8), FromTime))) <= dbo.GetCurrentDate()
//	                        And Not Exists (Select 1 From tblNotification VNT Where VNT.NotificationType = 10 And VNT.JobPostId = VApp.JobPostId And VNT.VetId Is Not Null)";
//                BeanHelper.DBHelper.ExecuteNonQuery(sQry);
//                #endregion
//            }
//            catch { }
//        }
        void SendNotifications()
        {
            try
            {
                string sQry = @"Select NT.NotificationId, NT.ClientId, NT.AppHeading, NT.Title, NT.Remarks, NT.AppIconPath, NT.ImagePath, NT.NotificationType, CD.AcTokenId As AndroidTokenId, '' As IOSTokenId, JobPostId
                                From tblNotification NT Inner Join (Select * From tblClientDevice Where DeviceType = 1 And Isnull(AcTokenId, '') <> '') CD On NT.ClientId = CD.ClientId
                                Where NT.IsSent = 0 And NT.ClientId Is Not Null
                                Union All
                                Select NT.NotificationId, NT.ClientId, NT.AppHeading, NT.Title, NT.Remarks, NT.AppIconPath, NT.ImagePath, NT.NotificationType, '' As AndroidTokenId, CD.AcTokenId As IOSTokenId, JobPostId
                                From tblNotification NT Inner Join (Select * From tblClientDevice Where DeviceType = 2 And Isnull(AcTokenId, '') <> '') CD On NT.ClientId = CD.ClientId
                                Where NT.IsSent = 0 And NT.ClientId Is Not Null";
                BeanHelper.DBHelper = new HelpMe.DBAccess.DBHelper(ApplicationHelper.DatabaseHelper.HelpMeConnectionString, ApplicationHelper.DatabaseHelper.HelpMeDatabaseProvider);

                #region Send Notification
                DataTable dtNotification = BeanHelper.DBHelper.FillTable(sQry);
                if (dtNotification != null && dtNotification.Rows.Count > 0)
                {
                    foreach (DataRow drNotification in dtNotification.Rows)
                    {
                        #region Push Notification Android
                        if (!string.IsNullOrEmpty(Convert.ToString(drNotification["AndroidTokenId"])))
                        {
                            SendAndroidNotification(Convert.ToString(drNotification["AndroidTokenId"]),
                                                    Convert.ToString(drNotification["Title"]),
                                                    Convert.ToString(drNotification["Remarks"]),
                                                    Convert.ToString(drNotification["AppIconPath"]),
                                                    Convert.ToString(drNotification["ImagePath"]),
                                                    Convert.ToInt32(drNotification["NotificationType"]),
                                                    Convert.ToString(drNotification["JobPostId"]));
                        }
                        #endregion

                        #region Push Notification IOS
                        if (!string.IsNullOrEmpty(Convert.ToString(drNotification["IOSTokenId"])))
                        {
                            SendIOSNotification(Convert.ToString(drNotification["IOSTokenId"]),
                                                Convert.ToString(drNotification["Title"]),
                                                Convert.ToString(drNotification["Remarks"]),
                                                Convert.ToString(drNotification["AppIconPath"]),
                                                Convert.ToString(drNotification["ImagePath"]),
                                                Convert.ToInt32(drNotification["NotificationType"]),
                                                Convert.ToString(drNotification["JobPostId"]));
                        }
                        #endregion

                        BeanHelper.DBHelper.ExecuteNonQuery("Update tblNotification Set IsSent = 1 Where NotificationId = " + Convert.ToString(drNotification["NotificationId"]));
                    }
                }
                #endregion
            }
            catch { }
        }

        //void SendAndroidNotification(string TokenId, string sNotificationTitle, string sNotificationMessage, string JobPostId)
        //{
        //    try
        //    {
        //        WebRequest tRequest;
        //        tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        //        tRequest.Method = "post";
        //        tRequest.ContentType = "application/json";
        //        tRequest.Headers.Add(string.Format("Authorization: key={0}", Android_ApplicationId));
        //        tRequest.Headers.Add(string.Format("Sender: id={0}", Android_SenderId));

        //        string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + sNotificationMessage + "&data.time=" + System.DateTime.Now.ToString() + "®istration_id=" + TokenId + "";
        //        var serializer = new JavaScriptSerializer();
        //        var json = serializer.Serialize(postData);
        //        Byte[] byteArray = Encoding.UTF8.GetBytes(json);
        //        tRequest.ContentLength = byteArray.Length;

        //        Stream dataStream = tRequest.GetRequestStream();
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //        dataStream.Close();

        //        WebResponse tResponse = tRequest.GetResponse();
        //        dataStream = tResponse.GetResponseStream();
        //        StreamReader tReader = new StreamReader(dataStream);
        //        string sResponseFromServer = tReader.ReadToEnd();

        //        tReader.Close();
        //        dataStream.Close();
        //        tResponse.Close();


        //        ////string deviceId = "d9EEvHzOFwY:APA91bFLW-aeMCt-WJzoRWO2cUzm0LUba8zbtOofD85FILVshcXBCbDvnK1giCF-vOuyO4Fqu92yYUC8EwRELWuAeTZmI0fOxYO-ysNODGa91ZuOtsRaXa5heX2IOgvnjMAg9zllAgMy";
        //        //WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        //        //tRequest.Method = "post";
        //        //tRequest.ContentType = "application/json";
        //        //var data = new
        //        //{
        //        //    to = TokenId,
        //        //    notification = new
        //        //    {
        //        //        body = sNotificationMessage,
        //        //        title = sNotificationTitle,
        //        //        icon = "myicon"
        //        //    },
        //        //    priority = "high"
        //        //};

        //        //var serializer = new JavaScriptSerializer();
        //        //var json = serializer.Serialize(data);
        //        //Byte[] byteArray = Encoding.UTF8.GetBytes(json);
        //        //tRequest.Headers.Add(string.Format("Authorization: key={0}", Android_ApplicationId));
        //        //tRequest.Headers.Add(string.Format("Sender: id={0}", Android_SenderId));
        //        //tRequest.ContentLength = byteArray.Length;

        //        //using (Stream dataStream = tRequest.GetRequestStream())
        //        //{
        //        //    dataStream.Write(byteArray, 0, byteArray.Length);
        //        //    using (WebResponse tResponse = tRequest.GetResponse())
        //        //    {
        //        //        using (Stream dataStreamResponse = tResponse.GetResponseStream())
        //        //        {
        //        //            using (StreamReader tReader = new StreamReader(dataStreamResponse))
        //        //            {
        //        //                String sResponseFromServer = tReader.ReadToEnd();
        //        //            }
        //        //        }
        //        //    }
        //        //}
        //    }
        //    catch { }
        //}

        void SendAndroidNotification(string TokenId, string sNotificationTitle, string sNotificationMessage, string AppIconPath, string ImagePath, int NotificationType, string JobPostId)
        {
            try
            {
                if (string.IsNullOrEmpty(JobPostId))
                    JobPostId = "0";

                //string deviceId = "d9EEvHzOFwY:APA91bFLW-aeMCt-WJzoRWO2cUzm0LUba8zbtOofD85FILVshcXBCbDvnK1giCF-vOuyO4Fqu92yYUC8EwRELWuAeTZmI0fOxYO-ysNODGa91ZuOtsRaXa5heX2IOgvnjMAg9zllAgMy";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var pushdata = new
                {
                    to = TokenId,
                    notification = new
                    {
                        body = sNotificationMessage,
                        title = sNotificationTitle,
                        icon = "myicon"
                    },
                    priority = "high",
                    data = new
                    {
                        JobPostId = JobPostId,
                        AppIconPath = AppIconPath,
                        ImagePath = ImagePath,
                        NotificationType = NotificationType.ToString()
                    }
                };

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(pushdata);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", Android_ApplicationId));
                tRequest.Headers.Add(string.Format("Sender: id={0}", Android_SenderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch { }
        }
        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
            return false;
        }
        void SendIOSNotification(string TokenId, string sNotificationTitle, string sNotificationMessage, string AppIconPath, string ImagePath, int NotificationType, string JobPostId)
        {
            try
            {
                X509Certificate2 IOS_ClientCertificate = new X509Certificate2(System.IO.File.ReadAllBytes(IOS_CertificatePath), IOS_Password);
                X509Certificate2Collection IOS_CertificatesCollection = new X509Certificate2Collection(IOS_ClientCertificate);

                TcpClient client = new TcpClient(IOS_Hostname, IOS_Port);
                SslStream sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
                try
                {
                    sslStream.AuthenticateAsClient(IOS_Hostname, IOS_CertificatesCollection, SslProtocols.Tls, false);
                    MemoryStream memoryStream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(memoryStream);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write((byte)32);

                    writer.Write(HexStringToByteArray(TokenId));
                    //String payload = "{\"aps\":{\"alert\":\"" + sNotificationMessage + "\",\"badge\":1,\"sound\":\"default\"}}";
                    //String payload = "{\"aps\" : {\"alert\" : {\"title\" : \"" + sNotificationTitle + "\",\"body\" : \"" + sNotificationMessage + "\"},\"badge\" : 1,\"sound\" : \"default\"},\"JobPostId\" : \"" + JobPostId + "\",\"AppIconPath\" : \"" + AppIconPath + "\",\"ImagePath\" : \"" + ImagePath + "\", \"NotificationType\" : \"" + NotificationType.ToString() + "\"}";

                    String payload = "{\"aps\": {\"alert\" : {\"ImagePath\" : \"" + ImagePath + "\",\"NotificationType\" : \"" + NotificationType.ToString() + "\",\"JobPostId\" : \"" + JobPostId + "\",\"body\" : \"" + sNotificationMessage.Replace("\r\n", "") + "\",\"title\" : \"" + sNotificationTitle + "\"},\"badge\" : 1,\"sound\" : \"default\"}}";
                    //String payload = "{\"aps\":{\"alert\":\"" + sNotificationMessage + "\",\"badge\":1,\"sound\":\"default\"}}";
                    //                    String payload = @"{
                    //  ""aps"" : {
                    //          ""alert"" : {
                    //                    ""title"" : """ + sNotificationTitle + @""",
                    //                    ""body"" : """ + sNotificationMessage.Replace("\r\n", "") + @"""
                    //                    },
                    //                    ""badge"" : 1,
                    //                    ""sound"" : ""default""
                    //          }
                    //}";
                    //,
                    //""JobPostId"" : """ + JobPostId + @""",
                    //""AppIconPath"" : """ + AppIconPath + @""",
                    //""ImagePath"" : """ + ImagePath + @""", 
                    //""NotificationType"" : """ + NotificationType.ToString() + @"""

                    writer.Write((byte)0);
                    writer.Write((byte)payload.Length);
                    byte[] b1 = System.Text.Encoding.UTF8.GetBytes(payload);
                    writer.Write(b1);
                    writer.Flush();
                    byte[] array = memoryStream.ToArray();
                    sslStream.Write(array);
                    sslStream.Flush();
                    client.Close();
                }
                catch (System.Security.Authentication.AuthenticationException ex)
                {
                    client.Close();
                }
                catch { client.Close(); }
            }
            catch { }
        }
        #endregion
    }
}