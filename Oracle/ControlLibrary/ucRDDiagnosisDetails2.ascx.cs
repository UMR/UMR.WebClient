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

public partial class Oracle_ControlLibrary_ucRDDiagnosisDetails2 : System.Web.UI.UserControl
{
    #region Variable(s)
    private int _maxLength = 20;
    private long patientKey;
    private string medCode; 
    #endregion

    #region Public Properties
    public string MedCode
    {
        get { return medCode; }
        set { medCode = value; }
    }
    public long PatientKey
    {
        get { return patientKey; }
        set { patientKey = value; }
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
    #endregion

    #region Page Event(s)
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RadGridMPI.Columns.FindByUniqueName("ProviderID").Visible = this.ProviderSelection == "ProviderID" ? true : false;
            RadGridMPI.Columns.FindByUniqueName("ProviderName").Visible = this.ProviderSelection == "ProviderID" ? false : true;

            RadGridMPI.Columns.FindByUniqueName("IC").Visible = this.HealthcareSelection == "HealthcareID" ? true : false;
            RadGridMPI.Columns.FindByUniqueName("InstitutionName").Visible = this.HealthcareSelection == "HealthcareID" ? false : true;
        }
    } 
    #endregion

    #region Method(s)
    public void RefreshPage(string selectedColor)
    {
        this.SelectedColor = selectedColor;
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
    #endregion

    #region Event(s)
    protected void RadGridMPI_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            HyperLink editLink = e.Item.FindControl("Provider") as HyperLink;
            HyperLink providerNameLink = e.Item.FindControl("hlProviderName") as HyperLink;

            HyperLink institutionLink = e.Item.FindControl("Institution") as HyperLink;
            HyperLink institutionNameLink = e.Item.FindControl("hlHealthcare") as HyperLink;

            //HyperLink medicalContentIndexLink = e.Item.FindControl("lnkMedicalContentIndex") as HyperLink;

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
                    string providersName = drv["FIRSTNAME"].ToString() + " " + drv["LASTNAME"].ToString();


                    if (drv["DEGREE"] != null && !string.IsNullOrEmpty(drv["DEGREE"].ToString()))
                    {
                        providersName += ", " + drv["DEGREE"].ToString();
                    }
                    providerNameLink.Text = providersName.Trim().Length > _maxLength ? providersName.Substring(0, _maxLength).PadRight(_maxLength + 3, '.') : providersName;

                    editLink.Attributes["title"] = providersName;
                    editLink.Attributes["class"] = "tooltip";

                    providerNameLink.Attributes["title"] = providersName;
                    providerNameLink.Attributes["class"] = "tooltip";

                    institutionLink.Text = drv["InstitutionCode"].ToString();
                    string institutionmae = drv["InstitutionName"].ToString();
                    institutionNameLink.Text = institutionmae.Trim().Length > _maxLength ? institutionmae.Substring(0, _maxLength).PadRight(_maxLength + 3, '.') : institutionmae;

                    institutionLink.Attributes["title"] = institutionmae;
                    institutionLink.Attributes["class"] = "tooltip";

                    institutionNameLink.Attributes["title"] = institutionmae;
                    institutionNameLink.Attributes["class"] = "tooltip";


                    //medicalContentIndexLink.Text = drv["MedicalContentIndex"].ToString();
                    //string medCodeDesciption = drv["CodeDesc"].ToString().Replace("__", ", ");
                    //medCodeDesciption = medCodeDesciption.Replace("'", "\\'");
                    //medCodeDesciption = medCodeDesciption.Replace("\0", "");
                    //medicalContentIndexLink.Attributes["title"] = medCodeDesciption;
                    //medicalContentIndexLink.Attributes["class"] = "tooltip";
                }
                string codeStr = string.Format("HasCode=true&Code={0}&Type={1}&Version={2}&CodeDate={3}&PatientKey={4}",
                    drv["Code"], drv["CodeType"], drv["CodeVersion"], drv["DateOfService"], patientKey);
                
                editLink.Attributes["onclick"] = string.Format("return ShowProviderInfo('{0}','{1}','{2}');", new object[] { drv["ProviderID"], "S", codeStr });
                providerNameLink.Attributes["onclick"] = string.Format("return ShowProviderInfo('{0}','{1}','{2}');", drv["ProviderID"], "S", codeStr);

                string codeStrSingle = string.Format("HasCodeSingle=true&CodeMEDNDC={0}&Type={1}&Version={2}&DoctorID={3}&CodeDate={4}",
                    drv["Code"],drv["CodeType"],drv["CodeVersion"], drv["ProviderID"],drv["DateOfService"]);

                institutionLink.Attributes["onclick"] = string.Format("return ShowInstitutionDetails('{0}','{1}','{2}');", drv["InstitutionCode"].ToString(), patientKey, codeStrSingle);
                institutionNameLink.Attributes["onclick"] = string.Format("return ShowInstitutionDetails('{0}','{1}','{2}');", drv["InstitutionCode"].ToString(), patientKey, codeStrSingle);

                string codeDate = drv["DateOfService"].ToString();

                if (!PatientManager.DateRangeApplied)
                    e.Item.BackColor = PatientManager.GetRowColorBasedOnDate(codeDate);

                if (SelectedColor != null)
                {
                    string colorHex = System.Drawing.ColorTranslator.ToHtml(e.Item.BackColor);
                    if (SelectedColor.IndexOf(colorHex) < 0) e.Item.BackColor = System.Drawing.Color.Transparent;
                }
            }

            //.......
            Literal ltCodeModifier = e.Item.FindControl("ltCodeModifier") as Literal;
            if (ltCodeModifier != null)
            {
                drv = e.Item.DataItem as DataRowView;
                if (drv != null)
                {
                    string codeType = drv["CodeType"].ToString().Trim();
                    string medCode = drv["Code"].ToString().Trim();
                    int codeVersion = Convert.ToInt32(drv["CodeVersion"].ToString().Trim());

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
    protected void RadGridMPI_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        string gridSortString = this.RadGridMPI.MasterTableView.SortExpressions.GetSortString();

        DataSet dsMPIInfo = PatientManager.GetRDDiagnosisDetail(PatientKey, MedCode);

        DataTable dtMPIInfo = dsMPIInfo.Tables[0].Copy();
        DataView dvwMPIInfo = dtMPIInfo.DefaultView;
        dvwMPIInfo.RowFilter = string.Empty;
        DataTable dtMPIInfoDistinct = dvwMPIInfo.ToTable();

        //Apply Sorting
        DataView dtView = new DataView(dtMPIInfoDistinct);
        dtView.Sort = gridSortString;
        RadGridMPI.MasterTableView.DataSource = dtView.ToTable();
    }
    protected void RadGridMPI_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridSortExpression sortExpr = new GridSortExpression();
        sortExpr.FieldName = e.SortExpression;
        sortExpr.SortOrder = e.OldSortOrder;

        e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);

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
            case "InstitutionCode":
                if (e.NewSortOrder == GridSortOrder.None)
                    RadGridMPI.Columns.FindByUniqueName("IC").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";
                else
                    RadGridMPI.Columns.FindByUniqueName("IC").HeaderImageUrl = "";
                break;
            case "FirstName":
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
    #endregion
}
