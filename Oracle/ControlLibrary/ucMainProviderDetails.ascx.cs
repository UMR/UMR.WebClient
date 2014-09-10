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

public partial class Oracle_ControlLibrary_ucMainProviderDetails : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string user = HttpContext.Current.User.Identity.Name;
            long patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());

            DataSet ds = PatientManager.GetPrincipalHealthCareInfo(patientKey, user);
            string providerID = ds.Tables[0].Rows[0]["DoctorID"].ToString();
            dtvMainProviderDetails.DataSource = PatientManager.GetProviderInfo(providerID).Tables[0];
            dtvMainProviderDetails.DataBind();

            GridView grdHFs = dtvMainProviderDetails.FindControl("grdHFs") as GridView;
            grdHFs.DataSource = PatientManager.GetProviderInfo(providerID).Tables[1];
            grdHFs.DataBind();

        }
    }
}
