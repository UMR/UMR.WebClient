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

public partial class Oracle_ControlLibrary_ucInsuranceInfo2 : System.Web.UI.UserControl
{
    private long patientKey;
    private bool NeedSorting
    {
        get
        {
            if (ViewState["NeedSorting"] != null) return bool.Parse(ViewState["NeedSorting"].ToString());
            else return false;
        }
        set
        {
            ViewState["NeedSorting"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString != null)
        {
            patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());

            if (NeedSorting == false)
            {
                grdInsurenceInfo.DataSource = PatientManager.GetInsuranceInformation(patientKey);
                grdInsurenceInfo.DataBind();
            }
        }
    }
    //protected void RadGridInsuranceInfo_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    //{
    //    RadGridInsuranceInfo.DataSource = PatientManager.GetInsuranceInformation(id, modifierID);
    //}
    protected void grdInsurenceInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            GridView gv = (GridView)sender;
            int index = int.Parse(e.CommandArgument.ToString());
            string companyName = gv.DataKeys[index].Value.ToString();

            //DataTable dt = new DataTable();
            //dt.Columns.Add("NameOfInsCompany");
            //dt.Columns.Add("PolicyNo");
            //dt.Columns.Add("ExpirationDate");
            //dt.Columns.Add("Type");
            //dt.Columns.Add("Phone");
            //dt.Columns.Add("Fax");
            //dt.Columns.Add("Email");
            //dt.Columns.Add("Website");
            //dt.Rows.Add(((DataTable)(gv.DataSource)).Rows[index]["NameOfInsCompany"].ToString(),
            //    ((DataTable)(gv.DataSource)).Rows[index]["PolicyNo"].ToString(),
            //    ((DataTable)(gv.DataSource)).Rows[index]["ExpirationDate"].ToString(),
            //    ((DataTable)(gv.DataSource)).Rows[index]["Type"].ToString(),
            //    ((DataTable)(gv.DataSource)).Rows[index]["Phone"].ToString(),
            //    ((DataTable)(gv.DataSource)).Rows[index]["Fax"].ToString(),
            //    ((DataTable)(gv.DataSource)).Rows[index]["Email"].ToString(),
            //    ((DataTable)(gv.DataSource)).Rows[index]["Website"].ToString());
            //dvwInsurenceInfo.DataSource = dt;
            //dvwInsurenceInfo.DataBind();

            DataTable dt = PatientManager.GetInsuranceInformation(patientKey);
            DataRow[] rows = dt.Select("NameOfInsCompany='" + companyName + "'");

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("NameOfInsCompany");
            dt2.Columns.Add("PolicyNo");
            dt2.Columns.Add("CommencementDate");
            dt2.Columns.Add("ExpirationDate");
            dt2.Columns.Add("Type");
            dt2.Columns.Add("Phone");
            dt2.Columns.Add("Fax");
            dt2.Columns.Add("Email");
            dt2.Columns.Add("Website");
            for (int i = 0; i < rows.Length; i++)
            {
                dt2.Rows.Add(rows[i]["NameOfInsCompany"].ToString(),
                             rows[i]["PolicyNo"].ToString(),
                             rows[i]["CommencementDate"].ToString(),
                             rows[i]["ExpirationDate"].ToString(),
                             rows[i]["Type"].ToString(),
                             rows[i]["Phone"].ToString(),
                             rows[i]["Fax"].ToString(),
                             rows[i]["Email"].ToString(),
                             rows[i]["Website"].ToString());
            }

            dvwInsurenceInfo.DataSource = dt2;
            dvwInsurenceInfo.DataBind();


        }
    }
    protected void grdInsurenceInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = "__doPostBack('ucIns$grdInsurenceInfo','Select$" + e.Row.RowIndex + "')";
        }
    }
    protected void grdInsurenceInfo_Sorting(object sender, GridViewSortEventArgs e)
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

        DataView dtView = new DataView(PatientManager.GetInsuranceInformation(patientKey));
        dtView.Sort = sortExpression + " " + sortDir;
        grdInsurenceInfo.DataSource = dtView.ToTable();
        grdInsurenceInfo.DataBind();

        if (sortDir == "ASC")
        {
            ViewState["SHORTDIRECTION"] = "DESC";
        }
        else
        {
            ViewState["SHORTDIRECTION"] = "ASC";
        }

        ViewState["LASTSORTEXPRESSION"] = sortExpression;
        NeedSorting = true;
    }
}
