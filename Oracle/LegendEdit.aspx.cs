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
using System.Drawing;

public partial class Oracle_LegendEdit : System.Web.UI.Page
{
    #region Page Event(s)
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadLegend();
            LoadHelpInfo();
            LoadDateRangeInfo();
        }
    }
    #endregion

    #region Method(s)
    private void LoadDateRangeInfo()
    {
        chkDateRange.Checked = PatientManager.DateRangeApplied;
        int days = ((TimeSpan)(PatientManager.dateRangeStart - PatientManager.dateRangeEnd)).Days;
        bool match = false;
        if (days % 7 == 0)
        {
            DropDownList1.SelectedIndex = 1;
            txtDateRange.Text = (days / 7).ToString();
            match = true;
        }
        if (days % 30 == 0)
        {
            DropDownList1.SelectedIndex = 2;
            txtDateRange.Text = (days / 30).ToString();
            match = true;
        }
        if (days % 365 == 0)
        {
            DropDownList1.SelectedIndex = 3;
            txtDateRange.Text = (days / 365).ToString();
            match = true;
        }
        if (match == false)
        {
            DropDownList1.SelectedIndex = 0;
            txtDateRange.Text = days.ToString();
        }
    }
    private void LoadHelpInfo()
    {
        long patientKey = Int64.Parse(Request.QueryString["PatientKey"].Trim());
        DataTable dt = PatientManager.GetLastDateAndFirstDate(patientKey);
        if (dt.Rows.Count <= 0) return;

        if (dt.Rows.Count > 0)
        {
            lblFirstDate.Text = DateTime.Parse(dt.Rows[0]["FirstDate"].ToString()).ToString("MM/dd/yyyy");
            lblLastDate.Text = DateTime.Parse(dt.Rows[0]["LastDate"].ToString()).ToString("MM/dd/yyyy");
        }
        DateTime lastDate = DateTime.Parse(dt.Rows[0]["LastDate"].ToString());
        TimeSpan diff = DateTime.Today - lastDate;
        int days = diff.Days;
        lblLegenReddHint.Text = days + " day(s)";
        if (days % 7 == 0)
        {
            lblLegenReddHint.Text = days / 7 + " week(s)";
        }
        if (days % 30 == 0)
        {
            lblLegenReddHint.Text = days / 30 + " month(s)";
        }
        if (days % 365 == 0)
        {
            lblLegenReddHint.Text = days / 365 + " year(s)";
        }

    }
    //private void LoadLegend2() //Previous
    //{
    //    string userId = HttpContext.Current.User.Identity.Name.Trim().ToUpper();
    //    DataSet dsLegend = PatientManager.GetLegendByUserID(userId);
    //    if (dsLegend == null || dsLegend.Tables[0].Rows.Count <= 0)
    //    {
    //        //add new legend
    //        PatientManager.LegendAdNew(userId.ToLower(), 365, 365, 730, 0);
    //    }
    //    else
    //    {
    //        //show legend
    //        int dayRange1 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange1"].ToString());
    //        int dayRange2 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange2"].ToString());
    //        int dayRange3 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange3"].ToString());

    //        #region Option for Color 1
    //        bool match = false;
    //        if (dayRange1 % 7 == 0)
    //        {
    //            //ddlDay1.SelectedIndex = 1;
    //            txtDay1.Text = (dayRange1 / 7).ToString();
    //            match = true;
    //        }
    //        if (dayRange1 % 30 == 0)
    //        {
    //            //ddlDay1.SelectedIndex = 2;
    //            txtDay1.Text = (dayRange1 / 30).ToString();
    //            match = true;
    //        }
    //        if (dayRange1 % 365 == 0)
    //        {
    //            //ddlDay1.SelectedIndex = 3;
    //            txtDay1.Text = (dayRange1 / 365).ToString();
    //            match = true;
    //        }
    //        if (match == false)
    //        {
    //            //ddlDay1.SelectedIndex = 0;
    //            txtDay1.Text = dayRange1.ToString();
    //        }
    //        #endregion

    //        #region Option for Color 2
    //        match = false;
    //        if (dayRange2 % 7 == 0)
    //        {
    //            //ddlDay2.SelectedIndex = 1;
    //            txtDay2.Text = (dayRange2 / 7).ToString();
    //            match = true;
    //        }
    //        if (dayRange2 % 30 == 0)
    //        {
    //            //ddlDay2.SelectedIndex = 2;
    //            txtDay2.Text = (dayRange2 / 30).ToString();
    //            match = true;
    //        }
    //        if (dayRange2 % 365 == 0)
    //        {
    //            //ddlDay2.SelectedIndex = 3;
    //            txtDay2.Text = (dayRange2 / 365).ToString();
    //            match = true;
    //        }
    //        if (match == false)
    //        {
    //            //ddlDay2.SelectedIndex = 0;
    //            txtDay2.Text = dayRange2.ToString();
    //        }
    //        #endregion

    //        #region Option for Color 3
    //        match = false;
    //        if (dayRange3 % 7 == 0)
    //        {
    //            //ddlDay3.SelectedIndex = 1;
    //            txtDay3.Text = (dayRange3 / 7).ToString();
    //            match = true;
    //        }
    //        if (dayRange3 % 30 == 0)
    //        {
    //            //ddlDay3.SelectedIndex = 2;
    //            txtDay3.Text = (dayRange3 / 30).ToString();
    //            match = true;
    //        }
    //        if (dayRange3 % 365 == 0)
    //        {
    //            //ddlDay3.SelectedIndex = 3;
    //            txtDay3.Text = (dayRange3 / 365).ToString();
    //            match = true;
    //        }
    //        if (match == false)
    //        {
    //            //ddlDay3.SelectedIndex = 0;
    //            txtDay3.Text = dayRange3.ToString();
    //        }
    //        #endregion

    //    }
    //}
    private void LoadLegend()
    {
        string userId = HttpContext.Current.User.Identity.Name.Trim().ToUpper();
        DataSet dsLegend = PatientManager.GetLegendByUserID(userId);
        if (dsLegend == null || dsLegend.Tables[0].Rows.Count <= 0)
        {
            //add new legend
            PatientManager.LegendAddNewDefault(userId.ToLower());
        }
        else
        {
            DateTime date1 = Convert.ToDateTime(dsLegend.Tables[0].Rows[0]["FirstDate"]);
            TimeSpan ts = DateTime.Now.Subtract(date1);
            int dayRange1 = ts.Days;
            int dayRange2 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange2"]);
            int dayRange3 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange3"]);

            txtDay1.Text = date1.ToString("MM/dd/yyyy");
            lblDateDiff1.Text = dayRange1.ToString();

            DateTime fromDate2 = date1.AddDays(-1);
            lblFromDate2.Text = fromDate2.ToString("MM/dd/yyyy");
            DateTime date2 = fromDate2.AddDays(-1 * dayRange2);
            txtDay2.Text = date2.ToString("MM/dd/yyyy");
            lblDateDiff2.Text = dayRange2.ToString();

            DateTime fromDate3 = date2.AddDays(-1);
            lblFromDate3.Text = fromDate3.ToString("MM/dd/yyyy");
            txtDay3.Text = fromDate3.AddDays(-1 * dayRange3).ToString("MM/dd/yyyy");
            lblDateDiff3.Text = dayRange3.ToString();
        }
    }
    #endregion

    #region Event(s)
    protected void btnSaveLegend_Click(object sender, ImageClickEventArgs e)
    {
        string userId = HttpContext.Current.User.Identity.Name.Trim().ToUpper();
        DateTime dayRange1 = DateTime.MinValue, toDate2 = DateTime.MinValue, toDate3= DateTime.MinValue;
        try
        {
            dayRange1 = Convert.ToDateTime(txtDay1.Text);
            toDate2 = Convert.ToDateTime(txtDay2.Text.Trim());
            toDate3 = Convert.ToDateTime(txtDay3.Text.Trim());

            DateTime fromDate2 = dayRange1.AddDays(-1);

            TimeSpan ts2 = fromDate2.Subtract(toDate2);
            int dayRange2 = ts2.Days;

            DateTime fromDate3 = toDate2.AddDays(-1);

            TimeSpan ts3 = fromDate3.Subtract(toDate3);
            int dayRange3 = ts3.Days;


            #region For Color1 -- animesh
            //if (ddlDay1.SelectedIndex == 0)
            //{
            //    dayRange1 = Convert.ToInt32(txtDay1.Text);
            //}
            //else if (ddlDay1.SelectedIndex == 1)
            //{
            //    dayRange1 = 7 * Convert.ToInt32(txtDay1.Text);
            //}
            //else if (ddlDay1.SelectedIndex == 2)
            //{
            //    dayRange1 = 30 * Convert.ToInt32(txtDay1.Text);
            //}
            //else if (ddlDay1.SelectedIndex == 3)
            //{
            //    dayRange1 = 365 * Convert.ToInt32(txtDay1.Text);
            //}
            #endregion

            #region For Color2 -- Animesh
            //if (ddlDay2.SelectedIndex == 0)
            //{
            //    dayRange2 = Convert.ToInt32(txtDay2.Text);
            //}
            //else if (ddlDay2.SelectedIndex == 1)
            //{
            //    dayRange2 = 7 * Convert.ToInt32(txtDay2.Text);
            //}
            //else if (ddlDay2.SelectedIndex == 2)
            //{
            //    dayRange2 = 30 * Convert.ToInt32(txtDay2.Text);
            //}
            //else if (ddlDay2.SelectedIndex == 3)
            //{
            //    dayRange2 = 365 * Convert.ToInt32(txtDay2.Text);
            //}
            #endregion

            #region For Color3 -- Animesh
            //if (ddlDay3.SelectedIndex == 0)
            //{
            //    dayRange3 = Convert.ToInt32(txtDay3.Text);
            //}
            //else if (ddlDay3.SelectedIndex == 1)
            //{
            //    dayRange3 = 7 * Convert.ToInt32(txtDay3.Text);
            //}
            //else if (ddlDay3.SelectedIndex == 2)
            //{
            //    dayRange3 = 30 * Convert.ToInt32(txtDay3.Text);
            //}
            //else if (ddlDay3.SelectedIndex == 3)
            //{
            //    dayRange3 = 365 * Convert.ToInt32(txtDay3.Text);
            //}
            #endregion

            PatientManager.UpdateLegend(userId, dayRange1, dayRange2, dayRange3, 0);

            LoadLegend();

            if (chkDateRange.Checked)
            {
                if (txtDateRange.Text.Trim() == String.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "dateRangeBlank", "alert('Date Range Field is Empty. Please Provide value.');", true);
                }
                else
                {
                    int daysBack = 0;
                    if (DropDownList1.SelectedIndex == 0) daysBack = Int32.Parse(txtDateRange.Text.Trim());
                    if (DropDownList1.SelectedIndex == 1) daysBack = 7 * Int32.Parse(txtDateRange.Text.Trim());
                    if (DropDownList1.SelectedIndex == 2) daysBack = 30 * Int32.Parse(txtDateRange.Text.Trim());
                    if (DropDownList1.SelectedIndex == 3) daysBack = 365 * Int32.Parse(txtDateRange.Text.Trim());
                    PatientManager.dateRangeEnd = DateTime.Today.Subtract(new TimeSpan(daysBack, 0, 0, 0));
                    PatientManager.DateRangeApplied = true;
                }
            }
            else
            {
                PatientManager.DateRangeApplied = false;
            }
            lblMessage.Text = "Successfully Saved";
            lblMessage.ForeColor = Color.Green;
            lblMessage.Visible = true;
            issaved.Value = "saved";
        }
        catch
        {
            lblMessage.Text = "Failed to save.";
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            issaved.Value = "NotSaved";
        }
    }
    #endregion
}
