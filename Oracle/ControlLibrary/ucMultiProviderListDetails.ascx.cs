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
using System.Drawing;

public partial class Oracle_ControlLibrary_ucMultiProviderListDetails : System.Web.UI.UserControl
{

    long patientKay;
    string user, primaryProviderId;
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
        //if (!IsPostBack)
        {
            patientKay = Int64.Parse(Request.QueryString["PatientKey"].ToString());

            user = HttpContext.Current.User.Identity.Name;
            DataSet ds = PatientManager.GetPrincipalHealthCareInfo(patientKay, user);
            primaryProviderId = ds.Tables[0].Rows[0]["DoctorID"].ToString().Trim();

            if (NeedSorting == false)
            {
                grdProvider.DataSource = PatientManager.GetMultipleProviderInfo(patientKay);
                grdProvider.DataBind();
            }
        }
    }

    public void RefreshProviderGrid(string selectedColor)
    {
        SelectedColor = selectedColor;
        grdProvider.DataSource = PatientManager.GetMultipleProviderInfo(patientKay);
        grdProvider.DataBind();
    }
    protected void grdProvider_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Details")
        {
            GridView gv = (GridView)sender;
            int index = int.Parse(e.CommandArgument.ToString());
            string doctorID = gv.DataKeys[index].Value.ToString();
            //string desc = gv.Rows[index].Cells[1].Text;

            //Now get the details
            dvwProvider.DataSource = PatientManager.GetProviderInfo(doctorID).Tables[0];
            dvwProvider.DataBind();

            GridView grdHFs = dvwProvider.FindControl("grdHFs") as GridView;
            grdHFs.DataSource = PatientManager.GetProviderInfo(doctorID).Tables[1];
            grdHFs.DataBind();
        }
    }
    public string LoadDetails(string doctorID)
    {
        dvwProvider.DataSource = PatientManager.GetProviderInfo(doctorID).Tables[0];
        dvwProvider.DataBind();
        return "Success";
    }
    protected void grdProvider_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //try
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        DataRowView rowView = e.Row.DataItem as DataRowView;
        //        string providerID = rowView.Row["DoctorID"].ToString();
        //        if (providerID.Equals(primaryProviderId))
        //        {
        //            lblPrimaryProviderName.Text = rowView.Row["FirstName"].ToString() + " " + rowView.Row["LastName"].ToString() + ", " + rowView.Row["Degree"].ToString() + "";

        //            //e.Row.BackColor = Color.RosyBrown;
        //            //e.Row.Font.Bold = true;
        //            //e.Row.ForeColor = Color.Wheat;
        //        }
        //        for (int i = 0; i < e.Row.Cells.Count - 1; i++)
        //        {
        //            e.Row.Cells[i].Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
        //            e.Row.Cells[i].Attributes["onmouseout"] = "this.style.textDecoration='none';";
        //            e.Row.Cells[i].Attributes["onclick"] = " __doPostBack('multiProviderList$grdProvider','Details$" + e.Row.RowIndex + "');";
        //        }

        //    }
        //}
        //catch { }


    }
    public string GetFreqLabelText(string CodeFreq, string DoctorID)
    {
        string hyperLink = "<a style=\"color:blue;text-decoration:underline;cursor:hand;\"  onClick='" + " ShowAllDate(&quot;" + DoctorID + "&quot;,&quot;" + patientKay + "&quot;);" + "'>" + CodeFreq + "</a>";
        return hyperLink;
    }

    protected void grdProvider_Sorting(object sender, GridViewSortEventArgs e)
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

        DataView dtView = new DataView(PatientManager.GetMultipleProviderInfo(patientKay).Tables[0]);
        dtView.Sort = sortExpression + " " + sortDir;
        grdProvider.DataSource = dtView.ToTable();
        grdProvider.DataBind();

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
        NeedSorting = true;

    }
    protected void grdProvider_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DataRowView rowView = e.Row.DataItem as DataRowView;
                string providerID = rowView.Row["DoctorID"].ToString();
                if (providerID.Equals(primaryProviderId))
                {
                    lblPrimaryProviderName.Text = rowView.Row["FirstName"].ToString() + " " + rowView.Row["LastName"].ToString() + ", " + rowView.Row["Degree"].ToString() + "";

                    //e.Row.BackColor = Color.RosyBrown;
                    //e.Row.Font.Bold = true;
                    //e.Row.ForeColor = Color.Wheat;
                }
                for (int i = 0; i < e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
                    e.Row.Cells[i].Attributes["onmouseout"] = "this.style.textDecoration='none';";
                    e.Row.Cells[i].Attributes["onclick"] = " __doPostBack('multiProviderList$grdProvider','Details$" + e.Row.RowIndex + "');";
                }

                // set background color of the row
                string lastDate = rowView.Row["LastCodeDate"].ToString();
                //Get the Back Color for the record based on "LastDate"
                if (!PatientManager.DateRangeApplied)
                    e.Row.BackColor = PatientManager.GetRowColorBasedOnDate(DateTime.Parse(lastDate).ToString("MM-dd-yyyy"));
                if (SelectedColor != null)
                {
                    string colorHex = System.Drawing.ColorTranslator.ToHtml(e.Row.BackColor);
                    if (SelectedColor.IndexOf(colorHex) < 0) e.Row.BackColor = System.Drawing.Color.Transparent;
                }
            }
        }
        catch { }
    }
    protected void grdHFs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink institutionLink = e.Row.FindControl("lnkHF") as HyperLink;
            if (institutionLink != null)
            {
                institutionLink.Attributes["href"] = "#";
                DataRowView rowView = e.Row.DataItem as DataRowView;
                if (rowView != null)
                {
                    string codesStr = "HasCode=true";
                    codesStr += "&PatientKey=" + patientKay;
                    codesStr += "&ProviderID=" + rowView.Row["DoctorID"].ToString();
                    institutionLink.Text = rowView.Row["INSTITUTION_ID"].ToString();
                    institutionLink.Attributes["title"] = rowView.Row["InstitutionName"].ToString();
                    institutionLink.Attributes["onclick"] = string.Format("return ShowInstitutionDetails('{0}','{1}');", rowView["INSTITUTION_ID"].ToString(), codesStr);
                }
            }
        }
    }
    protected void grdProvider_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdProvider.PageIndex = e.NewPageIndex;
      
        if (NeedSorting == false)
        {
            grdProvider.DataSource = PatientManager.GetMultipleProviderInfo(patientKay);
            grdProvider.DataBind();
        }
        else
        {
            DataView dtView = new DataView(PatientManager.GetMultipleProviderInfo(patientKay).Tables[0]);
            dtView.Sort = SortExpressionWithDirection;
            grdProvider.DataSource = dtView.ToTable();
            grdProvider.DataBind();
        }
    }
}
