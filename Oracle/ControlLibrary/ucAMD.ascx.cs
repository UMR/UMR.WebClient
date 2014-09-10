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

public partial class Oracle_ControlLibrary_ucAMD : System.Web.UI.UserControl
{
    long patientKey;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
        {
            patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
        }
        DataTable dtAMD = PatientManager.GetAMDForPatient(patientKey);
        RadGridAMD.DataSource = dtAMD;
        RadGridAMD.DataBind();
    }
}
