using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq.Expressions;
using System.Drawing;
using System.Diagnostics;
using System.Net.NetworkInformation;
//using HelpMe.Error;
using HelpMe.BusinessAccess;
//using HelpMe.Properties;
//using HelpMe.DocHelper;
//using HelpMe.UserInterface;
//using DevExpress.XtraGrid;

namespace HelpMe.HelpMeHelper
{
    public static class HelpMeCommonHelper
    {
        #region Public Variables
        public static bool CtrlPressed;
        public static bool IsDBSelected = false;
        //public static List<LstSelShapes> objLstSelShapes = new List<LstSelShapes>();
        //public class LstSelShapes
        //{
        //    public bool IsSelect;
        //    public string POSUKey;
        //    public string CabinetName;
        //    public int FloorKey;
        //    public string FloorName;
        //}
        #endregion

        #region Properties
        //public static string MessageTitle
        //{
        //    get
        //    {
        //        return HelpMe.Properties.Settings.Default.MessageTitle;
        //    }
        //}
        public static string SpecialCharaterMessage
        {
            get
            {
                return "Special characters are not allowed.";
            }
        }

        public static Int64 ActiveLoginId { get; set; }
        public static string ActiveLoginName { get; set; }
        public static Int64 ActiveUserRoleId { get; set; }
        public static string ActiveUserRoleName { get; set; }
        public static int UserType { get; set; }
        public static string LastSearchOption { get; set; }
        public static int SearchTmpId { get; set; }

        public static Int64 ActiveEmployeeId { get; set; }
        public static Int64 ActiveDepartmentId { get; set; }
        public static string ActiveDepartmentName { get; set; }
        public static string ActiveFolderPath { get; set; }

        public static DataTable dtColumnSettings { get; set; }
        public static string ReportLayoutPath { get; set; }
        public static string XMLPath { get; set; }
        public static string ParameterValue { get; set; }
        public static void EmptyCabinetCache(System.IO.DirectoryInfo directory)
        {
            foreach (System.IO.FileInfo file in directory.GetFiles())
            {
                try
                {
                    file.Delete();

                }
                catch { }
            }

            foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories())
            {
                try
                {
                    subDirectory.Delete(true);
                }
                catch { }
            }
        }
        public static string ConvertValueToDatetime(DateTime dtPropValue, string sFormat)
        {
            return ((System.DateTime)(dtPropValue)).ToString(sFormat);
        }
        public static string ConvertValueToNumber(decimal dblPropValue, int iDecimal, bool blnThousandSep, string sSymbol)
        {
            string sPropValue = ((System.Decimal)(dblPropValue)).ToString("N" + iDecimal.ToString());
            if (!blnThousandSep)
                sPropValue = sPropValue.Replace(",", string.Empty);

            if (!string.IsNullOrEmpty(sSymbol.Trim()))
                sPropValue = sSymbol.Trim() + " " + sPropValue;

            return sPropValue;
        }
        public static string ConvertBinaryToFile(string fPath, byte[] document)
        {
            FileStream fs = new FileStream(fPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(document);
            bw.Flush();
            fs.Flush();
            fs.Close();
            bw.Close();
            return fPath;
        }

        public static string ReportPath { get; set; }
        public static string Printer1 { get; set; }
        public static string Printer2 { get; set; }
        public static string Printer3 { get; set; }
        public static int PrintWhileSaving { get; set; }
        public static int AskForPrint { get; set; }
        public static int PrintPreview { get; set; }
        public static int DefaultDocumentStatusId { get; set; }
        public static int ArchiveDocumentStatusId { get; set; }
        public static string SMSUserName { get; set; }
        public static string SMSUserPassword { get; set; }

        public static Int64 AdminUserRoleId { get; set; }
        public static Int64 GeneralUserRoleId { get; set; }

        public static string DomainName { get; set; }
        public static string PCUserName { get; set; }
        public static string MachineName { get; set; }
        public static Int64 PCActiveLoginId { get; set; }
        public static string ExeType { get; set; }

        public static bool Lic_DemoVersion { get; set; }
        public static string Lic_ProjectCode { get; set; }
        public static string Lic_ProjectName { get; set; }
        public static string Lic_CompanyName { get; set; }
        public static string Lic_CustomerId { get; set; }
        public static DateTime Lic_FirstDate { get; set; }
        public static DateTime Lic_LastDate { get; set; }
        public static int Lic_MaxTillCount { get; set; }
        public static int Lic_MaxUserCount { get; set; }
        public static string Lic_Module { get; set; }
        public static bool Lic_LicenseMismatch { get; set; }

        public static string ServerName { get; set; }
        public static string DatabaseName { get; set; }
        public static string ServerUser { get; set; }
        public static string ServerPassword { get; set; }

        //public static frmDOCMain frmDOCMain { get; set; }
        //public static frmShowNotification frmShowNotification { get; set; }
        //public static frmDocSearch frmDocSearch { get; set; }
        public static SqlConnection DOCConnection { get; set; }

        public static string DOCConnectionString { get; set; }
        public static string DOCWizagCommonConnectionString { get; set; }
        public static string DOCDatabaseProvider { get; set; }

        //public static Cursor BusyCursor
        //{ get; set; }

        public static decimal DollarAmount { get; set; }
        //public static string DOCErrorHelperSubject
        //{
        //    get
        //    {
        //        return Settings.Default.DOCErrorHelperSubject;
        //    }
        //}
        public static int ItemsPerPage
        { get; set; }

        public static SqlConnection Connection { get; set; }
        //public static string ApplicationDatabaseVersion
        //{
        //    get
        //    {
        //        return HelpMe.Properties.Settings.Default.ApplicationDatabaseVersion;
        //    }
        //}
        public static string CurrentDatabaseVersion { get; set; }
        //public static object GetOLEDBConnString()
        //{
        //    string[] Temp = DOCCommonHelper.DOCConnectionString.Split(';');
        //    if (Temp[2].ToString().Substring(0, 3) == "UID")
        //        Temp[2] = "User ID=" + Temp[2].Split('=')[1];

        //    if (Temp[2].ToString().Substring(0, 7) == "User ID")
        //        return "Provider=SQLOLEDB;" + Temp[0].ToString() + ";" + Temp[1].ToString() + ";" + Temp[2].ToString() + ";" + Temp[3].ToString();
        //    else
        //        return "Provider=SQLOLEDB;Integrated Security=SSPI;Persist Security Info=True;" + Temp[0].ToString() + ";" + Temp[1].ToString();
        //}
        public static string StringToFormulaForString(string inputValue)
        {

            string result = "";
            string quote = "\"";
            string quoteQuote = "\"\"";

            //try
            //{

            result = inputValue != null ? inputValue : String.Empty;

            // Replace all (") with ("").
            result = result.Replace(quote, quoteQuote);

            // Add ("") around the whole string.
            result = quote + result + quote;
            //}
            //catch (Exception err)
            //{
            //   System.Diagnostics.Debug.WriteLine(err.Message);
            //   throw;
            // }

            return result;
        }

        public static bool IsNumeric(string s)
        {
            try
            {
                Double.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsIPAddress(string ipAddress)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ipAddress, @"^(25[0-5]|2[0-4]\d|[0-1]?\d?\d)(\.(25[0-5]|2[0-4]\d|[0-1]?\d?\d)){3}$");
        }
        public static bool IsValid_IP(string ip)
        {
            System.Net.IPAddress ot;
            bool valid = false;
            if (string.IsNullOrEmpty(ip))
            {
                valid = false;
            }
            else
            {
                valid = System.Net.IPAddress.TryParse(ip, out ot);
            }
            return valid;
        }

        //static ErrorHelper _DOCErrorHelper;
        //public static ErrorHelper DOCErrorHelper
        //{
        //    get
        //    {
        //        if (_DOCErrorHelper == null)
        //            return _DOCErrorHelper = new ErrorHelper();

        //        return _DOCErrorHelper;
        //    }
        //    set
        //    {
        //        _DOCErrorHelper = value;
        //    }
        //}
        #endregion

        #region Functions
        public static string GetMacAddress()
        {
            string sMacAddress = string.Empty;
            NetworkInterface nic = NetworkInterface.GetAllNetworkInterfaces().Where(n => n.OperationalStatus == OperationalStatus.Up).FirstOrDefault();
            if (nic != null)
                sMacAddress = Convert.ToString(nic.GetPhysicalAddress());

            return sMacAddress;
        }
        //public static bool IsValidate(Panel pnl, ToolTip toolTip, ref string sError)
        //{
        //    bool IsValid = true;
        //    foreach (Control ctrl in pnl.Controls)
        //    {
        //        try
        //        {
        //            if (Convert.ToString(ctrl.Tag).IndexOf("R") >= 0 && ctrl.Tag.ToString().Substring(1, 1) == "R")
        //            {
        //                switch (Convert.ToString(ctrl.GetType()))
        //                {
        //                    case "System.Windows.Forms.TextBox":
        //                        if (string.IsNullOrEmpty(((System.Windows.Forms.TextBox)ctrl).Text))
        //                        {
        //                            sError = Convert.ToString(toolTip.GetToolTip(ctrl));
        //                            ctrl.Focus();
        //                            IsValid = false;
        //                            break;
        //                        }
        //                        break;
        //                    case "System.Windows.Forms.ComboBox":
        //                        if (((System.Windows.Forms.ComboBox)ctrl).SelectedValue != null && Convert.ToInt32(((System.Windows.Forms.ComboBox)ctrl).SelectedValue) <= 0)
        //                        {
        //                            sError = Convert.ToString(toolTip.GetToolTip(ctrl));
        //                            ctrl.Focus();
        //                            IsValid = false;
        //                            break;
        //                        }
        //                        break;
        //                    default:
        //                        break;
        //                }
        //            }
        //            if (!IsValid)
        //                break;
        //        }
        //        catch { }
        //    }
        //    return IsValid;
        //}
        //public static void SetNormal(Panel pnl)
        //{
        //    foreach (Control ctrl in pnl.Controls)
        //    {
        //        try
        //        {
        //            switch (Convert.ToString(ctrl.GetType()))
        //            {
        //                case "System.Windows.Forms.TextBox":
        //                    ((System.Windows.Forms.TextBox)ctrl).Text = string.Empty;
        //                    break;
        //                case "System.Windows.Forms.ComboBox":
        //                    ((System.Windows.Forms.ComboBox)ctrl).SelectedValue = "0";
        //                    break;
        //                case "System.Windows.Forms.CheckBox":
        //                    ((System.Windows.Forms.CheckBox)ctrl).Checked = false;
        //                    break;
        //                case "System.Windows.Forms.RadioButton":
        //                    ((System.Windows.Forms.RadioButton)ctrl).Tag = string.Empty;
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //        catch { }
        //    }
        //}
        public static byte[] ConvertFileToBinary(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            BinaryReader br = new BinaryReader(fs);
            byte[] document = new byte[fs.Length];
            br.Read(document, 0, (int)fs.Length);

            fs.Flush();
            br.Close();
            fs.Close();
            return document;
        }
        //public static string ConvertBinaryToFile(byte[] document)
        //{
        //    string fPath = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + System.Guid.NewGuid().ToString() + ".vsd";
        //    FileStream fs = new FileStream(fPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        //    BinaryWriter bw = new BinaryWriter(fs);

        //    bw.Write(document);
        //    bw.Flush();
        //    fs.Flush();
        //    fs.Close();
        //    bw.Close();
        //    return fPath;
        //}
        public static bool CompareDate(DateTime d1, DateTime d2)
        {
            bool flg = false;

            if (d1.Year == d2.Year && d1.Month == d2.Month && d1.Day == d2.Day
                && d1.Hour == d2.Hour && d1.Minute == d2.Minute && d1.Second == d2.Second
                )
            {
                flg = true;
            }
            return flg;
        }
        public static bool CheckLength(int Maxlengh, string inputStr)
        {
            if (inputStr.Length > Maxlengh)
                return false;
            else
                return true;
        }
        public static bool ValidateString(string inputStr)
        {
            foreach (char ch in inputStr)
            {
                if ((ch >= 48 && ch <= 57) || (ch >= 97 && ch <= 122) || (ch >= 65 && ch <= 90) || (ch == 45) || (ch == 8))
                {

                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public static bool ValidateString(string inputStr, bool allowSpecialChars)
        {
            foreach (char ch in inputStr)
            {
                if (allowSpecialChars)
                {
                    if ((ch >= 48 && ch <= 57) || (ch >= 97 && ch <= 122) || (ch >= 65 && ch <= 90) || (ch == 44) ||
                        (ch == 45) || (ch == 95) || (ch == 32) || (ch == 36) || (ch == 8))
                    {

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if ((ch >= 48 && ch <= 57) || (ch >= 97 && ch <= 122) || (ch >= 65 && ch <= 90) || (ch == 45) || (ch == 32) || (ch == 8))
                    {

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool validateCharacter(char inputChar)
        {
            if (inputChar == '`' || inputChar == '!' || inputChar == '@' || inputChar == '$' || inputChar == '%' ||
                inputChar == '(' || inputChar == ')' || inputChar == '-' || inputChar == '+' || inputChar == '=' ||
                inputChar == '[' || inputChar == ']' || inputChar == '{' || inputChar == '}' || inputChar == '/' || inputChar == '\\' ||
                inputChar == '*' || inputChar == '<' || inputChar == '>' || inputChar == '|' || inputChar == ':' || inputChar == '?' || inputChar == '\"' ||
                inputChar == ' ' || inputChar == ',' || inputChar == '#' || inputChar == '$' || inputChar == '&' || inputChar == '^' || inputChar == '\'')
            {
                return false;
            }
            else
                return true;
        }
        public static bool IsAuthenticated(string sMenuName, int AuthType)
        {
            string sQuery = @"Select Count(Usr.LoginId) As UsrCount
                            From tblLogin Usr Inner Join tblUserRights UsrRights On Usr.UserRoleId = UsrRights.UserRoleId
                            Where Usr.LoginId = " + ActiveLoginId.ToString() + @" And UsrRights.MenuName = '" + sMenuName + @"' And 
	                            (Case " + AuthType.ToString() + @" When 0 Then UsrRights.IsSave When 1 Then UsrRights.IsUpdate When 2 Then UsrRights.IsDelete End) = 1";

            DataTable dt = BeanHelper.DBHelper.FillTable(sQuery);
            if (dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["UsrCount"]) > 0)
                return true;
            else
                return false;
        }

        public static string Encrypt(string strText)
        {
            try
            {
                byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(strText);
                string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
                return returnValue;
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
        public static string Decrypt(string strText)
        {
            try
            {
                byte[] encodedDataAsBytes = System.Convert.FromBase64String(strText);
                string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
                return returnValue;
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
        //public static string DemoLicenseKey()
        //{
        //    string sPrjInstallType = "DEMO";
        //    string sPrjCode = "Wizag_VIPL_QuickDoc";
        //    string sPrjName = "Quick Doc";
        //    string sCompanyName = "Demo Company";
        //    string sCustomerId = DOCCommonHelper.Encrypt(DOCCommonHelper.Encrypt(DOCCommonHelper.GetMacAddress()));
        //    string sFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        //    string sToDate = DateTime.Now.Date.AddMonths(1).ToString("dd-MMM-yyyy");
        //    string sMaxTillCount = "2";
        //    string sMaxUserCount = "2";
        //    string sLic_Module = string.Empty;
        //    string sInputString = sPrjInstallType + "|" + sPrjCode + "|" + sPrjName + "|" + sCompanyName + "|" + sCustomerId + "|" + sFromDate + "|" + sToDate + "|" + sMaxTillCount + "|" + sMaxUserCount + "|" + sLic_Module;

        //    DOCDatabaseConfiguration.Configurations.DatabaseSecurity obj = new DOCDatabaseConfiguration.Configurations.DatabaseSecurity();
        //    return obj.EncryptString(obj.EncryptString(sInputString));
        //}

        #endregion

        public class DateOfSelectedDay
        {
            public DateOfSelectedDay()
            { }

            public string DD;
            public string MM;
            public string YYYY;
        }
    }

    //public class ItemExpressionCondition
    //{
    //    StyleFormatCondition fcondition;
    //    public ItemExpressionCondition(StyleFormatCondition fcondition)
    //    {
    //        this.fcondition = fcondition;
    //    }
    //    public bool IsExpressionCondition
    //    {
    //        get
    //        {
    //            return fcondition.Condition == FormatConditionEnum.Expression;
    //        }
    //    }
    //    public override string ToString()
    //    {
    //        if (fcondition.Expression == string.Empty)
    //            return string.Format("Empty Condition {0}", Index);
    //        return fcondition.Expression;
    //    }
    //    public int Index { get { return fcondition.Collection.IndexOf(fcondition); } }
    //    public StyleFormatCondition Condition { get { return fcondition; } }
    //}

    //public class AdvancedCursors
    //{
    //    [DllImport("User32.dll")]
    //    private static extern IntPtr LoadCursorFromFile(String str);
    //    public static Cursor Create(string filename)
    //    {
    //        IntPtr hCursor = LoadCursorFromFile(filename);

    //        if (hCursor.ToInt32() == 0)
    //        {
    //            return new Cursor(hCursor);
    //        }
    //        else
    //        {
    //            if (!IntPtr.Zero.Equals(hCursor))
    //            {
    //                return new Cursor(hCursor);
    //            }
    //            else
    //            {
    //                throw new ApplicationException("Could not create cursor from file " + filename);
    //            }
    //        }
    //    }
    //}

    public class GenericSorter<T>
    {
        public IEnumerable<T> Sort(IEnumerable<T> source, string sortBy, string sortDirection)
        {
            var param = Expression.Parameter(typeof(T), "item");

            var sortExpression = Expression.Lambda<Func<T, object>>
                (Expression.Convert(Expression.Property(param, sortBy), typeof(object)), param);

            switch (sortDirection.ToLower())
            {
                case "asc":
                    return source.AsQueryable<T>().OrderBy<T, object>(sortExpression);
                default:
                    return source.AsQueryable<T>().OrderByDescending<T, object>(sortExpression);

            }
        }
    }
}