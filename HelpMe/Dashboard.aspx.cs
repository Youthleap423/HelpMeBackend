using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HelpMe.BusinessAccess;
using HelpMe.Helpers;
using HelpMe.Shared.Utilities;

namespace HelpMe
{
    public partial class Dashboard : System.Web.UI.Page
    {
        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.ValidateSession())
                {
                    Response.Redirect("index.aspx", false);
                    return;
                }
            }
            catch (Exception ex)
            {
                LogManager.Log(ex);
            }
        }        
        #endregion

        #region Upload File
        protected byte[] ConvertFileToBinary(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs);
            byte[] Letter = new byte[fs.Length];
            br.Read(Letter, 0, (int)fs.Length);

            fs.Flush();
            br.Close();
            fs.Close();
            return Letter;
        }
        #endregion
    }
}