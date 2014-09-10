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

public partial class Oracle_ControlLibrary_ucRDDiagnosisDetails : System.Web.UI.UserControl
{
    private long patientKey;
    private string medCode; //, codeVersion, disCode, disType, codeType, ;

    #region Public Properties
    //public string DisCode
    //{
    //    get { return disCode; }
    //    set { disCode = value; }
    //}
    //public string CodeVersion
    //{
    //    get { return codeVersion; }
    //    set { codeVersion = value; }
    //}
    //public string CodeType
    //{
    //    get { return codeType; }
    //    set { codeType = value; }
    //}
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
    #endregion
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

            //RadGrid1.MasterTableView.DataSource = PatientManager.GetRDDiagnosisDetail(id, modifierID, codeType,
            //                                                                        medCode, Convert.ToDecimal(codeVersion));
            //RadGrid1.DataBind();

            grdDiagnosisDetails.DataSource = PatientManager.GetRDDiagnosisDetail(patientKey, medCode);
            grdDiagnosisDetails.DataBind();
        }
    }
    public void RefreshPage(string selectedColor)
    {
        SelectedColor = selectedColor;

        grdDiagnosisDetails.DataSource = PatientManager.GetRDDiagnosisDetail(patientKey, medCode);
        grdDiagnosisDetails.DataBind();
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
    //            editLink.Attributes["onclick"] = string.Format("return ShowProviderInfo('{0}', '{1}');",
    //                                                                        new object[] { drv["ProviderID"],"S" });
    //            editLink.Attributes["onMouseOver"] = string.Format("return HideStatus()");
    //        }
    //    }

    //}
    //protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    //{
    //    //RadGrid1.MasterTableView.DataSource = PatientManager.GetRDDiagnosisDetail(id, modifierID, codeType,
    //    //                                                                            medCode, Convert.ToDecimal(codeVersion));
    //    RadGrid1.MasterTableView.DataSource = PatientManager.GetRDDiagnosisDetail(id, modifierID, medCode);
    //}

    protected void grdDiagnosisDetails_RowCreated(object sender, GridViewRowEventArgs e)
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
                    string dateOfService = rowView.Row["DateOfService"].ToString();

                    if (!PatientManager.DateRangeApplied)
                        e.Row.BackColor = PatientManager.GetRowColorBasedOnDate(dateOfService);
                    if (SelectedColor != null)
                    {
                        string colorHex = System.Drawing.ColorTranslator.ToHtml(e.Row.BackColor);
                        if (SelectedColor.IndexOf(colorHex) < 0) e.Row.BackColor = System.Drawing.Color.Transparent;
                    }

                    editLink.Text = rowView.Row["ProviderID"].ToString();
                    string providersName = rowView.Row["FirstName"].ToString() + " " + rowView.Row["LastName"].ToString();
                    if (rowView.Row["Degree"] != null && !string.IsNullOrEmpty(rowView.Row["Degree"].ToString()))
                    {
                        providersName += ", " + rowView.Row["Degree"].ToString();
                    }
                    editLink.Attributes["title"] =providersName;
                    editLink.Attributes["class"] = "tooltip";

                    string codeStr = "HasCode=true";
                    codeStr += "&Code=" + rowView.Row["Code"].ToString();
                    codeStr += "&Type=" + rowView.Row["CodeType"].ToString();
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
                    string dateOfService = rowView.Row["DateOfService"].ToString();
                    string codeStrSingle = "HasCodeSingle=true";
                    codeStrSingle += "&CodeMEDNDC=" + rowView.Row["Code"].ToString();
                    codeStrSingle += "&Type=" + rowView.Row["CodeType"].ToString();
                    codeStrSingle += "&Version=" + rowView.Row["CodeVersion"].ToString();
                    codeStrSingle += "&DoctorID=" + rowView.Row["ProviderID"].ToString();
                    codeStrSingle += "&CodeDate=" + dateOfService;

                    institutionLink.Text = rowView.Row["InstitutionCode"].ToString();
                    institutionLink.Attributes["title"] = rowView.Row["InstitutionName"].ToString();
                    institutionLink.Attributes["class"] = "tooltip";
                    institutionLink.Attributes["onclick"] = string.Format("return ShowInstitutionDetails('{0}','{1}','{2}');", rowView["InstitutionCode"].ToString(),patientKey, codeStrSingle);
                }
            }
            Literal ltCodeModifier = e.Row.FindControl("ltCodeModifier") as Literal;
            if (ltCodeModifier != null)
            {
                DataRowView rowView = e.Row.DataItem as DataRowView;
                if (rowView != null)
                {
                    string codeType = rowView.Row["CodeType"].ToString().Trim();
                    string medCode = rowView.Row["Code"].ToString().Trim();
                    int codeVersion = Convert.ToInt32(rowView.Row["CodeVersion"].ToString().Trim());

                    DataSet ds = PatientManager.GetCodeModifiersByMedcode(codeType, medCode, codeVersion);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            sb.Append("<a class=\"tooltip\" title=\"" + ds.Tables[0].Rows[j]["LongDescription"].ToString() + "\" style=\"color:#355E3B;\">" + ds.Tables[0].Rows[j]["ModifierCode"].ToString() + "</a>");
                            if (j < ds.Tables[0].Rows.Count - 1)
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
    protected void grdDiagnosisDetails_Sorting(object sender, GridViewSortEventArgs e)
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


        DataSet dsRD = PatientManager.GetRDDiagnosisDetail(patientKey, medCode);
        //Create the Source Table... Default to original dsRD table
        DataTable dtRD = dsRD.Tables[0].Copy();
        DataView dtView = new DataView(dtRD);
        dtView.Sort = sortExpression + " " + sortDir;
        grdDiagnosisDetails.DataSource = dtView.ToTable();
        grdDiagnosisDetails.DataBind();

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
