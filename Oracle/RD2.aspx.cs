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

public partial class Oracle_RD2 : System.Web.UI.Page
{
    long patientKey;
    string disCode, codeType = "";
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
        patientKey =Int64.Parse(Request.QueryString["PatientKey"]);
        disCode = Request.QueryString["disCode"];
        //codeType = Request.QueryString["codeType"];
        if (!IsPostBack)
        {
            DataSet dsrdDetail = PatientManager.GetRemarkableDisciplineDetail(disCode);
            if (dsrdDetail.Tables[0].Rows.Count > 0)
            {
                this.Title = dsrdDetail.Tables[0].Rows[0]["DETAIL"].ToString();
            }
            //Now get the Data
            DataSet dsRD = PatientManager.GetRDDiagnosis(patientKey, Convert.ToDecimal(disCode));
            //Create the Source Table... Default to original dsRD table
            DataTable dtRD = dsRD.Tables[0].Copy();
            //Check if CodeType filter string has been provided or not
            if (!String.IsNullOrEmpty(codeType))
            {
                //Apply the filter on the data
                DataView dvwRD = dsRD.Tables[0].DefaultView;
                dvwRD.RowFilter = codeType.Replace("@", "'");
                dtRD.Clear();
                dtRD = dvwRD.ToTable();
            }
            //Now Set The DataSource and Bind
            RadGridRDDiagnosisMain.DataSource = dtRD;
            RadGridRDDiagnosisMain.DataBind();
        }
        UcLegendCompact1.FilterApplied += new LegendCompactEventHandler(RefreshPage);
    }

    private void RefreshPage(object sender, string selectedColor)
    {
        SelectedColor = selectedColor;

        DataSet dsrdDetail = PatientManager.GetRemarkableDisciplineDetail(disCode);
        if (dsrdDetail.Tables[0].Rows.Count > 0)
        {
            this.Title = dsrdDetail.Tables[0].Rows[0]["DETAIL"].ToString();
        }
        //Now get the Data
        DataSet dsRD = PatientManager.GetRDDiagnosis(patientKey, Convert.ToDecimal(disCode));
        //Create the Source Table... Default to original dsRD table
        DataTable dtRD = dsRD.Tables[0].Copy();
        //Check if CodeType filter string has been provided or not
        if (!String.IsNullOrEmpty(codeType))
        {
            //Apply the filter on the data
            DataView dvwRD = dsRD.Tables[0].DefaultView;
            dvwRD.RowFilter = codeType.Replace("@", "'");
            dtRD.Clear();
            dtRD = dvwRD.ToTable();
        }
        //Now Set The DataSource and Bind
        RadGridRDDiagnosisMain.DataSource = dtRD;
        RadGridRDDiagnosisMain.DataBind();
    }

    protected void RadGridRDDiagnosisMain_RowCreated(object sender, GridViewRowEventArgs e)
    {
        int i = e.Row.DataItemIndex;
        if (i > -1)
        {
            HyperLink editLink = e.Row.FindControl("Details") as HyperLink;
            if (editLink != null)
            {
                //editLink.Attributes["href"] = "#";

                DataRowView rowView = e.Row.DataItem as DataRowView;
                if (rowView != null)
                {
                    string lastDate = rowView.Row["LastDate"].ToString();
                    //Get the Back Color for the record based on "LastDate"
                    if (!PatientManager.DateRangeApplied)
                        e.Row.BackColor = PatientManager.GetRowColorBasedOnDate(lastDate);
                    if (SelectedColor != null)
                    {
                        string colorHex = System.Drawing.ColorTranslator.ToHtml(e.Row.BackColor);
                        if (SelectedColor.IndexOf(colorHex) < 0) e.Row.BackColor = System.Drawing.Color.Transparent;
                    }

                    //Check to see if we need to put the >!< prefix or not...
                    DateTime MaxOfCodeDate = PatientManager.GetMaxOfCodeDate(patientKey, 'D');
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
                    //********************************************** Added

                    DataSet dsRMDisc = PatientManager.GetRemarkableDisciplineListForMedCode(rowView.Row["MedCode"].ToString(), disCode);
                    string relatedDisList = string.Empty;
                    if (dsRMDisc != null)
                    {
                        relatedDisList = GetRemarkableDiscList(dsRMDisc);
                    }


                    //***************************************************************************************
                    // This is where we will show the description in the pop up ...
                    string strDesc = rowView.Row["CodeDescription"].ToString().Replace("__", ", "); // Replace the "__" with ", " --> Requested by Steven on April 29th, 2008
                    // Some strings have Single quotes in them, take care of that so we don't get JS error
                    strDesc = strDesc.Replace("'", "\\'");

                    string strHRef = "RDDetails.aspx?PatientKey=" + patientKey + "&medCode=" + rowView.Row["MedCode"] + "&disType=" + rowView.Row["DisciplineType"] + "&name=" + rowView.Row["DETAIL"] + "&desc=" + strDesc;
                    editLink.Attributes["onClick"] = string.Format("AddToUnvisitedDiscipline('{0}'); return ShowRemarkableDiscipline('{1}');", relatedDisList, strHRef);

                    editLink.Attributes["title"] = strDesc;
                    editLink.Attributes["class"] = "tooltip";

                }


            }
        }

    }
    private string GetRemarkableDiscList(DataSet dsRMDisc)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < dsRMDisc.Tables[0].Rows.Count; i++)
        {
            bool alreadOpend = false;
            string openedDiscipline = "";
            if (Request.QueryString["OpenedDiscipline"] != null)
            {
                openedDiscipline = Request.QueryString["OpenedDiscipline"];
            }

            if (openedDiscipline.Length > 0)
            {
                string[] strArr = openedDiscipline.Split(',');
                for (int k = 0; k < strArr.Length; k++)
                {
                    if (strArr[k].Trim().Equals(dsRMDisc.Tables[0].Rows[i]["DisCode"].ToString()))
                    {
                        alreadOpend = true;
                        break;
                    }
                }
                openedDiscipline += "," + dsRMDisc.Tables[0].Rows[i]["DisCode"].ToString();
            }
            if (alreadOpend == false)
                sb.Append(dsRMDisc.Tables[0].Rows[i]["DETAIL"].ToString().Trim() + ",");
        }
        string str = sb.ToString();
        if (str.Length > 0) str = str.Substring(0, str.Length - 1);
        return str;
    }
}
