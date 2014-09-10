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

public partial class Oracle_ControlLibrary_PrintPreview_ucPatientDemographicPreview2 : System.Web.UI.UserControl
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

        DataTable dtEducation = PatientManager.GetPatientEducation(patientKey);
        if (dtEducation != null && dtEducation.Rows.Count > 0)
        {
            DataTable dtGrammerSchool = dtEducation.Copy();
            DataView dvGrammar = dtGrammerSchool.DefaultView;
            dvGrammar.RowFilter = "EDUCATION_TYPE='Grammar School'";
            dtGrammerSchool = dvGrammar.ToTable();

            DataTable dtJrHighSchool = dtEducation.Copy();
            DataView dvJrHighSchool = dtJrHighSchool.DefaultView;
            dvJrHighSchool.RowFilter = "EDUCATION_TYPE='Jr. High School'";
            dtJrHighSchool = dvJrHighSchool.ToTable();

            DataTable dtHighSchool = dtEducation.Copy();
            DataView dvHighSchool = dtHighSchool.DefaultView;
            dvHighSchool.RowFilter = "EDUCATION_TYPE='High School'";
            dtHighSchool = dvHighSchool.ToTable();

            DataTable dtCollege = dtEducation.Copy();
            DataView dvCollege = dtCollege.DefaultView;
            dvCollege.RowFilter = "EDUCATION_TYPE='College'";
            dtCollege = dvCollege.ToTable();

            DataTable dtGraduateSchool = dtEducation.Copy();
            DataView dvGraduateSchool = dtGraduateSchool.DefaultView;
            dvGraduateSchool.RowFilter = "EDUCATION_TYPE='Graduate School'";
            dtGraduateSchool = dvGraduateSchool.ToTable();

            DataTable dtTradeSchool = dtEducation.Copy();
            DataView dvTradeSchool = dtTradeSchool.DefaultView;
            dvTradeSchool.RowFilter = "EDUCATION_TYPE='Trade, Business or Correspondence School'";
            dtTradeSchool = dvTradeSchool.ToTable();

            lblNoEducation.Visible = false;
            lblNoEducation.Text = "";
            if (dtGrammerSchool != null && dtGrammerSchool.Rows.Count > 0)
            {
                dataListGrammarSchool.Visible = true;
                dataListGrammarSchool.DataSource = dtGrammerSchool;
                dataListGrammarSchool.DataBind();
            }
            else
            {
                dataListGrammarSchool.Visible = false;
            }
            if (dtJrHighSchool != null && dtJrHighSchool.Rows.Count > 0)
            {
                dataListJrHighSchool.Visible = true;
                dataListJrHighSchool.DataSource = dtJrHighSchool;
                dataListJrHighSchool.DataBind();
            }
            else
            {
                dataListJrHighSchool.Visible = false;
            }
            if (dtHighSchool != null && dtHighSchool.Rows.Count > 0)
            {
                dataListHighSchool.Visible = true;
                dataListHighSchool.DataSource = dtHighSchool;
                dataListHighSchool.DataBind();
            }
            else
            {
                dataListHighSchool.Visible = false;
            }
            if (dtCollege != null && dtCollege.Rows.Count > 0)
            {
                dataListCollege.Visible = true;
                dataListCollege.DataSource = dtCollege;
                dataListCollege.DataBind();
            }
            else
            {
                dataListCollege.Visible = false;
            }
            if (dtGraduateSchool != null && dtGraduateSchool.Rows.Count > 0)
            {
                dataListGraduateSchool.Visible = true;
                dataListGraduateSchool.DataSource = dtGraduateSchool;
                dataListGraduateSchool.DataBind();
            }
            else
            {
                dataListGraduateSchool.Visible = false;
            }
            if (dtTradeSchool != null && dtTradeSchool.Rows.Count > 0)
            {
                dataListTrade.Visible = true;
                dataListTrade.DataSource = dtTradeSchool;
                dataListTrade.DataBind();
            }
            else
            {
                dataListTrade.Visible = false;
            }
        }
        else
        {
            lblNoEducation.Visible = true;
            lblNoEducation.Text = "N/A";
            dataListGrammarSchool.Visible = false;
            dataListJrHighSchool.Visible = false;
            dataListHighSchool.Visible = false;
            dataListCollege.Visible = false;
            dataListGraduateSchool.Visible = false;
            dataListTrade.Visible = false;
        }

        DataTable dtEmployer = PatientManager.GetPatientEmployment(patientKey);
        if (dtEmployer != null && dtEmployer.Rows.Count > 0)
        {
            DataTable dtCurrentEmployer = dtEmployer.Copy();
            DataView dvCurrentEmployer = dtCurrentEmployer.DefaultView;
            dvCurrentEmployer.RowFilter = "IsCurrent='True'";
            dtCurrentEmployer = dvCurrentEmployer.ToTable();

            DataTable dtFormerEmployer = dtEmployer.Copy();
            DataView dvFormerEmployer = dtFormerEmployer.DefaultView;
            dvFormerEmployer.RowFilter = "IsCurrent='False'";
            dtFormerEmployer = dvFormerEmployer.ToTable();

            lblNoEmplyer.Visible = false;
            if (dtCurrentEmployer != null && dtCurrentEmployer.Rows.Count > 0)
            {
                dataListEmployerCurrent.Visible = true;
                dataListEmployerCurrent.DataSource = dtCurrentEmployer;
                dataListEmployerCurrent.DataBind();

                lblOccupation.Text = dtCurrentEmployer.Rows[0]["JOBTITLE"].ToString();
            }
            else
            {
                dataListEmployerCurrent.Visible = false;
            }
            if (dtFormerEmployer != null && dtFormerEmployer.Rows.Count > 0)
            {
                dataListEmployerFormer.Visible = true;
                dataListEmployerFormer.DataSource = dtFormerEmployer;
                dataListEmployerFormer.DataBind();
            }
            else
            {
                dataListEmployerFormer.Visible = false;
            }
            
        }
        else
        {
            lblNoEmplyer.Visible = true;
            lblNoEmplyer.Text = "N/A";
            dataListEmployerCurrent.Visible = false;
            dataListEmployerFormer.Visible = false;
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

        //this.lblEmployer.Text = (String.IsNullOrEmpty(row["Employer"].ToString()) ? "N/A" : row["Employer"].ToString());
        //this.lblEducation.Text = (String.IsNullOrEmpty(row["Education"].ToString()) ? "N/A" : row["Education"].ToString());
        this.lblEmail.Text = (String.IsNullOrEmpty(row["Email"].ToString()) ? "N/A" : String.Format("<a href=\"mailto:{0}\">{0}</a>", row["Email"].ToString()));
        this.lblSSN.Text = (String.IsNullOrEmpty(row["PatientID"].ToString()) ? "" : String.Format("{0:###-##-####}", Double.Parse(row["PatientID"].ToString())));

        this.lblPassword.Text = (String.IsNullOrEmpty(row["Password"].ToString()) ? "N/A" : row["Password"].ToString());
        this.lblPasswordHint.Text = (String.IsNullOrEmpty(row["PasswordHint"].ToString()) ? "N/A" : row["PasswordHint"].ToString());

        this.lblWeight.Text = (String.IsNullOrEmpty(row["Weight"].ToString()) ? string.Empty : row["Weight"].ToString());
        this.lblHeight.Text = (String.IsNullOrEmpty(row["Height"].ToString()) ? string.Empty : row["Height"].ToString());
    }

    private string SetAge(DataRow row)
    {
        DateTime DOB;
        string Age="";
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
