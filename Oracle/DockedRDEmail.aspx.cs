using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

public partial class Oracle_DockedRDEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["CommunicationPageLoaded"] = true;
            ViewState["emailHidden"] = Request["emailHidden"];
            ViewState["subject"] = Request["subject"];
        }
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        postbackCount.Value = (Int32.Parse(postbackCount.Value) + 1).ToString();

        if (ViewState["emailHidden"] == null) return;
        string mailBody = ViewState["emailHidden"].ToString();
        string subject = ViewState["subject"].ToString();

        try
        {
            EmailManager client = new EmailManager(txtEmailAddress.Text.Trim(), subject, true, mailBody);
            client.Send();
            ClientScript.RegisterStartupScript(this.GetType(), "successscript", "alert('Mail Successfully Sent');", true);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "successscript", "alert('" + ex.Message + "');", true);
        }
    }
}