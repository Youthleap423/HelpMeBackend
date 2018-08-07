using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Device.Location;
using Stripe;
using System.Net.Mail;
using System.Net;

namespace WebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    //To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.ScriptService]
    public class HelpMeData : System.Web.Services.WebService
    {
        //Encryption Key :  2r0XiUb+GJPhA3OCvs+NOA== 
        //HelpMe

        //Client Info.
        #region Get Client Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientInfo(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Client = new Client();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice" + "/DefaultPics/profilepic.png";

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClient] @OpType = 'S', @ClientId = " + ClientId.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        Client.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        Client.ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        Client.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        Client.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        Client.Gender = Convert.ToInt32(dt.Rows[0]["Gender"]);
                        Client.GenderDisp = Convert.ToString(dt.Rows[0]["GenderDisp"]);
                        Client.Address1 = Convert.ToString(dt.Rows[0]["Address1"]);
                        Client.Address2 = Convert.ToString(dt.Rows[0]["Address2"]);
                        Client.CityId = Convert.ToInt64(dt.Rows[0]["City"]);
                        Client.City = Convert.ToString(dt.Rows[0]["CityName"]);
                        Client.POBox = Convert.ToString(dt.Rows[0]["POBox"]);
                        Client.StateId = Convert.ToInt64(dt.Rows[0]["State"]);
                        Client.State = Convert.ToString(dt.Rows[0]["StateName"]);
                        Client.CountryId = Convert.ToInt64(dt.Rows[0]["Country"]);
                        Client.Country = Convert.ToString(dt.Rows[0]["CountryName"]);
                        Client.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                        Client.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                        Client.Password = Convert.ToString(dt.Rows[0]["Password"]);
                        Client.ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[0]["ProfilePic"]);
                        Client.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);
                        Client.CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]);
                        Client.EndDate = Convert.ToString(dt.Rows[0]["EndDate"]);
                        Client.AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]);
                        Client.RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]);
                        Client.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        Client.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        Client.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        Client.IsClientProfile = Convert.ToInt32(dt.Rows[0]["IsClientProfile"]);
                        Client.Rating = Convert.ToDouble(dt.Rows[0]["Rating"]);
                        Client.Points = Convert.ToDouble(dt.Rows[0]["Points"]);
                        Client.HelpMe = Convert.ToDouble(dt.Rows[0]["HelpMe"]);
                        Client.Offered = Convert.ToDouble(dt.Rows[0]["Offered"]);
                        Client.Radious = Convert.ToDouble(dt.Rows[0]["Radious"]);

                        Client.BirthDate = Convert.ToString(dt.Rows[0]["BirthDate"]);
                        Client.IsBankInformation = Convert.ToInt32(dt.Rows[0]["IsBankInformation"]);
                        Client.BusinessTaxId = Convert.ToString(dt.Rows[0]["BusinessTaxId"]);
                        Client.PersonalIdNumber = Convert.ToString(dt.Rows[0]["PersonalIdNumber"]);
                        Client.BankAccountNumber = Convert.ToString(dt.Rows[0]["BankAccountNumber"]);
                        Client.RoutingNumber = Convert.ToString(dt.Rows[0]["RoutingNumber"]);
                        Client.PaymentMethod = Convert.ToInt32(dt.Rows[0]["PaymentMethod"]);
                        Client.PaymentMethodDisp = Convert.ToString(dt.Rows[0]["PaymentMethodDisp"]);
                        Client.LegalDocument = Convert.ToString(dt.Rows[0]["LegalDocument"]);

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = Client;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetClientInfo");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetClientInfo");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Get User
        #region Get User
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetUser(string AuthKey, string EmailId, string Password)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Client = new Client();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice" + "/DefaultPics/profilepic.png";

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspGetUser] @OpType = 'GetUser', @EmailId = '" + EmailId.Replace("'", "''") + "', @Password = '" + Password.Replace("'", "''") + "'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;
                        Client.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        Client.ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        Client.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        Client.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        Client.Gender = Convert.ToInt32(dt.Rows[0]["Gender"]);
                        Client.GenderDisp = Convert.ToString(dt.Rows[0]["GenderDisp"]);
                        Client.Address1 = Convert.ToString(dt.Rows[0]["Address1"]);
                        Client.Address2 = Convert.ToString(dt.Rows[0]["Address2"]);
                        Client.CityId = Convert.ToInt64(dt.Rows[0]["City"]);
                        Client.City = Convert.ToString(dt.Rows[0]["CityName"]);
                        Client.POBox = Convert.ToString(dt.Rows[0]["POBox"]);
                        Client.StateId = Convert.ToInt64(dt.Rows[0]["State"]);
                        Client.State = Convert.ToString(dt.Rows[0]["StateName"]);
                        Client.CountryId = Convert.ToInt64(dt.Rows[0]["Country"]);
                        Client.Country = Convert.ToString(dt.Rows[0]["CountryName"]);
                        Client.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                        Client.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                        Client.Password = Convert.ToString(dt.Rows[0]["Password"]);
                        Client.ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[0]["ProfilePic"]);
                        Client.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);
                        Client.CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]);
                        Client.EndDate = Convert.ToString(dt.Rows[0]["EndDate"]);
                        Client.AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]);
                        Client.RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]);
                        Client.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        Client.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        Client.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        Client.IsClientProfile = Convert.ToInt32(dt.Rows[0]["IsClientProfile"]);
                        Client.Rating = Convert.ToDouble(dt.Rows[0]["Rating"]);
                        Client.Points = Convert.ToDouble(dt.Rows[0]["Points"]);
                        Client.HelpMe = Convert.ToDouble(dt.Rows[0]["HelpMe"]);
                        Client.Offered = Convert.ToDouble(dt.Rows[0]["Offered"]);
                        Client.Radious = Convert.ToDouble(dt.Rows[0]["Radious"]);

                        Client.BirthDate = Convert.ToString(dt.Rows[0]["BirthDate"]);
                        Client.IsBankInformation = Convert.ToInt32(dt.Rows[0]["IsBankInformation"]);
                        Client.BusinessTaxId = Convert.ToString(dt.Rows[0]["BusinessTaxId"]);
                        Client.PersonalIdNumber = Convert.ToString(dt.Rows[0]["PersonalIdNumber"]);
                        Client.BankAccountNumber = Convert.ToString(dt.Rows[0]["BankAccountNumber"]);
                        Client.RoutingNumber = Convert.ToString(dt.Rows[0]["RoutingNumber"]);
                        Client.PaymentMethod = Convert.ToInt32(dt.Rows[0]["PaymentMethod"]);
                        Client.PaymentMethodDisp = Convert.ToString(dt.Rows[0]["PaymentMethodDisp"]);
                        Client.LegalDocument = Convert.ToString(dt.Rows[0]["LegalDocument"]);

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = Client;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetUser");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetUser");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Get All User
        #region Get All User
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetAllUser(string AuthKey, string SearchBy, Int32 ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Client = new List<Client>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice" + "/DefaultPics/profilepic.png";

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspGetUser] @OpType = 'GetAllUser', @ClientId = " + ClientId.ToString() + ", @SearchBy = '" + SearchBy.Replace("'", "''") + "'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Client.Add(new Client()
                            {
                                DataId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                                ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                                FirstName = Convert.ToString(dt.Rows[i]["FirstName"]),
                                LastName = Convert.ToString(dt.Rows[i]["LastName"]),
                                Gender = Convert.ToInt32(dt.Rows[i]["Gender"]),
                                GenderDisp = Convert.ToString(dt.Rows[i]["GenderDisp"]),
                                Address1 = Convert.ToString(dt.Rows[i]["Address1"]),
                                Address2 = Convert.ToString(dt.Rows[i]["Address2"]),
                                CityId = Convert.ToInt64(dt.Rows[i]["City"]),
                                City = Convert.ToString(dt.Rows[i]["CityName"]),
                                POBox = Convert.ToString(dt.Rows[i]["POBox"]),
                                StateId = Convert.ToInt64(dt.Rows[i]["State"]),
                                State = Convert.ToString(dt.Rows[i]["StateName"]),
                                CountryId = Convert.ToInt64(dt.Rows[i]["Country"]),
                                Country = Convert.ToString(dt.Rows[i]["CountryName"]),
                                PhoneNo = Convert.ToString(dt.Rows[i]["PhoneNo"]),
                                EmailId = Convert.ToString(dt.Rows[i]["EmailId"]),
                                Password = Convert.ToString(dt.Rows[i]["Password"]),
                                ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[i]["ProfilePic"]),
                                IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]),
                                CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                                EndDate = Convert.ToString(dt.Rows[i]["EndDate"]),
                                AcTokenId = Convert.ToString(dt.Rows[i]["AcTokenId"]),
                                RegisteredBy = Convert.ToString(dt.Rows[i]["RegisteredBy"]),
                                Latitude = Convert.ToDouble(dt.Rows[i]["Latitude"]),
                                Longitude = Convert.ToDouble(dt.Rows[i]["Longitude"]),
                                Altitude = Convert.ToDouble(dt.Rows[i]["Altitude"]),
                                IsClientProfile = Convert.ToInt32(dt.Rows[i]["IsClientProfile"]),
                                Radious = Convert.ToDouble(dt.Rows[i]["Radious"]),

                                BirthDate = Convert.ToString(dt.Rows[i]["BirthDate"]),
                                IsBankInformation = Convert.ToInt32(dt.Rows[i]["IsBankInformation"]),
                                BusinessTaxId = Convert.ToString(dt.Rows[i]["BusinessTaxId"]),
                                PersonalIdNumber = Convert.ToString(dt.Rows[i]["PersonalIdNumber"]),
                                BankAccountNumber = Convert.ToString(dt.Rows[i]["BankAccountNumber"]),
                                RoutingNumber = Convert.ToString(dt.Rows[i]["RoutingNumber"]),
                                PaymentMethod = Convert.ToInt32(dt.Rows[i]["PaymentMethod"]),
                                PaymentMethodDisp = Convert.ToString(dt.Rows[i]["PaymentMethodDisp"]),
                                LegalDocument = Convert.ToString(dt.Rows[i]["LegalDocument"])
                            });
                        }

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = Client;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetUser");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetUser");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Client Insert
        #region Client Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientInsert(string AuthKey, string FirstName, string LastName, string EmailId, string Password, string AcTokenId, string RegisteredBy, string ProfilePic)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Client = new Client();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    string sClientPicsPath = "http://helpme.devs-vis.com/webservice/ClientPics/";
                    string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/profilepic.png";

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClient] @OpType = 'IsExists', @RegisteredBy = '" + RegisteredBy + "', @EmailId = '" + EmailId.Replace("'", "''") + "'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["IsExists"]) == 1)
                        {
                            //if (Convert.ToInt32(dt.Rows[i]["RegisteredBy"]) == 2)
                            if (Convert.ToInt32(RegisteredBy) == 2)
                            {
                                blnDataFound = true;

                                dt = objDBHelper.FillTable("Exec [uspGetUser] @OpType = 'GetUser', @EmailId = '" + EmailId.Replace("'", "''") + "', @Password = '" + Password.Replace("'", "''") + "'");
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    blnDataFound = true;
                                    Client.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                                    Client.ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                                    Client.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                                    Client.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                                    Client.Gender = Convert.ToInt32(dt.Rows[0]["Gender"]);
                                    Client.GenderDisp = Convert.ToString(dt.Rows[0]["GenderDisp"]);
                                    Client.Address1 = Convert.ToString(dt.Rows[0]["Address1"]);
                                    Client.Address2 = Convert.ToString(dt.Rows[0]["Address2"]);
                                    Client.CityId = Convert.ToInt64(dt.Rows[0]["City"]);
                                    Client.City = Convert.ToString(dt.Rows[0]["CityName"]);
                                    Client.POBox = Convert.ToString(dt.Rows[0]["POBox"]);
                                    Client.StateId = Convert.ToInt64(dt.Rows[0]["State"]);
                                    Client.State = Convert.ToString(dt.Rows[0]["StateName"]);
                                    Client.CountryId = Convert.ToInt64(dt.Rows[0]["Country"]);
                                    Client.Country = Convert.ToString(dt.Rows[0]["CountryName"]);
                                    Client.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                                    Client.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                                    Client.Password = Convert.ToString(dt.Rows[0]["Password"]);
                                    Client.ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[0]["ProfilePic"]);
                                    Client.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);
                                    Client.CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]);
                                    Client.EndDate = Convert.ToString(dt.Rows[0]["EndDate"]);
                                    Client.AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]);
                                    Client.RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]);
                                    Client.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                                    Client.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                                    Client.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                                    Client.IsClientProfile = Convert.ToInt32(dt.Rows[0]["IsClientProfile"]);
                                    Client.Rating = Convert.ToDouble(dt.Rows[0]["Rating"]);
                                    Client.Points = Convert.ToDouble(dt.Rows[0]["Points"]);
                                    Client.HelpMe = Convert.ToDouble(dt.Rows[0]["HelpMe"]);
                                    Client.Offered = Convert.ToDouble(dt.Rows[0]["Offered"]);
                                    Client.Radious = Convert.ToDouble(dt.Rows[0]["Radious"]);

                                    Client.BirthDate = Convert.ToString(dt.Rows[0]["BirthDate"]);
                                    Client.IsBankInformation = Convert.ToInt32(dt.Rows[0]["IsBankInformation"]);
                                    Client.BusinessTaxId = Convert.ToString(dt.Rows[0]["BusinessTaxId"]);
                                    Client.PersonalIdNumber = Convert.ToString(dt.Rows[0]["PersonalIdNumber"]);
                                    Client.BankAccountNumber = Convert.ToString(dt.Rows[0]["BankAccountNumber"]);
                                    Client.RoutingNumber = Convert.ToString(dt.Rows[0]["RoutingNumber"]);
                                    Client.PaymentMethod = Convert.ToInt32(dt.Rows[0]["PaymentMethod"]);
                                    Client.PaymentMethodDisp = Convert.ToString(dt.Rows[0]["PaymentMethodDisp"]);
                                    Client.LegalDocument = Convert.ToString(dt.Rows[0]["LegalDocument"]);

                                    DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                                    DataConfirmation.IsError = false;
                                    DataConfirmation.ErrorNumber = 0;
                                    DataConfirmation.Error = string.Empty;
                                    DataConfirmation.DataConfirm_DataObject = Client;
                                }
                            }
                            else
                            {
                                blnDataFound = true;
                                DataConfirmation.DataId = 0;
                                DataConfirmation.IsError = true;
                                DataConfirmation.ErrorNumber = 9999;
                                DataConfirmation.Error = "User is already exists";
                                DataConfirmation.DataConfirm_DataObject = null;
                            }
                        }
                    }

                    if (!blnDataFound)
                    {
                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        string sClientInsert = @"Exec [uspClient] @ClientId = 0, @FirstName = '" + FirstName.Replace("'", "''") + "', @LastName = '" + LastName.Replace("'", "''") + @"', 
                                                                    @EmailId = '" + EmailId.Replace("'", "''") + "', @Password = '" + Password.Replace("'", "''") + "', @AcTokenId = '" + AcTokenId.Replace("'", "''") + @"', 
                                                                    @RegisteredBy = '" + RegisteredBy.Replace("'", "''") + @"', @OpType = 'I'";
                        dt = objDBHelper.FillTable(sClientInsert);
                        if (dt != null && dt.Rows.Count > 0)
                        {

                            bool bln = false;
                            if (!string.IsNullOrEmpty(ProfilePic))
                            {
                                objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                                objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                                string sError = string.Empty;
                                byte[] TextAsBytes = System.Convert.FromBase64String(ProfilePic);
                                sClientPicsPath = Server.MapPath(".") + "/ClientPics/" + Convert.ToString(dt.Rows[0]["ClientId"]) + ".png";
                                bln = ConvertBinaryToFile(sClientPicsPath, TextAsBytes, ref sError);
                                if (bln)
                                {
                                    sClientPicsPath = "http://helpme.devs-vis.com/webservice/ClientPics/" + Convert.ToString(dt.Rows[0]["ClientId"]) + ".png";
                                    objDBHelper.ExecuteNonQuery("Update tblClient Set ProfilePic = '" + sClientPicsPath + "' Where ClientId = " + Convert.ToString(dt.Rows[0]["ClientId"]));
                                }
                                else
                                    objDBHelper.ExecuteNonQuery("Update tblClient Set ProfilePic = '' Where ClientId = " + Convert.ToString(dt.Rows[0]["ClientId"]));
                            }

                            blnDataFound = true;
                            Client.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                            Client.ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                            Client.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                            Client.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                            Client.Gender = Convert.ToInt32(dt.Rows[0]["Gender"]);
                            Client.GenderDisp = Convert.ToString(dt.Rows[0]["GenderDisp"]);
                            Client.Address1 = Convert.ToString(dt.Rows[0]["Address1"]);
                            Client.Address2 = Convert.ToString(dt.Rows[0]["Address2"]);
                            Client.CityId = Convert.ToInt64(dt.Rows[0]["City"]);
                            Client.City = Convert.ToString(dt.Rows[0]["CityName"]);
                            Client.POBox = Convert.ToString(dt.Rows[0]["POBox"]);
                            Client.StateId = Convert.ToInt64(dt.Rows[0]["State"]);
                            Client.State = Convert.ToString(dt.Rows[0]["StateName"]);
                            Client.CountryId = Convert.ToInt64(dt.Rows[0]["Country"]);
                            Client.Country = Convert.ToString(dt.Rows[0]["CountryName"]);
                            Client.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                            Client.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                            Client.Password = Convert.ToString(dt.Rows[0]["Password"]);
                            Client.ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[0]["ProfilePic"]);
                            Client.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);
                            Client.CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]);
                            Client.EndDate = Convert.ToString(dt.Rows[0]["EndDate"]);
                            Client.AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]);
                            Client.RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]);
                            Client.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                            Client.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                            Client.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                            Client.IsClientProfile = Convert.ToInt32(dt.Rows[0]["IsClientProfile"]);
                            Client.Rating = Convert.ToDouble(dt.Rows[0]["Rating"]);
                            Client.Points = Convert.ToDouble(dt.Rows[0]["Points"]);
                            Client.HelpMe = Convert.ToDouble(dt.Rows[0]["HelpMe"]);
                            Client.Offered = Convert.ToDouble(dt.Rows[0]["Offered"]);

                            Client.BirthDate = Convert.ToString(dt.Rows[0]["BirthDate"]);
                            Client.IsBankInformation = Convert.ToInt32(dt.Rows[0]["IsBankInformation"]);
                            Client.BusinessTaxId = Convert.ToString(dt.Rows[0]["BusinessTaxId"]);
                            Client.PersonalIdNumber = Convert.ToString(dt.Rows[0]["PersonalIdNumber"]);
                            Client.BankAccountNumber = Convert.ToString(dt.Rows[0]["BankAccountNumber"]);
                            Client.RoutingNumber = Convert.ToString(dt.Rows[0]["RoutingNumber"]);
                            Client.PaymentMethod = Convert.ToInt32(dt.Rows[0]["PaymentMethod"]);
                            Client.PaymentMethodDisp = Convert.ToString(dt.Rows[0]["PaymentMethodDisp"]);
                            Client.LegalDocument = Convert.ToString(dt.Rows[0]["LegalDocument"]);

                            DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                            DataConfirmation.IsError = false;
                            DataConfirmation.ErrorNumber = 0;
                            DataConfirmation.Error = string.Empty;
                            DataConfirmation.DataConfirm_DataObject = Client;
                        }
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ClientInsert");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ClientInsert");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Client Update
        #region Client Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientUpdate(string AuthKey, Int64 ClientId, string FirstName, string LastName, int Gender, string Address1, string Address2, string City, string POBox, string State, string Country, string PhoneNo, string EmailId,
                                                string BirthDate, int IsBankInformation, string BusinessTaxId, string PersonalIdNumber, string BankAccountNumber, string RoutingNumber, string LegalDocument, int PaymentMethod)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Client = new Client();
            var DataConfirmation = new DataConfirmation();
            int OldIsBankInformation = 0;
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    //JobPostInsert(AuthKey, 3, "JobTitle", "JobDescription", null, 1, 0, 0, 52, 52, 52, 52, 52, 52, 2, "06/30/2017 10:10:10", 100, "", "", "", "");

                    string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice" + "/DefaultPics/profilepic.png";
                    string sQry = @"Exec [uspClient] 
                                    @ClientId = " + ClientId.ToString() + ", @FirstName = '" + FirstName.Replace("'", "''") + "', @LastName = '" + LastName.Replace("'", "''") + "', @Gender = " + Gender + @", 
                                    @Address1 = '" + Address1.Replace("'", "''") + "', @Address2 = '" + Address2.Replace("'", "''") + "', @City = '" + City.Replace("'", "''") + "', @POBox = '" + POBox.Replace("'", "''") + @"', 
                                    @State = '" + State.Replace("'", "''") + "', @Country = '" + Country.Replace("'", "''") + @"', @PhoneNo = '" + PhoneNo.Replace("'", "''") + "', @EmailId = '" + EmailId.Replace("'", "''") + @"', 
                    
                                    @BirthDate = '" + BirthDate + "', @IsBankInformation = " + IsBankInformation + ", @BusinessTaxId = '" + BusinessTaxId.Replace("'", "''") + "', @PersonalIdNumber = '" + PersonalIdNumber.Replace("'", "''") + @"', 
                                    @BankAccountNumber = '" + BankAccountNumber.Replace("'", "''") + "', @RoutingNumber = '" + RoutingNumber.Replace("'", "''") + "', @PaymentMethod = " + PaymentMethod.ToString() + ", @OpType = 'U'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    OldIsBankInformation = Convert.ToInt32(objDBHelper.ExecuteScalar("Select Isnull(IsBankInformation, 0) From tblClient Where ClientId = " + ClientId.ToString()));

                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        Client.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        Client.ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        Client.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        Client.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        Client.Gender = Convert.ToInt32(dt.Rows[0]["Gender"]);
                        Client.GenderDisp = Convert.ToString(dt.Rows[0]["GenderDisp"]);
                        Client.Address1 = Convert.ToString(dt.Rows[0]["Address1"]);
                        Client.Address2 = Convert.ToString(dt.Rows[0]["Address2"]);
                        Client.CityId = Convert.ToInt64(dt.Rows[0]["City"]);
                        Client.City = Convert.ToString(dt.Rows[0]["CityName"]);
                        Client.POBox = Convert.ToString(dt.Rows[0]["POBox"]);
                        Client.StateId = Convert.ToInt64(dt.Rows[0]["State"]);
                        Client.State = Convert.ToString(dt.Rows[0]["StateName"]);
                        Client.CountryId = Convert.ToInt64(dt.Rows[0]["Country"]);
                        Client.Country = Convert.ToString(dt.Rows[0]["CountryName"]);
                        Client.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                        Client.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                        Client.Password = Convert.ToString(dt.Rows[0]["Password"]);
                        Client.ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[0]["ProfilePic"]);
                        Client.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);
                        Client.CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]);
                        Client.EndDate = Convert.ToString(dt.Rows[0]["EndDate"]);
                        Client.AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]);
                        Client.RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]);
                        Client.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        Client.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        Client.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        Client.IsClientProfile = Convert.ToInt32(dt.Rows[0]["IsClientProfile"]);
                        Client.Rating = Convert.ToDouble(dt.Rows[0]["Rating"]);
                        Client.Points = Convert.ToDouble(dt.Rows[0]["Points"]);
                        Client.HelpMe = Convert.ToDouble(dt.Rows[0]["HelpMe"]);
                        Client.Offered = Convert.ToDouble(dt.Rows[0]["Offered"]);
                        Client.Radious = Convert.ToDouble(dt.Rows[0]["Radious"]);

                        Client.BirthDate = Convert.ToString(dt.Rows[0]["BirthDate"]);
                        Client.IsBankInformation = Convert.ToInt32(dt.Rows[0]["IsBankInformation"]);
                        Client.BusinessTaxId = Convert.ToString(dt.Rows[0]["BusinessTaxId"]);
                        Client.PersonalIdNumber = Convert.ToString(dt.Rows[0]["PersonalIdNumber"]);
                        Client.BankAccountNumber = Convert.ToString(dt.Rows[0]["BankAccountNumber"]);
                        Client.RoutingNumber = Convert.ToString(dt.Rows[0]["RoutingNumber"]);
                        Client.PaymentMethod = Convert.ToInt32(dt.Rows[0]["PaymentMethod"]);
                        Client.PaymentMethodDisp = Convert.ToString(dt.Rows[0]["PaymentMethodDisp"]);

                        bool bln = false;
                        string sError = string.Empty;
                        string sClientDocumentPath = "http://helpme.devs-vis.com/webservice/ClientDocument/";
                        if (!string.IsNullOrEmpty(LegalDocument))
                        {
                            objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                            objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                            byte[] TextAsBytes = System.Convert.FromBase64String(LegalDocument);
                            sClientDocumentPath = Server.MapPath(".") + "/ClientDocument/" + ClientId.ToString() + ".png";
                            bln = ConvertBinaryToFile(sClientDocumentPath, TextAsBytes, ref sError);
                            if (bln)
                            {
                                sClientDocumentPath = "http://helpme.devs-vis.com/webservice/ClientDocument/" + ClientId.ToString() + ".png";
                                objDBHelper.ExecuteNonQuery("Update tblClient Set LegalDocument = '" + sClientDocumentPath + "' Where ClientId = " + ClientId.ToString());
                            }
                            else
                                objDBHelper.ExecuteNonQuery("Update tblClient Set LegalDocument = '' Where ClientId = " + ClientId.ToString());
                        }

                        if ((OldIsBankInformation == 0 && Client.IsBankInformation == 1) || string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["StripeAccountId"])))
                        {
                            try
                            {
                                string Stripe_ApiKey = string.Empty;

                                int AppMode = Convert.ToInt32(objDBHelper.ExecuteScalar("Select Isnull((Select AppMode From tblGeneralSettings), 0)"));
                                if (AppMode == 0)
                                    Stripe_ApiKey = "sk_test_eLa53cuPkNlgl7RwI6Z9GIiM";
                                else
                                    Stripe_ApiKey = "sk_live_laT7frDDnFvoFRMNpO5efIIn";

                                StripeConfiguration.SetApiKey(Stripe_ApiKey);

                                ////string Stripe_ApiKey = "sk_live_laT7frDDnFvoFRMNpO5efIIn";      // "sk_test_eLa53cuPkNlgl7RwI6Z9GIiM";
                                //string Stripe_ApiKey = "sk_test_eLa53cuPkNlgl7RwI6Z9GIiM";      // "sk_live_laT7frDDnFvoFRMNpO5efIIn";
                                //StripeConfiguration.SetApiKey(Stripe_ApiKey);
                                
                                #region Create Stripe Account
                                var objLegalEntity = new StripeAccountLegalEntityOptions();
                                objLegalEntity.FirstName = Client.FirstName;
                                objLegalEntity.LastName = Client.LastName;
                                objLegalEntity.Type = "individual";
                                objLegalEntity.BirthDay = Convert.ToDateTime(Client.BirthDate).Day;
                                objLegalEntity.BirthMonth = Convert.ToDateTime(Client.BirthDate).Month;
                                objLegalEntity.BirthYear = Convert.ToDateTime(Client.BirthDate).Year;
                                objLegalEntity.BusinessName = Client.FirstName + " " + Client.LastName;
                                objLegalEntity.BusinessTaxId = Client.BusinessTaxId;
                                objLegalEntity.PersonalIdNumber = Client.PersonalIdNumber;
                                objLegalEntity.AddressLine1 = Client.Address1;
                                objLegalEntity.AddressLine2 = Client.Address2;
                                objLegalEntity.AddressCity = Client.City;
                                objLegalEntity.AddressState = Client.State.Substring(0, 2);
                                objLegalEntity.AddressPostalCode = Client.POBox;

                                var objAccountOptions = new StripeAccountCreateOptions()
                                {
                                    Email = Client.EmailId,
                                    Type = "custom",
                                    Country = "CA",
                                    LegalEntity = objLegalEntity,
                                    TosAcceptanceDate = DateTime.Now,
                                    TosAcceptanceIp = "192.168.1.1"
                                };

                                var objAccountService = new StripeAccountService();
                                StripeAccount objAccount = objAccountService.Create(objAccountOptions);

                                var objStripeResponse_Account = new WebService.StripeResponse_Account();
                                objStripeResponse_Account.DataId = 1;
                                objStripeResponse_Account.id = GetReponseValue("id", objAccount.StripeResponse.ResponseJson.ToString());
                                #endregion

                                #region Stripe File Upload
                                //try
                                //{
                                //    string sLegalDocumentFileId = "file_" + objStripeResponse_Account.id.ToString();
                                //    var filename = "ftp://23.239.203.150/wwwroot/webservice/DefaultPics/profilepic.png";
                                //    using (FileStream stream = File.Open(filename, FileMode.Open))
                                //    {
                                //        var fileService = new StripeFileUploadService();
                                //        StripeFileUpload file = fileService.Create(filename, stream, StripeFilePurpose.BusinessLogo);
                                //    }
                                //}
                                //catch { }


                                //WebClient request = new WebClient();
                                //string sFTPUser = "helpme";
                                //string sFTPPwd = "sBZRAVd4eE64";
                                //request.Credentials = new NetworkCredential(sFTPUser, sFTPPwd);
                                //try
                                //{
                                //    byte[] newFileData = request.DownloadData("ftp://23.239.203.150/wwwroot/webservice/DefaultPics/profilepic.png");
                                //    Stream fileStream = new MemoryStream(newFileData);

                                //    var obj = new StripeRequestOptions();
                                //    obj.StripeConnectAccountId = objStripeResponse_Account.id.ToString();
                                //    obj.ApiKey = Stripe_ApiKey;

                                //    var objStripeFileUploadService = new StripeFileUploadService(Stripe_ApiKey);
                                //    objStripeFileUploadService.ApiKey = Stripe_ApiKey;
                                //    objStripeFileUploadService.Create(sLegalDocumentFileId, fileStream, StripeFilePurpose.BusinessLogo, obj);
                                //}
                                //catch { }
                                #endregion

                                #region Create Stripe Card
                                var objBankAccountOptions = new StripeAccountBankAccountOptions();
                                objBankAccountOptions.AccountNumber = Client.BankAccountNumber;
                                objBankAccountOptions.Country = "CA";
                                objBankAccountOptions.Currency = "cad";
                                objBankAccountOptions.RoutingNumber = "11000-000";
                                var objStripeBankAccount = new StripeAccountUpdateOptions()
                                {
                                    ExternalBankAccount = objBankAccountOptions
                                };
                                var objBankAccountService = new StripeAccountService();
                                StripeAccount objBankAccount = objBankAccountService.Update(objStripeResponse_Account.id, objStripeBankAccount);

                                objDBHelper.ExecuteNonQuery("Update tblClient Set StripeAccountId = '" + objStripeResponse_Account.id + "' Where ClientId = " + Client.ClientId.ToString());
                                #endregion
                            }
                            catch { }
                        }


                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = Client;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ClientUpdate");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ClientUpdate");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Client Legal Document Remove
        #region Client Legal Document Remove
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientLegalDocumentRemove(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Client = new Client();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/profilepic.png";
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    objDBHelper.ExecuteNonQuery("Update tblClient Set LegalDocument = '' Where ClientId = " + ClientId.ToString());

                    DataTable dt = objDBHelper.FillTable("Exec uspClient @OpType = 'S', @ClientId = " + ClientId.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        Client.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        Client.ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        Client.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        Client.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        Client.Gender = Convert.ToInt32(dt.Rows[0]["Gender"]);
                        Client.GenderDisp = Convert.ToString(dt.Rows[0]["GenderDisp"]);
                        Client.Address1 = Convert.ToString(dt.Rows[0]["Address1"]);
                        Client.Address2 = Convert.ToString(dt.Rows[0]["Address2"]);
                        Client.CityId = Convert.ToInt64(dt.Rows[0]["City"]);
                        Client.City = Convert.ToString(dt.Rows[0]["CityName"]);
                        Client.POBox = Convert.ToString(dt.Rows[0]["POBox"]);
                        Client.StateId = Convert.ToInt64(dt.Rows[0]["State"]);
                        Client.State = Convert.ToString(dt.Rows[0]["StateName"]);
                        Client.CountryId = Convert.ToInt64(dt.Rows[0]["Country"]);
                        Client.Country = Convert.ToString(dt.Rows[0]["CountryName"]);
                        Client.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                        Client.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                        Client.Password = Convert.ToString(dt.Rows[0]["Password"]);
                        Client.ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[0]["ProfilePic"]);
                        Client.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);
                        Client.CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]);
                        Client.EndDate = Convert.ToString(dt.Rows[0]["EndDate"]);
                        Client.AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]);
                        Client.RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]);
                        Client.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        Client.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        Client.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        Client.IsClientProfile = Convert.ToInt32(dt.Rows[0]["IsClientProfile"]);
                        Client.Rating = Convert.ToDouble(dt.Rows[0]["Rating"]);
                        Client.Points = Convert.ToDouble(dt.Rows[0]["Points"]);
                        Client.HelpMe = Convert.ToDouble(dt.Rows[0]["HelpMe"]);
                        Client.Offered = Convert.ToDouble(dt.Rows[0]["Offered"]);
                        Client.Radious = Convert.ToDouble(dt.Rows[0]["Radious"]);

                        Client.BirthDate = Convert.ToString(dt.Rows[0]["BirthDate"]);
                        Client.IsBankInformation = Convert.ToInt32(dt.Rows[0]["IsBankInformation"]);
                        Client.BusinessTaxId = Convert.ToString(dt.Rows[0]["BusinessTaxId"]);
                        Client.PersonalIdNumber = Convert.ToString(dt.Rows[0]["PersonalIdNumber"]);
                        Client.BankAccountNumber = Convert.ToString(dt.Rows[0]["BankAccountNumber"]);
                        Client.RoutingNumber = Convert.ToString(dt.Rows[0]["RoutingNumber"]);
                        Client.PaymentMethod = Convert.ToInt32(dt.Rows[0]["PaymentMethod"]);
                        Client.PaymentMethodDisp = Convert.ToString(dt.Rows[0]["PaymentMethodDisp"]);
                        Client.LegalDocument = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["LegalDocument"])) ? string.Empty : Convert.ToString(dt.Rows[0]["LegalDocument"]);

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = Client;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ClientLegalDocumentRemove");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ClientLegalDocumentRemove");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Client Profile Pic Update
        #region Client Profile Pic Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientProfilePicUpdate(string AuthKey, Int64 ClientId, string ProfilePic)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Client = new Client();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                string sError = string.Empty;
                if (Decrypt == "HelpMe")
                {
                    string sClientPicsPath = "http://helpme.devs-vis.com/webservice/ClientPics/";
                    string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/profilepic.png";

                    bool bln = false;
                    if (!string.IsNullOrEmpty(ProfilePic))
                    {
                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                        byte[] TextAsBytes = System.Convert.FromBase64String(ProfilePic);
                        sClientPicsPath = Server.MapPath(".") + "/ClientPics/" + ClientId.ToString() + ".png";
                        bln = ConvertBinaryToFile(sClientPicsPath, TextAsBytes, ref sError);
                        if (bln)
                        {
                            sClientPicsPath = "http://helpme.devs-vis.com/webservice/ClientPics/" + ClientId.ToString() + ".png";
                            objDBHelper.ExecuteNonQuery("Update tblClient Set ProfilePic = '" + sClientPicsPath + "' Where ClientId = " + ClientId.ToString());
                        }
                        else
                            objDBHelper.ExecuteNonQuery("Update tblClient Set ProfilePic = '' Where ClientId = " + ClientId.ToString());
                    }

                    if (string.IsNullOrEmpty(sError))
                    {
                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        DataTable dt = objDBHelper.FillTable("Exec uspClient @OpType = 'S', @ClientId = " + ClientId.ToString());
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            blnDataFound = true;

                            Client.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                            Client.ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                            Client.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                            Client.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                            Client.Gender = Convert.ToInt32(dt.Rows[0]["Gender"]);
                            Client.GenderDisp = Convert.ToString(dt.Rows[0]["GenderDisp"]);
                            Client.Address1 = Convert.ToString(dt.Rows[0]["Address1"]);
                            Client.Address2 = Convert.ToString(dt.Rows[0]["Address2"]);
                            Client.CityId = Convert.ToInt64(dt.Rows[0]["City"]);
                            Client.City = Convert.ToString(dt.Rows[0]["CityName"]);
                            Client.POBox = Convert.ToString(dt.Rows[0]["POBox"]);
                            Client.StateId = Convert.ToInt64(dt.Rows[0]["State"]);
                            Client.State = Convert.ToString(dt.Rows[0]["StateName"]);
                            Client.CountryId = Convert.ToInt64(dt.Rows[0]["Country"]);
                            Client.Country = Convert.ToString(dt.Rows[0]["CountryName"]);
                            Client.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                            Client.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                            Client.Password = Convert.ToString(dt.Rows[0]["Password"]);
                            Client.ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[0]["ProfilePic"]);
                            Client.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);
                            Client.CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]);
                            Client.EndDate = Convert.ToString(dt.Rows[0]["EndDate"]);
                            Client.AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]);
                            Client.RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]);
                            Client.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                            Client.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                            Client.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                            Client.IsClientProfile = Convert.ToInt32(dt.Rows[0]["IsClientProfile"]);
                            Client.Rating = Convert.ToDouble(dt.Rows[0]["Rating"]);
                            Client.Points = Convert.ToDouble(dt.Rows[0]["Points"]);
                            Client.HelpMe = Convert.ToDouble(dt.Rows[0]["HelpMe"]);
                            Client.Offered = Convert.ToDouble(dt.Rows[0]["Offered"]);
                            Client.Radious = Convert.ToDouble(dt.Rows[0]["Radious"]);

                            Client.BirthDate = Convert.ToString(dt.Rows[0]["BirthDate"]);
                            Client.IsBankInformation = Convert.ToInt32(dt.Rows[0]["IsBankInformation"]);
                            Client.BusinessTaxId = Convert.ToString(dt.Rows[0]["BusinessTaxId"]);
                            Client.PersonalIdNumber = Convert.ToString(dt.Rows[0]["PersonalIdNumber"]);
                            Client.BankAccountNumber = Convert.ToString(dt.Rows[0]["BankAccountNumber"]);
                            Client.RoutingNumber = Convert.ToString(dt.Rows[0]["RoutingNumber"]);
                            Client.PaymentMethod = Convert.ToInt32(dt.Rows[0]["PaymentMethod"]);
                            Client.PaymentMethodDisp = Convert.ToString(dt.Rows[0]["PaymentMethodDisp"]);
                            Client.LegalDocument = Convert.ToString(dt.Rows[0]["LegalDocument"]);

                            DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                            DataConfirmation.IsError = false;
                            DataConfirmation.ErrorNumber = 0;
                            DataConfirmation.Error = string.Empty;
                            DataConfirmation.DataConfirm_DataObject = Client;
                        }
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    if (!string.IsNullOrEmpty(sError))
                        DataConfirmation.Error = sError;
                    else
                        DataConfirmation.Error = "No Record found.";

                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ClientProfilePicUpdate");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ClientProfilePicUpdate");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Client Profile Pic Remove
        #region Client Profile Pic Remove
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientProfilePicRemove(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Client = new Client();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/profilepic.png";
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    objDBHelper.ExecuteNonQuery("Update tblClient Set ProfilePic = '' Where ClientId = " + ClientId.ToString());

                    DataTable dt = objDBHelper.FillTable("Exec uspClient @OpType = 'S', @ClientId = " + ClientId.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        Client.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        Client.ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        Client.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        Client.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        Client.Gender = Convert.ToInt32(dt.Rows[0]["Gender"]);
                        Client.GenderDisp = Convert.ToString(dt.Rows[0]["GenderDisp"]);
                        Client.Address1 = Convert.ToString(dt.Rows[0]["Address1"]);
                        Client.Address2 = Convert.ToString(dt.Rows[0]["Address2"]);
                        Client.CityId = Convert.ToInt64(dt.Rows[0]["City"]);
                        Client.City = Convert.ToString(dt.Rows[0]["CityName"]);
                        Client.POBox = Convert.ToString(dt.Rows[0]["POBox"]);
                        Client.StateId = Convert.ToInt64(dt.Rows[0]["State"]);
                        Client.State = Convert.ToString(dt.Rows[0]["StateName"]);
                        Client.CountryId = Convert.ToInt64(dt.Rows[0]["Country"]);
                        Client.Country = Convert.ToString(dt.Rows[0]["CountryName"]);
                        Client.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                        Client.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                        Client.Password = Convert.ToString(dt.Rows[0]["Password"]);
                        Client.ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[0]["ProfilePic"]);
                        Client.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);
                        Client.CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]);
                        Client.EndDate = Convert.ToString(dt.Rows[0]["EndDate"]);
                        Client.AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]);
                        Client.RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]);
                        Client.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        Client.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        Client.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        Client.IsClientProfile = Convert.ToInt32(dt.Rows[0]["IsClientProfile"]);
                        Client.Rating = Convert.ToDouble(dt.Rows[0]["Rating"]);
                        Client.Points = Convert.ToDouble(dt.Rows[0]["Points"]);
                        Client.HelpMe = Convert.ToDouble(dt.Rows[0]["HelpMe"]);
                        Client.Offered = Convert.ToDouble(dt.Rows[0]["Offered"]);
                        Client.Radious = Convert.ToDouble(dt.Rows[0]["Radious"]);

                        Client.BirthDate = Convert.ToString(dt.Rows[0]["BirthDate"]);
                        Client.IsBankInformation = Convert.ToInt32(dt.Rows[0]["IsBankInformation"]);
                        Client.BusinessTaxId = Convert.ToString(dt.Rows[0]["BusinessTaxId"]);
                        Client.PersonalIdNumber = Convert.ToString(dt.Rows[0]["PersonalIdNumber"]);
                        Client.BankAccountNumber = Convert.ToString(dt.Rows[0]["BankAccountNumber"]);
                        Client.RoutingNumber = Convert.ToString(dt.Rows[0]["RoutingNumber"]);
                        Client.PaymentMethod = Convert.ToInt32(dt.Rows[0]["PaymentMethod"]);
                        Client.PaymentMethodDisp = Convert.ToString(dt.Rows[0]["PaymentMethodDisp"]);
                        Client.LegalDocument = Convert.ToString(dt.Rows[0]["LegalDocument"]);

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = Client;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ClientProfilePicRemove");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ClientProfilePicRemove");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Client Radious Update
        #region Client Radious Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientRadiousUpdate(string AuthKey, Int64 ClientId, double Radious)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Client = new Client();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                string sError = string.Empty;
                if (Decrypt == "HelpMe")
                {
                    string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/profilepic.png";

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    objDBHelper.ExecuteNonQuery("Update tblClient Set Radious = " + Radious + " Where ClientId = " + ClientId.ToString());

                    if (string.IsNullOrEmpty(sError))
                    {
                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        DataTable dt = objDBHelper.FillTable("Exec uspClient @OpType = 'S', @ClientId = " + ClientId.ToString());
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            blnDataFound = true;

                            Client.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                            Client.ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                            Client.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                            Client.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                            Client.Gender = Convert.ToInt32(dt.Rows[0]["Gender"]);
                            Client.GenderDisp = Convert.ToString(dt.Rows[0]["GenderDisp"]);
                            Client.Address1 = Convert.ToString(dt.Rows[0]["Address1"]);
                            Client.Address2 = Convert.ToString(dt.Rows[0]["Address2"]);
                            Client.CityId = Convert.ToInt64(dt.Rows[0]["City"]);
                            Client.City = Convert.ToString(dt.Rows[0]["CityName"]);
                            Client.POBox = Convert.ToString(dt.Rows[0]["POBox"]);
                            Client.StateId = Convert.ToInt64(dt.Rows[0]["State"]);
                            Client.State = Convert.ToString(dt.Rows[0]["StateName"]);
                            Client.CountryId = Convert.ToInt64(dt.Rows[0]["Country"]);
                            Client.Country = Convert.ToString(dt.Rows[0]["CountryName"]);
                            Client.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                            Client.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                            Client.Password = Convert.ToString(dt.Rows[0]["Password"]);
                            Client.ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[0]["ProfilePic"]);
                            Client.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);
                            Client.CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]);
                            Client.EndDate = Convert.ToString(dt.Rows[0]["EndDate"]);
                            Client.AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]);
                            Client.RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]);
                            Client.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                            Client.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                            Client.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                            Client.IsClientProfile = Convert.ToInt32(dt.Rows[0]["IsClientProfile"]);
                            Client.Rating = Convert.ToDouble(dt.Rows[0]["Rating"]);
                            Client.Points = Convert.ToDouble(dt.Rows[0]["Points"]);
                            Client.HelpMe = Convert.ToDouble(dt.Rows[0]["HelpMe"]);
                            Client.Offered = Convert.ToDouble(dt.Rows[0]["Offered"]);
                            Client.Radious = Convert.ToDouble(dt.Rows[0]["Radious"]);

                            Client.BirthDate = Convert.ToString(dt.Rows[0]["BirthDate"]);
                            Client.IsBankInformation = Convert.ToInt32(dt.Rows[0]["IsBankInformation"]);
                            Client.BusinessTaxId = Convert.ToString(dt.Rows[0]["BusinessTaxId"]);
                            Client.PersonalIdNumber = Convert.ToString(dt.Rows[0]["PersonalIdNumber"]);
                            Client.BankAccountNumber = Convert.ToString(dt.Rows[0]["BankAccountNumber"]);
                            Client.RoutingNumber = Convert.ToString(dt.Rows[0]["RoutingNumber"]);
                            Client.PaymentMethod = Convert.ToInt32(dt.Rows[0]["PaymentMethod"]);
                            Client.PaymentMethodDisp = Convert.ToString(dt.Rows[0]["PaymentMethodDisp"]);
                            Client.LegalDocument = Convert.ToString(dt.Rows[0]["LegalDocument"]);

                            DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                            DataConfirmation.IsError = false;
                            DataConfirmation.ErrorNumber = 0;
                            DataConfirmation.Error = string.Empty;
                            DataConfirmation.DataConfirm_DataObject = Client;
                        }
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    if (!string.IsNullOrEmpty(sError))
                        DataConfirmation.Error = sError;
                    else
                        DataConfirmation.Error = "No Record found.";

                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ClientProfilePicUpdate");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ClientProfilePicUpdate");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Client Watch Video Credit Point Add 
        #region Client Watch Video Credit Point Add
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientWatchVideoCreditPointAdd(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Client = new Client();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                string sError = string.Empty;
                if (Decrypt == "HelpMe")
                {
                    string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/profilepic.png";

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = @"Declare @CreditPoint As Int = 0
                                    Declare @ClientId As BigInt = " + ClientId.ToString() + @"

                                    Select @CreditPoint = Isnull(WatchVideo, 0) From tblGeneralSettings 
                                    Update tblClient Set CreditPoint = CreditPoint + @CreditPoint Where ClientId = @ClientId";

                    objDBHelper.ExecuteNonQuery(sQry);

                    if (string.IsNullOrEmpty(sError))
                    {
                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        DataTable dt = objDBHelper.FillTable("Exec uspClient @OpType = 'S', @ClientId = " + ClientId.ToString());
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            blnDataFound = true;

                            Client.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                            Client.ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                            Client.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                            Client.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                            Client.Gender = Convert.ToInt32(dt.Rows[0]["Gender"]);
                            Client.GenderDisp = Convert.ToString(dt.Rows[0]["GenderDisp"]);
                            Client.Address1 = Convert.ToString(dt.Rows[0]["Address1"]);
                            Client.Address2 = Convert.ToString(dt.Rows[0]["Address2"]);
                            Client.CityId = Convert.ToInt64(dt.Rows[0]["City"]);
                            Client.City = Convert.ToString(dt.Rows[0]["CityName"]);
                            Client.POBox = Convert.ToString(dt.Rows[0]["POBox"]);
                            Client.StateId = Convert.ToInt64(dt.Rows[0]["State"]);
                            Client.State = Convert.ToString(dt.Rows[0]["StateName"]);
                            Client.CountryId = Convert.ToInt64(dt.Rows[0]["Country"]);
                            Client.Country = Convert.ToString(dt.Rows[0]["CountryName"]);
                            Client.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                            Client.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                            Client.Password = Convert.ToString(dt.Rows[0]["Password"]);
                            Client.ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[0]["ProfilePic"]);
                            Client.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);
                            Client.CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]);
                            Client.EndDate = Convert.ToString(dt.Rows[0]["EndDate"]);
                            Client.AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]);
                            Client.RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]);
                            Client.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                            Client.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                            Client.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                            Client.IsClientProfile = Convert.ToInt32(dt.Rows[0]["IsClientProfile"]);
                            Client.Rating = Convert.ToDouble(dt.Rows[0]["Rating"]);
                            Client.Points = Convert.ToDouble(dt.Rows[0]["Points"]);
                            Client.HelpMe = Convert.ToDouble(dt.Rows[0]["HelpMe"]);
                            Client.Offered = Convert.ToDouble(dt.Rows[0]["Offered"]);
                            Client.Radious = Convert.ToDouble(dt.Rows[0]["Radious"]);

                            Client.BirthDate = Convert.ToString(dt.Rows[0]["BirthDate"]);
                            Client.IsBankInformation = Convert.ToInt32(dt.Rows[0]["IsBankInformation"]);
                            Client.BusinessTaxId = Convert.ToString(dt.Rows[0]["BusinessTaxId"]);
                            Client.PersonalIdNumber = Convert.ToString(dt.Rows[0]["PersonalIdNumber"]);
                            Client.BankAccountNumber = Convert.ToString(dt.Rows[0]["BankAccountNumber"]);
                            Client.RoutingNumber = Convert.ToString(dt.Rows[0]["RoutingNumber"]);
                            Client.PaymentMethod = Convert.ToInt32(dt.Rows[0]["PaymentMethod"]);
                            Client.PaymentMethodDisp = Convert.ToString(dt.Rows[0]["PaymentMethodDisp"]);
                            Client.LegalDocument = Convert.ToString(dt.Rows[0]["LegalDocument"]);

                            DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                            DataConfirmation.IsError = false;
                            DataConfirmation.ErrorNumber = 0;
                            DataConfirmation.Error = string.Empty;
                            DataConfirmation.DataConfirm_DataObject = Client;
                        }
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    if (!string.IsNullOrEmpty(sError))
                        DataConfirmation.Error = sError;
                    else
                        DataConfirmation.Error = "No Record found.";

                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ClientProfilePicUpdate");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ClientProfilePicUpdate");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Forgot Password
        #region ForgotPassword
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ForgotPassword(string AuthKey, string EmailId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Client = new Client();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice" + "/DefaultPics/profilepic.png";

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspGetUser] @OpType = 'ForgotPassword', @EmailId = '" + EmailId.Replace("'", "''") + "'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        Client.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        Client.ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        Client.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        Client.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        Client.Gender = Convert.ToInt32(dt.Rows[0]["Gender"]);
                        Client.GenderDisp = Convert.ToString(dt.Rows[0]["GenderDisp"]);
                        Client.Address1 = Convert.ToString(dt.Rows[0]["Address1"]);
                        Client.Address2 = Convert.ToString(dt.Rows[0]["Address2"]);
                        Client.CityId = Convert.ToInt64(dt.Rows[0]["City"]);
                        Client.City = Convert.ToString(dt.Rows[0]["CityName"]);
                        Client.POBox = Convert.ToString(dt.Rows[0]["POBox"]);
                        Client.StateId = Convert.ToInt64(dt.Rows[0]["State"]);
                        Client.State = Convert.ToString(dt.Rows[0]["StateName"]);
                        Client.CountryId = Convert.ToInt64(dt.Rows[0]["Country"]);
                        Client.Country = Convert.ToString(dt.Rows[0]["CountryName"]);
                        Client.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                        Client.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                        Client.Password = Convert.ToString(dt.Rows[0]["Password"]);
                        Client.ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[0]["ProfilePic"]);
                        Client.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);
                        Client.CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]);
                        Client.EndDate = Convert.ToString(dt.Rows[0]["EndDate"]);
                        Client.AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]);
                        Client.RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]);
                        Client.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        Client.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        Client.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        Client.IsClientProfile = Convert.ToInt32(dt.Rows[0]["IsClientProfile"]);
                        Client.Rating = Convert.ToDouble(dt.Rows[0]["Rating"]);
                        Client.Points = Convert.ToDouble(dt.Rows[0]["Points"]);
                        Client.HelpMe = Convert.ToDouble(dt.Rows[0]["HelpMe"]);
                        Client.Offered = Convert.ToDouble(dt.Rows[0]["Offered"]);
                        Client.Radious = Convert.ToDouble(dt.Rows[0]["Radious"]);

                        Client.BirthDate = Convert.ToString(dt.Rows[0]["BirthDate"]);
                        Client.IsBankInformation = Convert.ToInt32(dt.Rows[0]["IsBankInformation"]);
                        Client.BusinessTaxId = Convert.ToString(dt.Rows[0]["BusinessTaxId"]);
                        Client.PersonalIdNumber = Convert.ToString(dt.Rows[0]["PersonalIdNumber"]);
                        Client.BankAccountNumber = Convert.ToString(dt.Rows[0]["BankAccountNumber"]);
                        Client.RoutingNumber = Convert.ToString(dt.Rows[0]["RoutingNumber"]);
                        Client.PaymentMethod = Convert.ToInt32(dt.Rows[0]["PaymentMethod"]);
                        Client.PaymentMethodDisp = Convert.ToString(dt.Rows[0]["PaymentMethodDisp"]);
                        Client.LegalDocument = Convert.ToString(dt.Rows[0]["LegalDocument"]);

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = Client;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ForgotPassword");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ForgotPassword");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Change Password
        #region Client Change Password
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientChangePassword(string AuthKey, Int64 ClientId, string Password)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Client = new Client();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice" + "/DefaultPics/profilepic.png";

                    string sQry = @"Exec [uspClient] @ClientId = " + ClientId.ToString() + ", @Password = '" + Password.Replace("'", "''") + "', @OpType = 'CP'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        Client.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        Client.ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        Client.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        Client.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        Client.Gender = Convert.ToInt32(dt.Rows[0]["Gender"]);
                        Client.GenderDisp = Convert.ToString(dt.Rows[0]["GenderDisp"]);
                        Client.Address1 = Convert.ToString(dt.Rows[0]["Address1"]);
                        Client.Address2 = Convert.ToString(dt.Rows[0]["Address2"]);
                        Client.CityId = Convert.ToInt64(dt.Rows[0]["City"]);
                        Client.City = Convert.ToString(dt.Rows[0]["CityName"]);
                        Client.POBox = Convert.ToString(dt.Rows[0]["POBox"]);
                        Client.StateId = Convert.ToInt64(dt.Rows[0]["State"]);
                        Client.State = Convert.ToString(dt.Rows[0]["StateName"]);
                        Client.CountryId = Convert.ToInt64(dt.Rows[0]["Country"]);
                        Client.Country = Convert.ToString(dt.Rows[0]["CountryName"]);
                        Client.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                        Client.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                        Client.Password = Convert.ToString(dt.Rows[0]["Password"]);
                        Client.ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[0]["ProfilePic"]);
                        Client.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);
                        Client.CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]);
                        Client.EndDate = Convert.ToString(dt.Rows[0]["EndDate"]);
                        Client.AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]);
                        Client.RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]);
                        Client.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        Client.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        Client.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        Client.IsClientProfile = Convert.ToInt32(dt.Rows[0]["IsClientProfile"]);
                        Client.Rating = Convert.ToDouble(dt.Rows[0]["Rating"]);
                        Client.Points = Convert.ToDouble(dt.Rows[0]["Points"]);
                        Client.HelpMe = Convert.ToDouble(dt.Rows[0]["HelpMe"]);
                        Client.Offered = Convert.ToDouble(dt.Rows[0]["Offered"]);
                        Client.Radious = Convert.ToDouble(dt.Rows[0]["Radious"]);

                        Client.BirthDate = Convert.ToString(dt.Rows[0]["BirthDate"]);
                        Client.IsBankInformation = Convert.ToInt32(dt.Rows[0]["IsBankInformation"]);
                        Client.BusinessTaxId = Convert.ToString(dt.Rows[0]["BusinessTaxId"]);
                        Client.PersonalIdNumber = Convert.ToString(dt.Rows[0]["PersonalIdNumber"]);
                        Client.BankAccountNumber = Convert.ToString(dt.Rows[0]["BankAccountNumber"]);
                        Client.RoutingNumber = Convert.ToString(dt.Rows[0]["RoutingNumber"]);
                        Client.PaymentMethod = Convert.ToInt32(dt.Rows[0]["PaymentMethod"]);
                        Client.PaymentMethodDisp = Convert.ToString(dt.Rows[0]["PaymentMethodDisp"]);
                        Client.LegalDocument = Convert.ToString(dt.Rows[0]["LegalDocument"]);

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = Client;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ChangePassword");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ChangePassword");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Category Function
        #region Get Category
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCategory(string AuthKey)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Category = new List<Category>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    string sWebSiteImagePath = "http://helpme.devs-vis.com/Images/";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspCategory] @OpType = 'Get'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        Category.Add(new Category()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["CategoryId"]),
                            CategoryId = Convert.ToInt32(dt.Rows[i]["CategoryId"]),
                            CategoryName = Convert.ToString(dt.Rows[i]["CategoryName"]),
                            Icon1 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Icon1"])) == true ? string.Empty : sWebSiteImagePath + Convert.ToString(dt.Rows[i]["Icon1"]),
                            Icon2 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Icon2"])) == true ? string.Empty : sWebSiteImagePath + Convert.ToString(dt.Rows[i]["Icon2"]),
                            CategoryPoints = Convert.ToInt32(dt.Rows[i]["CategoryPoints"]),
                            ColorCode = Convert.ToString(dt.Rows[i]["ColorCode"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = Category;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetCategory");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetCategory");
                Context.Response.Write(data);
            }
        }
        #endregion

        //JobPost Insert
        #region JobPost Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JobPostInsert(string AuthKey, int ClientId, string JobTitle, string JobDescription, string JobPhoto, int CategoryId, int JobPostingPoints, double JobPostingAmount,
                                    float Latitude, float Longitude, float Altitude, float Latitude_1, float Longitude_1, float Altitude_1, int JobTimeFlag, int JobHour, string JobDoneTime, double JobAmount, int JobAmountFlag,
                                    string PaymentTime, string PaymentId, string PaymentStatus, string PaymentResponse, string JobPhoto1, string JobPhoto2, string JobPhoto3)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPost = new JobPost();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    string sJobPostPicPath = "http://helpme.devs-vis.com/webservice/JobPostPics/";
                    string sJobPostPicPath1 = "http://helpme.devs-vis.com/webservice/JobPostPics/";
                    string sJobPostPicPath2 = "http://helpme.devs-vis.com/webservice/JobPostPics/";
                    string sJobPostPicPath3 = "http://helpme.devs-vis.com/webservice/JobPostPics/";
                    string sQry = "Exec [dbo].[uspJobPost] @JobPostId = 0, @ClientId = " + ClientId + ", @JobTitle = '" + JobTitle.Replace("'", "''") + "', @JobDescription = '" + JobDescription.Replace("'", "''") + @"', 
                                                            @JobPhoto = '" + sJobPostPicPath + "', @JobPhoto1 = '" + sJobPostPicPath1 + "', @JobPhoto2 = '" + sJobPostPicPath2 + "', @JobPhoto3 = '" + sJobPostPicPath3 + @"', 
                                                            @IsFree = 0, @CategoryId = " + CategoryId + ", @JobPostingPoints = " + JobPostingPoints + @", 
                                                            @JobPostingAmount = " + JobPostingAmount + ", @Latitude = " + Latitude + ", @Longitude = " + Longitude + ", @Altitude = " + Altitude + ", @Latitude_1 = " + Latitude_1 + ", @Longitude_1 = " + Longitude_1 + ", @Altitude_1 = " + Altitude_1 + @", 
                                                            @JobTimeFlag = " + JobTimeFlag.ToString() + ", @JobHour = " + JobHour + ", @JobDoneTime = '" + JobDoneTime.Replace("'", "''") + "', @JobAmount = " + JobAmount + ", @JobAmountFlag = " + JobAmountFlag.ToString() + @",
                                                            @PaymentTime = '" + PaymentTime.Replace("'", "''") + "', @PaymentId = '" + PaymentId.Replace("'", "''") + "', @PaymentStatus = '" + PaymentStatus.Replace("'", "''") + @"', 
                                                            @PaymentResponse = '" + PaymentResponse.Replace("'", "''") + "', @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string sWebSiteImagePath = "http://helpme.devs-vis.com/Images/";
                        string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/jobpic.png";
                        bool bln = false;
                        string sError = string.Empty;

                        #region JobPhoto
                        if (!string.IsNullOrEmpty(JobPhoto))
                        {
                            byte[] TextAsBytes = System.Convert.FromBase64String(JobPhoto);
                            sJobPostPicPath = Server.MapPath(".") + "/JobPostPics/" + Convert.ToInt32(dt.Rows[0]["JobPostId"]) + ".png";
                            bln = ConvertBinaryToFile(sJobPostPicPath, TextAsBytes, ref sError);
                            if (!bln)
                                objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto = '" + sDefaultPicsPath + "' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        }
                        else
                            objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto = '" + sDefaultPicsPath + "' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        #endregion

                        #region JobPhoto1
                        if (!string.IsNullOrEmpty(JobPhoto1))
                        {
                            byte[] TextAsBytes = System.Convert.FromBase64String(JobPhoto1);
                            sJobPostPicPath1 = Server.MapPath(".") + "/JobPostPics/" + Convert.ToInt32(dt.Rows[0]["JobPostId"]) + "_1.png";
                            bln = ConvertBinaryToFile(sJobPostPicPath1, TextAsBytes, ref sError);
                            if (!bln)
                                objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto1 = '' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        }
                        else
                            objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto1 = '' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        #endregion

                        #region JobPhoto2
                        if (!string.IsNullOrEmpty(JobPhoto2))
                        {
                            byte[] TextAsBytes = System.Convert.FromBase64String(JobPhoto2);
                            sJobPostPicPath2 = Server.MapPath(".") + "/JobPostPics/" + Convert.ToInt32(dt.Rows[0]["JobPostId"]) + "_2.png";
                            bln = ConvertBinaryToFile(sJobPostPicPath2, TextAsBytes, ref sError);
                            if (!bln)
                                objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto2 = '' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        }
                        else
                            objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto2 = '' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        #endregion

                        #region JobPhoto3
                        if (!string.IsNullOrEmpty(JobPhoto3))
                        {
                            byte[] TextAsBytes = System.Convert.FromBase64String(JobPhoto3);
                            sJobPostPicPath3 = Server.MapPath(".") + "/JobPostPics/" + Convert.ToInt32(dt.Rows[0]["JobPostId"]) + "_3.png";
                            bln = ConvertBinaryToFile(sJobPostPicPath3, TextAsBytes, ref sError);
                            if (!bln)
                                objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto3 = '' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        }
                        else
                            objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto3 = '' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        #endregion

                        blnDataFound = true;
                        JobPost.DataId = Convert.ToInt32(dt.Rows[0]["JobPostId"]);
                        JobPost.JobPostId = Convert.ToInt64(dt.Rows[0]["JobPostId"]);
                        JobPost.ClientId = Convert.ToInt64(dt.Rows[0]["ClientId"]);
                        JobPost.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        JobPost.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        JobPost.JobTitle = Convert.ToString(dt.Rows[0]["JobTitle"]);
                        JobPost.JobDescription = Convert.ToString(dt.Rows[0]["JobDescription"]);

                        JobPost.JobPhoto = (bln == true) ? Convert.ToString(dt.Rows[0]["JobPhoto"]) : sDefaultPicsPath;
                        JobPost.JobPhoto1 = (bln == true) ? Convert.ToString(dt.Rows[0]["JobPhoto1"]) : string.Empty;
                        JobPost.JobPhoto2 = (bln == true) ? Convert.ToString(dt.Rows[0]["JobPhoto2"]) : string.Empty;
                        JobPost.JobPhoto3 = (bln == true) ? Convert.ToString(dt.Rows[0]["JobPhoto3"]) : string.Empty;
                        JobPost.IsFree = Convert.ToInt32(dt.Rows[0]["IsFree"]);

                        JobPost.CategoryId = Convert.ToInt32(dt.Rows[0]["CategoryId"]);
                        JobPost.CategoryName = Convert.ToString(dt.Rows[0]["CategoryName"]);
                        JobPost.CategoryIcon1 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["CategoryIcon1"])) == true ? string.Empty : sWebSiteImagePath + Convert.ToString(dt.Rows[0]["CategoryIcon1"]);
                        JobPost.CategoryIcon2 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["CategoryIcon2"])) == true ? string.Empty : sWebSiteImagePath + Convert.ToString(dt.Rows[0]["CategoryIcon2"]);
                        JobPost.CategoryColorCode = Convert.ToString(dt.Rows[0]["CategoryColorCode"]);

                        JobPost.JobPostingPoints = Convert.ToInt32(dt.Rows[0]["JobPostingPoints"]);
                        JobPost.JobPostingAmount = Convert.ToDouble(dt.Rows[0]["JobPostingAmount"]);

                        JobPost.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        JobPost.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        JobPost.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        JobPost.Latitude_1 = Convert.ToDouble(dt.Rows[0]["Latitude_1"]);
                        JobPost.Longitude_1 = Convert.ToDouble(dt.Rows[0]["Longitude_1"]);
                        JobPost.Altitude_1 = Convert.ToDouble(dt.Rows[0]["Altitude_1"]);

                        JobPost.JobTimeFlag = Convert.ToInt32(dt.Rows[0]["JobTimeFlag"]);
                        JobPost.JobHour = Convert.ToInt32(dt.Rows[0]["JobHour"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["JobDoneTime"])))
                            JobPost.JobDoneTime = Convert.ToDateTime(dt.Rows[0]["JobDoneTime"]).ToString("MM/dd/yyyy hh:mm:ss");

                        JobPost.JobAmount = Convert.ToDouble(dt.Rows[0]["JobAmount"]);
                        JobPost.JobAmountFlag = Convert.ToInt32(dt.Rows[0]["JobAmountFlag"]);
                        JobPost.PaymentTime = Convert.ToString(dt.Rows[0]["PaymentTime"]);
                        JobPost.PaymentId = Convert.ToString(dt.Rows[0]["PaymentId"]);
                        JobPost.PaymentStatus = Convert.ToString(dt.Rows[0]["PaymentStatus"]);
                        JobPost.PaymentResponse = Convert.ToString(dt.Rows[0]["PaymentResponse"]);
                        JobPost.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");

                        if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])))
                            JobPost.EndDate = Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");

                        JobPost.JobPostTimeDiff = Convert.ToString(dt.Rows[0]["JobPostTimeDiff"]);
                        JobPost.IsHire = Convert.ToInt32(dt.Rows[0]["IsHire"]);
                        JobPost.ChatGroupId = Convert.ToString(dt.Rows[0]["ChatGroupId"]);

                        if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ChatGroupId"])))
                        {
                            string sHisHer = Convert.ToInt32(dt.Rows[0]["Gender"]) == 0 ? "his" : "her";
                            string sRemarks = Convert.ToString(dt.Rows[0]["FirstName"]) + " " + Convert.ToString(dt.Rows[0]["LastName"]) + " invited you to put an offer on " + sHisHer + " Job Post";

                            string sClientJobPost = string.Empty;
                            DataTable dtClientJobPost = null;
                            try
                            {
                                //Job Posting on selected group
                                sClientJobPost = "Select Distinct ClientId From tblChatGroupMember Where ChatGroupId In (" + Convert.ToString(dt.Rows[0]["ChatGroupId"]) + ") And ClientId <> " + Convert.ToString(dt.Rows[0]["ClientId"]);
                                dtClientJobPost = objDBHelper.FillTable(sClientJobPost);
                                for (int i = 0; i < dtClientJobPost.Rows.Count; i++)
                                {
                                    InsertNotifications(AuthKey, Convert.ToInt64(dtClientJobPost.Rows[i]["ClientId"]), "JobPost", sRemarks, string.Empty, 5, Convert.ToString(dt.Rows[0]["JobPostId"]));
                                }
                            }
                            catch { }

                            try
                            {
                                //Job Posting on Area and Category
                                sClientJobPost = @"Select CM.ClientId 
                                                From tblJobPost JP 
	                                                Inner Join tblClientCategory CLC On JP.CategoryId = CLC.CategoryId
	                                                Inner Join tblClient CM On CLC.ClientId = CM.ClientId
                                                Where JP.JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]) + @" And
	                                                ([dbo].[Distance](JP.Latitude, JP.Longitude, Isnull(CM.Latitude, 0), Isnull(CM.Longitude, 0)) <= CM.Radious Or CM.Radious = 0)";
                                dtClientJobPost = objDBHelper.FillTable(sClientJobPost);
                                for (int i = 0; i < dtClientJobPost.Rows.Count; i++)
                                {
                                    if (Convert.ToInt64(dt.Rows[0]["ClientId"]) != Convert.ToInt64(dtClientJobPost.Rows[i]["ClientId"]))
                                    {
                                        InsertNotifications(AuthKey, Convert.ToInt64(dtClientJobPost.Rows[i]["ClientId"]), "JobPost", sRemarks, string.Empty, 5, Convert.ToString(dt.Rows[0]["JobPostId"]));
                                    }
                                }
                            }
                            catch { }
                        }

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["JobPostId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPost;
                    }
                    else
                    {
                        DataConfirmation.DataId = 0;
                        DataConfirmation.IsError = true;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = "No record found";
                        DataConfirmation.DataConfirm_DataObject = null;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "JobPostInsert");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "JobPostInsert");
                Context.Response.Write(data);
            }
        }
        #endregion

        //JobPost Update
        #region JobPost Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JobPostUpdate(string AuthKey, int JobPostId, int ClientId, string JobTitle, string JobDescription, string JobPhoto, int CategoryId, int JobPostingPoints, double JobPostingAmount,
                                    float Latitude, float Longitude, float Altitude, float Latitude_1, float Longitude_1, float Altitude_1, int JobTimeFlag, int JobHour, string JobDoneTime, double JobAmount, int JobAmountFlag,
                                    string PaymentTime, string PaymentId, string PaymentStatus, string PaymentResponse, string JobPhoto1, string JobPhoto2, string JobPhoto3)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPost = new JobPost();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    string sJobPostPicPath = "http://helpme.devs-vis.com/webservice/JobPostPics/";
                    string sJobPostPicPath1 = "http://helpme.devs-vis.com/webservice/JobPostPics/";
                    string sJobPostPicPath2 = "http://helpme.devs-vis.com/webservice/JobPostPics/";
                    string sJobPostPicPath3 = "http://helpme.devs-vis.com/webservice/JobPostPics/";
                    string sQry = "Exec [dbo].[uspJobPost] @JobPostId = " + JobPostId + ", @ClientId = " + ClientId + ", @JobTitle = '" + JobTitle.Replace("'", "''") + "', @JobDescription = '" + JobDescription.Replace("'", "''") + @"', 
                                                            @JobPhoto = '" + sJobPostPicPath + "', @IsFree = 0, @CategoryId = " + CategoryId + ", @JobPostingPoints = " + JobPostingPoints + @", 
                                                            @JobPostingAmount = " + JobPostingAmount + ", @Latitude = " + Latitude + ", @Longitude = " + Longitude + ", @Altitude = " + Altitude + ", @Latitude_1 = " + Latitude_1 + ", @Longitude_1 = " + Longitude_1 + ", @Altitude_1 = " + Altitude_1 + @", 
                                                            @JobTimeFlag = " + JobTimeFlag.ToString() + ", @JobHour = " + JobHour + ", @JobDoneTime = '" + JobDoneTime.Replace("'", "''") + "', @JobAmount = " + JobAmount + ", @JobAmountFlag = " + JobAmountFlag.ToString() + @", 
                                                            @PaymentTime = '" + PaymentTime.Replace("'", "''") + "', @PaymentId = '" + PaymentId.Replace("'", "''") + "', @PaymentStatus = '" + PaymentStatus.Replace("'", "''") + @"', 
                                                            @PaymentResponse = '" + PaymentResponse.Replace("'", "''") + "', @OpType = 'U'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string sWebSiteImagePath = "http://helpme.devs-vis.com/Images/";
                        string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/jobpic.png";

                        bool bln = false;
                        string sError = string.Empty;

                        #region JobPhoto
                        if (!string.IsNullOrEmpty(JobPhoto))
                        {
                            byte[] TextAsBytes = System.Convert.FromBase64String(JobPhoto);
                            sJobPostPicPath = Server.MapPath(".") + "/JobPostPics/" + Convert.ToInt32(dt.Rows[0]["JobPostId"]) + ".png";
                            bln = ConvertBinaryToFile(sJobPostPicPath, TextAsBytes, ref sError);
                            if (!bln)
                                objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto = '" + sDefaultPicsPath + "' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        }
                        else
                            objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto = '" + sDefaultPicsPath + "' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        #endregion

                        #region JobPhoto1
                        if (!string.IsNullOrEmpty(JobPhoto1))
                        {
                            byte[] TextAsBytes = System.Convert.FromBase64String(JobPhoto1);
                            sJobPostPicPath1 = Server.MapPath(".") + "/JobPostPics/" + Convert.ToInt32(dt.Rows[0]["JobPostId"]) + "_1.png";
                            bln = ConvertBinaryToFile(sJobPostPicPath1, TextAsBytes, ref sError);
                            if (!bln)
                                objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto1 = '' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        }
                        else
                            objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto1 = '' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        #endregion

                        #region JobPhoto2
                        if (!string.IsNullOrEmpty(JobPhoto2))
                        {
                            byte[] TextAsBytes = System.Convert.FromBase64String(JobPhoto2);
                            sJobPostPicPath2 = Server.MapPath(".") + "/JobPostPics/" + Convert.ToInt32(dt.Rows[0]["JobPostId"]) + "_2.png";
                            bln = ConvertBinaryToFile(sJobPostPicPath2, TextAsBytes, ref sError);
                            if (!bln)
                                objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto2 = '' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        }
                        else
                            objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto2 = '' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        #endregion

                        #region JobPhoto3
                        if (!string.IsNullOrEmpty(JobPhoto3))
                        {
                            byte[] TextAsBytes = System.Convert.FromBase64String(JobPhoto3);
                            sJobPostPicPath3 = Server.MapPath(".") + "/JobPostPics/" + Convert.ToInt32(dt.Rows[0]["JobPostId"]) + "_3.png";
                            bln = ConvertBinaryToFile(sJobPostPicPath3, TextAsBytes, ref sError);
                            if (!bln)
                                objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto3 = '' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        }
                        else
                            objDBHelper.ExecuteNonQuery("Update tblJobPost Set JobPhoto3 = '' Where JobPostId = " + Convert.ToString(dt.Rows[0]["JobPostId"]));
                        #endregion

                        blnDataFound = true;
                        JobPost.DataId = Convert.ToInt32(dt.Rows[0]["JobPostId"]);
                        JobPost.JobPostId = Convert.ToInt64(dt.Rows[0]["JobPostId"]);
                        JobPost.ClientId = Convert.ToInt64(dt.Rows[0]["ClientId"]);
                        JobPost.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        JobPost.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        JobPost.JobTitle = Convert.ToString(dt.Rows[0]["JobTitle"]);
                        JobPost.JobDescription = Convert.ToString(dt.Rows[0]["JobDescription"]);

                        JobPost.JobPhoto = (bln == true) ? Convert.ToString(dt.Rows[0]["JobPhoto"]) : sDefaultPicsPath;
                        JobPost.JobPhoto1 = (bln == true) ? Convert.ToString(dt.Rows[0]["JobPhoto1"]) : string.Empty;
                        JobPost.JobPhoto2 = (bln == true) ? Convert.ToString(dt.Rows[0]["JobPhoto2"]) : string.Empty;
                        JobPost.JobPhoto3 = (bln == true) ? Convert.ToString(dt.Rows[0]["JobPhoto3"]) : string.Empty;
                        JobPost.IsFree = Convert.ToInt32(dt.Rows[0]["IsFree"]);

                        JobPost.CategoryId = Convert.ToInt32(dt.Rows[0]["CategoryId"]);
                        JobPost.CategoryName = Convert.ToString(dt.Rows[0]["CategoryName"]);
                        JobPost.CategoryIcon1 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["CategoryIcon1"])) == true ? string.Empty : sWebSiteImagePath + Convert.ToString(dt.Rows[0]["CategoryIcon1"]);
                        JobPost.CategoryIcon2 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["CategoryIcon2"])) == true ? string.Empty : sWebSiteImagePath + Convert.ToString(dt.Rows[0]["CategoryIcon2"]);
                        JobPost.CategoryColorCode = Convert.ToString(dt.Rows[0]["CategoryColorCode"]);

                        JobPost.JobPostingPoints = Convert.ToInt32(dt.Rows[0]["JobPostingPoints"]);
                        JobPost.JobPostingAmount = Convert.ToDouble(dt.Rows[0]["JobPostingAmount"]);

                        JobPost.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        JobPost.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        JobPost.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        JobPost.Latitude_1 = Convert.ToDouble(dt.Rows[0]["Latitude_1"]);
                        JobPost.Longitude_1 = Convert.ToDouble(dt.Rows[0]["Longitude_1"]);
                        JobPost.Altitude_1 = Convert.ToDouble(dt.Rows[0]["Altitude_1"]);

                        JobPost.JobTimeFlag = Convert.ToInt32(dt.Rows[0]["JobTimeFlag"]);
                        JobPost.JobHour = Convert.ToInt32(dt.Rows[0]["JobHour"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["JobDoneTime"])))
                            JobPost.JobDoneTime = Convert.ToDateTime(dt.Rows[0]["JobDoneTime"]).ToString("MM/dd/yyyy hh:mm:ss");

                        JobPost.JobAmount = Convert.ToDouble(dt.Rows[0]["JobAmount"]);
                        JobPost.JobAmountFlag = Convert.ToInt32(dt.Rows[0]["JobAmountFlag"]);
                        JobPost.PaymentTime = Convert.ToString(dt.Rows[0]["PaymentTime"]);
                        JobPost.PaymentId = Convert.ToString(dt.Rows[0]["PaymentId"]);
                        JobPost.PaymentStatus = Convert.ToString(dt.Rows[0]["PaymentStatus"]);
                        JobPost.PaymentResponse = Convert.ToString(dt.Rows[0]["PaymentResponse"]);
                        JobPost.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");

                        JobPost.JobPostTimeDiff = Convert.ToString(dt.Rows[0]["JobPostTimeDiff"]);
                        JobPost.IsHire = Convert.ToInt32(dt.Rows[0]["IsHire"]);

                        if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])))
                            JobPost.EndDate = Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["JobPostId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPost;
                    }
                    else
                    {
                        DataConfirmation.DataId = 0;
                        DataConfirmation.IsError = true;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = "No record found";
                        DataConfirmation.DataConfirm_DataObject = null;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "JobPostUpdate");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "JobPostUpdate");
                Context.Response.Write(data);
            }
        }
        #endregion

        //JobPost_AcceptOffer
        #region JobPost_AcceptOffer
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JobPost_AcceptOffer(string AuthKey, int JobPostId, int ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPostOffer = new JobPostOffer();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspJobPostOffer] @JobPostId = " + JobPostId + ", @ClientId = '" + ClientId + "', @OpType = 'AcceptOffer'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string sRemarks = "Your job post offer has been approved.";
                        InsertNotifications(AuthKey, ClientId, "JobPost Offer Accept", sRemarks, string.Empty, 6, Convert.ToString(dt.Rows[0]["JobPostId"]));

                        blnDataFound = true;

                        JobPostOffer.DataId = Convert.ToInt32(dt.Rows[0]["JobPostOfferId"]);
                        JobPostOffer.JobPostOfferId = Convert.ToInt64(dt.Rows[0]["JobPostOfferId"]);
                        JobPostOffer.JobPostId = Convert.ToInt64(dt.Rows[0]["JobPostId"]);
                        JobPostOffer.ClientId = Convert.ToInt64(dt.Rows[0]["ClientId"]);
                        JobPostOffer.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        JobPostOffer.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        JobPostOffer.OfferAmount = Convert.ToDouble(dt.Rows[0]["OfferAmount"]);
                        JobPostOffer.IsHire = Convert.ToInt32(dt.Rows[0]["IsHire"]);
                        JobPostOffer.IsMyOffer = Convert.ToInt32(dt.Rows[0]["IsMyOffer"]);
                        JobPostOffer.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        JobPostOffer.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        JobPostOffer.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        JobPostOffer.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPostOffer.EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPostOffer.IssueRemarks = Convert.ToString(dt.Rows[0]["IssueRemarks"]);
                        JobPostOffer.CancelReason = Convert.ToString(dt.Rows[0]["CancelReason"]);
                        JobPostOffer.IssuePic = Convert.ToString(dt.Rows[0]["IssuePic"]);
                        JobPostOffer.JobPostTimeDiff = Convert.ToString(dt.Rows[0]["JobPostTimeDiff"]);

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["JobPostId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPostOffer;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "JobPost_AcceptOffer");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "JobPost_AcceptOffer");
                Context.Response.Write(data);
            }
        }
        #endregion

        //JobPost_RejectOffer
        #region JobPost_RejectOffer
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JobPost_RejectOffer(string AuthKey, int JobPostId, int ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPostOffer = new JobPostOffer();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspJobPostOffer] @JobPostId = " + JobPostId + ", @ClientId = '" + ClientId + "', @OpType = 'RejectOffer'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string sRemarks = "Your job post offer has been rejected.";
                        InsertNotifications(AuthKey, ClientId, "JobPost Offer Reject", sRemarks, string.Empty, 6, Convert.ToString(dt.Rows[0]["JobPostId"]));

                        blnDataFound = true;

                        JobPostOffer.DataId = Convert.ToInt32(dt.Rows[0]["JobPostOfferId"]);
                        JobPostOffer.JobPostOfferId = Convert.ToInt64(dt.Rows[0]["JobPostOfferId"]);
                        JobPostOffer.JobPostId = Convert.ToInt64(dt.Rows[0]["JobPostId"]);
                        JobPostOffer.ClientId = Convert.ToInt64(dt.Rows[0]["ClientId"]);
                        JobPostOffer.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        JobPostOffer.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        JobPostOffer.OfferAmount = Convert.ToDouble(dt.Rows[0]["OfferAmount"]);
                        JobPostOffer.IsHire = Convert.ToInt32(dt.Rows[0]["IsHire"]);
                        JobPostOffer.IsMyOffer = Convert.ToInt32(dt.Rows[0]["IsMyOffer"]);
                        JobPostOffer.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        JobPostOffer.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        JobPostOffer.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        JobPostOffer.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPostOffer.EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPostOffer.IssueRemarks = Convert.ToString(dt.Rows[0]["IssueRemarks"]);
                        JobPostOffer.CancelReason = Convert.ToString(dt.Rows[0]["CancelReason"]);
                        JobPostOffer.IssuePic = Convert.ToString(dt.Rows[0]["IssuePic"]);
                        JobPostOffer.JobPostTimeDiff = Convert.ToString(dt.Rows[0]["JobPostTimeDiff"]);

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["JobPostId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPostOffer;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "JobPost_RejectOffer");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "JobPost_RejectOffer");
                Context.Response.Write(data);
            }
        }
        #endregion

        //JobPost_CancelOffer
        #region JobPost_CancelOffer
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JobPost_CancelOffer(string AuthKey, int JobPostId, int ClientId, string CancelReason)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPostOffer = new JobPostOffer();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspJobPostOffer] @JobPostId = " + JobPostId + ", @ClientId = '" + ClientId + "', @CancelReason = '" + CancelReason.Replace("'", "''") + "', @OpType = 'CancelOffer'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string sRemarks = "Your job post offer has been cancelled.";
                        InsertNotifications(AuthKey, ClientId, "JobPost Offer Cancel", sRemarks, string.Empty, 9, Convert.ToString(dt.Rows[0]["JobPostId"]));

                        blnDataFound = true;

                        JobPostOffer.DataId = Convert.ToInt32(dt.Rows[0]["JobPostOfferId"]);
                        JobPostOffer.JobPostOfferId = Convert.ToInt64(dt.Rows[0]["JobPostOfferId"]);
                        JobPostOffer.JobPostId = Convert.ToInt64(dt.Rows[0]["JobPostId"]);
                        JobPostOffer.ClientId = Convert.ToInt64(dt.Rows[0]["ClientId"]);
                        JobPostOffer.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        JobPostOffer.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        JobPostOffer.OfferAmount = Convert.ToDouble(dt.Rows[0]["OfferAmount"]);
                        JobPostOffer.IsHire = Convert.ToInt32(dt.Rows[0]["IsHire"]);
                        JobPostOffer.IsMyOffer = Convert.ToInt32(dt.Rows[0]["IsMyOffer"]);
                        JobPostOffer.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        JobPostOffer.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        JobPostOffer.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        JobPostOffer.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPostOffer.EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPostOffer.IssueRemarks = Convert.ToString(dt.Rows[0]["IssueRemarks"]);
                        JobPostOffer.CancelReason = Convert.ToString(dt.Rows[0]["CancelReason"]);
                        JobPostOffer.IssuePic = Convert.ToString(dt.Rows[0]["IssuePic"]);
                        JobPostOffer.JobPostTimeDiff = Convert.ToString(dt.Rows[0]["JobPostTimeDiff"]);

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["JobPostId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPostOffer;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "JobPost_CancelOffer");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "JobPost_CancelOffer");
                Context.Response.Write(data);
            }
        }
        #endregion

        //JobPost_FinishOffer
        #region JobPost_FinishOffer
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JobPost_FinishOffer(string AuthKey, int JobPostId, int ClientId, int JobActualHour)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPostOffer = new JobPostOffer();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspJobPostOffer] @JobPostId = " + JobPostId + ", @ClientId = '" + ClientId + "', @JobActualHour = " + JobActualHour.ToString() + ", @OpType = 'FinishOffer'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string sRemarks = "Your Job marked as Completed.";
                        InsertNotifications(AuthKey, ClientId, "JobPost Complete", sRemarks, string.Empty, 7, Convert.ToString(dt.Rows[0]["JobPostId"]));

                        blnDataFound = true;

                        JobPostOffer.DataId = Convert.ToInt32(dt.Rows[0]["JobPostOfferId"]);
                        JobPostOffer.JobPostOfferId = Convert.ToInt64(dt.Rows[0]["JobPostOfferId"]);
                        JobPostOffer.JobPostId = Convert.ToInt64(dt.Rows[0]["JobPostId"]);
                        JobPostOffer.ClientId = Convert.ToInt64(dt.Rows[0]["ClientId"]);
                        JobPostOffer.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        JobPostOffer.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        JobPostOffer.OfferAmount = Convert.ToDouble(dt.Rows[0]["OfferAmount"]);
                        JobPostOffer.IsHire = Convert.ToInt32(dt.Rows[0]["IsHire"]);
                        JobPostOffer.IsMyOffer = Convert.ToInt32(dt.Rows[0]["IsMyOffer"]);
                        JobPostOffer.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        JobPostOffer.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        JobPostOffer.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        JobPostOffer.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPostOffer.EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPostOffer.IssueRemarks = Convert.ToString(dt.Rows[0]["IssueRemarks"]);
                        JobPostOffer.CancelReason = Convert.ToString(dt.Rows[0]["CancelReason"]);
                        JobPostOffer.IssuePic = Convert.ToString(dt.Rows[0]["IssuePic"]);
                        JobPostOffer.JobPostTimeDiff = Convert.ToString(dt.Rows[0]["JobPostTimeDiff"]);

                        #region Payment Calculation
                        string sHelper_StripeAccountId = Convert.ToString(dt.Rows[0]["Helper_StripeAccountId"]);
                        string sHelpSeeker_StripeAccountId = Convert.ToString(dt.Rows[0]["HelpSeeker_StripeAccountId"]);

                        int JobAmountFlag = Convert.ToInt32(dt.Rows[0]["JobAmountFlag"]);
                        double JobAmount = Convert.ToDouble(dt.Rows[0]["OfferAmount"]);
                        int JobHour = Convert.ToInt32(dt.Rows[0]["JobHour"]);

                        double TotalPayment = 0;
                        double RefundAmount = 0;
                        double StripeDeductionAmount = 0;
                        double DeductionAmount = 0;
                        double HelperAmount = 0;
                        if (JobAmountFlag == 0)
                        {
                            TotalPayment = JobAmount;
                            RefundAmount = 0;
                        }
                        else
                        {
                            if (JobActualHour >= JobHour)
                            {
                                TotalPayment = JobActualHour * JobAmount;
                                RefundAmount = 0;
                            }
                            else
                            {
                                TotalPayment = JobActualHour * JobAmount;
                                RefundAmount = (JobHour - JobActualHour) * JobAmount;
                            }
                        }
                        StripeDeductionAmount = (TotalPayment * 0.029) + 0.30;
                        DeductionAmount = (TotalPayment - StripeDeductionAmount) * 0.10;
                        HelperAmount = (TotalPayment - (StripeDeductionAmount + DeductionAmount));

                        sQry = @"Update tblJobPost Set 
                                    TotalPayment = " + TotalPayment + @", 
                                    RefundAmount = " + RefundAmount + @", 
                                    StripeDeductionAmount = " + StripeDeductionAmount + @",                                    
                                    DeductionAmount = " + DeductionAmount + @", 
                                    HelperAmount = " + HelperAmount + @"                                    
                                Where JobPostId = " + JobPostId;
                        objDBHelper.ExecuteNonQuery(sQry);
                        #endregion

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["JobPostId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPostOffer;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "JobPost_FinishOffer");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "JobPost_FinishOffer");
                Context.Response.Write(data);
            }
        }
        #endregion

        //JobPost_FinishOffer_Issue
        #region JobPost_FinishOffer_Issue
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JobPost_FinishOffer_Issue(string AuthKey, int JobPostId, int ClientId, int JobActualHour, string IssueRemarks, string IssuePic)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPostOffer = new JobPostOffer();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sIssuePicsPath = "http://helpme.devs-vis.com/webservice/IssuePics/";
                    bool bln = false;
                    if (!string.IsNullOrEmpty(IssuePic))
                    {
                        byte[] TextAsBytes = System.Convert.FromBase64String(IssuePic);
                        string sError = string.Empty;
                        sIssuePicsPath = Server.MapPath(".") + "/IssuePics/" + ClientId.ToString() + ".png";
                        bln = ConvertBinaryToFile(sIssuePicsPath, TextAsBytes, ref sError);
                        if (bln)
                        {
                            sIssuePicsPath = "http://helpme.devs-vis.com/webservice/IssuePics/J" + JobPostId.ToString() + "C" + ClientId.ToString() + ".png";
                            objDBHelper.ExecuteNonQuery("Update tblJobPostOffer Set IssuePic = '" + sIssuePicsPath + "' Where JobPostId = " + JobPostId.ToString() + " And ClientId = " + ClientId.ToString());
                        }
                        else
                            objDBHelper.ExecuteNonQuery("Update tblJobPostOffer Set IssuePic = '' Where JobPostId = " + JobPostId.ToString() + " And ClientId = " + ClientId.ToString());
                    }


                    string sQry = "Exec [dbo].[uspJobPostOffer] @JobPostId = " + JobPostId + ", @ClientId = '" + ClientId + "', @IssueRemarks = '" + IssueRemarks + "', @JobActualHour = " + JobActualHour.ToString() + ", @OpType = 'FinishOffer_Issue'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string sRemarks = "Your Job marked as Completed with issue updated. Issue Remarks : " + IssueRemarks;
                        InsertNotifications(AuthKey, ClientId, "JobPost Complete with Issue", sRemarks, string.Empty, 10, Convert.ToString(dt.Rows[0]["JobPostId"]));

                        blnDataFound = true;

                        JobPostOffer.DataId = Convert.ToInt32(dt.Rows[0]["JobPostOfferId"]);
                        JobPostOffer.JobPostOfferId = Convert.ToInt64(dt.Rows[0]["JobPostOfferId"]);
                        JobPostOffer.JobPostId = Convert.ToInt64(dt.Rows[0]["JobPostId"]);
                        JobPostOffer.ClientId = Convert.ToInt64(dt.Rows[0]["ClientId"]);
                        JobPostOffer.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        JobPostOffer.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        JobPostOffer.OfferAmount = Convert.ToDouble(dt.Rows[0]["OfferAmount"]);
                        JobPostOffer.IsHire = Convert.ToInt32(dt.Rows[0]["IsHire"]);
                        JobPostOffer.IsMyOffer = Convert.ToInt32(dt.Rows[0]["IsMyOffer"]);
                        JobPostOffer.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        JobPostOffer.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        JobPostOffer.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        JobPostOffer.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPostOffer.EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPostOffer.IssueRemarks = Convert.ToString(dt.Rows[0]["IssueRemarks"]);
                        JobPostOffer.CancelReason = Convert.ToString(dt.Rows[0]["CancelReason"]);
                        JobPostOffer.IssuePic = Convert.ToString(dt.Rows[0]["IssuePic"]);
                        JobPostOffer.JobPostTimeDiff = Convert.ToString(dt.Rows[0]["JobPostTimeDiff"]);

                        #region Payment Calculation
                        string sHelper_StripeAccountId = Convert.ToString(dt.Rows[0]["Helper_StripeAccountId"]);
                        string sHelpSeeker_StripeAccountId = Convert.ToString(dt.Rows[0]["HelpSeeker_StripeAccountId"]);

                        int JobAmountFlag = Convert.ToInt32(dt.Rows[0]["JobAmountFlag"]);
                        double JobAmount = Convert.ToDouble(dt.Rows[0]["OfferAmount"]);
                        int JobHour = Convert.ToInt32(dt.Rows[0]["JobHour"]);

                        double TotalPayment = 0;
                        double RefundAmount = 0;
                        double StripeDeductionAmount = 0;
                        double DeductionAmount = 0;
                        double HelperAmount = 0;
                        if (JobAmountFlag == 0)
                        {
                            TotalPayment = JobAmount;
                            RefundAmount = 0;
                        }
                        else
                        {
                            if (JobActualHour >= JobHour)
                            {
                                TotalPayment = JobActualHour * JobAmount;
                                RefundAmount = 0;
                            }
                            else
                            {
                                TotalPayment = JobActualHour * JobAmount;
                                RefundAmount = (JobHour - JobActualHour) * JobAmount;
                            }
                        }
                        StripeDeductionAmount = (TotalPayment * 0.029) + 0.30;
                        DeductionAmount = (TotalPayment - StripeDeductionAmount) * 0.10;
                        HelperAmount = (TotalPayment - (StripeDeductionAmount + DeductionAmount));

                        sQry = @"Update tblJobPost Set 
                                    TotalPayment = " + TotalPayment + @", 
                                    RefundAmount = " + RefundAmount + @", 
                                    StripeDeductionAmount = " + StripeDeductionAmount + @",                                    
                                    DeductionAmount = " + DeductionAmount + @", 
                                    HelperAmount = " + HelperAmount + @"                                    
                                Where JobPostId = " + JobPostId;
                        objDBHelper.ExecuteNonQuery(sQry);
                        #endregion

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["JobPostId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPostOffer;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "JobPost_FinishOffer_Issue");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "JobPost_FinishOffer_Issue");
                Context.Response.Write(data);
            }
        }
        #endregion

        //JobPostOffer Insert
        #region JobPostOffer Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JobPostOfferInsert(string AuthKey, int JobPostId, string ClientId, double OfferAmount, float Latitude, float Longitude, float Altitude)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPostOffer = new JobPostOffer();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspJobPostOffer] @JobPostOfferId = 0, @JobPostId = " + JobPostId + ", @ClientId = " + ClientId + ", @OfferAmount = " + OfferAmount + ", @Latitude = " + Latitude + ", @Longitude = " + Longitude + ", @Altitude = " + Altitude + ", @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        string sRemarks = Convert.ToString(dt.Rows[0]["FirstName"]) + " " + Convert.ToString(dt.Rows[0]["LastName"]) + " has put an offer on your Job Post";
                        InsertNotifications(AuthKey, Convert.ToInt64(dt.Rows[0]["JobPost_ClientId"]), "JobPost Offer", sRemarks, string.Empty, 1, Convert.ToString(dt.Rows[0]["JobPostId"]));

                        JobPostOffer.DataId = Convert.ToInt32(dt.Rows[0]["JobPostOfferId"]);
                        JobPostOffer.JobPostOfferId = Convert.ToInt64(dt.Rows[0]["JobPostOfferId"]);
                        JobPostOffer.JobPostId = Convert.ToInt64(dt.Rows[0]["JobPostId"]);
                        JobPostOffer.ClientId = Convert.ToInt64(dt.Rows[0]["ClientId"]);
                        JobPostOffer.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        JobPostOffer.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        JobPostOffer.OfferAmount = Convert.ToDouble(dt.Rows[0]["OfferAmount"]);
                        JobPostOffer.IsHire = Convert.ToInt32(dt.Rows[0]["IsHire"]);
                        JobPostOffer.IsMyOffer = Convert.ToInt32(dt.Rows[0]["IsMyOffer"]);
                        JobPostOffer.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        JobPostOffer.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        JobPostOffer.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        JobPostOffer.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPostOffer.EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPostOffer.IssueRemarks = Convert.ToString(dt.Rows[0]["IssueRemarks"]);
                        JobPostOffer.CancelReason = Convert.ToString(dt.Rows[0]["CancelReason"]);
                        JobPostOffer.IssuePic = Convert.ToString(dt.Rows[0]["IssuePic"]);
                        JobPostOffer.JobPostTimeDiff = Convert.ToString(dt.Rows[0]["JobPostTimeDiff"]);

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["JobPostOfferId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPostOffer;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "JobPostOfferInsert");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "JobPostOfferInsert");
                Context.Response.Write(data);
            }
        }
        #endregion

        //JobPostOffer Update
        #region JobPostOffer Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JobPostOfferUpdate(string AuthKey, int JobPostOfferId, int JobPostId, string ClientId, double OfferAmount, float Latitude, float Longitude, float Altitude)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPostOffer = new JobPostOffer();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspJobPostOffer] @JobPostOfferId = " + JobPostOfferId + ", @JobPostId = " + JobPostId + ", @ClientId = " + ClientId + ", @OfferAmount = " + OfferAmount + ", @Latitude = " + Latitude + ", @Longitude = " + Longitude + ", @Altitude = " + Altitude + ", @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        string sRemarks = Convert.ToString(dt.Rows[0]["FirstName"]) + " " + Convert.ToString(dt.Rows[0]["LastName"]) + " has modify an offer on your Job Post";
                        InsertNotifications(AuthKey, Convert.ToInt64(dt.Rows[0]["JobPost_ClientId"]), "JobPost Offer", sRemarks, string.Empty, 2, Convert.ToString(dt.Rows[0]["JobPostId"]));

                        JobPostOffer.DataId = Convert.ToInt32(dt.Rows[0]["JobPostOfferId"]);
                        JobPostOffer.JobPostOfferId = Convert.ToInt64(dt.Rows[0]["JobPostOfferId"]);
                        JobPostOffer.JobPostId = Convert.ToInt64(dt.Rows[0]["JobPostId"]);
                        JobPostOffer.ClientId = Convert.ToInt64(dt.Rows[0]["ClientId"]);
                        JobPostOffer.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        JobPostOffer.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        JobPostOffer.OfferAmount = Convert.ToDouble(dt.Rows[0]["OfferAmount"]);
                        JobPostOffer.IsHire = Convert.ToInt32(dt.Rows[0]["IsHire"]);
                        JobPostOffer.IsMyOffer = Convert.ToInt32(dt.Rows[0]["IsMyOffer"]);
                        JobPostOffer.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        JobPostOffer.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        JobPostOffer.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                        JobPostOffer.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPostOffer.EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPostOffer.IssueRemarks = Convert.ToString(dt.Rows[0]["IssueRemarks"]);
                        JobPostOffer.CancelReason = Convert.ToString(dt.Rows[0]["CancelReason"]);
                        JobPostOffer.IssuePic = Convert.ToString(dt.Rows[0]["IssuePic"]);
                        JobPostOffer.JobPostTimeDiff = Convert.ToString(dt.Rows[0]["JobPostTimeDiff"]);

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["JobPostOfferId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPostOffer;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "JobPostOfferUpdate");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "JobPostOfferUpdate");
                Context.Response.Write(data);
            }
        }
        #endregion

        //JobPostView Insert
        #region JobPostView Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JobPostViewInsert(string AuthKey, int JobPostId, string ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPostView = new JobPostView();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspJobPostView] @JobPostViewId = 0, @JobPostId = " + JobPostId + ", @ClientId = " + ClientId + ", @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        JobPostView.DataId = Convert.ToInt32(dt.Rows[0]["JobPostViewId"]);
                        JobPostView.JobPostViewId = Convert.ToInt64(dt.Rows[0]["JobPostViewId"]);
                        JobPostView.JobPostId = Convert.ToInt64(dt.Rows[0]["JobPostId"]);
                        JobPostView.ClientId = Convert.ToInt64(dt.Rows[0]["ClientId"]);
                        JobPostView.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        JobPostView.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        JobPostView.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["JobPostViewId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPostView;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "JobPostViewInsert");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "JobPostViewInsert");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Get JobPost Helper
        #region Get JobPost Helper
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetJobPost_Helper(string AuthKey, int ClientId, string CategoryId, string Keyword, int PageNumber, int Records)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPost = new List<JobPost>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    string sWebSiteImagePath = "http://helpme.devs-vis.com/Images/";
                    string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/jobpic.png";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspJobPostPaging] @ClientId = " + ClientId.ToString() + ", @CategoryId = '" + CategoryId.ToString() + "', @Keyword = '" + Keyword.Replace("'", "''") + "', @PageNumber = " + PageNumber.ToString() + ", @Records = " + Records.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            blnDataFound = true;
                            JobPost.Add(new JobPost()
                            {
                                DataId = Convert.ToInt32(dt.Rows[i]["JobPostId"]),
                                JobPostId = Convert.ToInt32(dt.Rows[i]["JobPostId"]),
                                ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                                FirstName = Convert.ToString(dt.Rows[i]["FirstName"]),
                                LastName = Convert.ToString(dt.Rows[i]["LastName"]),
                                JobTitle = Convert.ToString(dt.Rows[i]["JobTitle"]),
                                JobDescription = Convert.ToString(dt.Rows[i]["JobDescription"]),

                                JobPhoto = !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto"])) ? Convert.ToString(dt.Rows[i]["JobPhoto"]) : sDefaultPicsPath,
                                JobPhoto1 = !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto1"])) ? Convert.ToString(dt.Rows[i]["JobPhoto1"]) : string.Empty,
                                JobPhoto2 = !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto2"])) ? Convert.ToString(dt.Rows[i]["JobPhoto2"]) : string.Empty,
                                JobPhoto3 = !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto3"])) ? Convert.ToString(dt.Rows[i]["JobPhoto3"]) : string.Empty,
                                IsFree = Convert.ToInt32(dt.Rows[i]["IsFree"]),

                                CategoryId = Convert.ToInt32(dt.Rows[i]["CategoryId"]),
                                CategoryName = Convert.ToString(dt.Rows[i]["CategoryName"]),
                                CategoryIcon1 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["CategoryIcon1"])) == true ? string.Empty : sWebSiteImagePath + Convert.ToString(dt.Rows[i]["CategoryIcon1"]),
                                CategoryIcon2 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["CategoryIcon2"])) == true ? string.Empty : sWebSiteImagePath + Convert.ToString(dt.Rows[i]["CategoryIcon2"]),
                                CategoryColorCode = Convert.ToString(dt.Rows[i]["CategoryColorCode"]),

                                JobPostingPoints = Convert.ToInt32(dt.Rows[i]["JobPostingPoints"]),
                                JobPostingAmount = Convert.ToDouble(dt.Rows[i]["JobPostingAmount"]),
                                Latitude = Convert.ToDouble(dt.Rows[i]["Latitude"]),
                                Longitude = Convert.ToDouble(dt.Rows[i]["Longitude"]),
                                Altitude = Convert.ToDouble(dt.Rows[i]["Altitude"]),
                                Latitude_1 = Convert.ToDouble(dt.Rows[i]["Latitude_1"]),
                                Longitude_1 = Convert.ToDouble(dt.Rows[i]["Longitude_1"]),
                                Altitude_1 = Convert.ToDouble(dt.Rows[i]["Altitude_1"]),
                                JobTimeFlag = Convert.ToInt32(dt.Rows[i]["JobTimeFlag"]),
                                JobHour = Convert.ToInt32(dt.Rows[i]["JobHour"]),
                                JobDoneTime = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobDoneTime"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["JobDoneTime"]).ToString("MM/dd/yyyy hh:mm:ss"),
                                JobAmount = Convert.ToDouble(dt.Rows[i]["JobAmount"]),
                                JobAmountFlag = Convert.ToInt32(dt.Rows[i]["JobAmountFlag"]),
                                PaymentTime = Convert.ToString(dt.Rows[i]["PaymentTime"]),
                                PaymentId = Convert.ToString(dt.Rows[i]["PaymentId"]),
                                PaymentStatus = Convert.ToString(dt.Rows[i]["PaymentStatus"]),
                                PaymentResponse = Convert.ToString(dt.Rows[i]["PaymentResponse"]),
                                CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                                EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss"),
                                IsHire = Convert.ToInt32(dt.Rows[i]["IsHire"]),

                                RowNo = Convert.ToInt32(dt.Rows[i]["RowNo"]),
                                TotalRowNo = Convert.ToInt32(dt.Rows[i]["TotalRowNo"]),
                                TotalPage = Convert.ToInt32(dt.Rows[i]["TotalPage"]),
                                BestOffer = Convert.ToInt32(dt.Rows[i]["BestOffer"]),
                                JobPostTimeDiff = Convert.ToString(dt.Rows[i]["JobPostTimeDiff"])
                            });
                        }
                    }

                    if (blnDataFound)
                    {
                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["JobPostId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPost;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetJobPost_Helper");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetJobPost_Helper");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Get JobPost HelpSeeker
        #region Get JobPost HelpSeeker
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetJobPost_HelpSeeker(string AuthKey, int ClientId, string CategoryId, string Keyword, int PageNumber, int Records)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPost = new List<JobPost>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    string sWebSiteImagePath = "http://helpme.devs-vis.com/Images/";
                    string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/jobpic.png";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspJobPostPaging_HelpSeeker] @ClientId = " + ClientId.ToString() + ", @CategoryId = '" + CategoryId.ToString() + "', @Keyword = '" + Keyword.Replace("'", "''") + "', @PageNumber = " + PageNumber.ToString() + ", @Records = " + Records.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            blnDataFound = true;
                            JobPost.Add(new JobPost()
                            {
                                DataId = Convert.ToInt32(dt.Rows[i]["JobPostId"]),
                                JobPostId = Convert.ToInt32(dt.Rows[i]["JobPostId"]),
                                ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                                FirstName = Convert.ToString(dt.Rows[i]["FirstName"]),
                                LastName = Convert.ToString(dt.Rows[i]["LastName"]),
                                JobTitle = Convert.ToString(dt.Rows[i]["JobTitle"]),
                                JobDescription = Convert.ToString(dt.Rows[i]["JobDescription"]),

                                JobPhoto = !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto"])) ? Convert.ToString(dt.Rows[i]["JobPhoto"]) : sDefaultPicsPath,
                                JobPhoto1 = !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto1"])) ? Convert.ToString(dt.Rows[i]["JobPhoto1"]) : string.Empty,
                                JobPhoto2 = !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto2"])) ? Convert.ToString(dt.Rows[i]["JobPhoto2"]) : string.Empty,
                                JobPhoto3 = !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto3"])) ? Convert.ToString(dt.Rows[i]["JobPhoto3"]) : string.Empty,
                                IsFree = Convert.ToInt32(dt.Rows[i]["IsFree"]),

                                CategoryId = Convert.ToInt32(dt.Rows[i]["CategoryId"]),
                                CategoryName = Convert.ToString(dt.Rows[i]["CategoryName"]),
                                CategoryIcon1 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["CategoryIcon1"])) == true ? string.Empty : sWebSiteImagePath + Convert.ToString(dt.Rows[i]["CategoryIcon1"]),
                                CategoryIcon2 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["CategoryIcon2"])) == true ? string.Empty : sWebSiteImagePath + Convert.ToString(dt.Rows[i]["CategoryIcon2"]),
                                CategoryColorCode = Convert.ToString(dt.Rows[i]["CategoryColorCode"]),

                                JobPostingPoints = Convert.ToInt32(dt.Rows[i]["JobPostingPoints"]),
                                JobPostingAmount = Convert.ToDouble(dt.Rows[i]["JobPostingAmount"]),
                                Latitude = Convert.ToDouble(dt.Rows[i]["Latitude"]),
                                Longitude = Convert.ToDouble(dt.Rows[i]["Longitude"]),
                                Altitude = Convert.ToDouble(dt.Rows[i]["Altitude"]),
                                Latitude_1 = Convert.ToDouble(dt.Rows[i]["Latitude_1"]),
                                Longitude_1 = Convert.ToDouble(dt.Rows[i]["Longitude_1"]),
                                Altitude_1 = Convert.ToDouble(dt.Rows[i]["Altitude_1"]),
                                JobTimeFlag = Convert.ToInt32(dt.Rows[i]["JobTimeFlag"]),
                                JobHour = Convert.ToInt32(dt.Rows[i]["JobHour"]),
                                JobDoneTime = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobDoneTime"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["JobDoneTime"]).ToString("MM/dd/yyyy hh:mm:ss"),
                                JobAmount = Convert.ToDouble(dt.Rows[i]["JobAmount"]),
                                JobAmountFlag = Convert.ToInt32(dt.Rows[i]["JobAmountFlag"]),
                                PaymentTime = Convert.ToString(dt.Rows[i]["PaymentTime"]),
                                PaymentId = Convert.ToString(dt.Rows[i]["PaymentId"]),
                                PaymentStatus = Convert.ToString(dt.Rows[i]["PaymentStatus"]),
                                PaymentResponse = Convert.ToString(dt.Rows[i]["PaymentResponse"]),
                                CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                                EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss"),
                                IsHire = Convert.ToInt32(dt.Rows[i]["IsHire"]),

                                RowNo = Convert.ToInt32(dt.Rows[i]["RowNo"]),
                                TotalRowNo = Convert.ToInt32(dt.Rows[i]["TotalRowNo"]),
                                TotalPage = Convert.ToInt32(dt.Rows[i]["TotalPage"]),
                                BestOffer = Convert.ToInt32(dt.Rows[i]["BestOffer"]),
                                JobPostTimeDiff = Convert.ToString(dt.Rows[i]["JobPostTimeDiff"])
                            });
                        }
                    }

                    if (blnDataFound)
                    {
                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["JobPostId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPost;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetJobPost_HelpSeeker");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetJobPost_HelpSeeker");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Get JobPost Detail
        #region Get JobPost Detail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetJobPostDetail(string AuthKey, int JobPostId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPost = new JobPost();
            var JobPostOffer = new List<JobPostOffer>();
            var JobPostView = new List<JobPostView>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    string sWebSiteImagePath = "http://helpme.devs-vis.com/Images/";
                    string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/jobpic.png";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataSet ds = objDBHelper.FillDataset("Exec [uspJobPost] @OpType = 'GetJobPost', @JobPostId = " + JobPostId.ToString());
                    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        blnDataFound = true;
                        JobPost.DataId = Convert.ToInt32(ds.Tables[0].Rows[0]["JobPostId"]);
                        JobPost.JobPostId = Convert.ToInt32(ds.Tables[0].Rows[0]["JobPostId"]);
                        JobPost.ClientId = Convert.ToInt32(ds.Tables[0].Rows[0]["ClientId"]);
                        JobPost.FirstName = Convert.ToString(ds.Tables[0].Rows[0]["FirstName"]);
                        JobPost.LastName = Convert.ToString(ds.Tables[0].Rows[0]["LastName"]);
                        JobPost.JobTitle = Convert.ToString(ds.Tables[0].Rows[0]["JobTitle"]);
                        JobPost.JobDescription = Convert.ToString(ds.Tables[0].Rows[0]["JobDescription"]);

                        JobPost.JobPhoto = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["JobPhoto"])) ? Convert.ToString(ds.Tables[0].Rows[0]["JobPhoto"]) : sDefaultPicsPath;
                        JobPost.JobPhoto1 = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["JobPhoto1"])) ? Convert.ToString(ds.Tables[0].Rows[0]["JobPhoto1"]) : string.Empty;
                        JobPost.JobPhoto2 = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["JobPhoto2"])) ? Convert.ToString(ds.Tables[0].Rows[0]["JobPhoto2"]) : string.Empty;
                        JobPost.JobPhoto3 = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["JobPhoto3"])) ? Convert.ToString(ds.Tables[0].Rows[0]["JobPhoto3"]) : string.Empty;
                        JobPost.IsFree = Convert.ToInt32(ds.Tables[0].Rows[0]["IsFree"]);

                        JobPost.CategoryId = Convert.ToInt32(ds.Tables[0].Rows[0]["CategoryId"]);
                        JobPost.CategoryName = Convert.ToString(ds.Tables[0].Rows[0]["CategoryName"]);
                        JobPost.CategoryIcon1 = string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CategoryIcon1"])) == true ? string.Empty : sWebSiteImagePath + Convert.ToString(ds.Tables[0].Rows[0]["CategoryIcon1"]);
                        JobPost.CategoryIcon2 = string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CategoryIcon2"])) == true ? string.Empty : sWebSiteImagePath + Convert.ToString(ds.Tables[0].Rows[0]["CategoryIcon2"]);
                        JobPost.CategoryColorCode = Convert.ToString(ds.Tables[0].Rows[0]["CategoryColorCode"]);

                        JobPost.JobPostingPoints = Convert.ToInt32(ds.Tables[0].Rows[0]["JobPostingPoints"]);
                        JobPost.JobPostingAmount = Convert.ToDouble(ds.Tables[0].Rows[0]["JobPostingAmount"]);

                        JobPost.Latitude = Convert.ToDouble(ds.Tables[0].Rows[0]["Latitude"]);
                        JobPost.Longitude = Convert.ToDouble(ds.Tables[0].Rows[0]["Longitude"]);
                        JobPost.Altitude = Convert.ToDouble(ds.Tables[0].Rows[0]["Altitude"]);
                        JobPost.Latitude_1 = Convert.ToDouble(ds.Tables[0].Rows[0]["Latitude_1"]);
                        JobPost.Longitude_1 = Convert.ToDouble(ds.Tables[0].Rows[0]["Longitude_1"]);
                        JobPost.Altitude_1 = Convert.ToDouble(ds.Tables[0].Rows[0]["Altitude_1"]);

                        JobPost.JobTimeFlag = Convert.ToInt32(ds.Tables[0].Rows[0]["JobTimeFlag"]);
                        JobPost.JobHour = Convert.ToInt32(ds.Tables[0].Rows[0]["JobHour"]);
                        JobPost.JobDoneTime = string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["JobDoneTime"])) == true ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["JobDoneTime"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPost.JobAmount = Convert.ToDouble(ds.Tables[0].Rows[0]["JobAmount"]);
                        JobPost.JobAmountFlag = Convert.ToInt32(ds.Tables[0].Rows[0]["JobAmountFlag"]);
                        JobPost.PaymentTime = Convert.ToString(ds.Tables[0].Rows[0]["PaymentTime"]);
                        JobPost.PaymentId = Convert.ToString(ds.Tables[0].Rows[0]["PaymentId"]);
                        JobPost.PaymentStatus = Convert.ToString(ds.Tables[0].Rows[0]["PaymentStatus"]);
                        JobPost.PaymentResponse = Convert.ToString(ds.Tables[0].Rows[0]["PaymentResponse"]);
                        JobPost.CreatedOn = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPost.EndDate = string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");
                        JobPost.JobPostTimeDiff = Convert.ToString(ds.Tables[0].Rows[0]["JobPostTimeDiff"]);

                        JobPost.IsHire = Convert.ToInt32(ds.Tables[0].Rows[0]["IsHire"]);
                        JobPost.BestOffer = Convert.ToInt32(ds.Tables[0].Rows[0]["BestOffer"]);

                        #region Job Post Offer
                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                JobPostOffer.Add(new JobPostOffer()
                                {
                                    JobPostOfferId = Convert.ToInt32(ds.Tables[1].Rows[i]["JobPostOfferId"]),
                                    JobPostId = Convert.ToInt32(ds.Tables[1].Rows[i]["JobPostId"]),
                                    ClientId = Convert.ToInt32(ds.Tables[1].Rows[i]["ClientId"]),
                                    FirstName = Convert.ToString(ds.Tables[1].Rows[i]["FirstName"]),
                                    LastName = Convert.ToString(ds.Tables[1].Rows[i]["LastName"]),
                                    OfferAmount = Convert.ToDouble(ds.Tables[1].Rows[i]["OfferAmount"]),
                                    IsHire = Convert.ToInt32(ds.Tables[1].Rows[i]["IsHire"]),
                                    IsMyOffer = 0,
                                    Latitude = Convert.ToDouble(ds.Tables[1].Rows[i]["Latitude"]),
                                    Longitude = Convert.ToDouble(ds.Tables[1].Rows[i]["Longitude"]),
                                    Altitude = Convert.ToDouble(ds.Tables[1].Rows[i]["Altitude"]),
                                    CreatedOn = Convert.ToDateTime(ds.Tables[1].Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                                    EndDate = string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(ds.Tables[1].Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss"),
                                    JobPostTimeDiff = Convert.ToString(ds.Tables[1].Rows[i]["JobPostTimeDiff"])
                                });
                            }
                        }
                        JobPost.JobPostOffer = JobPostOffer;
                        #endregion

                        #region Job Post View
                        if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                            {
                                JobPostView.Add(new JobPostView()
                                {
                                    JobPostViewId = Convert.ToInt32(ds.Tables[2].Rows[i]["JobPostViewId"]),
                                    JobPostId = Convert.ToInt32(ds.Tables[2].Rows[i]["JobPostId"]),
                                    ClientId = Convert.ToInt32(ds.Tables[2].Rows[i]["ClientId"]),
                                    FirstName = Convert.ToString(ds.Tables[2].Rows[i]["FirstName"]),
                                    LastName = Convert.ToString(ds.Tables[2].Rows[i]["LastName"]),
                                    CreatedOn = string.IsNullOrEmpty(Convert.ToString(ds.Tables[2].Rows[i]["CreatedOn"])) == true ? string.Empty : Convert.ToDateTime(ds.Tables[2].Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss")
                                });
                            }
                        }
                        JobPost.JobPostView = JobPostView;
                        #endregion

                        DataConfirmation.DataId = Convert.ToInt32(ds.Tables[0].Rows[0]["JobPostId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPost;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetJobPostDetail");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetJobPostDetail");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Get JobPost MyOffers
        #region Get JobPost MyOffers
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetJobPost_MyOffers(string AuthKey, int ClientId, int PageNumber, int Records)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPost = new List<JobPost>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    string sWebSiteImagePath = "http://helpme.devs-vis.com/Images/";
                    string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/jobpic.png";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspJobPostMyOffersPaging] @ClientId = " + ClientId.ToString() + ", @PageNumber = " + PageNumber.ToString() + ", @Records = " + Records.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            blnDataFound = true;
                            JobPost.Add(new JobPost()
                            {
                                DataId = Convert.ToInt32(dt.Rows[i]["JobPostId"]),
                                JobPostId = Convert.ToInt32(dt.Rows[i]["JobPostId"]),
                                ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                                FirstName = Convert.ToString(dt.Rows[i]["FirstName"]),
                                LastName = Convert.ToString(dt.Rows[i]["LastName"]),
                                JobTitle = Convert.ToString(dt.Rows[i]["JobTitle"]),
                                JobDescription = Convert.ToString(dt.Rows[i]["JobDescription"]),

                                JobPhoto = !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto"])) ? Convert.ToString(dt.Rows[i]["JobPhoto"]) : sDefaultPicsPath,
                                JobPhoto1 = !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto1"])) ? Convert.ToString(dt.Rows[i]["JobPhoto1"]) : string.Empty,
                                JobPhoto2 = !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto2"])) ? Convert.ToString(dt.Rows[i]["JobPhoto2"]) : string.Empty,
                                JobPhoto3 = !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto3"])) ? Convert.ToString(dt.Rows[i]["JobPhoto3"]) : string.Empty,
                                IsFree = Convert.ToInt32(dt.Rows[i]["IsFree"]),

                                CategoryId = Convert.ToInt32(dt.Rows[i]["CategoryId"]),
                                CategoryName = Convert.ToString(dt.Rows[i]["CategoryName"]),
                                CategoryIcon1 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["CategoryIcon1"])) == true ? string.Empty : sWebSiteImagePath + Convert.ToString(dt.Rows[i]["CategoryIcon1"]),
                                CategoryIcon2 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["CategoryIcon2"])) == true ? string.Empty : sWebSiteImagePath + Convert.ToString(dt.Rows[i]["CategoryIcon2"]),
                                CategoryColorCode = Convert.ToString(dt.Rows[i]["CategoryColorCode"]),

                                JobPostingPoints = Convert.ToInt32(dt.Rows[i]["JobPostingPoints"]),
                                JobPostingAmount = Convert.ToDouble(dt.Rows[i]["JobPostingAmount"]),
                                Latitude = Convert.ToDouble(dt.Rows[i]["Latitude"]),
                                Longitude = Convert.ToDouble(dt.Rows[i]["Longitude"]),
                                Altitude = Convert.ToDouble(dt.Rows[i]["Altitude"]),
                                Latitude_1 = Convert.ToDouble(dt.Rows[i]["Latitude_1"]),
                                Longitude_1 = Convert.ToDouble(dt.Rows[i]["Longitude_1"]),
                                Altitude_1 = Convert.ToDouble(dt.Rows[i]["Altitude_1"]),
                                JobTimeFlag = Convert.ToInt32(dt.Rows[i]["JobTimeFlag"]),
                                JobHour = Convert.ToInt32(dt.Rows[i]["JobHour"]),
                                JobDoneTime = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobDoneTime"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["JobDoneTime"]).ToString("MM/dd/yyyy hh:mm:ss"),
                                JobAmount = Convert.ToDouble(dt.Rows[i]["JobAmount"]),
                                JobAmountFlag = Convert.ToInt32(dt.Rows[i]["JobAmountFlag"]),
                                PaymentTime = Convert.ToString(dt.Rows[i]["PaymentTime"]),
                                PaymentId = Convert.ToString(dt.Rows[i]["PaymentId"]),
                                PaymentStatus = Convert.ToString(dt.Rows[i]["PaymentStatus"]),
                                PaymentResponse = Convert.ToString(dt.Rows[i]["PaymentResponse"]),
                                CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                                EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss"),

                                MyOfferAmount = Convert.ToDouble(dt.Rows[i]["MyOfferAmount"]),
                                IsHire = Convert.ToInt32(dt.Rows[i]["IsHire"]),

                                RowNo = Convert.ToInt32(dt.Rows[i]["RowNo"]),
                                TotalRowNo = Convert.ToInt32(dt.Rows[i]["TotalRowNo"]),
                                TotalPage = Convert.ToInt32(dt.Rows[i]["TotalPage"]),
                                BestOffer = Convert.ToInt32(dt.Rows[i]["BestOffer"]),
                                JobPostTimeDiff = Convert.ToString(dt.Rows[i]["JobPostTimeDiff"])
                            });
                        }
                    }

                    if (blnDataFound)
                    {
                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["JobPostId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPost;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetJobPost_MyOffers");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetJobPost_MyOffers");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Get JobPost Offers
        #region Get JobPost Offers
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetJobPost_AllPostOffers(string AuthKey, int JobPostId, int ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPostOffer = new List<JobPostOffer>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice" + "/DefaultPics/profilepic.png";

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspJobPostOffer] @JobPostId = " + JobPostId.ToString() + ", @ClientId = " + ClientId.ToString() + ", @OpType = 'S'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            blnDataFound = true;
                            JobPostOffer.Add(new JobPostOffer()
                            {
                                DataId = Convert.ToInt32(dt.Rows[i]["JobPostOfferId"]),
                                JobPostOfferId = Convert.ToInt32(dt.Rows[i]["JobPostOfferId"]),
                                JobPostId = Convert.ToInt32(dt.Rows[i]["JobPostId"]),
                                ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                                FirstName = Convert.ToString(dt.Rows[i]["FirstName"]),
                                LastName = Convert.ToString(dt.Rows[i]["LastName"]),
                                ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[i]["ProfilePic"]),
                                OfferAmount = Convert.ToDouble(dt.Rows[i]["OfferAmount"]),
                                IsHire = Convert.ToInt32(dt.Rows[i]["IsHire"]),
                                IsMyOffer = Convert.ToInt32(dt.Rows[i]["IsMyOffer"]),
                                Latitude = Convert.ToDouble(dt.Rows[i]["Latitude"]),
                                Longitude = Convert.ToDouble(dt.Rows[i]["Longitude"]),
                                Altitude = Convert.ToDouble(dt.Rows[i]["Altitude"]),
                                CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                                EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss"),
                                JobPostTimeDiff = Convert.ToString(dt.Rows[i]["JobPostTimeDiff"]),
                                JobAmountFlag = Convert.ToInt32(dt.Rows[i]["JobAmountFlag"]),
                                JobHour = Convert.ToInt32(dt.Rows[i]["JobHour"])
                            });
                        }
                    }

                    if (blnDataFound)
                    {
                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["JobPostOfferId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPostOffer;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetJobPost_AllPostOffers");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetJobPost_AllPostOffers");
                Context.Response.Write(data);
            }
        }
        #endregion

        //ChatGroup Insert
        #region ChatGroup Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ChatGroupInsert(string AuthKey, int ChatGroupId, string ChatGroupName, string ChatGroupPic)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var ChatGroup = new ChatGroup();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = string.Empty;
                    string sChatGroupPicPath = "http://helpme.devs-vis.com/webservice/ChatGroupPics/";
                    if (ChatGroupId == 0)
                        sQry = "Exec [dbo].[uspChatGroup] @ChatGroupId = " + ChatGroupId + ", @ChatGroupName = '" + ChatGroupName + "', @ChatGroupPic = '" + sChatGroupPicPath + "', @OpType = 'I'";
                    else
                        sQry = "Exec [dbo].[uspChatGroup] @ChatGroupId = " + ChatGroupId + ", @ChatGroupName = '" + ChatGroupName + "', @ChatGroupPic = '" + sChatGroupPicPath + "', @OpType = 'U'";

                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/ChatGroupPic.png";

                        bool bln = false;
                        string sError = string.Empty;
                        if (!string.IsNullOrEmpty(ChatGroupPic))
                        {
                            byte[] TextAsBytes = System.Convert.FromBase64String(ChatGroupPic);
                            sChatGroupPicPath = Server.MapPath(".") + "/ChatGroupPics/" + Convert.ToInt32(dt.Rows[0]["ChatGroupId"]) + ".png";
                            bln = ConvertBinaryToFile(sChatGroupPicPath, TextAsBytes, ref sError);
                            if (!bln)
                            {
                                objDBHelper.ExecuteNonQuery("Update tblChatGroup Set ChatGroupPic = '' Where ChatGroupId = " + Convert.ToString(dt.Rows[0]["ChatGroupId"]));
                            }
                        }

                        blnDataFound = true;
                        ChatGroup.DataId = Convert.ToInt32(dt.Rows[0]["ChatGroupId"]);
                        ChatGroup.ChatGroupId = Convert.ToInt64(dt.Rows[0]["ChatGroupId"]);
                        ChatGroup.ChatGroupName = Convert.ToString(dt.Rows[0]["ChatGroupName"]);
                        ChatGroup.ChatGroupPic = (bln == true) ? Convert.ToString(dt.Rows[0]["ChatGroupPic"]) : sDefaultPicsPath;
                        ChatGroup.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        ChatGroup.EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ChatGroupId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = ChatGroup;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ChatGroupInsert");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ChatGroupInsert");
                Context.Response.Write(data);
            }
        }
        #endregion

        //ChatGroupMember Insert
        #region ChatGroupMember Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ChatGroupMemberInsert(string AuthKey, int ChatGroupId, string ClientId, int AdminClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var ChatGroupMember = new List<ChatGroupMember>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspChatGroupMember] @ChatGroupMemberId = 0, @ChatGroupId = " + ChatGroupId + ", @ClientId = '" + ClientId + "', @AdminClientId = " + AdminClientId + ", @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        string sAdminClientName = Convert.ToString(objDBHelper.ExecuteScalar("Select Isnull((Select FirstName + ' ' + LastName From tblClient Where ClientId = " + AdminClientId + "), '')"));
                        string sRemarks = sAdminClientName + " invited you to join a Group Chat";

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (AdminClientId != Convert.ToInt64(dt.Rows[i]["ClientId"]))
                                InsertNotifications(AuthKey, Convert.ToInt64(dt.Rows[i]["ClientId"]), "Group Chat", sRemarks, string.Empty, 4, Convert.ToString(dt.Rows[i]["ChatGroupMemberId"]));

                            blnDataFound = true;
                            ChatGroupMember.Add(new ChatGroupMember()
                            {
                                DataId = Convert.ToInt32(dt.Rows[i]["ChatGroupMemberId"]),
                                ChatGroupMemberId = Convert.ToInt32(dt.Rows[i]["ChatGroupMemberId"]),
                                ChatGroupId = Convert.ToInt32(dt.Rows[i]["ChatGroupId"]),
                                ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                                FirstName = Convert.ToString(dt.Rows[i]["FirstName"]),
                                LastName = Convert.ToString(dt.Rows[i]["LastName"]),
                                IsAdmin = Convert.ToInt32(dt.Rows[i]["IsAdmin"]),
                                CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                                EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss")
                            });
                        }

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ChatGroupMemberId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = ChatGroupMember;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ChatGroupMemberInsert");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ChatGroupMemberInsert");
                Context.Response.Write(data);
            }
        }
        #endregion

        //ChatGroupMember Leave
        #region ChatGroupMember Leave
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ChatGroupMemberLeave(string AuthKey, int ChatGroupId, int ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var DataConfirmation = new DataConfirmation();
            //var ChatGroupMember = new List<ChatGroupMember>();
            //var ChatUser = new List<ChatUser>();
            //string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/ChatGroupPic.png";
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspChatGroupMember] @ChatGroupMemberId = 0, @ChatGroupId = " + ChatGroupId + ", @ChatClientId = " + ClientId + ", @OpType = 'LeaveGroup'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    ChatUser.Add(new ChatUser()
                        //    {
                        //        DataId = Convert.ToInt32(dt.Rows[i]["Id"]),
                        //        IsGroup = Convert.ToInt32(dt.Rows[i]["IsGroup"]),
                        //        Id = Convert.ToInt32(dt.Rows[i]["Id"]),
                        //        Name = Convert.ToString(dt.Rows[i]["Name"]),
                        //        ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProfilePic"])) == true ? sDefaultPicsPath : Convert.ToString(dt.Rows[i]["ProfilePic"]),
                        //        IsAdmin = Convert.ToInt32(dt.Rows[i]["IsAdmin"])
                        //    });
                        //}
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ChatGroupMemberLeave");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ChatGroupMemberLeave");
                Context.Response.Write(data);
            }
        }
        #endregion

        //ChatGroupMember Delete
        #region ChatGroupMember Delete
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ChatGroupMemberDelete(string AuthKey, int ChatGroupId, int ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var DataConfirmation = new DataConfirmation();
            //var ChatGroupMember = new List<ChatGroupMember>();
            //var ChatUser = new List<ChatUser>();
            //string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/ChatGroupPic.png";
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspChatGroupMember] @ChatGroupMemberId = 0, @ChatGroupId = " + ChatGroupId + ", @ChatClientId = " + ClientId + ", @OpType = 'DeleteGroup'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    ChatUser.Add(new ChatUser()
                        //    {
                        //        DataId = Convert.ToInt32(dt.Rows[i]["Id"]),
                        //        IsGroup = Convert.ToInt32(dt.Rows[i]["IsGroup"]),
                        //        Id = Convert.ToInt32(dt.Rows[i]["Id"]),
                        //        Name = Convert.ToString(dt.Rows[i]["Name"]),
                        //        ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProfilePic"])) == true ? sDefaultPicsPath : Convert.ToString(dt.Rows[i]["ProfilePic"]),
                        //        IsAdmin = Convert.ToInt32(dt.Rows[i]["IsAdmin"])
                        //    });
                        //}
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ChatGroupMemberDelete");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ChatGroupMemberDelete");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Country Function
        #region Get Country
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCountry(string AuthKey)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Country = new List<Country>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspCountry] @OpType = 'Get'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        Country.Add(new Country()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["CountryId"]),
                            CountryId = Convert.ToInt32(dt.Rows[i]["CountryId"]),
                            CountryName = Convert.ToString(dt.Rows[i]["CountryName"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = Country;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetCountry");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetCountry");
                Context.Response.Write(data);
            }
        }
        #endregion

        //State Function
        #region Get State
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetState(string AuthKey, Int64 CountryId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var State = new List<State>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspState] @OpType = 'Get', @CountryId = " + CountryId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        State.Add(new State()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["StateId"]),
                            StateId = Convert.ToInt32(dt.Rows[i]["StateId"]),
                            StateName = Convert.ToString(dt.Rows[i]["StateName"]),
                            CountryId = Convert.ToInt32(dt.Rows[i]["CountryId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = State;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetState");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetState");
                Context.Response.Write(data);
            }
        }
        #endregion

        //City Function
        #region Get City
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCity(string AuthKey, Int64 StateId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var City = new List<City>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspCity] @OpType = 'Get', @StateId = " + StateId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        City.Add(new City()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["CityId"]),
                            CityId = Convert.ToInt32(dt.Rows[i]["CityId"]),
                            CityName = Convert.ToString(dt.Rows[i]["CityName"]),
                            StateId = Convert.ToInt32(dt.Rows[i]["StateId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = City;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetCity");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE Statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetCity");
                Context.Response.Write(data);
            }
        }
        #endregion

        //ChatUser Insert
        #region ChatUser Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ChatUserInsert(string AuthKey, int ClientId, int ToClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var ChatUserData = new ChatUserData();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspChatUser] @ClientId = " + ClientId + ", @ToClientId = " + ToClientId + ", @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        string sRemarks = Convert.ToString(dt.Rows[0]["FromClientName"]) + " invited you to join a Chat";
                        InsertNotifications(AuthKey, Convert.ToInt64(dt.Rows[0]["ToClientId"]), "User Chat", sRemarks, string.Empty, 3, Convert.ToString(dt.Rows[0]["ChatUserId"]));

                        ChatUserData.DataId = Convert.ToInt32(dt.Rows[0]["ChatUserId"]);
                        ChatUserData.ChatUserId = Convert.ToInt32(dt.Rows[0]["ChatUserId"]);
                        ChatUserData.FromClientId = Convert.ToInt32(dt.Rows[0]["FromClientId"]);
                        ChatUserData.FromClientName = Convert.ToString(dt.Rows[0]["FromClientName"]);
                        ChatUserData.ToClientId = Convert.ToInt32(dt.Rows[0]["ToClientId"]);
                        ChatUserData.ToClientName = Convert.ToString(dt.Rows[0]["ToClientName"]);
                        ChatUserData.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        ChatUserData.EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ChatUserId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = ChatUserData;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ChatUserInsert");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ChatUserInsert");
                Context.Response.Write(data);
            }
        }
        #endregion

        //ChatUser Delete
        #region ChatUser Delete
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ChatUserDelete(string AuthKey, int ClientId, int ToClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            //var ChatUser = new List<ChatUser>();
            var DataConfirmation = new DataConfirmation();
            //string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/ChatGroupPic.png";
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspChatUser] @ClientId = " + ClientId + ", @ToClientId = " + ToClientId + ", @OpType = 'D'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        //ChatUser.Add(new ChatUser()
                        //{
                        //    DataId = Convert.ToInt32(dt.Rows[i]["Id"]),
                        //    IsGroup = Convert.ToInt32(dt.Rows[i]["IsGroup"]),
                        //    Id = Convert.ToInt32(dt.Rows[i]["Id"]),
                        //    Name = Convert.ToString(dt.Rows[i]["Name"]),
                        //    ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProfilePic"])) == true ? sDefaultPicsPath : Convert.ToString(dt.Rows[i]["ProfilePic"]),
                        //    IsAdmin = Convert.ToInt32(dt.Rows[i]["IsAdmin"])
                        //});
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ChatUserDelete");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ChatUserDelete");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Chat User Function
        #region Get Chat User
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetChatUser(string AuthKey, Int64 ClientId, int Flag)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var ChatUser = new List<ChatUser>();
            var DataConfirmation = new DataConfirmation();
            string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/ChatGroupPic.png";
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = null;
                    if (Flag == 0)
                        dt = objDBHelper.FillTable("Exec [uspChatUser] @OpType = 'Get0', @ClientId = " + ClientId.ToString());
                    else
                        dt = objDBHelper.FillTable("Exec [uspChatUser] @OpType = 'Get1', @ClientId = " + ClientId.ToString());

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        ChatUser.Add(new ChatUser()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["Id"]),
                            IsGroup = Convert.ToInt32(dt.Rows[i]["IsGroup"]),
                            Id = Convert.ToInt32(dt.Rows[i]["Id"]),
                            Name = Convert.ToString(dt.Rows[i]["Name"]),
                            ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProfilePic"])) == true ? sDefaultPicsPath : Convert.ToString(dt.Rows[i]["ProfilePic"]),
                            IsAdmin = Convert.ToInt32(dt.Rows[i]["IsAdmin"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = ChatUser;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetChatUser");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE Statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetChatUser");
                Context.Response.Write(data);
            }
        }
        #endregion

        //ChatGroupMember Function
        #region Get ChatGroupMember
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetChatGroupMember(string AuthKey, Int64 ChatGroupId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var ChatGroupMember = new List<ChatGroupMember>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspChatGroupMember] @OpType = 'S', @ChatGroupId = " + ChatGroupId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        ChatGroupMember.Add(new ChatGroupMember()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ChatGroupMemberId"]),
                            ChatGroupMemberId = Convert.ToInt32(dt.Rows[i]["ChatGroupMemberId"]),
                            ChatGroupId = Convert.ToInt32(dt.Rows[i]["ChatGroupId"]),
                            ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                            FirstName = Convert.ToString(dt.Rows[i]["FirstName"]),
                            LastName = Convert.ToString(dt.Rows[i]["LastName"]),
                            IsAdmin = Convert.ToInt32(dt.Rows[i]["IsAdmin"]),
                            CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                            EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss")
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = ChatGroupMember;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetChatGroupMember");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE Statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetChatGroupMember");
                Context.Response.Write(data);
            }
        }
        #endregion

        //JobPostDecline Insert
        #region JobPostDecline Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JobPostDeclineInsert(string AuthKey, int JobPostId, string ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var JobPostDecline = new JobPostDecline();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = @"Declare @JobPostDeclineId As BigInt = 0
                                    Insert Into tblJobPostDecline (JobPostId, ClientId, CreatedOn) Values (" + JobPostId + ", " + ClientId + @", GetDate())
                                    Set @JobPostDeclineId = Scope_Identity()
                                    Select @JobPostDeclineId As JobPostDeclineId";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        JobPostDecline.DataId = Convert.ToInt32(dt.Rows[0]["JobPostDeclineId"]);
                        JobPostDecline.JobPostDeclineId = Convert.ToInt64(dt.Rows[0]["JobPostDeclineId"]);
                        JobPostDecline.JobPostId = JobPostId;
                        JobPostDecline.ClientId = Convert.ToInt64(ClientId);

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["JobPostDeclineId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = JobPostDecline;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "JobPostDeclineInsert");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "JobPostDeclineInsert");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Package Function
        #region Get Package
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPackage(string AuthKey)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Package = new List<Package>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspPackage] @OpType = 'Get'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        Package.Add(new Package()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["PackageId"]),
                            PackageId = Convert.ToInt32(dt.Rows[i]["PackageId"]),
                            PackageName = Convert.ToString(dt.Rows[i]["PackageName"]),
                            Description = Convert.ToString(dt.Rows[i]["Description"]),
                            CreditPost = Convert.ToInt32(dt.Rows[i]["CreditPost"]),
                            CreditPoint = Convert.ToInt32(dt.Rows[i]["CreditPoint"]),
                            Amount = Convert.ToDouble(dt.Rows[i]["Amount"]),
                            CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                            EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss")
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = Package;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetPackage");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetPackage");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Subscription Insert
        #region Subscription Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SubscriptionInsert(string AuthKey, int ClientId, int PackageId, int CreditPost, int CreditPoint, double PaymentAmount, string PaymentTime, string PaymentId, string PaymentStatus, string PaymentResponse)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Subscription = new Subscription();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspSubscription] @ClientId = " + ClientId + ", @PackageId = " + PackageId + ", @CreditPost = " + CreditPost + ", @CreditPoint = " + CreditPoint + ", @PaymentAmount = " + PaymentAmount + ", @PaymentTime = '" + PaymentTime + "', @PaymentId = '" + PaymentId + "', @PaymentStatus = '" + PaymentStatus + "', @PaymentResponse = '" + PaymentResponse + "', @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        Subscription.DataId = Convert.ToInt32(dt.Rows[0]["SubscriptionId"]);
                        Subscription.SubscriptionId = Convert.ToInt64(dt.Rows[0]["SubscriptionId"]);
                        Subscription.ClientId = Convert.ToInt64(dt.Rows[0]["ClientId"]);
                        Subscription.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        Subscription.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        Subscription.PackageId = Convert.ToInt64(dt.Rows[0]["PackageId"]);
                        Subscription.CreditPost = Convert.ToInt32(dt.Rows[0]["CreditPost"]);
                        Subscription.CreditPoint = Convert.ToInt32(dt.Rows[0]["CreditPoint"]);
                        Subscription.PackageName = Convert.ToString(dt.Rows[0]["PackageName"]);
                        Subscription.PaymentAmount = Convert.ToString(dt.Rows[0]["PaymentAmount"]);
                        Subscription.PaymentTime = Convert.ToString(dt.Rows[0]["PaymentTime"]);
                        Subscription.PaymentId = Convert.ToString(dt.Rows[0]["PaymentId"]);
                        Subscription.PaymentStatus = Convert.ToString(dt.Rows[0]["PaymentStatus"]);
                        Subscription.PaymentResponse = Convert.ToString(dt.Rows[0]["PaymentResponse"]);
                        Subscription.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        Subscription.EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["SubscriptionId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = Subscription;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "SubscriptionInsert");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "SubscriptionInsert");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Subscription Function
        #region Get Subscription
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetSubscription(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Subscription = new List<Subscription>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspSubscription] @OpType = 'GetSubscription', @ClientId = " + ClientId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        Subscription.Add(new Subscription()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["SubscriptionId"]),
                            SubscriptionId = Convert.ToInt64(dt.Rows[i]["SubscriptionId"]),
                            ClientId = Convert.ToInt64(dt.Rows[i]["ClientId"]),
                            FirstName = Convert.ToString(dt.Rows[i]["FirstName"]),
                            LastName = Convert.ToString(dt.Rows[i]["LastName"]),
                            PackageId = Convert.ToInt64(dt.Rows[i]["PackageId"]),
                            CreditPost = Convert.ToInt32(dt.Rows[i]["CreditPost"]),
                            CreditPoint = Convert.ToInt32(dt.Rows[i]["CreditPoint"]),
                            PackageName = Convert.ToString(dt.Rows[i]["PackageName"]),
                            PaymentAmount = Convert.ToString(dt.Rows[i]["PaymentAmount"]),
                            PaymentTime = Convert.ToString(dt.Rows[i]["PaymentTime"]),
                            PaymentId = Convert.ToString(dt.Rows[i]["PaymentId"]),
                            PaymentStatus = Convert.ToString(dt.Rows[i]["PaymentStatus"]),
                            PaymentResponse = Convert.ToString(dt.Rows[i]["PaymentResponse"]),
                            CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                            EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss")
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = Subscription;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetSubscription");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE Statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetSubscription");
                Context.Response.Write(data);
            }
        }
        #endregion

        //ProductRedeem Insert
        #region ProductRedeem Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ProductRedeemInsert(string AuthKey, int ClientId, int ProductId, int RedeemPoint)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var ProductRedeem = new ProductRedeem();
            var DataConfirmation = new DataConfirmation();
            string sWebSiteImagePath = "http://helpme.devs-vis.com/Images/Product/";
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspProductRedeem] @ClientId = " + ClientId + ", @ProductId = " + ProductId + ", @RedeemPoint = " + RedeemPoint + ", @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        ProductRedeem.DataId = Convert.ToInt32(dt.Rows[0]["ProductRedeemId"]);
                        ProductRedeem.ProductRedeemId = Convert.ToInt64(dt.Rows[0]["ProductRedeemId"]);
                        ProductRedeem.ClientId = Convert.ToInt64(dt.Rows[0]["ClientId"]);
                        ProductRedeem.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        ProductRedeem.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        ProductRedeem.ProductId = Convert.ToInt64(dt.Rows[0]["ProductId"]);
                        ProductRedeem.ProductName = Convert.ToString(dt.Rows[0]["ProductName"]);
                        ProductRedeem.RedeemPoint = Convert.ToInt32(dt.Rows[0]["RedeemPoint"]);
                        ProductRedeem.ProductImage = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProductImage"])) == true ? string.Empty : (sWebSiteImagePath + Convert.ToString(dt.Rows[0]["ProductImage"]));
                        ProductRedeem.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        ProductRedeem.EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ProductRedeemId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = ProductRedeem;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ProductRedeemInsert");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ProductRedeemInsert");
                Context.Response.Write(data);
            }
        }
        #endregion

        //ProductRedeem Function
        #region Get ProductRedeem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetProductRedeem(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var ProductRedeem = new List<ProductRedeem>();
            var DataConfirmation = new DataConfirmation();
            string sWebSiteImagePath = "http://helpme.devs-vis.com/Images/Product/";
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspProductRedeem] @OpType = 'GetProductRedeem', @ClientId = " + ClientId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        ProductRedeem.Add(new ProductRedeem()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ProductRedeemId"]),
                            ProductRedeemId = Convert.ToInt64(dt.Rows[i]["ProductRedeemId"]),
                            ClientId = Convert.ToInt64(dt.Rows[i]["ClientId"]),
                            FirstName = Convert.ToString(dt.Rows[i]["FirstName"]),
                            LastName = Convert.ToString(dt.Rows[i]["LastName"]),
                            ProductId = Convert.ToInt64(dt.Rows[i]["ProductId"]),
                            ProductName = Convert.ToString(dt.Rows[i]["ProductName"]),
                            ProductImage = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProductImage"])) == true ? string.Empty : (sWebSiteImagePath + Convert.ToString(dt.Rows[i]["ProductImage"])),
                            RedeemPoint = Convert.ToInt32(dt.Rows[i]["RedeemPoint"]),
                            CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                            EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss")
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = ProductRedeem;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetProductRedeem");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE Statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetProductRedeem");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Review Insert
        #region Review Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ReviewInsert(string AuthKey, int ClientId, int JobPostId, double Rating, string ReviewData)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Review = new Review();
            var DataConfirmation = new DataConfirmation();
            string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/jobpic.png";
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspReview] @ClientId = " + ClientId + ", @JobPostId = " + JobPostId + ", @Rating = " + Rating + ", @Review = '" + ReviewData + "', @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        Review.DataId = Convert.ToInt32(dt.Rows[0]["ReviewId"]);
                        Review.ReviewId = Convert.ToInt64(dt.Rows[0]["ReviewId"]);
                        Review.ClientId = Convert.ToInt64(dt.Rows[0]["ClientId"]);
                        Review.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                        Review.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                        Review.JobPostId = Convert.ToInt64(dt.Rows[0]["JobPostId"]);
                        Review.Rating = Convert.ToInt32(dt.Rows[0]["Rating"]);
                        Review.ReviewData = Convert.ToString(dt.Rows[0]["Review"]);
                        Review.JobPhoto = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["JobPhoto"])) == true ? sDefaultPicsPath : Convert.ToString(dt.Rows[0]["JobPhoto"]);
                        Review.JobPhoto1 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["JobPhoto1"])) == true ? string.Empty : Convert.ToString(dt.Rows[0]["JobPhoto1"]);
                        Review.JobPhoto2 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["JobPhoto2"])) == true ? string.Empty : Convert.ToString(dt.Rows[0]["JobPhoto2"]);
                        Review.JobPhoto3 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["JobPhoto3"])) == true ? string.Empty : Convert.ToString(dt.Rows[0]["JobPhoto3"]);
                        Review.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        Review.EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ReviewId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = Review;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ReviewInsert");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ReviewInsert");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Review Function
        #region Get Review
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetReview(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Review = new List<Review>();
            var DataConfirmation = new DataConfirmation();
            string sDefaultClientPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/profilepic.png";
            string sDefaultPicsPath = "http://helpme.devs-vis.com/webservice/DefaultPics/jobpic.png";
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspReview] @OpType = 'GetReview', @ClientId = " + ClientId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        Review.Add(new Review()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ReviewId"]),
                            ReviewId = Convert.ToInt64(dt.Rows[i]["ReviewId"]),
                            ClientId = Convert.ToInt64(dt.Rows[i]["ClientId"]),
                            FirstName = Convert.ToString(dt.Rows[i]["FirstName"]),
                            LastName = Convert.ToString(dt.Rows[i]["LastName"]),
                            JobPostId = Convert.ToInt64(dt.Rows[i]["JobPostId"]),
                            Rating = Convert.ToInt32(dt.Rows[i]["Rating"]),
                            ReviewData = Convert.ToString(dt.Rows[i]["Review"]),
                            JobPhoto = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto"])) == true ? sDefaultPicsPath : Convert.ToString(dt.Rows[i]["JobPhoto"]),
                            JobPhoto1 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto1"])) == true ? string.Empty : Convert.ToString(dt.Rows[i]["JobPhoto1"]),
                            JobPhoto2 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto2"])) == true ? string.Empty : Convert.ToString(dt.Rows[i]["JobPhoto2"]),
                            JobPhoto3 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["JobPhoto3"])) == true ? string.Empty : Convert.ToString(dt.Rows[i]["JobPhoto3"]),
                            CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                            EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss"),

                            JobTitle = Convert.ToString(dt.Rows[i]["JobTitle"]),
                            OfferAmount = Convert.ToDouble(dt.Rows[i]["OfferAmount"]),
                            HelpSeeker_ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["HelpSeeker_ProfilePic"])) == true ? sDefaultClientPicsPath : Convert.ToString(dt.Rows[i]["HelpSeeker_ProfilePic"]),
                            ReviewTimeDiff = Convert.ToString(dt.Rows[i]["ReviewTimeDiff"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = Review;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetReview");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE Statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetReview");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Set Client Location
        #region Set Client Location
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SetClientLocation(string AuthKey, Int64 ClientId, double Latitude, double Longitude, double Altitude)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var IdentityLocation = new IdentityLocation();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientLocation] @ClientId = " + ClientId.ToString() + ", @Latitude = " + Latitude.ToString() + ", @Longitude = " + Longitude.ToString() + ", @Altitude = " + Altitude.ToString() + ", @OpType = 'I'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;
                        IdentityLocation.DataId = Convert.ToInt32(ClientId);
                        IdentityLocation.Id = Convert.ToInt32(ClientId);
                        IdentityLocation.Latitude = Latitude;
                        IdentityLocation.Longitude = Longitude;
                        IdentityLocation.Altitude = Altitude;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = IdentityLocation;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "SetClientLocation");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE Statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "SetClientLocation");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Get Client Location
        #region Get Client Location
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientLocation(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var IdentityLocation = new IdentityLocation();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientLocation] @ClientId = " + ClientId.ToString() + ", @OpType = 'S'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        IdentityLocation.Id = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        IdentityLocation.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        IdentityLocation.Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]);
                        IdentityLocation.Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]);
                        IdentityLocation.Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]);
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = IdentityLocation;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetClientLocation");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE Statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetClientLocation");
                Context.Response.Write(data);
            }
        }
        #endregion

        //ARView Function
        #region Get ARView
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetARView(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var ARView = new List<ARView>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string sWebSiteImagePath = "http://helpme.devs-vis.com/Images/";
                string AppIconPath = "http://helpme.devs-vis.com/webservice/DefaultPics/AppIcon.png";
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspARView] @ClientId = " + ClientId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        ARView.Add(new ARView()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                            ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                            JobPostId = Convert.ToInt32(dt.Rows[i]["JobPostId"]),
                            FirstName = Convert.ToString(dt.Rows[i]["FirstName"]),
                            LastName = Convert.ToString(dt.Rows[i]["LastName"]),
                            Latitude = Convert.ToDouble(dt.Rows[i]["Latitude"]),
                            Longitude = Convert.ToDouble(dt.Rows[i]["Longitude"]),
                            Altitude = Convert.ToDouble(dt.Rows[i]["Altitude"]),
                            Rating = Convert.ToDouble(dt.Rows[i]["Rating"]),
                            CategoryId = Convert.ToInt32(dt.Rows[i]["CategoryId"]),
                            CategoryName = Convert.ToString(dt.Rows[i]["CategoryName"]),
                            CategoryIcon1 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["CategoryIcon1"])) == true ? AppIconPath : sWebSiteImagePath + Convert.ToString(dt.Rows[i]["CategoryIcon1"]),
                            CategoryIcon2 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["CategoryIcon2"])) == true ? AppIconPath : sWebSiteImagePath + Convert.ToString(dt.Rows[i]["CategoryIcon2"]),
                            CategoryColorCode = Convert.ToString(dt.Rows[i]["CategoryColorCode"]),
                            Distance = Convert.ToDouble(dt.Rows[i]["Distance"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = ARView;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetARView");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE Statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetARView");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Set Client PaymentType
        #region Set Client PaymentType
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SetClientPaymentType(string AuthKey, Int64 ClientId, int PaymentType)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var ClientPaymentType = new ClientPaymentType();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    //string Stripe_ApiKey = "sk_live_laT7frDDnFvoFRMNpO5efIIn";      // "sk_test_eLa53cuPkNlgl7RwI6Z9GIiM";
                    //StripeConfiguration.SetApiKey(Stripe_ApiKey);

                    //#region Create Stripe Account
                    //var objLegalEntity = new StripeAccountLegalEntityOptions();
                    //objLegalEntity.FirstName = "John";
                    //objLegalEntity.LastName = "Deere";
                    //objLegalEntity.Type = "company";
                    //objLegalEntity.BirthDay = 14;
                    //objLegalEntity.BirthMonth = 8;
                    //objLegalEntity.BirthYear = 1983;
                    //objLegalEntity.BusinessName = "John";
                    //objLegalEntity.BusinessTaxId = "121212121";
                    //objLegalEntity.PersonalIdNumber = "121212121";
                    //objLegalEntity.AddressLine1 = string.Empty;
                    //objLegalEntity.AddressLine2 = string.Empty;

                    //var objAccountOptions = new StripeAccountCreateOptions()
                    //{
                    //    Email = "JohnDeere@gmail.com",
                    //    Type = "custom",
                    //    Country = "CA",
                    //    LegalEntity = objLegalEntity,
                    //    TosAcceptanceDate = DateTime.Now,
                    //    TosAcceptanceIp = "192.168.1.9"
                    //};
                    //var objAccountService = new StripeAccountService();
                    //StripeAccount objAccount = objAccountService.Create(objAccountOptions);

                    //var objStripeResponse_Account = new WebService.StripeResponse_Account();
                    //objStripeResponse_Account.DataId = 1;
                    //objStripeResponse_Account.id = GetReponseValue("id", objAccount.StripeResponse.ResponseJson.ToString());
                    //objStripeResponse_Account.sobject = GetReponseValue("object", objAccount.StripeResponse.ResponseJson.ToString());
                    //objStripeResponse_Account.business_name = GetReponseValue("business_name", objAccount.StripeResponse.ResponseJson.ToString());
                    //#endregion

                    //#region Create Stripe Card
                    //var objBankAccountOptions = new StripeAccountBankAccountOptions();
                    //objBankAccountOptions.AccountNumber = "000123456789";
                    //objBankAccountOptions.Country = "CA";
                    //objBankAccountOptions.Currency = "cad";
                    //objBankAccountOptions.RoutingNumber = "11000-000";

                    //var objStripeBankAccount = new StripeAccountUpdateOptions()
                    //{
                    //    ExternalBankAccount = objBankAccountOptions
                    //};
                    //var objBankAccountService = new StripeAccountService();
                    //StripeAccount objBankAccount = objBankAccountService.Update(objStripeResponse_Account.id, objStripeBankAccount);

                    //var objStripeResponse_Bank_Account = new WebService.StripeResponse_Account();
                    //objStripeResponse_Bank_Account.DataId = 1;
                    //objStripeResponse_Bank_Account.id = GetReponseValue("id", objBankAccount.StripeResponse.ResponseJson.ToString());
                    //objStripeResponse_Bank_Account.sobject = GetReponseValue("object", objBankAccount.StripeResponse.ResponseJson.ToString());
                    //objStripeResponse_Bank_Account.business_name = GetReponseValue("business_name", objBankAccount.StripeResponse.ResponseJson.ToString());
                    //#endregion

                    //#region Payment to Stripe
                    //var myCharge = new StripeChargeCreateOptions();
                    //myCharge.Amount = 100 * 100;
                    //myCharge.Currency = "cad";
                    //myCharge.Description = "Test Payment";
                    //myCharge.SourceTokenOrExistingSourceId = "tok_1B5pe6DN8wM8jx6i1rWvCUXv";
                    //myCharge.Capture = true;

                    //var chargeService = new StripeChargeService();
                    //StripeCharge stripeCharge = chargeService.Create(myCharge);

                    //var StripeResponse = new WebService.StripeResponse();
                    //StripeResponse.DataId = 1;
                    //StripeResponse.id = GetReponseValue("id", stripeCharge.StripeResponse.ResponseJson.ToString());
                    //StripeResponse.amount = GetReponseValue("amount", stripeCharge.StripeResponse.ResponseJson.ToString());
                    //StripeResponse.status = GetReponseValue("status", stripeCharge.StripeResponse.ResponseJson.ToString());
                    //StripeResponse.failure_code = GetReponseValue("failure_code", stripeCharge.StripeResponse.ResponseJson.ToString());
                    //StripeResponse.failure_message = GetReponseValue("failure_message", stripeCharge.StripeResponse.ResponseJson.ToString());
                    //#endregion

                    //#region Payout
                    //var payoutOptions = new StripeChargeCreateOptions();
                    //payoutOptions.Amount = 900;
                    //payoutOptions.Currency = "cad";
                    //payoutOptions.SourceTokenOrExistingSourceId = tok_customer;
                    //payoutOptions.Destination = objStripeResponse_Bank_Account.id;

                    //var payoutService = new StripeChargeService();
                    //var payout = payoutService.Create(payoutOptions);
                    //#endregion

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientPaymentType] @ClientId = " + ClientId.ToString() + ", @PaymentType = " + PaymentType.ToString() + ", @OpType = 'I'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;
                        ClientPaymentType.DataId = Convert.ToInt32(dt.Rows[0]["ClientPaymentTypeId"]);
                        ClientPaymentType.ClientPaymentTypeId = Convert.ToInt64(dt.Rows[0]["ClientPaymentTypeId"]);
                        ClientPaymentType.ClientId = Convert.ToInt64(dt.Rows[0]["ClientId"]);
                        ClientPaymentType.PaymentType = Convert.ToInt32(dt.Rows[0]["PaymentType"]);
                        ClientPaymentType.PaymentTypeName = Convert.ToString(dt.Rows[0]["PaymentTypeName"]);
                        ClientPaymentType.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        ClientPaymentType.EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = ClientPaymentType;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "SetClientPaymentType");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE Statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "SetClientPaymentType");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Get Client PaymentType
        #region Get Client PaymentType
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientPaymentType(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var ClientPaymentType = new List<ClientPaymentType>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientPaymentType] @ClientId = " + ClientId.ToString() + ", @OpType = 'S'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        ClientPaymentType.Add(new ClientPaymentType()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientPaymentTypeId"]),
                            ClientPaymentTypeId = Convert.ToInt64(dt.Rows[i]["ClientPaymentTypeId"]),
                            ClientId = Convert.ToInt64(dt.Rows[i]["ClientId"]),
                            PaymentType = Convert.ToInt32(dt.Rows[i]["PaymentType"]),
                            PaymentTypeName = Convert.ToString(dt.Rows[i]["PaymentTypeName"]),
                            CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                            EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss")
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = ClientPaymentType;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetClientPaymentType");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE Statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetClientPaymentType");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Set Client AppFeedback
        #region Set Client AppFeedback
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SetClientAppFeedback(string AuthKey, Int64 ClientId, string AppFeedback)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var ClientAppFeedback = new ClientAppFeedback();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientAppFeedback] @ClientId = " + ClientId.ToString() + ", @AppFeedback = '" + AppFeedback.ToString() + "', @OpType = 'I'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;
                        ClientAppFeedback.DataId = Convert.ToInt32(dt.Rows[0]["ClientAppFeedbackId"]);
                        ClientAppFeedback.ClientAppFeedbackId = Convert.ToInt64(dt.Rows[0]["ClientAppFeedbackId"]);
                        ClientAppFeedback.ClientId = Convert.ToInt64(dt.Rows[0]["ClientId"]);
                        ClientAppFeedback.AppFeedback = Convert.ToString(dt.Rows[0]["AppFeedback"]);
                        ClientAppFeedback.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        ClientAppFeedback.EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = ClientAppFeedback;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "SetClientAppFeedback");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE Statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "SetClientAppFeedback");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Get Client AppFeedback
        #region Get Client AppFeedback
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientAppFeedback(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var ClientAppFeedback = new List<ClientAppFeedback>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientAppFeedback] @ClientId = " + ClientId.ToString() + ", @OpType = 'S'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        ClientAppFeedback.Add(new ClientAppFeedback()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientAppFeedbackId"]),
                            ClientAppFeedbackId = Convert.ToInt64(dt.Rows[i]["ClientAppFeedbackId"]),
                            ClientId = Convert.ToInt64(dt.Rows[i]["ClientId"]),
                            AppFeedback = Convert.ToString(dt.Rows[i]["AppFeedback"]),
                            CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                            EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss")
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = ClientAppFeedback;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetClientAppFeedback");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE Statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetClientAppFeedback");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Pics Upload
        #region PicsUpload_Download
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
        protected bool ConvertBinaryToFile(string filePath, byte[] Pic, ref string sError)
        {
            try
            {
                if (Pic == null)
                {
                    sError = "Pic1";
                    return false;
                }
                else
                {
                    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(Pic);
                    bw.Flush();
                    fs.Flush();
                    fs.Close();
                    bw.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                sError = ex.Message;
                return false;
            }
        }
        #endregion

        //Product Function
        #region Get Product
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetProduct(string AuthKey)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var Product = new List<Product>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    string sWebSiteImagePath = "http://helpme.devs-vis.com/Images/Product/";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspProduct] @OpType = 'Get'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        Product.Add(new Product()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ProductId"]),
                            ProductId = Convert.ToInt32(dt.Rows[i]["ProductId"]),
                            ProductName = Convert.ToString(dt.Rows[i]["ProductName"]),
                            Description = Convert.ToString(dt.Rows[i]["Description"]),
                            ProductImage = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProductImage"])) == true ? string.Empty : sWebSiteImagePath + Convert.ToString(dt.Rows[i]["ProductImage"]),
                            Point = Convert.ToInt32(dt.Rows[i]["Point"]),
                            CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                            EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss")
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = Product;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetProduct");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetProduct");
                Context.Response.Write(data);
            }
        }
        #endregion

        //About Us Function
        #region Get AboutUs
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetAboutUs(string AuthKey)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var AboutUs = new AboutUs();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspAboutUs] @OpType = 'Get'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;

                        AboutUs = new AboutUs();
                        AboutUs.DataId = Convert.ToInt32(dt.Rows[i]["AboutUsId"]);
                        AboutUs.AboutUsId = Convert.ToInt32(dt.Rows[i]["AboutUsId"]);
                        AboutUs.Remarks = Convert.ToString(dt.Rows[i]["Remarks"]);
                        AboutUs.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss");
                        AboutUs.EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss");
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = AboutUs;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetAboutUs");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetAboutUs");
                Context.Response.Write(data);
            }
        }

        #endregion

        #region Set Client TokenId
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SetClientTokenId(string AuthKey, Int64 ClientId, string AcTokenId, int DeviceType, string DeviceBrand, string DeviceModel, string DeviceProduct, string DeviceSDKVersion)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var DeviceTokenId = new DeviceTokenId();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientAcToken] @ClientId = " + ClientId.ToString() + ", @AcTokenId = '" + AcTokenId.Replace("'", "''") + "', @DeviceType = " + DeviceType.ToString() + ", @DeviceBrand = '" + DeviceBrand.Replace("'", "''") + "', @DeviceModel = '" + DeviceModel.Replace("'", "''") + "', @DeviceProduct = '" + DeviceProduct.Replace("'", "''") + "', @DeviceSDKVersion = '" + DeviceSDKVersion.Replace("'", "''") + "', @OpType = 'I'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;

                        DeviceTokenId = new DeviceTokenId();
                        DeviceTokenId.DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        DeviceTokenId.Id = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        DeviceTokenId.AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]);
                        DeviceTokenId.DeviceType = Convert.ToInt32(dt.Rows[0]["DeviceType"]);
                        DeviceTokenId.DeviceBrand = Convert.ToString(dt.Rows[0]["DeviceBrand"]);
                        DeviceTokenId.DeviceModel = Convert.ToString(dt.Rows[0]["DeviceModel"]);
                        DeviceTokenId.DeviceProduct = Convert.ToString(dt.Rows[0]["DeviceProduct"]);
                        DeviceTokenId.DeviceSDKVersion = Convert.ToString(dt.Rows[0]["DeviceSDKVersion"]);
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = DeviceTokenId;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "SetClientTokenId");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "SetClientTokenId");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Client - Notifications
        #region Get Notifications Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientNotifications(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            var DataConfirmation = new DataConfirmation();
            var Notifications = new List<Notifications>();
            bool blnDataFound = false;
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Select * From vwNotificationGet Where Flag = 0 And Id = " + ClientId.ToString() + " Order By NotificationId Desc");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        Notifications.Add(new Notifications()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["NotificationId"]),
                            NotificationId = Convert.ToInt64(dt.Rows[i]["NotificationId"]),
                            Id = Convert.ToInt32(dt.Rows[i]["Id"]),
                            Name = Convert.ToString(dt.Rows[i]["Name"]),
                            AppHeading = Convert.ToString(dt.Rows[i]["AppHeading"]),
                            Title = Convert.ToString(dt.Rows[i]["Title"]),
                            Remarks = Convert.ToString(dt.Rows[i]["Remarks"]),
                            AppIconPath = Convert.ToString(dt.Rows[i]["AppIconPath"]),
                            ImagePath = Convert.ToString(dt.Rows[i]["ImagePath"]),
                            NotificationType = Convert.ToInt32(dt.Rows[i]["NotificationType"]),
                            IsSent = Convert.ToInt32(dt.Rows[i]["IsSent"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            Flag = Convert.ToInt32(dt.Rows[i]["Flag"]),
                            JobPostId = Convert.ToInt64(dt.Rows[i]["JobPostId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = Notifications;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetAboutUs");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetClientNotifications");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Insert - Notifications
        #region Insert Notifications Info
        public void InsertNotifications(string AuthKey, Int64 ClientId, string Title, string Remarks, string ImagePath, int NotificationType, string JobPostId)
        {
            try
            {
                //1	    //Job Post offer
                //2	    //Job Post offer - Modification
                //3	    //Group Chat
                //4	    //Group Chat Invitation
                //5	    //Job Posting
                //6	    //Job Offer Accept
                //7	    //Job Completion
                //8	    //Job Offer Reject
                //9	    //Job Offer Cancel
                //10    //Job Completion - with Issue

                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string AppHeading = "uHelpMe";
                    string AppIconPath = "http://helpme.devs-vis.com/webservice/DefaultPics/AppIcon.png";
                    string sQry = @"If Not Exists (Select 1 From tblNotification Where ClientId = " + ClientId.ToString() + " And NotificationType = " + NotificationType.ToString() + " And JobPostId = '" + JobPostId + "' And Remarks = '" + Remarks.Replace("'", "''") + @"')
                                    Begin
                                        Insert Into tblNotification (ClientId, AppHeading, Title, Remarks, AppIconPath, ImagePath, NotificationType, JobPostId, IsSent, CreatedOn)
                                        Values (" + (ClientId == 0 ? "Null" : ClientId.ToString()) + ", '" + AppHeading + "', '" + Title + "', '" + Remarks + "', '" + AppIconPath + @"', 
                                                '" + ImagePath + "', " + NotificationType.ToString() + ", '" + JobPostId + @"', 0, GetDate())                    
                                    End";
                    objDBHelper.ExecuteNonQuery(sQry);
                }
            }
            catch { }
        }
        #endregion

        //Insert - Stripe Payment
        #region Insert Stripe Payment
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DoStripePayment(string AuthKey, int iAmount, string sCurrency, string sDescription, string sTokenId, string sJobPostId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var StripeResponse = new StripeResponse();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    string Stripe_ApiKey = string.Empty;

                    int AppMode = Convert.ToInt32(objDBHelper.ExecuteScalar("Select Isnull((Select AppMode From tblGeneralSettings), 0)"));
                    if (AppMode == 0)
                        Stripe_ApiKey = "sk_test_eLa53cuPkNlgl7RwI6Z9GIiM";
                    else
                        Stripe_ApiKey = "sk_live_laT7frDDnFvoFRMNpO5efIIn";

                    StripeConfiguration.SetApiKey(Stripe_ApiKey);

                    ////string Stripe_ApiKey = "sk_live_laT7frDDnFvoFRMNpO5efIIn";      // "sk_test_eLa53cuPkNlgl7RwI6Z9GIiM";
                    //string Stripe_ApiKey = "sk_test_eLa53cuPkNlgl7RwI6Z9GIiM";      // "sk_live_laT7frDDnFvoFRMNpO5efIIn";
                    //StripeConfiguration.SetApiKey(Stripe_ApiKey);

                    var myCharge = new StripeChargeCreateOptions();


                    // always set these properties
                    myCharge.Amount = iAmount * 100;                          //value passes in cents
                    myCharge.Currency = sCurrency;                          //"usd"

                    // set this if you want to
                    myCharge.Description = sDescription;                    //"Charge it like it's hot";

                    myCharge.SourceTokenOrExistingSourceId = sTokenId;      //*tokenId or existingSourceId*;

                    //// set this property if using a customer - this MUST be set if you are using an existing source!
                    //myCharge.CustomerId = *customerId*;

                    //// set this if you have your own application fees (you must have your application configured first within Stripe)
                    //myCharge.ApplicationFee = 25;

                    // (not required) set this to false if you don't want to capture the charge yet - requires you call capture later
                    myCharge.Capture = true;

                    var chargeService = new StripeChargeService();
                    StripeCharge stripeCharge = chargeService.Create(myCharge);

                    blnDataFound = true;
                    StripeResponse = new WebService.StripeResponse();
                    StripeResponse.DataId = 1;
                    StripeResponse.id = GetReponseValue("id", stripeCharge.StripeResponse.ResponseJson.ToString());
                    StripeResponse.amount = GetReponseValue("amount", stripeCharge.StripeResponse.ResponseJson.ToString());
                    StripeResponse.status = GetReponseValue("status", stripeCharge.StripeResponse.ResponseJson.ToString());
                    StripeResponse.failure_code = GetReponseValue("failure_code", stripeCharge.StripeResponse.ResponseJson.ToString());
                    StripeResponse.failure_message = GetReponseValue("failure_message", stripeCharge.StripeResponse.ResponseJson.ToString());

                    if (!string.IsNullOrEmpty(sJobPostId) && sJobPostId != "0")
                    {
                        string sQry = @"Insert Into tblJobPostPayment (JobPostId, ChargeId, Amount, TransferAmount, RefundAmount, ResponseJSON) 
                                        Values (" + sJobPostId + ", '" + StripeResponse.id + "', " + StripeResponse.amount + ", 0, 0, '" + stripeCharge.StripeResponse.ResponseJson.ToString() + "')";

                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        objDBHelper.ExecuteNonQuery(sQry);
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = StripeResponse;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "DoStripePayment");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "DoStripePayment");
                Context.Response.Write(data);
            }
        }
        #endregion

        //Send An Email
        #region Send An Email
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SendEmail(string AuthKey, string sMailMessage)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var CrashReportEmail = new CrashReportEmail();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                if (Decrypt == "HelpMe")
                {
                    MailMessage Message = new MailMessage();
                    Message.From = new MailAddress("testmail@veeritsolution.com");
                    Message.To.Add("veerkrupa.hitesh@gmail.com");
                    Message.Subject = "Crash report - uHelpMe";
                    Message.IsBodyHtml = false;
                    Message.Body = sMailMessage;

                    Message.Attachments.Clear();

                    SmtpClient SmtpServer = new SmtpClient("mail.veeritsolution.com");
                    SmtpServer.Timeout = 0;
                    SmtpServer.Port = 25;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("testmail@veeritsolution.com", "6TFDw82zaR6j");
                    SmtpServer.EnableSsl = false;
                    SmtpServer.Send(Message);

                    blnDataFound = true;
                    CrashReportEmail = new WebService.CrashReportEmail();
                    CrashReportEmail.DataId = 1;
                    CrashReportEmail.Status = "Success";
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = CrashReportEmail;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "SendEmail");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "SendEmail");
                Context.Response.Write(data);
            }
        }
        #endregion

        private string GetReponseValue(string ObjectName, string Response)
        {
            try
            {
                Response = Response.Replace("\"", "").Replace("\n", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty);        //.Split(',')[0].Split(':')[0].Trim() == "id"

                string objValue = string.Empty;
                string[] arrResponse = Response.Split(',');
                foreach (string sResponse in arrResponse)
                {
                    string[] arrObj = sResponse.Split(':');
                    if (arrObj[0].Trim() == ObjectName)
                    {
                        objValue = arrObj[1].Trim();
                        break;
                    }
                }
                return objValue;
            }
            catch
            {
                return string.Empty;
            }
        }

        //ClientCategory Insert
        #region ClientCategory Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientCategoryInsert(string AuthKey, int ClientId, string CategoryId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var ClientCategory = new List<ClientCategory>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = "Exec [dbo].[uspClientCategory] @ClientCategoryId = 0, @ClientId = " + ClientId + ", @CategoryId = '" + CategoryId + "', @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        blnDataFound = true;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            blnDataFound = true;
                            ClientCategory.Add(new ClientCategory()
                            {
                                DataId = Convert.ToInt32(dt.Rows[i]["ClientCategoryId"]),
                                ClientCategoryId = Convert.ToInt32(dt.Rows[i]["ClientCategoryId"]),
                                ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                                CategoryId = Convert.ToInt32(dt.Rows[i]["CategoryId"]),
                                FirstName = Convert.ToString(dt.Rows[i]["FirstName"]),
                                LastName = Convert.ToString(dt.Rows[i]["LastName"]),
                                CategoryName = Convert.ToString(dt.Rows[i]["CategoryName"]),
                                CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                                EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss")
                            });
                        }

                        DataConfirmation.DataId = Convert.ToInt32(dt.Rows[0]["ClientCategoryId"]);
                        DataConfirmation.IsError = false;
                        DataConfirmation.ErrorNumber = 0;
                        DataConfirmation.Error = string.Empty;
                        DataConfirmation.DataConfirm_DataObject = ClientCategory;
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                    data = serializer.Serialize(DataConfirmation);
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "ClientCategoryInsert");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "ClientCategoryInsert");
                Context.Response.Write(data);
            }
        }
        #endregion

        //ClientCategory Function
        #region Get ClientCategory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientCategory(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            bool blnDataFound = false;
            var ClientCategory = new List<ClientCategory>();
            var DataConfirmation = new DataConfirmation();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "HelpMe")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientCategory] @OpType = 'S', @ClientId = " + ClientId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataFound = true;
                        ClientCategory.Add(new ClientCategory()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientCategoryId"]),
                            ClientCategoryId = Convert.ToInt32(dt.Rows[i]["ClientCategoryId"]),
                            ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                            FirstName = Convert.ToString(dt.Rows[i]["FirstName"]),
                            LastName = Convert.ToString(dt.Rows[i]["LastName"]),
                            CategoryId = Convert.ToInt32(dt.Rows[i]["CategoryId"]),
                            CategoryName = Convert.ToString(dt.Rows[i]["CategoryName"]),
                            CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]).ToString("MM/dd/yyyy hh:mm:ss"),
                            EndDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EndDate"])) == true ? string.Empty : Convert.ToDateTime(dt.Rows[i]["EndDate"]).ToString("MM/dd/yyyy hh:mm:ss")
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataFound)
                {
                    DataConfirmation.DataId = 1;
                    DataConfirmation.IsError = false;
                    DataConfirmation.ErrorNumber = 0;
                    DataConfirmation.Error = string.Empty;
                    DataConfirmation.DataConfirm_DataObject = ClientCategory;

                    data = serializer.Serialize(DataConfirmation);
                }
                else
                {
                    DataConfirmation.DataId = 0;
                    DataConfirmation.IsError = true;
                    DataConfirmation.ErrorNumber = 999;
                    DataConfirmation.Error = "No Record found.";
                    DataConfirmation.DataConfirm_DataObject = null;

                    data = serializer.Serialize(DataConfirmation);
                }

                data = data.Replace("DataConfirm_DataObject", "GetClientCategory");
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new DataConfirmation();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    if (iErrorNumber == 50000)
                    {
                        iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[1]);
                        sMessage = Convert.ToString(Ex.Message.Split('|')[2]);
                    }
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE Statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.DataId = 0;
                DataConfirmation.IsError = true;
                DataConfirmation.ErrorNumber = iErrorNumber;
                DataConfirmation.Error = sMessage;
                DataConfirmation.DataConfirm_DataObject = null;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                data = data.Replace("DataConfirm_DataObject", "GetClientCategory");
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Comment
        //#region Transfer
        ////var transferoptions = new StripeTransferCreateOptions();
        ////transferoptions.Amount = 90;
        ////transferoptions.Currency = "cad";
        ////transferoptions.Destination = "ba_1B5DDcF25wpXO7haNAstzh7e";    ///card_1B57JKDN8wM8jx6iRHapY7bI";// StripeResponse_CreditCard.id;

        ////var transferService = new StripeTransferService();
        ////var transfer = transferService.Create(transferoptions);
        //#endregion

        //#region Create Stripe Bank Account
        ////var objBankAccountOptions = new StripeAccountBankAccountOptions();
        ////objBankAccountOptions.AccountNumber = "000123456789";
        ////objBankAccountOptions.Country = "CA";
        ////objBankAccountOptions.Currency = "cad";
        ////objBankAccountOptions.RoutingNumber = "11000-000";
        //#endregion

        //#region Create Customer
        ////var customerOptions = new StripeCustomerCreateOptions()
        ////{
        ////    Description = Customer_Description,
        ////    SourceToken = "tok_1B57JvDN8wM8jx6iSxGTi8Iq"
        ////};
        ////var customerService = new StripeCustomerService();
        ////StripeCustomer customer = customerService.Create(customerOptions);

        ////var StripeResponse_Customer = new StripeResponse_Customer();
        ////StripeResponse_Customer = new WebService.StripeResponse_Customer();
        ////StripeResponse_Customer.DataId = 1;
        ////StripeResponse_Customer.id = GetReponseValue("id", customer.StripeResponse.ResponseJson.ToString());
        ////StripeResponse_Customer.default_source = GetReponseValue("default_source", customer.StripeResponse.ResponseJson.ToString());
        ////StripeResponse_Customer.description = GetReponseValue("description", customer.StripeResponse.ResponseJson.ToString());
        ////StripeResponse_Customer.email = GetReponseValue("email", customer.StripeResponse.ResponseJson.ToString());
        //#endregion

        //#region Create Card
        ////var cardOptions = new StripeCardCreateOptions()
        ////{
        ////    SourceToken = "tok_1B57KnDN8wM8jx6ijAPpp1Pr"
        ////};

        ////var cardService = new StripeCardService();
        ////StripeCard card = cardService.Create(StripeResponse_Customer.id, cardOptions);

        ////var StripeResponse_CreditCard = new StripeResponse_CreditCard();
        ////StripeResponse_CreditCard = new WebService.StripeResponse_CreditCard();
        ////StripeResponse_CreditCard.DataId = 1;
        ////StripeResponse_CreditCard.id = GetReponseValue("id", card.StripeResponse.ResponseJson.ToString());
        ////StripeResponse_CreditCard.brand = GetReponseValue("brand", card.StripeResponse.ResponseJson.ToString());
        ////StripeResponse_CreditCard.cvc_check = GetReponseValue("cvc_check", card.StripeResponse.ResponseJson.ToString());
        ////StripeResponse_CreditCard.exp_month = GetReponseValue("exp_month", card.StripeResponse.ResponseJson.ToString());
        ////StripeResponse_CreditCard.exp_year = GetReponseValue("exp_year", card.StripeResponse.ResponseJson.ToString());
        //#endregion
        #endregion
    }
}