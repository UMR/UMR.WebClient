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

public partial class Oracle_LRPM : System.Web.UI.Page
{
    private long patientKey;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
        {
            patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
        }
        RadGridLRPM.DataBind();
    }

    protected void RadGridLRPM_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            HyperLink editLink = e.Item.FindControl("Provider") as HyperLink;
            if (editLink != null)
            {
                editLink.Attributes["href"] = "#";
            }
            DataRowView drv;
            if (e.Item.DataItem != null)
            {
                drv = e.Item.DataItem as DataRowView;
                if (drv != null)
                {
                    editLink.Text = drv["ProviderID"].ToString();   // e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["DETAIL"].ToString();
                }
                editLink.Attributes["onclick"] = string.Format("return ShowProviderDetails('{0}','{1}');",
                                                                            new object[] { drv["ProviderID"],"S" });
            }
        }
    }
    protected void RadGridLRPM_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGridLRPM.MasterTableView.DataSource = PatientManager.GetLRPMInfo(patientKey);
    }
    public string GetQueryString()
    {
        return "PatientKey=" + Request.QueryString["PatientKey"];
    }
}
