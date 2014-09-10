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

public partial class Oracle_ControlLibrary_ucQuickLinks : System.Web.UI.UserControl
{
    private long patientKey;
    string lastMPIDate, lastAccessDate, lastPMDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        patientKey =Int64.Parse( Request.QueryString["PatientKey"].ToString());

        //First Get the latest dates for different Links... ex. Last MPI Date, Last Accessed Date etc.
        DataSet dsDates = PatientManager.GetLastDates(patientKey);
        if (dsDates != null)
        {
            lastMPIDate = dsDates.Tables[0].Rows[0]["PatientLastExam"].ToString();
            lastAccessDate = dsDates.Tables[0].Rows[0]["PatientLastAccess"].ToString();
            lastPMDate = dsDates.Tables[0].Rows[0]["PatientLastPrescription"].ToString();
        }
        //This is where we will build the Quick Links...

        //Start with the Master Patient Index
        LinkButton lbMPI = this.FindControl("lbMPI") as LinkButton;
        if (lbMPI != null)
        {
            //lbMPI.Text = "Master Patient Index as of Last Exam -- " + lastPMDate + " EST";
            //lbMPI.Text = "Master Patient Index";
            lbMPI.Attributes["onclick"] = string.Format("return ShowMPI('{0}');", new object[] { patientKey });
        }

        //Last Recorded Prescription/Medication Link
        LinkButton lbLRP = FindControl("lbLRP") as LinkButton;
        if (lbLRP != null)
        {
            //lbLRP.Text = "Last Ordered Prescription -- " + lastPMDate + " EST";
            //lbLRP.Text = "Last Ordered Prescription";
            lbLRP.Attributes["onclick"] = string.Format("return ShowLRPM('{0}');", new object[] {patientKey});
        }

        //Last Record Accessed
        LinkButton lbLRA = this.FindControl("lbLRA") as LinkButton;
        if (lbLRA != null)
        {
            //lbLRA.Text = "Last Record Accessed -- " + lastAccessDate + " EST";
            //lbLRA.Text = "Last Record Accessed";
            lbLRA.Attributes["onclick"] = string.Format("return ShowLRA('{0}');", new object[] {patientKey });
        }

        //HealthCare Provider Information
        LinkButton lbHPL = this.FindControl("lbHPL") as LinkButton;
        if (lbHPL != null)
        {
            //lbHPL.Text = "Healthcare Provider(s)";
            lbHPL.Attributes["onclick"] = string.Format("return ShowHPList('{0}','{1}');", new object[] { patientKey, "M" }); // Sending "M" as DispType to get the list of Providers and not the details...
        }

        //Advanced Medical Directives
        LinkButton lbAMD = this.FindControl("lbAMD") as LinkButton;
        if (lbAMD != null)
        {
            //lbAMD.Text = "Advanced Medical Directive(s)";
            lbAMD.Attributes["onclick"] = string.Format("return ShowAMD('{0}');", new object[] { patientKey });
        }

        //Set Preferece
        LinkButton lbOption = FindControl("lbOption") as LinkButton;
        if (lbOption != null)
        {
            lbOption.Attributes["onclick"] = string.Format("return SetOption('{0}');", patientKey);
        }
        //Set Primary Provider
        LinkButton lbPP = FindControl("lbPrimaryProvider") as LinkButton;
        if (lbPP != null)
        {
            lbPP.Attributes["onclick"] = string.Format("return ShowPrimaryProvider('{0}');", patientKey);
        }
        //Set Insurance
        LinkButton lbIns = FindControl("lbInsurance") as LinkButton;
        if (lbIns != null)
        {
            lbIns.Attributes["onclick"] = string.Format("return ShowInsurance('{0}');", patientKey);
        }
        //Set Emergency
        LinkButton lbEmer = FindControl("lbEmergency") as LinkButton;
        if (lbEmer != null)
        {
            lbEmer.Attributes["onclick"] = string.Format("return ShowEmergency('{0}');", patientKey);
        }
        //Set Demographics
        LinkButton lbDemo = FindControl("lbDemographic") as LinkButton;
        if (lbDemo != null)
        {
            lbDemo.Attributes["onclick"] = string.Format("return ShowDemographic('{0}');",patientKey);
        }
        //Set UnremarkableDisciplines
        LinkButton lbUnRD = FindControl("lbUnRD") as LinkButton;
        if (lbUnRD != null)
        {
            lbUnRD.Attributes["onclick"] = string.Format("return ShowUnremarkable('{0}');", patientKey);
        }
        //Set Legend
        LinkButton lbLegend = FindControl("lbLegend") as LinkButton;
        if (lbLegend != null)
        {
            lbLegend.Attributes["onclick"] = string.Format("return ShowLegend('{0}');",patientKey);
        }
        //Set Disclaimer
        LinkButton lbDisclaimer = FindControl("lbDisclaimer") as LinkButton;
        if (lbDisclaimer != null)
        {
            lbDisclaimer.Attributes["onclick"] = string.Format("return ShowDisclaimer();");
        }

        //Set Healtcare Facilities
        LinkButton lbHealthcareFacilities = FindControl("lbHF") as LinkButton;
        if (lbHealthcareFacilities != null)
        {
            lbHealthcareFacilities.Attributes["onclick"] = string.Format("return ShowHealthcareFacilities('{0}');",patientKey);
        }
    }


}
