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

public partial class Oracle_ControlLibrary_ucInsuranceInfo : System.Web.UI.UserControl
{
    private long patientKey;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString != null)
        {
            patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
        }
    }
    protected void RadGridInsuranceInfo_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        RadGridInsuranceInfo.DataSource = PatientManager.GetInsuranceInformation(patientKey);
    }
}
