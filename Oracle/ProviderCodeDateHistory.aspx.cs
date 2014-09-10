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
using System.Drawing;
using System.Text;

public partial class Oracle_ProviderCodeDateHistory : System.Web.UI.Page
{
    private int _maxLength = 20;
    private string strFilter = "";
    public string CodeDate
    {
        get
        {
            if (ViewState["CODEDATE"] != null)
                return ViewState["CODEDATE"].ToString();
            else return null;
        }
        set
        {
            ViewState["CODEDATE"] = value;
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
        if (!Page.IsPostBack)
        {
            //Need to test carefully : Animesh
            long patientKey = Int64.Parse(Request.QueryString["PatientKey"].Trim());
            string DoctorID = Request.QueryString["DoctorID"].Trim();

            GridView1.DataSource = PatientManager.GetProviderCodeDateHistory(DoctorID, patientKey);
            GridView1.DataBind();

            RadGridMPI.Columns.FindByUniqueName("ProviderID").Visible = this.ProviderSelection == "ProviderID" ? true : false;
            RadGridMPI.Columns.FindByUniqueName("ProviderName").Visible = this.ProviderSelection == "ProviderID" ? false : true;

            RadGridMPI.Columns.FindByUniqueName("IC").Visible = this.HealthcareSelection == "HealthcareID" ? true : false;
            RadGridMPI.Columns.FindByUniqueName("InstitutionName").Visible = this.HealthcareSelection == "HealthcareID" ? false : true;
        }

        UcLegendCompact1.FilterApplied += new LegendCompactEventHandler(RefreshPage);
    }
    protected void RefreshPage(object sender, string selectedColor)
    {
        SelectedColor = selectedColor;
        long patientKey = Int64.Parse(Request.QueryString["PatientKey"].Trim());
        string DoctorID = Request.QueryString["DoctorID"].Trim();

        GridView1.DataSource = PatientManager.GetProviderCodeDateHistory(DoctorID, patientKey);
        GridView1.DataBind();

        RadGridMPI.Rebind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = "__doPostBack('GridView1','Select$" + e.Row.RowIndex + "');";

            DataRowView rowView = e.Row.DataItem as DataRowView;
            string codeDate = rowView.Row["CODEDATE"].ToString();
            if (!PatientManager.DateRangeApplied)
                e.Row.BackColor = PatientManager.GetRowColorBasedOnDate(DateTime.Parse(codeDate).ToString("MM-dd-yyyy"));
            if (SelectedColor != null)
            {
                string colorHex = System.Drawing.ColorTranslator.ToHtml(e.Row.BackColor);
                if (SelectedColor.IndexOf(colorHex) < 0) e.Row.BackColor = System.Drawing.Color.Transparent;
            }

        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            GridView gv = (GridView)sender;
            int index = int.Parse(e.CommandArgument.ToString());
            string codedate = gv.DataKeys[index].Value.ToString();

            CodeDate = codedate;

            DateTime dt = DateTime.Parse(codedate);

            long patientKey = Int64.Parse(Request.QueryString["PatientKey"].Trim());
            string DoctorID = Request.QueryString["DoctorID"].Trim();

            //Now get the details
            RadGridMPI.MasterTableView.DataSource = PatientManager.GetMPIInfoSpecific(patientKey, DoctorID, dt.ToString("dd-MMM-yy hh.mm.ss tt"));
            RadGridMPI.DataBind();
            filterDiv.Visible = true;
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        long patientKey = Int64.Parse(Request.QueryString["PatientKey"].Trim());
        string DoctorID = Request.QueryString["DoctorID"].Trim();
        DataTable dt = PatientManager.GetProviderCodeDateHistory(DoctorID, patientKey);

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

        DataView dtView = new DataView(dt);
        dtView.Sort = sortExpression + " " + sortDir;
        GridView1.DataSource = dtView.ToTable();
        GridView1.DataBind();
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

    protected void RadGridMPI_ItemCreated(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            HyperLink editLink = e.Item.FindControl("Provider") as HyperLink;
            HyperLink providerNameLink = e.Item.FindControl("hlProviderName") as HyperLink;
            HyperLink institutionLink = e.Item.FindControl("Institution") as HyperLink;
            HyperLink institutionNameLink = e.Item.FindControl("hlHealthcare") as HyperLink;

            HyperLink medicalContentIndexLink = e.Item.FindControl("lnkMedicalContentIndex") as HyperLink;

            if (editLink != null)
            {
                editLink.Attributes["href"] = "#";
            }
            if (providerNameLink != null)
            {
                providerNameLink.Attributes["href"] = "#";
            }
            if (institutionLink != null)
            {
                institutionLink.Attributes["href"] = "#";
            }
            if (institutionNameLink != null)
            {
                institutionNameLink.Attributes["href"] = "#";
            }

            DataRowView drv;
            if (e.Item.DataItem != null)
            {
                drv = e.Item.DataItem as DataRowView;
                if (drv != null)
                {
                    editLink.Text = drv["ProviderID"].ToString();   // e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["DETAIL"].ToString();
                    string providersName = drv["DoctorsFirstName"].ToString() + " " + drv["DoctorsLastName"].ToString();


                    if (drv["DEGREE"] != null && !string.IsNullOrEmpty(drv["DEGREE"].ToString()))
                    {
                        providersName += ", " + drv["DEGREE"].ToString();
                    }
                    providerNameLink.Text = providersName.Trim().Length > _maxLength ? providersName.Substring(0, _maxLength).PadRight(_maxLength + 3, '.') : providersName;

                    editLink.Attributes["title"] = providersName;
                    editLink.Attributes["class"] = "tooltip";

                    providerNameLink.Attributes["title"] = providersName;
                    providerNameLink.Attributes["class"] = "tooltip";

                    institutionLink.Text = drv["InstituteCode"].ToString();
                    string institutionmae = drv["InstitutionName"].ToString();
                    institutionNameLink.Text = institutionmae.Trim().Length > _maxLength ? institutionmae.Substring(0, _maxLength).PadRight(_maxLength + 3, '.') : institutionmae;

                    institutionLink.Attributes["title"] = institutionmae;
                    institutionLink.Attributes["class"] = "tooltip";
                    institutionNameLink.Attributes["title"] = institutionmae;
                    institutionNameLink.Attributes["class"] = "tooltip";

                    medicalContentIndexLink.Text = drv["MedicalContentIndex"].ToString();
                    string medCodeDesciption = drv["CodeDesc"].ToString().Replace("__", ", ");
                    medCodeDesciption = medCodeDesciption.Replace("'", "\\'");
                    medCodeDesciption = medCodeDesciption.Replace("\0", "");


                    string codeDesciptionHistory = drv["CodeDescriptionHistory"].ToString().Replace("__", ", ");
                    codeDesciptionHistory = codeDesciptionHistory.Replace("'", "\\'");
                    codeDesciptionHistory = codeDesciptionHistory.Replace("\0", "");
                    codeDesciptionHistory = "<div style=\"color:green;\">" + codeDesciptionHistory + "</div>";

                    medicalContentIndexLink.Attributes["title"] = medCodeDesciption + codeDesciptionHistory;
                    medicalContentIndexLink.Attributes["class"] = "tooltip";
                }
                long patientKey = Int64.Parse(Request.QueryString["PatientKey"].Trim());

                string codeStr = string.Format("HasCode=true&Code={0}&Type={1}&Version={2}&CodeDate={3} {4}&PatientKey={5}", 
                                drv["Code"], drv["Type"], drv["Version"], drv["DateOfService"], drv["ServiceTime"], patientKey);
                editLink.Attributes["onclick"] = string.Format("return ShowProviderDetails('{0}','{1}','{2}');", new object[] { drv["ProviderID"], "S", codeStr });
                providerNameLink.Attributes["onclick"] = string.Format("return ShowProviderDetails('{0}','{1}','{2}');", new object[] { drv["ProviderID"], "S", codeStr });

                string codeStrSingle = string.Format("HasCodeSingle=true&CodeMEDNDC={0}&Type={1}&Version={2}&DoctorID={3}&CodeDate={4} {5}", 
                        drv["Code"], drv["Type"],drv["Version"],drv["ProviderID"], drv["DateOfService"],drv["ServiceTime"]);
                institutionLink.Attributes["onclick"] = string.Format("return ShowInstitutionDetails('{0}','{1}','{2}');", drv["InstituteCode"].ToString(), patientKey, codeStrSingle);
                institutionNameLink.Attributes["onclick"] = string.Format("return ShowInstitutionDetails('{0}','{1}','{2}');", drv["InstituteCode"].ToString(), patientKey, codeStrSingle);

                string codeDate = drv["DateOfService"].ToString();
                if (!PatientManager.DateRangeApplied)
                    e.Item.BackColor = PatientManager.GetRowColorBasedOnDate(codeDate);
                if (SelectedColor != null)
                {
                    string colorHex = System.Drawing.ColorTranslator.ToHtml(e.Item.BackColor);
                    if (SelectedColor.IndexOf(colorHex) < 0) e.Item.BackColor = System.Drawing.Color.Transparent;
                }

            }

            Literal ltCodeModifier = e.Item.FindControl("ltCodeModifier") as Literal;
            if (ltCodeModifier != null)
            {
                drv = e.Item.DataItem as DataRowView;
                if (drv != null)
                {
                    string codeType = drv["Type"].ToString().Trim();
                    string medCode = drv["Code"].ToString().Trim();
                    int codeVersion = Convert.ToInt32(drv["Version"].ToString().Trim());

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
    protected void RadGridMPI_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridSortExpression sortExpr = new GridSortExpression();
        sortExpr.FieldName = e.SortExpression;
        sortExpr.SortOrder = e.OldSortOrder;
        e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
        string gridSortString = this.RadGridMPI.MasterTableView.SortExpressions.GetSortString();

        DateTime dt = DateTime.Parse(CodeDate);

        long patientKey = Int64.Parse(Request.QueryString["PatientKey"].Trim());
        string DoctorID = Request.QueryString["DoctorID"].Trim();

        //Aply filter
        DataSet ds = PatientManager.GetMPIInfoSpecific(patientKey, DoctorID, dt.ToString("dd-MMM-yy hh.mm.ss tt"));
        string strFilterOriginal = "Type=";
        ArrayList strArrCode = new ArrayList();
        foreach (ListItem chkCodeItem in cblCodeOption.Items)
        {
            if (chkCodeItem.Selected)
            {
                strArrCode.Add(chkCodeItem.Value);
            }
        }
        for (int j = 0; j < strArrCode.Count; j++)
        {
            if (j == 0)
                strFilter = strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
            else
                strFilter += " OR " + strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
        }
        DataTable dtMPIInfo = ds.Tables[0].Copy();
        DataView dvwMPIInfo = dtMPIInfo.DefaultView;
        dvwMPIInfo.RowFilter = strFilter;
        DataTable dtMPIInfoDistinct = dvwMPIInfo.ToTable();

        // apply sorting
        DataView dtView = new DataView(dtMPIInfoDistinct);
        dtView.Sort = gridSortString;
        RadGridMPI.MasterTableView.DataSource = dtView.ToTable();
        RadGridMPI.DataBind();


        RadGridMPI.Columns.FindByUniqueName("InstitutionName").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";
        RadGridMPI.Columns.FindByUniqueName("IC").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";
        RadGridMPI.Columns.FindByUniqueName("ProviderName").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";
        RadGridMPI.Columns.FindByUniqueName("ProviderID").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";


        switch (e.SortExpression)
        {
            case "InstitutionName":
                if (e.NewSortOrder == GridSortOrder.None)
                    RadGridMPI.Columns.FindByUniqueName("InstitutionName").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";
                else
                    RadGridMPI.Columns.FindByUniqueName("InstitutionName").HeaderImageUrl = "";
                break;
            case "InstituteCode":
                if (e.NewSortOrder == GridSortOrder.None)
                    RadGridMPI.Columns.FindByUniqueName("IC").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";
                else
                    RadGridMPI.Columns.FindByUniqueName("IC").HeaderImageUrl = "";
                break;
            case "DoctorsFirstName":
                if (e.NewSortOrder == GridSortOrder.None)
                    RadGridMPI.Columns.FindByUniqueName("ProviderName").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";
                else
                    RadGridMPI.Columns.FindByUniqueName("ProviderName").HeaderImageUrl = "";
                break;
            case "ProviderID":
                if (e.NewSortOrder == GridSortOrder.None)
                    RadGridMPI.Columns.FindByUniqueName("ProviderID").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";
                else
                    RadGridMPI.Columns.FindByUniqueName("ProviderID").HeaderImageUrl = "";
                break;
        }
    }

    protected void btnApplyCodeFilter_Click(object sender, EventArgs e)
    {
        DateTime dt = DateTime.Parse(CodeDate);

        long patientKey = Int64.Parse(Request.QueryString["PatientKey"].Trim());
        string DoctorID = Request.QueryString["DoctorID"].Trim();

        DataSet dsMPIInfo = PatientManager.GetMPIInfoSpecific(patientKey, DoctorID, dt.ToString("dd-MMM-yy hh.mm.ss tt"));

        string strFilterOriginal = "Type=";
        ArrayList strArrCode = new ArrayList();
        foreach (ListItem chkCodeItem in cblCodeOption.Items)
        {
            if (chkCodeItem.Selected)
            {
                strArrCode.Add(chkCodeItem.Value);
            }
        }
        for (int j = 0; j < strArrCode.Count; j++)
        {
            if (j == 0)
                strFilter = strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
            else
                strFilter += " OR " + strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
        }
        DataTable dtMPIInfo = dsMPIInfo.Tables[0].Copy();
        DataView dvwMPIInfo = dtMPIInfo.DefaultView;
        dvwMPIInfo.RowFilter = strFilter;
        DataTable dtMPIInfoDistinct = dvwMPIInfo.ToTable();
        RadGridMPI.MasterTableView.DataSource = dtMPIInfoDistinct;
        RadGridMPI.Rebind();
    }

    private void GridRowVisibility(bool isIDVisible)
    {
        if (!isIDVisible)
        {
            this.ProviderSelection = "ProviderName";
            RadGridMPI.Columns.FindByUniqueName("ProviderID").Visible = false;
            RadGridMPI.Columns.FindByUniqueName("ProviderName").Visible = true;

            this.HealthcareSelection = "HealthcareName";
            RadGridMPI.Columns.FindByUniqueName("InstitutionName").Visible = true;
            RadGridMPI.Columns.FindByUniqueName("IC").Visible = false;
        }
        else
        {
            this.ProviderSelection = "ProviderID";
            RadGridMPI.Columns.FindByUniqueName("ProviderName").Visible = false;
            RadGridMPI.Columns.FindByUniqueName("ProviderID").Visible = true;

            this.HealthcareSelection = "HealthcareID";
            RadGridMPI.Columns.FindByUniqueName("InstitutionName").Visible = false;
            RadGridMPI.Columns.FindByUniqueName("IC").Visible = true;
        }
    }
    protected void btnProviderID_Click(object sender, EventArgs e)
    {
        GridRowVisibility(false);
    }
    protected void btnProviderName_Click(object sender, EventArgs e)
    {
        GridRowVisibility(true);
    }

    protected void btnInstitutionID_Click(object sender, EventArgs e)
    {
        GridRowVisibility(false);
    }
    protected void btnInstitutionName_Click(object sender, EventArgs e)
    {
        GridRowVisibility(true);
    }
    
}
