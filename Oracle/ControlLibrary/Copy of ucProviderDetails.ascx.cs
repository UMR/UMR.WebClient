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


public partial class Oracle_ControlLibrary_ucProviderDetails : System.Web.UI.UserControl
{
    private string id;
    private long patientKey;
    //private RadGrid providerGrid;

    //public RadGrid ProviderGrid
    //{
    //    get { return providerGrid; }
    //}

    //protected override void OnInit(EventArgs e)
    //{
    //    providerGrid = this.RadGrid1;
    //}

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    //if (!IsPostBack)
    //    //{
    //    //    if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
    //    //    {
    //    //        string id = Request.QueryString["ID"].ToString();
    //    //        RadGrid1.MasterTableView.DataSource = PatientManager.GetProviderInfo(id);
    //    //        RadGrid1.DataBind();
    //    //    }

    //    //}
    //}



    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        string dispType = Request.QueryString["DispType"].ToString();
        if (dispType.Equals("S"))       //This request is coming from other pages (RDDiagDetails,RDMedDetails,MPI,LRPM etc.)Details...
        {
            id = Request.QueryString["ID"].ToString();
            RadGrid1.DataSource = PatientManager.GetProviderInfo(id);
        }
        else                            //This request will generate only from the HPList QuickLink in Result Page...
        {
            patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
            RadGrid1.DataSource = PatientManager.GetMultipleProviderInfo(patientKey);
        }
    }
}
