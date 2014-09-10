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
using Telerik.WebControls;

public partial class Oracle_RDNew : System.Web.UI.Page
{
    long patientKey;
    string codeType, medCode, codeVersion, disCode, disType;

    const string CODETYPEFILTER = "CodeType";

    private Oracle_ControlLibrary_ucRD2l diagMain = null;
    private Oracle_ControlLibrary_ucRDMedicationMain medMain = null;

    protected override void OnInit(EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
        {
            patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
            disCode = Request.QueryString["DisCode"].ToString();

            disType = Request.QueryString["DisType"].ToString();
            if (disType.Equals("D"))                //typeof(RD) == Diagnostics
            {
                string path = ("~/Oracle/ControlLibrary/ucRD2l.ascx");
                Oracle_ControlLibrary_ucRD2l ucDiagMain = (Oracle_ControlLibrary_ucRD2l)LoadControl(path);
                ucDiagMain.PatientKey = patientKey;
                if (Request.QueryString["CodeType"] != null)
                {
                    string strCodeType = Request.QueryString["CodeType"].ToString();
                    ucDiagMain.CodeType = strCodeType;

                    if (Session[CODETYPEFILTER] != null)
                    {
                        Session.Remove(CODETYPEFILTER);
                    }
                    Session.Add(CODETYPEFILTER, strCodeType);
                }
                ucDiagMain.DisCode = disCode;

                diagMain = ucDiagMain;
                phControl.Controls.Add(ucDiagMain);

                string disName = Request.QueryString["disName"].ToString();
                Label InjectScriptLabel = FindControl("InjectScriptLabelWindowTitle") as Label;
                if (InjectScriptLabel != null)
                {
                    InjectScriptLabel.Text = String.Format("<script>SetTitle('{0}')</script>", disName);
                }
            }
            else //typeOf(RD) == Medication
            {
                string path = string.Empty;
                #region Prescription & Medication
                Label lblAllerty = new Label();
                lblAllerty.Text = "<div style='font-family:Tahoma; font-size: 8pt; font-weight: bold; background-color: #DDEAFC;'>Allergy of Medication</div>";
                phControl.Controls.Add(lblAllerty);

                path = ("~/Oracle/ControlLibrary/ucRD2l.ascx");
                Oracle_ControlLibrary_ucRD2l ucDiagMain = (Oracle_ControlLibrary_ucRD2l)LoadControl(path);
                ucDiagMain.PatientKey = patientKey;
                if (Request.QueryString["CodeType"] != null)
                {
                    ucDiagMain.CodeType = Request.QueryString["CodeType"].ToString();
                }
                ucDiagMain.DisCode = "1"; //DisCode for "ALLEGRY of Medications"

                diagMain = ucDiagMain;
                phControl.Controls.Add(ucDiagMain);
                #endregion


                Label lblMedication = new Label();
                lblMedication.Text = "<br/><div style='font-family:Tahoma; font-size: 8pt; font-weight: bold; background-color: #DDEAFC;'>Prescription and Medication</div>";
                phControl.Controls.Add(lblMedication);

                path = ("~/Oracle/ControlLibrary/ucRDMedicationMain.ascx");
                Oracle_ControlLibrary_ucRDMedicationMain ucMedMain = (Oracle_ControlLibrary_ucRDMedicationMain)LoadControl(path);
                medMain = ucMedMain;
                ucMedMain.PatientKey = patientKey;
                phControl.Controls.Add(ucMedMain);

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

        UcLegendCompact2.FilterApplied += new LegendCompactEventHandler(RefreshRD);
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
