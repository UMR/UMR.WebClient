using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class Oracle_EmergencyTransferFormRequest : System.Web.UI.Page
{
    public long PatientKey
    {
        get
        {
            long id = 0;
            if (Request.QueryString["PatientKey"] != null)
            {
                id = Convert.ToInt64(Request.QueryString["PatientKey"]);
            }
            return id;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PopulateControls();
        }
    }

    private void PopulateControls()
    {
        txtDate.Text = DateTime.Now.ToShortDateString();
        DataSet dsPatient = PatientManager.GetPatientDemographics(PatientKey, User.Identity.Name);
        if (dsPatient != null && dsPatient.Tables.Count > 0)
        {
            this.Page.Title = "Emergency Transfer Form Request(" + dsPatient.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsPatient.Tables[0].Rows[0]["LastName"].ToString() + ")";
            lblPatientName.Text = "Emergency Transfer Form Request(" + dsPatient.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsPatient.Tables[0].Rows[0]["LastName"].ToString() + ")";
        }


        DataSet ds = PatientManager.GetInstitutionsForPatients(PatientKey);
        grdInstitutions.DataSource = ds;
        grdInstitutions.DataBind();


        DataSet dsPrimary = PatientManager.GetPrincipalHealthCareInfo(PatientKey, User.Identity.Name);
        if (dsPrimary != null && dsPrimary.Tables.Count > 0)
        {
            lnkPrimaryProvider.Visible = true;
            string name = dsPrimary.Tables[0].Rows[0]["ProviderFirstName"].ToString() + " " + dsPrimary.Tables[0].Rows[0]["ProviderLastName"].ToString();
            lnkPrimaryProvider.Text = name;
            lnkPrimaryProvider.CommandArgument = dsPrimary.Tables[0].Rows[0]["Email"].ToString() + "||" + dsPrimary.Tables[0].Rows[0]["ProviderPhoneNo"].ToString();
            lnkPrimaryProvider.ToolTip = dsPrimary.Tables[0].Rows[0]["Email"].ToString();
        }
        else
        {
            lnkPrimaryProvider.Visible = false;
        }


        DataSet dsEmergency = PatientManager.GetEmergencyContact(PatientKey, User.Identity.Name);
        grdEmeractgencyCont.DataSource = dsEmergency;
        grdEmeractgencyCont.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string temp = txtTemperature.Text.Trim();
        char tempUnit = Convert.ToChar(ddlTemperatureUnit.SelectedValue);
        string bloodPressureSys = txtBloodPressureSystolic.Text.Trim();
        string bloodPressureDia = txtBloodPressureDiastolic.Text.Trim();
        string pulse = txtPulse.Text.Trim();
        string pulseType = rdoPulseRegular.Checked ? "Regular" : "Irregular";
        string respiratory = txtRespiratoryRate.Text.Trim();
        string bmiWeight = txtWeight.Text.Trim();
        string bmiHeight = txtHeight.Text.Trim();
        string painScale = ddlScale.SelectedValue;
        string painLocation = txtLocation.Text.Trim();
        string pupilSizeRight = txtPupilSizeRight.Text.Trim();
        string pupilSizeLeft = txtPupilSizeRight.Text.Trim();
        string bloodGlucoseLevel = txtBloodGlucoseLevel.Text.Trim();

        DateTime date = DateTime.Now;

        try
        {
            date = DateTime.Parse(txtRespiratoryRate.Text.Trim());
        }
        catch { }

        string chiefComplain = txtChiefComplain.Text.Trim();

        string Error;
        int rowAffected = PatientManager.AddVitalSigns(PatientKey, date, temp, tempUnit, bloodPressureSys, bloodPressureDia, pulse, pulseType, respiratory, chiefComplain, bmiWeight, bmiHeight, painScale, painLocation, pupilSizeRight, pupilSizeLeft, bloodGlucoseLevel, out Error);
        if (rowAffected <= 0)
        {
            lblMessage.Text = Error;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
        }
        else
        {
            lblMessage.Text = "Saved successfully";
            lblMessage.ForeColor = Color.Green;
            lblMessage.Visible = true;
            //ClearControls();
        }

        btnSave.Visible = false;
        dvSendReportTo.Visible = true;
    }

    private void ClearControls()
    {
        txtTemperature.Text = "";
        txtBloodPressureSystolic.Text = "";
        txtPulse.Text = "";
        txtRespiratoryRate.Text = "";
        txtDate.Text = "";
        txtChiefComplain.Text = "";
    }
    protected void lnkHospital_Click(object sender, EventArgs e)
    {
        string email = ((LinkButton)sender).CommandArgument;
        if (email.Trim().Length > 0)
        {
            PatientManager.SendReportTo(PatientKey, email);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('report sent')", true);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('no email found')", true);
        }
    }
    protected void lnkPrimaryProvider_Click(object sender, EventArgs e)
    {
        lnkHospital_Click(sender, e);
    }
    protected void lnkFamilyMember_Click(object sender, EventArgs e)
    {
        lnkHospital_Click(sender, e);
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        List<string> emails = new List<string>();
        foreach (GridViewRow row in grdInstitutions.Rows)
        {
            CheckBox chkEmail = row.FindControl("chkEmail") as CheckBox;
            if (chkEmail != null && chkEmail.Checked == true)
            {
                HiddenField hdnEmail = row.FindControl("hdnEmail") as HiddenField;
                if (hdnEmail != null && hdnEmail.Value.Trim().Length > 0)
                {
                    emails.Add(hdnEmail.Value.Trim());
                }
            }
        }
        foreach (GridViewRow row in grdEmeractgencyCont.Rows)
        {
            CheckBox chkEmail = row.FindControl("chkEmail") as CheckBox;
            if (chkEmail != null && chkEmail.Checked == true)
            {
                HiddenField hdnEmail = row.FindControl("hdnEmail") as HiddenField;
                if (hdnEmail != null && hdnEmail.Value.Trim().Length > 0)
                {
                    emails.Add(hdnEmail.Value.Trim());
                }
            }
        }
        if (chkProviderEmail.Checked)
        {
            if (lnkPrimaryProvider.CommandArgument.Trim().Length > 0)
            {
                string[] strs = lnkPrimaryProvider.CommandArgument.Trim().Split(new string[] { "||" }, StringSplitOptions.None);
                if (strs.Length > 0)
                {
                    if (strs[0].Trim().Length > 0)
                    {
                        emails.Add(strs[0]);
                    }
                }
            }
        }

        string[] es = txtEmails.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < es.Length; i++)
        {
            emails.Add(es[i]);
        }

        foreach (string email in emails)
        {
            PatientManager.SendReportTo(PatientKey, email);
        }
        txtEmails.Text = "";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('report sent')", true);
    }

    protected void btnSendSMS_Click(object sender, EventArgs e)
    {

        List<string> phones = new List<string>();
        foreach (GridViewRow row in grdInstitutions.Rows)
        {
            CheckBox chkEmail = row.FindControl("chkEmail") as CheckBox;
            if (chkEmail != null && chkEmail.Checked == true)
            {
                HiddenField hdnPhone = row.FindControl("hdnPhone") as HiddenField;
                if (hdnPhone != null && hdnPhone.Value.Trim().Length > 0)
                {
                    phones.Add(hdnPhone.Value.Trim());
                }
            }
        }
        foreach (GridViewRow row in grdEmeractgencyCont.Rows)
        {
            CheckBox chkEmail = row.FindControl("chkEmail") as CheckBox;
            if (chkEmail != null && chkEmail.Checked == true)
            {
                HiddenField hdnPhone = row.FindControl("hdnPhone") as HiddenField;
                if (hdnPhone != null && hdnPhone.Value.Trim().Length > 0)
                {
                    phones.Add(hdnPhone.Value.Trim());
                }
            }
        }
        if (chkProviderEmail.Checked)
        {
            if (lnkPrimaryProvider.CommandArgument.Trim().Length > 0)
            {
                string[] strs = lnkPrimaryProvider.CommandArgument.Trim().Split(new string[] { "||" }, StringSplitOptions.None);
                if (strs.Length > 1)
                {
                    if (strs[1].Trim().Length > 0)
                    {
                        phones.Add(strs[1]);
                    }
                }
            }
        }

        string[] es = txtPhones.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < es.Length; i++)
        {
            phones.Add(es[i]);
        }


        PatientManager.SendSMSTo(PatientKey, phones);

        txtPhones.Text = "";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('report sent')", true);
    }
}