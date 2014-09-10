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

public partial class Oracle_ControlLibrary_ucUnRD : System.Web.UI.UserControl
{
    long patientKey;
    protected void Page_Load(object sender, EventArgs e)
    {
        patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
        dlNRD.DataSource = PatientManager.GetNRDList(patientKey);
        dlNRD.DataBind();
    }
}
