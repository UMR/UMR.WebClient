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


public delegate void LegendCompactEventHandler(object sender, string selectedColor);

public partial class Oracle_ControlLibrary_ucLegendCompactCheckBox : System.Web.UI.UserControl
{
    #region Variable(s)
    private string _defaultLedgendUser = "wayne";
    public LegendCompactEventHandler FilterApplied;
    #endregion

    #region Property
    public int PanelWidth
    {
        set { pnlLegend.Width = Unit.Percentage(value); }
    }
    #endregion

    #region Page Event(s)
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadLegend();

        if (PatientManager.DateRangeApplied)
        {
            this.Visible = false;
        }
        else
        {
            this.Visible = true;
        }
    }
    #endregion

    #region Method(s)
    private void PrepareLegend(DataSet dsLegend)
    {
        if (dsLegend == null || dsLegend.Tables[0].Rows.Count <= 0)
        {
        }
        else
        {
            #region Previous code ( Legend in dayrange)
            //show legend
            //int dayRange1 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange1"].ToString());
            //int dayRange2 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange2"].ToString());
            //int dayRange3 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange3"].ToString());

            #region Option for Color 1
            //bool match = false;
            //if (dayRange1 % 7 == 0)
            //{
            //    lblOption1.Text = (dayRange1 / 7).ToString() + " week(s)";
            //    match = true;
            //}
            //if (dayRange1 % 30 == 0)
            //{
            //    lblOption1.Text = (dayRange1 / 30).ToString() + " month(s)";
            //    match = true;
            //}
            //if (dayRange1 % 365 == 0)
            //{
            //    lblOption1.Text = (dayRange1 / 365).ToString() + " year(s)";
            //    match = true;
            //}
            //if (match == false)
            //{
            //    lblOption1.Text = dayRange1.ToString() + " day(s)";
            //}
            #endregion
            #region Option for Color 2
            //match = false;
            //if (dayRange2 % 7 == 0)
            //{
            //    lblOption2.Text = (dayRange2 / 7).ToString() + " week(s)";
            //    match = true;
            //}
            //if (dayRange2 % 30 == 0)
            //{
            //    lblOption2.Text = (dayRange2 / 30).ToString() + " month(s)";
            //    match = true;
            //}
            //if (dayRange2 % 365 == 0)
            //{
            //    lblOption2.Text = (dayRange2 / 365).ToString() + " year(s)";
            //    match = true;
            //}
            //if (match == false)
            //{
            //    lblOption2.Text = dayRange2.ToString() + " day(s)";
            //}
            #endregion
            #region Option for Color 3
            //match = false;
            //if (dayRange3 % 7 == 0)
            //{
            //    lblOption3.Text = (dayRange3 / 7).ToString() + " week(s)";
            //    match = true;
            //}
            //if (dayRange3 % 30 == 0)
            //{
            //    lblOption3.Text = (dayRange3 / 30).ToString() + " month(s)";
            //    match = true;
            //}
            //if (dayRange3 % 365 == 0)
            //{
            //    lblOption3.Text = (dayRange3 / 365).ToString() + " year(s)";
            //    match = true;
            //}
            //if (match == false)
            //{
            //    lblOption3.Text = dayRange3.ToString() + " day(s)";
            //}
            #endregion
            #endregion

            DateTime firstDate = Convert.ToDateTime(dsLegend.Tables[0].Rows[0]["FIRSTDATE"].ToString());
            TimeSpan dayDiff = DateTime.Now.Subtract(firstDate);

            int dayRange2 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange2"].ToString());
            int dayRange3 = Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange3"].ToString());

            lblOption1.Text = "Present to " + firstDate.ToString("MM/dd/yyyy");

            DateTime fromDate2 = firstDate.AddDays(-1);
            DateTime date2 = fromDate2.AddDays(-1 * dayRange2);
            lblOption2.Text = string.Format("{0} to {1}", fromDate2.ToString("MM/dd/yyyy"), date2.ToString("MM/dd/yyyy"));

            DateTime fromDate3 = date2.AddDays(-1);
            DateTime date3 = fromDate3.AddDays(-1 * dayRange3);
            lblOption3.Text = string.Format("{0} to {1}", fromDate3.ToString("MM/dd/yyyy"), date3.ToString("MM/dd/yyyy"));
        }
    }
    private void LoadLegend()
    {
        string userId = HttpContext.Current.User.Identity.Name.Trim().ToUpper();
        DataSet dsLegend = PatientManager.GetLegendByUserID(userId);
        if (dsLegend == null || dsLegend.Tables[0].Rows.Count <= 0)
        {
            /*
               Date: 20 May 2010 By: Animesh
               Scenario: When Trial user login and its legend is not 
               then set 'Dr. Wayne	Wells' ledgend as a default ledgend
           */
            if (userId.ToLower() == "TRIALUSER1" || userId == "TRIALUSER2" || userId == "TRIALUSER3")
            {
                dsLegend = PatientManager.GetLegendByUserID(_defaultLedgendUser.ToUpper());
                this.PrepareLegend(dsLegend);
            }
        }
        else
        {
            this.PrepareLegend(dsLegend);
        }
    }
    #endregion

    #region Event(s)
    protected void btnApply_Click(object sender, EventArgs e)
    {

        string selStr = "";
        if (CheckBox1.Checked) selStr += "#FF8282,";
        if (CheckBox2.Checked) selStr += "#F4A460,";
        if (CheckBox3.Checked) selStr += "#82FF82,";
        if (CheckBox4.Checked) selStr += "#8CDAFF,";
        if (selStr.Length > 0) selStr = selStr.Substring(0, selStr.Length - 1);

        if (FilterApplied != null)
        {
            FilterApplied(sender, selStr);
        }
    }
    #endregion
}
