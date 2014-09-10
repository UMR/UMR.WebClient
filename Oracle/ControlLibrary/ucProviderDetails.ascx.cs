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
using System.Text;


public partial class Oracle_ControlLibrary_ucProviderDetails : System.Web.UI.UserControl
{
    private string  id;
    public string ProviderFilter
    {
        get
        {
            if (ViewState["ProviderFilter"] != null)
                return ViewState["ProviderFilter"].ToString();
            else return null;
        }
        set
        {
            ViewState["ProviderFilter"] = value;
        }
    }
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
    //private RadGrid providerGrid;

    //public RadGrid ProviderGrid
    //{
    //    get { return providerGrid; }
    //}

    //protected override void OnInit(EventArgs e)
    //{
    //    providerGrid = this.RadGrid1;
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCode();
            if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
            {
                if (Request.QueryString["DispType"].ToString() == "S")
                {
                    id = Request.QueryString["ID"].ToString();
                    //grdProviderDetails.DataSource = PatientManager.GetProviderInfo(id);
                    //grdProviderDetails.DataBind();

                    detailsProviderDetails.DataSource = PatientManager.GetProviderInfo(id).Tables[0];
                    detailsProviderDetails.DataBind();

                    GridView grdHFs = detailsProviderDetails.FindControl("grdHFs") as GridView;
                    DataView dtView = PatientManager.GetProviderInfo(id).Tables[1].Copy().DefaultView;
                    dtView.RowFilter = ProviderFilter;
                    grdHFs.DataSource = dtView.ToTable();
                    grdHFs.DataBind();

                }
            }

        }
        UcLegendCompact1.FilterApplied += new LegendCompactEventHandler(RefreshGrid);
    }
    protected void RefreshGrid(object sender, string selectedColor)
    {
        SelectedColor = selectedColor;
        LoadCode();
    }
    private void LoadCode()
    {
        bool hasCode = false;
        if (Request.QueryString["HasCode"] != null)
        {
            hasCode = Boolean.Parse(Request.QueryString["HasCode"].Trim());
        }
        if (hasCode == false) { codeDiv.Visible = false; return; }


        string code = string.Empty;
        string type = string.Empty;
        string version = string.Empty;
        string codedate = string.Empty;
        string doctorid = string.Empty;
        long patientKey= 0;

        if (Request.QueryString["Code"] != null) code = Request.QueryString["Code"].Trim();
        if (Request.QueryString["Type"] != null) type = Request.QueryString["Type"].Trim();
        if (Request.QueryString["Version"] != null) version = Request.QueryString["Version"].Trim();
        if (Request.QueryString["CodeDate"] != null) codedate = Request.QueryString["CodeDate"].Trim();
        if (Request.QueryString["ID"] != null) doctorid = Request.QueryString["ID"].Trim();
        if (Request.QueryString["PatientKey"] != null) patientKey = Int64.Parse(Request.QueryString["PatientKey"].Trim());

        DataSet ds = PatientManager.GetMPIInfoSpecific(patientKey, doctorid, DateTime.Parse(codedate.Trim()).ToString("dd-MMM-yy hh.mm.ss tt"));
        DataView dtView = ds.Tables[0].Copy().DefaultView;
        dtView.RowFilter = "Code='" + code + "' AND Type='" + type + "' AND Version='" + version + "' AND PROVIDERID='" + doctorid + "'";
        grdCode.DataSource = dtView.ToTable();
        grdCode.DataBind();
        codeDiv.Visible = true;

    }



    //protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    //{
    //    string dispType = Request.QueryString["DispType"].ToString();
    //    if (dispType.Equals("S"))       //This request is coming from other pages (RDDiagDetails,RDMedDetails,MPI,LRPM etc.)Details...
    //    {
    //        id = Request.QueryString["ID"].ToString();
    //        RadGrid1.DataSource = PatientManager.GetProviderInfo(id);
    //    }
    //    //else                            //This request will generate only from the HPList QuickLink in Result Page...
    //    //{
    //    //    id = Request.QueryString["ID"].ToString();
    //    //    modifierID = Request.QueryString["ModifierID"].ToString();
    //    //    RadGrid1.DataSource = PatientManager.GetMultipleProviderInfo(id, modifierID);
    //    //}
    //}

    protected void grdProviderDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {

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
                    institutionLink.Text = rowView.Row["INSTITUTION_ID"].ToString();
                    institutionLink.Attributes["onclick"] = string.Format("return ShowInstitutionDetails('{0}');", rowView["INSTITUTION_ID"].ToString());
                }
            }
        }
    }
    protected void grdCode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = e.Row.DataItem as DataRowView;
            string dateOfService = DateTime.Parse(rowView.Row["DateOfService"].ToString()).ToString("MM-dd-yyyy"); ;
            if (!PatientManager.DateRangeApplied)
                e.Row.BackColor = PatientManager.GetRowColorBasedOnDate(dateOfService);
            if (SelectedColor != null)
            {
                string colorHex = System.Drawing.ColorTranslator.ToHtml(e.Row.BackColor);
                if (SelectedColor.IndexOf(colorHex) < 0) e.Row.BackColor = System.Drawing.Color.Transparent;
            }

            HyperLink medicalContentIndexLink = e.Row.FindControl("lnkMedicalContentIndex") as HyperLink;
            medicalContentIndexLink.Text = rowView["MedicalContentIndex"].ToString();
            string medCodeDesciption = rowView["CodeDesc"].ToString().Replace("__", ", ");
            medCodeDesciption = medCodeDesciption.Replace("'", "\\'");
            medCodeDesciption = medCodeDesciption.Replace("\0", "");

            string codeDesciptionHistory = rowView["CodeDescriptionHistory"].ToString().Replace("__", ", ");
            codeDesciptionHistory = codeDesciptionHistory.Replace("'", "\\'");
            codeDesciptionHistory = codeDesciptionHistory.Replace("\0", "");
            codeDesciptionHistory = "<div style=\"color:green;\">" + codeDesciptionHistory + "</div>";

            medicalContentIndexLink.Attributes["title"] = medCodeDesciption + codeDesciptionHistory;
            medicalContentIndexLink.Attributes["class"] = "tooltip";

            ProviderFilter = "INSTITUTION_ID='" + rowView["InstituteCode"].ToString()+"'";

            Literal ltCodeModifier = e.Row.FindControl("ltCodeModifier") as Literal;
            if (ltCodeModifier != null)
            {
                rowView = e.Row.DataItem as DataRowView;
                if (rowView != null)
                {
                    string codeType = rowView.Row["Type"].ToString().Trim();
                    string medCode = rowView.Row["Code"].ToString().Trim();
                    int codeVersion = Convert.ToInt32(rowView.Row["Version"].ToString().Trim());

                    DataSet ds = PatientManager.GetCodeModifiersByMedcode(codeType, medCode, codeVersion);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            sb.Append("<a class=\"tooltip\" title=\"" + ds.Tables[0].Rows[i]["LongDescription"].ToString() + "\" style=\"color:#355E3B;\">" + ds.Tables[0].Rows[i]["ModifierCode"].ToString() + "</a>");
                            if (i < ds.Tables[0].Rows.Count - 1)
                            {
                                sb.Append(", ");
                            }
                        }
                        ltCodeModifier.Text = sb.ToString();
                    }
                    else
                    {
                        ltCodeModifier.Text = "N/A";
                    }
                }
            }
        }
    }
}
