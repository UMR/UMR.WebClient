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
using System.Collections;
using System.Collections.Generic;

public partial class ControlLibrary_crtlSearchGrid : System.Web.UI.UserControl
{
    public string PatientID
    {
        get { return ViewState["PatientID"] == null ? string.Empty : ViewState["PatientID"].ToString(); }
        set { ViewState["PatientID"] = value; }
    }
    public string Modifier
    {
        get { return ViewState["Modifier"] == null ? string.Empty : ViewState["Modifier"].ToString(); }
        set { ViewState["Modifier"] = value; }
    }
    public string FirstName
    {
        get { return ViewState["FirstName"] == null ? string.Empty : ViewState["FirstName"].ToString(); }
        set { ViewState["FirstName"] = value; }
    }
    public string LastName
    {
        get { return ViewState["LastName"] == null ? string.Empty : ViewState["LastName"].ToString(); }
        set { ViewState["LastName"] = value; }
    }
    public DateTime DOB
    {
        get { return Convert.ToDateTime(ViewState["DOB"]); }
        set { ViewState["DOB"] = value; }
    }
    
    private DataTable dtLastPatientID;
    public RadGrid Grid
    {
        get { return RadGrid1; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtLastPatientID = PatientManager.GetIdForLastPatientAccessed(HttpContext.Current.User.Identity.Name.ToUpper());
            if (dtLastPatientID.Rows.Count > 0)
            {
                RadGrid1.DataSource = dtLastPatientID;
                RadGrid1.Rebind();

            }
            else
            {
                RadGrid1.DataSource = string.Empty;
                RadGrid1.Rebind();
            }
        }
    }

    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            HyperLink linkID = item["PatientID"].Controls[0] as HyperLink;
            
            DataRowView drv = e.Item.DataItem as DataRowView;

            if (linkID != null)
            {
                //linkID.Attributes["onMouseOver"] = string.Format("return HideStatus()");
                linkID.ForeColor = System.Drawing.Color.Blue;

                if (drv != null)
                {
                    //string name = drv["FirstName"].ToString() + " " + drv["LastName"].ToString();
                    //linkID.Attributes["onClick"] = string.Format("ChangeTitle('Medical History Index for: " + name + "')");
                    long patientKey = Convert.ToInt64(drv["PatientKey"].ToString());
                    int alertCount = PatientManager.GetPatientGetActiveAlertCount(patientKey, (byte)EnumAlertStatus.NurseDone);

                    if (alertCount <= 0)
                    {
                        int docAlertCount = PatientManager.GetPatientGetActiveDOCAlertCount(patientKey, (byte)EnumAlertStatus.DocDone);
                        if (docAlertCount <= 0)
                        {
                            Label butt = (Label)item["DANAlert"].FindControl("lblBut");
                            butt.Text = "Nurse Alert Network";
                        }
                        else
                        {
                            Label butt = (Label)item["DANAlert"].FindControl("lblBut");
                            butt.Text = "P.E.T.N.";
                        }
                    }
                    else
                    {
                        Label butt = (Label)item["DANAlert"].FindControl("lblBut");
                        butt.Text = "Provider Alert Network";
                    }
                }

            }

                    
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        string orderBy = string.Empty;
        if (RadGrid1.MasterTableView.SortExpressions.Count != 0)
        {
            orderBy = string.Format("{0} {1}", RadGrid1.MasterTableView.SortExpressions[0].FieldName, RadGrid1.MasterTableView.SortExpressions[0].SortOrderAsString());
        }
        RadGrid1.MasterTableView.VirtualItemCount = PatientManager.GetPatientCount(PatientID, Modifier, FirstName, LastName, DOB);
        DataTable dtPatient = PatientManager.GetPatientList(PatientID, Modifier, FirstName, LastName, DOB, RadGrid1.CurrentPageIndex, RadGrid1.PageSize, orderBy);
        RadGrid1.DataSource = dtPatient;
    }  
}
