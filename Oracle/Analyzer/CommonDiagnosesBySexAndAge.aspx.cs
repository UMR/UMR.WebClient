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

public partial class Oracle_Analyzer_CommonDiagnosesBySexAndAge : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            RadGridReport.Visible = false;
        }
    }
    protected void RadGridReport_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        char sex = Convert.ToChar(ddlSex.SelectedValue);
        int ageFrom = Convert.ToInt32(txtFrom.Text.Trim());
        int ageTo = Convert.ToInt32(txtTo.Text.Trim());

        if (ageFrom > 0 && ageTo > 0)
        {
            RadGridReport.MasterTableView.VirtualItemCount = PatientManager.GetAnalyzerCommonDiagnosesCount(sex, ageFrom, ageTo);

            DataSet ds = PatientManager.GetAnalyzerCommonDiagnoses(sex, ageFrom, ageTo, RadGridReport.PageSize, RadGridReport.CurrentPageIndex);
            if (ds.Tables[0] != null)
            {
                RadGridReport.DataSource = ds.Tables[0];
            }
        }
    }
    protected void RadGridReport_DetailTableDataBind(object source, Telerik.WebControls.GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        switch (e.DetailTableView.Name)
        {
            case "InnerGrid":
                char sex = Convert.ToChar(ddlSex.SelectedValue);
                int ageFrom = Convert.ToInt32(txtFrom.Text.Trim());
                int ageTo = Convert.ToInt32(txtTo.Text.Trim());

                string codeType = Convert.ToString(dataItem.GetDataKeyValue("CODE_TYPE"));
                string medcode = Convert.ToString(dataItem.GetDataKeyValue("MEDCODE"));
                string codeVersion = Convert.ToString(dataItem.GetDataKeyValue("CODE_VERSION"));

                int patientCount = PatientManager.GetAnalyzerPatientByMedcodeAgeSexCount(sex, ageFrom, ageTo, codeType, medcode, Convert.ToInt32(codeVersion));
                e.DetailTableView.VirtualItemCount = patientCount;
                
                DataTable dtPatients = PatientManager.GetAnalyzerPatientByMedcodeAgeSex(sex, ageFrom, ageTo, codeType, medcode, Convert.ToInt32(codeVersion), e.DetailTableView.PageSize, e.DetailTableView.CurrentPageIndex);

                if (dtPatients != null)
                {
                    e.DetailTableView.DataSource = dtPatients;
                }
                break;
            default:
                break;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        RadGridReport.Visible = true;
        RadGridReport.Rebind();
    }
}
