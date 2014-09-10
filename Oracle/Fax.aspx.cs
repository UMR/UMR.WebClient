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

public partial class Oracle_Fax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["faxHidden"] = Request["faxHidden"];
            ViewState["subjectFax"] = Request["subjectFax"];
        }
    }


    protected void btnSendFax_Click(object sender, EventArgs e)
    {
        postbackCount.Value = (Int32.Parse(postbackCount.Value) + 1).ToString();

        if (ViewState["faxHidden"] == null) return;
        string faxBody = ViewState["faxHidden"].ToString();
        string subject = ViewState["subjectFax"].ToString();
        string faxNo = txtFaxNo.Text.Trim();
        string recipientName = txtRecipientName.Text.Trim();

        string faxMessage = "<div><h3>" + subject + "</h3>" + faxBody + "</div>";
        try
        {
            Utility.FaxSender.SendFax(faxMessage, recipientName, faxNo);
            ClientScript.RegisterStartupScript(this.GetType(), "successscript", "alert('Fax Successfully Sent');", true);
            //lblMessage.Text = "Mail Successfully Sent";

        }
        catch (Exception ex)
        {
            //lblMessage.Text = ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "successscript", "alert('" + ex.Message + "');", true);
        }
    }
}
