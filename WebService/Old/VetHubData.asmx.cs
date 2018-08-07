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

namespace WebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    //To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.ScriptService]
    public class VetHubData : System.Web.Services.WebService
    {
        //Encryption Key :  tykNlEEiDwqo7a+sMPP7Ql7TRkvpnZSQ4tvJOFBYxW0= 
        //AniVetHub

        //Degree Function

        #region Get General Settings
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetGeneralSettings(string AuthKey)
        {
            string data = string.Empty;
            var GeneralSettings = new List<GeneralSettings>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspGeneralSettings] @OpType = 'S'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        GeneralSettings.Add(new GeneralSettings()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["GeneralSettingId"]),
                            GeneralSettingId = Convert.ToInt32(dt.Rows[i]["GeneralSettingId"]),
                            MinutePerSession = Convert.ToInt32(dt.Rows[i]["MinutePerSession"]),
                            FreeSession = Convert.ToInt32(dt.Rows[i]["FreeSession"]),
                            TotalSession1 = Convert.ToInt32(dt.Rows[i]["TotalSession1"]),
                            RatePerTotalSession1 = Convert.ToInt32(dt.Rows[i]["RatePerTotalSession1"]),
                            TotalSession2 = Convert.ToInt32(dt.Rows[i]["TotalSession2"]),
                            RatePerTotalSession2 = Convert.ToInt32(dt.Rows[i]["RatePerTotalSession2"]),
                            TotalSession3 = Convert.ToInt32(dt.Rows[i]["TotalSession3"]),
                            RatePerTotalSession3 = Convert.ToInt32(dt.Rows[i]["RatePerTotalSession3"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(GeneralSettings);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get Package Settings
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPackageSettings(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var VetCredit = new List<VetCredit>();
            var PackageSettings = new List<PackageSettings>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    int IsVetPractiseUser = 0;
                    DataTable dtVet = objDBHelper.FillTable("Select Isnull(IsVetPractiseUser, 0) As IsVetPractiseUser, Isnull(SessionCredit, 0) As SessionCredit From tblVet Where VetId = " + VetId.ToString());
                    for (int i = 0; i < dtVet.Rows.Count; i++)
                    {
                        IsVetPractiseUser = Convert.ToInt32(dtVet.Rows[i]["IsVetPractiseUser"]);

                        blnDataExists = true;
                        VetCredit.Add(new VetCredit()
                        {
                            DataId = VetId,
                            VetId = VetId,
                            SessionCredit = Convert.ToInt32(dtVet.Rows[i]["SessionCredit"])
                        });
                    }

                    string sPackageQry = string.Empty;
                    if (IsVetPractiseUser == 0)
                        sPackageQry = "Select *, '#' + CONVERT(VARCHAR(max), CRYPT_GEN_RANDOM(3), 2) As ColorCode From vwPackageSettingsGet Where GeneralSettingId = 1";
                    else
                        sPackageQry = "Select *, '#' + CONVERT(VARCHAR(max), CRYPT_GEN_RANDOM(3), 2) As ColorCode From vwPackageSettingsGet Where GeneralSettingId = 2";

                    DataTable dt = objDBHelper.FillTable(sPackageQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        PackageSettings.Add(new PackageSettings()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["PackageSettingId"]),
                            PackageSettingId = Convert.ToInt32(dt.Rows[i]["PackageSettingId"]),
                            PackageName = Convert.ToString(dt.Rows[i]["PackageName"]),
                            SessionCount = Convert.ToInt32(dt.Rows[i]["SessionCount"]),
                            PackageAmount = Convert.ToInt32(dt.Rows[i]["PackageAmount"]),
                            ColorCode = Convert.ToString(dt.Rows[i]["ColorCode"])
                        });
                    }
                }

                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //data = serializer.Serialize(PackageSettings);
                //Context.Response.Write(data);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    dynamic colVet = new { VetCredit = VetCredit };
                    data = serializer.Serialize(colVet);

                    dynamic colPackageSettings = new { PackageSettings = PackageSettings };
                    data = data + "," + serializer.Serialize(colPackageSettings);
                    data = "[" + data + "]";

                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get Degree Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetDegreeInfo(string AuthKey)
        {
            string data = string.Empty;
            var Degree = new List<Degree>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspDegree] @OpType = 'Get'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        Degree.Add(new Degree()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["DegreeId"]),
                            DegreeId = Convert.ToInt32(dt.Rows[i]["DegreeId"]),
                            DegreeName = Convert.ToString(dt.Rows[i]["DegreeName"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(Degree);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //University Function
        #region Get University Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetUniversityInfo(string AuthKey)
        {
            string data = string.Empty;
            var University = new List<University>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspUniversity] @OpType = 'Get'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        University.Add(new University()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["UniversityId"]),
                            UniversityId = Convert.ToInt32(dt.Rows[i]["UniversityId"]),
                            UniversityName = Convert.ToString(dt.Rows[i]["UniversityName"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(University);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //FeedbackType Function
        #region Get FeedbackType Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetFeedbackTypeInfo(string AuthKey)
        {
            string data = string.Empty;
            var FeedbackType = new List<FeedbackType>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspFeedbackType] @OpType = 'Get'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        FeedbackType.Add(new FeedbackType()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["FeedbackTypeId"]),
                            FeedbackTypeId = Convert.ToInt32(dt.Rows[i]["FeedbackTypeId"]),
                            FeedbackTypeName = Convert.ToString(dt.Rows[i]["FeedbackTypeName"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(FeedbackType);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //RejectReason Function
        #region Get RejectReason Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetRejectReasonInfo(string AuthKey)
        {
            string data = string.Empty;
            var RejectReason = new List<RejectReason>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspRejectReason] @OpType = 'Get'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        RejectReason.Add(new RejectReason()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["RejectReasonId"]),
                            RejectReasonId = Convert.ToInt32(dt.Rows[i]["RejectReasonId"]),
                            RejectReasonName = Convert.ToString(dt.Rows[i]["RejectReasonName"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(RejectReason);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //PetType Function
        #region Get PetType Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPetTypeInfo(string AuthKey)
        {
            string data = string.Empty;
            var PetType = new List<PetType>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspPetType] @OpType = 'Get'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        PetType.Add(new PetType()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["PetTypeId"]),
                            PetTypeId = Convert.ToInt32(dt.Rows[i]["PetTypeId"]),
                            PetTypeName = Convert.ToString(dt.Rows[i]["PetTypeName"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(PetType);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //PetBreed Function
        #region Get PetBreed Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPetBreedInfo(string AuthKey, Int64 PetTypeId)
        {
            string data = string.Empty;
            var PetBreed = new List<PetBreed>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspPetBreed] @OpType = 'Get', @PetTypeId = " + PetTypeId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        PetBreed.Add(new PetBreed()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["PetBreedId"]),
                            PetBreedId = Convert.ToInt32(dt.Rows[i]["PetBreedId"]),
                            PetBreedName = Convert.ToString(dt.Rows[i]["PetBreedName"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(PetBreed);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //PetSymptoms Function
        #region Get PetSymptoms Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPetSymptomsInfo(string AuthKey, Int64 PetTypeId)
        {
            string data = string.Empty;
            var PetSymptoms = new List<PetSymptoms>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspSymptoms_PetType] @OpType = 'Get', @PetTypeId = " + PetTypeId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        PetSymptoms.Add(new PetSymptoms()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["SymptomsId"]),
                            SymptomsId = Convert.ToInt32(dt.Rows[i]["SymptomsId"]),
                            SymptomsName = Convert.ToString(dt.Rows[i]["SymptomsName"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(PetSymptoms);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Country Function
        #region Get Country Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCountryInfo(string AuthKey)
        {
            string data = string.Empty;
            var Country = new List<Country>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspCountry] @OpType = 'Get'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        Country.Add(new Country()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["CountryId"]),
                            CountryId = Convert.ToInt32(dt.Rows[i]["CountryId"]),
                            CountryName = Convert.ToString(dt.Rows[i]["CountryName"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(Country);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //State Function
        #region Get State Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetStateInfo(string AuthKey, Int64 CountryId)
        {
            string data = string.Empty;
            var State = new List<State>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspState] @OpType = 'Get', @CountryId = " + CountryId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        State.Add(new State()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["StateId"]),
                            StateId = Convert.ToInt32(dt.Rows[i]["StateId"]),
                            StateName = Convert.ToString(dt.Rows[i]["StateName"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(State);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //City Function
        #region Get City Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCityInfo(string AuthKey, Int64 StateId)
        {
            string data = string.Empty;
            var City = new List<City>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspCity] @OpType = 'Get', @StateId = " + StateId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        City.Add(new City()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["CityId"]),
                            CityId = Convert.ToInt32(dt.Rows[i]["CityId"]),
                            CityName = Convert.ToString(dt.Rows[i]["CityName"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(City);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //TimeSlot Function
        #region Get TimeSlot Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetTimeSlotInfo(string AuthKey)
        {
            string data = string.Empty;
            var TimeSlot = new List<TimeSlot>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspTimeSlot] @OpType = 'S'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        TimeSlot.Add(new TimeSlot()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["TimeSlotId"]),
                            TimeSlotId = Convert.ToInt32(dt.Rows[i]["TimeSlotId"]),
                            TimeSlotName = Convert.ToString(dt.Rows[i]["TimeSlotName"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(TimeSlot);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //TimeSlot Function
        #region Get Sub TimeSlot Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetTimeSubSlotInfo(string AuthKey, Int64 TimeSlotId)
        {
            string data = string.Empty;
            var TimeSlot = new List<TimeSlot>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspTimeSlot] @OpType = 'SubTimeSlot', @TimeSlotId = " + TimeSlotId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        TimeSlot.Add(new TimeSlot()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["TimeSlotId"]),
                            TimeSlotId = Convert.ToInt32(dt.Rows[i]["TimeSlotId"]),
                            TimeSlotName = Convert.ToString(dt.Rows[i]["TimeSlotName"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(TimeSlot);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //VetPractise Function
        #region Get VetPractise Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetPractiseInfo(string AuthKey)
        {
            string data = string.Empty;
            var VetPractise = new List<VetPractise>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = @"Select VetId As VetPractiseId, VetName As VetPractiseName From tblVet Where EndDate Is Null And IsVerified = 1 And IsVetPractiseUser = 1 Order by VetName Asc";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetPractise.Add(new VetPractise()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetPractiseId"]),
                            VetPractiseId = Convert.ToInt32(dt.Rows[i]["VetPractiseId"]),
                            VetPractiseName = Convert.ToString(dt.Rows[i]["VetPractiseName"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetPractise);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet Function     
        #region ForgotPassword
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ForgotPassword(string AuthKey, string UserName)
        {
            string data = string.Empty;
            var Vet = new List<Vet>();
            var Client = new List<Client>();
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                int IUser = 0;
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspGetUser] @OpType = 'ForgotPassword', @UserName = '" + UserName.Replace("'", "''") + "'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string sDefaultPicsPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                        string sDefaultBannerPicPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";
                        string sDefaultPicsPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractiseprofilepic.png";
                        string sDefaultBannerPicPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractisebannerpic.png";
                        string sDefaultPicsPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/profilepic.png";
                        string sDefaultBannerPicPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/bannerpic.png";

                        IUser = Convert.ToInt32(dt.Rows[0]["IUser"]);
                        if (IUser == 1)
                        {
                            Vet.Add(new Vet()
                            {
                                DataId = 1,
                                IsVetPractiseUser = Convert.ToInt32(dt.Rows[0]["IsVetPractiseUser"]),
                                VetId = Convert.ToInt32(dt.Rows[0]["VetId"]),
                                VetName = Convert.ToString(dt.Rows[0]["VetName"]),
                                Address1 = Convert.ToString(dt.Rows[0]["Address1"]),
                                Address2 = Convert.ToString(dt.Rows[0]["Address2"]),
                                CityId = Convert.ToInt64(dt.Rows[0]["City"]),
                                City = Convert.ToString(dt.Rows[0]["CityName"]),
                                POBox = Convert.ToString(dt.Rows[0]["POBox"]),
                                StateId = Convert.ToInt64(dt.Rows[0]["State"]),
                                State = Convert.ToString(dt.Rows[0]["StateName"]),
                                CountryId = Convert.ToInt64(dt.Rows[0]["Country"]),
                                Country = Convert.ToString(dt.Rows[0]["CountryName"]),
                                PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]),
                                EmailId = Convert.ToString(dt.Rows[0]["EmailId"]),
                                SessionRate = Convert.ToDouble(dt.Rows[0]["SessionRate"]),
                                Biography = Convert.ToString(dt.Rows[0]["Biography"]),
                                TotalExp = Convert.ToDouble(dt.Rows[0]["TotalExp"]),
                                UserName = Convert.ToString(dt.Rows[0]["UserName"]),
                                Password = Convert.ToString(dt.Rows[0]["Password"]),
                                IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]),
                                CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]),
                                EndDate = Convert.ToString(dt.Rows[0]["EndDate"]),
                                IsVerified = Convert.ToInt32(dt.Rows[0]["IsVerified"]),
                                AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]),
                                RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]),
                                ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? (Convert.ToInt32(dt.Rows[0]["IsVetPractiseUser"]) == 0 ? sDefaultPicsPath_Vet : sDefaultPicsPath_VetPractise) : Convert.ToString(dt.Rows[0]["ProfilePic"]),
                                BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["BannerPic"])) ? (Convert.ToInt32(dt.Rows[0]["IsVetPractiseUser"]) == 0 ? sDefaultBannerPicPath_Vet : sDefaultBannerPicPath_VetPractise) : Convert.ToString(dt.Rows[0]["BannerPic"]),
                                OnlineStatus = Convert.ToInt32(dt.Rows[0]["OnlineStatus"]),
                                Rating = Convert.ToDouble(dt.Rows[0]["Rating"]),
                                Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]),
                                Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]),
                                Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]),
                                PaypalAccountId = Convert.ToString(dt.Rows[0]["PaypalAccountId"]),
                                SessionCredit = Convert.ToInt32(dt.Rows[0]["SessionCredit"]),

                                IsVetProfile = Convert.ToInt32(dt.Rows[0]["IsVetProfile"]),
                                IsVetAccount = Convert.ToInt32(dt.Rows[0]["IsVetAccount"]),
                                IsVetEducation = Convert.ToInt32(dt.Rows[0]["IsVetEducation"]),
                                IsVetExperience = Convert.ToInt32(dt.Rows[0]["IsVetExperience"]),
                                IsVetExpertise = Convert.ToInt32(dt.Rows[0]["IsVetExpertise"]),
                                IsVetTimeSlot = Convert.ToInt32(dt.Rows[0]["IsVetTimeSlot"]),
                                IsVetClinic = Convert.ToInt32(dt.Rows[0]["IsVetClinic"])
                            });
                        }
                        else if (IUser == 2)
                        {
                            Client.Add(new Client()
                            {
                                DataId = 2,
                                ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"]),
                                ClientName = Convert.ToString(dt.Rows[0]["ClientName"]),
                                Address1 = Convert.ToString(dt.Rows[0]["Address1"]),
                                Address2 = Convert.ToString(dt.Rows[0]["Address2"]),
                                CityId = Convert.ToInt64(dt.Rows[0]["City"]),
                                City = Convert.ToString(dt.Rows[0]["CityName"]),
                                POBox = Convert.ToString(dt.Rows[0]["POBox"]),
                                StateId = Convert.ToInt64(dt.Rows[0]["State"]),
                                State = Convert.ToString(dt.Rows[0]["StateName"]),
                                CountryId = Convert.ToInt64(dt.Rows[0]["Country"]),
                                Country = Convert.ToString(dt.Rows[0]["CountryName"]),
                                PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]),
                                EmailId = Convert.ToString(dt.Rows[0]["EmailId"]),
                                UserName = Convert.ToString(dt.Rows[0]["UserName"]),
                                Password = Convert.ToString(dt.Rows[0]["Password"]),
                                ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? sDefaultPicsPath_Client : Convert.ToString(dt.Rows[0]["ProfilePic"]),
                                BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["BannerPic"])) ? sDefaultBannerPicPath_Client : Convert.ToString(dt.Rows[0]["BannerPic"]),
                                AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]),
                                VetName = Convert.ToString(dt.Rows[0]["VetName"]),
                                VetAddress = Convert.ToString(dt.Rows[0]["VetAddress"]),
                                VetContactNo = Convert.ToString(dt.Rows[0]["VetContactNo"]),
                                VetEmailId = Convert.ToString(dt.Rows[0]["VetEmailId"]),

                                IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]),
                                CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]),
                                EndDate = Convert.ToString(dt.Rows[0]["EndDate"]),
                                RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]),

                                Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]),
                                Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]),
                                Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]),

                                IsClientProfile = Convert.ToInt32(dt.Rows[0]["IsClientProfile"]),
                                IsClientPetProfile = Convert.ToInt32(dt.Rows[0]["IsClientPetProfile"])
                            });
                        }
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (IUser == 1)
                    data = serializer.Serialize(Vet);
                else if (IUser == 2)
                    data = serializer.Serialize(Client);
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }

                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get User
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetUser(string AuthKey, string UserName, string Password)
        {
            string data = string.Empty;
            var Vet = new List<Vet>();
            var Client = new List<Client>();
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                int IUser = 0;
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspGetUser] @OpType = 'GetUser', @UserName = '" + UserName.Replace("'", "''") + "', @Password = '" + Password.Replace("'", "''") + "'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string sDefaultPicsPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                        string sDefaultBannerPicPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";
                        string sDefaultPicsPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractiseprofilepic.png";
                        string sDefaultBannerPicPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractisebannerpic.png";
                        string sDefaultPicsPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/profilepic.png";
                        string sDefaultBannerPicPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/bannerpic.png";

                        IUser = Convert.ToInt32(dt.Rows[0]["IUser"]);
                        if (IUser == 1)
                        {
                            Vet.Add(new Vet()
                            {
                                DataId = 1,
                                IsVetPractiseUser = Convert.ToInt32(dt.Rows[0]["IsVetPractiseUser"]),
                                VetId = Convert.ToInt32(dt.Rows[0]["VetId"]),
                                VetName = Convert.ToString(dt.Rows[0]["VetName"]),
                                Address1 = Convert.ToString(dt.Rows[0]["Address1"]),
                                Address2 = Convert.ToString(dt.Rows[0]["Address2"]),
                                CityId = Convert.ToInt64(dt.Rows[0]["City"]),
                                City = Convert.ToString(dt.Rows[0]["CityName"]),
                                POBox = Convert.ToString(dt.Rows[0]["POBox"]),
                                StateId = Convert.ToInt64(dt.Rows[0]["State"]),
                                State = Convert.ToString(dt.Rows[0]["StateName"]),
                                CountryId = Convert.ToInt64(dt.Rows[0]["Country"]),
                                Country = Convert.ToString(dt.Rows[0]["CountryName"]),
                                PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]),
                                EmailId = Convert.ToString(dt.Rows[0]["EmailId"]),
                                SessionRate = Convert.ToDouble(dt.Rows[0]["SessionRate"]),
                                Biography = Convert.ToString(dt.Rows[0]["Biography"]),
                                TotalExp = Convert.ToDouble(dt.Rows[0]["TotalExp"]),
                                UserName = Convert.ToString(dt.Rows[0]["UserName"]),
                                Password = Convert.ToString(dt.Rows[0]["Password"]),
                                IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]),
                                CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]),
                                EndDate = Convert.ToString(dt.Rows[0]["EndDate"]),
                                IsVerified = Convert.ToInt32(dt.Rows[0]["IsVerified"]),
                                AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]),
                                RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]),
                                ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? (Convert.ToInt32(dt.Rows[0]["IsVetPractiseUser"]) == 0 ? sDefaultPicsPath_Vet : sDefaultPicsPath_VetPractise) : Convert.ToString(dt.Rows[0]["ProfilePic"]),
                                BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["BannerPic"])) ? (Convert.ToInt32(dt.Rows[0]["IsVetPractiseUser"]) == 0 ? sDefaultBannerPicPath_Vet : sDefaultBannerPicPath_VetPractise) : Convert.ToString(dt.Rows[0]["BannerPic"]),
                                OnlineStatus = Convert.ToInt32(dt.Rows[0]["OnlineStatus"]),
                                Rating = Convert.ToDouble(dt.Rows[0]["Rating"]),
                                Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]),
                                Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]),
                                Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]),
                                PaypalAccountId = Convert.ToString(dt.Rows[0]["PaypalAccountId"]),
                                SessionCredit = Convert.ToInt32(dt.Rows[0]["SessionCredit"]),

                                IsVetProfile = Convert.ToInt32(dt.Rows[0]["IsVetProfile"]),
                                IsVetAccount = Convert.ToInt32(dt.Rows[0]["IsVetAccount"]),
                                IsVetEducation = Convert.ToInt32(dt.Rows[0]["IsVetEducation"]),
                                IsVetExperience = Convert.ToInt32(dt.Rows[0]["IsVetExperience"]),
                                IsVetExpertise = Convert.ToInt32(dt.Rows[0]["IsVetExpertise"]),
                                IsVetTimeSlot = Convert.ToInt32(dt.Rows[0]["IsVetTimeSlot"]),
                                IsVetClinic = Convert.ToInt32(dt.Rows[0]["IsVetClinic"])
                            });
                        }
                        else if (IUser == 2)
                        {
                            Client.Add(new Client()
                            {
                                DataId = 2,
                                ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"]),
                                ClientName = Convert.ToString(dt.Rows[0]["ClientName"]),
                                Address1 = Convert.ToString(dt.Rows[0]["Address1"]),
                                Address2 = Convert.ToString(dt.Rows[0]["Address2"]),
                                CityId = Convert.ToInt64(dt.Rows[0]["City"]),
                                City = Convert.ToString(dt.Rows[0]["CityName"]),
                                POBox = Convert.ToString(dt.Rows[0]["POBox"]),
                                StateId = Convert.ToInt64(dt.Rows[0]["State"]),
                                State = Convert.ToString(dt.Rows[0]["StateName"]),
                                CountryId = Convert.ToInt64(dt.Rows[0]["Country"]),
                                Country = Convert.ToString(dt.Rows[0]["CountryName"]),
                                PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]),
                                EmailId = Convert.ToString(dt.Rows[0]["EmailId"]),
                                UserName = Convert.ToString(dt.Rows[0]["UserName"]),
                                Password = Convert.ToString(dt.Rows[0]["Password"]),
                                IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]),
                                CreatedOn = Convert.ToString(dt.Rows[0]["CreatedOn"]),
                                EndDate = Convert.ToString(dt.Rows[0]["EndDate"]),
                                AcTokenId = Convert.ToString(dt.Rows[0]["AcTokenId"]),
                                RegisteredBy = Convert.ToString(dt.Rows[0]["RegisteredBy"]),
                                ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ProfilePic"])) ? sDefaultPicsPath_Client : Convert.ToString(dt.Rows[0]["ProfilePic"]),
                                BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["BannerPic"])) ? sDefaultBannerPicPath_Client : Convert.ToString(dt.Rows[0]["BannerPic"]),

                                Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]),
                                Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]),
                                Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"]),

                                VetName = Convert.ToString(dt.Rows[0]["VetName"]),
                                VetAddress = Convert.ToString(dt.Rows[0]["VetAddress"]),
                                VetContactNo = Convert.ToString(dt.Rows[0]["VetContactNo"]),
                                VetEmailId = Convert.ToString(dt.Rows[0]["VetEmailId"]),

                                IsClientProfile = Convert.ToInt32(dt.Rows[0]["IsClientProfile"]),
                                IsClientPetProfile = Convert.ToInt32(dt.Rows[0]["IsClientPetProfile"])
                            });
                        }
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (IUser == 1)
                    data = serializer.Serialize(Vet);
                else if (IUser == 2)
                    data = serializer.Serialize(Client);
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }

                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get Vet Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetInfo(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var Vet = new List<Vet>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sDefaultPicsPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                    string sDefaultBannerPicPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVet] @OpType = 'S', @VetId = " + VetId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        Vet.Add(new Vet()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            IsVetPractiseUser = Convert.ToInt32(dt.Rows[i]["IsVetPractiseUser"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            VetName = Convert.ToString(dt.Rows[i]["VetName"]),
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
                            SessionRate = Convert.ToDouble(dt.Rows[i]["SessionRate"]),
                            Biography = Convert.ToString(dt.Rows[i]["Biography"]),
                            TotalExp = Convert.ToDouble(dt.Rows[i]["TotalExp"]),
                            UserName = Convert.ToString(dt.Rows[i]["UserName"]),
                            Password = Convert.ToString(dt.Rows[i]["Password"]),
                            ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[i]["ProfilePic"]),
                            BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["BannerPic"])) ? sDefaultBannerPicPath : Convert.ToString(dt.Rows[i]["BannerPic"]),
                            IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"]),
                            IsVerified = Convert.ToInt32(dt.Rows[i]["IsVerified"]),
                            AcTokenId = Convert.ToString(dt.Rows[i]["AcTokenId"]),
                            RegisteredBy = Convert.ToString(dt.Rows[i]["RegisteredBy"]),
                            OnlineStatus = Convert.ToInt32(dt.Rows[i]["OnlineStatus"]),
                            Rating = Convert.ToDouble(dt.Rows[i]["Rating"]),
                            Latitude = Convert.ToDouble(dt.Rows[i]["Latitude"]),
                            Longitude = Convert.ToDouble(dt.Rows[i]["Longitude"]),
                            Altitude = Convert.ToDouble(dt.Rows[i]["Altitude"]),
                            PaypalAccountId = Convert.ToString(dt.Rows[i]["PaypalAccountId"]),
                            SessionCredit = Convert.ToInt32(dt.Rows[i]["SessionCredit"]),

                            IsVetProfile = Convert.ToInt32(dt.Rows[i]["IsVetProfile"]),
                            IsVetAccount = Convert.ToInt32(dt.Rows[i]["IsVetAccount"]),
                            IsVetEducation = Convert.ToInt32(dt.Rows[i]["IsVetEducation"]),
                            IsVetExperience = Convert.ToInt32(dt.Rows[i]["IsVetExperience"]),
                            IsVetExpertise = Convert.ToInt32(dt.Rows[i]["IsVetExpertise"]),
                            IsVetTimeSlot = Convert.ToInt32(dt.Rows[i]["IsVetTimeSlot"]),
                            IsVetClinic = Convert.ToInt32(dt.Rows[i]["IsVetClinic"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(Vet);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet User Exists
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetExists(string AuthKey, string UserName)
        {
            string data = string.Empty;
            var IsExists = new List<IsExists>();
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVet] @OpType = 'IsExists', @UserName = '" + UserName.Replace("'", "''") + "'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IsExists.Add(new IsExists()
                        {
                            IsDataExists = Convert.ToInt32(dt.Rows[i]["IsExists"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(IsExists);
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetInsert(string AuthKey, string VetName, string PhoneNo, string EmailId, string UserName, string Password, string AcTokenId, string RegisteredBy, int IsVetPractiseUser)
        {
            string data = string.Empty;
            bool blnVetUser = false;
            bool blnDataExists = false;
            var Vet = new List<Vet>();
            var IsExists = new List<IsExists>();
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    //string sFilePath = "C:\\Users\\Admin\\Downloads\\_AniVetHub\\profilepic.png";
                    //Hashtable ParaValues = new Hashtable();
                    //ParaValues.Add("@DefaultPic", ConvertFileToBinary(sFilePath));

                    //objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    //objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    //objDBHelper.ExecuteStoredProcedure("uspSettingsInsert", ParaValues, true);

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVet] @OpType = 'IsExists', @UserName = '" + UserName.Replace("'", "''") + "'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["IsExists"]) == 1)
                            blnDataExists = true;

                        IsExists.Add(new IsExists()
                        {
                            IsDataExists = Convert.ToInt32(dt.Rows[i]["IsExists"])
                        });
                    }

                    if (!blnDataExists)
                    {
                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        dt = objDBHelper.FillTable("Exec [uspVet] @VetId = 0, @VetName = '" + VetName.Replace("'", "''") + "', @PhoneNo = '" + PhoneNo.Replace("'", "''") + "', @EmailId = '" + EmailId.Replace("'", "''") + "', @UserName = '" + UserName.Replace("'", "''") + "', @Password = '" + Password.Replace("'", "''") + "', @AcTokenId = '" + AcTokenId.Replace("'", "''") + "', @RegisteredBy = '" + RegisteredBy.ToString() + "', @IsVetPractiseUser = " + IsVetPractiseUser.ToString() + ", @OpType = 'I'");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                            objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                            DataTable dtUser = objDBHelper.FillTable("Exec [uspGetUser] @OpType = 'GetUser', @UserName = '" + UserName.Replace("'", "''") + "', @Password = '" + Password.Replace("'", "''") + "'");
                            if (dtUser != null && dtUser.Rows.Count > 0)
                            {
                                string sDefaultPicsPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                                string sDefaultBannerPicPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";
                                blnVetUser = true;

                                Vet.Add(new Vet()
                                {
                                    DataId = 1,
                                    IsVetPractiseUser = Convert.ToInt32(dtUser.Rows[0]["IsVetPractiseUser"]),
                                    VetId = Convert.ToInt32(dtUser.Rows[0]["VetId"]),
                                    VetName = Convert.ToString(dtUser.Rows[0]["VetName"]),
                                    Address1 = Convert.ToString(dtUser.Rows[0]["Address1"]),
                                    Address2 = Convert.ToString(dtUser.Rows[0]["Address2"]),
                                    CityId = Convert.ToInt64(dtUser.Rows[0]["City"]),
                                    City = Convert.ToString(dtUser.Rows[0]["CityName"]),
                                    POBox = Convert.ToString(dtUser.Rows[0]["POBox"]),
                                    StateId = Convert.ToInt64(dtUser.Rows[0]["State"]),
                                    State = Convert.ToString(dtUser.Rows[0]["StateName"]),
                                    CountryId = Convert.ToInt64(dtUser.Rows[0]["Country"]),
                                    Country = Convert.ToString(dtUser.Rows[0]["CountryName"]),
                                    PhoneNo = Convert.ToString(dtUser.Rows[0]["PhoneNo"]),
                                    EmailId = Convert.ToString(dtUser.Rows[0]["EmailId"]),
                                    SessionRate = Convert.ToDouble(dtUser.Rows[0]["SessionRate"]),
                                    Biography = Convert.ToString(dtUser.Rows[0]["Biography"]),
                                    TotalExp = Convert.ToDouble(dtUser.Rows[0]["TotalExp"]),
                                    UserName = Convert.ToString(dtUser.Rows[0]["UserName"]),
                                    Password = Convert.ToString(dtUser.Rows[0]["Password"]),
                                    IsActive = Convert.ToInt32(dtUser.Rows[0]["IsActive"]),
                                    CreatedOn = Convert.ToString(dtUser.Rows[0]["CreatedOn"]),
                                    EndDate = Convert.ToString(dtUser.Rows[0]["EndDate"]),
                                    IsVerified = Convert.ToInt32(dtUser.Rows[0]["IsVerified"]),
                                    AcTokenId = Convert.ToString(dtUser.Rows[0]["AcTokenId"]),
                                    RegisteredBy = Convert.ToString(dtUser.Rows[0]["RegisteredBy"]),
                                    ProfilePic = string.IsNullOrEmpty(Convert.ToString(dtUser.Rows[0]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dtUser.Rows[0]["ProfilePic"]),
                                    BannerPic = string.IsNullOrEmpty(Convert.ToString(dtUser.Rows[0]["BannerPic"])) ? sDefaultBannerPicPath : Convert.ToString(dtUser.Rows[0]["BannerPic"]),
                                    OnlineStatus = Convert.ToInt32(dtUser.Rows[0]["OnlineStatus"]),
                                    Rating = Convert.ToDouble(dtUser.Rows[0]["Rating"]),
                                    Latitude = Convert.ToDouble(dtUser.Rows[0]["Latitude"]),
                                    Longitude = Convert.ToDouble(dtUser.Rows[0]["Longitude"]),
                                    Altitude = Convert.ToDouble(dtUser.Rows[0]["Altitude"]),
                                    PaypalAccountId = Convert.ToString(dtUser.Rows[0]["PaypalAccountId"]),
                                    SessionCredit = Convert.ToInt32(dtUser.Rows[0]["SessionCredit"]),

                                    IsVetProfile = Convert.ToInt32(dtUser.Rows[0]["IsVetProfile"]),
                                    IsVetAccount = Convert.ToInt32(dtUser.Rows[0]["IsVetAccount"]),
                                    IsVetEducation = Convert.ToInt32(dtUser.Rows[0]["IsVetEducation"]),
                                    IsVetExperience = Convert.ToInt32(dtUser.Rows[0]["IsVetExperience"]),
                                    IsVetExpertise = Convert.ToInt32(dtUser.Rows[0]["IsVetExpertise"]),
                                    IsVetTimeSlot = Convert.ToInt32(dtUser.Rows[0]["IsVetTimeSlot"]),
                                    IsVetClinic = Convert.ToInt32(dtUser.Rows[0]["IsVetClinic"])
                                });
                            }
                        }
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(IsExists);
                    Context.Response.Write(data);
                }
                else
                {
                    if (blnVetUser)
                    {
                        data = serializer.Serialize(Vet);
                        Context.Response.Write(data);
                    }
                    else
                    {
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(0),
                            Error = "No Record found.",
                            ErrorNumber = 999
                        });
                        data = serializer.Serialize(DataConfirmation);
                        Context.Response.Write(data);
                    }
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetUpdate(string AuthKey, Int64 VetId, string VetName, string Address1, string Address2, string City, string POBox, string State, string Country, string PhoneNo, string EmailId, double SessionRate, string AcTokenId, string Biography)
        {
            string data = string.Empty;
            var Vet = new List<Vet>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sQry = @"Exec [uspVet] 
                                    @VetId = " + VetId.ToString() + ", @VetName = '" + VetName.Replace("'", "''") + "', @Address1 = '" + Address1.Replace("'", "''") + "', @Address2 = '" + Address2.Replace("'", "''") + @"', 
                                    @City = '" + City.Replace("'", "''") + "', @POBox = '" + POBox.Replace("'", "''") + "', @State = '" + State.Replace("'", "''") + "', @Country = '" + Country.Replace("'", "''") + @"', 
                                    @PhoneNo = '" + PhoneNo.Replace("'", "''") + "', @EmailId = '" + EmailId.Replace("'", "''") + "', @SessionRate = " + SessionRate.ToString() + ", @AcTokenId = '" + AcTokenId.Replace("'", "''") + "', @Biography = '" + Biography.Replace("'", "''") + "', @OpType = 'U'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        string sDefaultPicsPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                        string sDefaultBannerPicPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";

                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        DataTable dt_Vet = objDBHelper.FillTable("Exec [uspVet] @OpType = 'S', @VetId = " + Convert.ToString(dt.Rows[k]["VetId"]));
                        for (int i = 0; i < dt_Vet.Rows.Count; i++)
                        {
                            blnDataExists = true;
                            Vet.Add(new Vet()
                            {
                                DataId = Convert.ToInt32(dt_Vet.Rows[i]["VetId"]),
                                IsVetPractiseUser = Convert.ToInt32(dt_Vet.Rows[i]["IsVetPractiseUser"]),
                                VetId = Convert.ToInt32(dt_Vet.Rows[i]["VetId"]),
                                VetName = Convert.ToString(dt_Vet.Rows[i]["VetName"]),
                                Address1 = Convert.ToString(dt_Vet.Rows[i]["Address1"]),
                                Address2 = Convert.ToString(dt_Vet.Rows[i]["Address2"]),
                                CityId = Convert.ToInt64(dt_Vet.Rows[i]["City"]),
                                City = Convert.ToString(dt_Vet.Rows[i]["CityName"]),
                                POBox = Convert.ToString(dt_Vet.Rows[i]["POBox"]),
                                StateId = Convert.ToInt64(dt_Vet.Rows[i]["State"]),
                                State = Convert.ToString(dt_Vet.Rows[i]["StateName"]),
                                CountryId = Convert.ToInt64(dt_Vet.Rows[i]["Country"]),
                                Country = Convert.ToString(dt_Vet.Rows[i]["CountryName"]),
                                PhoneNo = Convert.ToString(dt_Vet.Rows[i]["PhoneNo"]),
                                EmailId = Convert.ToString(dt_Vet.Rows[i]["EmailId"]),
                                SessionRate = Convert.ToDouble(dt_Vet.Rows[i]["SessionRate"]),
                                Biography = Convert.ToString(dt_Vet.Rows[i]["Biography"]),
                                TotalExp = Convert.ToDouble(dt_Vet.Rows[i]["TotalExp"]),
                                UserName = Convert.ToString(dt_Vet.Rows[i]["UserName"]),
                                Password = Convert.ToString(dt_Vet.Rows[i]["Password"]),
                                ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt_Vet.Rows[i]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt_Vet.Rows[i]["ProfilePic"]),
                                BannerPic = string.IsNullOrEmpty(Convert.ToString(dt_Vet.Rows[i]["BannerPic"])) ? sDefaultBannerPicPath : Convert.ToString(dt_Vet.Rows[i]["BannerPic"]),
                                IsActive = Convert.ToInt32(dt_Vet.Rows[i]["IsActive"]),
                                CreatedOn = Convert.ToString(dt_Vet.Rows[i]["CreatedOn"]),
                                EndDate = Convert.ToString(dt_Vet.Rows[i]["EndDate"]),
                                IsVerified = Convert.ToInt32(dt_Vet.Rows[i]["IsVerified"]),
                                AcTokenId = Convert.ToString(dt_Vet.Rows[i]["AcTokenId"]),
                                RegisteredBy = Convert.ToString(dt_Vet.Rows[i]["RegisteredBy"]),
                                OnlineStatus = Convert.ToInt32(dt_Vet.Rows[i]["OnlineStatus"]),
                                Rating = Convert.ToDouble(dt_Vet.Rows[i]["Rating"]),
                                Latitude = Convert.ToDouble(dt_Vet.Rows[i]["Latitude"]),
                                Longitude = Convert.ToDouble(dt_Vet.Rows[i]["Longitude"]),
                                Altitude = Convert.ToDouble(dt_Vet.Rows[i]["Altitude"]),
                                PaypalAccountId = Convert.ToString(dt_Vet.Rows[i]["PaypalAccountId"]),
                                SessionCredit = Convert.ToInt32(dt_Vet.Rows[i]["SessionCredit"]),

                                IsVetProfile = Convert.ToInt32(dt_Vet.Rows[i]["IsVetProfile"]),
                                IsVetAccount = Convert.ToInt32(dt_Vet.Rows[i]["IsVetAccount"]),
                                IsVetEducation = Convert.ToInt32(dt_Vet.Rows[i]["IsVetEducation"]),
                                IsVetExperience = Convert.ToInt32(dt_Vet.Rows[i]["IsVetExperience"]),
                                IsVetExpertise = Convert.ToInt32(dt_Vet.Rows[i]["IsVetExpertise"]),
                                IsVetTimeSlot = Convert.ToInt32(dt_Vet.Rows[i]["IsVetTimeSlot"]),
                                IsVetClinic = Convert.ToInt32(dt_Vet.Rows[i]["IsVetClinic"])
                            });
                        }
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(Vet);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet Change Password
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetChangePassword(string AuthKey, Int64 VetId, string Password)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sQry = @"Exec [uspVet] @VetId = " + VetId.ToString() + ", @Password = '" + Password.Replace("'", "''") + "', @OpType = 'CP'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet Profile Pic Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetProfilePicUpdate(string AuthKey, Int64 VetId, byte[] ProfilePic)
        {
            string data = string.Empty;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sVetPicsPath = Server.MapPath(".") + "/VetPics/" + VetId.ToString() + ".png";
                    string sVetPicsPath_Save = "http://admin.anivethub.com/WebService" + "/VetPics/" + VetId.ToString() + ".png";
                    string sError = string.Empty;
                    if (ConvertBinaryToFile(sVetPicsPath, ProfilePic, ref sError))
                    {
                        Hashtable ParaValues = new Hashtable();
                        ParaValues.Add("@VetId", VetId);
                        ParaValues.Add("@ProfilePic", sVetPicsPath_Save);
                        ParaValues.Add("@OpType", "PP");

                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        objDBHelper.ExecuteStoredProcedure("uspVet", ParaValues, true);

                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(VetId),
                            Error = sVetPicsPath_Save
                        });
                    }
                    else
                    {
                        Hashtable ParaValues = new Hashtable();
                        ParaValues.Add("@VetId", VetId);
                        ParaValues.Add("@ProfilePic", string.Empty);
                        ParaValues.Add("@OpType", "PP");

                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        objDBHelper.ExecuteStoredProcedure("uspVet", ParaValues, true);

                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = 0,
                            Error = sError
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet Profile Pic Remove
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetProfilePicRemove(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sVetPicsPath = Server.MapPath(".") + "/VetPics/" + VetId.ToString() + ".png";
                    try
                    {
                        System.IO.File.Delete(sVetPicsPath);
                    }
                    catch { }

                    Hashtable ParaValues = new Hashtable();
                    ParaValues.Add("@VetId", VetId);
                    ParaValues.Add("@ProfilePic", string.Empty);
                    ParaValues.Add("@OpType", "PP");

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    objDBHelper.ExecuteStoredProcedure("uspVet", ParaValues, true);

                    blnDataExists = true;
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(VetId)
                    });
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet Banner Pic Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetBannerPicUpdate(string AuthKey, Int64 VetId, byte[] BannerPic)
        {
            string data = string.Empty;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sVetBannerPicsPath = Server.MapPath(".") + "/VetBannerPics/" + VetId.ToString() + ".png";
                    string sVetBannerPicsPath_Save = "http://admin.anivethub.com/WebService" + "/VetBannerPics/" + VetId.ToString() + ".png";
                    string sError = string.Empty;
                    if (ConvertBinaryToFile(sVetBannerPicsPath, BannerPic, ref sError))
                    {
                        Hashtable ParaValues = new Hashtable();
                        ParaValues.Add("@VetId", VetId);
                        ParaValues.Add("@BannerPic", sVetBannerPicsPath_Save);
                        ParaValues.Add("@OpType", "BP");

                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        objDBHelper.ExecuteStoredProcedure("uspVet", ParaValues, true);

                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(VetId),
                            Error = sVetBannerPicsPath_Save
                        });
                    }
                    else
                    {
                        Hashtable ParaValues = new Hashtable();
                        ParaValues.Add("@VetId", VetId);
                        ParaValues.Add("@BannerPic", string.Empty);
                        ParaValues.Add("@OpType", "BP");

                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        objDBHelper.ExecuteStoredProcedure("uspVet", ParaValues, true);

                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = 0,
                            Error = sError
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet Banner Pic Remove
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetBannerPicRemove(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sVetBannerPicsPath = Server.MapPath(".") + "/VetBannerPics/" + VetId.ToString() + ".png";
                    try
                    {
                        System.IO.File.Delete(sVetBannerPicsPath);
                    }
                    catch { }

                    Hashtable ParaValues = new Hashtable();
                    ParaValues.Add("@VetId", VetId);
                    ParaValues.Add("@BannerPic", string.Empty);
                    ParaValues.Add("@OpType", "BP");

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    objDBHelper.ExecuteStoredProcedure("uspVet", ParaValues, true);

                    blnDataExists = true;
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(VetId)
                    });
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get Vet Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SearchVet(string AuthKey, int IsOnline, int IsBusy, int IsOffline, int MinRate, int MaxRate, int MinDistance, int MaxDistance, int SortBy, int SortType, int PageNumber, int Records, int ClientId, int IsVet, int IsVetPractise, string VetName)
        {
            string data = string.Empty;
            var Vet = new List<Vet>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sDefaultPicsPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                    string sDefaultBannerPicPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = @"Exec [uspVetSearch] @IsOnline = " + IsOnline.ToString() + ", @IsBusy = " + IsBusy.ToString() + ", @IsOffline = " + IsOffline.ToString() + @", 
                                                        @MinRate = " + MinRate.ToString() + ", @MaxRate = " + MaxRate.ToString() + @", 
                                                        @IsVet = " + IsVet.ToString() + ", @IsVetPractise = " + IsVetPractise.ToString() + ", @VetName = '" + VetName + @"',
                                                        @MinDistance = " + MinDistance.ToString() + ", @MaxDistance = " + MaxDistance.ToString() + @", 
                                                        @SortBy = " + SortBy.ToString() + ", @SortType = " + SortType.ToString() + @", 
                                                        @PageNumber = " + PageNumber.ToString() + ", @Records = " + Records.ToString() + ", @ClientId = " + ClientId.ToString();
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double dblKM = 0;
                        try
                        {
                            var sCoord = new GeoCoordinate(Convert.ToDouble(dt.Rows[i]["ClientLatitude"]), Convert.ToDouble(dt.Rows[i]["ClientLongitude"]));
                            var eCoord = new GeoCoordinate(Convert.ToDouble(dt.Rows[i]["Latitude"]), Convert.ToDouble(dt.Rows[i]["Longitude"]));
                            dblKM = sCoord.GetDistanceTo(eCoord);
                            dblKM = dblKM / 1000;
                        }
                        catch { }

                        blnDataExists = true;
                        Vet.Add(new Vet()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            IsVetPractiseUser = Convert.ToInt32(dt.Rows[i]["IsVetPractiseUser"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            VetName = Convert.ToString(dt.Rows[i]["VetName"]),
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
                            SessionRate = Convert.ToDouble(dt.Rows[i]["SessionRate"]),
                            Biography = Convert.ToString(dt.Rows[i]["Biography"]),
                            TotalExp = Convert.ToDouble(dt.Rows[i]["TotalExp"]),
                            UserName = Convert.ToString(dt.Rows[i]["UserName"]),
                            Password = Convert.ToString(dt.Rows[i]["Password"]),
                            ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[i]["ProfilePic"]),
                            BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["BannerPic"])) ? sDefaultBannerPicPath : Convert.ToString(dt.Rows[i]["BannerPic"]),
                            IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"]),
                            IsVerified = Convert.ToInt32(dt.Rows[i]["IsVerified"]),
                            AcTokenId = Convert.ToString(dt.Rows[i]["AcTokenId"]),
                            RegisteredBy = Convert.ToString(dt.Rows[i]["RegisteredBy"]),
                            PaypalAccountId = Convert.ToString(dt.Rows[i]["PaypalAccountId"]),
                            SessionCredit = Convert.ToInt32(dt.Rows[i]["SessionCredit"]),

                            IsVetProfile = Convert.ToInt32(dt.Rows[i]["IsVetProfile"]),
                            IsVetAccount = Convert.ToInt32(dt.Rows[i]["IsVetAccount"]),
                            IsVetEducation = Convert.ToInt32(dt.Rows[i]["IsVetEducation"]),
                            IsVetExperience = Convert.ToInt32(dt.Rows[i]["IsVetExperience"]),
                            IsVetExpertise = Convert.ToInt32(dt.Rows[i]["IsVetExpertise"]),
                            IsVetTimeSlot = Convert.ToInt32(dt.Rows[i]["IsVetTimeSlot"]),
                            IsVetClinic = Convert.ToInt32(dt.Rows[i]["IsVetClinic"]),

                            OnlineStatus = Convert.ToInt32(dt.Rows[i]["OnlineStatus"]),
                            VetExpertise = Convert.ToString(dt.Rows[i]["VetExpertise"]),
                            VetEducation = Convert.ToString(dt.Rows[i]["VetEducation"]),
                            Rating = Convert.ToDouble(dt.Rows[i]["Rating"]),

                            MinSessionRate = Convert.ToDouble(dt.Rows[i]["MinSessionRate"]),
                            MaxSessionRate = Convert.ToDouble(dt.Rows[i]["MaxSessionRate"]),

                            MinDistanceVet = Convert.ToDouble(dt.Rows[i]["MinDistanceVet"]),
                            MaxDistanceVet = Convert.ToDouble(dt.Rows[i]["MaxDistanceVet"]),
                            Distance = Convert.ToDouble(dt.Rows[i]["Distance"]),

                            Latitude = Convert.ToDouble(dt.Rows[i]["Latitude"]),
                            Longitude = Convert.ToDouble(dt.Rows[i]["Longitude"]),
                            Altitude = Convert.ToDouble(dt.Rows[i]["Altitude"]),

                            SortBy = Convert.ToInt32(dt.Rows[i]["SortBy"]),
                            SortType = Convert.ToInt32(dt.Rows[i]["SortType"]),

                            RowNo = Convert.ToInt32(dt.Rows[i]["RowNo"]),
                            TotalRowNo = Convert.ToInt32(dt.Rows[i]["TotalRowNo"]),
                            TotalPage = Convert.ToInt32(dt.Rows[i]["TotalPage"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(Vet);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get Vet Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SearchVet_VetPractise(string AuthKey, Int64 VetPractiseId, Int64 ClientId)
        {
            string data = string.Empty;
            var Vet = new List<Vet>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sDefaultPicsPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                    string sDefaultBannerPicPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = @"Exec [uspVetSearch_VetPractise] @VetPractiseId = " + VetPractiseId.ToString() + ", @ClientId = " + ClientId.ToString();
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double dblKM = 0;
                        try
                        {
                            var sCoord = new GeoCoordinate(Convert.ToDouble(dt.Rows[i]["ClientLatitude"]), Convert.ToDouble(dt.Rows[i]["ClientLongitude"]));
                            var eCoord = new GeoCoordinate(Convert.ToDouble(dt.Rows[i]["Latitude"]), Convert.ToDouble(dt.Rows[i]["Longitude"]));
                            dblKM = sCoord.GetDistanceTo(eCoord);
                            dblKM = dblKM / 1000;
                        }
                        catch { }

                        blnDataExists = true;
                        Vet.Add(new Vet()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            IsVetPractiseUser = Convert.ToInt32(dt.Rows[i]["IsVetPractiseUser"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            VetName = Convert.ToString(dt.Rows[i]["VetName"]),
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
                            SessionRate = Convert.ToDouble(dt.Rows[i]["SessionRate"]),
                            Biography = Convert.ToString(dt.Rows[i]["Biography"]),
                            TotalExp = Convert.ToDouble(dt.Rows[i]["TotalExp"]),
                            UserName = Convert.ToString(dt.Rows[i]["UserName"]),
                            Password = Convert.ToString(dt.Rows[i]["Password"]),
                            ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[i]["ProfilePic"]),
                            BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["BannerPic"])) ? sDefaultBannerPicPath : Convert.ToString(dt.Rows[i]["BannerPic"]),
                            IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"]),
                            IsVerified = Convert.ToInt32(dt.Rows[i]["IsVerified"]),
                            AcTokenId = Convert.ToString(dt.Rows[i]["AcTokenId"]),
                            RegisteredBy = Convert.ToString(dt.Rows[i]["RegisteredBy"]),
                            PaypalAccountId = Convert.ToString(dt.Rows[i]["PaypalAccountId"]),
                            SessionCredit = Convert.ToInt32(dt.Rows[i]["SessionCredit"]),

                            IsVetProfile = Convert.ToInt32(dt.Rows[i]["IsVetProfile"]),
                            IsVetAccount = Convert.ToInt32(dt.Rows[i]["IsVetAccount"]),
                            IsVetEducation = Convert.ToInt32(dt.Rows[i]["IsVetEducation"]),
                            IsVetExperience = Convert.ToInt32(dt.Rows[i]["IsVetExperience"]),
                            IsVetExpertise = Convert.ToInt32(dt.Rows[i]["IsVetExpertise"]),
                            IsVetTimeSlot = Convert.ToInt32(dt.Rows[i]["IsVetTimeSlot"]),
                            IsVetClinic = Convert.ToInt32(dt.Rows[i]["IsVetClinic"]),

                            OnlineStatus = Convert.ToInt32(dt.Rows[i]["OnlineStatus"]),
                            VetExpertise = Convert.ToString(dt.Rows[i]["VetExpertise"]),
                            VetEducation = Convert.ToString(dt.Rows[i]["VetEducation"]),
                            Rating = Convert.ToDouble(dt.Rows[i]["Rating"]),

                            MinSessionRate = Convert.ToDouble(dt.Rows[i]["MinSessionRate"]),
                            MaxSessionRate = Convert.ToDouble(dt.Rows[i]["MaxSessionRate"]),

                            MinDistanceVet = Convert.ToDouble(dt.Rows[i]["MinDistanceVet"]),
                            MaxDistanceVet = Convert.ToDouble(dt.Rows[i]["MaxDistanceVet"]),
                            Distance = Convert.ToDouble(dt.Rows[i]["Distance"]),

                            Latitude = Convert.ToDouble(dt.Rows[i]["Latitude"]),
                            Longitude = Convert.ToDouble(dt.Rows[i]["Longitude"]),
                            Altitude = Convert.ToDouble(dt.Rows[i]["Altitude"]),

                            SortBy = Convert.ToInt32(dt.Rows[i]["SortBy"]),
                            SortType = Convert.ToInt32(dt.Rows[i]["SortType"]),

                            RowNo = Convert.ToInt32(dt.Rows[i]["RowNo"]),
                            TotalRowNo = Convert.ToInt32(dt.Rows[i]["TotalRowNo"]),
                            TotalPage = Convert.ToInt32(dt.Rows[i]["TotalPage"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(Vet);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Update Vet Status
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetStatusUpdate(string AuthKey, Int64 VetId, int OnlineStatus)
        {
            string data = string.Empty;
            var VetStatus = new List<VetStatus>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetStatusChange] @VetId = " + VetId.ToString() + ", @OnlineStatus = " + OnlineStatus.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetStatus.Add(new VetStatus()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            OnlineStatus = Convert.ToInt32(dt.Rows[i]["OnlineStatus"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetStatus);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get Vet Status
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetStatus(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var VetStatus = new List<VetStatus>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetStatusGet] @VetId = " + VetId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetStatus.Add(new VetStatus()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            OnlineStatus = Convert.ToInt32(dt.Rows[i]["OnlineStatus"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetStatus);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Set Vet Location
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SetVetLocation(string AuthKey, Int64 VetId, double Latitude, double Longitude, double Altitude)
        {
            string data = string.Empty;
            var IdentityLocation = new List<IdentityLocation>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetLocation] @VetId = " + VetId.ToString() + ", @Latitude = " + Latitude.ToString() + ", @Longitude = " + Longitude.ToString() + ", @Altitude = " + Altitude.ToString() + ", @OpType = 'I'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        IdentityLocation.Add(new IdentityLocation()
                        {
                            DataId = Convert.ToInt32(VetId),
                            Id = Convert.ToInt32(VetId),
                            Latitude = Latitude,
                            Longitude = Longitude,
                            Altitude = Altitude
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(IdentityLocation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get Vet Location
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetLocation(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var IdentityLocation = new List<IdentityLocation>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetLocation] @VetId = " + VetId.ToString() + ", @OpType = 'S'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        IdentityLocation.Add(new IdentityLocation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[0]["VetId"]),
                            Id = Convert.ToInt32(dt.Rows[0]["VetId"]),
                            Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]),
                            Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]),
                            Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(IdentityLocation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Set Client Location
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SetClientLocation(string AuthKey, Int64 ClientId, double Latitude, double Longitude, double Altitude)
        {
            string data = string.Empty;
            var IdentityLocation = new List<IdentityLocation>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientLocation] @ClientId = " + ClientId.ToString() + ", @Latitude = " + Latitude.ToString() + ", @Longitude = " + Longitude.ToString() + ", @Altitude = " + Altitude.ToString() + ", @OpType = 'I'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        IdentityLocation.Add(new IdentityLocation()
                        {
                            DataId = Convert.ToInt32(ClientId),
                            Id = Convert.ToInt32(ClientId),
                            Latitude = Latitude,
                            Longitude = Longitude,
                            Altitude = Altitude
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(IdentityLocation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get Client Location
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientLocation(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            var IdentityLocation = new List<IdentityLocation>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientLocation] @ClientId = " + ClientId.ToString() + ", @OpType = 'S'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        IdentityLocation.Add(new IdentityLocation()
                        {
                            Id = Convert.ToInt32(dt.Rows[0]["ClientId"]),
                            DataId = Convert.ToInt32(dt.Rows[0]["ClientId"]),
                            Latitude = Convert.ToDouble(dt.Rows[0]["Latitude"]),
                            Longitude = Convert.ToDouble(dt.Rows[0]["Longitude"]),
                            Altitude = Convert.ToDouble(dt.Rows[0]["Altitude"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(IdentityLocation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - Get All Information
        #region Get Vet All Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetInfoAll(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var Vet = new List<Vet>();
            var VetEducation = new List<VetEducation>();
            var VetExperience = new List<VetExperience>();
            var VetExpertise = new List<VetExpertise>();
            var VetClinic = new List<VetClinic>();

            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sDefaultPicsPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                    string sDefaultBannerPicPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataSet dt_Vet_Info = objDBHelper.FillDataset("Exec [uspVetInfo] @VetId = " + VetId.ToString());

                    DataTable dt_Vet = dt_Vet_Info.Tables[0];
                    for (int i = 0; i < dt_Vet.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        Vet.Add(new Vet()
                        {
                            DataId = Convert.ToInt32(dt_Vet.Rows[i]["VetId"]),
                            IsVetPractiseUser = Convert.ToInt32(dt_Vet.Rows[i]["IsVetPractiseUser"]),
                            VetId = Convert.ToInt32(dt_Vet.Rows[i]["VetId"]),
                            VetName = Convert.ToString(dt_Vet.Rows[i]["VetName"]),
                            Address1 = Convert.ToString(dt_Vet.Rows[i]["Address1"]),
                            Address2 = Convert.ToString(dt_Vet.Rows[i]["Address2"]),
                            CityId = Convert.ToInt64(dt_Vet.Rows[i]["City"]),
                            City = Convert.ToString(dt_Vet.Rows[i]["CityName"]),
                            POBox = Convert.ToString(dt_Vet.Rows[i]["POBox"]),
                            StateId = Convert.ToInt64(dt_Vet.Rows[i]["State"]),
                            State = Convert.ToString(dt_Vet.Rows[i]["StateName"]),
                            CountryId = Convert.ToInt64(dt_Vet.Rows[i]["Country"]),
                            Country = Convert.ToString(dt_Vet.Rows[i]["CountryName"]),

                            PhoneNo = Convert.ToString(dt_Vet.Rows[i]["PhoneNo"]),
                            EmailId = Convert.ToString(dt_Vet.Rows[i]["EmailId"]),
                            SessionRate = Convert.ToDouble(dt_Vet.Rows[i]["SessionRate"]),
                            Biography = Convert.ToString(dt_Vet.Rows[i]["Biography"]),
                            TotalExp = Convert.ToDouble(dt_Vet.Rows[i]["TotalExp"]),

                            UserName = Convert.ToString(dt_Vet.Rows[i]["UserName"]),
                            Password = Convert.ToString(dt_Vet.Rows[i]["Password"]),
                            ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt_Vet.Rows[i]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt_Vet.Rows[i]["ProfilePic"]),
                            BannerPic = string.IsNullOrEmpty(Convert.ToString(dt_Vet.Rows[i]["BannerPic"])) ? sDefaultBannerPicPath : Convert.ToString(dt_Vet.Rows[i]["BannerPic"]),

                            IsActive = Convert.ToInt32(dt_Vet.Rows[i]["IsActive"]),
                            CreatedOn = Convert.ToString(dt_Vet.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt_Vet.Rows[i]["EndDate"]),
                            IsVerified = Convert.ToInt32(dt_Vet.Rows[i]["IsVerified"]),
                            AcTokenId = Convert.ToString(dt_Vet.Rows[i]["AcTokenId"]),
                            RegisteredBy = Convert.ToString(dt_Vet.Rows[i]["RegisteredBy"]),
                            PaypalAccountId = Convert.ToString(dt_Vet.Rows[i]["PaypalAccountId"]),
                            SessionCredit = Convert.ToInt32(dt_Vet.Rows[i]["SessionCredit"]),

                            IsVetProfile = Convert.ToInt32(dt_Vet.Rows[i]["IsVetProfile"]),
                            IsVetAccount = Convert.ToInt32(dt_Vet.Rows[i]["IsVetAccount"]),
                            IsVetEducation = Convert.ToInt32(dt_Vet.Rows[i]["IsVetEducation"]),
                            IsVetExperience = Convert.ToInt32(dt_Vet.Rows[i]["IsVetExperience"]),
                            IsVetExpertise = Convert.ToInt32(dt_Vet.Rows[i]["IsVetExpertise"]),
                            IsVetTimeSlot = Convert.ToInt32(dt_Vet.Rows[i]["IsVetTimeSlot"]),
                            IsVetClinic = Convert.ToInt32(dt_Vet.Rows[i]["IsVetClinic"]),

                            OnlineStatus = Convert.ToInt32(dt_Vet.Rows[i]["OnlineStatus"]),
                            VetExpertise = Convert.ToString(dt_Vet.Rows[i]["VetExpertise"]),
                            VetEducation = Convert.ToString(dt_Vet.Rows[i]["VetEducation"]),
                            Rating = Convert.ToDouble(dt_Vet.Rows[i]["Rating"]),

                            Latitude = Convert.ToDouble(dt_Vet.Rows[i]["Latitude"]),
                            Longitude = Convert.ToDouble(dt_Vet.Rows[i]["Longitude"]),
                            Altitude = Convert.ToDouble(dt_Vet.Rows[i]["Altitude"]),

                            SortBy = 0,
                            SortType = 0
                        });
                    }

                    DataTable dt_Edu = dt_Vet_Info.Tables[1];
                    for (int i = 0; i < dt_Edu.Rows.Count; i++)
                    {
                        VetEducation.Add(new VetEducation()
                        {
                            DataId = Convert.ToInt32(dt_Edu.Rows[i]["VetEducationId"]),
                            VetEducationId = Convert.ToInt32(dt_Edu.Rows[i]["VetEducationId"]),
                            VetId = Convert.ToInt32(dt_Edu.Rows[i]["VetId"]),
                            DegreeId = Convert.ToInt32(dt_Edu.Rows[i]["DegreeId"]),
                            DegreeName = Convert.ToString(dt_Edu.Rows[i]["DegreeName"]),
                            UniversityId = Convert.ToInt32(dt_Edu.Rows[i]["UniversityId"]),
                            UniversityName = Convert.ToString(dt_Edu.Rows[i]["UniversityName"]),
                            PassingYear = Convert.ToString(dt_Edu.Rows[i]["PassingYear"]),
                            Description = Convert.ToString(dt_Edu.Rows[i]["Description"]),
                            CreatedOn = Convert.ToString(dt_Edu.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt_Edu.Rows[i]["EndDate"])
                        });
                    }

                    DataTable dt_Experience = dt_Vet_Info.Tables[2];
                    for (int i = 0; i < dt_Experience.Rows.Count; i++)
                    {
                        VetExperience.Add(new VetExperience()
                        {
                            DataId = Convert.ToInt32(dt_Experience.Rows[i]["VetExperienceId"]),
                            VetExperienceId = Convert.ToInt32(dt_Experience.Rows[i]["VetExperienceId"]),
                            VetId = Convert.ToInt32(dt_Experience.Rows[i]["VetId"]),
                            Title = Convert.ToString(dt_Experience.Rows[i]["Title"]),
                            FromDate = Convert.ToString(dt_Experience.Rows[i]["FromDate"]),
                            ToDate = Convert.ToString(dt_Experience.Rows[i]["ToDate"]),
                            Description = Convert.ToString(dt_Experience.Rows[i]["Description"]),
                            CreatedOn = Convert.ToString(dt_Experience.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt_Experience.Rows[i]["EndDate"])
                        });
                    }

                    DataTable dt_Expertise = dt_Vet_Info.Tables[3];
                    for (int i = 0; i < dt_Expertise.Rows.Count; i++)
                    {
                        VetExpertise.Add(new VetExpertise()
                        {
                            DataId = Convert.ToInt32(dt_Expertise.Rows[i]["VetExpertiseId"]),
                            VetExpertiseId = Convert.ToInt32(dt_Expertise.Rows[i]["VetExpertiseId"]),
                            VetId = Convert.ToInt32(dt_Expertise.Rows[i]["VetId"]),
                            PetTypeId = Convert.ToInt32(dt_Expertise.Rows[i]["PetTypeId"]),
                            PetTypeName = Convert.ToString(dt_Expertise.Rows[i]["PetTypeName"]),
                            Proficiency = Convert.ToInt32(dt_Expertise.Rows[i]["Proficiency"]),
                            CreatedOn = Convert.ToString(dt_Expertise.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt_Expertise.Rows[i]["EndDate"])
                        });
                    }

                    DataTable dt_Clinic = dt_Vet_Info.Tables[4];
                    for (int i = 0; i < dt_Clinic.Rows.Count; i++)
                    {
                        VetClinic.Add(new VetClinic()
                        {
                            DataId = Convert.ToInt32(dt_Clinic.Rows[i]["VetClinicId"]),
                            VetClinicId = Convert.ToInt32(dt_Clinic.Rows[i]["VetClinicId"]),
                            VetId = Convert.ToInt32(dt_Clinic.Rows[i]["VetId"]),
                            ClinicName = Convert.ToString(dt_Clinic.Rows[i]["ClinicName"]),
                            Address1 = Convert.ToString(dt_Clinic.Rows[i]["Address1"]),
                            Address2 = Convert.ToString(dt_Clinic.Rows[i]["Address2"]),
                            CityId = Convert.ToInt64(dt_Clinic.Rows[i]["City"]),
                            City = Convert.ToString(dt_Clinic.Rows[i]["CityName"]),
                            POBox = Convert.ToString(dt_Clinic.Rows[i]["POBox"]),
                            StateId = Convert.ToInt64(dt_Clinic.Rows[i]["State"]),
                            State = Convert.ToString(dt_Clinic.Rows[i]["StateName"]),
                            CountryId = Convert.ToInt64(dt_Clinic.Rows[i]["Country"]),
                            Country = Convert.ToString(dt_Clinic.Rows[i]["CountryName"]),
                            PhoneNo = Convert.ToString(dt_Clinic.Rows[i]["PhoneNo"]),
                            EmailId = Convert.ToString(dt_Clinic.Rows[i]["EmailId"]),
                            CreatedOn = Convert.ToString(dt_Clinic.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt_Clinic.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    dynamic colVet = new { VetProfile = Vet };
                    data = serializer.Serialize(colVet);

                    dynamic colVetEdu = new { VetEducation = VetEducation };
                    data = data + "," + serializer.Serialize(colVetEdu);

                    dynamic colVetExperience = new { VetExperience = VetExperience };
                    data = data + "," + serializer.Serialize(colVetExperience);

                    dynamic colVetExpertise = new { VetExpertise = VetExpertise };
                    data = data + "," + serializer.Serialize(colVetExpertise);

                    dynamic colVetClinic = new { VetClinic = VetClinic };
                    data = data + "," + serializer.Serialize(colVetClinic);
                    data = "[" + data + "]";

                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - Account Function
        #region Vet-Account Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetAccountInsert(string AuthKey, Int64 VetId, string PaypalAccountId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetAccount] @VetId = " + VetId.ToString() + ", @PaypalAccountId = '" + PaypalAccountId.ToString() + "', @OpType = 'I'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetAccountId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetAccount Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetAccountInfo(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var VetAccount = new List<VetAccount>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetAccount] @OpType = 'Get', @VetId = " + VetId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetAccount.Add(new VetAccount()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetAccountId"]),
                            VetAccountId = Convert.ToInt32(dt.Rows[i]["VetAccountId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            PaypalAccountId = Convert.ToString(dt.Rows[i]["PaypalAccountId"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetAccount);
                    Context.Response.Write(data);
                }
                else
                {
                    VetAccount.Add(new VetAccount()
                    {
                        VetAccountId = 0,
                        VetId = VetId,
                        PaypalAccountId = string.Empty
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - Education Function
        #region Vet-Education Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetEducationInsert(string AuthKey, Int64 VetId, Int64 DegreeId, Int64 UniversityId, string PassingYear, string Description)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetEducation] @VetId = " + VetId.ToString() + ", @DegreeId = " + DegreeId.ToString() + ", @UniversityId = " + UniversityId.ToString() + ", @PassingYear = '" + PassingYear.Replace("'", "''") + "', @Description = '" + Description.Replace("'", "''") + "', @OpType = 'I'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetEducationId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Education Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetEducationUpdate(string AuthKey, Int64 VetEducationId, Int64 DegreeId, Int64 UniversityId, string PassingYear, string Description)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetEducation] @VetEducationId = " + VetEducationId.ToString() + ", @DegreeId = " + DegreeId.ToString() + ", @UniversityId = " + UniversityId.ToString() + ", @PassingYear = '" + PassingYear.Replace("'", "''") + "', @Description = '" + Description.Replace("'", "''") + "', @OpType = 'U'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetEducationId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Education Delete
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetEducationDelete(string AuthKey, Int64 VetEducationId)
        {
            string data = string.Empty;
            var VetEducation = new List<VetEducation>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetEducation] @VetEducationId = " + VetEducationId.ToString() + ", @OpType = 'D'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        DataTable dtVetEducation = objDBHelper.FillTable("Exec [uspVetEducation] @OpType = 'Get', @VetId = " + Convert.ToInt32(dt.Rows[i]["VetId"]));
                        for (int k = 0; k < dtVetEducation.Rows.Count; k++)
                        {
                            blnDataExists = true;
                            VetEducation.Add(new VetEducation()
                            {
                                DataId = Convert.ToInt32(dtVetEducation.Rows[k]["VetEducationId"]),
                                VetEducationId = Convert.ToInt32(dtVetEducation.Rows[k]["VetEducationId"]),
                                VetId = Convert.ToInt32(dtVetEducation.Rows[k]["VetId"]),
                                DegreeId = Convert.ToInt32(dtVetEducation.Rows[k]["DegreeId"]),
                                DegreeName = Convert.ToString(dtVetEducation.Rows[k]["DegreeName"]),
                                UniversityId = Convert.ToInt32(dtVetEducation.Rows[k]["UniversityId"]),
                                UniversityName = Convert.ToString(dtVetEducation.Rows[k]["UniversityName"]),
                                PassingYear = Convert.ToString(dtVetEducation.Rows[k]["PassingYear"]),
                                Description = Convert.ToString(dtVetEducation.Rows[k]["Description"]),
                                CreatedOn = Convert.ToString(dtVetEducation.Rows[k]["CreatedOn"]),
                                EndDate = Convert.ToString(dtVetEducation.Rows[k]["EndDate"])
                            });
                        }
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetEducation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetEducation Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetEducationInfo(string AuthKey, Int64 VetEducationId)
        {
            string data = string.Empty;
            var VetEducation = new List<VetEducation>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetEducation] @OpType = 'S', @VetEducationId = " + VetEducationId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetEducation.Add(new VetEducation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetEducationId"]),
                            VetEducationId = Convert.ToInt32(dt.Rows[i]["VetEducationId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            DegreeId = Convert.ToInt32(dt.Rows[i]["DegreeId"]),
                            DegreeName = Convert.ToString(dt.Rows[i]["DegreeName"]),
                            UniversityId = Convert.ToInt32(dt.Rows[i]["UniversityId"]),
                            UniversityName = Convert.ToString(dt.Rows[i]["UniversityName"]),
                            PassingYear = Convert.ToString(dt.Rows[i]["PassingYear"]),
                            Description = Convert.ToString(dt.Rows[i]["Description"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetEducation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetEducation Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetEducationInfoAll(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var VetEducation = new List<VetEducation>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetEducation] @OpType = 'Get', @VetId = " + VetId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetEducation.Add(new VetEducation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetEducationId"]),
                            VetEducationId = Convert.ToInt32(dt.Rows[i]["VetEducationId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            DegreeId = Convert.ToInt32(dt.Rows[i]["DegreeId"]),
                            DegreeName = Convert.ToString(dt.Rows[i]["DegreeName"]),
                            UniversityId = Convert.ToInt32(dt.Rows[i]["UniversityId"]),
                            UniversityName = Convert.ToString(dt.Rows[i]["UniversityName"]),
                            PassingYear = Convert.ToString(dt.Rows[i]["PassingYear"]),
                            Description = Convert.ToString(dt.Rows[i]["Description"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetEducation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - Experience Function
        #region Vet-Experience Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetExperienceInsert(string AuthKey, Int64 VetId, string Title, string FromDate, string ToDate, string Description)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetExperience] @VetId = " + VetId.ToString() + ", @Title = '" + Title.Replace("'", "''") + "', @FromDate = '" + FromDate + "', @ToDate = '" + ToDate + "', @Description = '" + Description.Replace("'", "''") + "', @OpType = 'I'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetExperienceId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Experience Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetExperienceUpdate(string AuthKey, Int64 VetExperienceId, string Title, string FromDate, string ToDate, string Description)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetExperience] @VetExperienceId = " + VetExperienceId.ToString() + ", @Title = '" + Title.Replace("'", "''") + "', @FromDate = '" + FromDate + "', @ToDate = '" + ToDate + "', @Description = '" + Description.Replace("'", "''") + "', @OpType = 'U'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetExperienceId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Experience Delete
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetExperienceDelete(string AuthKey, Int64 VetExperienceId)
        {
            string data = string.Empty;
            var VetExperience = new List<VetExperience>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetExperience] @VetExperienceId = " + VetExperienceId.ToString() + ", @OpType = 'D'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        DataTable dt_VetExperience = objDBHelper.FillTable("Exec [uspVetExperience] @OpType = 'Get', @VetId = " + Convert.ToInt32(dt.Rows[i]["VetId"]));
                        for (int k = 0; k < dt_VetExperience.Rows.Count; k++)
                        {
                            blnDataExists = true;
                            VetExperience.Add(new VetExperience()
                            {
                                DataId = Convert.ToInt32(dt_VetExperience.Rows[k]["VetExperienceId"]),
                                VetExperienceId = Convert.ToInt32(dt_VetExperience.Rows[k]["VetExperienceId"]),
                                VetId = Convert.ToInt32(dt_VetExperience.Rows[k]["VetId"]),
                                Title = Convert.ToString(dt_VetExperience.Rows[k]["Title"]),
                                FromDate = Convert.ToString(dt_VetExperience.Rows[k]["FromDate"]),
                                ToDate = Convert.ToString(dt_VetExperience.Rows[k]["ToDate"]),
                                Description = Convert.ToString(dt_VetExperience.Rows[k]["Description"]),
                                CreatedOn = Convert.ToString(dt_VetExperience.Rows[k]["CreatedOn"]),
                                EndDate = Convert.ToString(dt_VetExperience.Rows[k]["EndDate"])
                            });
                        }
                    }

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    if (blnDataExists)
                    {
                        data = serializer.Serialize(VetExperience);
                        Context.Response.Write(data);
                    }
                    else
                    {
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(0),
                            Error = "No Record found.",
                            ErrorNumber = 999
                        });
                        data = serializer.Serialize(DataConfirmation);
                        Context.Response.Write(data);
                    }
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetExperience Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetExperienceInfo(string AuthKey, Int64 VetExperienceId)
        {
            string data = string.Empty;
            var VetExperience = new List<VetExperience>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetExperience] @OpType = 'S', @VetExperienceId = " + VetExperienceId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetExperience.Add(new VetExperience()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetExperienceId"]),
                            VetExperienceId = Convert.ToInt32(dt.Rows[i]["VetExperienceId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            Title = Convert.ToString(dt.Rows[i]["Title"]),
                            FromDate = Convert.ToString(dt.Rows[i]["FromDate"]),
                            ToDate = Convert.ToString(dt.Rows[i]["ToDate"]),
                            Description = Convert.ToString(dt.Rows[i]["Description"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetExperience);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetExperience Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetExperienceInfoAll(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var VetExperience = new List<VetExperience>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetExperience] @OpType = 'Get', @VetId = " + VetId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetExperience.Add(new VetExperience()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetExperienceId"]),
                            VetExperienceId = Convert.ToInt32(dt.Rows[i]["VetExperienceId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            Title = Convert.ToString(dt.Rows[i]["Title"]),
                            FromDate = Convert.ToString(dt.Rows[i]["FromDate"]),
                            ToDate = Convert.ToString(dt.Rows[i]["ToDate"]),
                            Description = Convert.ToString(dt.Rows[i]["Description"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetExperience);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - Expertise Function
        #region Vet-Expertise Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetExpertiseInsert(string AuthKey, Int64 VetId, Int64 PetTypeId, string Proficiency)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetExpertise] @VetId = " + VetId.ToString() + ", @PetTypeId = " + PetTypeId.ToString() + ", @Proficiency = " + Proficiency + ", @OpType = 'I'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetExpertiseId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Expertise Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetExpertiseUpdate(string AuthKey, Int64 VetExpertiseId, Int64 PetTypeId, string Proficiency)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetExpertise] @VetExpertiseId = " + VetExpertiseId.ToString() + ", @PetTypeId = " + PetTypeId.ToString() + ", @Proficiency = " + Proficiency + ", @OpType = 'U'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetExpertiseId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Expertise Delete
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetExpertiseDelete(string AuthKey, Int64 VetExpertiseId)
        {
            string data = string.Empty;
            var VetExpertise = new List<VetExpertise>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetExpertise] @VetExpertiseId = " + VetExpertiseId.ToString() + ", @OpType = 'D'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        DataTable dt_VetExpertise = objDBHelper.FillTable("Exec [uspVetExpertise] @OpType = 'Get', @VetId = " + Convert.ToInt32(dt.Rows[i]["VetId"]));
                        for (int k = 0; k < dt_VetExpertise.Rows.Count; k++)
                        {
                            blnDataExists = true;
                            VetExpertise.Add(new VetExpertise()
                            {
                                DataId = Convert.ToInt32(dt_VetExpertise.Rows[k]["VetExpertiseId"]),
                                VetExpertiseId = Convert.ToInt32(dt_VetExpertise.Rows[k]["VetExpertiseId"]),
                                VetId = Convert.ToInt32(dt_VetExpertise.Rows[k]["VetId"]),
                                PetTypeId = Convert.ToInt32(dt_VetExpertise.Rows[k]["PetTypeId"]),
                                PetTypeName = Convert.ToString(dt_VetExpertise.Rows[k]["PetTypeName"]),
                                Proficiency = Convert.ToInt32(dt_VetExpertise.Rows[k]["Proficiency"]),
                                CreatedOn = Convert.ToString(dt_VetExpertise.Rows[k]["CreatedOn"]),
                                EndDate = Convert.ToString(dt_VetExpertise.Rows[k]["EndDate"])
                            });
                        }
                    }

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    if (blnDataExists)
                    {
                        data = serializer.Serialize(VetExpertise);
                        Context.Response.Write(data);
                    }
                    else
                    {
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(0),
                            Error = "No Record found.",
                            ErrorNumber = 999
                        });
                        data = serializer.Serialize(DataConfirmation);
                        Context.Response.Write(data);
                    }
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetExpertise Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetExpertiseInfo(string AuthKey, Int64 VetExpertiseId)
        {
            string data = string.Empty;
            var VetExpertise = new List<VetExpertise>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetExpertise] @OpType = 'S', @VetExpertiseId = " + VetExpertiseId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetExpertise.Add(new VetExpertise()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetExpertiseId"]),
                            VetExpertiseId = Convert.ToInt32(dt.Rows[i]["VetExpertiseId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            PetTypeId = Convert.ToInt32(dt.Rows[i]["PetTypeId"]),
                            PetTypeName = Convert.ToString(dt.Rows[i]["PetTypeName"]),
                            Proficiency = Convert.ToInt32(dt.Rows[i]["Proficiency"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetExpertise);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetExpertise Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetExpertiseInfoAll(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var VetExpertise = new List<VetExpertise>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetExpertise] @OpType = 'Get', @VetId = " + VetId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetExpertise.Add(new VetExpertise()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetExpertiseId"]),
                            VetExpertiseId = Convert.ToInt32(dt.Rows[i]["VetExpertiseId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            PetTypeId = Convert.ToInt32(dt.Rows[i]["PetTypeId"]),
                            PetTypeName = Convert.ToString(dt.Rows[i]["PetTypeName"]),
                            Proficiency = Convert.ToInt32(dt.Rows[i]["Proficiency"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetExpertise);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - Practise Function
        #region Vet-Practise Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetPractiseInsert(string AuthKey, Int64 VetId, Int64 VetPractiseId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    string sQry = @"Exec [uspVetPractise] @VetId = " + VetId.ToString() + ", @VetPractiseId = " + VetPractiseId.ToString() + ", @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetPractiseId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Practise Delete
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetPractiseDelete(string AuthKey, Int64 VetId, Int64 VetPractiseId)
        {
            string data = string.Empty;
            var VetPractise = new List<VetPractise>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetPractise] @VetId = " + VetId.ToString() + ", @VetPractiseId = " + VetPractiseId.ToString() + ", @OpType = 'D'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        DataTable dt_VetPractise = objDBHelper.FillTable("Exec [uspVetPractise] @OpType = 'Get', @VetId = " + Convert.ToInt32(dt.Rows[i]["VetId"]));
                        for (int k = 0; k < dt_VetPractise.Rows.Count; k++)
                        {
                            blnDataExists = true;
                            VetPractise.Add(new VetPractise()
                            {
                                DataId = Convert.ToInt32(dt_VetPractise.Rows[k]["VetPractiseId"]),
                                VetPractiseId = Convert.ToInt32(dt_VetPractise.Rows[k]["VetPractiseId"]),
                                VetId = Convert.ToInt32(dt_VetPractise.Rows[k]["VetId"]),
                                VetPractiseName = Convert.ToString(dt_VetPractise.Rows[k]["VetPractiseName"]),
                                VetName = Convert.ToString(dt_VetPractise.Rows[k]["VetName"])
                            });
                        }
                    }

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    if (blnDataExists)
                    {
                        data = serializer.Serialize(VetPractise);
                        Context.Response.Write(data);
                    }
                    else
                    {
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(0),
                            Error = "No Record found.",
                            ErrorNumber = 999
                        });
                        data = serializer.Serialize(DataConfirmation);
                        Context.Response.Write(data);
                    }
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetPractise Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetPractiseInfoAll(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var VetPractise = new List<VetPractise>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetPractise] @OpType = 'S', @VetId = " + VetId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetPractise.Add(new VetPractise()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetPractiseId"]),
                            VetPractiseId = Convert.ToInt32(dt.Rows[i]["VetPractiseId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            VetPractiseName = Convert.ToString(dt.Rows[i]["VetPractiseName"]),
                            VetName = Convert.ToString(dt.Rows[i]["VetName"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetPractise);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - Clinic Function
        #region Vet-Clinic Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetClinicInsert(string AuthKey, Int64 VetId, string ClinicName, string Address1, string Address2, string City, string POBox, string State, string Country, string PhoneNo, string EmailId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    string sQry = @"Exec [uspVetClinic] 
                                    @VetId = " + VetId.ToString() + ", @ClinicName = '" + ClinicName.Replace("'", "''") + "', @Address1 = '" + Address1.Replace("'", "''") + "', @Address2 = '" + Address2.Replace("'", "''") + @"', 
                                    @City = '" + City.Replace("'", "''") + "', @POBox = '" + POBox.Replace("'", "''") + "', @State = '" + State.Replace("'", "''") + "', @Country = '" + Country.Replace("'", "''") + @"', 
                                    @PhoneNo = '" + PhoneNo.Replace("'", "''") + "', @EmailId = '" + EmailId.Replace("'", "''") + "', @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetClinicId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Clinic Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetClinicUpdate(string AuthKey, Int64 VetClinicId, string ClinicName, string Address1, string Address2, string City, string POBox, string State, string Country, string PhoneNo, string EmailId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    string sQry = @"Exec [uspVetClinic] 
                                    @VetClinicId = " + VetClinicId.ToString() + ", @ClinicName = '" + ClinicName.Replace("'", "''") + "', @Address1 = '" + Address1.Replace("'", "''") + "', @Address2 = '" + Address2.Replace("'", "''") + @"', 
                                    @City = '" + City.Replace("'", "''") + "', @POBox = '" + POBox.Replace("'", "''") + "', @State = '" + State.Replace("'", "''") + "', @Country = '" + Country.Replace("'", "''") + @"', 
                                    @PhoneNo = '" + PhoneNo.Replace("'", "''") + "', @EmailId = '" + EmailId.Replace("'", "''") + "', @OpType = 'U'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetClinicId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Clinic Delete
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetClinicDelete(string AuthKey, Int64 VetClinicId)
        {
            string data = string.Empty;
            var VetClinic = new List<VetClinic>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetClinic] @VetClinicId = " + VetClinicId.ToString() + ", @OpType = 'D'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        DataTable dt_VetClinic = objDBHelper.FillTable("Exec [uspVetClinic] @OpType = 'Get', @VetId = " + Convert.ToInt32(dt.Rows[i]["VetId"]));
                        for (int k = 0; k < dt_VetClinic.Rows.Count; k++)
                        {
                            blnDataExists = true;
                            VetClinic.Add(new VetClinic()
                            {
                                DataId = Convert.ToInt32(dt_VetClinic.Rows[k]["VetClinicId"]),
                                VetClinicId = Convert.ToInt32(dt_VetClinic.Rows[k]["VetClinicId"]),
                                VetId = Convert.ToInt32(dt_VetClinic.Rows[k]["VetId"]),
                                ClinicName = Convert.ToString(dt_VetClinic.Rows[k]["ClinicName"]),
                                Address1 = Convert.ToString(dt_VetClinic.Rows[k]["Address1"]),
                                Address2 = Convert.ToString(dt_VetClinic.Rows[k]["Address2"]),
                                CityId = Convert.ToInt64(dt_VetClinic.Rows[k]["City"]),
                                City = Convert.ToString(dt_VetClinic.Rows[k]["CityName"]),
                                POBox = Convert.ToString(dt_VetClinic.Rows[k]["POBox"]),
                                StateId = Convert.ToInt64(dt_VetClinic.Rows[k]["State"]),
                                State = Convert.ToString(dt_VetClinic.Rows[k]["StateName"]),
                                CountryId = Convert.ToInt64(dt_VetClinic.Rows[k]["Country"]),
                                Country = Convert.ToString(dt_VetClinic.Rows[k]["CountryName"]),
                                PhoneNo = Convert.ToString(dt_VetClinic.Rows[k]["PhoneNo"]),
                                EmailId = Convert.ToString(dt_VetClinic.Rows[k]["EmailId"]),
                                CreatedOn = Convert.ToString(dt_VetClinic.Rows[k]["CreatedOn"]),
                                EndDate = Convert.ToString(dt_VetClinic.Rows[k]["EndDate"])
                            });
                        }
                    }

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    if (blnDataExists)
                    {
                        data = serializer.Serialize(VetClinic);
                        Context.Response.Write(data);
                    }
                    else
                    {
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(0),
                            Error = "No Record found.",
                            ErrorNumber = 999
                        });
                        data = serializer.Serialize(DataConfirmation);
                        Context.Response.Write(data);
                    }
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetClinic Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetClinicInfo(string AuthKey, Int64 VetClinicId)
        {
            string data = string.Empty;
            var VetClinic = new List<VetClinic>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetClinic] @OpType = 'S', @VetClinicId = " + VetClinicId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetClinic.Add(new VetClinic()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetClinicId"]),
                            VetClinicId = Convert.ToInt32(dt.Rows[i]["VetClinicId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            ClinicName = Convert.ToString(dt.Rows[i]["ClinicName"]),
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
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetClinic);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetClinic Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetClinicInfoAll(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var VetClinic = new List<VetClinic>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetClinic] @OpType = 'Get', @VetId = " + VetId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetClinic.Add(new VetClinic()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetClinicId"]),
                            VetClinicId = Convert.ToInt32(dt.Rows[i]["VetClinicId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            ClinicName = Convert.ToString(dt.Rows[i]["ClinicName"]),
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
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetClinic);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - ClinicTiming Function
        #region Vet-ClinicTiming Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetClinicTimingInsert(string AuthKey, Int64 VetClinicId, string DayNo, string FromTime1, string ToTime1, string FromTime2, string ToTime2, string IsClosed)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    string sQry = @"Exec [uspVetClinicTiming] 
                                    @VetClinicId = " + VetClinicId.ToString() + ", @DayNo = " + DayNo + ", @FromTime1 = '" + FromTime1.Replace("'", "''") + "', @ToTime1 = '" + ToTime1.Replace("'", "''") + @"', 
                                    @FromTime2 = '" + FromTime2.Replace("'", "''") + "', @ToTime2 = '" + ToTime2.Replace("'", "''") + "', @IsClosed = " + IsClosed + ", @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetClinicTimingId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-ClinicTiming Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetClinicTimingUpdate(string AuthKey, Int64 VetClinicTimingId, string DayNo, string FromTime1, string ToTime1, string FromTime2, string ToTime2, string IsClosed, string Country, string PhoneNo, string EmailId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    string sQry = @"Exec [uspVetClinicTiming] 
                                    @VetClinicTimingId = " + VetClinicTimingId.ToString() + ", @DayNo = " + DayNo + ", @FromTime1 = '" + FromTime1.Replace("'", "''") + "', @ToTime1 = '" + ToTime1.Replace("'", "''") + @"', 
                                    @FromTime2 = '" + FromTime2.Replace("'", "''") + "', @ToTime2 = '" + ToTime2.Replace("'", "''") + "', @IsClosed = " + IsClosed + ", @OpType = 'U'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetClinicTimingId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-ClinicTiming Delete
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetClinicTimingDelete(string AuthKey, Int64 VetClinicTimingId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetClinicTiming] @VetClinicTimingId = " + VetClinicTimingId.ToString() + ", @OpType = 'D'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetClinicTimingId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetClinicTiming Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetClinicTimingInfo(string AuthKey, Int64 VetClinicTimingId)
        {
            string data = string.Empty;
            var VetClinicTiming = new List<VetClinicTiming>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetClinicTiming] @OpType = 'S', @VetClinicTimingId = " + VetClinicTimingId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetClinicTiming.Add(new VetClinicTiming()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetClinicTimingId"]),
                            VetClinicTimingId = Convert.ToInt32(dt.Rows[i]["VetClinicTimingId"]),
                            VetClinicId = Convert.ToInt32(dt.Rows[i]["VetClinicId"]),
                            DayNo = Convert.ToInt32(dt.Rows[i]["DayNo"]),
                            FromTime1 = Convert.ToString(dt.Rows[i]["FromTime1"]),
                            ToTime1 = Convert.ToString(dt.Rows[i]["ToTime1"]),
                            FromTime2 = Convert.ToString(dt.Rows[i]["FromTime2"]),
                            ToTime2 = Convert.ToString(dt.Rows[i]["ToTime2"]),
                            IsClosed = Convert.ToInt32(dt.Rows[i]["IsClosed"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetClinicTiming);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetClinicTiming Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetClinicTimingInfoAll(string AuthKey, Int64 VetClinicId)
        {
            string data = string.Empty;
            var VetClinicTiming = new List<VetClinicTiming>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetClinicTiming] @OpType = 'Get', @VetClinicId = " + VetClinicId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetClinicTiming.Add(new VetClinicTiming()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetClinicTimingId"]),
                            VetClinicTimingId = Convert.ToInt32(dt.Rows[i]["VetClinicTimingId"]),
                            VetClinicId = Convert.ToInt32(dt.Rows[i]["VetClinicId"]),
                            DayNo = Convert.ToInt32(dt.Rows[i]["DayNo"]),
                            FromTime1 = Convert.ToString(dt.Rows[i]["FromTime1"]),
                            ToTime1 = Convert.ToString(dt.Rows[i]["ToTime1"]),
                            FromTime2 = Convert.ToString(dt.Rows[i]["FromTime2"]),
                            ToTime2 = Convert.ToString(dt.Rows[i]["ToTime2"]),
                            IsClosed = Convert.ToInt32(dt.Rows[i]["IsClosed"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetClinicTiming);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - TimeSlot Function
        #region Vet-TimeSlot Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetTimeSlotInsert(string AuthKey, Int64 VetId, Int64 TimeSlotId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetTimeSlot] @VetId = " + VetId.ToString() + ", @TimeSlotId = " + TimeSlotId.ToString() + ", @OpType = 'I'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetTimeSlotId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";
                else if (sMessage.IndexOf("Timeslot already selected") > 0)
                    sMessage = "Timeslot already selected";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-TimeSlot Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetTimeSlotUpdate(string AuthKey, Int64 VetTimeSlotId, Int64 TimeSlotId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetTimeSlot] @VetTimeSlotId = " + VetTimeSlotId.ToString() + ", @TimeSlotId = " + TimeSlotId.ToString() + ", @OpType = 'U'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetTimeSlotId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("FK_tblVetTimeSlot_tblTimeSlot") > 0)
                    sMessage = "Could not delete. Reference Exists for Vet Time Slot.";
                else if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-TimeSlot Delete
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetTimeSlotDelete(string AuthKey, Int64 VetTimeSlotId)
        {
            string data = string.Empty;
            var VetTimeSlot = new List<VetTimeSlot>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetTimeSlot] @VetTimeSlotId = " + VetTimeSlotId.ToString() + ", @OpType = 'D'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        DataTable dt_VetTimeSlot = objDBHelper.FillTable("Exec [uspVetTimeSlot] @OpType = 'Get', @VetId = " + Convert.ToInt32(dt.Rows[i]["VetId"]));
                        for (int k = 0; k < dt_VetTimeSlot.Rows.Count; k++)
                        {
                            blnDataExists = true;
                            VetTimeSlot.Add(new VetTimeSlot()
                            {
                                DataId = Convert.ToInt32(dt.Rows[i]["VetTimeSlotId"]),
                                VetTimeSlotId = Convert.ToInt32(dt_VetTimeSlot.Rows[k]["VetTimeSlotId"]),
                                VetId = Convert.ToInt32(dt_VetTimeSlot.Rows[k]["VetId"]),
                                TimeSlotId = Convert.ToInt32(dt_VetTimeSlot.Rows[k]["TimeSlotId"]),
                                TimeSlotName = Convert.ToString(dt_VetTimeSlot.Rows[k]["TimeSlotName"]),
                                CreatedOn = Convert.ToString(dt_VetTimeSlot.Rows[k]["CreatedOn"]),
                                EndDate = Convert.ToString(dt_VetTimeSlot.Rows[k]["EndDate"])
                            });
                        }
                    }

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    if (blnDataExists)
                    {
                        data = serializer.Serialize(VetTimeSlot);
                        Context.Response.Write(data);
                    }
                    else
                    {
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(0),
                            Error = "No Record found.",
                            ErrorNumber = 999
                        });
                        data = serializer.Serialize(DataConfirmation);
                        Context.Response.Write(data);
                    }
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("FK_tblVetTimeSlot_tblTimeSlot") > 0)
                    sMessage = "Could not delete. Reference Exists for Vet Time Slot.";
                else if (sMessage.IndexOf("FK_tblVetAppointment_tblVetTimeSlot") > 0)
                    sMessage = "Could not delete. Reference Exists for Vet Time Slot.";
                else if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetTimeSlot Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetTimeSlotInfo(string AuthKey, Int64 VetTimeSlotId)
        {
            string data = string.Empty;
            var VetTimeSlot = new List<VetTimeSlot>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetTimeSlot] @OpType = 'S', @VetTimeSlotId = " + VetTimeSlotId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetTimeSlot.Add(new VetTimeSlot()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetTimeSlotId"]),
                            VetTimeSlotId = Convert.ToInt32(dt.Rows[i]["VetTimeSlotId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            TimeSlotId = Convert.ToInt32(dt.Rows[i]["TimeSlotId"]),
                            TimeSlotName = Convert.ToString(dt.Rows[i]["TimeSlotName"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetTimeSlot);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetTimeSlot Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetTimeSlotInfoAll(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var VetTimeSlot = new List<VetTimeSlot>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetTimeSlot] @OpType = 'Get', @VetId = " + VetId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetTimeSlot.Add(new VetTimeSlot()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetTimeSlotId"]),
                            VetTimeSlotId = Convert.ToInt32(dt.Rows[i]["VetTimeSlotId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            TimeSlotId = Convert.ToInt32(dt.Rows[i]["TimeSlotId"]),
                            TimeSlotName = Convert.ToString(dt.Rows[i]["TimeSlotName"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetTimeSlot);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - Subscription Function
        #region Vet-Subscription Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetSubscriptionInsert(string AuthKey, Int64 VetId, Int64 SessionBuy, Int64 PaymentAmount)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetSubscription] @VetId = " + VetId.ToString() + ", @SessionBuy = " + SessionBuy.ToString() + ", @PaymentAmount = " + PaymentAmount.ToString() + ", @OpType = 'I'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetSubscriptionId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Subscription Delete
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetSubscriptionDelete(string AuthKey, Int64 VetSubscriptionId)
        {
            string data = string.Empty;
            var VetSubscription = new List<VetSubscription>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetSubscription] @VetSubscriptionId = " + VetSubscriptionId.ToString() + ", @OpType = 'D'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        DataTable dt_VetSubscription = objDBHelper.FillTable("Exec [uspVetSubscription] @OpType = 'Get', @VetId = " + Convert.ToInt32(dt.Rows[i]["VetId"]));
                        for (int k = 0; k < dt_VetSubscription.Rows.Count; k++)
                        {
                            blnDataExists = true;
                            VetSubscription.Add(new VetSubscription()
                            {
                                DataId = Convert.ToInt32(dt_VetSubscription.Rows[k]["VetSubscriptionId"]),
                                VetSubscriptionId = Convert.ToInt32(dt_VetSubscription.Rows[k]["VetSubscriptionId"]),
                                VetId = Convert.ToInt32(dt_VetSubscription.Rows[k]["VetId"]),
                                SessionBuy = Convert.ToInt32(dt_VetSubscription.Rows[k]["SessionBuy"]),
                                PaymentAmount = Convert.ToDouble(dt_VetSubscription.Rows[k]["PaymentAmount"]),
                                CreatedOn = Convert.ToString(dt_VetSubscription.Rows[k]["CreatedOn"]),
                                EndDate = Convert.ToString(dt_VetSubscription.Rows[k]["EndDate"])
                            });
                        }
                    }

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    if (blnDataExists)
                    {
                        data = serializer.Serialize(VetSubscription);
                        Context.Response.Write(data);
                    }
                    else
                    {
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(0),
                            Error = "No Record found.",
                            ErrorNumber = 999
                        });
                        data = serializer.Serialize(DataConfirmation);
                        Context.Response.Write(data);
                    }
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetSubscription Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetSubscriptionInfo(string AuthKey, Int64 VetSubscriptionId)
        {
            string data = string.Empty;
            var VetSubscription = new List<VetSubscription>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetSubscription] @OpType = 'S', @VetSubscriptionId = " + VetSubscriptionId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetSubscription.Add(new VetSubscription()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetSubscriptionId"]),
                            VetSubscriptionId = Convert.ToInt32(dt.Rows[i]["VetSubscriptionId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            SessionBuy = Convert.ToInt32(dt.Rows[i]["SessionBuy"]),
                            PaymentAmount = Convert.ToDouble(dt.Rows[i]["PaymentAmount"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetSubscription);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetSubscription Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetSubscriptionInfoAll(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var VetSubscription = new List<VetSubscription>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetSubscription] @OpType = 'Get', @VetId = " + VetId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetSubscription.Add(new VetSubscription()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetSubscriptionId"]),
                            VetSubscriptionId = Convert.ToInt32(dt.Rows[i]["VetSubscriptionId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            SessionBuy = Convert.ToInt32(dt.Rows[i]["SessionBuy"]),
                            PaymentAmount = Convert.ToDouble(dt.Rows[i]["PaymentAmount"]),
                            VetBannerPic = Convert.ToString(dt.Rows[i]["VetBannerPic"]),
                            VetProfilePic = Convert.ToString(dt.Rows[i]["VetProfilePic"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetSubscription);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - Appointment Function
        #region Vet-Appointment Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetAppointmentInsert(string AuthKey, Int64 VetId, Int64 VetPractiseId, Int64 ClientId, Int64 ClientPetId, string SymptomsDescription, Int64 VetTimeSlotId, string AppointmentDate)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sQry = @"Exec [uspVetAppointment] @VetId = " + VetId.ToString() + ", @VetPractiseId = " + VetPractiseId.ToString() + ", @ClientId = " + ClientId.ToString() + ", @ClientPetId = " + ClientPetId.ToString() + @", 
                                                            @SymptomsDescription = '" + SymptomsDescription.Replace("'", "''") + "', @VetTimeSlotId = " + VetTimeSlotId.ToString() + @", 
                                                            @AppointmentDate = '" + AppointmentDate + "', @OpType = 'I'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                            string sRemarks = @"Appointment Request has been generated.
    Appointment No. : " + Convert.ToString(dt.Rows[i]["AppointmentNo"]);
                            InsertNotifications(AuthKey, 0, VetId, "Appointment Request", sRemarks, string.Empty, 2);

                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetAppointmentId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Appointment Approve
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetAppointment_Approve(string AuthKey, Int64 VetAppointmentId, Int64 VetTimeSubSlotId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sQry = @"Exec [uspVetAppointment] @VetAppointmentId = " + VetAppointmentId.ToString() + ", @VetTimeSubSlotId = " + VetTimeSubSlotId.ToString() + ", @OpType = 'IApprove'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string sRemarks = @"Appointment has been approved.
    Appointment No. : " + Convert.ToString(dt.Rows[i]["AppointmentNo"]);
                        InsertNotifications(AuthKey, 0, Convert.ToInt64(dt.Rows[i]["VetId"]), "Appointment Request", sRemarks, string.Empty, 3);

                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetAppointmentId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Appointment Reject
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetAppointment_Reject(string AuthKey, Int64 VetAppointmentId, Int64 RejectReasonId, string RejectReasonRemarks)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sQry = @"Exec [uspVetAppointment] @VetAppointmentId = " + VetAppointmentId.ToString() + ", @RejectReasonId = " + RejectReasonId.ToString() + ", @RejectReasonRemarks = '" + RejectReasonRemarks + "', @OpType = 'IReject'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string sRemarks = @"Appointment has been rejected.
    Appointment No. : " + Convert.ToString(dt.Rows[i]["AppointmentNo"]);
                        InsertNotifications(AuthKey, 0, Convert.ToInt64(dt.Rows[i]["VetId"]), "Appointment Request", sRemarks, string.Empty, 4);

                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetAppointmentId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Appointment Client Confirm
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetAppointment_ClientConfirm(string AuthKey, Int64 VetAppointmentId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sQry = @"Exec [uspVetAppointment] @VetAppointmentId = " + VetAppointmentId.ToString() + ", @OpType = 'IClientConfirm'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetAppointmentId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Appointment Vet Confirm
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetAppointment_VetConfirm(string AuthKey, Int64 VetAppointmentId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sQry = @"Exec [uspVetAppointment] @VetAppointmentId = " + VetAppointmentId.ToString() + ", @OpType = 'IVetConfirm'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetAppointmentId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Appointment ClientCancel
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetAppointment_ClientCancel(string AuthKey, Int64 VetAppointmentId, Int64 ClientCancelReasonId, string ClientCancelReasonRemarks)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sQry = @"Exec [uspVetAppointment] @VetAppointmentId = " + VetAppointmentId.ToString() + ", @ClientCancelReasonId = " + ClientCancelReasonId.ToString() + ", @ClientCancelReasonRemarks = '" + ClientCancelReasonRemarks + "', @OpType = 'IClientCancel'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetAppointmentId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Appointment VetCancel
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetAppointment_VetCancel(string AuthKey, Int64 VetAppointmentId, Int64 VetCancelReasonId, string VetCancelReasonRemarks)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sQry = @"Exec [uspVetAppointment] @VetAppointmentId = " + VetAppointmentId.ToString() + ", @VetCancelReasonId = " + VetCancelReasonId.ToString() + ", @VetCancelReasonRemarks = '" + VetCancelReasonRemarks + "', @OpType = 'IVetCancel'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetAppointmentId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetAppointment Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetAppointmentInfo(string AuthKey, Int64 VetAppointmentId)
        {
            string data = string.Empty;
            var VetAppointment = new List<VetAppointment>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string sDefaultPicsPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                string sDefaultBannerPicPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";
                string sDefaultPicsPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractiseprofilepic.png";
                string sDefaultBannerPicPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractisebannerpic.png";
                string sDefaultPicsPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/profilepic.png";
                string sDefaultBannerPicPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/bannerpic.png";

                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetAppointment] @OpType = 'S', @VetAppointmentId = " + VetAppointmentId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetAppointment.Add(new VetAppointment()
                        {
                            DataId = Convert.ToInt64(dt.Rows[i]["VetAppointmentId"]),
                            VetAppointmentId = Convert.ToInt64(dt.Rows[i]["VetAppointmentId"]),
                            AppointmentNo = Convert.ToString(dt.Rows[i]["AppointmentNo"]),

                            VetId = Convert.ToInt64(dt.Rows[i]["VetId"]),
                            VetName = Convert.ToString(dt.Rows[i]["VetName"]),
                            VetUserName = Convert.ToString(dt.Rows[i]["VetUserName"]),
                            VetPhoneNo = Convert.ToString(dt.Rows[i]["VetPhoneNo"]),
                            VetEmailId = Convert.ToString(dt.Rows[i]["VetEmailId"]),
                            VetProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetProfilePic"])) ? sDefaultPicsPath_Vet : Convert.ToString(dt.Rows[i]["VetProfilePic"]),
                            VetBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetBannerPic"])) ? sDefaultBannerPicPath_Vet : Convert.ToString(dt.Rows[i]["VetBannerPic"]),

                            VetPractiseId = Convert.ToInt64(dt.Rows[i]["VetPractiseId"]),
                            VetPractiseName = Convert.ToString(dt.Rows[i]["VetPractiseName"]),
                            VetPractiseUserName = Convert.ToString(dt.Rows[i]["VetPractiseUserName"]),
                            VetPractisePhoneNo = Convert.ToString(dt.Rows[i]["VetPractisePhoneNo"]),
                            VetPractiseEmailId = Convert.ToString(dt.Rows[i]["VetPractiseEmailId"]),
                            VetPractiseProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"])) ? sDefaultPicsPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"]),
                            VetPractiseBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"])) ? sDefaultBannerPicPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"]),

                            ClientId = Convert.ToInt64(dt.Rows[i]["ClientId"]),
                            ClientName = Convert.ToString(dt.Rows[i]["ClientName"]),
                            ClientUserName = Convert.ToString(dt.Rows[i]["ClientUserName"]),
                            ClientPhoneNo = Convert.ToString(dt.Rows[i]["ClientPhoneNo"]),
                            ClientEmailId = Convert.ToString(dt.Rows[i]["ClientEmailId"]),
                            ClientProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ClientProfilePic"])) ? sDefaultPicsPath_Client : Convert.ToString(dt.Rows[i]["ClientProfilePic"]),
                            ClientBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ClientBannerPic"])) ? sDefaultBannerPicPath_Client : Convert.ToString(dt.Rows[i]["ClientBannerPic"]),

                            ClientPetId = Convert.ToInt64(dt.Rows[i]["ClientPetId"]),
                            PetName = Convert.ToString(dt.Rows[i]["PetName"]),
                            PetTypeName = Convert.ToString(dt.Rows[i]["PetTypeName"]),
                            PetBreedName = Convert.ToString(dt.Rows[i]["PetBreedName"]),
                            SymptomsDescription = Convert.ToString(dt.Rows[i]["SymptomsDescription"]),

                            VetTimeSubSlotId = Convert.ToInt64(dt.Rows[i]["VetTimeSubSlotId"]),
                            VetTimeSlotId = Convert.ToInt64(dt.Rows[i]["VetTimeSlotId"]),
                            TimeSlotId = Convert.ToInt64(dt.Rows[i]["TimeSlotId"]),
                            TimeSlotName = Convert.ToString(dt.Rows[i]["TimeSlotName"]),
                            AppointmentDate = Convert.ToString(dt.Rows[i]["AppointmentDate"]),
                            AppointmentDateDisp = Convert.ToString(dt.Rows[i]["AppointmentDateDisp"]),

                            AppointmentStatus = Convert.ToInt32(dt.Rows[i]["AppointmentStatus"]),
                            AppointmentStatusDisp = Convert.ToString(dt.Rows[i]["AppointmentStatusDisp"]),

                            RejectReasonId = Convert.ToInt64(dt.Rows[i]["RejectReasonId"]),
                            RejectReasonName = Convert.ToString(dt.Rows[i]["RejectReasonName"]),
                            RejectReasonRemarks = Convert.ToString(dt.Rows[i]["RejectReasonRemarks"]),

                            FromTime = Convert.ToString(dt.Rows[i]["FromTime"]),
                            ToTime = Convert.ToString(dt.Rows[i]["ToTime"]),

                            IsClientConfirm = Convert.ToInt32(dt.Rows[i]["IsClientConfirm"]),
                            ClientConfirmDisp = Convert.ToString(dt.Rows[i]["ClientConfirmDisp"]),
                            IsVetConfirm = Convert.ToInt32(dt.Rows[i]["IsVetConfirm"]),
                            VetConfirmDisp = Convert.ToString(dt.Rows[i]["VetConfirmDisp"]),

                            ClientCancelReasonId = Convert.ToInt64(dt.Rows[i]["ClientCancelReasonId"]),
                            ClientCancelReasonName = Convert.ToString(dt.Rows[i]["ClientCancelReasonName"]),
                            ClientCancelReasonRemarks = Convert.ToString(dt.Rows[i]["ClientCancelReasonRemarks"]),

                            VetCancelReasonId = Convert.ToInt64(dt.Rows[i]["VetCancelReasonId"]),
                            VetCancelReasonName = Convert.ToString(dt.Rows[i]["VetCancelReasonName"]),
                            VetCancelReasonRemarks = Convert.ToString(dt.Rows[i]["VetCancelReasonRemarks"]),

                            SessionStartOn = Convert.ToString(dt.Rows[i]["SessionStartOn"]),
                            SessionEndOn = Convert.ToString(dt.Rows[i]["SessionEndOn"]),
                            PaymentAmount = Convert.ToDecimal(dt.Rows[i]["PaymentAmount"]),
                            AvgRating = Convert.ToDecimal(dt.Rows[i]["AvgRating"]),
                            Review = Convert.ToString(dt.Rows[i]["Review"]),

                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetAppointment);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetAppointment Info Vet
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetAppointmentInfo_Vet(string AuthKey, Int64 VetId, int PageNumber, int Records)
        {
            string data = string.Empty;
            var VetAppointment = new List<VetAppointment>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string sDefaultPicsPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                string sDefaultBannerPicPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";
                string sDefaultPicsPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractiseprofilepic.png";
                string sDefaultBannerPicPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractisebannerpic.png";
                string sDefaultPicsPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/profilepic.png";
                string sDefaultBannerPicPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/bannerpic.png";

                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetAppointmentPaging] @OpType = 'GetVet', @VetId = " + VetId.ToString() + ", @PageNumber = " + PageNumber.ToString() + ", @Records = " + Records.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetAppointment.Add(new VetAppointment()
                        {
                            DataId = Convert.ToInt64(dt.Rows[i]["VetAppointmentId"]),
                            VetAppointmentId = Convert.ToInt64(dt.Rows[i]["VetAppointmentId"]),
                            AppointmentNo = Convert.ToString(dt.Rows[i]["AppointmentNo"]),

                            VetId = Convert.ToInt64(dt.Rows[i]["VetId"]),
                            VetName = Convert.ToString(dt.Rows[i]["VetName"]),
                            VetUserName = Convert.ToString(dt.Rows[i]["VetUserName"]),
                            VetPhoneNo = Convert.ToString(dt.Rows[i]["VetPhoneNo"]),
                            VetEmailId = Convert.ToString(dt.Rows[i]["VetEmailId"]),
                            VetProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetProfilePic"])) ? sDefaultPicsPath_Vet : Convert.ToString(dt.Rows[i]["VetProfilePic"]),
                            VetBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetBannerPic"])) ? sDefaultBannerPicPath_Vet : Convert.ToString(dt.Rows[i]["VetBannerPic"]),

                            VetPractiseId = Convert.ToInt64(dt.Rows[i]["VetPractiseId"]),
                            VetPractiseName = Convert.ToString(dt.Rows[i]["VetPractiseName"]),
                            VetPractiseUserName = Convert.ToString(dt.Rows[i]["VetPractiseUserName"]),
                            VetPractisePhoneNo = Convert.ToString(dt.Rows[i]["VetPractisePhoneNo"]),
                            VetPractiseEmailId = Convert.ToString(dt.Rows[i]["VetPractiseEmailId"]),
                            VetPractiseBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"])) ? sDefaultBannerPicPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"]),
                            VetPractiseProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"])) ? sDefaultPicsPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"]),

                            ClientId = Convert.ToInt64(dt.Rows[i]["ClientId"]),
                            ClientName = Convert.ToString(dt.Rows[i]["ClientName"]),
                            ClientUserName = Convert.ToString(dt.Rows[i]["ClientUserName"]),
                            ClientPhoneNo = Convert.ToString(dt.Rows[i]["ClientPhoneNo"]),
                            ClientEmailId = Convert.ToString(dt.Rows[i]["ClientEmailId"]),
                            ClientProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ClientProfilePic"])) ? sDefaultPicsPath_Client : Convert.ToString(dt.Rows[i]["ClientProfilePic"]),
                            ClientBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ClientBannerPic"])) ? sDefaultBannerPicPath_Client : Convert.ToString(dt.Rows[i]["ClientBannerPic"]),

                            ClientPetId = Convert.ToInt64(dt.Rows[i]["ClientPetId"]),
                            PetName = Convert.ToString(dt.Rows[i]["PetName"]),
                            PetTypeName = Convert.ToString(dt.Rows[i]["PetTypeName"]),
                            PetBreedName = Convert.ToString(dt.Rows[i]["PetBreedName"]),
                            SymptomsDescription = Convert.ToString(dt.Rows[i]["SymptomsDescription"]),

                            VetTimeSubSlotId = Convert.ToInt64(dt.Rows[i]["VetTimeSubSlotId"]),
                            VetTimeSlotId = Convert.ToInt64(dt.Rows[i]["VetTimeSlotId"]),
                            TimeSlotId = Convert.ToInt64(dt.Rows[i]["TimeSlotId"]),
                            TimeSlotName = Convert.ToString(dt.Rows[i]["TimeSlotName"]),
                            AppointmentDate = Convert.ToString(dt.Rows[i]["AppointmentDate"]),
                            AppointmentDateDisp = Convert.ToString(dt.Rows[i]["AppointmentDateDisp"]),

                            AppointmentStatus = Convert.ToInt32(dt.Rows[i]["AppointmentStatus"]),
                            AppointmentStatusDisp = Convert.ToString(dt.Rows[i]["AppointmentStatusDisp"]),

                            RejectReasonId = Convert.ToInt64(dt.Rows[i]["RejectReasonId"]),
                            RejectReasonName = Convert.ToString(dt.Rows[i]["RejectReasonName"]),
                            RejectReasonRemarks = Convert.ToString(dt.Rows[i]["RejectReasonRemarks"]),

                            FromTime = Convert.ToString(dt.Rows[i]["FromTime"]),
                            ToTime = Convert.ToString(dt.Rows[i]["ToTime"]),

                            IsClientConfirm = Convert.ToInt32(dt.Rows[i]["IsClientConfirm"]),
                            ClientConfirmDisp = Convert.ToString(dt.Rows[i]["ClientConfirmDisp"]),
                            IsVetConfirm = Convert.ToInt32(dt.Rows[i]["IsVetConfirm"]),
                            VetConfirmDisp = Convert.ToString(dt.Rows[i]["VetConfirmDisp"]),

                            ClientCancelReasonId = Convert.ToInt64(dt.Rows[i]["ClientCancelReasonId"]),
                            ClientCancelReasonName = Convert.ToString(dt.Rows[i]["ClientCancelReasonName"]),
                            ClientCancelReasonRemarks = Convert.ToString(dt.Rows[i]["ClientCancelReasonRemarks"]),

                            VetCancelReasonId = Convert.ToInt64(dt.Rows[i]["VetCancelReasonId"]),
                            VetCancelReasonName = Convert.ToString(dt.Rows[i]["VetCancelReasonName"]),
                            VetCancelReasonRemarks = Convert.ToString(dt.Rows[i]["VetCancelReasonRemarks"]),

                            SessionStartOn = Convert.ToString(dt.Rows[i]["SessionStartOn"]),
                            SessionEndOn = Convert.ToString(dt.Rows[i]["SessionEndOn"]),
                            PaymentAmount = Convert.ToDecimal(dt.Rows[i]["PaymentAmount"]),
                            AvgRating = Convert.ToDecimal(dt.Rows[i]["AvgRating"]),
                            Review = Convert.ToString(dt.Rows[i]["Review"]),

                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"]),

                            RowNo = Convert.ToInt32(dt.Rows[i]["RowNo"]),
                            TotalRowNo = Convert.ToInt32(dt.Rows[i]["TotalRowNo"]),
                            TotalPage = Convert.ToInt32(dt.Rows[i]["TotalPage"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetAppointment);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetAppointment Info VetPractise
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetAppointmentInfo_VetPractise(string AuthKey, Int64 VetPractiseId, int PageNumber, int Records)
        {
            string data = string.Empty;
            var VetAppointment = new List<VetAppointment>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string sDefaultPicsPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                string sDefaultBannerPicPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";
                string sDefaultPicsPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractiseprofilepic.png";
                string sDefaultBannerPicPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractisebannerpic.png";
                string sDefaultPicsPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/profilepic.png";
                string sDefaultBannerPicPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/bannerpic.png";

                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetAppointmentPaging] @OpType = 'GetVetPractise', @VetPractiseId = " + VetPractiseId.ToString() + ", @PageNumber = " + PageNumber.ToString() + ", @Records = " + Records.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetAppointment.Add(new VetAppointment()
                        {
                            DataId = Convert.ToInt64(dt.Rows[i]["VetAppointmentId"]),
                            VetAppointmentId = Convert.ToInt64(dt.Rows[i]["VetAppointmentId"]),
                            AppointmentNo = Convert.ToString(dt.Rows[i]["AppointmentNo"]),

                            VetId = Convert.ToInt64(dt.Rows[i]["VetId"]),
                            VetName = Convert.ToString(dt.Rows[i]["VetName"]),
                            VetUserName = Convert.ToString(dt.Rows[i]["VetUserName"]),
                            VetPhoneNo = Convert.ToString(dt.Rows[i]["VetPhoneNo"]),
                            VetEmailId = Convert.ToString(dt.Rows[i]["VetEmailId"]),
                            VetProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetProfilePic"])) ? sDefaultPicsPath_Vet : Convert.ToString(dt.Rows[i]["VetProfilePic"]),
                            VetBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetBannerPic"])) ? sDefaultBannerPicPath_Vet : Convert.ToString(dt.Rows[i]["VetBannerPic"]),

                            VetPractiseId = Convert.ToInt64(dt.Rows[i]["VetPractiseId"]),
                            VetPractiseName = Convert.ToString(dt.Rows[i]["VetPractiseName"]),
                            VetPractiseUserName = Convert.ToString(dt.Rows[i]["VetPractiseUserName"]),
                            VetPractisePhoneNo = Convert.ToString(dt.Rows[i]["VetPractisePhoneNo"]),
                            VetPractiseEmailId = Convert.ToString(dt.Rows[i]["VetPractiseEmailId"]),
                            VetPractiseBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"])) ? sDefaultBannerPicPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"]),
                            VetPractiseProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"])) ? sDefaultPicsPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"]),

                            ClientId = Convert.ToInt64(dt.Rows[i]["ClientId"]),
                            ClientName = Convert.ToString(dt.Rows[i]["ClientName"]),
                            ClientUserName = Convert.ToString(dt.Rows[i]["ClientUserName"]),
                            ClientPhoneNo = Convert.ToString(dt.Rows[i]["ClientPhoneNo"]),
                            ClientEmailId = Convert.ToString(dt.Rows[i]["ClientEmailId"]),
                            ClientProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ClientProfilePic"])) ? sDefaultPicsPath_Client : Convert.ToString(dt.Rows[i]["ClientProfilePic"]),
                            ClientBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ClientBannerPic"])) ? sDefaultBannerPicPath_Client : Convert.ToString(dt.Rows[i]["ClientBannerPic"]),

                            ClientPetId = Convert.ToInt64(dt.Rows[i]["ClientPetId"]),
                            PetName = Convert.ToString(dt.Rows[i]["PetName"]),
                            PetTypeName = Convert.ToString(dt.Rows[i]["PetTypeName"]),
                            PetBreedName = Convert.ToString(dt.Rows[i]["PetBreedName"]),
                            SymptomsDescription = Convert.ToString(dt.Rows[i]["SymptomsDescription"]),

                            VetTimeSubSlotId = Convert.ToInt64(dt.Rows[i]["VetTimeSubSlotId"]),
                            VetTimeSlotId = Convert.ToInt64(dt.Rows[i]["VetTimeSlotId"]),
                            TimeSlotId = Convert.ToInt64(dt.Rows[i]["TimeSlotId"]),
                            TimeSlotName = Convert.ToString(dt.Rows[i]["TimeSlotName"]),
                            AppointmentDate = Convert.ToString(dt.Rows[i]["AppointmentDate"]),
                            AppointmentDateDisp = Convert.ToString(dt.Rows[i]["AppointmentDateDisp"]),

                            AppointmentStatus = Convert.ToInt32(dt.Rows[i]["AppointmentStatus"]),
                            AppointmentStatusDisp = Convert.ToString(dt.Rows[i]["AppointmentStatusDisp"]),

                            RejectReasonId = Convert.ToInt64(dt.Rows[i]["RejectReasonId"]),
                            RejectReasonName = Convert.ToString(dt.Rows[i]["RejectReasonName"]),
                            RejectReasonRemarks = Convert.ToString(dt.Rows[i]["RejectReasonRemarks"]),

                            FromTime = Convert.ToString(dt.Rows[i]["FromTime"]),
                            ToTime = Convert.ToString(dt.Rows[i]["ToTime"]),

                            IsClientConfirm = Convert.ToInt32(dt.Rows[i]["IsClientConfirm"]),
                            ClientConfirmDisp = Convert.ToString(dt.Rows[i]["ClientConfirmDisp"]),
                            IsVetConfirm = Convert.ToInt32(dt.Rows[i]["IsVetConfirm"]),
                            VetConfirmDisp = Convert.ToString(dt.Rows[i]["VetConfirmDisp"]),

                            ClientCancelReasonId = Convert.ToInt64(dt.Rows[i]["ClientCancelReasonId"]),
                            ClientCancelReasonName = Convert.ToString(dt.Rows[i]["ClientCancelReasonName"]),
                            ClientCancelReasonRemarks = Convert.ToString(dt.Rows[i]["ClientCancelReasonRemarks"]),

                            VetCancelReasonId = Convert.ToInt64(dt.Rows[i]["VetCancelReasonId"]),
                            VetCancelReasonName = Convert.ToString(dt.Rows[i]["VetCancelReasonName"]),
                            VetCancelReasonRemarks = Convert.ToString(dt.Rows[i]["VetCancelReasonRemarks"]),

                            SessionStartOn = Convert.ToString(dt.Rows[i]["SessionStartOn"]),
                            SessionEndOn = Convert.ToString(dt.Rows[i]["SessionEndOn"]),
                            PaymentAmount = Convert.ToDecimal(dt.Rows[i]["PaymentAmount"]),
                            AvgRating = Convert.ToDecimal(dt.Rows[i]["AvgRating"]),
                            Review = Convert.ToString(dt.Rows[i]["Review"]),

                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"]),

                            RowNo = Convert.ToInt32(dt.Rows[i]["RowNo"]),
                            TotalRowNo = Convert.ToInt32(dt.Rows[i]["TotalRowNo"]),
                            TotalPage = Convert.ToInt32(dt.Rows[i]["TotalPage"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetAppointment);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetAppointment Info Client
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetAppointmentInfo_Client(string AuthKey, Int64 ClientId, int PageNumber, int Records)
        {
            string data = string.Empty;
            var VetAppointment = new List<VetAppointment>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string sDefaultPicsPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                string sDefaultBannerPicPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";
                string sDefaultPicsPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractiseprofilepic.png";
                string sDefaultBannerPicPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractisebannerpic.png";
                string sDefaultPicsPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/profilepic.png";
                string sDefaultBannerPicPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/bannerpic.png";

                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetAppointmentPaging] @OpType = 'GetClient', @ClientId = " + ClientId.ToString() + ", @PageNumber = " + PageNumber.ToString() + ", @Records = " + Records.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetAppointment.Add(new VetAppointment()
                        {
                            DataId = Convert.ToInt64(dt.Rows[i]["VetAppointmentId"]),
                            VetAppointmentId = Convert.ToInt64(dt.Rows[i]["VetAppointmentId"]),
                            AppointmentNo = Convert.ToString(dt.Rows[i]["AppointmentNo"]),

                            VetId = Convert.ToInt64(dt.Rows[i]["VetId"]),
                            VetName = Convert.ToString(dt.Rows[i]["VetName"]),
                            VetUserName = Convert.ToString(dt.Rows[i]["VetUserName"]),
                            VetPhoneNo = Convert.ToString(dt.Rows[i]["VetPhoneNo"]),
                            VetEmailId = Convert.ToString(dt.Rows[i]["VetEmailId"]),
                            VetProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetProfilePic"])) ? sDefaultPicsPath_Vet : Convert.ToString(dt.Rows[i]["VetProfilePic"]),
                            VetBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetBannerPic"])) ? sDefaultBannerPicPath_Vet : Convert.ToString(dt.Rows[i]["VetBannerPic"]),

                            VetPractiseId = Convert.ToInt64(dt.Rows[i]["VetPractiseId"]),
                            VetPractiseName = Convert.ToString(dt.Rows[i]["VetPractiseName"]),
                            VetPractiseUserName = Convert.ToString(dt.Rows[i]["VetPractiseUserName"]),
                            VetPractisePhoneNo = Convert.ToString(dt.Rows[i]["VetPractisePhoneNo"]),
                            VetPractiseEmailId = Convert.ToString(dt.Rows[i]["VetPractiseEmailId"]),
                            VetPractiseProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"])) ? sDefaultPicsPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"]),
                            VetPractiseBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"])) ? sDefaultBannerPicPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"]),

                            ClientId = Convert.ToInt64(dt.Rows[i]["ClientId"]),
                            ClientName = Convert.ToString(dt.Rows[i]["ClientName"]),
                            ClientUserName = Convert.ToString(dt.Rows[i]["ClientUserName"]),
                            ClientPhoneNo = Convert.ToString(dt.Rows[i]["ClientPhoneNo"]),
                            ClientEmailId = Convert.ToString(dt.Rows[i]["ClientEmailId"]),
                            ClientProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ClientProfilePic"])) ? sDefaultPicsPath_Client : Convert.ToString(dt.Rows[i]["ClientProfilePic"]),
                            ClientBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ClientBannerPic"])) ? sDefaultBannerPicPath_Client : Convert.ToString(dt.Rows[i]["ClientBannerPic"]),

                            ClientPetId = Convert.ToInt64(dt.Rows[i]["ClientPetId"]),
                            PetName = Convert.ToString(dt.Rows[i]["PetName"]),
                            PetTypeName = Convert.ToString(dt.Rows[i]["PetTypeName"]),
                            PetBreedName = Convert.ToString(dt.Rows[i]["PetBreedName"]),
                            SymptomsDescription = Convert.ToString(dt.Rows[i]["SymptomsDescription"]),

                            VetTimeSubSlotId = Convert.ToInt64(dt.Rows[i]["VetTimeSubSlotId"]),
                            VetTimeSlotId = Convert.ToInt64(dt.Rows[i]["VetTimeSlotId"]),
                            TimeSlotId = Convert.ToInt64(dt.Rows[i]["TimeSlotId"]),
                            TimeSlotName = Convert.ToString(dt.Rows[i]["TimeSlotName"]),
                            AppointmentDate = Convert.ToString(dt.Rows[i]["AppointmentDate"]),
                            AppointmentDateDisp = Convert.ToString(dt.Rows[i]["AppointmentDateDisp"]),

                            AppointmentStatus = Convert.ToInt32(dt.Rows[i]["AppointmentStatus"]),
                            AppointmentStatusDisp = Convert.ToString(dt.Rows[i]["AppointmentStatusDisp"]),

                            RejectReasonId = Convert.ToInt64(dt.Rows[i]["RejectReasonId"]),
                            RejectReasonName = Convert.ToString(dt.Rows[i]["RejectReasonName"]),
                            RejectReasonRemarks = Convert.ToString(dt.Rows[i]["RejectReasonRemarks"]),

                            FromTime = Convert.ToString(dt.Rows[i]["FromTime"]),
                            ToTime = Convert.ToString(dt.Rows[i]["ToTime"]),

                            IsClientConfirm = Convert.ToInt32(dt.Rows[i]["IsClientConfirm"]),
                            ClientConfirmDisp = Convert.ToString(dt.Rows[i]["ClientConfirmDisp"]),
                            IsVetConfirm = Convert.ToInt32(dt.Rows[i]["IsVetConfirm"]),
                            VetConfirmDisp = Convert.ToString(dt.Rows[i]["VetConfirmDisp"]),

                            ClientCancelReasonId = Convert.ToInt64(dt.Rows[i]["ClientCancelReasonId"]),
                            ClientCancelReasonName = Convert.ToString(dt.Rows[i]["ClientCancelReasonName"]),
                            ClientCancelReasonRemarks = Convert.ToString(dt.Rows[i]["ClientCancelReasonRemarks"]),

                            VetCancelReasonId = Convert.ToInt64(dt.Rows[i]["VetCancelReasonId"]),
                            VetCancelReasonName = Convert.ToString(dt.Rows[i]["VetCancelReasonName"]),
                            VetCancelReasonRemarks = Convert.ToString(dt.Rows[i]["VetCancelReasonRemarks"]),

                            SessionStartOn = Convert.ToString(dt.Rows[i]["SessionStartOn"]),
                            SessionEndOn = Convert.ToString(dt.Rows[i]["SessionEndOn"]),
                            PaymentAmount = Convert.ToDecimal(dt.Rows[i]["PaymentAmount"]),
                            AvgRating = Convert.ToDecimal(dt.Rows[i]["AvgRating"]),
                            Review = Convert.ToString(dt.Rows[i]["Review"]),

                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"]),

                            RowNo = Convert.ToInt32(dt.Rows[i]["RowNo"]),
                            TotalRowNo = Convert.ToInt32(dt.Rows[i]["TotalRowNo"]),
                            TotalPage = Convert.ToInt32(dt.Rows[i]["TotalPage"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetAppointment);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - Session Function
        #region Vet-Session Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetSessionInsert(string AuthKey, Int64 VetId, Int64 VetPractiseId, Int64 ClientId, Int64 ClientPetId, string SymptomsDescription, string SessionStartOn, decimal PaymentAmount, Int64 VetAppointmentId,
                                        string PaymentTime, string PaymentId, string PaymentStatus, string PaymentResponse)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sQry = @"Exec [uspVetSession] @VetId = " + VetId.ToString() + ", @VetPractiseId = " + VetPractiseId.ToString() + ", @ClientId = " + ClientId.ToString() + ", @ClientPetId = " + ClientPetId.ToString() + @", 
                                                        @SymptomsDescription = '" + SymptomsDescription.Replace("'", "''") + "', @SessionStartOn = '" + SessionStartOn + @"', 
                                                        @SessionEndOn = '', @PaymentAmount = " + PaymentAmount + ", @AvgRating = 0, @Review = '', @VetAppointmentId = " + VetAppointmentId.ToString() + @", 
                                                        @PaymentTime = '" + PaymentTime + "', @PaymentId = '" + PaymentId + "', @PaymentStatus = '" + PaymentStatus + "', @PaymentResponse = '" + PaymentResponse + "', @OpType = 'I'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetSessionId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Session Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetSessionUpdate(string AuthKey, Int64 VetSessionId, string SessionEndOn)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sQry = @"Exec [uspVetSession] @VetSessionId = " + VetSessionId.ToString() + ", @SessionEndOn = '" + SessionEndOn + "', @OpType = 'U'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetSessionId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Session Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetSessionUpdateReview(string AuthKey, Int64 VetSessionId, string Review)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sQry = @"Exec [uspVetSession] @VetSessionId = " + VetSessionId.ToString() + ", @Review = '" + Review + "', @OpType = 'UReview'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetSessionId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetSession Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetSessionInfo(string AuthKey, Int64 VetSessionId)
        {
            string data = string.Empty;
            var VetSession = new List<VetSession>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string sDefaultPicsPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                string sDefaultBannerPicPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";
                string sDefaultPicsPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractiseprofilepic.png";
                string sDefaultBannerPicPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractisebannerpic.png";
                string sDefaultPicsPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/profilepic.png";
                string sDefaultBannerPicPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/bannerpic.png";

                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetSession] @OpType = 'S', @VetSessionId = " + VetSessionId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetSession.Add(new VetSession()
                        {
                            DataId = Convert.ToInt64(dt.Rows[i]["VetSessionId"]),
                            VetSessionId = Convert.ToInt64(dt.Rows[i]["VetSessionId"]),
                            VetSessionNo = Convert.ToString(dt.Rows[i]["VetSessionNo"]),

                            VetId = Convert.ToInt64(dt.Rows[i]["VetId"]),
                            VetName = Convert.ToString(dt.Rows[i]["VetName"]),
                            VetPhoneNo = Convert.ToString(dt.Rows[i]["VetPhoneNo"]),
                            VetEmailId = Convert.ToString(dt.Rows[i]["VetEmailId"]),
                            Vet_ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Vet_ProfilePic"])) ? sDefaultPicsPath_Vet : Convert.ToString(dt.Rows[i]["Vet_ProfilePic"]),
                            Vet_BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Vet_BannerPic"])) ? sDefaultBannerPicPath_Vet : Convert.ToString(dt.Rows[i]["Vet_BannerPic"]),

                            VetPractiseId = Convert.ToInt64(dt.Rows[i]["VetPractiseId"]),
                            VetPractiseName = Convert.ToString(dt.Rows[i]["VetPractiseName"]),
                            VetPractisePhoneNo = Convert.ToString(dt.Rows[i]["VetPractisePhoneNo"]),
                            VetPractiseEmailId = Convert.ToString(dt.Rows[i]["VetPractiseEmailId"]),
                            VetPractiseProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"])) ? sDefaultPicsPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"]),
                            VetPractiseBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"])) ? sDefaultBannerPicPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"]),

                            ClientId = Convert.ToInt64(dt.Rows[i]["ClientId"]),
                            ClientName = Convert.ToString(dt.Rows[i]["ClientName"]),
                            ClientPhoneNo = Convert.ToString(dt.Rows[i]["ClientPhoneNo"]),
                            ClientEmailId = Convert.ToString(dt.Rows[i]["ClientEmailId"]),
                            Client_ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Client_ProfilePic"])) ? sDefaultPicsPath_Client : Convert.ToString(dt.Rows[i]["Client_ProfilePic"]),
                            Client_BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Client_BannerPic"])) ? sDefaultBannerPicPath_Client : Convert.ToString(dt.Rows[i]["Client_BannerPic"]),

                            ClientPetId = Convert.ToInt64(dt.Rows[i]["ClientPetId"]),
                            PetName = Convert.ToString(dt.Rows[i]["PetName"]),
                            PetTypeName = Convert.ToString(dt.Rows[i]["PetTypeName"]),
                            PetBreedName = Convert.ToString(dt.Rows[i]["PetBreedName"]),
                            SymptomsDescription = Convert.ToString(dt.Rows[i]["SymptomsDescription"]),
                            SessionStartOn = Convert.ToString(dt.Rows[i]["SessionStartOn"]),
                            SessionEndOn = Convert.ToString(dt.Rows[i]["SessionEndOn"]),
                            PaymentAmount = Convert.ToDecimal(dt.Rows[i]["PaymentAmount"]),
                            AvgRating = Convert.ToDecimal(dt.Rows[i]["AvgRating"]),
                            Review = Convert.ToString(dt.Rows[i]["Review"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"]),
                            VetAppointmentId = Convert.ToInt64(dt.Rows[i]["VetAppointmentId"]),
                            AppointmentNo = Convert.ToString(dt.Rows[i]["AppointmentNo"]),

                            PaymentTime = Convert.ToString(dt.Rows[i]["PaymentTime"]),
                            PaymentId = Convert.ToString(dt.Rows[i]["PaymentId"]),
                            PaymentStatus = Convert.ToString(dt.Rows[i]["PaymentStatus"]),
                            PaymentResponse = Convert.ToString(dt.Rows[i]["PaymentResponse"]),

                            TicketStatus = Convert.ToInt32(dt.Rows[i]["TicketStatus"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetSession);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetSession Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetSessionInfoAll(string AuthKey, Int64 VetId, int PageNumber, int Records)
        {
            string data = string.Empty;
            var VetSession = new List<VetSession>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string sDefaultPicsPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                string sDefaultBannerPicPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";
                string sDefaultPicsPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractiseprofilepic.png";
                string sDefaultBannerPicPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractisebannerpic.png";
                string sDefaultPicsPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/profilepic.png";
                string sDefaultBannerPicPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/bannerpic.png";

                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetSessionPaging] @OpType = 'Get', @VetId = " + VetId.ToString() + ", @PageNumber = " + PageNumber.ToString() + ", @Records = " + Records.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetSession.Add(new VetSession()
                        {
                            DataId = Convert.ToInt64(dt.Rows[i]["VetSessionId"]),
                            VetSessionId = Convert.ToInt64(dt.Rows[i]["VetSessionId"]),
                            VetSessionNo = Convert.ToString(dt.Rows[i]["VetSessionNo"]),

                            VetId = Convert.ToInt64(dt.Rows[i]["VetId"]),
                            VetName = Convert.ToString(dt.Rows[i]["VetName"]),
                            VetPhoneNo = Convert.ToString(dt.Rows[i]["VetPhoneNo"]),
                            VetEmailId = Convert.ToString(dt.Rows[i]["VetEmailId"]),
                            Vet_ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Vet_ProfilePic"])) ? sDefaultPicsPath_Vet : Convert.ToString(dt.Rows[i]["Vet_ProfilePic"]),
                            Vet_BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Vet_BannerPic"])) ? sDefaultBannerPicPath_Vet : Convert.ToString(dt.Rows[i]["Vet_BannerPic"]),

                            VetPractiseId = Convert.ToInt64(dt.Rows[i]["VetPractiseId"]),
                            VetPractiseName = Convert.ToString(dt.Rows[i]["VetPractiseName"]),
                            VetPractisePhoneNo = Convert.ToString(dt.Rows[i]["VetPractisePhoneNo"]),
                            VetPractiseEmailId = Convert.ToString(dt.Rows[i]["VetPractiseEmailId"]),
                            VetPractiseProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"])) ? sDefaultPicsPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"]),
                            VetPractiseBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"])) ? sDefaultBannerPicPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"]),

                            ClientId = Convert.ToInt64(dt.Rows[i]["ClientId"]),
                            ClientName = Convert.ToString(dt.Rows[i]["ClientName"]),
                            ClientPhoneNo = Convert.ToString(dt.Rows[i]["ClientPhoneNo"]),
                            ClientEmailId = Convert.ToString(dt.Rows[i]["ClientEmailId"]),
                            Client_ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Client_ProfilePic"])) ? sDefaultPicsPath_Client : Convert.ToString(dt.Rows[i]["Client_ProfilePic"]),
                            Client_BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Client_BannerPic"])) ? sDefaultBannerPicPath_Client : Convert.ToString(dt.Rows[i]["Client_BannerPic"]),

                            ClientPetId = Convert.ToInt64(dt.Rows[i]["ClientPetId"]),
                            PetName = Convert.ToString(dt.Rows[i]["PetName"]),
                            PetTypeName = Convert.ToString(dt.Rows[i]["PetTypeName"]),
                            PetBreedName = Convert.ToString(dt.Rows[i]["PetBreedName"]),
                            SymptomsDescription = Convert.ToString(dt.Rows[i]["SymptomsDescription"]),
                            SessionStartOn = Convert.ToString(dt.Rows[i]["SessionStartOn"]),
                            SessionEndOn = Convert.ToString(dt.Rows[i]["SessionEndOn"]),
                            PaymentAmount = Convert.ToDecimal(dt.Rows[i]["PaymentAmount"]),
                            AvgRating = Convert.ToDecimal(dt.Rows[i]["AvgRating"]),
                            Review = Convert.ToString(dt.Rows[i]["Review"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"]),
                            VetAppointmentId = Convert.ToInt64(dt.Rows[i]["VetAppointmentId"]),
                            AppointmentNo = Convert.ToString(dt.Rows[i]["AppointmentNo"]),

                            PaymentTime = Convert.ToString(dt.Rows[i]["PaymentTime"]),
                            PaymentId = Convert.ToString(dt.Rows[i]["PaymentId"]),
                            PaymentStatus = Convert.ToString(dt.Rows[i]["PaymentStatus"]),
                            PaymentResponse = Convert.ToString(dt.Rows[i]["PaymentResponse"]),

                            RowNo = Convert.ToInt32(dt.Rows[i]["RowNo"]),
                            TotalRowNo = Convert.ToInt32(dt.Rows[i]["TotalRowNo"]),
                            TotalPage = Convert.ToInt32(dt.Rows[i]["TotalPage"]),

                            TicketStatus = Convert.ToInt32(dt.Rows[i]["TicketStatus"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetSession);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetSession Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetSessionInfoAll_VetPractise(string AuthKey, Int64 VetPractiseId, int PageNumber, int Records)
        {
            string data = string.Empty;
            var VetSession = new List<VetSession>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string sDefaultPicsPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                string sDefaultBannerPicPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";
                string sDefaultPicsPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractiseprofilepic.png";
                string sDefaultBannerPicPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractisebannerpic.png";
                string sDefaultPicsPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/profilepic.png";
                string sDefaultBannerPicPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/bannerpic.png";

                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetSessionPaging] @OpType = 'GetVetPractise', @VetPractiseId = " + VetPractiseId.ToString() + ", @PageNumber = " + PageNumber.ToString() + ", @Records = " + Records.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetSession.Add(new VetSession()
                        {
                            DataId = Convert.ToInt64(dt.Rows[i]["VetSessionId"]),
                            VetSessionId = Convert.ToInt64(dt.Rows[i]["VetSessionId"]),
                            VetSessionNo = Convert.ToString(dt.Rows[i]["VetSessionNo"]),

                            VetId = Convert.ToInt64(dt.Rows[i]["VetId"]),
                            VetName = Convert.ToString(dt.Rows[i]["VetName"]),
                            VetPhoneNo = Convert.ToString(dt.Rows[i]["VetPhoneNo"]),
                            VetEmailId = Convert.ToString(dt.Rows[i]["VetEmailId"]),
                            Vet_ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Vet_ProfilePic"])) ? sDefaultPicsPath_Vet : Convert.ToString(dt.Rows[i]["Vet_ProfilePic"]),
                            Vet_BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Vet_BannerPic"])) ? sDefaultBannerPicPath_Vet : Convert.ToString(dt.Rows[i]["Vet_BannerPic"]),

                            VetPractiseId = Convert.ToInt64(dt.Rows[i]["VetPractiseId"]),
                            VetPractiseName = Convert.ToString(dt.Rows[i]["VetPractiseName"]),
                            VetPractisePhoneNo = Convert.ToString(dt.Rows[i]["VetPractisePhoneNo"]),
                            VetPractiseEmailId = Convert.ToString(dt.Rows[i]["VetPractiseEmailId"]),
                            VetPractiseProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"])) ? sDefaultPicsPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"]),
                            VetPractiseBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"])) ? sDefaultBannerPicPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"]),

                            ClientId = Convert.ToInt64(dt.Rows[i]["ClientId"]),
                            ClientName = Convert.ToString(dt.Rows[i]["ClientName"]),
                            ClientPhoneNo = Convert.ToString(dt.Rows[i]["ClientPhoneNo"]),
                            ClientEmailId = Convert.ToString(dt.Rows[i]["ClientEmailId"]),
                            Client_ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Client_ProfilePic"])) ? sDefaultPicsPath_Client : Convert.ToString(dt.Rows[i]["Client_ProfilePic"]),
                            Client_BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Client_BannerPic"])) ? sDefaultBannerPicPath_Client : Convert.ToString(dt.Rows[i]["Client_BannerPic"]),

                            ClientPetId = Convert.ToInt64(dt.Rows[i]["ClientPetId"]),
                            PetName = Convert.ToString(dt.Rows[i]["PetName"]),
                            PetTypeName = Convert.ToString(dt.Rows[i]["PetTypeName"]),
                            PetBreedName = Convert.ToString(dt.Rows[i]["PetBreedName"]),
                            SymptomsDescription = Convert.ToString(dt.Rows[i]["SymptomsDescription"]),
                            SessionStartOn = Convert.ToString(dt.Rows[i]["SessionStartOn"]),
                            SessionEndOn = Convert.ToString(dt.Rows[i]["SessionEndOn"]),
                            PaymentAmount = Convert.ToDecimal(dt.Rows[i]["PaymentAmount"]),
                            AvgRating = Convert.ToDecimal(dt.Rows[i]["AvgRating"]),
                            Review = Convert.ToString(dt.Rows[i]["Review"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"]),
                            VetAppointmentId = Convert.ToInt64(dt.Rows[i]["VetAppointmentId"]),
                            AppointmentNo = Convert.ToString(dt.Rows[i]["AppointmentNo"]),

                            PaymentTime = Convert.ToString(dt.Rows[i]["PaymentTime"]),
                            PaymentId = Convert.ToString(dt.Rows[i]["PaymentId"]),
                            PaymentStatus = Convert.ToString(dt.Rows[i]["PaymentStatus"]),
                            PaymentResponse = Convert.ToString(dt.Rows[i]["PaymentResponse"]),

                            RowNo = Convert.ToInt32(dt.Rows[i]["RowNo"]),
                            TotalRowNo = Convert.ToInt32(dt.Rows[i]["TotalRowNo"]),
                            TotalPage = Convert.ToInt32(dt.Rows[i]["TotalPage"]),

                            TicketStatus = Convert.ToInt32(dt.Rows[i]["TicketStatus"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetSession);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetSession Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetSessionInfoAll_Client(string AuthKey, Int64 ClientId, int PageNumber, int Records)
        {
            string data = string.Empty;
            var VetSession = new List<VetSession>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string sDefaultPicsPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetprofilepic.png";
                string sDefaultBannerPicPath_Vet = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetbannerpic.png";
                string sDefaultPicsPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractiseprofilepic.png";
                string sDefaultBannerPicPath_VetPractise = "http://admin.anivethub.com/WebService" + "/DefaultPics/vetpractisebannerpic.png";
                string sDefaultPicsPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/profilepic.png";
                string sDefaultBannerPicPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/bannerpic.png";

                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetSessionPaging] @OpType = 'GetClient', @ClientId = " + ClientId.ToString() + ", @PageNumber = " + PageNumber.ToString() + ", @Records = " + Records.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetSession.Add(new VetSession()
                        {
                            DataId = Convert.ToInt64(dt.Rows[i]["VetSessionId"]),
                            VetSessionId = Convert.ToInt64(dt.Rows[i]["VetSessionId"]),
                            VetSessionNo = Convert.ToString(dt.Rows[i]["VetSessionNo"]),

                            VetId = Convert.ToInt64(dt.Rows[i]["VetId"]),
                            VetName = Convert.ToString(dt.Rows[i]["VetName"]),
                            VetPhoneNo = Convert.ToString(dt.Rows[i]["VetPhoneNo"]),
                            VetEmailId = Convert.ToString(dt.Rows[i]["VetEmailId"]),
                            Vet_ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Vet_ProfilePic"])) ? sDefaultPicsPath_Vet : Convert.ToString(dt.Rows[i]["Vet_ProfilePic"]),
                            Vet_BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Vet_BannerPic"])) ? sDefaultBannerPicPath_Vet : Convert.ToString(dt.Rows[i]["Vet_BannerPic"]),

                            VetPractiseId = Convert.ToInt64(dt.Rows[i]["VetPractiseId"]),
                            VetPractiseName = Convert.ToString(dt.Rows[i]["VetPractiseName"]),
                            VetPractisePhoneNo = Convert.ToString(dt.Rows[i]["VetPractisePhoneNo"]),
                            VetPractiseEmailId = Convert.ToString(dt.Rows[i]["VetPractiseEmailId"]),
                            VetPractiseProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"])) ? sDefaultPicsPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseProfilePic"]),
                            VetPractiseBannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"])) ? sDefaultBannerPicPath_VetPractise : Convert.ToString(dt.Rows[i]["VetPractiseBannerPic"]),

                            ClientId = Convert.ToInt64(dt.Rows[i]["ClientId"]),
                            ClientName = Convert.ToString(dt.Rows[i]["ClientName"]),
                            ClientPhoneNo = Convert.ToString(dt.Rows[i]["ClientPhoneNo"]),
                            ClientEmailId = Convert.ToString(dt.Rows[i]["ClientEmailId"]),
                            Client_ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Client_ProfilePic"])) ? sDefaultPicsPath_Client : Convert.ToString(dt.Rows[i]["Client_ProfilePic"]),
                            Client_BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Client_BannerPic"])) ? sDefaultBannerPicPath_Client : Convert.ToString(dt.Rows[i]["Client_BannerPic"]),

                            ClientPetId = Convert.ToInt64(dt.Rows[i]["ClientPetId"]),
                            PetName = Convert.ToString(dt.Rows[i]["PetName"]),
                            PetTypeName = Convert.ToString(dt.Rows[i]["PetTypeName"]),
                            PetBreedName = Convert.ToString(dt.Rows[i]["PetBreedName"]),
                            SymptomsDescription = Convert.ToString(dt.Rows[i]["SymptomsDescription"]),
                            SessionStartOn = Convert.ToString(dt.Rows[i]["SessionStartOn"]),
                            SessionEndOn = Convert.ToString(dt.Rows[i]["SessionEndOn"]),
                            PaymentAmount = Convert.ToDecimal(dt.Rows[i]["PaymentAmount"]),
                            AvgRating = Convert.ToDecimal(dt.Rows[i]["AvgRating"]),
                            Review = Convert.ToString(dt.Rows[i]["Review"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"]),
                            VetAppointmentId = Convert.ToInt64(dt.Rows[i]["VetAppointmentId"]),
                            AppointmentNo = Convert.ToString(dt.Rows[i]["AppointmentNo"]),

                            PaymentTime = Convert.ToString(dt.Rows[i]["PaymentTime"]),
                            PaymentId = Convert.ToString(dt.Rows[i]["PaymentId"]),
                            PaymentStatus = Convert.ToString(dt.Rows[i]["PaymentStatus"]),
                            PaymentResponse = Convert.ToString(dt.Rows[i]["PaymentResponse"]),

                            RowNo = Convert.ToInt32(dt.Rows[i]["RowNo"]),
                            TotalRowNo = Convert.ToInt32(dt.Rows[i]["TotalRowNo"]),
                            TotalPage = Convert.ToInt32(dt.Rows[i]["TotalPage"]),

                            TicketStatus = Convert.ToInt32(dt.Rows[i]["TicketStatus"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetSession);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetSession Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetSession_ReviewRating(string AuthKey, Int64 VetId, int PageNumber, int Records)
        {
            string data = string.Empty;
            var VetSession_ReviewRating = new List<VetSession_ReviewRating>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string sDefaultPicsPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/profilepic.png";
                string sDefaultBannerPicPath_Client = "http://admin.anivethub.com/WebService" + "/DefaultPics/bannerpic.png";

                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetSession_ReviewRating] @OpType = 'Get', @VetId = " + VetId.ToString() + ", @PageNumber = " + PageNumber.ToString() + ", @Records = " + Records.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetSession_ReviewRating.Add(new VetSession_ReviewRating()
                        {
                            DataId = Convert.ToInt64(dt.Rows[i]["VetId"]),
                            VetId = Convert.ToInt64(dt.Rows[i]["VetId"]),
                            ClientId = Convert.ToInt64(dt.Rows[i]["ClientId"]),
                            ClientName = Convert.ToString(dt.Rows[i]["ClientName"]),
                            AvgRating = Convert.ToDecimal(dt.Rows[i]["AvgRating"]),
                            Review = Convert.ToString(dt.Rows[i]["Review"]),
                            Client_ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Client_ProfilePic"])) ? sDefaultPicsPath_Client : Convert.ToString(dt.Rows[i]["Client_ProfilePic"]),
                            Client_BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Client_BannerPic"])) ? sDefaultBannerPicPath_Client : Convert.ToString(dt.Rows[i]["Client_BannerPic"]),

                            RowNo = Convert.ToInt32(dt.Rows[i]["RowNo"]),
                            TotalRowNo = Convert.ToInt32(dt.Rows[i]["TotalRowNo"]),
                            TotalPage = Convert.ToInt32(dt.Rows[i]["TotalPage"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetSession_ReviewRating);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - SessionFeedback Function
        #region Vet-SessionFeedback Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetSessionFeedbackInsert(string AuthKey, Int64 VetSessionId, Int64 FeedbackTypeId, int Rating)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    string sQry = @"Exec [uspVetSessionFeedback] @VetSessionId = " + VetSessionId.ToString() + ", @FeedbackTypeId = " + FeedbackTypeId + ", @Rating = " + Rating + ", @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetSessionFeedbackId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetSessionFeedback Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetSessionFeedbackInfo(string AuthKey, Int64 VetSessionFeedbackId)
        {
            string data = string.Empty;
            var VetSessionFeedback = new List<VetSessionFeedback>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetSessionFeedback] @OpType = 'S', @VetSessionFeedbackId = " + VetSessionFeedbackId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetSessionFeedback.Add(new VetSessionFeedback()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetSessionFeedbackId"]),
                            VetSessionFeedbackId = Convert.ToInt32(dt.Rows[i]["VetSessionFeedbackId"]),
                            VetSessionId = Convert.ToInt32(dt.Rows[i]["VetSessionId"]),
                            FeedbackTypeId = Convert.ToInt32(dt.Rows[i]["FeedbackTypeId"]),
                            FeedbackTypeName = Convert.ToString(dt.Rows[i]["FeedbackTypeName"]),
                            Rating = Convert.ToInt32(dt.Rows[i]["Rating"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetSessionFeedback);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetSessionFeedback Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetSessionFeedbackInfoAll(string AuthKey, Int64 VetSessionId)
        {
            string data = string.Empty;
            var VetSessionFeedback = new List<VetSessionFeedback>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetSessionFeedback] @OpType = 'Get', @VetSessionId = " + VetSessionId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetSessionFeedback.Add(new VetSessionFeedback()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetSessionFeedbackId"]),
                            VetSessionFeedbackId = Convert.ToInt32(dt.Rows[i]["VetSessionFeedbackId"]),
                            VetSessionId = Convert.ToInt32(dt.Rows[i]["VetSessionId"]),
                            FeedbackTypeId = Convert.ToInt32(dt.Rows[i]["FeedbackTypeId"]),
                            FeedbackTypeName = Convert.ToString(dt.Rows[i]["FeedbackTypeName"]),
                            Rating = Convert.ToInt32(dt.Rows[i]["Rating"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetSessionFeedback);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Client Function
        #region Get Client Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientInfo(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            var Client = new List<Client>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sDefaultPicsPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/profilepic.png";
                    string sDefaultBannerPicPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/bannerpic.png";

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClient] @OpType = 'S', @ClientId = " + ClientId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        Client.Add(new Client()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                            ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                            ClientName = Convert.ToString(dt.Rows[i]["ClientName"]),
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
                            UserName = Convert.ToString(dt.Rows[i]["UserName"]),
                            Password = Convert.ToString(dt.Rows[i]["Password"]),
                            ProfilePic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dt.Rows[i]["ProfilePic"]),
                            BannerPic = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["BannerPic"])) ? sDefaultBannerPicPath : Convert.ToString(dt.Rows[i]["BannerPic"]),
                            IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"]),
                            AcTokenId = Convert.ToString(dt.Rows[i]["AcTokenId"]),
                            RegisteredBy = Convert.ToString(dt.Rows[i]["RegisteredBy"]),

                            Latitude = Convert.ToDouble(dt.Rows[i]["Latitude"]),
                            Longitude = Convert.ToDouble(dt.Rows[i]["Longitude"]),
                            Altitude = Convert.ToDouble(dt.Rows[i]["Altitude"]),

                            VetName = Convert.ToString(dt.Rows[i]["VetName"]),
                            VetAddress = Convert.ToString(dt.Rows[i]["VetAddress"]),
                            VetContactNo = Convert.ToString(dt.Rows[i]["VetContactNo"]),
                            VetEmailId = Convert.ToString(dt.Rows[i]["VetEmailId"]),

                            IsClientProfile = Convert.ToInt32(dt.Rows[i]["IsClientProfile"]),
                            IsClientPetProfile = Convert.ToInt32(dt.Rows[i]["IsClientPetProfile"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(Client);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Client User Exists
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientExists(string AuthKey, string UserName)
        {
            string data = string.Empty;
            var IsExists = new List<IsExists>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClient] @OpType = 'IsExists', @UserName = '" + UserName.Replace("'", "''") + "'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        IsExists.Add(new IsExists()
                        {
                            IsDataExists = Convert.ToInt32(dt.Rows[i]["IsExists"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(IsExists);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Client Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientInsert(string AuthKey, string ClientName, string PhoneNo, string EmailId, string UserName, string Password, string AcTokenId, string RegisteredBy)
        {
            string data = string.Empty;
            bool blnClientUser = false;
            var Client = new List<Client>();
            var IsExists = new List<IsExists>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    //string sClientBannerPicsPath = Server.MapPath(".") + "/DefaultPics/bannerpic.png";
                    //byte[] BannerPic = ConvertFileToBinary(sClientBannerPicsPath);
                    //ClientBannerPicUpdate(AuthKey, 59, BannerPic);

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClient] @OpType = 'IsExists', @UserName = '" + UserName.Replace("'", "''") + "'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["IsExists"]) == 1)
                            blnDataExists = true;

                        IsExists.Add(new IsExists()
                        {
                            IsDataExists = Convert.ToInt32(dt.Rows[i]["IsExists"])
                        });
                    }

                    if (!blnDataExists)
                    {
                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        dt = objDBHelper.FillTable("Exec [uspClient] @ClientId = 0, @ClientName = '" + ClientName.Replace("'", "''") + "', @PhoneNo = '" + PhoneNo.Replace("'", "''") + "', @EmailId = '" + EmailId.Replace("'", "''") + "', @UserName = '" + UserName.Replace("'", "''") + "', @Password = '" + Password.Replace("'", "''") + "', @AcTokenId = '" + AcTokenId.Replace("'", "''") + "', @RegisteredBy = '" + RegisteredBy.ToString() + "', @OpType = 'I'");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                            objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                            DataTable dtUser = objDBHelper.FillTable("Exec [uspGetUser] @OpType = 'GetUser', @UserName = '" + UserName.Replace("'", "''") + "', @Password = '" + Password.Replace("'", "''") + "'");
                            if (dtUser != null && dtUser.Rows.Count > 0)
                            {
                                string sDefaultPicsPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/profilepic.png";
                                string sDefaultBannerPicPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/bannerpic.png";
                                blnClientUser = true;

                                Client.Add(new Client()
                                {
                                    DataId = 2,
                                    ClientId = Convert.ToInt32(dtUser.Rows[0]["ClientId"]),
                                    ClientName = Convert.ToString(dtUser.Rows[0]["ClientName"]),
                                    Address1 = Convert.ToString(dtUser.Rows[0]["Address1"]),
                                    Address2 = Convert.ToString(dtUser.Rows[0]["Address2"]),
                                    CityId = Convert.ToInt64(dtUser.Rows[0]["City"]),
                                    City = Convert.ToString(dtUser.Rows[0]["CityName"]),
                                    POBox = Convert.ToString(dtUser.Rows[0]["POBox"]),
                                    StateId = Convert.ToInt64(dtUser.Rows[0]["State"]),
                                    State = Convert.ToString(dtUser.Rows[0]["StateName"]),
                                    CountryId = Convert.ToInt64(dtUser.Rows[0]["Country"]),
                                    Country = Convert.ToString(dtUser.Rows[0]["CountryName"]),
                                    PhoneNo = Convert.ToString(dtUser.Rows[0]["PhoneNo"]),
                                    EmailId = Convert.ToString(dtUser.Rows[0]["EmailId"]),
                                    UserName = Convert.ToString(dtUser.Rows[0]["UserName"]),
                                    Password = Convert.ToString(dtUser.Rows[0]["Password"]),
                                    IsActive = Convert.ToInt32(dtUser.Rows[0]["IsActive"]),
                                    CreatedOn = Convert.ToString(dtUser.Rows[0]["CreatedOn"]),
                                    EndDate = Convert.ToString(dtUser.Rows[0]["EndDate"]),
                                    AcTokenId = Convert.ToString(dtUser.Rows[0]["AcTokenId"]),
                                    RegisteredBy = Convert.ToString(dtUser.Rows[0]["RegisteredBy"]),
                                    ProfilePic = string.IsNullOrEmpty(Convert.ToString(dtUser.Rows[0]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dtUser.Rows[0]["ProfilePic"]),
                                    BannerPic = string.IsNullOrEmpty(Convert.ToString(dtUser.Rows[0]["BannerPic"])) ? sDefaultBannerPicPath : Convert.ToString(dtUser.Rows[0]["BannerPic"]),

                                    Latitude = Convert.ToDouble(dtUser.Rows[0]["Latitude"]),
                                    Longitude = Convert.ToDouble(dtUser.Rows[0]["Longitude"]),
                                    Altitude = Convert.ToDouble(dtUser.Rows[0]["Altitude"]),

                                    VetName = Convert.ToString(dtUser.Rows[0]["VetName"]),
                                    VetAddress = Convert.ToString(dtUser.Rows[0]["VetAddress"]),
                                    VetContactNo = Convert.ToString(dtUser.Rows[0]["VetContactNo"]),
                                    VetEmailId = Convert.ToString(dtUser.Rows[0]["VetEmailId"]),

                                    IsClientProfile = Convert.ToInt32(dtUser.Rows[0]["IsClientProfile"]),
                                    IsClientPetProfile = Convert.ToInt32(dtUser.Rows[0]["IsClientPetProfile"])
                                });
                            }
                        }
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(IsExists);
                    Context.Response.Write(data);
                }
                else
                {
                    if (blnClientUser)
                    {
                        data = serializer.Serialize(Client);
                        Context.Response.Write(data);
                    }
                    else
                    {
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(0),
                            Error = "No Record found.",
                            ErrorNumber = 999
                        });
                        data = serializer.Serialize(DataConfirmation);
                        Context.Response.Write(data);
                    }
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Client Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientUpdate(string AuthKey, Int64 ClientId, string ClientName, string Address1, string Address2, string City, string POBox, string State, string Country, string PhoneNo, string EmailId, string AcTokenId,
                                    string VetName, string VetAddress, string VetContactNo, string VetEmailId)
        {
            string data = string.Empty;
            var Client = new List<Client>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sQry = @"Exec [uspClient] 
                                    @ClientId = " + ClientId.ToString() + ", @ClientName = '" + ClientName.Replace("'", "''") + "', @Address1 = '" + Address1.Replace("'", "''") + "', @Address2 = '" + Address2.Replace("'", "''") + @"', 
                                    @City = '" + City.Replace("'", "''") + "', @POBox = '" + POBox.Replace("'", "''") + "', @State = '" + State.Replace("'", "''") + "', @Country = '" + Country.Replace("'", "''") + @"', 
                                    @PhoneNo = '" + PhoneNo.Replace("'", "''") + "', @EmailId = '" + EmailId.Replace("'", "''") + "', @AcTokenId = '" + AcTokenId.Replace("'", "''") + @"', 
                                    @VetName = '" + VetName.Replace("'", "''") + "', @VetAddress = '" + VetAddress.Replace("'", "''") + "', @VetContactNo = '" + VetContactNo.Replace("'", "''") + "', @VetEmailId = '" + VetEmailId.Replace("'", "''") + "', @OpType = 'U'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string sDefaultPicsPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/profilepic.png";
                        string sDefaultBannerPicPath = "http://admin.anivethub.com/WebService" + "/DefaultPics/bannerpic.png";

                        DataTable dtClient = objDBHelper.FillTable("Exec [uspClient] @OpType = 'S', @ClientId = " + Convert.ToString(dt.Rows[i]["ClientId"]));
                        for (int k = 0; k < dt.Rows.Count; k++)
                        {
                            blnDataExists = true;
                            Client.Add(new Client()
                            {
                                DataId = Convert.ToInt32(dtClient.Rows[k]["ClientId"]),
                                ClientId = Convert.ToInt32(dtClient.Rows[k]["ClientId"]),
                                ClientName = Convert.ToString(dtClient.Rows[k]["ClientName"]),
                                Address1 = Convert.ToString(dtClient.Rows[k]["Address1"]),
                                Address2 = Convert.ToString(dtClient.Rows[k]["Address2"]),
                                CityId = Convert.ToInt64(dtClient.Rows[k]["City"]),
                                City = Convert.ToString(dtClient.Rows[k]["CityName"]),
                                POBox = Convert.ToString(dtClient.Rows[k]["POBox"]),
                                StateId = Convert.ToInt64(dtClient.Rows[k]["State"]),
                                State = Convert.ToString(dtClient.Rows[k]["StateName"]),
                                CountryId = Convert.ToInt64(dtClient.Rows[k]["Country"]),
                                Country = Convert.ToString(dtClient.Rows[k]["CountryName"]),
                                PhoneNo = Convert.ToString(dtClient.Rows[k]["PhoneNo"]),
                                EmailId = Convert.ToString(dtClient.Rows[k]["EmailId"]),
                                UserName = Convert.ToString(dtClient.Rows[k]["UserName"]),
                                Password = Convert.ToString(dtClient.Rows[k]["Password"]),
                                ProfilePic = string.IsNullOrEmpty(Convert.ToString(dtClient.Rows[k]["ProfilePic"])) ? sDefaultPicsPath : Convert.ToString(dtClient.Rows[k]["ProfilePic"]),
                                BannerPic = string.IsNullOrEmpty(Convert.ToString(dtClient.Rows[k]["BannerPic"])) ? sDefaultBannerPicPath : Convert.ToString(dtClient.Rows[k]["BannerPic"]),
                                IsActive = Convert.ToInt32(dtClient.Rows[k]["IsActive"]),
                                CreatedOn = Convert.ToString(dtClient.Rows[k]["CreatedOn"]),
                                EndDate = Convert.ToString(dtClient.Rows[k]["EndDate"]),
                                AcTokenId = Convert.ToString(dtClient.Rows[k]["AcTokenId"]),
                                RegisteredBy = Convert.ToString(dtClient.Rows[k]["RegisteredBy"]),

                                Latitude = Convert.ToDouble(dtClient.Rows[k]["Latitude"]),
                                Longitude = Convert.ToDouble(dtClient.Rows[k]["Longitude"]),
                                Altitude = Convert.ToDouble(dtClient.Rows[k]["Altitude"]),

                                VetName = Convert.ToString(dtClient.Rows[k]["VetName"]),
                                VetAddress = Convert.ToString(dtClient.Rows[k]["VetAddress"]),
                                VetContactNo = Convert.ToString(dtClient.Rows[k]["VetContactNo"]),
                                VetEmailId = Convert.ToString(dtClient.Rows[k]["VetEmailId"]),

                                IsClientProfile = Convert.ToInt32(dtClient.Rows[k]["IsClientProfile"]),
                                IsClientPetProfile = Convert.ToInt32(dtClient.Rows[k]["IsClientPetProfile"])
                            });
                        }

                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(Client);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Client Change Password
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientChangePassword(string AuthKey, Int64 ClientId, string Password)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sQry = @"Exec [uspClient] @ClientId = " + ClientId.ToString() + ", @Password = '" + Password.Replace("'", "''") + "', @OpType = 'CP'";
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Client Profile Pic Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientProfilePicUpdate(string AuthKey, Int64 ClientId, byte[] ProfilePic)
        {
            string data = string.Empty;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sClientPicsPath = Server.MapPath(".") + "/ClientPics/" + ClientId.ToString() + ".png";
                    string sClientPicsPath_Save = "http://admin.anivethub.com/WebService" + "/ClientPics/" + ClientId.ToString() + ".png";
                    string sError = string.Empty;
                    if (ConvertBinaryToFile(sClientPicsPath, ProfilePic, ref sError))
                    {
                        Hashtable ParaValues = new Hashtable();
                        ParaValues.Add("@ClientId", ClientId);
                        ParaValues.Add("@ProfilePic", sClientPicsPath_Save);
                        ParaValues.Add("@OpType", "PP");

                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        objDBHelper.ExecuteStoredProcedure("uspClient", ParaValues, true);

                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(ClientId),
                            Error = sClientPicsPath_Save
                        });
                    }
                    else
                    {
                        Hashtable ParaValues = new Hashtable();
                        ParaValues.Add("@ClientId", ClientId);
                        ParaValues.Add("@ProfilePic", string.Empty);
                        ParaValues.Add("@OpType", "PP");

                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        objDBHelper.ExecuteStoredProcedure("uspClient", ParaValues, true);

                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = 0,
                            Error = sError
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Client Profile Pic Remove
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientProfilePicRemove(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sClientPicsPath = Server.MapPath(".") + "/ClientPics/" + ClientId.ToString() + ".png";
                    try
                    {
                        System.IO.File.Delete(sClientPicsPath);
                    }
                    catch { }

                    Hashtable ParaValues = new Hashtable();
                    ParaValues.Add("@ClientId", ClientId);
                    ParaValues.Add("@ProfilePic", string.Empty);
                    ParaValues.Add("@OpType", "PP");

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    objDBHelper.ExecuteStoredProcedure("uspClient", ParaValues, true);

                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(ClientId)
                    });
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Client Banner Pic Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientBannerPicUpdate(string AuthKey, Int64 ClientId, byte[] BannerPic)
        {
            string data = string.Empty;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sClientBannerPicsPath = Server.MapPath(".") + "/ClientBannerPics/" + ClientId.ToString() + ".png";
                    string sClientBannerPicsPath_Save = "http://admin.anivethub.com/WebService" + "/ClientBannerPics/" + ClientId.ToString() + ".png";
                    string sError = string.Empty;
                    if (ConvertBinaryToFile(sClientBannerPicsPath, BannerPic, ref sError))
                    {
                        Hashtable ParaValues = new Hashtable();
                        ParaValues.Add("@ClientId", ClientId);
                        ParaValues.Add("@BannerPic", sClientBannerPicsPath_Save);
                        ParaValues.Add("@OpType", "BP");

                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        objDBHelper.ExecuteStoredProcedure("uspClient", ParaValues, true);

                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(ClientId),
                            Error = sClientBannerPicsPath_Save
                        });
                    }
                    else
                    {
                        Hashtable ParaValues = new Hashtable();
                        ParaValues.Add("@ClientId", ClientId);
                        ParaValues.Add("@BannerPic", string.Empty);
                        ParaValues.Add("@OpType", "BP");

                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        objDBHelper.ExecuteStoredProcedure("uspClient", ParaValues, true);

                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = 0,
                            Error = sError
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Client Banner Pic Remove
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientBannerPicRemove(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sClientBannerPicsPath = Server.MapPath(".") + "/ClientBannerPics/" + ClientId.ToString() + ".png";
                    try
                    {
                        System.IO.File.Delete(sClientBannerPicsPath);
                    }
                    catch { }

                    Hashtable ParaValues = new Hashtable();
                    ParaValues.Add("@ClientId", ClientId);
                    ParaValues.Add("@BannerPic", string.Empty);
                    ParaValues.Add("@OpType", "BP");

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    objDBHelper.ExecuteStoredProcedure("uspClient", ParaValues, true);

                    blnDataExists = true;
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(ClientId)
                    });
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Client - Pet Function
        #region Client-Pet Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientPetInsert(string AuthKey, Int64 ClientId, string PetName, long PetTypeId, long PetBreedId, int Gender, int Status, string BirthDate, decimal Weight, string Description)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    string sQry = @"Exec [uspClientPet] 
                                    @ClientId = " + ClientId.ToString() + ", @PetName = '" + PetName.Replace("'", "''") + "', @PetTypeId = " + PetTypeId + ", @PetBreedId = " + PetBreedId + @", 
                                    @Gender = " + Gender + ", @Status = " + Status + ", @BirthDate =  '" + BirthDate + "', @Weight = " + Weight + ", @Description = '" + Description.Replace("'", "''") + "', @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientPetId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("FK_tblVetAppointment_tblClientPet") > 0)
                    sMessage = "Could not delete. Reference Exists for Vet Appointment";
                else if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Client-Pet Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientPetUpdate(string AuthKey, Int64 ClientPetId, string PetName, long PetTypeId, long PetBreedId, int Gender, int Status, string BirthDate, decimal Weight, string Description)
        {
            string data = string.Empty;
            var ClientPet = new List<ClientPet>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    string sQry = @"Exec [uspClientPet] 
                                    @ClientPetId = " + ClientPetId.ToString() + ", @PetName = '" + PetName.Replace("'", "''") + "', @PetTypeId = " + PetTypeId + ", @PetBreedId = " + PetBreedId + @", 
                                    @Gender = " + Gender + ", @Status = " + Status + ", @BirthDate = '" + BirthDate + "', @Weight = " + Weight + ", @Description = '" + Description.Replace("'", "''") + "', @OpType = 'U'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {

                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        DataTable dt_Vet = objDBHelper.FillTable("Exec [uspClientPet] @OpType = 'Get', @ClientId = " + Convert.ToString(dt.Rows[k]["ClientId"]));
                        for (int i = 0; i < dt_Vet.Rows.Count; i++)
                        {
                            blnDataExists = true;
                            ClientPet.Add(new ClientPet()
                            {
                                DataId = Convert.ToInt32(dt_Vet.Rows[i]["ClientPetId"]),
                                ClientPetId = Convert.ToInt32(dt_Vet.Rows[i]["ClientPetId"]),
                                ClientId = Convert.ToInt32(dt_Vet.Rows[i]["ClientId"]),
                                PetName = Convert.ToString(dt_Vet.Rows[i]["PetName"]),
                                PetTypeId = Convert.ToInt64(dt_Vet.Rows[i]["PetTypeId"]),
                                PetTypeName = Convert.ToString(dt_Vet.Rows[i]["PetTypeName"]),
                                PetBreedId = Convert.ToInt64(dt_Vet.Rows[i]["PetBreedId"]),
                                PetBreedName = Convert.ToString(dt_Vet.Rows[i]["PetBreedName"]),
                                Gender = Convert.ToInt32(dt_Vet.Rows[i]["Gender"]),
                                Status = Convert.ToInt32(dt_Vet.Rows[i]["Status"]),
                                BirthDate = Convert.ToString(dt_Vet.Rows[i]["BirthDate"]),
                                Weight = Convert.ToDecimal(dt_Vet.Rows[i]["Weight"]),
                                Description = Convert.ToString(dt_Vet.Rows[i]["Description"]),
                                CreatedOn = Convert.ToString(dt_Vet.Rows[i]["CreatedOn"]),
                                EndDate = Convert.ToString(dt_Vet.Rows[i]["EndDate"]),
                                ClientPetPicPath = Convert.ToString(dt_Vet.Rows[i]["ClientPetPicPath"])
                            });
                        }
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(ClientPet);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("FK_tblVetAppointment_tblClientPet") > 0)
                    sMessage = "Could not delete. Reference Exists for Vet Appointment";
                else if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Client-Pet Delete
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientPetDelete(string AuthKey, Int64 ClientPetId)
        {
            string data = string.Empty;
            var ClientPet = new List<ClientPet>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientPet] @ClientPetId = " + ClientPetId.ToString() + ", @OpType = 'D'");
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {

                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        DataTable dt_Vet = objDBHelper.FillTable("Exec [uspClientPet] @OpType = 'Get', @ClientId = " + Convert.ToString(dt.Rows[k]["ClientId"]));
                        for (int i = 0; i < dt_Vet.Rows.Count; i++)
                        {
                            blnDataExists = true;
                            ClientPet.Add(new ClientPet()
                            {
                                DataId = Convert.ToInt32(dt_Vet.Rows[i]["ClientPetId"]),
                                ClientPetId = Convert.ToInt32(dt_Vet.Rows[i]["ClientPetId"]),
                                ClientId = Convert.ToInt32(dt_Vet.Rows[i]["ClientId"]),
                                PetName = Convert.ToString(dt_Vet.Rows[i]["PetName"]),
                                PetTypeId = Convert.ToInt64(dt_Vet.Rows[i]["PetTypeId"]),
                                PetTypeName = Convert.ToString(dt_Vet.Rows[i]["PetTypeName"]),
                                PetBreedId = Convert.ToInt64(dt_Vet.Rows[i]["PetBreedId"]),
                                PetBreedName = Convert.ToString(dt_Vet.Rows[i]["PetBreedName"]),
                                Gender = Convert.ToInt32(dt_Vet.Rows[i]["Gender"]),
                                Status = Convert.ToInt32(dt_Vet.Rows[i]["Status"]),
                                BirthDate = Convert.ToString(dt_Vet.Rows[i]["BirthDate"]),
                                Weight = Convert.ToDecimal(dt_Vet.Rows[i]["Weight"]),
                                Description = Convert.ToString(dt_Vet.Rows[i]["Description"]),
                                CreatedOn = Convert.ToString(dt_Vet.Rows[i]["CreatedOn"]),
                                EndDate = Convert.ToString(dt_Vet.Rows[i]["EndDate"]),
                                ClientPetPicPath = Convert.ToString(dt_Vet.Rows[i]["ClientPetPicPath"])
                            });
                        }
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(ClientPet);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("FK_tblClientPetTreatment_tblClientPet") > 0)
                    sMessage = "Could not delete. Reference Exists for Pet Treatment";
                else if (sMessage.IndexOf("FK_tblVetAppointment_tblClientPet") > 0)
                    sMessage = "Could not delete. Reference Exists for Vet Appointment";
                else if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get ClientPet Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientPetInfo(string AuthKey, Int64 ClientPetId)
        {
            string data = string.Empty;
            var ClientPet = new List<ClientPet>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientPet] @OpType = 'S', @ClientPetId = " + ClientPetId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        ClientPet.Add(new ClientPet()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientPetId"]),
                            ClientPetId = Convert.ToInt32(dt.Rows[i]["ClientPetId"]),
                            ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                            PetName = Convert.ToString(dt.Rows[i]["PetName"]),
                            PetTypeId = Convert.ToInt64(dt.Rows[i]["PetTypeId"]),
                            PetTypeName = Convert.ToString(dt.Rows[i]["PetTypeName"]),
                            PetBreedId = Convert.ToInt64(dt.Rows[i]["PetBreedId"]),
                            PetBreedName = Convert.ToString(dt.Rows[i]["PetBreedName"]),
                            Gender = Convert.ToInt32(dt.Rows[i]["Gender"]),
                            Status = Convert.ToInt32(dt.Rows[i]["Status"]),
                            BirthDate = Convert.ToString(dt.Rows[i]["BirthDate"]),
                            Weight = Convert.ToDecimal(dt.Rows[i]["Weight"]),
                            Description = Convert.ToString(dt.Rows[i]["Description"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"]),
                            ClientPetPicPath1 = Convert.ToString(dt.Rows[i]["ClientPetPicPath1"]),
                            ClientPetPicPath2 = Convert.ToString(dt.Rows[i]["ClientPetPicPath2"]),
                            ClientPetPicPath3 = Convert.ToString(dt.Rows[i]["ClientPetPicPath3"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(ClientPet);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("FK_tblVetAppointment_tblClientPet") > 0)
                    sMessage = "Could not delete. Reference Exists for Vet Appointment";
                else if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get ClientPet Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientPetInfoAll(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            var ClientPet = new List<ClientPet>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientPet] @OpType = 'Get', @ClientId = " + ClientId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        ClientPet.Add(new ClientPet()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientPetId"]),
                            ClientPetId = Convert.ToInt32(dt.Rows[i]["ClientPetId"]),
                            ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                            PetName = Convert.ToString(dt.Rows[i]["PetName"]),
                            PetTypeId = Convert.ToInt64(dt.Rows[i]["PetTypeId"]),
                            PetTypeName = Convert.ToString(dt.Rows[i]["PetTypeName"]),
                            PetBreedId = Convert.ToInt64(dt.Rows[i]["PetBreedId"]),
                            PetBreedName = Convert.ToString(dt.Rows[i]["PetBreedName"]),
                            Gender = Convert.ToInt32(dt.Rows[i]["Gender"]),
                            Status = Convert.ToInt32(dt.Rows[i]["Status"]),
                            BirthDate = Convert.ToString(dt.Rows[i]["BirthDate"]),
                            Weight = Convert.ToDecimal(dt.Rows[i]["Weight"]),
                            Description = Convert.ToString(dt.Rows[i]["Description"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"]),
                            ClientPetPicPath = Convert.ToString(dt.Rows[i]["ClientPetPicPath"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(ClientPet);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("FK_tblVetAppointment_tblClientPet") > 0)
                    sMessage = "Could not delete. Reference Exists for Vet Appointment";
                else if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get ClientPet Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientPet_WeightHistory(string AuthKey, Int64 ClientPetId)
        {
            string data = string.Empty;
            var ClientPetWeightHistory = new List<ClientPetWeightHistory>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientPet] @OpType = 'PetWeight', @ClientPetId = " + ClientPetId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        ClientPetWeightHistory.Add(new ClientPetWeightHistory()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientPetWeightHistoryId"]),
                            ClientPetWeightHistoryId = Convert.ToInt32(dt.Rows[i]["ClientPetWeightHistoryId"]),
                            ClientPetId = Convert.ToInt32(dt.Rows[i]["ClientPetId"]),
                            Weight = Convert.ToDouble(dt.Rows[i]["Weight"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(ClientPetWeightHistory);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Client - PetPics Function
        #region Client-PetPics Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientPetPicsInsert(string AuthKey, Int64 ClientPetId, string PicTitle, byte[] Pic)
        {
            string data = string.Empty;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string sQry = @"Exec [uspClientPetPics] @ClientPetId = " + ClientPetId.ToString() + ", @PicTitle = '" + PicTitle.Replace("'", "''") + "', @PicPath = '', @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string sClientPetPicsPath = Server.MapPath(".") + "/ClientPetPics/" + Convert.ToString(dt.Rows[i]["ClientPetPicsId"]) + ".png";
                        string sClientPetPicsPath_Save = Convert.ToString(dt.Rows[0]["PicPath"]);

                        string sError = string.Empty;
                        if (ConvertBinaryToFile(sClientPetPicsPath, Pic, ref sError))
                        {
                            DataConfirmation.Add(new DataConfirmation()
                            {
                                DataId = Convert.ToInt32(dt.Rows[i]["ClientPetPicsId"]),
                                Error = sClientPetPicsPath_Save
                            });
                        }
                        else
                        {
                            DataConfirmation.Add(new DataConfirmation()
                            {
                                DataId = Convert.ToInt32(0),
                                Error = "No Record found.",
                                ErrorNumber = 999
                            });
                        }
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("FK_tblVetAppointment_tblClientPet") > 0)
                    sMessage = "Could not delete. Reference Exists for Vet Appointment";
                else if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Client-PetPics Delete
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientPetPicsDelete(string AuthKey, Int64 ClientPetPicsId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    string sClientPetPicsPath = Server.MapPath(".") + "/ClientPetPics/" + ClientPetPicsId.ToString() + ".png";
                    try
                    {
                        System.IO.File.Delete(sClientPetPicsPath);
                    }
                    catch { }

                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientPetPics] @ClientPetPicsId = " + ClientPetPicsId.ToString() + ", @OpType = 'D'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientPetPicsId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("FK_tblVetAppointment_tblClientPet") > 0)
                    sMessage = "Could not delete. Reference Exists for Vet Appointment";
                else if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get ClientPetPics Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientPetPicsInfo(string AuthKey, Int64 ClientPetPicsId)
        {
            string data = string.Empty;
            var ClientPetPics = new List<ClientPetPics>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientPetPics] @OpType = 'S', @ClientPetPicsId = " + ClientPetPicsId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        ClientPetPics.Add(new ClientPetPics()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientPetPicsId"]),
                            ClientPetPicsId = Convert.ToInt32(dt.Rows[i]["ClientPetPicsId"]),
                            ClientPetId = Convert.ToInt32(dt.Rows[i]["ClientPetId"]),
                            PicTitle = Convert.ToString(dt.Rows[i]["PicTitle"]),
                            PicPath = Convert.ToString(dt.Rows[i]["PicPath"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(ClientPetPics);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("FK_tblVetAppointment_tblClientPet") > 0)
                    sMessage = "Could not delete. Reference Exists for Vet Appointment";
                else if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get ClientPetPics Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientPetPicsInfoAll(string AuthKey, Int64 ClientPetId)
        {
            string data = string.Empty;
            var ClientPetPics = new List<ClientPetPics>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientPetPics] @OpType = 'Get', @ClientPetId = " + ClientPetId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        ClientPetPics.Add(new ClientPetPics()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientPetPicsId"]),
                            ClientPetPicsId = Convert.ToInt32(dt.Rows[i]["ClientPetPicsId"]),
                            ClientPetId = Convert.ToInt32(dt.Rows[i]["ClientPetId"]),
                            PicTitle = Convert.ToString(dt.Rows[i]["PicTitle"]),
                            PicPath = Convert.ToString(dt.Rows[i]["PicPath"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(ClientPetPics);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("FK_tblVetAppointment_tblClientPet") > 0)
                    sMessage = "Could not delete. Reference Exists for Vet Appointment";
                else if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Pics Upload - Download
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
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(Pic);
                bw.Flush();
                fs.Flush();
                fs.Close();
                bw.Close();
                return true;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
                return false;
            }
        }
        #endregion

        #region Set Vet TokenId
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SetVetTokenId(string AuthKey, Int64 VetId, string AcTokenId, int DeviceType, string DeviceBrand, string DeviceModel, string DeviceProduct, string DeviceSDKVersion)
        {
            string data = string.Empty;
            var DeviceTokenId = new List<DeviceTokenId>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetAcToken] @VetId = " + VetId.ToString() + ", @AcTokenId = '" + AcTokenId + "', @DeviceType = " + DeviceType + ", @DeviceBrand = '" + DeviceBrand + "', @DeviceModel = '" + DeviceModel + "', @DeviceProduct = '" + DeviceProduct + "', @DeviceSDKVersion = '" + DeviceSDKVersion + "', @OpType = 'I'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DeviceTokenId.Add(new DeviceTokenId()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            Id = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            AcTokenId = Convert.ToString(dt.Rows[i]["AcTokenId"]),
                            DeviceType = Convert.ToInt32(dt.Rows[i]["DeviceType"]),
                            DeviceBrand = Convert.ToString(dt.Rows[i]["DeviceBrand"]),
                            DeviceModel = Convert.ToString(dt.Rows[i]["DeviceModel"]),
                            DeviceProduct = Convert.ToString(dt.Rows[i]["DeviceProduct"]),
                            DeviceSDKVersion = Convert.ToString(dt.Rows[i]["DeviceSDKVersion"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DeviceTokenId);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get Vet TokenId
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetTokenId(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var DeviceTokenId = new List<DeviceTokenId>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetAcToken] @VetId = " + VetId.ToString() + ", @OpType = 'S'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DeviceTokenId.Add(new DeviceTokenId()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            Id = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            AcTokenId = Convert.ToString(dt.Rows[i]["AcTokenId"]),
                            DeviceType = Convert.ToInt32(dt.Rows[i]["DeviceType"]),
                            DeviceBrand = Convert.ToString(dt.Rows[i]["DeviceBrand"]),
                            DeviceModel = Convert.ToString(dt.Rows[i]["DeviceModel"]),
                            DeviceProduct = Convert.ToString(dt.Rows[i]["DeviceProduct"]),
                            DeviceSDKVersion = Convert.ToString(dt.Rows[i]["DeviceSDKVersion"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DeviceTokenId);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
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
            var DeviceTokenId = new List<DeviceTokenId>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientAcToken] @ClientId = " + ClientId.ToString() + ", @AcTokenId = '" + AcTokenId + "', @DeviceType = " + DeviceType + ", @DeviceBrand = '" + DeviceBrand + "', @DeviceModel = '" + DeviceModel + "', @DeviceProduct = '" + DeviceProduct + "', @DeviceSDKVersion = '" + DeviceSDKVersion + "', @OpType = 'I'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DeviceTokenId.Add(new DeviceTokenId()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                            Id = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                            AcTokenId = Convert.ToString(dt.Rows[i]["AcTokenId"]),
                            DeviceType = Convert.ToInt32(dt.Rows[i]["DeviceType"]),
                            DeviceBrand = Convert.ToString(dt.Rows[i]["DeviceBrand"]),
                            DeviceModel = Convert.ToString(dt.Rows[i]["DeviceModel"]),
                            DeviceProduct = Convert.ToString(dt.Rows[i]["DeviceProduct"]),
                            DeviceSDKVersion = Convert.ToString(dt.Rows[i]["DeviceSDKVersion"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DeviceTokenId);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get Client TokenId
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientTokenId(string AuthKey, Int64 ClientId)
        {
            string data = string.Empty;
            var DeviceTokenId = new List<DeviceTokenId>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientAcToken] @ClientId = " + ClientId.ToString() + ", @OpType = 'S'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DeviceTokenId.Add(new DeviceTokenId()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                            Id = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                            AcTokenId = Convert.ToString(dt.Rows[i]["AcTokenId"]),
                            DeviceType = Convert.ToInt32(dt.Rows[i]["DeviceType"]),
                            DeviceBrand = Convert.ToString(dt.Rows[i]["DeviceBrand"]),
                            DeviceModel = Convert.ToString(dt.Rows[i]["DeviceModel"]),
                            DeviceProduct = Convert.ToString(dt.Rows[i]["DeviceProduct"]),
                            DeviceSDKVersion = Convert.ToString(dt.Rows[i]["DeviceSDKVersion"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DeviceTokenId);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //ClientPet - Treatment Function
        #region ClientPet-Treatment Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientPetTreatmentInsert(string AuthKey, Int64 ClientPetId, Int64 SymptomsId, string Treatment, string FromDate, string ToDate)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientPetTreatment] @ClientPetId = " + ClientPetId.ToString() + ", @SymptomsId = " + SymptomsId + ", @Treatment = '" + Treatment.Replace("'", "''") + "', @FromDate = '" + FromDate + "', @ToDate = '" + ToDate + "', @OpType = 'I'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientPetTreatmentId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region ClientPet-Treatment Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientPetTreatmentUpdate(string AuthKey, Int64 ClientPetTreatmentId, Int64 SymptomsId, string Treatment, string FromDate, string ToDate)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientPetTreatment] @ClientPetTreatmentId = " + ClientPetTreatmentId.ToString() + ", @SymptomsId = " + SymptomsId + ", @Treatment = '" + Treatment.Replace("'", "''") + "', @FromDate = '" + FromDate + "', @ToDate = '" + ToDate + "', @OpType = 'U'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientPetTreatmentId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region ClientPet-Treatment Delete
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClientPetTreatmentDelete(string AuthKey, Int64 ClientPetTreatmentId)
        {
            string data = string.Empty;
            var ClientPetTreatment = new List<ClientPetTreatment>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientPetTreatment] @ClientPetTreatmentId = " + ClientPetTreatmentId.ToString() + ", @OpType = 'D'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                        objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                        DataTable dt_ClientPetTreatment = objDBHelper.FillTable("Exec [uspClientPetTreatment] @OpType = 'Get', @ClientPetId = " + Convert.ToInt32(dt.Rows[i]["ClientPetId"]));
                        for (int k = 0; k < dt_ClientPetTreatment.Rows.Count; k++)
                        {
                            blnDataExists = true;
                            ClientPetTreatment.Add(new ClientPetTreatment()
                            {
                                DataId = Convert.ToInt32(dt_ClientPetTreatment.Rows[k]["ClientPetTreatmentId"]),
                                ClientPetTreatmentId = Convert.ToInt32(dt_ClientPetTreatment.Rows[k]["ClientPetTreatmentId"]),
                                ClientPetId = Convert.ToInt32(dt_ClientPetTreatment.Rows[k]["ClientPetId"]),
                                SymptomsId = Convert.ToInt32(dt_ClientPetTreatment.Rows[k]["SymptomsId"]),
                                Treatment = Convert.ToString(dt_ClientPetTreatment.Rows[k]["Treatment"]),
                                FromDate = Convert.ToString(dt_ClientPetTreatment.Rows[k]["FromDate"]),
                                ToDate = Convert.ToString(dt_ClientPetTreatment.Rows[k]["ToDate"]),
                                CreatedOn = Convert.ToString(dt_ClientPetTreatment.Rows[k]["CreatedOn"])
                            });
                        }
                    }

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    if (blnDataExists)
                    {
                        data = serializer.Serialize(ClientPetTreatment);
                        Context.Response.Write(data);
                    }
                    else
                    {
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(0),
                            Error = "No Record found.",
                            ErrorNumber = 999
                        });
                        data = serializer.Serialize(DataConfirmation);
                        Context.Response.Write(data);
                    }
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get ClientPetTreatment Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientPetTreatmentInfo(string AuthKey, Int64 ClientPetTreatmentId)
        {
            string data = string.Empty;
            var ClientPetTreatment = new List<ClientPetTreatment>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientPetTreatment] @OpType = 'S', @ClientPetTreatmentId = " + ClientPetTreatmentId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        ClientPetTreatment.Add(new ClientPetTreatment()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientPetTreatmentId"]),
                            ClientPetTreatmentId = Convert.ToInt32(dt.Rows[i]["ClientPetTreatmentId"]),
                            ClientPetId = Convert.ToInt32(dt.Rows[i]["ClientPetId"]),
                            SymptomsId = Convert.ToInt32(dt.Rows[i]["SymptomsId"]),
                            SymptomsName = Convert.ToString(dt.Rows[i]["SymptomsName"]),
                            Treatment = Convert.ToString(dt.Rows[i]["Treatment"]),
                            FromDate = Convert.ToString(dt.Rows[i]["FromDate"]),
                            ToDate = Convert.ToString(dt.Rows[i]["ToDate"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(ClientPetTreatment);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get ClientPetTreatment Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientPetTreatmentInfoAll(string AuthKey, Int64 ClientPetId)
        {
            string data = string.Empty;
            var ClientPetTreatment = new List<ClientPetTreatment>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspClientPetTreatment] @OpType = 'Get', @ClientPetId = " + ClientPetId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        ClientPetTreatment.Add(new ClientPetTreatment()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["ClientPetTreatmentId"]),
                            ClientPetTreatmentId = Convert.ToInt32(dt.Rows[i]["ClientPetTreatmentId"]),
                            ClientPetId = Convert.ToInt32(dt.Rows[i]["ClientPetId"]),
                            SymptomsId = Convert.ToInt32(dt.Rows[i]["SymptomsId"]),
                            SymptomsName = Convert.ToString(dt.Rows[i]["SymptomsName"]),
                            Treatment = Convert.ToString(dt.Rows[i]["Treatment"]),
                            FromDate = Convert.ToString(dt.Rows[i]["FromDate"]),
                            ToDate = Convert.ToString(dt.Rows[i]["ToDate"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(ClientPetTreatment);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - Session_Ticket Function
        #region Vet-Session_Ticket Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetSession_TicketInsert(string AuthKey, Int64 VetSessionId, Int64 ClientId, Int64 VetId, Int64 LoginId, string Remarks, int Flag)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                int ShowToClient = 0;
                int ShowToVet = 0;

                if (Flag == 1)
                    ShowToClient = 1;
                else if (Flag == 2)
                    ShowToVet = 1;

                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetSession_Ticket] @VetSessionId = " + VetSessionId.ToString() + ", @ClientId = " + ClientId.ToString() + ", @VetId = " + VetId.ToString() + ", @LoginId = " + LoginId.ToString() + ", @Remarks = '" + Remarks + "', @ShowToVet = " + ShowToVet.ToString() + ", @ShowToClient = " + ShowToClient + ", @Flag = " + Flag + ", @OpType = 'I'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetSession_TicketId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetSession_Ticket Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetSession_TicketInfo_Client(string AuthKey, Int64 VetSessionId)
        {
            string data = string.Empty;
            var VetSession_Ticket = new List<VetSession_Ticket>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetSession_Ticket] @OpType = 'Client', @VetSessionId = " + VetSessionId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetSession_Ticket.Add(new VetSession_Ticket()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetSession_TicketId"]),
                            VetSession_TicketId = Convert.ToInt32(dt.Rows[i]["VetSession_TicketId"]),
                            VetSessionId = Convert.ToInt32(dt.Rows[i]["VetSessionId"]),
                            VetSessionNo = Convert.ToString(dt.Rows[i]["VetSessionNo"]),
                            IsFreeCall = Convert.ToInt32(dt.Rows[i]["IsFreeCall"]),
                            IsRefund = Convert.ToInt32(dt.Rows[i]["IsRefund"]),
                            ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                            ClientName = Convert.ToString(dt.Rows[i]["ClientName"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            VetName = Convert.ToString(dt.Rows[i]["VetName"]),
                            LoginId = Convert.ToInt32(dt.Rows[i]["LoginId"]),
                            LoginName = Convert.ToString(dt.Rows[i]["LoginName"]),
                            Remarks = Convert.ToString(dt.Rows[i]["Remarks"]),
                            ShowToVet = Convert.ToInt32(dt.Rows[i]["ShowToVet"]),
                            ShowToClient = Convert.ToInt32(dt.Rows[i]["ShowToClient"]),
                            Flag = Convert.ToInt32(dt.Rows[i]["Flag"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetSession_Ticket);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetSession_Ticket Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetSession_TicketInfo_Vet(string AuthKey, Int64 VetSessionId)
        {
            string data = string.Empty;
            var VetSession_Ticket = new List<VetSession_Ticket>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetSession_Ticket] @OpType = 'Vet', @VetSessionId = " + VetSessionId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetSession_Ticket.Add(new VetSession_Ticket()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetSession_TicketId"]),
                            VetSession_TicketId = Convert.ToInt32(dt.Rows[i]["VetSession_TicketId"]),
                            VetSessionId = Convert.ToInt32(dt.Rows[i]["VetSessionId"]),
                            VetSessionNo = Convert.ToString(dt.Rows[i]["VetSessionNo"]),
                            IsFreeCall = Convert.ToInt32(dt.Rows[i]["IsFreeCall"]),
                            IsRefund = Convert.ToInt32(dt.Rows[i]["IsRefund"]),
                            ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"]),
                            ClientName = Convert.ToString(dt.Rows[i]["ClientName"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            VetName = Convert.ToString(dt.Rows[i]["VetName"]),
                            LoginId = Convert.ToInt32(dt.Rows[i]["LoginId"]),
                            LoginName = Convert.ToString(dt.Rows[i]["LoginName"]),
                            Remarks = Convert.ToString(dt.Rows[i]["Remarks"]),
                            ShowToVet = Convert.ToInt32(dt.Rows[i]["ShowToVet"]),
                            ShowToClient = Convert.ToInt32(dt.Rows[i]["ShowToClient"]),
                            Flag = Convert.ToInt32(dt.Rows[i]["Flag"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetSession_Ticket);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - Rate Function
        #region Vet-Rate Insert
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetRateInsert(string AuthKey, Int64 VetId, string DayNo, string Normal_FromTime, string Normal_ToTime, double Normal_SessionRate, string Special_FromTime, string Special_ToTime, double Special_SessionRate, string OtherDays)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    string sQry = @"Exec [uspVetRate] 
                                    @VetId = " + VetId.ToString() + ", @DayNo = " + DayNo + @", 
                                    @Normal_FromTime = '" + Normal_FromTime.Replace("'", "''") + "', @Normal_ToTime = '" + Normal_ToTime.Replace("'", "''") + "', @Normal_SessionRate = " + Normal_SessionRate + @", 
                                    @Special_FromTime = '" + Special_FromTime.Replace("'", "''") + "', @Special_ToTime = '" + Special_ToTime.Replace("'", "''") + "', @Special_SessionRate = " + Special_SessionRate + ", @OtherDays = '" + OtherDays + "', @OpType = 'I'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetRateId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Rate Update
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetRateUpdate(string AuthKey, Int64 VetRateId, string DayNo, string Normal_FromTime, string Normal_ToTime, double Normal_SessionRate, string Special_FromTime, string Special_ToTime, double Special_SessionRate)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    string sQry = @"Exec [uspVetRate] 
                                    @VetRateId = " + VetRateId.ToString() + ", @DayNo = " + DayNo + @", 
                                    @Normal_FromTime = '" + Normal_FromTime.Replace("'", "''") + "', @Normal_ToTime = '" + Normal_ToTime.Replace("'", "''") + "', @Normal_SessionRate = " + Normal_SessionRate + @", 
                                    @Special_FromTime = '" + Special_FromTime.Replace("'", "''") + "', @Special_ToTime = '" + Special_ToTime.Replace("'", "''") + "', @Special_SessionRate = " + Special_SessionRate + @", @OpType = 'U'";
                    DataTable dt = objDBHelper.FillTable(sQry);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetRateId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Vet-Rate Delete
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VetRateDelete(string AuthKey, Int64 VetRateId)
        {
            string data = string.Empty;
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetRate] @VetRateId = " + VetRateId.ToString() + ", @OpType = 'D'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        DataConfirmation.Add(new DataConfirmation()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetRateId"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetRate Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetRateInfo(string AuthKey, Int64 VetRateId)
        {
            string data = string.Empty;
            var VetRate = new List<VetRate>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetRate] @OpType = 'S', @VetRateId = " + VetRateId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetRate.Add(new VetRate()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetRateId"]),
                            VetRateId = Convert.ToInt32(dt.Rows[i]["VetRateId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            DayNo = Convert.ToInt32(dt.Rows[i]["DayNo"]),
                            Normal_FromTime = Convert.ToString(dt.Rows[i]["Normal_FromTime"]),
                            Normal_ToTime = Convert.ToString(dt.Rows[i]["Normal_ToTime"]),
                            Normal_SessionRate = Convert.ToInt32(dt.Rows[i]["Normal_SessionRate"]),
                            Special_FromTime = Convert.ToString(dt.Rows[i]["Special_FromTime"]),
                            Special_ToTime = Convert.ToString(dt.Rows[i]["Special_ToTime"]),
                            Special_SessionRate = Convert.ToInt32(dt.Rows[i]["Special_SessionRate"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetRate);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        #region Get VetRate Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetRateInfoAll(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var VetRate = new List<VetRate>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Exec [uspVetRate] @OpType = 'Get', @VetId = " + VetId.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
                        VetRate.Add(new VetRate()
                        {
                            DataId = Convert.ToInt32(dt.Rows[i]["VetRateId"]),
                            VetRateId = Convert.ToInt32(dt.Rows[i]["VetRateId"]),
                            VetId = Convert.ToInt32(dt.Rows[i]["VetId"]),
                            DayNo = Convert.ToInt32(dt.Rows[i]["DayNo"]),
                            Normal_FromTime = Convert.ToString(dt.Rows[i]["Normal_FromTime"]),
                            Normal_ToTime = Convert.ToString(dt.Rows[i]["Normal_ToTime"]),
                            Normal_SessionRate = Convert.ToInt32(dt.Rows[i]["Normal_SessionRate"]),
                            Special_FromTime = Convert.ToString(dt.Rows[i]["Special_FromTime"]),
                            Special_ToTime = Convert.ToString(dt.Rows[i]["Special_ToTime"]),
                            Special_SessionRate = Convert.ToInt32(dt.Rows[i]["Special_SessionRate"]),
                            CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]),
                            EndDate = Convert.ToString(dt.Rows[i]["EndDate"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(VetRate);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }
                if (sMessage.IndexOf("The DELETE statement conflicted with the REFERENCE constraint") > 0)
                    sMessage = "Could not delete. Reference Exists.";

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Vet - Notifications
        #region Get Notifications Info
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetVetNotifications(string AuthKey, Int64 VetId)
        {
            string data = string.Empty;
            var Notifications = new List<Notifications>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Select * From vwNotificationGet Where Flag = 1 And Id = " + VetId.ToString() + " Order By NotificationId Desc");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
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
                            Flag = Convert.ToInt32(dt.Rows[i]["Flag"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(Notifications);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
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
            var Notifications = new List<Notifications>();
            bool blnDataExists = false;
            var DataConfirmation = new List<DataConfirmation>();
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();
                    DataTable dt = objDBHelper.FillTable("Select * From vwNotificationGet Where Flag = 0 And Id = " + ClientId.ToString() + " Order By NotificationId Desc");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        blnDataExists = true;
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
                            Flag = Convert.ToInt32(dt.Rows[i]["Flag"])
                        });
                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (blnDataExists)
                {
                    data = serializer.Serialize(Notifications);
                    Context.Response.Write(data);
                }
                else
                {
                    DataConfirmation.Add(new DataConfirmation()
                    {
                        DataId = Convert.ToInt32(0),
                        Error = "No Record found.",
                        ErrorNumber = 999
                    });
                    data = serializer.Serialize(DataConfirmation);
                    Context.Response.Write(data);
                }
            }
            catch (Exception Ex)
            {
                DataConfirmation = new List<DataConfirmation>();

                int iErrorNumber = 0;
                string sMessage = string.Empty;
                if (Ex.Message.IndexOf('|') > 0)
                {
                    iErrorNumber = Convert.ToInt32(Ex.Message.Split('|')[0]);
                    sMessage = Convert.ToString(Ex.Message.Split('|')[1]);
                }
                else
                {
                    iErrorNumber = 0;
                    sMessage = Ex.Message;
                }

                DataConfirmation.Add(new DataConfirmation() { DataId = 0, ErrorNumber = iErrorNumber, Error = sMessage });
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(DataConfirmation);
                Context.Response.Write(data);
            }
        }
        #endregion

        //Insert - Notifications
        #region Insert Notifications Info
        public void InsertNotifications(string AuthKey, Int64 ClientId, Int64 VetId, string Title, string Remarks, string ImagePath, int NotificationType)
        {
            try
            {
                string Decrypt = GeneralClass.Decrypt(AuthKey);
                WebService.DBAccess.DBHelper objDBHelper = new WebService.DBAccess.DBHelper();
                if (Decrypt == "AniVetHub")
                {
                    objDBHelper.ConnectionString = ConfigurationManager.AppSettings["Conn"].ToString();
                    objDBHelper.ProviderName = ConfigurationManager.AppSettings["Provider"].ToString();

                    string AppHeading = "AniVet Hub";
                    string AppIconPath = "http://admin.anivethub.com/WebService/DefaultPics/AppIcon.png";
                    string sQry = @"Insert Into tblNotification (ClientId, VetId, AppHeading, Title, Remarks, AppIconPath, ImagePath, NotificationType, IsSent, CreatedOn)
                                    Values (" + (ClientId == 0 ? "Null" : ClientId.ToString()) + ", " + (VetId == 0 ? "Null" : VetId.ToString()) + ", '" + AppHeading + "', '" + Title + "', '" + Remarks + "', '" + AppIconPath + "', '" + ImagePath + "', " + NotificationType.ToString() + ", 0, GetDate())";
                    objDBHelper.ExecuteNonQuery(sQry);
                }
            }
            catch { }
        }
        #endregion
    }
}