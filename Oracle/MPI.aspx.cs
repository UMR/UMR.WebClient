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

public partial class Oracle_MPI : System.Web.UI.Page
{
    private int _maxLength = 20;
    private long patientKey;
    private string strFilter = "";

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
        if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
        {
            patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
        }
        UcLegendCompact1.FilterApplied += new LegendCompactEventHandler(RefreshMPI);


        if (!IsPostBack)
        {

            if (Session["SelectedCodeIndex"] != null)
            {
                string selectedCodeIndex = Session["SelectedCodeIndex"].ToString();
                if (!selectedCodeIndex.Equals(string.Empty))
                {
                    foreach (ListItem chkCodeItem in cblCodeOption.Items)
                    {
                        if (chkCodeItem.Selected)
                        {
                            chkCodeItem.Selected = false;
                        }
                    }

                    Session["SelectedCodeIndex"] = selectedCodeIndex;
                    string[] selIndexStr = selectedCodeIndex.Split(new char[] { ',' });
                    for (int i = 0; i < selIndexStr.Length; i++)
                    {
                        int index = 0;
                        if (Int32.TryParse((selIndexStr[i]), out index))
                        {
                            cblCodeOption.Items[index].Selected = true;
                        }
                    }
                }
            }


            RadGridMPI.Columns.FindByUniqueName("ProviderID").Visible = this.ProviderSelection == "ProviderID" ? true : false;
            RadGridMPI.Columns.FindByUniqueName("ProviderName").Visible = this.ProviderSelection == "ProviderID" ? false : true;

            RadGridMPI.Columns.FindByUniqueName("IC").Visible = this.HealthcareSelection == "HealthcareID" ? true : false;
            RadGridMPI.Columns.FindByUniqueName("InstitutionName").Visible = this.HealthcareSelection == "HealthcareID" ? false : true;
        }
    }
    public void RefreshMPI(object sender, string selectedColor)
    {
        SelectedColor = selectedColor;
        RadGridMPI.Rebind();
    }
    protected void RadGridMPI_ItemCreated(object sender, GridItemEventArgs e)
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

                    medicalContentIndexLink.ToolTip= medCodeDesciption+codeDesciptionHistory;
                    medicalContentIndexLink.Attributes["class"] = "tooltip";
                }
                string codeStr = "HasCode=true";
                codeStr += "&Code=" + drv["Code"].ToString();
                codeStr += "&Type=" + drv["Type"].ToString();
                codeStr += "&Version=" + drv["Version"].ToString();
                codeStr += "&CodeDate=" + DateTime.Parse(drv["DateOfService"].ToString()+ " " + drv["ServiceTime"].ToString());
                codeStr += "&PatientKey=" + patientKey;

                editLink.Attributes["onclick"] = string.Format("return ShowProviderDetails('{0}','{1}','{2}');", new object[] { drv["ProviderID"], "S", codeStr });
                providerNameLink.Attributes["onclick"] = string.Format("return ShowProviderDetails('{0}','{1}','{2}');", drv["ProviderID"], "S", codeStr);

                string codeStrSingle = "HasCodeSingle=true";
                codeStrSingle += "&CodeMEDNDC=" + drv["Code"].ToString();
                codeStrSingle += "&Type=" + drv["Type"].ToString();
                codeStrSingle += "&Version=" + drv["Version"].ToString();
                codeStrSingle += "&DoctorID=" + drv["ProviderID"].ToString();
                codeStrSingle += "&CodeDate=" + DateTime.Parse(drv["DateOfService"].ToString() + " " + drv["ServiceTime"].ToString());

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

            //.......
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
    protected void RadGridMPI_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        //RadGridMPI.MasterTableView.DataSource = PatientManager.GetMPIInfo(id, modifierID);

        string gridSortString = this.RadGridMPI.MasterTableView.SortExpressions.GetSortString();

        //Aply filter
        DataSet dsMPIInfo = PatientManager.GetMPIInfo(patientKey);
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

        //Apply Sorting
        DataView dtView = new DataView(dtMPIInfoDistinct);
        dtView.Sort = gridSortString;
        RadGridMPI.MasterTableView.DataSource = dtView.ToTable();
    }

    //protected void grdMPI_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    int i = e.Row.DataItemIndex;
    //    if (i > -1)
    //    {
    //        HyperLink editLink = e.Row.FindControl("Provider") as HyperLink;
    //        if (editLink != null)
    //        {
    //            editLink.Attributes["href"] = "#";
    //            DataRowView rowView = e.Row.DataItem as DataRowView;
    //            if (rowView != null)
    //            {
    //                editLink.Text = rowView.Row["ProviderID"].ToString();
    //                editLink.Attributes["onclick"] = string.Format("return ShowProviderDetails('{0}','{1}');",
    //                                                                        new object[] { rowView.Row["ProviderID"], "S" });
    //            }
    //            editLink.Attributes["onMouseOver"] = string.Format("return HideStatus()");
    //        }
    //    }
    //}
    public string GetQueryString()
    {
        string filterText = "";
        foreach (ListItem chkCodeItem in cblCodeOption.Items)
        {
            if (chkCodeItem.Selected)
            {
                filterText += chkCodeItem.Value + ",";
            }
        }
        if (filterText.Length > 0)
        {
            filterText = filterText.Substring(0, filterText.Length - 1);
        }
        return "PatientKey=" + Request.QueryString["PatientKey"] + "" + "&filterText=" + filterText;
    }
    public string GetQueryStringWithPageCount()
    {
        string filterText = "";
        foreach (ListItem chkCodeItem in cblCodeOption.Items)
        {
            if (chkCodeItem.Selected)
            {
                filterText += chkCodeItem.Value + ",";
            }
        }
        if (filterText.Length > 0)
        {
            filterText = filterText.Substring(0, filterText.Length - 1);
        }
        return "PatientKey=" + Request.QueryString["PatientKey"] + "&PageCount=" + RadGridMPI.MasterTableView.PageCount + "&filterText=" + filterText;
    }
    protected void btnApplyCodeFilter_Click(object sender, EventArgs e)
    {
        DataSet dsMPIInfo = PatientManager.GetMPIInfo(patientKey);
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
    protected void RadGridMPI_SortCommand(object source, GridSortCommandEventArgs e)
    {
        //string sortExpression = e.SortExpression;
        //string sortDir = "ASC";
        //if (e.OldSortOrder == GridSortOrder.None)
        //{
        //    sortDir = "ASC";
        //}
        //else if (e.OldSortOrder == GridSortOrder.Ascending)
        //{
        //    sortDir = "DESC";
        //}
        //else if (e.OldSortOrder == GridSortOrder.Descending)
        //{
        //    sortDir = "ASC";
        //}
        ////Aply filter
        //DataSet dsMPIInfo = PatientManager.GetMPIInfo(id, modifierID);
        //string strFilterOriginal = "Type=";
        //ArrayList strArrCode = new ArrayList();
        //foreach (ListItem chkCodeItem in cblCodeOption.Items)
        //{
        //    if (chkCodeItem.Selected)
        //    {
        //        strArrCode.Add(chkCodeItem.Value);
        //    }
        //}
        //for (int j = 0; j < strArrCode.Count; j++)
        //{
        //    if (j == 0)
        //        strFilter = strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
        //    else
        //        strFilter += " OR " + strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
        //}
        //DataTable dtMPIInfo = dsMPIInfo.Tables[0].Copy();
        //DataView dvwMPIInfo = dtMPIInfo.DefaultView;
        //dvwMPIInfo.RowFilter = strFilter;
        //DataTable dtMPIInfoDistinct = dvwMPIInfo.ToTable();

        ////Apply Sorting
        //DataView dtView = new DataView(dtMPIInfoDistinct);

        //dtView.Sort = sortExpression + " " + sortDir;
        //e.Item.OwnerTableView.DataSource = dtView.ToTable();
        //e.Item.OwnerTableView.Rebind();



        //switch (e.SortExpression)
        //{
        //    case "InstitutionName":
        //        RadGridMPI.Columns.FindByUniqueName("Healthcare").Visible = false;
        //        RadGridMPI.Columns.FindByUniqueName("IC").Visible = true;

        //        e.Canceled = true;
        //        break;
        //    case "InstituteCode":
        //        RadGridMPI.Columns.FindByUniqueName("Healthcare").Visible = true;
        //        RadGridMPI.Columns.FindByUniqueName("IC").Visible = false;

        //        e.Canceled = true;
        //        break;
        //    case "DoctorsFirstName":
        //        RadGridMPI.Columns.FindByUniqueName("TemplateColumn").Visible = true;
        //        RadGridMPI.Columns.FindByUniqueName("ProviderName").Visible = false;

        //        e.Canceled = true;
        //        break;
        //    case "ProviderID":
        //        RadGridMPI.Columns.FindByUniqueName("TemplateColumn").Visible = false;
        //        RadGridMPI.Columns.FindByUniqueName("ProviderName").Visible = true;

        //        e.Canceled = true;
        //        break;

        //}
        //if (!e.Canceled)
        //{
        GridSortExpression sortExpr = new GridSortExpression();
        sortExpr.FieldName = e.SortExpression;
        sortExpr.SortOrder = e.OldSortOrder;

        e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
        //}


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
