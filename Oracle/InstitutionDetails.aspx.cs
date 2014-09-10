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
using System.Text;

public partial class Oracle_InstitutionDetails : System.Web.UI.Page
{
    private int _maxLength = 20;
    private string institutionCode;
    private string ProviderSelection
    {
        get
        {
            string option = string.Empty;

            if (Session["ProviderSelection"] == null)
            {
                option = "ProviderID";
                Session["ProviderSelection"] = option;
            }
            else
                option = Session["ProviderSelection"].ToString();

            return option;
        }
        set
        {
            Session["ProviderSelection"] = value;
        }
    }
    private string HealthcareSelection
    {
        get
        {
            string option = string.Empty;

            if (Session["HealthcareSelection"] == null)
            {
                option = "HealthcareID";
                Session["HealthcareSelection"] = option;
            }
            else
                option = Session["HealthcareSelection"].ToString();

            return option;
        }
        set
        {
            Session["HealthcareSelection"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
        {
            //id = Request.QueryString["ID"].ToString();
            //modifierID = Request.QueryString["ModifierID"].ToString();
            institutionCode = Request.QueryString["code"].ToString();
        }
        LoadCode();
        LoadCodeSingle();
        DisplayInstitutionInfo(institutionCode);

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

    private void LoadCodeSingle()
    {
        UcLegendCompact2.FilterApplied += new LegendCompactEventHandler(RefreshCodeSingleGrid);
        bool hasCode = false;
        if (Request.QueryString["HasCodeSingle"] != null)
        {
            hasCode = Boolean.Parse(Request.QueryString["HasCodeSingle"].Trim());
        }
        if (hasCode == false) { codedivSingle.Visible = false; return; }


        string code = string.Empty;
        string type = string.Empty;
        string version = string.Empty;
        string codedate = string.Empty;
        string doctorid = string.Empty;
        long PatientKey = 0;

        if (Request.QueryString["CodeMEDNDC"] != null) code = Request.QueryString["CodeMEDNDC"].Trim();
        if (Request.QueryString["Type"] != null) type = Request.QueryString["Type"].Trim();
        if (Request.QueryString["Version"] != null) version = Request.QueryString["Version"].Trim();
        if (Request.QueryString["CodeDate"] != null) codedate = Request.QueryString["CodeDate"].Trim();
        if (Request.QueryString["DoctorID"] != null) doctorid = Request.QueryString["DoctorID"].Trim();
        if (Request.QueryString["PatientKey"] != null) PatientKey = Int64.Parse(Request.QueryString["PatientKey"].Trim());

        DataSet ds = PatientManager.GetMPIInfoSpecific(PatientKey, doctorid, DateTime.Parse(codedate.Trim()).ToString("dd-MMM-yy hh.mm.ss tt"));
        DataView dtView = ds.Tables[0].Copy().DefaultView;
        dtView.RowFilter = "Code='" + code + "' AND Type='" + type + "' AND Version='" + version + "' AND PROVIDERID='" + doctorid + "'";
        grdCodeSingle.DataSource = dtView.ToTable();
        grdCodeSingle.DataBind();
        
        this.CodeSingleColumnVisibility();
        codedivSingle.Visible = true;
    }

    private void DisplayInstitutionInfo(string institutionCode)
    {
        DataSet dsIns = PatientManager.GetInstitutionInfo(institutionCode.Trim());
        dvwInstitution.DataSource = dsIns;
        dvwInstitution.DataBind();
    }
    private void LoadCode()
    {
        UcLegendCompact1.FilterApplied += new LegendCompactEventHandler(RefreshCodeGrid);
        bool hasCode = false;
        if (Request.QueryString["HasCode"] != null)
        {
            hasCode = Boolean.Parse(Request.QueryString["HasCode"].Trim());
        }
        if (hasCode == false) { codeDiv.Visible = false; return; }


        string providerid = string.Empty;
        long patientKey = 0;
        string institutionid = string.Empty;

        if (Request.QueryString["code"] != null) institutionid = Request.QueryString["code"].Trim();
        if (Request.QueryString["ProviderID"] != null) providerid = Request.QueryString["ProviderID"].Trim();
        if (Request.QueryString["PatientKey"] != null) patientKey = Int64.Parse(Request.QueryString["PatientKey"].Trim());

        DataSet ds = PatientManager.GetMPIInfo(patientKey);
        DataView dtView = ds.Tables[0].Copy().DefaultView;
        dtView.RowFilter = "InstituteCode='" + institutionid + "' AND PROVIDERID='" + providerid + "'";
        grdCode.DataSource = dtView.ToTable();
        grdCode.DataBind();
        this.GridCodeColumnVisibility();
        codeDiv.Visible = true;
    }

    protected void RefreshCodeGrid(object sender, string selectedColor)
    {
        SelectedColor = selectedColor;
        grdCode.DataBind();
        this.GridCodeColumnVisibility();
    }
    protected void RefreshCodeSingleGrid(object sender, string selectedColor)
    {
        SelectedColor = selectedColor;
        grdCodeSingle.DataBind();
        this.CodeSingleColumnVisibility();
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

            string medCodeDesciption = rowView["MedicalContentIndex"].ToString().Replace("__", ", ");
            medCodeDesciption = medCodeDesciption.Replace("'", "\\'");
            medCodeDesciption = medCodeDesciption.Replace("\0", "");

            string codeDesciptionHistory = rowView["CodeDescriptionHistory"].ToString().Replace("__", ", ");
            codeDesciptionHistory = codeDesciptionHistory.Replace("'", "\\'");
            codeDesciptionHistory = codeDesciptionHistory.Replace("\0", "");
            codeDesciptionHistory = "<div style=\"color:green;\">" + codeDesciptionHistory + "</div>";

            medicalContentIndexLink.Attributes["title"] = medCodeDesciption + codeDesciptionHistory;
            medicalContentIndexLink.Attributes["class"] = "tooltip";

            //string providerName = rowView["PROVIDERNAME"].ToString();
            string providerName = rowView["DoctorsFirstName"].ToString() + " " + rowView["DoctorsLastName"].ToString();
            string providerId = rowView["PROVIDERID"].ToString();
            if (rowView["DEGREE"] != null && !string.IsNullOrEmpty(rowView["DEGREE"].ToString()))
            {
                providerName += ", " + rowView["DEGREE"].ToString();
            }
            HyperLink lnkProviderName = e.Row.FindControl("lnkProviderName") as HyperLink;
            lnkProviderName.Text = providerName.Trim().Length > _maxLength ? providerName.Substring(0, _maxLength).PadRight(_maxLength + 3, '.') : providerName;

            HyperLink lnkProviderID = e.Row.FindControl("lnkProviderID") as HyperLink;
            lnkProviderID.Text = providerId;

            lnkProviderID.Attributes["title"] = providerName;
            lnkProviderID.Attributes["class"] = "tooltip";

            lnkProviderName.Attributes["title"] = providerName;
            lnkProviderName.Attributes["class"] = "tooltip";


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
    protected void grdCode_Sorting(object sender, GridViewSortEventArgs e)
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


        bool hasCode = false;
        if (Request.QueryString["HasCode"] != null)
        {
            hasCode = Boolean.Parse(Request.QueryString["HasCode"].Trim());
        }
        if (hasCode == false) { codeDiv.Visible = false; return; }


        string providerid = string.Empty;
        long patientKey = 0;
        string institutionid = string.Empty;

        if (Request.QueryString["code"] != null) institutionid = Request.QueryString["code"].Trim();
        if (Request.QueryString["ProviderID"] != null) providerid = Request.QueryString["ProviderID"].Trim();
        if (Request.QueryString["PatientKey"] != null) patientKey = Int64.Parse(Request.QueryString["PatientKey"].Trim());

        DataSet ds = PatientManager.GetMPIInfo(patientKey);
        DataView dtView = ds.Tables[0].Copy().DefaultView;
        dtView.RowFilter = "InstituteCode='" + institutionid + "' AND PROVIDERID='" + providerid + "'";

        dtView.Sort = sortExpression + " " + sortDir;
        grdCode.DataSource = dtView.ToTable();
        grdCode.DataBind();

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
    protected void grdCodeSingle_RowDataBound(object sender, GridViewRowEventArgs e)
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

            //string providerName = rowView["PROVIDERNAME"].ToString();
            string providerName = rowView["DoctorsFirstName"].ToString() + " " + rowView["DoctorsLastName"].ToString();
            string providerId = rowView["PROVIDERID"].ToString();
            if (rowView["DEGREE"] != null && !string.IsNullOrEmpty(rowView["DEGREE"].ToString()))
            {
                providerName += ", " + rowView["DEGREE"].ToString();
            }

            HyperLink lnkProviderName = e.Row.FindControl("lnkProviderName") as HyperLink;
            lnkProviderName.Text = providerName.Trim().Length > _maxLength ? providerName.Substring(0, _maxLength).PadRight(_maxLength + 3, '.') : providerName;

            HyperLink lnkProviderID = e.Row.FindControl("lnkProviderID") as HyperLink;
            lnkProviderID.Text = providerId;

            lnkProviderID.Attributes["title"] = providerName;
            lnkProviderID.Attributes["class"] = "tooltip";

            lnkProviderName.Attributes["title"] = providerName;
            lnkProviderName.Attributes["class"] = "tooltip";


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

    private void CodeSingleColumnVisibility()
    {
        grdCodeSingle.Columns[7].Visible = this.ProviderSelection == "ProviderID" ? false : true;
        grdCodeSingle.Columns[8].Visible = this.ProviderSelection == "ProviderID" ? true: false;
    }
    private void GridCodeColumnVisibility()
    {
        grdCode.Columns[7].Visible = this.ProviderSelection == "ProviderID" ? false : true;
        grdCode.Columns[8].Visible = this.ProviderSelection == "ProviderID" ? true : false;
    }

    protected void btnSingleProviderID_Click(object sender, EventArgs e)
    {
        this.HealthcareSelection = "HealthcareName";
        this.ProviderSelection = "ProviderName";
        this.CodeSingleColumnVisibility();
        //grdCodeSingle.Columns[7].Visible = true;
        //grdCodeSingle.Columns[8].Visible = false;
    }
    protected void btnSingleProviderName_Click(object sender, EventArgs e)
    {
        this.HealthcareSelection = "HealthcareID";
        this.ProviderSelection = "ProviderID";
        this.CodeSingleColumnVisibility();
        //grdCodeSingle.Columns[7].Visible = false;
        //grdCodeSingle.Columns[8].Visible = true;
    }

    protected void btnProviderID_Click(object sender, EventArgs e)
    {
        this.HealthcareSelection = "HealthcareName";
        this.ProviderSelection = "ProviderName";
        this.GridCodeColumnVisibility();
        //grdCode.Columns[7].Visible = true;
        //grdCode.Columns[8].Visible = false;
    }
    protected void btnProviderName_Click(object sender, EventArgs e)
    {
        this.HealthcareSelection = "HealthcareID";
        this.ProviderSelection = "ProviderID";
        this.GridCodeColumnVisibility();
        //grdCode.Columns[7].Visible = false;
        //grdCode.Columns[8].Visible = true;
    }
}
