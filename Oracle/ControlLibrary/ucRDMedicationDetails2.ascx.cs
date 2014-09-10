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

public partial class Oracle_ControlLibrary_ucRDMedicationDetails2 : System.Web.UI.UserControl
{
    #region Variable(s)
    private int _maxLength = 20;
    private long patientKey;
    private string ndcCode; 
    #endregion

    #region Property
    public long PatientKey
    {
        get { return patientKey; }
        set { patientKey = value; }
    }
    public string NDCCode
    {
        get { return ndcCode; }
        set { ndcCode = value; }
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
            if (!IsPostBack)
            {
                RadGrid1.Columns.FindByUniqueName("ProviderID").Visible = this.ProviderSelection == "ProviderID" ? true : false;
                RadGrid1.Columns.FindByUniqueName("ProviderName").Visible = this.ProviderSelection == "ProviderID" ? false : true;

                RadGrid1.Columns.FindByUniqueName("IC").Visible = this.HealthcareSelection == "HealthcareID" ? true : false;
                RadGrid1.Columns.FindByUniqueName("InstitutionName").Visible = this.HealthcareSelection == "HealthcareID" ? false : true;
            }
        }
    } 
    #endregion

    #region Method(s)
    public void RefreshPage(string selectedColor)
    {
        SelectedColor = selectedColor;
        RadGrid1.Rebind();
    }
    private void GridRowVisibility(bool isIDVisible)
    {
        if (!isIDVisible)
        {
            this.ProviderSelection = "ProviderName";
            RadGrid1.Columns.FindByUniqueName("ProviderID").Visible = false;
            RadGrid1.Columns.FindByUniqueName("ProviderName").Visible = true;

            this.HealthcareSelection = "HealthcareName";
            RadGrid1.Columns.FindByUniqueName("InstitutionName").Visible = true;
            RadGrid1.Columns.FindByUniqueName("IC").Visible = false;
        }
        else
        {
            this.ProviderSelection = "ProviderID";
            RadGrid1.Columns.FindByUniqueName("ProviderName").Visible = false;
            RadGrid1.Columns.FindByUniqueName("ProviderID").Visible = true;

            this.HealthcareSelection = "HealthcareID";
            RadGrid1.Columns.FindByUniqueName("InstitutionName").Visible = false;
            RadGrid1.Columns.FindByUniqueName("IC").Visible = true;
        }
    }
    #endregion

    #region Event(s)
    protected void RadGrid1_ItemCreated(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            HyperLink editLink = e.Item.FindControl("Provider") as HyperLink;
            HyperLink providerNameLink = e.Item.FindControl("hlProviderName") as HyperLink;

            HyperLink institutionLink = e.Item.FindControl("Institution") as HyperLink;
            HyperLink institutionNameLink = e.Item.FindControl("hlHealthcare") as HyperLink;

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
                }
                string codeStr = string.Format("HasCode=true&Code={0}&Version={1}&CodeDate={2}&PatientKey={3}",
                        drv["NDCCode"], drv["CodeVersion"],drv["DateOfService"],patientKey);

                editLink.Attributes["onclick"] = string.Format("return ShowProviderInfo('{0}','{1}','{2}');", new object[] { drv["ProviderID"], "S", codeStr });
                providerNameLink.Attributes["onclick"] = string.Format("return ShowProviderInfo('{0}','{1}','{2}');", drv["ProviderID"], "S", codeStr);

                string codeStrSingle = string.Format("HasCodeSingle=true&CodeMEDNDC={0}&Version={1}&DoctorID={2}&CodeDate={3}", 
                    drv["NDCCode"],drv["CodeVersion"],drv["ProviderID"],drv["DateOfService"]);

                institutionLink.Attributes["onclick"] = string.Format("return ShowInstitutionDetails('{0}','{1}','{2}');", drv["InstitutionCode"].ToString(), patientKey, codeStrSingle);
                institutionNameLink.Attributes["onclick"] = string.Format("return ShowInstitutionDetails('{0}','{1}','{2}');", drv["InstitutionCode"].ToString(), patientKey, codeStrSingle);

                string codeDate = Convert.ToDateTime(drv["DateOfService"]).ToString("MM-dd-yyyy");

                if (!PatientManager.DateRangeApplied)
                    e.Item.BackColor = PatientManager.GetRowColorBasedOnDate(codeDate);

                if (SelectedColor != null)
                {
                    string colorHex = System.Drawing.ColorTranslator.ToHtml(e.Item.BackColor);
                    if (SelectedColor.IndexOf(colorHex) < 0) e.Item.BackColor = System.Drawing.Color.Transparent;
                }
            }
        }
    }
    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        string gridSortString = RadGrid1.MasterTableView.SortExpressions.GetSortString();
        DataSet dsMPIInfo = PatientManager.GetRDMedicationDetail(PatientKey, NDCCode);

        DataTable dtMPIInfo = dsMPIInfo.Tables[0].Copy();
        DataView dvwMPIInfo = dtMPIInfo.DefaultView;
        dvwMPIInfo.RowFilter = string.Empty;
        DataTable dtMPIInfoDistinct = dvwMPIInfo.ToTable();

        //Apply Sorting
        DataView dtView = new DataView(dtMPIInfoDistinct);
        dtView.Sort = gridSortString;
        RadGrid1.MasterTableView.DataSource = dtView.ToTable();
    }
    protected void RadGrid1_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridSortExpression sortExpr = new GridSortExpression();
        sortExpr.FieldName = e.SortExpression;
        sortExpr.SortOrder = e.OldSortOrder;

        e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);

        RadGrid1.Columns.FindByUniqueName("InstitutionName").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";
        RadGrid1.Columns.FindByUniqueName("IC").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";
        RadGrid1.Columns.FindByUniqueName("ProviderName").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";
        RadGrid1.Columns.FindByUniqueName("ProviderID").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";

        switch (e.SortExpression)
        {
            case "InstitutionName":
                if (e.NewSortOrder == GridSortOrder.None)
                    RadGrid1.Columns.FindByUniqueName("InstitutionName").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";
                else
                    RadGrid1.Columns.FindByUniqueName("InstitutionName").HeaderImageUrl = "";
                break;
            case "InstitutionCode":
                if (e.NewSortOrder == GridSortOrder.None)
                    RadGrid1.Columns.FindByUniqueName("IC").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";
                else
                    RadGrid1.Columns.FindByUniqueName("IC").HeaderImageUrl = "";
                break;
            case "FirstName":
                if (e.NewSortOrder == GridSortOrder.None)
                    RadGrid1.Columns.FindByUniqueName("ProviderName").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";
                else
                    RadGrid1.Columns.FindByUniqueName("ProviderName").HeaderImageUrl = "";
                break;
            case "ProviderID":
                if (e.NewSortOrder == GridSortOrder.None)
                    RadGrid1.Columns.FindByUniqueName("ProviderID").HeaderImageUrl = "~/Oracle/images/Unsortedlist.png";
                else
                    RadGrid1.Columns.FindByUniqueName("ProviderID").HeaderImageUrl = "";
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
