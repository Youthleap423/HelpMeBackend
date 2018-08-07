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
using HelpMe.BusinessAccess;
using HelpMe.Entities;

namespace HelpMe
{
    public partial class Notification : System.Web.UI.Page
    {
        #region Variables

        #endregion

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
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Notification" + ConfigurationManager.AppSettings["Version"].ToString();

                if (!IsPostBack)
                {
                    ViewState["Row"] = "";
                    TitleCaption.Text = "Notification";
                    ArrayList coldata = new ArrayList();
                    ArrayList coltype = new ArrayList();

                    ddlType_SelectedIndexChanged(null, null);
                    FillClient();
                    FillNotification();
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }



        #endregion

        private void FillNotification()
        {
            try
            {
                int iNotificationId = 0;
                if (Request.QueryString["p2"] == null)
                    iNotificationId = 0;
                else
                    iNotificationId = int.Parse(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString());

                string strQur = @"select  [ClientId],[Remarks] ,[NotificationType],[KeyParameter],Title from  [dbo].[tblNotification] where NotificationId=" + iNotificationId + "";
                DataTable dtNotification = BeanHelper.DBHelper.FillTable(strQur);
                if (dtNotification.Rows.Count > 0)
                {
                    ddlType.SelectedValue = Convert.ToString(dtNotification.Rows[0]["NotificationType"]);
                    ddlClient.SelectedValue = Convert.ToString(dtNotification.Rows[0]["ClientId"]);
                    txtMessage.Text = Convert.ToString(dtNotification.Rows[0]["Remarks"]);
                    txtTitle.Text = Convert.ToString(dtNotification.Rows[0]["Title"]);
                    string[] split = dtNotification.Rows[0]["KeyParameter"].ToString().Split('|');
                    if (split.Length <= 6)
                    {
                        txtKeyParameter1.Text = Convert.ToString(split[0]);
                        txtKeyParameter2.Text = Convert.ToString(split[1]);
                        txtKeyParameter3.Text = Convert.ToString(split[2]);
                        txtKeyParameter4.Text = Convert.ToString(split[3]);
                        txtKeyParameter5.Text = Convert.ToString(split[4]);
                        txtKeyParameter6.Text = Convert.ToString(split[5]);

                    }
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }

        #region Click Event
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int iNotificationId = 0;
                if (Request.QueryString["p2"] == null)
                    iNotificationId = 0;
                else
                    iNotificationId = int.Parse(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString());

                HelpMe.Entities.Notification objNotification = new HelpMe.Entities.Notification();

                objNotification.NotificationId = iNotificationId;

                objNotification.ClientId = Convert.ToInt32(ddlClient.SelectedValue);
                objNotification.NotificationType = Convert.ToInt32(ddlType.SelectedValue);
                objNotification.Remarks = Convert.ToString(txtMessage.Text);
                objNotification.Title = Convert.ToString(txtTitle.Text);

                string strparamete = string.Empty;

                if (Convert.ToString(txtKeyParameter1.Text) != "")
                {
                    if (strparamete == "")
                        strparamete = strparamete + Convert.ToString(txtKeyParameter1.Text).Trim();
                    else
                        strparamete = Convert.ToString(txtKeyParameter1.Text).Trim();
                }
                else
                    strparamete = Convert.ToString(txtKeyParameter1.Text).Trim();

                if (Convert.ToString(txtKeyParameter2.Text) != "")
                {
                    if (strparamete == "")
                        strparamete = strparamete + Convert.ToString(txtKeyParameter2.Text).Trim();
                    else
                        strparamete = strparamete + "|" + Convert.ToString(txtKeyParameter2.Text).Trim();
                }
                else
                    strparamete = strparamete + "|" + Convert.ToString(txtKeyParameter2.Text).Trim();

                if (Convert.ToString(txtKeyParameter3.Text) != "")
                {
                    if (strparamete == "")
                        strparamete = strparamete + Convert.ToString(txtKeyParameter3.Text).Trim();
                    else
                        strparamete = strparamete + "|" + Convert.ToString(txtKeyParameter3.Text).Trim();
                }
                else
                    strparamete = strparamete + "|" + Convert.ToString(txtKeyParameter3.Text).Trim();

                if (Convert.ToString(txtKeyParameter4.Text) != "")
                {
                    if (strparamete == "")
                        strparamete = strparamete + Convert.ToString(txtKeyParameter4.Text).Trim();
                    else
                        strparamete = strparamete + "|" + Convert.ToString(txtKeyParameter4.Text).Trim();
                }
                else
                    strparamete = strparamete + "|" + Convert.ToString(txtKeyParameter4.Text).Trim();

                if (Convert.ToString(txtKeyParameter5.Text) != "")
                {
                    if (strparamete == "")
                        strparamete = strparamete + Convert.ToString(txtKeyParameter5.Text).Trim();
                    else
                        strparamete = strparamete + "|" + Convert.ToString(txtKeyParameter5.Text).Trim();
                }
                else
                    strparamete = strparamete + "|" + Convert.ToString(txtKeyParameter5.Text).Trim();

                if (Convert.ToString(txtKeyParameter6.Text) != "")
                {
                    if (strparamete == "")
                        strparamete = strparamete + Convert.ToString(txtKeyParameter6.Text).Trim();
                    else
                        strparamete = strparamete + "|" + Convert.ToString(txtKeyParameter6.Text).Trim();
                }
                else
                    strparamete = strparamete + "|" + Convert.ToString(txtKeyParameter6.Text).Trim();

                objNotification.KeyParameter = strparamete;



                if (iNotificationId == 0)
                {
                    string SQryInsert = @"INSERT INTO [dbo].[tblNotification]
           ([ClientId],[AppHeading],[Title],[Remarks],[AppIconPath],[ImagePath],[NotificationType],[IsSent],[CreatedOn],[KeyParameter])
     VALUES
           (" + objNotification.ClientId + ",'UHelpMe','" + objNotification.Title + "','" + objNotification.Remarks + "','',''," + objNotification.NotificationType + ",1,GETDATE(),'" + strparamete + "')";
                    BeanHelper.DBHelper.ExecuteNonQuery(SQryInsert);

                }
                else
                {
                    string SQry = @"update tblNotification set ClientId=" + objNotification.ClientId + ",Title='" + objNotification.Title + "',NotificationType=" + objNotification.NotificationType + ",KeyParameter='" + strparamete + "',Remarks='" + objNotification.Remarks + "' where NotificationId=" + iNotificationId + "";
                    BeanHelper.DBHelper.ExecuteNonQuery(SQry);
                }

                Response.Redirect("~/Search.aspx?p1=1&p2=" + HtmlSerializer.ObjectToHTML("Notification"), false);
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Search.aspx?p1=1&p2=" + HtmlSerializer.ObjectToHTML("Notification"), false);
        }
        #endregion

        protected void FillClient()
        {
            try
            {
                ddlClient.DataSource = BeanHelper.DBHelper.FillTable(@"Select ClientId, FirstName +' '+ LastName as ClientName  from tblClient where   EndDate is null order by FirstName asc");
                ddlClient.DataTextField = "ClientName";
                ddlClient.DataValueField = "ClientId";
                ddlClient.DataBind();
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlType.SelectedValue == "1")
                    divClinet.Visible = true;

            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
    }
}