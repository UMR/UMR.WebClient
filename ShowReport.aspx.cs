using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ShowReport : System.Web.UI.Page
{
    public int ReportId
    {
        get
        {
            int id = 0;
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                id = Convert.ToInt32(Request.QueryString["id"]);
            }
            return id;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadReport();
        }
    }

    private void LoadReport()
    {
        reportPlaceHolder.Text = GetReportById(ReportId);
    }

    private string GetReportById(int reportId)
    {
        string html = "";
        DataSet ds = PatientManager.GetSMSReportHTMLById(reportId);
        if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            html = ds.Tables[0].Rows[0]["ReportHTML"].ToString();
        }
        return html;
    }
}