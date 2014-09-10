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
using Telerik.WebControls;

public partial class Oracle_ControlLibrary_ucRD2l : System.Web.UI.UserControl
{
    string codeType = "";

    #region Property
    public long PatientKey
    {
        get
        {
            long key = 0;
            if (ViewState["PatientKey"] != null)
                key = Convert.ToInt64(ViewState["PatientKey"]);
            return key;
        }
        set
        {
            ViewState["PatientKey"] = value;
        }
    }
    public string DisCode
    {
        get
        {
            string key = string.Empty;
            if (ViewState["DisCode"] != null)
                key = ViewState["DisCode"].ToString();
            return key;
        }
        set
        {
            ViewState["DisCode"] = value;
        }
    }
    public string CodeType
    {
        get
        {
            string key = string.Empty;
            if (ViewState["CodeType"] != null)
                key = ViewState["CodeType"].ToString();
            return key;
        }
        set
        {
            ViewState["CodeType"] = value;
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
    public string Clicked
    {
        get
        {
            if (ViewState["Clicked"] != null)
            {
                return ViewState["Clicked"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        set
        {
            ViewState["Clicked"] = value;
        }
    }
    #endregion

    #region Page Event(s)
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (!IsPostBack)
        //{
            DataSet dsRD = PatientManager.GetRDDiagnosis(PatientKey, Convert.ToDecimal(DisCode));
            DataTable dtRD = dsRD.Tables[0].Copy();

            if (!String.IsNullOrEmpty(CodeType))
            {
                DataView dvwRD = dsRD.Tables[0].DefaultView;
                dvwRD.RowFilter = CodeType.Replace("@", "'");
                dtRD.Clear();
                dtRD = dvwRD.ToTable();
            }

            RadGridRDDiagnosisMain.DataSource = dtRD;
            RadGridRDDiagnosisMain.DataBind(); 
        //}
    }
    #endregion

    #region Method(s)
    public void RefreshPage(string selectedColor)
    {
        SelectedColor = selectedColor;

        DataSet dsRD = PatientManager.GetRDDiagnosis(PatientKey, Convert.ToDecimal(DisCode));
        DataTable dtRD = dsRD.Tables[0].Copy();

        if (!String.IsNullOrEmpty(CodeType))
        {

            DataView dvwRD = dsRD.Tables[0].DefaultView;
            dvwRD.RowFilter = CodeType.Replace("@", "'");
            dtRD.Clear();
            dtRD = dvwRD.ToTable();
        }
        RadGridRDDiagnosisMain.DataSource = dtRD;
        RadGridRDDiagnosisMain.DataBind();
    }
    public void RefreshPage(object sender, string selectedColor)
    {
        SelectedColor = selectedColor;
        DataSet dsRD = PatientManager.GetRDDiagnosis(PatientKey, Convert.ToDecimal(DisCode));
        DataTable dtRD = dsRD.Tables[0].Copy();

        RadGridRDDiagnosisMain.DataSource = dtRD;
        RadGridRDDiagnosisMain.DataBind();
    }
    private string GetRelatedDiscipline(string discode, DataTable dt)
    {
        if (dt.Rows.Count == 0) return string.Empty;

        StringBuilder str = new StringBuilder(discode);

        foreach (DataRow oRow in dt.Rows)
        {
            str.Append(str.Length == 0 ? oRow["DisCode"].ToString() : string.Format(",{0}", oRow["DisCode"]));
        }
        return str.ToString();
    }
    private string RelatedDisciplineNames(DataTable dt)
    {
        if (dt.Rows.Count == 0) return string.Format("<b>Related Discipline(s):</b> N/A");
        StringBuilder str = new StringBuilder();

        foreach (DataRow oRow in dt.Select(string.Empty, "DETAIL ASC"))
        {
            str.Append(str.Length == 0 ? oRow["DETAIL"].ToString() : string.Format(", {0}", oRow["DETAIL"]));
        }
        return string.Format("<b>Related Discipline(s):</b> {0}", str.ToString());
    }
    #endregion

    #region Event(s)
    protected void RadGridRDDiagnosisMain_RowCreated(object sender, GridViewRowEventArgs e)
    {
        int i = e.Row.DataItemIndex;
        if (i > -1)
        {
            HyperLink editLink = e.Row.FindControl("Details") as HyperLink;
            if (editLink != null)
            {
                DataRowView rowView = e.Row.DataItem as DataRowView;
                if (rowView != null)
                {
                    string lastDate = rowView.Row["LastDate"].ToString();
                    if (!PatientManager.DateRangeApplied)
                        e.Row.BackColor = PatientManager.GetRowColorBasedOnDate(lastDate);
                    if (SelectedColor != null)
                    {
                        string colorHex = System.Drawing.ColorTranslator.ToHtml(e.Row.BackColor);
                        if (SelectedColor.IndexOf(colorHex) < 0) e.Row.BackColor = System.Drawing.Color.Transparent;
                    }
                    DateTime MaxOfCodeDate = PatientManager.GetMaxOfCodeDate(PatientKey, 'D');
                    DateTime rowLastDate = PatientManager.GetDateFromParts(lastDate);
                    if (MaxOfCodeDate.Date.CompareTo(rowLastDate.Date) == 0)
                    {
                        Label lblSign = e.Row.FindControl("lblExclamation") as Label;
                        if (lblSign != null)
                        {
                            lblSign.Text = "!";
                        }
                    }
                    editLink.Text = rowView.Row["DETAIL"].ToString();


                    DataSet dsRMDisc = PatientManager.GetRemarkableDisciplineListForMedCode(rowView.Row["MedCode"].ToString(), DisCode);

                        string strDesc = rowView.Row["CodeDescription"].ToString().Replace("__", ", ").Replace('\0', ' ').Trim();
                        strDesc = strDesc.Replace("'", "\\'");

                        string codedescriptionHistory = GetCodeDescriptinChangeHistoryString(rowView.Row["CodeType"].ToString(), rowView.Row["MedCode"].ToString());//GetCodeDescriptinChangeHistory
                        //if (rowView.Row["CodeDiscriptionHistory"] != null)
                        //{
                        //    codedescriptionHistory = rowView.Row["CodeDiscriptionHistory"].ToString().Replace("__", ", ").Replace('\0', ' ').Trim();
                        //    codedescriptionHistory = "<div style=\"color:green;\">" + codedescriptionHistory + "</div>";
                        //}

                        //editLink.Attributes["class"] = "tooltip";
                        //editLink.ToolTip = string.Format("<div style=\"color:blue;\">{0}</div>{1}", RelatedDisciplineNames(dsRMDisc.Tables.Count == 0 ? new DataTable() : dsRMDisc.Tables[0]), codedescriptionHistory);

                        editLink.Attributes["class"] = "rdcluetip";
                        editLink.Attributes["rel"] = String.Format("RDTooltip.aspx?discode={0}&codetype={1}&medcode={2}", DisCode, rowView.Row["CodeType"].ToString(), rowView.Row["MedCode"].ToString());

                        if (editLink.Text.Trim() != Clicked.Trim())
                        {
                            if (dsRMDisc.Tables[0].Rows.Count != 0)
                                editLink.Attributes["onClick"] = string.Format("LoadRelatedDiscipline({0},\"{1}\",\"{2}\")", PatientKey, this.GetRelatedDiscipline(DisCode, dsRMDisc.Tables.Count == 0 ? new DataTable() : dsRMDisc.Tables[0]), editLink.Text);
                        }
                        else
                        {
                            //editLink.ToolTip = string.Empty;
                            //editLink.Attributes["class"] = "disableRD";

                            //editLink.Attributes["disabled"] = "disabled";

                            editLink.Attributes["style"] = "color:Black; cursor:pointer";
                        }

                        string strHRef = "RDDetails.aspx?PatientKey=" + PatientKey + "&medCode=" + rowView.Row["MedCode"].ToString().Replace("'", "\\'") + "&disType=" + rowView.Row["DisciplineType"].ToString().Replace("'", "\\'") + "&name=" + rowView.Row["DETAIL"].ToString().Replace("'", "\\'");
                        ImageButton imgDetail = e.Row.FindControl("btnDetail") as ImageButton;
                        if (imgDetail != null)
                        {
                            strHRef += "&desc=" + strDesc;

                            imgDetail.ToolTip = "Drill down";
                            imgDetail.Attributes["onClick"] = string.Format("return ShowRemarkableDiscipline('{0}')", strHRef);
                        }
                    
                }
            }
        }
    }

    private string GetCodeDescriptinChangeHistoryString(string codeType, string medcode)
    {
        DataTable dt = PatientManager.GetCodeDescriptinChangeHistory(codeType, medcode);
        if (dt != null && dt.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div>");
            string strDesc = dt.Rows[0]["DETAIL"].ToString().Replace("__", ", ").Replace('\0', ' ').Trim();
            strDesc = strDesc.Replace("'", "\\'");

            sb.Append("<div>" + strDesc + "</div>");
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (i <(dt.Rows.Count - 1))
                {
                    sb.Append("<div  style=\"color:green;\">" + dt.Rows[i]["CODE_VERSION"].ToString() + " <span>-</span>Code Modified</div>"); 
                }
                else
                {
                    sb.Append("<div  style=\"color:green;\">" + dt.Rows[i]["CODE_VERSION"].ToString() + " <span>-</span>Code Originated</div>"); 
                }
            }
            sb.Append("</div>");
            return sb.ToString();
        }
        return "";
    }
    #endregion
}
