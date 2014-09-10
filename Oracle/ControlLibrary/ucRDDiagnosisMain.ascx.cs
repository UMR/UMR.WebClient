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
using System.Text;

public partial class Oracle_ControlLibrary_ucRDDiagnosisMain : System.Web.UI.UserControl
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

    public long PaientKey
    {
        get { return patientKey; }
        set { patientKey = value; }
    }

    #endregion
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
        if (!IsPostBack)
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
    public void RefreshPage(string selectedColor)
    {
        SelectedColor = selectedColor;

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

    //protected void RadGridRDDiagnosisMain_ItemCreated(object sender, Telerik.WebControls.GridItemEventArgs e)
    //{
    //    if (e.Item is GridDataItem)
    //    {
    //        HyperLink editLink = e.Item.FindControl("Details") as HyperLink;
    //        if (editLink != null)
    //        {
    //            editLink.Attributes["href"] = "#";
    //        }
    //        DataRowView drv;
    //        if (e.Item.DataItem != null)
    //        {

    //            drv = e.Item.DataItem as DataRowView;
    //            if (drv != null)
    //            {
    //                //string s = "<font color=" + "'Red'" + ">!</font>";
    //                //editLink.Text = s + drv["DETAIL"].ToString(); 
    //                editLink.Text = drv["DETAIL"].ToString();   // e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["DETAIL"].ToString();
    //            }
    //            //editLink.Attributes["onclick"] = string.Format("return ShowRemarkableDisciplineDrillDown('{0}','{1}','{2}','{3}','{4}','{5}','{6}');",
    //            //                                                            new object[] { id, modifierID, drv["CODE_TYPE"], drv["MEDCODE"], drv["CODE_VERSION"], drv["DISCODE"], drv["DISCIPLINETYPE"] });
    //            string discType = drv["DisciplineType"].ToString();
    //            editLink.Attributes["onclick"] = string.Format("return ShowRemarkableDisciplineDrillDown('{0}','{1}','{2}','{3}');",
    //                                                                        new object[] { id, modifierID, drv["MedCode"], drv["DisciplineType"] });
    //            editLink.Attributes["onMouseOver"] = string.Format("return HideStatus()");
    //        }
    //    }

    //}
    //protected void RadGridRDDiagnosisMain_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    //{
    //    //RadGridRDDiagnosisMain.MasterTableView.DataSource = PatientManager.GetRDDiagnosis(id, modifierID, codeType, medCode, Convert.ToDecimal(codeVersion), Convert.ToDecimal(disCode));
    //    RadGridRDDiagnosisMain.MasterTableView.DataSource = PatientManager.GetRDDiagnosis(id, modifierID, Convert.ToDecimal(disCode));
    //}


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

                    string strHRef = "RDDetails.aspx?PatientKey=" + patientKey + "&medCode=" + rowView.Row["MedCode"] + "&disType=" + rowView.Row["DisciplineType"] + "&name=" + rowView.Row["DETAIL"];
                    //editLink.Attributes["href"] = "#";
                    //**********************************************

                    //editLink.Attributes["onclick"] = string.Format("return ShowRemarkableDisciplineDrillDown('{0}','{1}','{2}','{3}');",
                    //                                                            new object[] { id, modifierID, rowView.Row["MedCode"], rowView.Row["DisciplineType"] });

                    //********************** Changed the previous(Original) line to following for testing... 
                    //editLink.Attributes["onclick"] = string.Format("return ShowRemarkableDiscipline('{0}')", editLink.Attributes["href"].ToString());


                    //***************************************************************************************
                    // This is where we will show the description in the pop up ...
                    string strDesc = rowView.Row["CodeDescription"].ToString().Replace("__", ", "); // Replace the "__" with ", " --> Requested by Steven on April 29th, 2008
                    // Some strings have Single quotes in them, take care of that so we don't get JS error
                    strDesc = strDesc.Replace("'", "\\'");
                    strHRef += "&desc=" + strDesc;
                    editLink.Attributes["ondblclick"] = string.Format("return ShowRemarkableDiscipline('{0}')", strHRef);

                    DataSet dsRMDisc = PatientManager.GetRemarkableDisciplineListForMedCode(rowView.Row["MedCode"].ToString(), disCode);
                    string rmDiscLnk = string.Empty;
                    string showAllRelatedDispStr = string.Empty;
                    string relatedDisList = string.Empty;
                    if (dsRMDisc != null)
                    {
                        relatedDisList = GetRemarkableDiscList(dsRMDisc);
                        rmDiscLnk = GetRemarkableDiscLink(dsRMDisc);
                        showAllRelatedDispStr = GetAllRelatedDisiplineButtonHtml(dsRMDisc, disCode);
                    }
                    editLink.Attributes["title"] = strDesc;
                    editLink.Attributes["class"] = "tooltip";

                    string linkNum = (i + 2).ToString();
                    if ((i + 2) < 10) linkNum = "0" + linkNum;
                    string linkClientId = "ctl01_RadGridRDDiagnosisMain_ctl" + linkNum + "_Details";

                    //Now set the attribute
                    editLink.Attributes["onClick"] = string.Format("return ShowDescription('{0}','{1}','{2}','{3}','{4}')", strDesc, rmDiscLnk, showAllRelatedDispStr, linkClientId, relatedDisList);

                    //lnkAllRelatedDiscipline.Text = "Show All";
                    //lnkAllRelatedDiscipline.Attributes["onClick"] = string.Format("return ShowAllRelatedDiscipline({0})", dsRMDisc);
                }
                //editLink.Attributes["onMouseOver"] = string.Format("return HideStatus()");

                //string strDesc = "Esophagoscopy, rigid or flexible; diagnostic, with or without collection of specimen(s) by brushing or washing (separate procedure)";

            }
        }
    }


    private string GetAllRelatedDisiplineButtonHtml(DataSet dsRMDisc, string disCode)
    {
        int rowCount = dsRMDisc.Tables[0].Rows.Count;
        if (rowCount <= 0) return "";

        string discodes = disCode + ",";
        string disNames = "";
        for (int i = 0; i < dsRMDisc.Tables[0].Rows.Count; i++)
        {
            discodes += dsRMDisc.Tables[0].Rows[i]["DisCode"].ToString() + ",";
            disNames += dsRMDisc.Tables[0].Rows[i]["Detail"].ToString() + ",";
        }
        discodes = discodes.Substring(0, discodes.Length - 1);
        if (disNames.Trim().Length > 0)
        {
            disNames = disNames.Trim();
            disNames = disNames.Substring(0, disNames.Length - 1);
        }
        StringBuilder sb = new StringBuilder();
        string linkText = "Show all related disciplines";

        //By: Animesh
        //string linkUrl = "TiledRD.aspx?discodes=" + discodes + "&PatientKey=" + patientKey ;
        string linkUrl = string.Format("DockedRD.aspx?discodes={0}&PatientKey={1}&ClickedRD={2}", discodes, patientKey, disCode);


        string onclickStr = string.Format("AddToVisitedList(&quot;{0}&quot;);  return ShowAllRelatedDiscipline(&quot;{1}&quot;);", disNames, linkUrl);
        string lnkButton = string.Format("<a href=\"{0}\" onclick=\"{1}\" >{2}</a>", "#", onclickStr, linkText);
        sb.Append(lnkButton);

        return sb.ToString();
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

    private string GetRemarkableDiscLink(DataSet dsRMDisc)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<ul>");
        if (dsRMDisc.Tables[0] != null)
        {
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
                else
                {
                    openedDiscipline += Request.QueryString["DisCode"] + "," + dsRMDisc.Tables[0].Rows[i]["DisCode"].ToString();
                }
                string linkText = dsRMDisc.Tables[0].Rows[i]["DETAIL"].ToString();
                string linkUrl = "RD.aspx?PatientKey=" + patientKey + "&DisCode=" + dsRMDisc.Tables[0].Rows[i]["DisCode"].ToString() + "&DisType=D&DisName=" + linkText + "&OpenedDiscipline=" + openedDiscipline;
                string onclickStr = string.Format("return ShowRemarkableDiscipline(&quot;{0}&quot;,&quot;{1}&quot;);", linkUrl, linkText);
                string lnkButton = string.Format("<a href=\"{0}\" onclick=\"{1}\" id=\"rd_{2}\" >{3} </a>", "#", onclickStr, linkText, linkText);
                if (alreadOpend == true)
                {
                    lnkButton = string.Format("&nbsp;<span disabled=\"disabled\">" + linkText + "</span>&nbsp;&nbsp;&nbsp;");
                }
                sb.Append("<li>" + lnkButton + "</li>");
            }
        }
        sb.Append("</ul>");

        return sb.ToString();
    }

    protected void RadGridRDDiagnosisMain_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;
        string sortDir = "ASC";
        if (ViewState["SHORTDIRECTION"] != null)
        {
            sortDir = ViewState["SHORTDIRECTION"].ToString();
        }

        if (ViewState["LASTSORTEXPRESSION"] != null)
        {
            if (!ViewState["LASTSORTEXPRESSION"].ToString().Equals(sortExpression))
            {
                sortDir = "ASC";
            }
        }


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

        DataView dtView = new DataView(dtRD);
        dtView.Sort = sortExpression + " " + sortDir;
        RadGridRDDiagnosisMain.DataSource = dtView.ToTable();
        RadGridRDDiagnosisMain.DataBind();

        if (sortDir == "ASC")
        {
            ViewState["SHORTDIRECTION"] = "DESC";
        }
        else
        {
            ViewState["SHORTDIRECTION"] = "ASC";
        }

        ViewState["LASTSORTEXPRESSION"] = sortExpression;
    }
}
