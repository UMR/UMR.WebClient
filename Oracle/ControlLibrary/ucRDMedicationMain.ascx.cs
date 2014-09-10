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


public partial class Oracle_ControlLibrary_ucRDMedicationMain : System.Web.UI.UserControl
{
    private long patientKey;
  
    public long PatientKey
    {
        get { return patientKey; }
        set { patientKey = value; }
    }

    
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
            //RadGridRDMedicationMain.MasterTableView.DataSource = PatientManager.GetRDMedication(id, modifierID);
            DataSet dsRDMed = PatientManager.GetRDMedication(patientKey);
            RadGridRDMedicationMain.DataSource = dsRDMed;
            RadGridRDMedicationMain.DataBind();
        }
    }
    public void RefreshPage(string selectedColor)
    {
        SelectedColor = selectedColor;

        RadGridRDMedicationMain.DataSource = PatientManager.GetRDMedication(patientKey);
        RadGridRDMedicationMain.DataBind();
    }
    //protected void RadGridRDMedicationMain_ItemCreated(object sender, Telerik.WebControls.GridItemEventArgs e)
    //{
    //    if (e.Item is GridDataItem)
    //    {
    //        HyperLink editLink = e.Item.FindControl("Brand") as HyperLink;
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
    //                editLink.Text = drv["BrandName"].ToString();   // e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["DETAIL"].ToString();
    //            }
    //            string disType = drv["DisciplineType"].ToString();
    //            editLink.Attributes["onclick"] = string.Format("return ShowRDMedicationDrillDown('{0}','{1}','{2}')",
    //                                                                        new object[] { id, modifierID, drv["DisciplineType"] });
    //            editLink.Attributes["onMouseOver"] = string.Format("return HideStatus()");
    //        }
    //    }

    //}
    //protected void RadGridRDMedicationMain_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    //{
    //    RadGridRDMedicationMain.MasterTableView.DataSource = PatientManager.GetRDMedication(id, modifierID);
    //}

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
                    if (SelectedColor != null)
                    {
                        string colorHex = System.Drawing.ColorTranslator.ToHtml(e.Row.BackColor);
                        if (SelectedColor.IndexOf(colorHex) < 0) e.Row.BackColor = System.Drawing.Color.Transparent;
                    }


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
                    editLink.Attributes["onclick"] = string.Format("return ShowRDMedicationDrillDown('{0}','{1}','{2}','{3}')",
                                                                               new object[] { patientKey, rowView.Row["DisciplineType"], rowView.Row["BrandName"], rowView.Row["NDCCode"] });
                }
                editLink.Attributes["onMouseOver"] = string.Format("return HideStatus()");
            }
        }
    }


    protected void btnApplyCodeFilter_Click(object sender, EventArgs e)
    {
        if (cblCodeOption.Items[0].Selected == false)
        {
            RadGridRDMedicationMain.Visible = false;
        }
        else
        {
            RadGridRDMedicationMain.Visible = true;
        }
        RadGridRDMedicationMain.DataSource = PatientManager.GetRDMedication(patientKey);
        RadGridRDMedicationMain.DataBind();
    }
    protected void RadGridRDMedicationMain_Sorting(object sender, GridViewSortEventArgs e)
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


        DataSet dsRD = PatientManager.GetRDMedication(patientKey);
        //Create the Source Table... Default to original dsRD table
        DataTable dtRD = dsRD.Tables[0].Copy();
        DataView dtView = new DataView(dtRD);
        dtView.Sort = sortExpression + " " + sortDir;
        RadGridRDMedicationMain.DataSource = dtView.ToTable();
        RadGridRDMedicationMain.DataBind();

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
