using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;

namespace WebService
{
    public class GeneralClass
    {
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }

    public class Country
    {
        public int DataId { get; set; }
        public long CountryId { get; set; }
        public string CountryName { get; set; }
    }

    public class State
    {
        public int DataId { get; set; }
        public long StateId { get; set; }
        public string StateName { get; set; }
        public long CountryId { get; set; }
    }

    public class City
    {
        public int DataId { get; set; }
        public long CityId { get; set; }
        public string CityName { get; set; }
        public long StateId { get; set; }
    }

    public class Category
    {
        public int DataId { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Icon1 { get; set; }
        public string Icon2 { get; set; }
        public int CategoryPoints { get; set; }
        public string ColorCode { get; set; }
    }

    public class Product
    {
        public int DataId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public int Point { get; set; }
        public string CreatedOn { get; set; }
        public string EndDate { get; set; }
    }

    public class Package
    {
        public int DataId { get; set; }
        public long PackageId { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }
        public int CreditPost { get; set; }
        public int CreditPoint { get; set; }
        public double Amount { get; set; }
        public string CreatedOn { get; set; }
        public string EndDate { get; set; }
    }

    public class Client
    {
        public int DataId { get; set; }
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string GenderDisp { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public long CityId { get; set; }
        public string POBox { get; set; }
        public long StateId { get; set; }
        public long CountryId { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string BirthDate { get; set; }
        public string Password { get; set; }
        public string ProfilePic { get; set; }
        public string AcTokenId { get; set; }
        public int IsActive { get; set; }
        public string CreatedOn { get; set; }
        public string EndDate { get; set; }
        public string RegisteredBy { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }

        public double Rating { get; set; }
        public double Points { get; set; }
        public double HelpMe { get; set; }
        public double Offered { get; set; }

        public double Radious { get; set; }
        public int IsClientProfile { get; set; }

        public int IsBankInformation { get; set; }
        public string BusinessTaxId { get; set; }
        public string PersonalIdNumber { get; set; }
        public string BankAccountNumber { get; set; }
        public string RoutingNumber { get; set; }

        public int PaymentMethod { get; set; }
        public string PaymentMethodDisp { get; set; }
        public string LegalDocument { get; set; }
    }

    public class JobPost
    {
        public int DataId { get; set; }
        public long JobPostId { get; set; }
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobPhoto { get; set; }
        public string JobPhoto1 { get; set; }
        public string JobPhoto2 { get; set; }
        public string JobPhoto3 { get; set; }
        public int IsFree { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon1 { get; set; }
        public string CategoryIcon2 { get; set; }
        public string CategoryColorCode { get; set; }
        public int JobPostingPoints { get; set; }
        public double JobPostingAmount { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double Latitude_1 { get; set; }
        public double Longitude_1 { get; set; }
        public double Altitude_1 { get; set; }
        public int JobTimeFlag { get; set; }
        public int JobHour { get; set; }
        public string JobDoneTime { get; set; }
        public double JobAmount { get; set; }
        public int JobAmountFlag { get; set; }
        public string PaymentTime { get; set; }
        public string PaymentId { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentResponse { get; set; }
        public string CreatedOn { get; set; }
        public string EndDate { get; set; }

        public int IsHire { get; set; }
        public double MyOfferAmount { get; set; }
        public string ChatGroupId { get; set; }

        public int RowNo { get; set; }
        public int TotalRowNo { get; set; }
        public int TotalPage { get; set; }
        public double BestOffer { get; set; }
        public string JobPostTimeDiff { get; set; }

        public object JobPostOffer { get; set; }
        public object JobPostView { get; set; }
    }

    public class JobPostOffer
    {
        public int DataId { get; set; }
        public long JobPostOfferId { get; set; }
        public long JobPostId { get; set; }
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePic { get; set; }
        public double OfferAmount { get; set; }
        public int IsHire { get; set; }
        public int IsMyOffer { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public string CreatedOn { get; set; }
        public string EndDate { get; set; }
        public string JobPostTimeDiff { get; set; }
        public string CancelReason { get; set; }
        public string IssueRemarks { get; set; }
        public string IssuePic { get; set; }
        public int JobAmountFlag { get; set; }
        public int JobHour { get; set; }
    }

    public class JobPostView
    {
        public long DataId { get; set; }
        public long JobPostViewId { get; set; }
        public long JobPostId { get; set; }
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedOn { get; set; }
    }

    public class JobPostDecline
    {
        public int DataId { get; set; }
        public long JobPostDeclineId { get; set; }
        public long JobPostId { get; set; }
        public long ClientId { get; set; }
    }

    public class ChatGroup
    {
        public int DataId { get; set; }
        public long ChatGroupId { get; set; }
        public string ChatGroupName { get; set; }
        public string ChatGroupPic { get; set; }
        public string CreatedOn { get; set; }
        public string EndDate { get; set; }
    }

    public class ChatGroupMember
    {
        public int DataId { get; set; }
        public long ChatGroupMemberId { get; set; }
        public long ChatGroupId { get; set; }
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IsAdmin { get; set; }
        public string CreatedOn { get; set; }
        public string EndDate { get; set; }
    }

    public class IdentityLocation
    {
        public int DataId { get; set; }
        public long Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
    }

    public class ClientPaymentType
    {
        public int DataId { get; set; }
        public long ClientPaymentTypeId { get; set; }
        public long ClientId { get; set; }
        public int PaymentType { get; set; }
        public string PaymentTypeName { get; set; }
        public string CreatedOn { get; set; }
        public string EndDate { get; set; }
    }

    public class ClientAppFeedback
    {
        public int DataId { get; set; }
        public long ClientAppFeedbackId { get; set; }
        public long ClientId { get; set; }
        public string AppFeedback { get; set; }
        public string CreatedOn { get; set; }
        public string EndDate { get; set; }
    }

    public class ARView
    {
        public int DataId { get; set; }
        public int ClientId { get; set; }
        public int JobPostId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double Rating { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon1 { get; set; }
        public string CategoryIcon2 { get; set; }
        public string CategoryColorCode { get; set; }
        public double Distance { get; set; }
    }

    public class DeviceTokenId
    {
        public int DataId { get; set; }
        public long Id { get; set; }
        public string AcTokenId { get; set; }
        public int DeviceType { get; set; }
        public string DeviceBrand { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceProduct { get; set; }
        public string DeviceSDKVersion { get; set; }
    }

    public class StripeResponse
    {
        public int DataId { get; set; }
        public string id { get; set; }
        public string amount { get; set; }
        public string status { get; set; }
        public string failure_code { get; set; }
        public string failure_message { get; set; }
    }

    public class DataConfirmation
    {
        public int DataId { get; set; }
        public bool IsError { get; set; }
        public int ErrorNumber { get; set; }
        public string Error { get; set; }
        public object DataConfirm_DataObject { get; set; }
    }

    public class MSGClass
    {
        public int Code { get; set; }
        public string MSG { get; set; }
    }

    public class ChatUser
    {
        public int DataId { get; set; }
        public int IsGroup { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfilePic { get; set; }
        public int IsAdmin { get; set; }
    }

    public class ChatUserData
    {
        public int DataId { get; set; }
        public int ChatUserId { get; set; }
        public int FromClientId { get; set; }
        public string FromClientName { get; set; }
        public int ToClientId { get; set; }
        public string ToClientName { get; set; }
        public string CreatedOn { get; set; }
        public string EndDate { get; set; }
    }

    public class Subscription
    {
        public int DataId { get; set; }
        public long SubscriptionId { get; set; }
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PackageId { get; set; }
        public int CreditPost { get; set; }
        public int CreditPoint { get; set; }
        public string PackageName { get; set; }
        public string PaymentAmount { get; set; }
        public string PaymentTime { get; set; }
        public string PaymentId { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentResponse { get; set; }
        public string CreatedOn { get; set; }
        public string EndDate { get; set; }
    }

    public class ProductRedeem
    {
        public int DataId { get; set; }
        public long ProductRedeemId { get; set; }
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int RedeemPoint { get; set; }
        public string ProductImage { get; set; }
        public string CreatedOn { get; set; }
        public string EndDate { get; set; }
    }

    public class Review
    {
        public int DataId { get; set; }
        public long ReviewId { get; set; }
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long JobPostId { get; set; }
        public double Rating { get; set; }
        public string ReviewData { get; set; }
        public string JobPhoto { get; set; }
        public string JobPhoto1 { get; set; }
        public string JobPhoto2 { get; set; }
        public string JobPhoto3 { get; set; }
        public string CreatedOn { get; set; }
        public string EndDate { get; set; }

        public string JobTitle { get; set; }
        public double OfferAmount { get; set; }
        public string HelpSeeker_ProfilePic { get; set; }
        public string ReviewTimeDiff { get; set; }
    }

    public class AboutUs
    {
        public int DataId { get; set; }
        public long AboutUsId { get; set; }
        public string Remarks { get; set; }
        public string CreatedOn { get; set; }
        public string EndDate { get; set; }
    }

    public class Notifications
    {
        public int DataId { get; set; }
        public long NotificationId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string AppHeading { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public string AppIconPath { get; set; }
        public string ImagePath { get; set; }
        public int NotificationType { get; set; }
        public int IsSent { get; set; }
        public string CreatedOn { get; set; }
        public int Flag { get; set; }
        public long JobPostId { get; set; }
    }

    public class ClientCategory
    {
        public int DataId { get; set; }
        public int ClientCategoryId { get; set; }
        public int ClientId { get; set; }
        public int CategoryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CategoryName { get; set; }
        public string CreatedOn { get; set; }
        public string EndDate { get; set; }
    }

    public class CrashReportEmail
    {
        public int DataId { get; set; }
        public string Status { get; set; }
    }

    public class StripeResponse_Account
    {
        public int DataId { get; set; }
        public string id { get; set; }
        public string sobject { get; set; }
        public string business_name { get; set; }
    }

    public class StripeResponse_Customer
    {
        public int DataId { get; set; }
        public string id { get; set; }
        public string default_source { get; set; }
        public string description { get; set; }
        public string email { get; set; }
    }

    public class StripeResponse_CreditCard
    {
        public int DataId { get; set; }
        public string id { get; set; }
        public string brand { get; set; }
        public string cvc_check { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
    }
}