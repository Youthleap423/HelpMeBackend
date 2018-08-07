using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using HelpMe.Error;
using System.Web;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Device.Location;

namespace HelpMeSync
{
    public partial class frmMDI : Form
    {
        #region "Form Events"
        public frmMDI()
        {
            try
            {
                InitializeComponent();
                this.Text = "HelpMe Sync™ [ HelpMe Sync Management ]";

                ToolStripStatusLabel lblVersion = new ToolStripStatusLabel();
                this.ssBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { lblVersion });
                lblVersion.Name = "lblVersion";
                lblVersion.Size = new System.Drawing.Size(109, 17);
                lblVersion.Text = "HelpMe Sync™ [ HelpMe Sync Management ]";
            }
            catch (Exception ex)
            {
                InvoiceCommonHelper.InvoiceErrorHelper.ShowError(ex, System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod().ToString(), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, ex.Message, ApplicationHelper.CommonHelper.MessageTitle);
            }
        }
        #endregion

        #region "Click Events"
        private void mnuSynchronization_Click(object sender, EventArgs e)
        {
            frmSettings objfrmSettings = new frmSettings();
            objfrmSettings.ShowDialog();
        }
        private void mnuEmailSettings_Click(object sender, EventArgs e)
        {
            frmEmailSettings objfrmEmailSettings = new frmEmailSettings();
            objfrmEmailSettings.ShowDialog();
        }
        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }
        //private void mnuFCM_Click(object sender, EventArgs e)
        //{
        //    int port = 2195;
        //    String hostname = "gateway.sandbox.push.apple.com";
        //    String certificatePath = Application.StartupPath + "\\IOSCertificate\\iphone_dev.p12";
        //    X509Certificate2 clientCertificate = new X509Certificate2(System.IO.File.ReadAllBytes(certificatePath), "123456");
        //    X509Certificate2Collection certificatesCollection = new X509Certificate2Collection(clientCertificate);

        //    TcpClient client = new TcpClient(hostname, port);
        //    SslStream sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);

        //    try
        //    {
        //        sslStream.AuthenticateAsClient(hostname, certificatesCollection, SslProtocols.Tls, false);
        //        MemoryStream memoryStream = new MemoryStream();
        //        BinaryWriter writer = new BinaryWriter(memoryStream);
        //        writer.Write((byte)0);
        //        writer.Write((byte)0);
        //        writer.Write((byte)32);

        //        writer.Write(HexStringToByteArray("cab62e91978e3a315cdc2222eac1e025f8e3c4cfb0f4033a94ca75a57aca1f0d"));
        //        String payload = "{\"aps\":{\"alert\":\"" + "Hi,, This Is a Sample Push Notification For IPhone.." + "\",\"badge\":1,\"sound\":\"default\"}}";
        //        writer.Write((byte)0);
        //        writer.Write((byte)payload.Length);
        //        byte[] b1 = System.Text.Encoding.UTF8.GetBytes(payload);
        //        writer.Write(b1);
        //        writer.Flush();
        //        byte[] array = memoryStream.ToArray();
        //        sslStream.Write(array);
        //        sslStream.Flush();
        //        client.Close();
        //    }
        //    catch (System.Security.Authentication.AuthenticationException ex)
        //    {
        //        client.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        client.Close();
        //    }

        //    try
        //    {
        //        var applicationID = "AAAA9vG3Rcw:APA91bFJNeoyaI73xFQi5OS65qi1ka2rzP5vLkkAPc33OftFCzFE1pkC9f4uM4vfytj60PhoaRAGOF9ci6axx8FEnL6sUPiAcU0XOuEwXtOnOLmcA-zuHAJ8moHcbfTTC21Cm5R54YtG6hdomBnBjGQ0IP9a3KIYgg";
        //        var senderId = "1060617274828";
        //        string deviceId = "d9EEvHzOFwY:APA91bFLW-aeMCt-WJzoRWO2cUzm0LUba8zbtOofD85FILVshcXBCbDvnK1giCF-vOuyO4Fqu92yYUC8EwRELWuAeTZmI0fOxYO-ysNODGa91ZuOtsRaXa5heX2IOgvnjMAg9zllAgMy";
        //        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        //        tRequest.Method = "post";
        //        tRequest.ContentType = "application/json";
        //        var data = new
        //        {
        //            to = deviceId,
        //            notification = new
        //            {
        //                body = "This is the message Body",
        //                title = "This is the title of Message",
        //                icon = "myicon"
        //            },
        //            priority = "high"
        //        };

        //        var serializer = new JavaScriptSerializer();
        //        var json = serializer.Serialize(data);
        //        Byte[] byteArray = Encoding.UTF8.GetBytes(json);
        //        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
        //        tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
        //        tRequest.ContentLength = byteArray.Length;

        //        using (Stream dataStream = tRequest.GetRequestStream())
        //        {
        //            dataStream.Write(byteArray, 0, byteArray.Length);
        //            using (WebResponse tResponse = tRequest.GetResponse())
        //            {
        //                using (Stream dataStreamResponse = tResponse.GetResponseStream())
        //                {
        //                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
        //                    {
        //                        String sResponseFromServer = tReader.ReadToEnd();
        //                        //Label3.Text = sResponseFromServer;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }

    public static class InvoiceCommonHelper
    {
        static ErrorHelper _InvoiceErrorHelper;
        public static ErrorHelper InvoiceErrorHelper
        {
            get
            {
                if (_InvoiceErrorHelper == null)
                    return _InvoiceErrorHelper = new ErrorHelper();

                return _InvoiceErrorHelper;
            }
            set
            {
                _InvoiceErrorHelper = value;
            }
        }
    }
}