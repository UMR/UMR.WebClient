using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

public partial class Oracle_ControlLibrary_ucRDQuickLinksButtons : System.Web.UI.UserControl
{
    private string _defaultLedgendUser = "wayne";
    private long patientKey;
    private string strFilter = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
        //We changed the query so need to do a distinct on the source table ...Now query returns Multiple Orthopedics for example... cause Orthopedics both has ICD-9 and CPT-4
        DataSet dsRD = new DataSet();
        if (Request.QueryString["reloaded"] != null && Request.QueryString["reloaded"] == "true")
        {
            // the page is reloaded and for that we do not need to access time entry to UMR_Patient_Access table
            dsRD = PatientManager.GetRemarkableDisciplineList(patientKey);
        }
        else if (HttpContext.Current.User.Identity.Name.ToLower().Trim().Equals("test"))
        {
            dsRD = PatientManager.GetRemarkableDisciplineList(patientKey);
        }
        else
        {
            dsRD = PatientManager.GetRemarkableDisciplineList(patientKey, HttpContext.Current.User.Identity.Name.ToString());
        }

        DataTable dtRD = dsRD.Tables[0].Copy();
        DataView dvwRD = dtRD.DefaultView;
        strFilter = LoadFilters();
        if (strFilter.Trim().Length > 0)
        {
            dvwRD.RowFilter = strFilter;
        }
        DataTable dtRDDistinct = dvwRD.ToTable(true, new string[] { "Detail", "DisCode", "DisciplineType" });

        //DataTable dtRDDistinct = dsRD.Tables[0].DefaultView.ToTable(true, new string[] { "Detail", "DisCode", "DisciplineType" });

        //***************************************************************************************
        //dlRDQuickLinks.DataSource = PatientManager.GetRemarkableDisciplineList(id, modifierID);
        //***************************************************************************************
        //Now Sort the List //May 5th 2008
        DataView dvwSortedRDList = new DataView(dtRDDistinct, "", "Detail", DataViewRowState.CurrentRows);
        if (dvwSortedRDList != null)
        {
            this.phRD.Controls.Clear();
            BuildRDLinkButtons(dvwSortedRDList.ToTable());
        }


        DataTable dtURD = dsRD.Tables[0].Copy();
        DataView dvwURD = dtURD.DefaultView;
        strFilter = LoadURDFilters();
        if (strFilter.Trim().Length > 0)
        {
            dvwURD.RowFilter = strFilter;
        }
        DataTable dtURDNew = dvwURD.ToTable(true, new string[] { "Detail", "DisCode", "DisciplineType" });
        DataView dvwSortedURDList = new DataView(dtURDNew, "", "Detail", DataViewRowState.CurrentRows);


        //Now bind the new Table to the DataList
        if (dtURDNew != null)
        {
            litURDHeader.Text = "";
            litURDHeader.Text = MakeURDHeader();
            //Clear the place holder first...
            this.phURD.Controls.Clear();
            BuildURDLinkButtons(dvwSortedURDList.ToTable());
        }
        else
        {
            litURDHeader.Text = string.Empty;
        }

        string selectedCodeIndex = "";
        for (int i = 0; i < cblCodeOption.Items.Count; i++)
        {
            if (cblCodeOption.Items[i].Selected)
            {
                selectedCodeIndex += i + ",";
            }
        }
        Session["SelectedCodeIndex"] = selectedCodeIndex;

        //dlRDQuickLinks.DataBind();
        LoadLegend();

        if (PatientManager.DateRangeApplied)
        {
            pnlLegend.Visible = false;
        }
        else
        {
            pnlLegend.Visible = true;
        }
    }

    private void BuildRDLinkButtons(DataTable dtSortedRDList)
    {
        #region Old
        //Table tblMain = new Table();
        //tblMain.ID = "tblMain";
        //tblMain.Width = Unit.Percentage(100);
        //tblMain.CellPadding = 1;
        //TableRow tblRowNew = new TableRow();
        //int RowWidth, CellWidth;
        //int TotalRow = dtSortedRDList.Rows.Count;
        //TableRow tblRowCurrent = tblRowNew;
        //if (TotalRow > 3)
        //{
        //    RowWidth = 100;
        //    CellWidth = 25;
        //}
        //else if (TotalRow == 1)
        //{
        //    tblMain.Width = Unit.Percentage(25);
        //    RowWidth = 50;
        //    CellWidth = 25;
        //}
        //else
        //{
        //    tblMain.Width = Unit.Percentage(75);
        //    RowWidth = 75;
        //    CellWidth = 25; // (int)((100 - (100 % TotalRow)) / TotalRow);
        //}
        //for (int i = 0; i < TotalRow; i++)
        //{
        //    tblRowCurrent.Width = Unit.Percentage(RowWidth);
        //    if ((i % 4) == 0)
        //    {
        //        //Create the new table Row
        //        tblRowNew = new TableRow();
        //        tblRowNew.Width = Unit.Percentage(RowWidth);
        //        tblRowCurrent = tblRowNew;
        //        //Create the table Cell to put the item
        //        TableCell tblCell = new TableCell();
        //        tblCell.Width = Unit.Percentage(CellWidth);
        //        //Now add the item in the table cell
        //        AddLinkButton(dtSortedRDList.Rows[i], ref tblCell);
        //        //Add the Cell to the Row
        //        tblRowCurrent.Cells.Add(tblCell);
        //        //Add the row to the MainTable
        //        tblMain.Rows.Add(tblRowCurrent);
        //    }
        //    else
        //    {
        //        //Simply create another cell and add the link button
        //        TableCell tblCell = new TableCell();
        //        tblCell.Width = Unit.Percentage(CellWidth);
        //        //Now add the item in the table cell
        //        AddLinkButton(dtSortedRDList.Rows[i], ref tblCell);
        //        //Add the Cell to the Row
        //        tblRowCurrent.Cells.Add(tblCell);
        //    }
        //}
        ////Now Add the table in the place Holder
        //this.phRD.Controls.Add(tblMain); 
        #endregion

        ColorButtonContainer cont = new ColorButtonContainer();
        cont.MaxColumn = 5;
        for (int i = 0; i < dtSortedRDList.Rows.Count; i++)
        {
            int discode = Int32.Parse(dtSortedRDList.Rows[i]["DISCODE"].ToString().Trim());
            string maxDate = PatientManager.GetMaxDate(discode, patientKey);
            if (maxDate.Trim().Equals("")) maxDate = "01-JAN-1996";
            ColorButton btn = new ColorButton();
            btn.ID = dtSortedRDList.Rows[i]["Detail"].ToString() + "Button";
            btn.Text = dtSortedRDList.Rows[i]["Detail"].ToString();
            // if (i == 3 || i == 1 || i == 9) btn.Animate = true;
            if (!PatientManager.DateRangeApplied)
                btn.ButtonColor = PatientManager.GetRowColorBasedOnDate(DateTime.Parse(maxDate.Trim()).ToString("MM-dd-yyyy"));
            else btn.ButtonColor = Color.LightGray;

            //By: Animesh
            //string strHRef = "RD.aspx?PatientKey=" + patientKey + "&DisCode=" + dtSortedRDList.Rows[i]["DisCode"].ToString() + "&DisType=" + dtSortedRDList.Rows[i]["DisciplineType"].ToString() + "&DisName=" + dtSortedRDList.Rows[i]["Detail"].ToString();
            string strHRef = "RDNew.aspx?PatientKey=" + patientKey + "&DisCode=" + dtSortedRDList.Rows[i]["DisCode"].ToString() + "&DisType=" + dtSortedRDList.Rows[i]["DisciplineType"].ToString() + "&DisName=" + dtSortedRDList.Rows[i]["Detail"].ToString();

            //Now add the strFilter(if not empty) to the querystring
            if (!string.IsNullOrEmpty(strFilter))
            {
                strHRef += "&CodeType=" + strFilter.Replace("'", "@"); //RD.aspx Page will forward this CodeType filter to ucDiagnosisMain.ascx for filtering data
            }

            btn.ClientScript = string.Format("SetVisited('{0}'); return ShowRemarkableDiscipline('{1}')", dtSortedRDList.Rows[i]["Detail"].ToString(), strHRef);
            cont.AddButton(btn);
        }

        Label lblButtons = new Label();
        lblButtons.Text = cont.GetHTMLWithScript();
        this.phRD.Controls.Add(lblButtons);
    }

    private void BuildURDLinkButtons(DataTable dtSortedURDList)
    {
        ColorButtonContainer cont = new ColorButtonContainer();
        cont.MaxColumn = 5;
        for (int i = 0; i < dtSortedURDList.Rows.Count; i++)
        {
            int discode = Int32.Parse(dtSortedURDList.Rows[i]["DISCODE"].ToString().Trim());
            string maxDate = PatientManager.GetMaxDate(discode, patientKey);
            if (maxDate.Trim().Equals("")) maxDate = "01-JAN-1996";
            ColorButton btn = new ColorButton();
            btn.ID = dtSortedURDList.Rows[i]["Detail"].ToString() + "URDButton";
            btn.Text = dtSortedURDList.Rows[i]["Detail"].ToString();
            // if (i == 3 || i == 1 || i == 9) btn.Animate = true;
            if (!PatientManager.DateRangeApplied)
                btn.ButtonColor = PatientManager.GetRowColorBasedOnDate(DateTime.Parse(maxDate.Trim()).ToString("MM-dd-yyyy"));
            else btn.ButtonColor = Color.LightGray;

            //By: Animesh
            //string strHRef = "RD.aspx?PatientKey=" + patientKey + "&DisCode=" + dtSortedRDList.Rows[i]["DisCode"].ToString() + "&DisType=" + dtSortedRDList.Rows[i]["DisciplineType"].ToString() + "&DisName=" + dtSortedRDList.Rows[i]["Detail"].ToString();
            string strHRef = "RDNew.aspx?PatientKey=" + patientKey + "&DisCode=" + dtSortedURDList.Rows[i]["DisCode"].ToString() + "&DisType=" + dtSortedURDList.Rows[i]["DisciplineType"].ToString() + "&DisName=" + dtSortedURDList.Rows[i]["Detail"].ToString();

            //Now add the strFilter(if not empty) to the querystring
            if (!string.IsNullOrEmpty(strFilter))
            {
                strHRef += "&CodeType=" + strFilter.Replace("'", "@"); //RD.aspx Page will forward this CodeType filter to ucDiagnosisMain.ascx for filtering data
            }

            btn.ClientScript = string.Format("SetVisited('{0}'); return ShowRemarkableDiscipline('{1}')", dtSortedURDList.Rows[i]["Detail"].ToString(), strHRef);
            cont.AddButton(btn);
        }

        Label lblUButtons = new Label();
        lblUButtons.Text = cont.GetHTMLWithScript();
        this.phURD.Controls.Add(lblUButtons);
    }
    //Prepare the filter string based on CheckBoxes selected...
    private string LoadFilters()
    {
        const string FILTERORGINAL = "CodeType=";
        const string OR = " OR ";
        StringBuilder sbFilter = null;
        int checkedCOunt = 0;
        if (cblCodeOption != null)
        {
            for (int i = 0; i < cblCodeOption.Items.Count; i++)
            {
                if (cblCodeOption.Items[i].Selected)
                {
                    if (checkedCOunt == 0)
                    {
                        sbFilter = new StringBuilder(FILTERORGINAL);
                        sbFilter.Append("'");
                        sbFilter.Append(cblCodeOption.Items[i].Value.Trim());
                        sbFilter.Append("'");
                        checkedCOunt++;
                    }
                    else
                    {
                        sbFilter.Append(OR);
                        sbFilter.Append(FILTERORGINAL);
                        sbFilter.Append("'");
                        sbFilter.Append(cblCodeOption.Items[i].Value.Trim());
                        sbFilter.Append("'");
                        checkedCOunt++;
                    }

                }
            }
            checkedCOunt = 0;
        }
        if (sbFilter != null)
        {
            return sbFilter.ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    private string MakeURDHeader()
    {
        const string FILTERORGINAL = "Encounter(s) not shown due to applied filters in the following discipline(s) (";
        StringBuilder sbURDHeader = null;
        int checkedCOunt = 0;
        if (cblCodeOption != null)
        {
            for (int i = 0; i < cblCodeOption.Items.Count; i++)
            {
                if (cblCodeOption.Items[i].Selected)
                {
                    if (checkedCOunt == 0)
                    {
                        sbURDHeader = new StringBuilder(FILTERORGINAL);
                        sbURDHeader.Append(cblCodeOption.Items[i].Text.Trim());
                        checkedCOunt++;
                    }
                    else
                    {
                        sbURDHeader.Append(", ");
                        sbURDHeader.Append(cblCodeOption.Items[i].Text.Trim());
                        checkedCOunt++;
                    }
                }
            }

            checkedCOunt = 0;
        }
        if (sbURDHeader != null)
        {
            sbURDHeader.Append(")");
            return sbURDHeader.ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    private string LoadURDFilters()
    {
        const string FILTERORGINAL = "CodeType<>";
        const string OR = " AND ";
        StringBuilder sbFilter = null;
        int checkedCOunt = 0;
        if (cblCodeOption != null)
        {
            for (int i = 0; i < cblCodeOption.Items.Count; i++)
            {
                if (cblCodeOption.Items[i].Selected)
                {
                    if (checkedCOunt == 0)
                    {

                        sbFilter = new StringBuilder(FILTERORGINAL);
                        sbFilter.Append("'");
                        sbFilter.Append(cblCodeOption.Items[i].Value.Trim());
                        sbFilter.Append("'");
                        checkedCOunt++;
                    }
                    else
                    {

                        sbFilter.Append(OR);
                        sbFilter.Append(FILTERORGINAL);
                        sbFilter.Append("'");
                        sbFilter.Append(cblCodeOption.Items[i].Value.Trim());
                        sbFilter.Append("'");
                        checkedCOunt++;
                    }

                }
            }
            checkedCOunt = 0;
        }
        if (sbFilter != null)
        {
            return sbFilter.ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    private void AddLinkButton(DataRow Row, ref TableCell TD)
    {
        if (Row != null && Row["Detail"] != DBNull.Value || Row["Detail"].ToString() != string.Empty)
        {
            LinkButton editLink = new LinkButton();
            //Set the Attributes
            editLink.ID = Row["Detail"].ToString() + "Button";
            editLink.Font.Underline = false;
            //Set link button properties...
            editLink.Font.Bold = false;
            editLink.Font.Size = FontUnit.Point(10);
            editLink.Font.Names = new string[] { "Arial", "Verdana" };
            editLink.Height = Unit.Pixel(35);
            editLink.Width = Unit.Percentage(90);
            editLink.ForeColor = System.Drawing.Color.Maroon;
            editLink.CssClass = "button";
            //Now set the HREF and Text for this button
            editLink.Text = String.Format("<span>{0}</span>", Row["Detail"].ToString());
            string strHRef = "RD.aspx?PatientKey=" + patientKey + "&DisCode=" + Row["DisCode"].ToString() + "&DisType=" + Row["DisciplineType"].ToString() + "&DisName=" + Row["Detail"].ToString();
            //Now add the strFilter(if not empty) to the querystring
            if (!string.IsNullOrEmpty(strFilter))
            {
                strHRef += "&CodeType=" + strFilter.Replace("'", "@"); //RD.aspx Page will forward this CodeType filter to ucDiagnosisMain.ascx for filtering data
            }
            editLink.Attributes["href"] = strHRef;
            editLink.Attributes["onclick"] = string.Format("return ShowRemarkableDiscipline('{0}')", editLink.Attributes["href"].ToString());
            //Now add the link into the Cell
            TD.Controls.Add(editLink);

        }
    }
    protected void btnApplyCodeFilter_Click(object sender, EventArgs e)
    {
        patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
        //Get the Original DisciplineList
        DataSet dsRD = PatientManager.GetRemarkableDisciplineList(patientKey);

        //Now prepare the filter string based on CheckBoxes selected...

        //string strFilterOriginal = "CodeType=";
        //ArrayList strArrCode = new ArrayList();
        //foreach (ListItem chkCodeItem in cblCodeOption.Items)
        //{
        //    if (chkCodeItem.Selected)
        //    {
        //        strArrCode.Add(chkCodeItem.Value);
        //    }
        //}
        //for (int j = 0; j < strArrCode.Count; j++)
        //{
        //    if (j == 0)
        //        strFilter = strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
        //    else
        //        strFilter += " OR " + strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
        //}

        //Get the Original Table and apply row filter
        DataTable dtRD = dsRD.Tables[0].Copy();
        DataTable dtURD = dsRD.Tables[0].Copy();

        DataView dvwRD = dtRD.DefaultView;
        strFilter = LoadFilters();
        dvwRD.RowFilter = strFilter;
        DataTable dtRDNew = dvwRD.ToTable(true, new string[] { "Detail", "DisCode", "DisciplineType" });
        DataView dvwSortedRDList = new DataView(dtRDNew, "", "Detail", DataViewRowState.CurrentRows);


        //Now bind the new Table to the DataList
        if (dtRDNew != null)
        {
            //Clear the place holder first...
            this.phRD.Controls.Clear();
            BuildRDLinkButtons(dvwSortedRDList.ToTable());
        }

        DataView dvwURD = dtURD.DefaultView;
        strFilter = LoadURDFilters();
        dvwURD.RowFilter = strFilter;
        DataTable dtURDNew = dvwURD.ToTable(true, new string[] { "Detail", "DisCode", "DisciplineType" });
        DataView dvwSortedURDList = new DataView(dtURDNew, "", "Detail", DataViewRowState.CurrentRows);


        //Now bind the new Table to the DataList
        if (dtURDNew != null)
        {
            litURDHeader.Text = "";
            litURDHeader.Text = MakeURDHeader();
            //Clear the place holder first...
            this.phURD.Controls.Clear();
            BuildURDLinkButtons(dvwSortedURDList.ToTable());
        }
        else
        {
            litURDHeader.Text = string.Empty;
        }



        string selectedCodeIndex = "";
        for (int i = 0; i < cblCodeOption.Items.Count; i++)
        {
            if (cblCodeOption.Items[i].Selected)
            {
                selectedCodeIndex += i + ",";
            }
        }
        Session["SelectedCodeIndex"] = selectedCodeIndex;
    }

    private void PrepareLegend(DataSet dsLegend)
    {
        if (dsLegend == null || dsLegend.Tables[0].Rows.Count <= 0)
        {
        }
        else
        {
            #region Previous Code
            ////show legend
            //DateTime firstDate = Convert.ToDateTime(dsLegend.Tables[0].Rows[0]["FIRSTDATE"].ToString());
            //TimeSpan dayDiff = DateTime.Now.Subtract(firstDate);

            //int dayRange1 = dayDiff.Days;
            //int dayRange2 = dayDiff.Days + Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange2"].ToString());
            //int dayRange3 = dayDiff.Days + Convert.ToInt32(dsLegend.Tables[0].Rows[0]["DayRange3"].ToString());


            //#region Option for Color 1
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
            //#endregion

            //#region Option for Color 2
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
            //#endregion

            //#region Option for Color 3
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
            //#endregion
            //lblOption1.Text = "0 to " + lblOption1.Text;
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
    public string GetQueryString()
    {
        return "PatientKey=" + Request.QueryString["PatientKey"];
    }
}
