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

public partial class Oracle_ControlLibrary_ucRDMedicationDetails : System.Web.UI.UserControl
{

    private long patientKey;
    private string ndcCode;

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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
            //{
            //    id = Request.QueryString["ID"].ToString();
            //    modifierID = Request.QueryString["ModifierID"].ToString();
            //}
            //RadGrid1.MasterTableView.DataSource = PatientManager.GetRDMedicationDetail(id, modifierID);
            //RadGrid1.DataBind();

            grdMedicationDetails.DataSource = PatientManager.GetRDMedicationDetail(patientKey, ndcCode);
            grdMedicationDetails.DataBind();
        }
    }
    public void RefreshPage(string selectedColor)
    {
        SelectedColor = selectedColor;

        grdMedicationDetails.DataSource = PatientManager.GetRDMedicationDetail(patientKey, ndcCode);
        grdMedicationDetails.DataBind();
    }
    //protected void RadGrid1_ItemCreated(object sender, Telerik.WebControls.GridItemEventArgs e)
    //{
    //    if (e.Item is GridDataItem)
    //    {
    //        HyperLink editLink = e.Item.FindControl("Provider") as HyperLink;
    //        if (editLink != null)
    //        {
    //            editLink.Attributes["href"] = "#";
    //        }
    //        DataRowView drv;
    //        if (e.Item.DataItem != null)
    //        {
    //            drv = e.Item.DataItem as DataRowView;
    //            if (drv != null)
    //            {
    //                editLink.Text = drv["ProviderID"].ToString();   // e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["DETAIL"].ToString();
    //            }
    //            editLink.Attributes["onclick"] = string.Format("return ShowProviderInfo('{0}','{1}');",
    //                                                                        new object[] { drv["ProviderID"], "S" });
    //            editLink.Attributes["onMouseOver"] = string.Format("return HideStatus()");
    //        }
    //    }

    //}
    //protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    //{
    //    RadGrid1.MasterTableView.DataSource = PatientManager.GetRDMedicationDetail(id, modifierID);
    //}

    protected void grdMedicationDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        int i = e.Row.DataItemIndex;
        if (i > -1)
        {
            HyperLink editLink = e.Row.FindControl("Provider") as HyperLink;
            HyperLink institutionLink = e.Row.FindControl("Institution") as HyperLink;
            if (editLink != null)
            {
                editLink.Attributes["href"] = "#";
                DataRowView rowView = e.Row.DataItem as DataRowView;
                if (rowView != null)
                {
                    string dateOfService = DateTime.Parse(rowView.Row["DateOfService"].ToString()).ToString("MM-dd-yyyy"); ;
                    if (!PatientManager.DateRangeApplied)
                        e.Row.BackColor = PatientManager.GetRowColorBasedOnDate(dateOfService);
                    if (SelectedColor != null)
                    {
                        string colorHex = System.Drawing.ColorTranslator.ToHtml(e.Row.BackColor);
                        if (SelectedColor.IndexOf(colorHex) < 0) e.Row.BackColor = System.Drawing.Color.Transparent;
                    }

                    editLink.Text = rowView.Row["ProviderID"].ToString();
                    editLink.Attributes["title"] = rowView.Row["FirstName"].ToString() + " " + rowView.Row["LastName"].ToString() + ", " + rowView.Row["Degree"].ToString();
                    editLink.Attributes["class"] = "tooltip";

                    string codeStr = "HasCode=true";
                    codeStr += "&Code=" + rowView.Row["NDCCode"].ToString();
                    codeStr += "&Type=NDC";
                    codeStr += "&Version=" + rowView.Row["CodeVersion"].ToString();
                    codeStr += "&CodeDate=" + dateOfService;
                    codeStr += "&PatientKey=" + patientKey;

                    editLink.Attributes["onclick"] = string.Format("return ShowProviderInfo('{0}','{1}','{2}');",
                                                                            new object[] { rowView.Row["ProviderID"], "S", codeStr });
                }
                editLink.Attributes["onMouseOver"] = string.Format("return HideStatus()");
            }
            if (institutionLink != null)
            {
                institutionLink.Attributes["href"] = "#";
                DataRowView rowView = e.Row.DataItem as DataRowView;
                if (rowView != null)
                {
                    string dateOfService = DateTime.Parse(rowView.Row["DateOfService"].ToString()).ToString("MM-dd-yyyy"); ;
                    string codeStrSingle = "HasCodeSingle=true";
                    codeStrSingle += "&CodeMEDNDC=" + rowView.Row["NDCCode"].ToString();
                    codeStrSingle += "&Type=NDC";
                    codeStrSingle += "&Version=" + rowView.Row["CodeVersion"].ToString();
                    codeStrSingle += "&DoctorID=" + rowView.Row["ProviderID"].ToString();
                    codeStrSingle += "&CodeDate=" + dateOfService;
                    institutionLink.Text = rowView.Row["InstitutionCode"].ToString();
                    institutionLink.Attributes["title"] = rowView.Row["InstitutionName"].ToString();
                    institutionLink.Attributes["class"] = "tooltip";
                    institutionLink.Attributes["onclick"] = string.Format("return ShowInstitutionDetails('{0}','{1}','{2}');", rowView["InstitutionCode"].ToString(), patientKey, codeStrSingle);
                }
            }
        }
    }
    protected void grdMedicationDetails_Sorting(object sender, GridViewSortEventArgs e)
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


        DataSet dsRD = PatientManager.GetRDMedicationDetail(patientKey, ndcCode);
        //Create the Source Table... Default to original dsRD table
        DataTable dtRD = dsRD.Tables[0].Copy();
        DataView dtView = new DataView(dtRD);
        dtView.Sort = sortExpression + " " + sortDir;
        grdMedicationDetails.DataSource = dtView.ToTable();
        grdMedicationDetails.DataBind();

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
