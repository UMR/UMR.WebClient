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

public partial class Oracle_ControlLibrary_PrintPreview_ucRDDiagnosisMainPreview : System.Web.UI.UserControl
{
    private long patientKey;
    private string disCode, codeType; //, medCode, codeVersion, 

    #region Public Properties
    public string DisCode
    {
        get { return disCode; }
        set { disCode = value; }
    }

    //public string CodeVersion
    //{
    //    get { return codeVersion; }
    //    set { codeVersion = value; }
    //}

    //public string MedCode
    //{
    //    get { return medCode; }
    //    set { medCode = value; }
    //}

    public string CodeType
    {
        get { return codeType; }
        set { codeType = value; }
    }

    public long PatientKey
    {
        get { return patientKey; }
        set { patientKey = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
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

    }

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
                    //Get the Back Color for the record based on "LastDate"
                    if (!PatientManager.DateRangeApplied)
                        e.Row.BackColor = PatientManager.GetRowColorBasedOnDate(lastDate);

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
                    string strHRef = "RDDetails.aspx?PatientKey=" + patientKey  + "&medCode=" + rowView.Row["MedCode"] + "&disType=" + rowView.Row["DisciplineType"] + "&name=" + rowView.Row["DETAIL"] + "&BackButton=true";
                    editLink.Attributes["href"] = strHRef;
                    //Comment out the "onclick" attribute -- else the browser will not change the link color on click...
                    //editLink.Attributes["onclick"] = string.Format("return ShowRemarkableDiscipline('{0}')", editLink.Attributes["href"].ToString());
                    //***************************************************************************************
                    // This is where we will show the description in the pop up ... NOT for PREVIEW...
                    //string strDesc = rowView.Row["CodeDescription"].ToString();
                    //editLink.Attributes["onMouseOver"] = string.Format("return ShowDescription('{0}')", strDesc);
                }
            }
        }
    }
}
