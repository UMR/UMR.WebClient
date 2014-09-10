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

public partial class Oracle_ControlLibrary_ucEmergencyContact2 : System.Web.UI.UserControl
{
    private long patientKey;
    private string user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            user = HttpContext.Current.User.Identity.Name;
            patientKey = long.Parse(Request.QueryString["PatientKey"].ToString());

            grdViewEmergencyContact.DataSource = PatientManager.GetEmergencyContact(patientKey, user);
            grdViewEmergencyContact.DataBind();
        }
    }

    protected void grdViewEmergencyContact_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            GridView gv = (GridView)sender;
            int index = int.Parse(e.CommandArgument.ToString());

            string contactFirstName = gv.DataKeys[index].Values[0].ToString();
            string contactLastName = gv.DataKeys[index].Values[1].ToString();
            string contactRelation = gv.DataKeys[index].Values[2].ToString();

            user = HttpContext.Current.User.Identity.Name;
            patientKey = long.Parse(Request.QueryString["PatientKey"].ToString());

            DataTable dtMain = PatientManager.GetEmergencyContact(patientKey, user).Tables[0];
            DataRow[] rows = dtMain.Select("ContactFirstName='" + contactFirstName + "' AND ContactLastName='" + contactLastName + "' AND ContactRelation='" + contactRelation + "'");

            DataTable dt = new DataTable();
            dt.Columns.Add("ContactFirstName");
            dt.Columns.Add("ContactLastName");
            dt.Columns.Add("ContactRelation");
            dt.Columns.Add("ContactHomePhone");
            dt.Columns.Add("ContactBusinessPhone");
            dt.Columns.Add("ContactCellPhone");
            dt.Columns.Add("ContactPagerNo");
            dt.Columns.Add("ContactEmail");
            dt.Columns.Add("ContactCountry");
            dt.Columns.Add("ContactCityTown");
            dt.Columns.Add("ContactZIP");
            dt.Columns.Add("ContactStreetAddress");
            dt.Columns.Add("ContactState");
            dt.Columns.Add("ContactCounty");
            dt.Rows.Add(rows[0]["ContactFirstName"].ToString(),
                rows[0]["ContactLastName"].ToString(),
                rows[0]["ContactRelation"].ToString(),
                rows[0]["ContactHomePhone"].ToString(),
                rows[0]["ContactBusinessPhone"].ToString(),
                rows[0]["ContactCellPhone"].ToString(),
                rows[0]["ContactPagerNo"].ToString(),
                rows[0]["ContactEmail"].ToString(),
                rows[0]["ContactCountry"].ToString(),
                rows[0]["ContactCityTown"].ToString(),
                rows[0]["ContactZIP"].ToString(),
                rows[0]["ContactStreetAddress"].ToString(),
                rows[0]["ContactState"].ToString(),
                rows[0]["ContactCounty"].ToString());
            dvwEmergencyContact.DataSource = dt;
            dvwEmergencyContact.DataBind();
        }
    }
    protected void grdViewEmergencyContact_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = "__doPostBack('ucEmergency$grdViewEmergencyContact','Select$" + e.Row.RowIndex + "')";
        }
    }
    protected void grdViewEmergencyContact_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;
        string sortDir = "ASC";
        if (ViewState["SHORTDIRECTION"] != null)
        {
            sortDir = ViewState["SHORTDIRECTION"].ToString();
        }

        if (ViewState["LASTSORTEXPRESSION"] != null)
        {
            if (!ViewState["LASTSORTEXPRESSION"].ToString().Equals(sortExpression))
            {
                sortDir = "ASC";
            }
        }

        user = HttpContext.Current.User.Identity.Name;
        patientKey = long.Parse(Request.QueryString["PatientKey"].ToString());

        DataView dtView = new DataView(PatientManager.GetEmergencyContact(patientKey, user).Tables[0]);
        dtView.Sort = sortExpression + " " + sortDir;
        grdViewEmergencyContact.DataSource = dtView.ToTable();
        grdViewEmergencyContact.DataBind();

        if (sortDir == "ASC")
        {
            ViewState["SHORTDIRECTION"] = "DESC";
        }
        else
        {
            ViewState["SHORTDIRECTION"] = "ASC";
        }

        ViewState["LASTSORTEXPRESSION"] = sortExpression;

    }
}
