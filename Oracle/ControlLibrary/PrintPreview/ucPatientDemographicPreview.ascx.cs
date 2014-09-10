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

public partial class Oracle_ControlLibrary_PrintPreview_ucPatientDemographicPreview : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        long patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
        string UserId = HttpContext.Current.User.Identity.Name.ToUpper();
        DataSet ds = PatientManager.GetPatientDemographics(patientKey, UserId);
        if (ds != null)
        {
            DataBindControls(ds);
        }
    }

    private void DataBindControls(DataSet ds)
    {
        DataRow row = ds.Tables[0].Rows[0];
        this.lblID.Text = (String.IsNullOrEmpty(row["PatientID"].ToString()) ? "" : row["PatientID"].ToString());
        this.lblPrefix.Text = (String.IsNullOrEmpty(row["Prefix"].ToString()) ? "" : row["Prefix"].ToString());
        //88
        this.lblFName.Text = (String.IsNullOrEmpty(row["FirstName"].ToString()) ? "" : row["FirstName"].ToString());
        this.lblMName.Text = (String.IsNullOrEmpty(row["MiddleName"].ToString()) ? "" : row["MiddleName"].ToString());
        this.lblLName.Text = (String.IsNullOrEmpty(row["LastName"].ToString()) ? "" : row["LastName"].ToString());
        this.lblSuffix.Text = (String.IsNullOrEmpty(row["Suffix"].ToString()) ? "" : row["Suffix"].ToString());
        this.lblAddress.Text = (String.IsNullOrEmpty(row["StreetAddress"].ToString()) ? "" : row["StreetAddress"].ToString());
        this.lblCity.Text = (String.IsNullOrEmpty(row["CityTown"].ToString()) ? "" : row["CityTown"].ToString());
        this.lblCountry.Text = (String.IsNullOrEmpty(row["Country"].ToString()) ? "" : row["Country"].ToString());
        this.lblState.Text = (String.IsNullOrEmpty(row["State"].ToString()) ? "" : row["State"].ToString());
        this.lblZip.Text = (String.IsNullOrEmpty(row["Zip"].ToString()) ? "" : row["Zip"].ToString());
        //*
        this.lblPhone.Text = ((row["HomePhone"] == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)row["HomePhone"])));
        this.lblBPhone.Text = ((row["BusinessPhone"] == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)row["BusinessPhone"])));
        this.lblCellPhone.Text = ((row["CellPhone"] == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)row["CellPhone"])));
        this.lblPager.Text = ((row["Pager"] == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)row["Pager"])));
        this.lblAge.Text = SetAge(row);
        //*
        this.lblPMarks.Text = (String.IsNullOrEmpty(row["PhysicalMarks"].ToString()) ? "" : row["PhysicalMarks"].ToString());
        this.lblDOB.Text = (String.IsNullOrEmpty(row["DateOfBirth"].ToString()) ? "" : row["DateOfBirth"].ToString());
        this.lblBPlace.Text = (String.IsNullOrEmpty(row["BirthPlace"].ToString()) ? "" : row["BirthPlace"].ToString());
        this.lblSex.Text = (String.IsNullOrEmpty(row["Sex"].ToString()) ? "" : row["Sex"].ToString());
        this.lblMaritalStatus.Text = (String.IsNullOrEmpty(row["MaritalStatus"].ToString()) ? "" : row["MaritalStatus"].ToString());
        this.lblLanguage.Text = (String.IsNullOrEmpty(row["LanguagesSpoken"].ToString()) ? "" : row["LanguagesSpoken"].ToString());
        this.lblReligion.Text = (String.IsNullOrEmpty(row["Religion"].ToString()) ? "" : row["Religion"].ToString());
        this.lblBlood.Text = (String.IsNullOrEmpty(row["BloodType"].ToString()) ? "" : row["BloodType"].ToString());
        this.lblOccupation.Text = (String.IsNullOrEmpty(row["Occupation"].ToString()) ? "" : row["Occupation"].ToString());
        this.lblHair.Text = (String.IsNullOrEmpty(row["HairColor"].ToString()) ? "" : row["HairColor"].ToString());
        this.lblEye.Text = (String.IsNullOrEmpty(row["EyeColor"].ToString()) ? "" : row["EyeColor"].ToString());
        this.lblRace.Text = (String.IsNullOrEmpty(row["Race"].ToString()) ? "" : row["Race"].ToString());
        this.lblCounty.Text = (String.IsNullOrEmpty(row["County"].ToString()) ? "" : row["County"].ToString());
        this.lblEmployer.Text = (String.IsNullOrEmpty(row["Employer"].ToString()) ? "" : row["Employer"].ToString());
        this.lblEducation.Text = (String.IsNullOrEmpty(row["Education"].ToString()) ? "" : row["Education"].ToString());
        this.lblEmail.Text = (String.IsNullOrEmpty(row["Email"].ToString()) ? "" : String.Format("<a href=\"mailto:{0}\">{0}</a>", row["Email"].ToString()));
        this.lblSSN.Text = (String.IsNullOrEmpty(row["PatientID"].ToString()) ? "" : String.Format("{0:###-##-####}", Double.Parse(row["PatientID"].ToString())));
        this.lblWeight.Text = (String.IsNullOrEmpty(row["Weight"].ToString()) ? string.Empty : row["Weight"].ToString());
        this.lblHeight.Text = (String.IsNullOrEmpty(row["Height"].ToString()) ? string.Empty : row["Height"].ToString());
    }

    private string SetAge(DataRow row)
    {
        DateTime DOB;
        string Age="0";
        DateTime.TryParse(row["DateOfBirth"].ToString(), out DOB);
        if (DOB != null)
        {
            Age = FormatAge(DOB, DateTime.Now);
        }
        return Age;
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
