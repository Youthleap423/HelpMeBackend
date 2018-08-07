using HelpMe.BusinessAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using HelpMe.Shared.Utilities;
using HelpMe.Helpers;

namespace HelpMe
{
    public partial class ClientInfo_Old : System.Web.UI.Page
    {
        #region Page_Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.ValidateSession())
                {
                    Response.Redirect("~/index.aspx", false);
                    return;
                }
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Client Info - Version : " + ConfigurationManager.AppSettings["Version"].ToString();

                try
                {
                    if (Request.QueryString["p2"] != null)
                    {
                        HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString();
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Client"), false);
                }

                if (!IsPostBack)
                {
                    if (Request.QueryString["p2"] != null)
                    {
                        FillCountry();
                        DisplayData();
                    }
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }
        #endregion

        #region Function
        protected void DisplayData()
        {
            try
            {
                if (Request.QueryString["p2"] != null)
                {
                    DataTable dt = BeanHelper.ClientBean.GetData(ConversionHelper.ConvertToInt32(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString()));
                    if (dt.Rows.Count > 0)
                    {

                        txtfirstname.Text = Convert.ToString(dt.Rows[0]["FirstName"]);
                        txtlastname.Text = Convert.ToString(dt.Rows[0]["LastName"]);
                        txtAddress1.Text = Convert.ToString(dt.Rows[0]["Address1"]);
                        txtAddress2.Text = Convert.ToString(dt.Rows[0]["Address2"]);
                        ddlCountry.SelectedValue = Convert.ToString(dt.Rows[0]["Country"]);
                        ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["State"]);
                        ddlCity.SelectedValue = Convert.ToString(dt.Rows[0]["City"]);
                        txtPOBox.Text = Convert.ToString(dt.Rows[0]["POBox"]);
                        txtEmailId.Text = Convert.ToString(dt.Rows[0]["EmailId"]);
                        txtPhoneNo.Text = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                        txtpassword.Text = Convert.ToString(dt.Rows[0]["Password"]);
                        txtCreditPoint.Text = Convert.ToString(dt.Rows[0]["CreditPoint"]);

                        if (Convert.ToInt32(dt.Rows[0]["Gender"]) == 1)
                            rdogender.SelectedValue = "1";
                        else
                            rdogender.SelectedValue = "2";


                        if (Convert.ToInt32(dt.Rows[0]["IsActive"]) == 1)
                            ChkIsActive.Checked = true;
                        else
                            ChkIsActive.Checked = false;

                        if (Convert.ToInt32(dt.Rows[0]["IsClientProfile"]) == 1)
                            ChkIsClientProfile.Checked = true;
                        else
                            ChkIsClientProfile.Checked = false;
                        ddlCountry_SelectedIndexChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }
        #endregion

        #region Client
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int iClientId = 0;
                if (Request.QueryString["p2"] == null)
                    iClientId = 0;
                else
                    iClientId = int.Parse(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString());

                HelpMe.Entities.Client objClient = new HelpMe.Entities.Client();

                objClient.ClientId = iClientId;
                objClient.FirstName = Convert.ToString(txtfirstname.Text.Trim()).Replace("'", "''").Trim();
                objClient.LastName = Convert.ToString(txtlastname.Text);
                objClient.Address1 = Convert.ToString(txtAddress1.Text);
                objClient.Address2 = Convert.ToString(txtAddress2.Text);
                if (ddlCity.SelectedValue != null || ddlCity.SelectedValue != "")
                    objClient.City = Convert.ToInt64(ddlCity.SelectedValue);
                if (ddlState.SelectedValue != null || ddlState.SelectedValue != "")
                    objClient.State = Convert.ToInt64(ddlState.SelectedValue);
                if (ddlCountry.SelectedValue != null || ddlCountry.SelectedValue != "" || ddlCountry.SelectedValue != "-- Select Country --")
                    objClient.Country = Convert.ToInt64(ddlCountry.SelectedValue);
                objClient.POBox = Convert.ToString(txtPOBox.Text);

                objClient.Address1 = Convert.ToString(txtEmailId.Text);
                objClient.EmailId = Convert.ToString(txtEmailId.Text);
                objClient.PhoneNo = Convert.ToString(txtPhoneNo.Text);
                objClient.IsActive = ChkIsActive.Checked == true ? 1 : 0;
                if (txtCreditPoint.Text == null || txtCreditPoint.Text == "")
                    txtCreditPoint.Text = "0";
                objClient.CreditPoint = Convert.ToInt32(txtCreditPoint.Text);
                objClient.Gender = Convert.ToInt32(rdogender.SelectedValue);
                objClient.IsClientProfile = ChkIsClientProfile.Checked == true ? 1 : 0;

                int ReturnValue;
                BeanHelper.ClientBean.ObjClient = objClient;
                if (iClientId == 0)
                    ReturnValue = BeanHelper.ClientBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.I);
                else
                    ReturnValue = BeanHelper.ClientBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.U);

                if (ReturnValue > 0)
                {
                    if (iClientId == 0)
                        lblErrorMsg.Text = "Client Saved Successfully.";
                    else
                        lblErrorMsg.Text = "Client Updated Successfully.";
                }

                Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Client"), false);
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }
        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Client"), false);
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }
        #endregion

        protected void FillCountry()
        {
            try
            {
                DataTable dt = BeanHelper.DBHelper.FillTable("Select CountryId, CountryName from tblCountry");
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataSource = dt;
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, "-- Select Country --");

            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Int64 iCountryId = 0;
                if (ddlCountry.SelectedValue == "-- Select Country --")
                    iCountryId = 0;
                else
                    iCountryId = Convert.ToInt64(ddlCountry.SelectedValue);

                DataTable dt = BeanHelper.DBHelper.FillTable("Select StateId, StateName, CountryId from tblState where CountryId=" + iCountryId + "");
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "StateId";
                ddlState.Items.Insert(0, "-- Select State --");
                if (dt.Rows.Count > 0)
                {
                    ddlState.DataSource = dt;
                    ddlState.DataBind();
                    ddlState_SelectedIndexChanged(null, null);
                }
                else
                    ddlCity.Items.Insert(0, "-- Select City --");

            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Int64 iStateId = 0;
                if (ddlState.SelectedValue == "-- Select State --")
                    iStateId = 0;
                else
                    iStateId = Convert.ToInt64(ddlState.SelectedValue);
                DataTable dt = BeanHelper.DBHelper.FillTable("Select CityId,CityName,StateId from tblCity where StateId=" + iStateId + "");
                ddlCity.DataTextField = "CityName";
                ddlCity.DataValueField = "CityId";
                ddlCity.Items.Insert(0, "-- Select City --");
                if (dt.Rows.Count > 0)
                {
                    ddlCity.DataSource = dt;
                    ddlCity.DataBind();
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }


    }
}