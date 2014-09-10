using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

public partial class Oracle_NurseNotes : System.Web.UI.Page
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

    private void FillNurses()
    {
        DataSet dsNurses = PatientManager.GetNurses();

        if (ddlNurse.Items != null)
        {
            ddlNurse.Items.Clear(); 
        }

        int index = 0;
        foreach (DataRow dr in dsNurses.Tables[0].Rows)
        {
            ListItem li = new ListItem(dsNurses.Tables[0].Rows[index]["FIRSTNAME"].ToString() + " " + dsNurses.Tables[0].Rows[index]["LASTNAME"].ToString(), dsNurses.Tables[0].Rows[index]["NURSE_ID"].ToString());
            ddlNurse.Items.Add(li);
            index++;
        }
    }

    private void PopulateControls()
    {
        txtDate.Text = DateTime.Now.ToShortDateString();
        txtTime.Text = DateTime.Now.Hour.ToString()+":"+DateTime.Now.Minute.ToString();
        DataSet dsPatient = PatientManager.GetPatientDemographics(PatientKey, User.Identity.Name);
        if (dsPatient != null && dsPatient.Tables.Count > 0)
        {
            this.Page.Title = "Provider Alert Network(" + dsPatient.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsPatient.Tables[0].Rows[0]["LastName"].ToString() + ")";
            lblPatientName.Text = "Provider Alert Network For " + dsPatient.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsPatient.Tables[0].Rows[0]["LastName"].ToString();
        }

        int alertCount = PatientManager.GetPatientGetActiveAlertCount(PatientKey, (byte)EnumAlertStatus.NurseDone);

        if (alertCount <= 0)
        {
                int docAlertCount = PatientManager.GetPatientGetActiveDOCAlertCount(PatientKey, (byte)EnumAlertStatus.DocDone);
                if (docAlertCount <= 0)
                {

                    #region Nurse
                    this.Page.Title = "Nurse Alert Network(" + dsPatient.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsPatient.Tables[0].Rows[0]["LastName"].ToString() + ")";
                    lblPatientName.Text = "Nurse Alert Network For " + dsPatient.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsPatient.Tables[0].Rows[0]["LastName"].ToString();

                    DataSet dsPrimary = PatientManager.GetPrincipalHealthCareInfo(PatientKey, User.Identity.Name);
                    if (dsPrimary != null && dsPrimary.Tables.Count > 0)
                    {
                        lnkPrimaryProvider.Visible = true;
                        string name = dsPrimary.Tables[0].Rows[0]["ProviderFirstName"].ToString() + " " + dsPrimary.Tables[0].Rows[0]["ProviderLastName"].ToString();
                        lnkPrimaryProvider.Text = name;
                        lnkPrimaryProvider.CommandArgument = dsPrimary.Tables[0].Rows[0]["Email"].ToString() + "||" + dsPrimary.Tables[0].Rows[0]["ProviderPhoneNo"].ToString();
                        lnkPrimaryProvider.ToolTip = dsPrimary.Tables[0].Rows[0]["Email"].ToString();

                        if (String.IsNullOrEmpty(dsPrimary.Tables[0].Rows[0]["Email"].ToString()))
                        {
                            dvMissingEmails.Visible = true;
                            rfvEmail.Enabled = true;
                        }
                        else
                        {
                            dvMissingEmails.Visible = false;
                            rfvEmail.Enabled = false;
                        }

                        chkProviderEmail.Checked = true;
                        chkProviderEmail.Enabled = false;
                    }
                    else
                    {
                        dvMissingEmails.Visible = true;
                        rfvEmail.Enabled = true;

                        lnkPrimaryProvider.Visible = false;
                        chkProviderEmail.Checked = false;
                        chkProviderEmail.Enabled = false;
                    }

                    FillNurses();
                    dvImageUpLoader.Visible = true;
                    txtDate.Enabled = true;

                    txtNurseSubjective.Text = string.Empty;
                    //txtObjective.Text = string.Empty;
                    txtNurseAssessment.Text = string.Empty;
                    txtNurseNote.Text = string.Empty;


                    txtTemperature.Text = string.Empty;
                    ddlTemperatureUnit.SelectedIndex = 0;
                    ddlHeightUnit.SelectedIndex = 0;
                    ddlWeightUnit.SelectedIndex = 0;
                    txtBloodPressureSystolic.Text = string.Empty;
                    txtBloodPressureDiastolic.Text = string.Empty;

                    rdoPulseIrregular.Checked = false;
                    rdoPulseRegular.Checked = true;

                    txtPulse.Text = string.Empty;
                    //txtTemperature.Text = dsVitalSigns.Tables[0].Rows[0][""].ToString();
                    txtRespiratoryRate.Text = string.Empty;
                    txtWeight.Text = string.Empty;
                    txtHeight.Text = string.Empty;
                    ddlScale.SelectedIndex = 0;
                    txtLocation.Text = string.Empty;
                    txtPupilSizeRight.Text = string.Empty;
                    txtPupilSizeRight.Text = string.Empty;
                    txtBloodGlucoseLevel.Text = string.Empty;
                    lblPhysicianName.Text = "";

                    lblHeightInch.Visible = false;
                    txtHeightInch.Visible = false;
                    txtHeightInch.Text = string.Empty;

                    txtNurseSubjective.ReadOnly = false;
                    //txtObjective.ReadOnly = false;

                    pnlNurse.Visible = true;
                    pnlNurseMail.Visible = true;
                    pnlNurseNote.Visible = true;
                    pnlVital.Visible = true;

                    gdvPhoto.DataSource = null;
                    gdvPhoto.DataBind();

                    pnlPhysician.Visible = false;
                    pnlPhysicianMail.Visible = false;
                    pnlPhysicianPlan.Visible = false;
                    pnlVitalDoc.Visible = false;
                    dvSendReportTo.Visible = false;
                    pnlETFRNotes.Visible = false;
                    pnlEtrfnMail.Visible = false;
                    pnlETRF.Visible = false;

                    chkSenSMS.Checked = false;
                    dvMail.Visible = true;
                    dvTxtMailTo.Visible = true;


                    #endregion
                }
                else
                {
                    #region ETFR
                    this.Page.Title = "Patient Emergency Transfer Network(" + dsPatient.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsPatient.Tables[0].Rows[0]["LastName"].ToString() + ")";
                    lblPatientName.Text = "Patient Emergency Transfer Network For " + dsPatient.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsPatient.Tables[0].Rows[0]["LastName"].ToString();
                    
                    DataSet ds = PatientManager.GetInstitutionsForPatients(PatientKey);
                    if (ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                    {
                        lblNoInst.Text = "";
                        grdInstitutions.DataSource = ds;
                        grdInstitutions.DataBind();
                    }
                    else
                    {
                        lblNoInst.Text = "No institutions provided";
                    }

                    DataSet dsPlans = PatientManager.GetPlans();
                    rblPlan.DataSource = dsPlans;
                    rblPlan.DataBind();

                    DataSet dsEmergency = PatientManager.GetEmergencyContact(PatientKey, User.Identity.Name);
                    if (dsEmergency.Tables[0].Rows != null && dsEmergency.Tables[0].Rows.Count>0)
                    {
                        lblNoFam.Text = "";
                        grdEmeractgencyCont.DataSource = dsEmergency;
                        grdEmeractgencyCont.DataBind();
                    }
                    else
                    {
                        lblNoFam.Text = "No emergency contact provided";
                    }


                    DataSet dsAlert = PatientManager.GetPatientGetActiveDOCAlert(PatientKey, (byte)EnumAlertStatus.DocDone);
                    long alertID = Int64.Parse(dsAlert.Tables[0].Rows[0]["ALERT_ID"].ToString());

                    DataSet dsNurseSoap = PatientManager.GetPatientNurseSoapsByAlertID(alertID);
                    DataSet dsSoap = PatientManager.GetPatientDOCSoapsByAlertID(alertID);
                    int vitalSignID = Int32.Parse(dsSoap.Tables[0].Rows[0]["RECORDED_VITALSIGNS_ID"].ToString());
                    long soapID = Int64.Parse(dsSoap.Tables[0].Rows[0]["SOAPS_ID"].ToString());

                    DataSet dsVitalSigns = PatientManager.GetPatientVitalSignsByID(vitalSignID);

                    string docID = dsSoap.Tables[0].Rows[0]["DOCTOR_ID"].ToString();
                    DataSet dsDoc = PatientManager.GetProviderInfo(docID);


                    lblPhysicianNameETRF.Text = dsDoc.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsDoc.Tables[0].Rows[0]["LastName"].ToString();

                    if (dsPatient != null && dsPatient.Tables.Count > 0)
                    {
                        this.Page.Title = "Patient Emergency Transfer Network Request(" + dsPatient.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsPatient.Tables[0].Rows[0]["LastName"].ToString() + ")";
                        lblPatientName.Text = "Patient Emergency Transfer Network Request For " + dsPatient.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsPatient.Tables[0].Rows[0]["LastName"].ToString();
                    }


                    if (dsVitalSigns.Tables[0].Rows[0]["PATIENT_IMAGE"] != null)
                    {
                        gdvPhoto.DataSource = dsVitalSigns;
                        gdvPhoto.DataBind();
                    }
                    else
                    {
                        gdvPhoto.DataSource = null;
                        gdvPhoto.DataBind();
                        gdvPhoto.Visible = false;
                    }

                    txtNurseSubjective.ReadOnly = true;
                    //txtObjective.ReadOnly = true;
                    txtDate.ReadOnly = true;

                    txtNurseSubjective.Text = dsSoap.Tables[0].Rows[0]["SUBJECTIVE"].ToString();
                    //txtObjective.Text = dsSoap.Tables[0].Rows[0]["OBJECTIVE"].ToString();
                    txtDocAssETFR.Text = dsSoap.Tables[0].Rows[0]["ASSESSMENT"].ToString();
                    txtDate.Text = Convert.ToDateTime(dsSoap.Tables[0].Rows[0]["DATE_ADDED"].ToString()).ToString("MM dd yyyy");
                    txtDate.Enabled = false;

                    lblDate.Text = Convert.ToDateTime(dsNurseSoap.Tables[0].Rows[0]["DATE_ADDED"].ToString()).ToString("MM dd yyyy");

                    rblPlan.SelectedValue = dsSoap.Tables[0].Rows[0]["UMR_PLAN_ID"].ToString();
                    lblDocPlan.Text = rblPlan.SelectedItem.Text;

                    lblTemperature.Text = dsVitalSigns.Tables[0].Rows[0]["TEMPERATURE"].ToString();
                    lblTemperatureUnit.Text = dsVitalSigns.Tables[0].Rows[0]["TEMPERATURE_UNIT"].ToString();
                    lblBloodPressureSystolic.Text = dsVitalSigns.Tables[0].Rows[0]["BLOOD_PRESSURE_SYSTOLIC"].ToString();
                    lblBloodPressureDiastolic.Text = dsVitalSigns.Tables[0].Rows[0]["BLOOD_PRESSURE_DIASTOLIC"].ToString();

                    lblPulse.Text = dsVitalSigns.Tables[0].Rows[0]["PULSE"].ToString();
                    lblPulseRate.Text = dsVitalSigns.Tables[0].Rows[0]["PULSE_TYPE"].ToString();

                    lblRespiratoryRate.Text = dsVitalSigns.Tables[0].Rows[0]["RESPIRATORY_RATE"].ToString();
                    lblWeight.Text = dsVitalSigns.Tables[0].Rows[0]["BMI_WEIGHT"].ToString();
                    lblHeight.Text = dsVitalSigns.Tables[0].Rows[0]["BMI_HEIGHT"].ToString();
                    lblPainScale.Text = dsVitalSigns.Tables[0].Rows[0]["PAIN_SCALE"].ToString();
                    lblLocation.Text = dsVitalSigns.Tables[0].Rows[0]["PAIN_LOCATION"].ToString();
                    lblPupilSizeRight.Text = dsVitalSigns.Tables[0].Rows[0]["PUPIL_SIZE_RIGHT"].ToString();
                    lblPupilSizeLeft.Text = dsVitalSigns.Tables[0].Rows[0]["PUPIL_SIZE_LEFT"].ToString();
                    lblBloodGlucoseLevel.Text = dsVitalSigns.Tables[0].Rows[0]["BLOOD_GLUCOSE_LEVEL"].ToString();

                    litProb.Text = dsSoap.Tables[0].Rows[0]["DOC_PROBLEMS"].ToString();
                    litPrescriptRx.Text = dsSoap.Tables[0].Rows[0]["DOC_PRESCRIPTION"].ToString();
                    litDiagonsTest.Text = dsSoap.Tables[0].Rows[0]["DOC_DIAGNOSTICTEST"].ToString();
                    litLab.Text = dsSoap.Tables[0].Rows[0]["DOC_LAB"].ToString();
                    litProcedures.Text = dsSoap.Tables[0].Rows[0]["DOC_PROCEDURES"].ToString();
                    litImmu.Text = dsSoap.Tables[0].Rows[0]["DOC_IMMUNIZATION"].ToString();
                    litPatientEdu.Text = dsSoap.Tables[0].Rows[0]["DOC_PAT_EDUCATIONS"].ToString();
                    litRespond.Text = dsSoap.Tables[0].Rows[0]["DOC_RESPOND"].ToString();
                    litReferTo.Text = dsSoap.Tables[0].Rows[0]["DOC_REFER"].ToString();



                    pnlNurse.Visible = false;
                    pnlNurseMail.Visible = false;
                    pnlNurseNote.Visible = false;
                    pnlVital.Visible = false;
                    dvImageUpLoader.Visible = false;
                    pnlPhysicianPlan.Visible = false;
                    pnlEtrfnMail.Visible = false;
                    pnlETRF.Visible = true;

                    pnlPhysician.Visible = false;
                    pnlPhysicianMail.Visible = true;
                    pnlEtrfnMail.Visible = true;
                    pnlETFRNotes.Visible = true;
                    pnlVitalDoc.Visible = true;
                    dvTxtMailTo.Visible = true;
                    dvMail.Visible = true;

                    btnSave.Visible = false;
                    dvSendReportTo.Visible = true;


                    dvMissingEmails.Visible = false;
                    rfvEmail.Enabled = false;
                    #endregion
                }
        }
        else
        {
            #region Doctor

            this.Page.Title = "Provider Alert Network(" + dsPatient.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsPatient.Tables[0].Rows[0]["LastName"].ToString() + ")";
            lblPatientName.Text = "Provider Alert Network For " + dsPatient.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsPatient.Tables[0].Rows[0]["LastName"].ToString();
        
            lblNurseName.Text = string.Empty;
            lblNurseObjective.Text = string.Empty;
            lblNurseSubjective.Text = string.Empty;

            DataSet ds = PatientManager.GetInstitutionsForPatients(PatientKey);
            grdInstitutions.DataSource = ds;
            grdInstitutions.DataBind();

            DataSet dsPlans = PatientManager.GetPlans();
            rblPlan.DataSource = dsPlans;
            rblPlan.DataBind();
            rblPlan.SelectedIndex = 0;

            DataSet dsEmergency = PatientManager.GetEmergencyContact(PatientKey, User.Identity.Name);
            if (dsEmergency.Tables[0].Rows != null && dsEmergency.Tables[0].Rows.Count > 0)
            {
                lblNoFam.Text = "";
                divNoFam.Visible = false;
                grdEmeractgencyCont.DataSource = dsEmergency;
                grdEmeractgencyCont.DataBind();
            }
            else
            {
                lblNoFam.Text = "No emergency contact provided";
                divNoFam.Visible = true;
            }


            DataSet dsAlert = PatientManager.GetPatientGetActiveAlert(PatientKey, (byte)EnumAlertStatus.NurseDone);
            long alertID = Int64.Parse(dsAlert.Tables[0].Rows[0]["ALERT_ID"].ToString());

            DataSet dsSoap = PatientManager.GetPatientNurseSoapsByAlertID(alertID);
            int vitalSignID = Int32.Parse(dsSoap.Tables[0].Rows[0]["RECORDED_VITALSIGNS_ID"].ToString());
            long soapID = Int64.Parse(dsSoap.Tables[0].Rows[0]["SOAPS_ID"].ToString());
            string nurseID = dsSoap.Tables[0].Rows[0]["NURSE_ID"].ToString();
            
            DataSet nurse = PatientManager.GET_NURSE_BY_ID(nurseID);
            lblNurseName.Text = nurse.Tables[0].Rows[0]["FIRSTNAME"].ToString() + " " + nurse.Tables[0].Rows[0]["LASTNAME"].ToString();

            DataSet dsVitalSigns = PatientManager.GetPatientVitalSignsByID(vitalSignID);

            DataSet dsSoapAddress = PatientManager.GetPatientPatientSoapsAddressBySoapID(soapID);
            string docID = dsSoapAddress.Tables[0].Rows[0]["DOCTOR_ID"].ToString();
            DataSet dsDoc = PatientManager.GetProviderInfo(docID);


            lblPhysicianName.Text = dsDoc.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsDoc.Tables[0].Rows[0]["LastName"].ToString();

            //txtNurseAssessment.ReadOnly = false;
            //txtObjective.ReadOnly = false;
            //txtPhysicianAssessment.ReadOnly = false;
            txtDate.Enabled = true;

            if (dsVitalSigns.Tables[0].Rows[0]["PATIENT_IMAGE"] != null)
            {
                gdvPhoto.DataSource = dsVitalSigns;
                gdvPhoto.DataBind();
            }
            else
            {
                gdvPhoto.DataSource = null;
                gdvPhoto.DataBind();
                gdvPhoto.Visible = false;
            }

            txtDate.ReadOnly = false;

            lblNurseSubjective.Text = dsSoap.Tables[0].Rows[0]["SUBJECTIVE"].ToString() + Environment.NewLine + dsSoap.Tables[0].Rows[0]["OBJECTIVE"].ToString();
            lblNurseObjective.Text = dsSoap.Tables[0].Rows[0]["ASSESSMENT"].ToString();
            lblDate.Text = Convert.ToDateTime(dsSoap.Tables[0].Rows[0]["DATE_ADDED"].ToString()).ToString("MM-dd-yyyy HH:mm");

            lblTemperature.Text = dsVitalSigns.Tables[0].Rows[0]["TEMPERATURE"].ToString();
            lblTemperatureUnit.Text = dsVitalSigns.Tables[0].Rows[0]["TEMPERATURE_UNIT"].ToString();
            lblBloodPressureSystolic.Text = dsVitalSigns.Tables[0].Rows[0]["BLOOD_PRESSURE_SYSTOLIC"].ToString();
            lblBloodPressureDiastolic.Text = dsVitalSigns.Tables[0].Rows[0]["BLOOD_PRESSURE_DIASTOLIC"].ToString();

            lblPulse.Text = dsVitalSigns.Tables[0].Rows[0]["PULSE"].ToString();
            lblPulseRate.Text = dsVitalSigns.Tables[0].Rows[0]["PULSE_TYPE"].ToString();

            lblRespiratoryRate.Text = dsVitalSigns.Tables[0].Rows[0]["RESPIRATORY_RATE"].ToString();
            lblWeight.Text = dsVitalSigns.Tables[0].Rows[0]["BMI_WEIGHT"].ToString();
            lblHeight.Text = dsVitalSigns.Tables[0].Rows[0]["BMI_HEIGHT"].ToString();
            lblPainScale.Text = dsVitalSigns.Tables[0].Rows[0]["PAIN_SCALE"].ToString();
            lblLocation.Text = dsVitalSigns.Tables[0].Rows[0]["PAIN_LOCATION"].ToString();
            lblPupilSizeRight.Text = dsVitalSigns.Tables[0].Rows[0]["PUPIL_SIZE_RIGHT"].ToString();
            lblPupilSizeLeft.Text = dsVitalSigns.Tables[0].Rows[0]["PUPIL_SIZE_LEFT"].ToString();
            lblBloodGlucoseLevel.Text = dsVitalSigns.Tables[0].Rows[0]["BLOOD_GLUCOSE_LEVEL"].ToString();


            pnlNurse.Visible = false;
            pnlNurseMail.Visible = false;
            pnlEtrfnMail.Visible = false;
            pnlNurseNote.Visible = false;
            pnlVital.Visible = false;
            pnlETFRNotes.Visible = false;

            dvImageUpLoader.Visible = false;
            pnlPhysicianMail.Visible = false;

            pnlETRF.Visible = false;
            pnlPhysicianMail.Visible = true;
            pnlPhysician.Visible = true;
            pnlPhysicianPlan.Visible = true;
            pnlVitalDoc.Visible = true;

            dvMail.Visible = false;
            dvMail.Visible = true;
            btnSave.Visible = true;

            dvMissingEmails.Visible = false;
            rfvEmail.Enabled = false;



            chkPatientNotExaminedByProvider.Checked = true;

            //txtEmergingSrch.Text = string.Empty;
            //ddlEmerging.DataSource = null;
            //ddlEmerging.DataBind();
            txtEmails.Text = string.Empty;
            txtProviderAssesmentSrch.Text = string.Empty;
            cboProvidersAssesment.DataSource = null;
            cboProvidersAssesment.DataBind();
            cboProvidersAssesment.Items.Clear();

            txtRespondToSrch.Text = string.Empty;
            ddlRespondTo.DataSource = null;
            ddlRespondTo.DataBind();
            ddlRespondTo.Items.Clear();

            txtReferToSrch.Text = string.Empty;
            ddlReferTo.DataSource = null;
            ddlReferTo.DataBind();
            ddlReferTo.Items.Clear();

            //txtAlertNotificationList.Text = string.Empty;
            //ddlAlertNotificationList.DataSource = null;
            //ddlAlertNotificationList.DataBind();

            txtRadiologySrch.Text = string.Empty;
            ddlRadiology.DataSource = null;
            ddlRadiology.DataBind();
            ddlRadiology.Items.Clear();

            txtLabSrch.Text = string.Empty;
            ddlLab.DataSource = null;
            ddlLab.DataBind();
            ddlLab.Items.Clear();

            txtProceduresSrch.Text = string.Empty;
            ddlProcedures.DataSource = null;
            ddlProcedures.DataBind();
            ddlProcedures.Items.Clear();

            txtPerformanceSrch.Text = string.Empty;
            ddlPerformance.DataSource = null;
            ddlPerformance.DataBind();
            ddlPerformance.Items.Clear();

            txtEmergingSrch.Text = string.Empty;
            ddlEmerging.DataSource = null;
            ddlEmerging.DataBind();
            ddlEmerging.Items.Clear();

            txtOtherSrch.Text = string.Empty;
            ddlOther.DataSource = null;
            ddlOther.DataBind();
            ddlOther.Items.Clear();

            #endregion
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Old
        //string temp = txtTemperature.Text.Trim();
        //char tempUnit = Convert.ToChar(ddlTemperatureUnit.SelectedValue);
        //string bloodPressureSys = txtBloodPressureSystolic.Text.Trim();
        //string bloodPressureDia = txtBloodPressureDiastolic.Text.Trim();
        //string pulse = txtPulse.Text.Trim();
        //string pulseType = rdoPulseRegular.Checked ? "Regular" : "Irregular";
        //string respiratory = txtRespiratoryRate.Text.Trim();
        //string bmiWeight = txtWeight.Text.Trim();
        //string bmiHeight = txtHeight.Text.Trim();
        //string painScale = ddlScale.SelectedValue;
        //string painLocation = txtLocation.Text.Trim();
        //string pupilSizeRight = txtPupilSizeRight.Text.Trim();
        //string pupilSizeLeft = txtPupilSizeRight.Text.Trim();
        //string bloodGlucoseLevel = txtBloodGlucoseLevel.Text.Trim();

        //DateTime date = DateTime.Now;

        //try
        //{
        //    date = DateTime.Parse(txtRespiratoryRate.Text.Trim());
        //}
        //catch { }

        //string chiefComplain = txtSubjective.Text.Trim();

        //string Error;
        //int rowAffected = PatientManager.AddVitalSigns(PatientKey, date, temp, tempUnit, bloodPressureSys, bloodPressureDia, pulse, pulseType, respiratory, chiefComplain, bmiWeight, bmiHeight, painScale, painLocation, pupilSizeRight, pupilSizeLeft, bloodGlucoseLevel, out Error);
        //if (rowAffected <= 0)
        //{
        //    lblMessage.Text = Error;
        //    lblMessage.ForeColor = Color.Red;
        //    lblMessage.Visible = true;
        //}
        //else
        //{
        //    lblMessage.Text = "Saved successfully";
        //    lblMessage.ForeColor = Color.Green;
        //    lblMessage.Visible = true;
        //    //ClearControls();
        //} 
        #endregion

        //SendMails();
        try
        {
            ValidateEmails();

            DataSet dsAlert = PatientManager.GetPatientGetActiveAlert(PatientKey, (byte)EnumAlertStatus.NurseDone);
            long alertID = Int64.Parse(dsAlert.Tables[0].Rows[0]["ALERT_ID"].ToString());

            DataSet dsSoap = PatientManager.GetPatientNurseSoapsByAlertID(alertID);
            int vitalSignID = Int32.Parse(dsSoap.Tables[0].Rows[0]["RECORDED_VITALSIGNS_ID"].ToString());
            long ownerSoapID = Int64.Parse(dsSoap.Tables[0].Rows[0]["SOAPS_ID"].ToString());

            DataSet dsSoapAddress = PatientManager.GetPatientPatientSoapsAddressBySoapID(ownerSoapID);
            string docID = dsSoapAddress.Tables[0].Rows[0]["DOCTOR_ID"].ToString();

            UpdateAlertForDoctor(alertID);
            SaveDoctorsSoap(vitalSignID, alertID, ownerSoapID, docID);

            SendMails(alertID, vitalSignID, ownerSoapID);
            btnSave.Visible = false;
            dvSendReportTo.Visible = false;


            PopulateControls();
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "close", "function InitializeAutoCompleteBox() {alert('report saved');window.close();}", true);
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }

    protected void btnDAN_Click(object sender, EventArgs e)
    {
        try
        {
            ValidateEmails();

            long alertID = SaveAlert();
            int vitalID = SaveVitals();
            long soapID = SaveNurseSoap(vitalID, alertID);
            //SaveNurseSoapAddress(soapID);
            SendMails(alertID, vitalID, soapID);


            PopulateControls();
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "close", "function InitializeAutoCompleteBox() {alert('report saved');window.close();}", true);
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }

    private long SaveAlert()
    {
        long patientKey = PatientKey;
        long alertID = 0;

        DateTime nurse_soap_time_stamp = DateTime.Now;
        bool has_nurse_soap = true;
        byte nurse_soap_escalation_count =0;
        byte alert_status= (byte)EnumAlertStatus.NurseDone;
        bool is_active = true;



        string ErrorMessage;
        int rowAffected = PatientManager.AddAlerts(nurse_soap_time_stamp,
                                has_nurse_soap,
                                nurse_soap_escalation_count,
                                patientKey,
                                null,
                                null,
                                null,
                                alert_status,
                                is_active,
                                out alertID,
                                out ErrorMessage);

        if (rowAffected <= 0)
        {
            lblMessage.Text = ErrorMessage;
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

        return alertID;
    }
    private long UpdateAlertForDoctor(long alertID)
    {

        DateTime doctor_soap_time_stamp = DateTime.Now;
        bool has_doctor_soap = true;
        byte alert_status = (byte)EnumAlertStatus.DocDone;
        bool is_active = true;

        if(rblPlan.SelectedIndex == 0 || rblPlan.SelectedIndex == 4)
        {
            is_active = false;
        }

        string ErrorMessage;
        int rowAffected = PatientManager.UpdateAlertWithDoctorResponse(alertID,
                                doctor_soap_time_stamp,
                                has_doctor_soap,
                                alert_status,
                                is_active,
                                out ErrorMessage);

        if (rowAffected <= 0)
        {
            lblMessage.Text = ErrorMessage;
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

        return alertID;
    }
    private long UpdateAlertForETFR(long alertID)
    {
        byte alert_status = (byte)EnumAlertStatus.ETFRDone;
        bool is_active = false;

        string ErrorMessage;
        int rowAffected = PatientManager.UpdateAlertWithDoctorResponse(alertID,
                                DateTime.Now,
                                true,
                                alert_status,
                                is_active,
                                out ErrorMessage);

        if (rowAffected <= 0)
        {
            lblMessage.Text = ErrorMessage;
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

        return alertID;
    }
    private long SaveNurseSoap(int recorded_vitalsigns_id,long alert_id)
    {

        long patient_key = PatientKey;
        string temp_Date = txtDate.Text.Trim() + " " + txtTime.Text.Trim();
        DateTime date_added = Convert.ToDateTime(temp_Date); 
        string nurse_id = ddlNurse.SelectedValue;

        string subjective = txtNurseSubjective.Text.Trim();
        string objective= " ";
        string assessment = txtNurseAssessment.Text.Trim();
        string notes = txtNurseNote.Text.Trim();
        long identiy = 0;



        string ErrorMessage;
        int rowAffected = PatientManager.AddSoaps(null,
                                patient_key,
                                date_added,
                                null,
                                null,
                                nurse_id,
                                recorded_vitalsigns_id,
                                alert_id,
                                subjective,
                                objective,
                                assessment,
                                notes,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                false,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                out identiy,
                                out ErrorMessage);

        if (rowAffected <= 0)
        {
            lblMessage.Text = ErrorMessage;
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


        PatientManager.AddPatientDiagonses(PatientKey, Constants.UMR_NURSE_SOAP_CODE_TYPE, Constants.UMR_NURSE_SOAP_CODE_VERSION,
                                           Constants.UMR_NURSE_SOAP_MEDCODE, DateTime.Now, Constants.UMR_SOAP_HOSPITALIZATION,
                                           null, null, Constants.UMR_SOAP_VISIBILITY, out ErrorMessage);

        return identiy;
    }
    private long SaveDoctorsSoap(int recorded_vitalsigns_id, long alert_id, long owner_id, string doctor_id)
    {

        long patient_key = PatientKey;

        DateTime date_added = DateTime.Now;

        string subjective = lblNurseSubjective.Text.Trim();
        string objective = lblNurseObjective.Text.Trim();
        string assessment = cboProvidersAssesment.Text.Trim();
        string notes = rblPlan.SelectedItem.Text.Trim();
        int umr_plan_id = Convert.ToInt32(rblPlan.SelectedValue);



        string doc_problems = " ";
        string doc_prescription = txtPrescription.Text.Trim();
        string doc_diagnostictest = " ";
        string doc_lab = ddlLab.Text.Trim();
        string doc_procedures = ddlProcedures.Text.Trim();
        string doc_immunization = txtImmunization.Text.Trim();
        string doc_pat_educations = txtPatEducation.Text.Trim();
        string doc_respond = ddlRespondTo.Text.Trim();
        string doc_refer = ddlReferTo.Text.Trim();


        bool examined_by_provider = chkPatientNotExaminedByProvider.Checked;
        string doc_radiology = txtRadiologySrch.Text.Trim();
        string doc_performance_measurement = txtPerformanceSrch.Text.Trim();
        string doc_emerging_tech_sv = txtEmergingSrch.Text.Trim();
        string doc_other_text = txtOtherSrch.Text.Trim();
        string doc_followup = txtFollowUp.Text.Trim();

        string doc_other = txtOther.Text.Trim();

        long identiy = 0;



        string ErrorMessage;
        int rowAffected = PatientManager.AddSoaps(owner_id,
                                patient_key,
                                date_added,
                                null,
                                doctor_id,
                                null,
                                recorded_vitalsigns_id,
                                alert_id,
                                subjective,
                                objective,
                                assessment,
                                notes,
                                umr_plan_id,
                                doc_problems,
                                doc_prescription,
                                doc_diagnostictest,
                                doc_lab,
                                doc_procedures,
                                doc_immunization,
                                doc_pat_educations,
                                doc_respond,
                                doc_refer,
                                examined_by_provider,
                                doc_radiology,
                                doc_performance_measurement,
                                doc_emerging_tech_sv,
                                doc_other_text,
                                doc_followup,
                                doc_other,
                                out identiy,
                                out ErrorMessage);

        if (rowAffected <= 0)
        {
            lblMessage.Text = ErrorMessage;
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


        PatientManager.AddPatientDiagonses(PatientKey, Constants.UMR_DOCTOR_SOAP_CODE_TYPE, Constants.UMR_DOCTOR_SOAP_CODE_VERSION,
                                           Constants.UMR_DOCTOR_SOAP_MEDCODE, DateTime.Now, Constants.UMR_SOAP_HOSPITALIZATION,
                                           null, doctor_id, Constants.UMR_SOAP_VISIBILITY, out ErrorMessage);

        return identiy;
    }

    private long SaveNurseSoapAddress(long soaps_id)
    {
        string doctor_id = null;

        long identiy = 0;

                DataSet dsPrimary = PatientManager.GetPrincipalHealthCareInfo(PatientKey, User.Identity.Name);
        if (dsPrimary != null && dsPrimary.Tables.Count > 0)
        {
            doctor_id = dsPrimary.Tables[0].Rows[0]["DoctorID"].ToString();
        }

        string ErrorMessage;
        int rowAffected = PatientManager.AddSoapsAddress(soaps_id,
                                null,
                                null,
                                doctor_id,
                                null,
                                null,
                                out identiy,
                                out ErrorMessage);

        if (rowAffected <= 0)
        {
            lblMessage.Text = ErrorMessage;
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

        return identiy;
    }
    private long SaveInstituteSoapAddress(long soaps_id, string institution_id)
    {
        long identiy = 0;


        string ErrorMessage;
        int rowAffected = PatientManager.AddSoapsAddress(soaps_id,
                                null,
                                null,
                                null,
                                null,
                                institution_id,
                                out identiy,
                                out ErrorMessage);

        if (rowAffected <= 0)
        {
            lblMessage.Text = ErrorMessage;
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

        return identiy;
    }
    private long SaveEmailSoapAddress(long soaps_id, string emailaddress)
    {
        //string emailaddress = txtEmails.Text.Trim();

        long identiy = 0;

        string ErrorMessage;
        int rowAffected = PatientManager.AddSoapsAddress(soaps_id,
                                emailaddress,
                                null,
                                null,
                                null,
                                null,
                                out identiy,
                                out ErrorMessage);

        if (rowAffected <= 0)
        {
            lblMessage.Text = ErrorMessage;
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

        return identiy;
    }

    private void ChckExtension()
    {
        string fileType = System.IO.Path.GetExtension(fudPatientImage.PostedFile.FileName).ToLower();

        if (fileType != ".jpg")
        {
            throw new Exception("Not a Valid file format for patient image, only .jpg format is supported");
        }
    }
    private int SaveVitals()
    {
        string nurseID = hnfNurseName.Value;

        long patientKey = PatientKey;
        int alertID = 0;


        string temperature = txtTemperature.Text.Trim();
        char tempUnit = Convert.ToChar(ddlTemperatureUnit.SelectedValue);
        string bloodPressureSys = txtBloodPressureSystolic.Text.Trim();
        string bloodPressureDia = txtBloodPressureDiastolic.Text.Trim();
        string pulse = txtPulse.Text.Trim();
        string pulseType = rdoPulseRegular.Checked ? "Regular" : "Irregular";
        string respiratoryRate = txtRespiratoryRate.Text.Trim();
        string chiefComplain = txtNurseSubjective.Text.Trim();
        string BMIWeight = txtWeight.Text.Trim() + " " + ddlWeightUnit.Text.Trim();
        string BMIHeight = txtHeight.Text.Trim() + " " + ddlHeightUnit.Text.Trim();

        if (txtHeightInch.Visible == true && txtHeightInch.Text.Trim().Length > 0)
        {
            BMIHeight = BMIHeight + " " + txtHeightInch.Text.Trim() + " " + lblHeightInch.Text.Trim();
        } 

        string painScale = ddlScale.SelectedValue;
        string painLocation = txtLocation.Text.Trim();
        string pupilSizeRight = txtPupilSizeRight.Text.Trim();
        string pupilSizeLeft = txtPupilSizeRight.Text.Trim();
        string bloodGlucoseLevel = txtBloodGlucoseLevel.Text.Trim();
        byte[] patient_image = null;

        if (fudPatientImage.HasFile && fudPatientImage.PostedFile != null)
        {
            ChckExtension();
            patient_image = new byte[fudPatientImage.PostedFile.InputStream.Length];
            fudPatientImage.PostedFile.InputStream.Read(patient_image, 0, patient_image.Length);
        }
        //out string ErrorMessage;


        DateTime record_date = DateTime.Now;
        try
        {
            record_date = DateTime.Parse(txtRespiratoryRate.Text.Trim());
        }
        catch { }


        string ErrorMessage;
        int rowAffected;
        if (patient_image == null)
        {
            rowAffected = PatientManager.AddVitalSigns(patientKey,
                        record_date,
                        temperature,
                        tempUnit,
                        bloodPressureSys,
                        bloodPressureDia,
                        pulse,
                        pulseType,
                        respiratoryRate,
                        chiefComplain,
                        BMIWeight,
                        BMIHeight,
                        painScale,
                        painLocation,
                        pupilSizeRight,
                        pupilSizeLeft,
                        bloodGlucoseLevel,
                        null,
                        out alertID,
                        out ErrorMessage);
        }
        else
        {
            rowAffected = PatientManager.AddVitalSigns(patientKey,
                        record_date,
                        temperature,
                        tempUnit,
                        bloodPressureSys,
                        bloodPressureDia,
                        pulse,
                        pulseType,
                        respiratoryRate,
                        chiefComplain,
                        BMIWeight,
                        BMIHeight,
                        painScale,
                        painLocation,
                        pupilSizeRight,
                        pupilSizeLeft,
                        bloodGlucoseLevel,
                        patient_image,
                        out alertID,
                        out ErrorMessage);
        }

        if (rowAffected <= 0)
        {
            lblMessage.Text = ErrorMessage;
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

        return alertID;
    }

    public void Server_ValidateEmails(object source, ServerValidateEventArgs args)
    {
            string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            string unvEmails = args.Value;
            string[] es = unvEmails.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            args.IsValid = true;
            for (int i = 0; i < es.Length; i++)
            {
                Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
                Match match = rgx.Match(es[i]);
                if (!match.Success)
                {
                    args.IsValid=false;
                }
            }
    }

    private void ValidateEmails()
    {
        string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        string unvEmails = txtEmails.Text.Trim();
        string[] es = unvEmails.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < es.Length; i++)
        {
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = rgx.Match(es[i]);
            if (!match.Success)
            {
                throw new Exception("Invalid Email Address:" + es[i]);
            }
        }
    }

    private void SendMails(long alertID, int vitalID, long soaps_id)
    {
        try
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
                        string institution_id = grdInstitutions.DataKeys[row.DataItemIndex].Value.ToString();
                        SaveInstituteSoapAddress(soaps_id, institution_id);
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
                        SaveEmailSoapAddress(soaps_id, hdnEmail.Value.Trim());
                    }
                }
            }
            if (chkProviderEmail.Checked)
            {
                SaveNurseSoapAddress(soaps_id);
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


            ValidateEmails();

            string[] es = txtEmails.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < es.Length; i++)
            {
                emails.Add(es[i]);
                SaveEmailSoapAddress(soaps_id, es[i]);
            }

            foreach (string email in emails)
            {
                PatientManager.SendEmailTo(PatientKey, email, vitalID, soaps_id, alertID);
            }
            txtEmails.Text = "";

            if (chkSenSMS.Checked)
            {
                List<string> phones = new List<string>();
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

                                StringBuilder sbSMSText = new StringBuilder("Please reply to this SMS by entering one of the following numbers. Each number represents the tasks given below:");
                                sbSMSText.Append("\n");
                                sbSMSText.Append(@"Reply “1” to Continue to Monitor");
                                sbSMSText.Append("\n");
                                sbSMSText.Append(@"Reply “2” to Transfer to ER for Evaluation");
                                sbSMSText.Append("\n");
                                sbSMSText.Append(@"Reply “3” to Transfer to Hospital for Observation Status");
                                sbSMSText.Append("\n");
                                sbSMSText.Append(@"Reply “4” to Transfer to Hospital for Inpatient Admission");
                                sbSMSText.Append("\n");
                                sbSMSText.Append(@"Reply “5” for Doctor call-in");


                                PatientManager.SendSMSTo(phones, sbSMSText.ToString());
                            }
                        }
                    }
                } 
            }

            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('S.O.A.P and other information has been sent to respective recipient(s).')", true);

            //StringBuilder sbSMSText = new StringBuilder("Please reply to this SMS by entering one of the following numbers. Each number represents the tasks given below:");
            //sbSMSText.Append("\n");
            //sbSMSText.Append(@"Reply “1” to Continue to Monitor");
            //sbSMSText.Append("\n");
            //sbSMSText.Append(@"Reply “2” to Transfer to ER for Evaluation");
            //sbSMSText.Append("\n");
            //sbSMSText.Append(@"Reply “3” to Transfer to Hospital for Observation Status");
            //sbSMSText.Append("\n");
            //sbSMSText.Append(@"Reply “4” to Transfer to Hospital for Inpatient Admission");
            //sbSMSText.Append("\n");
            //sbSMSText.Append(@"Reply “5” for Doctor call-in");
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }

    private void ClearControls()
    {
        txtTemperature.Text = "";
        txtBloodPressureSystolic.Text = "";
        txtPulse.Text = "";
        txtRespiratoryRate.Text = "";
        txtDate.Text = "";
        txtNurseSubjective.Text = "";
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
        try
        {
            ValidateEmails();

            DataSet dsAlert = PatientManager.GetPatientGetActiveDOCAlert(PatientKey, (byte)EnumAlertStatus.DocDone);

            long alertID = Int64.Parse(dsAlert.Tables[0].Rows[0]["ALERT_ID"].ToString());
            DataSet dsSoap = PatientManager.GetPatientDOCSoapsByAlertID(alertID);

            int vitalSignID = Int32.Parse(dsSoap.Tables[0].Rows[0]["RECORDED_VITALSIGNS_ID"].ToString());
            long soapID = Int64.Parse(dsSoap.Tables[0].Rows[0]["SOAPS_ID"].ToString());

            UpdateAlertForETFR(alertID);

            SendMails(alertID, vitalSignID, soapID);

            PopulateControls();
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "close", "window.close();", true);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "close", "function InitializeAutoCompleteBox() {alert('report saved');window.close();}", true);
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }

    }

    protected void txtProviderAssesmentSrch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtProviderAssesmentSrch.Text.Trim().Length > 0)
            {
                DataSet dt = PatientManager.GetAllDiagnosticTest(txtProviderAssesmentSrch.Text.Trim());
                cboProvidersAssesment.DataSource = dt.Tables[0];
                cboProvidersAssesment.DataBind();
                //cboProvidersAssesment.SelectedIndex = -1;
                cboProvidersAssesment_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }
    protected void cboProvidersAssesment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cboProvidersAssesment.SelectedIndex >= 0)
            {
                txtProviderAssesmentSrch.Text = cboProvidersAssesment.SelectedValue;
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }

    protected void txtRadiologySrch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtRadiologySrch.Text.Trim().Length > 0)
            {
                DataSet dt = PatientManager.GetRadiology(txtRadiologySrch.Text.Trim());
                ddlRadiology.DataSource = dt.Tables[0];
                ddlRadiology.DataBind();
               // ddlRadiology.SelectedIndex = -1;
                ddlRadiology_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }
    protected void ddlRadiology_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlRadiology.SelectedIndex >= 0)
            {
                txtRadiologySrch.Text = ddlRadiology.SelectedValue;
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }

    protected void txtLabSrch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtLabSrch.Text.Trim().Length > 0)
            {
                DataSet dt = PatientManager.GetPathologyLaboratory(txtLabSrch.Text.Trim());
                ddlLab.DataSource = dt.Tables[0];
                ddlLab.DataBind();
                //ddlLab.SelectedIndex = -1;
                ddlLab_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }

    }
    protected void ddlLab_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlLab.SelectedIndex >= 0)
            {
                txtLabSrch.Text = ddlLab.SelectedValue;
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }

    }

    protected void txtProceduresSrch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtProceduresSrch.Text.Trim().Length > 0)
            {
                DataSet dt = PatientManager.GetProceduresOthers(txtProceduresSrch.Text.Trim());
                ddlProcedures.DataSource = dt.Tables[0];
                ddlProcedures.DataBind();
                //ddlProcedures.SelectedIndex = -1;
                ddlProcedures_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }
    protected void ddlProcedures_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProcedures.SelectedIndex >= 0)
            {
                txtProceduresSrch.Text = ddlProcedures.SelectedValue;
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }

    protected void txtPerformanceSrch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtPerformanceSrch.Text.Trim().Length > 0)
            {
                DataSet dt = PatientManager.GetPerformanceMeasurements(txtPerformanceSrch.Text.Trim());
                ddlPerformance.DataSource = dt.Tables[0];
                ddlPerformance.DataBind();
                //ddlPerformance.SelectedIndex = -1;
                ddlPerformance_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }
    protected void ddlPerformance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPerformance.SelectedIndex >= 0)
            {
                txtPerformanceSrch.Text = ddlPerformance.SelectedValue;
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }

    protected void txtEmergingSrch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtEmergingSrch.Text.Trim().Length > 0)
            {
                DataSet dt = PatientManager.GetEmergingTechServices(txtEmergingSrch.Text.Trim());
                ddlEmerging.DataSource = dt.Tables[0];
                ddlEmerging.DataBind();
                //ddlEmerging.SelectedIndex = -1;
                ddlEmerging_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }
    protected void ddlEmerging_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlEmerging.SelectedIndex >= 0)
            {
                txtEmergingSrch.Text = ddlEmerging.SelectedValue;
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }

    protected void txtOtherSrch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtOtherSrch.Text.Trim().Length > 0)
            {
                DataSet dt = PatientManager.GetProceduresOthers(txtOtherSrch.Text.Trim());
                ddlOther.DataSource = dt.Tables[0];
                ddlOther.DataBind();
                //ddlOther.SelectedIndex = -1;
                ddlOther_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }
    protected void ddlOther_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlOther.SelectedIndex >= 0)
            {
                txtOtherSrch.Text = ddlOther.SelectedValue;
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }


    protected void txtRespondToSrch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtRespondToSrch.Text.Trim().Length > 0)
            {
                DataSet dt = PatientManager.GET_UMR_PROVIDER_BY_NAME(txtRespondToSrch.Text.Trim());
                ddlRespondTo.DataSource = dt.Tables[0];
                ddlRespondTo.DataBind();
                //ddlRespondTo.SelectedIndex = -1;
                ddlRespondTo_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }
    protected void ddlRespondTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlRespondTo.SelectedIndex >= 0)
            {
                txtRespondToSrch.Text = ddlRespondTo.SelectedValue;
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }

    protected void txtReferToSrch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtReferToSrch.Text.Trim().Length > 0)
            {
                DataSet dt = PatientManager.GET_UMR_PROVIDER_BY_NAME(txtReferToSrch.Text.Trim());
                ddlReferTo.DataSource = dt.Tables[0];
                ddlReferTo.DataBind();
                //ddlReferTo.SelectedIndex = -1;
                ddlReferTo_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }
    protected void ddlReferTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlRespondTo.SelectedIndex >= 0)
            {
                txtReferToSrch.Text = ddlReferTo.SelectedValue;
            }
        }
        catch (Exception Ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "alert('" + Ex.Message + "')", true);
        }
    }


    protected void ddlHeightUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlHeightUnit.SelectedIndex == 2)
        {
            txtHeightInch.Visible = true;
            lblHeightInch.Visible = true;
        }
        else
        {
            txtHeightInch.Visible = false;
            lblHeightInch.Visible = false;
        }
    }
}