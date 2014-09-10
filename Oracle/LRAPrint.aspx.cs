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


public partial class Oracle_LRAPrint : System.Web.UI.Page
{
    private long patientKey;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
        {
            patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
        }
        RadGridLRA.DataBind();
    }
    protected void RadGridLRA_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        RadGridLRA.MasterTableView.DataSource = PatientManager.GetLRAInfo(patientKey);
    }
}
