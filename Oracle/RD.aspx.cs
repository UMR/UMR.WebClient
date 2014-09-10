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


public partial class Oracle_RD : System.Web.UI.Page
{

    long patientKey;
    string codeType, medCode, codeVersion, disCode, disType;
    private Oracle_ControlLibrary_ucRDDiagnosisMain diagMain = null;
    private Oracle_ControlLibrary_ucRDMedicationMain medMain = null;

    protected override void OnInit(EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
        {
            patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
            //codeType = Request.QueryString["CodeType"].ToString();
            //medCode = Request.QueryString["MedCode"].ToString();
            //codeVersion = Request.QueryString["CodeVersion"].ToString();
            disCode = Request.QueryString["DisCode"].ToString();

            //"DisType" in the query string tells us what type of RD it is...
            //Currently we have only 2 types --> Diagnostics and Medication
            disType = Request.QueryString["DisType"].ToString();
            if (disType.Equals("D"))                //typeof(RD) == Diagnostics
            {
                //********************************************************************************
                //******** Previously we went through ucRDDiagnosis instead of ucRDDiagnosisMain
                //  commenting out to ReUse the User Control - ucRDDiagnosisMain
                //*********************************************************************************
                //string path = ("~/Oracle/ControlLibrary/ucRDDiagnosis.ascx");
                //Oracle_ControlLibrary_ucRDDiagnosis ucDiag =
                //    (Oracle_ControlLibrary_ucRDDiagnosis)LoadControl(path);
                //phControl.Controls.Add(ucDiag);
                //*********************************************************************************
                string path = ("~/Oracle/ControlLibrary/ucRDDiagnosisMain.ascx");
                Oracle_ControlLibrary_ucRDDiagnosisMain ucDiagMain = (Oracle_ControlLibrary_ucRDDiagnosisMain)LoadControl(path);
                diagMain = ucDiagMain;
                //Set the required Parameter Values in the User Control
                ucDiagMain.PaientKey = patientKey;
                //Check if we have a CodeType Filter or not...
                if (Request.QueryString["CodeType"] != null)
                {
                    ucDiagMain.CodeType = Request.QueryString["CodeType"].ToString();
                }

                //ucDiagMain.MedCode = medCode;
                //ucDiagMain.CodeVersion = codeVersion;
                ucDiagMain.DisCode = disCode;
                //Now Add the control
                phControl.Controls.Add(ucDiagMain);

                //This is where we set the Window Title... April 28, 2009
                string disName = Request.QueryString["disName"].ToString();
                Label InjectScriptLabel = FindControl("InjectScriptLabelWindowTitle") as Label;
                if (InjectScriptLabel != null)
                {
                    //InjectScriptLabel.Text = "<script>SetTitle('" + fName + "','" + lName + "')</" + "script>";
                    InjectScriptLabel.Text = String.Format("<script>SetTitle('{0}')</script>", disName);
                }

            }
            else                                //typeOf(RD) == Medication
            {
                //********************************************************************************
                //******** Previously we went through ucRDMedication instead of ucRDMedicationMain
                //  commenting out to ReUse the User Control - ucRDMedicationMain
                //*********************************************************************************
                //string path = ("~/Oracle/ControlLibrary/ucRDMedication.ascx");
                //Oracle_ControlLibrary_ucRDMedication ucMed =
                //    (Oracle_ControlLibrary_ucRDMedication)LoadControl(path);
                //phControl.Controls.Add(ucMed);
                //**********************************************************************************
                string path = ("~/Oracle/ControlLibrary/ucRDMedicationMain.ascx");
                Oracle_ControlLibrary_ucRDMedicationMain ucMedMain = (Oracle_ControlLibrary_ucRDMedicationMain)LoadControl(path);
                medMain = ucMedMain;
                ucMedMain.PatientKey = patientKey;
                //Add the control
                phControl.Controls.Add(ucMedMain);

                //*********
                Label InjectScriptLabel = FindControl("InjectScriptLabelWindowTitle") as Label;
                if (InjectScriptLabel != null)
                {
                    InjectScriptLabel.Text = string.Format("<script>SetTitle('{0}')</script>", "Prescription and Medication");
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        bool enableBackButton = false;
        if (Request.QueryString["BackButton"] != null)
        {
            enableBackButton = bool.Parse(Request.QueryString["BackButton"]);
        }

        if (enableBackButton)
        {
            backButtonTd.Visible = true;
        }
        else
        {
            backButtonTd.Visible = false;
        }

        UcLegendCompact1.FilterApplied += new LegendCompactEventHandler(RefreshRD);
    }

    protected void RefreshRD(object sender, string selectedColor)
    {
        if (diagMain != null)
        {
            diagMain.RefreshPage(selectedColor);
        }
        if (medMain != null)
        {
            medMain.RefreshPage(selectedColor);
        }
    }
}
