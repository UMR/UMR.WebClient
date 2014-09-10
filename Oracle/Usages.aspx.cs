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
using System.Collections.Generic;

public partial class Oracle_Usages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void RadGridUsages_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        RadGridUsages.DataSource = PatientManager.GetUsagesLogins();
    }
    protected void RadGridUsages_DetailTableDataBind(object source, Telerik.WebControls.GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        switch (e.DetailTableView.Name)
        {
            case "InnerGrid":
                {
                    object dataKeyValue = dataItem.GetDataKeyValue("USER_ID").ToString();

                    string userid = Convert.ToString(dataKeyValue);
                    DataSet ds = PatientManager.GetUsagesLoginsByUserID(userid);

                    e.DetailTableView.DataSource = GetAccessHistory(ds);
                    break;
                }
        }
    }

    private List<AccessHitory> GetAccessHistory(DataSet dataSet)
    {
        List<AccessHitory> accessHistory = new List<AccessHitory>();
        if (dataSet.Tables.Count > 0)
        {
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                string userId = dataSet.Tables[0].Rows[i][0].ToString();
                DateTime mindate = (DateTime)(dataSet.Tables[0].Rows[i][1]);
                DateTime maxDate = DateTime.Now.AddYears(25);

                DataView dv = new DataView(dataSet.Tables[0]);
                dv.RowFilter = "LOGINTIME > '" + mindate.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                dv.Sort = "LOGINTIME";
                DataTable dt = dv.ToTable();
                if (dt.Rows.Count > 0)
                {
                    maxDate = DateTime.Parse(dt.Rows[0][1].ToString());
                }

                DataSet ds = PatientManager.GetUsagesPatientAccess(userId, mindate, maxDate);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        AccessHitory obj = new AccessHitory();
                        obj.LoginTime = DateTime.Parse(dataSet.Tables[0].Rows[i][1].ToString());
                        obj.PatientName = ds.Tables[0].Rows[j][0].ToString();
                        accessHistory.Add(obj);
                    }
                }
                else
                {
                    AccessHitory obj = new AccessHitory();
                    obj.LoginTime = DateTime.Parse(dataSet.Tables[0].Rows[i][1].ToString());
                    obj.PatientName = "N/A";
                    accessHistory.Add(obj);
                }
            }
        }
        return accessHistory;
    }
}

public class AccessHitory
{
    private DateTime _LoginTime;

    public DateTime LoginTime
    {
        get { return _LoginTime; }
        set { _LoginTime = value; }
    }
    private string _PatientName;

    public string PatientName
    {
        get { return _PatientName; }
        set { _PatientName = value; }
    }

}
