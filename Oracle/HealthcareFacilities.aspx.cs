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

public partial class Oracle_HealthcareFacilities : System.Web.UI.Page
{
    long patientKey;
    private string SelectedColor
    {
        get
        {
            if (ViewState["SelectedColor"] != null)
            {
                return ViewState["SelectedColor"].ToString();
            }
            else
            {
                return null;
            }
        }
        set
        {
            ViewState["SelectedColor"] = value;
        }
    }
    private string SortExpressionWithDirection
    {
        get
        {
            if (ViewState["SortExpressionWithDirection"] != null) return ViewState["SortExpressionWithDirection"].ToString();
            else return null;
        }
        set
        {
            ViewState["SortExpressionWithDirection"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        patientKey = Int64.Parse(Request.QueryString["PatientKey"].Trim());
       
        if (!Page.IsPostBack)
        {
            grdInstitutions.DataSource = PatientManager.GetInstitutionsForPatients(patientKey);
            grdInstitutions.DataBind();
        }

        UcLegendCompact1.FilterApplied += new LegendCompactEventHandler(RefreshGrid);
    }
    protected void RefreshGrid(object sender, string selectedColor)
    {
        SelectedColor = selectedColor;
        grdInstitutions.DataSource = PatientManager.GetInstitutionsForPatients(patientKey);
        grdInstitutions.DataBind();
    }
    protected void grdInstitutions_Sorting(object sender, GridViewSortEventArgs e)
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

        DataView dtView = new DataView(PatientManager.GetInstitutionsForPatients(patientKey).Tables[0]);
        dtView.Sort = sortExpression + " " + sortDir;
        grdInstitutions.DataSource = dtView.ToTable();
        grdInstitutions.DataBind();

        SortExpressionWithDirection = sortExpression + " " + sortDir;
        
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
    protected void grdInstitutions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = e.Row.DataItem as DataRowView;
            string lastDate = rowView.Row["LastCodeDate"].ToString();
            //Get the Back Color for the record based on "LastDate"
            if (!PatientManager.DateRangeApplied)
                e.Row.BackColor = PatientManager.GetRowColorBasedOnDate(DateTime.Parse(lastDate).ToString("MM-dd-yyyy"));
            if (SelectedColor != null)
            {
                string colorHex = System.Drawing.ColorTranslator.ToHtml(e.Row.BackColor);
                if (SelectedColor.IndexOf(colorHex) < 0) e.Row.BackColor = System.Drawing.Color.Transparent;
            }

            for (int i = 0; i < e.Row.Cells.Count - 1; i++)
            {
                e.Row.Cells[i].Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
                e.Row.Cells[i].Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.Cells[i].Attributes["onclick"] = " __doPostBack('grdInstitutions','Details$" + e.Row.RowIndex + "');";
            }
            string institutionID = rowView.Row["InstitutionID"].ToString();
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //e.Row.Attributes["onclick"] = "ShowInstitutionDrilldown('" + institutionID + "','" + id + "','" + modifierID + "');";
        }
    }
    public string GetFreqLabelText(string CodeFreq, string institutionID)
    {
        string hyperLink = "<a style=\"color:blue;text-decoration:underline;cursor:hand;\"  onClick='" + " ShowInstitutionDrilldown(&quot;" + institutionID + "&quot;,&quot;" + patientKey + "&quot;);" + "'>" + CodeFreq + "</a>";
        return hyperLink;
    }
    protected void grdInstitutions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Details")
        {
            GridView gv = (GridView)sender;
            int index = int.Parse(e.CommandArgument.ToString());
            string institutionID = gv.DataKeys[index].Value.ToString();

            //Now get the details
            dvwHF.DataSource = PatientManager.GetInstitutionInfo(institutionID);
            dvwHF.DataBind();
        }
    }
    protected void grdInstitutions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdInstitutions.PageIndex=e.NewPageIndex;
        long patientKey = Int64.Parse(Request.QueryString["PatientKey"].Trim());

        if (SortExpressionWithDirection==null)
        {
            grdInstitutions.DataSource = PatientManager.GetInstitutionsForPatients(patientKey);
            grdInstitutions.DataBind();
        }
        else
        {
            DataView dtView = new DataView(PatientManager.GetInstitutionsForPatients(patientKey).Tables[0]);
            dtView.Sort = SortExpressionWithDirection;
            grdInstitutions.DataSource = dtView.ToTable();
            grdInstitutions.DataBind();
        }
    }
}
