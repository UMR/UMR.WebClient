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

public partial class Oracle_ControlLibrary_ucRD2 : System.Web.UI.UserControl
{
    long patientKey;
    string disCode, codeType = "";
    RadDockableObject rdoParent;
    protected void Page_Load(object sender, EventArgs e)
    {
        rdoParent = GetParentDockableObject(this);
        string parentID = rdoParent.ID;

        string[] strArr = parentID.Split('_');

        patientKey = Int64.Parse(strArr[0]);
        disCode = strArr[1];

        if (!IsPostBack)
        {
            DataSet dsrdDetail = PatientManager.GetRemarkableDisciplineDetail(disCode);
            if (dsrdDetail.Tables[0].Rows.Count > 0)
            {
                rdoParent.Text = dsrdDetail.Tables[0].Rows[0]["DETAIL"].ToString();
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
    }
    private RadDockableObject GetParentDockableObject(Control control)
    {
        if (control.Parent == null) return null;
        if (control.Parent is RadDockableObject) return (RadDockableObject)control.Parent;
        else return GetParentDockableObject(control.Parent);
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
                    //********************************************** Added

                    string strHRef = "RDDetails.aspx?PatientKey=" + patientKey + "&medCode=" + rowView.Row["MedCode"] + "&disType=" + rowView.Row["DisciplineType"] + "&name=" + rowView.Row["DETAIL"];
                    editLink.Attributes["onClick"] = string.Format("return ShowRemarkableDiscipline('{0}')", strHRef);

                    //***************************************************************************************
                    // This is where we will show the description in the pop up ...
                    string strDesc = rowView.Row["CodeDescription"].ToString().Replace("__", ", "); // Replace the "__" with ", " --> Requested by Steven on April 29th, 2008
                    // Some strings have Single quotes in them, take care of that so we don't get JS error
                    strDesc = strDesc.Replace("'", "\\'");

                    editLink.Attributes["title"] = strDesc;
                    editLink.Attributes["class"] = "tooltip";

                }


            }
        }

    }
}
