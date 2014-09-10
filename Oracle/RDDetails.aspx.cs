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

public partial class Oracle_RDDetails : System.Web.UI.Page
{
    long patientKey;
    string medCode, disType;
    Oracle_ControlLibrary_ucRDDiagnosisDetails2 diagDetails = null;
    Oracle_ControlLibrary_ucRDMedicationDetails2 medDetails = null;
    protected override void OnInit(EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
        {
            patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
            //"DisType" in the query string tells us what type of RD it is...
            //Currently we have only 2 types --> Diagnostics and Medication
            disType = Request.QueryString["DisType"].ToString();
            if (disType.Equals("D"))
            {
                //**** Previous Version ****************************************************
                //string path = ("~/Oracle/ControlLibrary/ucRDDiagnosisDetails.ascx");
                //Oracle_ControlLibrary_ucRDDiagnosisDetails ucDiagDetail =
                //    (Oracle_ControlLibrary_ucRDDiagnosisDetails)LoadControl(path);
                //phControl.Controls.Add(ucDiagDetail);
                //Page.Title = "Diagnosis Details";
                //**************************************************************************
                //Get the remaining parameter values from the QueryString
                //codeType = Request.QueryString["CodeType"].ToString();
                medCode = Request.QueryString["MedCode"].ToString();
                //codeVersion = Request.QueryString["CodeVersion"].ToString();
                //disCode = Request.QueryString["DisCode"].ToString();
                //Now set the control path

                /*Comment out by: Animesh | Date: 13 May 2010 | Reason: MPI alike screen. Grid will display provider ID and ProvierName. */
                /*
                    string path = ("~/Oracle/ControlLibrary/ucRDDiagnosisDetails.ascx");
                    Oracle_ControlLibrary_ucRDDiagnosisDetails ucDiagDetail = (Oracle_ControlLibrary_ucRDDiagnosisDetails)LoadControl(path);
                    diagDetails = ucDiagDetail;
                    //Set the required Parameter Values in the User Control
                    ucDiagDetail.PatientKey = patientKey;
                    //ucDiagDetail.CodeType = codeType;
                    ucDiagDetail.MedCode = medCode;
                    //ucDiagDetail.CodeVersion = codeVersion;
                    //ucDiagDetail.DisCode = disCode;
                    //Now Add the control
                    phControl.Controls.Add(ucDiagDetail);
                */
                string path = ("~/Oracle/ControlLibrary/ucRDDiagnosisDetails2.ascx");
                Oracle_ControlLibrary_ucRDDiagnosisDetails2 ucDiagDetail = (Oracle_ControlLibrary_ucRDDiagnosisDetails2)LoadControl(path);
                diagDetails = ucDiagDetail;
                ucDiagDetail.PatientKey = patientKey;
                ucDiagDetail.MedCode = medCode;
                phControl.Controls.Add(ucDiagDetail);

                Page.Title = "Diagnosis Details";
                //*********************  ADD Medical Content Index Name ********************
                this.lblName.Text = "Medical Contect Index: " + Request.QueryString["name"].ToString(); // Lumbar spinal puncture diagnostic"
                if (Request.QueryString["desc"] != null)
                {
                    this.lblName.Attributes["title"] = Request.QueryString["desc"].ToString();
                }
                //**************************************************************************
            }
            else
            {
                //**** Previous Version ****************************************************
                //string path = ("~/Oracle/ControlLibrary/ucRDMedicationDetails.ascx");
                //Oracle_ControlLibrary_ucRDMedicationDetails ucMedDetail =
                //    (Oracle_ControlLibrary_ucRDMedicationDetails)LoadControl(path);
                //phControl.Controls.Add(ucMedDetail);
                //Page.Title = "Medication Details";
                //**************************************************************************

                string path = ("~/Oracle/ControlLibrary/ucRDMedicationDetails2.ascx");
                Oracle_ControlLibrary_ucRDMedicationDetails2 ucMedMain = (Oracle_ControlLibrary_ucRDMedicationDetails2)LoadControl(path);
                medDetails = ucMedMain;
                ucMedMain.PatientKey = patientKey;
                ucMedMain.NDCCode = Request.QueryString["NDCCode"].ToString().Trim();
                //Add the control
                phControl.Controls.Add(ucMedMain);
                Page.Title = "Prescription and Medication Details";
                //*********************  ADD Brand Name ************************************
                this.lblName.Text = "Prescription: " + Request.QueryString["name"].ToString();
                //**************************************************************************
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (EnbaleBackButton)
            {
                backButtonTd.Visible = true;
            }
            else
            {
                backButtonTd.Visible = false;
            }
        }
        UcLegendCompact1.FilterApplied += new LegendCompactEventHandler(RefreshRD);
    }

    public bool EnbaleBackButton
    {
        get
        {
            bool enabled = false;
            if (Request.QueryString["BackButton"] != null)
            {
                enabled = Boolean.Parse(Request.QueryString["BackButton"]);
            }
            return enabled;
        }
    }
    protected void RefreshRD(object sender, string selectedColor)
    {
        if (diagDetails != null)
        {
            diagDetails.RefreshPage(selectedColor);
        }
        if (medDetails != null)
        {
            medDetails.RefreshPage(selectedColor);
        }
    }
}
