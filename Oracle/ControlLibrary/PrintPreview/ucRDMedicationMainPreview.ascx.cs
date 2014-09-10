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

public partial class Oracle_ControlLibrary_PrintPreview_ucRDMedicationMainPreview : System.Web.UI.UserControl
{
    private long patientKey;

    public long PatientKey
    {
        get { return patientKey; }
        set { patientKey = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            //RadGridRDMedicationMain.MasterTableView.DataSource = PatientManager.GetRDMedication(id, modifierID);
            RadGridRDMedicationMain.DataSource = PatientManager.GetRDMedication(patientKey);
            RadGridRDMedicationMain.DataBind();
        }
    }

    protected void RadGridRDMedicationMain_RowCreated(object sender, GridViewRowEventArgs e)
    {
        int i = e.Row.DataItemIndex;
        if (i > -1)
        {
            HyperLink editLink = e.Row.FindControl("Brand") as HyperLink;
            if (editLink != null)
            {
                editLink.Attributes["href"] = "#";
                DataRowView rowView = e.Row.DataItem as DataRowView;
                if (rowView != null)
                {
                    string lastDate = rowView.Row["LastDate"].ToString();
                    //Get the Back Color for the record based on "LastDate"
                    if (!PatientManager.DateRangeApplied)
                        e.Row.BackColor = PatientManager.GetRowColorBasedOnDate(lastDate);

                    //Check to see if we need to put the >!< prefix or not...
                    DateTime MaxOfCodeDate = PatientManager.GetMaxOfCodeDate(patientKey, 'N');
                    DateTime rowLastDate = PatientManager.GetDateFromParts(lastDate);
                    if (MaxOfCodeDate.Date.CompareTo(rowLastDate.Date) == 0)
                    {
                        Label lblSign = e.Row.FindControl("lblExclamation") as Label;
                        if (lblSign != null)
                        {
                            lblSign.Text = "!";
                        }
                    }
                    editLink.Text = rowView.Row["BrandName"].ToString();
                    string strHRef = "RDDetails.aspx?PatientKey=" + patientKey + "&disType=" + rowView.Row["DisciplineType"] + "&name=" + rowView.Row["BrandName"].ToString() + "&BackButton=true" + "&NDCCode=" + rowView.Row["NDCCode"].ToString();
                    editLink.Attributes["href"] = strHRef;
                    //editLink.Attributes["onclick"] = string.Format("return ShowRDMedicationDrillDown('{0}','{1}','{2}','{3}')",
                    //                                                           new object[] { id, modifierID, rowView.Row["DisciplineType"], rowView.Row["BrandName"] });
                }
                //editLink.Attributes["onMouseOver"] = string.Format("return HideStatus()");
            }
        }
    }
}
