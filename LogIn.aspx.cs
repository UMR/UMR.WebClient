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

public partial class LogIn : System.Web.UI.Page
{
    private string _defaultLedgendUser = "wayne";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtUserName.Focus();
        if (!String.IsNullOrEmpty(Request["auto_login"]) && Boolean.Parse(Request["auto_login"]) == true)
        {
            if (string.IsNullOrEmpty(Request.Form["USERNAME"])) return;
            //***********************
            string userName = Request.Form["USERNAME"].Trim();
            string Password = Request.Form["PASSWORD"].Trim();
            string errorMessage;
            bool validUser = PatientManager.ValidateUser(userName, Password, out errorMessage);
            if (validUser)
            {
                FormsAuthentication.SetAuthCookie(userName, false);
                FormsAuthentication.RedirectFromLoginPage(userName, false);
                PatientManager.UsagesLoginAdd(userName);
            }
            else
            {
                Label errMsg = FindControl("lblErrMsg") as Label;
                if (errMsg != null)
                    errMsg.Text = errorMessage;
            }
            //***********************
        }
    }

    /*
      Date: 20 May 2010 By: Animesh
      Scenario: When Trial user login and its legend is not 
      then set 'Dr. Wayne	Wells' ledgend as a default ledgend
    */
    private void LegendCreator()
    {
        string userId = txtUserName.Text.Trim().ToUpper();
        DataSet dsLegend = PatientManager.GetLegendByUserID(userId);
        if (dsLegend == null || dsLegend.Tables[0].Rows.Count <= 0)
        {
            if (userId == "TRIALUSER1" || userId == "TRIALUSER2" || userId == "TRIALUSER3")
            {
                dsLegend = PatientManager.GetLegendByUserID(_defaultLedgendUser.ToUpper());
                if (dsLegend == null || dsLegend.Tables[0].Rows.Count <= 0)
                {
                    PatientManager.LegendAddNewDefault(userId.ToLower());
                }
                else
                {
                    /* USER_ID, DAYRANGE1, DAYRANGE2, DAYRANGE3, DAYRANGE4 */
                    PatientManager.LegendAdNew(userId.ToLower(), Convert.ToDateTime(dsLegend.Tables[0].Rows[0]["FirstDate"]),
                        Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DAYRANGE2"]),
                        Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DAYRANGE3"]),
                        Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DAYRANGE4"]));
                }
            }
            else
            {
                PatientManager.LegendAddNewDefault(userId.ToLower());
            }
        }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        //***********************
        string userName = txtUserName.Text.Trim();
        string Password = txtPassword.Text.Trim();
        string errorMessage;
        bool validUser = PatientManager.ValidateUser(userName, Password, out errorMessage);
        if (validUser)
        {
            FormsAuthentication.SetAuthCookie(userName, false);
            FormsAuthentication.RedirectFromLoginPage(userName, false);
            PatientManager.UsagesLoginAdd(userName);
            this.LegendCreator();
        }
        else
        {
            Label errMsg = FindControl("lblErrMsg") as Label;
            if (errMsg != null)
                errMsg.Text = errorMessage;
        }
        //***********************
    }

}
