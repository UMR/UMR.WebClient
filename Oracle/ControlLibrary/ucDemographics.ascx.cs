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

public partial class Oracle_ControlLibrary_ucDemographics : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsCallback)
        //{
        long patientKey = Int64.Parse(Request.QueryString["PatientKey"].Trim());

        string UserId = HttpContext.Current.User.Identity.Name.ToUpper();
        DataSet ds = PatientManager.GetPatientDemographics(patientKey, UserId);
            if (ds != null)
            {
                string fName = (String.IsNullOrEmpty(ds.Tables[0].Rows[0]["FirstName"].ToString()) ? "" : ds.Tables[0].Rows[0]["FirstName"].ToString());
                string lName = (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["LastName"].ToString()) ? "" : ds.Tables[0].Rows[0]["LastName"].ToString());
                InjectScriptLabel.Text = "<script>SetTitle('" + fName + "','" + lName + "')</" + "script>";
            }
            dtvDemographics.DataSource = ds;
            dtvDemographics.DataBind();
        //}
    }

    protected void dtvDemographics_DataBound(object sender, EventArgs e)
    {
        Label lblAge = this.dtvDemographics.FindControl("lblAge") as Label;
        if (lblAge != null)
        {
            DataSet ds = dtvDemographics.DataSource as DataSet;
            DateTime DOB;
            DateTime.TryParse(ds.Tables[0].Rows[0]["DateOfBirth"].ToString(), out DOB);
            if (DOB != null)
            {
                string Age = FormatAge(DOB, DateTime.Now);
                lblAge.Text = Age;
            } 
        }
    }

    private string FormatAge(DateTime start, DateTime end)
    {
        int months = 0, days = 0;
        // Compute the difference between start year and end year. 
        int years = end.Year - start.Year;
        // Check if the last year was a full year. 
        if (end < start.AddYears(years) && years != 0)
            --years;
        start = start.AddYears(years);
        // Now we know start <= end and the diff between them is < 1 year. 
        if (start.Year == end.Year)
            months = end.Month - start.Month;
        else
            months = (12 - start.Month) + end.Month;
        // Check if the last month was a full month.
        if (end < start.AddMonths(months) && months != 0)
            --months;
        start = start.AddMonths(months);
        // Now we know that start < end and is within 1 month of each other. 
        days = (end - start).Days;
        // Return the final string...
        return years + " years " + months + " months " + days + " days";
    }

}
