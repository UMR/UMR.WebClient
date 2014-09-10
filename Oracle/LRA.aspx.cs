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

public partial class Oracle_LRA : System.Web.UI.Page
{
    private long patientKey;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
        {
            patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
        }
        //RadGridLRA.DataBind();
    }
    protected void RadGridLRA_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        string gridSortString = this.RadGridLRA.MasterTableView.SortExpressions.GetSortString();
        //Apply Sorting
        DataView dtView = new DataView(PatientManager.GetLRAInfo(patientKey).Tables[0]);
        dtView.Sort = gridSortString;
        RadGridLRA.MasterTableView.DataSource = dtView.ToTable();

        //RadGridLRA.MasterTableView.DataSource = PatientManager.GetLRAInfo(id, modifierID);
    }
    public string GetQueryString()
    {
        return "PatientKey=" + Request.QueryString["PatientKey"] + "";
    }
    public string GetQueryStringWithPageCount()
    {
        return "PatientKey=" + Request.QueryString["PatientKey"] + "&PageCount=" + RadGridLRA.MasterTableView.PageCount;
    }
    protected void RadGridLRA_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            HyperLink accessorLink = e.Item.FindControl("lnkAccessorName") as HyperLink;
            ImageButton imgDetail = e.Item.FindControl("btnDetail") as ImageButton;
            if (accessorLink != null)
            {
                accessorLink.Attributes["href"] = "#";
            }
            DataRowView drv;
            if (e.Item.DataItem != null)
            {
                drv = e.Item.DataItem as DataRowView;
                if (drv != null)
                {
                    accessorLink.Text = drv["UserName"].ToString();
                }
                accessorLink.Attributes["onclick"] = string.Format("return ShowAccessorDetails('{0}');", drv["UserName"]);

                if (imgDetail != null)
                {

                    imgDetail.ToolTip = "Drill down";
                    imgDetail.Attributes["onClick"] = string.Format("return ShowAccessorDetails('{0}');", drv["UserName"]);
                }
            }

        }
    }
    protected void RadGridLRA_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridSortExpression sortExpr = new GridSortExpression();
        sortExpr.FieldName = e.SortExpression;
        sortExpr.SortOrder = e.OldSortOrder;

        e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);

        if (e.OldSortOrder == GridSortOrder.None)
        {
            Session["SORTEXPRESSON"] = e.SortExpression + " ASC";
        }
        else if (e.OldSortOrder == GridSortOrder.Ascending)
        {
            Session["SORTEXPRESSON"] = e.SortExpression + " DESC";
        }
        else
        {
            Session["SORTEXPRESSON"] = e.SortExpression + " ASC";
        }
        // we will read this session from LRAPrint pages
    }
    public string GetDateString(string userid, string date)
    {
        string dateStr = "N/A";
        System.Data.DataTable dtUserInfo = PatientManager.GetUserInfo(userid);
        if (dtUserInfo.Rows[0]["Industry"].ToString().Trim() == "Healthcare")
        {
            DateTime dt = DateTime.Parse(date);
            DateTime dtCurrentlyInUse = new DateTime(1800, 1, 1);
            if (dt == dtCurrentlyInUse)
            {
                dateStr = "Currently in use";
            }
            else
            {
                dateStr = String.Format("{0:MM-dd-yyyy hh:mm:ss tt}", dt);
            }
        }
        return dateStr;
    }

    public static string GetLoginDuration(object start, object end)
    {
        string duration = "-";
        if ((start != null && end != null) && (start != DBNull.Value && end != DBNull.Value))
        {
            DateTime dtEnd = Convert.ToDateTime(end);
            DateTime dtStart = Convert.ToDateTime(start);

            if (dtEnd.Year != 1800)
            {
                duration = (dtEnd - dtStart).Hours + " hour(s) " + (dtEnd - dtStart).Minutes + " minute(s) " + (dtEnd - dtStart).Seconds + " second(s)";
            }
        }

        return duration;
    }
}
