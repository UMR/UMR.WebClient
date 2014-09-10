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
using System.Collections.Generic;

public partial class Oracle_PrintPreview : System.Web.UI.Page
{
    const string INNER_CONTROL_PATH = "~/Oracle/ControlLibrary/PrintPreview/ucRDShellPreview.ascx";
    const string ZONE_RD_LEFT = "ZoneRDLeft";
    const string ZONE_RD_MIDDLE = "ZoneRDMiddle";
    const string ZONE_RD_RIGHT = "ZoneRDRight";

    const string INNER_MDSCONTROL_PATH = "~/Oracle/ControlLibrary/PrintPreview/ucMDSPreview.ascx";
    const string ZONE_MDS = "ZoneMDS";
    const string MDS = "MDS";

    long patientKey;
    private string strFilter = "";
    private Oracle_ControlLibrary_PrintPreview_ucRDQuickLinksButtonsPreview ucRDQuickLinksButtons;
    string lastMPIDate, lastAccessDate, lastPMDate;

    protected override void OnInit(EventArgs e)
    {
        //id = Request.QueryString["ID"].ToString();
        //modifierID = Request.QueryString["ModifierID"].ToString();

        ////Get The Patient Demographics Data

        ////Get the Remarkable Discipline List
        //DataSet dsRD = PatientManager.GetRemarkableDisciplineList(id, modifierID);
        //DataTable dtRDDistinct = dsRD.Tables[0].DefaultView.ToTable(true, new string[] { "Detail", "DisCode", "DisciplineType" });
        //DataView dvwSortedRDList = new DataView(dtRDDistinct, "", "Detail", DataViewRowState.CurrentRows);
        //if (dvwSortedRDList != null)
        //{
        //    CreateRDObjects(dvwSortedRDList.ToTable());
        //}


        ////if(dsRD != null)
        ////{
        ////    CreateRDObjects(dsRD);
        ////}
        //base.OnInit(e);
        ucRDQuickLinksButtons = (Oracle_ControlLibrary_PrintPreview_ucRDQuickLinksButtonsPreview)(ZoneQL.FindControl("RadDockableObject2").FindControl("ZoneRDQL").FindControl("RadDockableObject1").FindControl("RDQLinks"));
        ucRDQuickLinksButtons.CodeTypeChanged = new EventHandler(ucRDQuickLinksButtons_CodeTypeChanged);
    }

    protected void ucRDQuickLinksButtons_CodeTypeChanged(object sender, EventArgs e)
    {
        LoadPreviewObjects();
        LoadPreviewMDSObjects();
    }
    private void LoadPreviewObjects()
    {
        patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());

        bool fromQueryString = true;
        if (ViewState["FromQueryString"] != null)
        {
            fromQueryString = Boolean.Parse(ViewState["FromQueryString"].ToString());
        }

        CheckBoxList cblCodeOption = (CheckBoxList)(ZoneQL.FindControl("RadDockableObject2").FindControl("ZoneRDQL").FindControl("RadDockableObject1").FindControl("RDQLinks").FindControl("cblCodeOption"));

        //Get The Patient Demographics Data

        //Get the Remarkable Discipline List
        DataSet dsRD = PatientManager.GetRemarkableDisciplineList(patientKey);

        string strFilterOriginal = "CodeType=";
        ArrayList strArrCode = new ArrayList();
        if (fromQueryString == false)
        {
            foreach (ListItem chkCodeItem in cblCodeOption.Items)
            {
                if (chkCodeItem.Selected)
                {
                    strArrCode.Add(chkCodeItem.Value);
                }
            }
            ViewState["FromQueryString"] = false;
        }
        else
        {
            string selectedCodeIndex = string.Empty;

            foreach (ListItem chkCodeItem in cblCodeOption.Items)
            {
                if (chkCodeItem.Selected)
                {
                    chkCodeItem.Selected = false;
                }
            }

            if (Request.QueryString["SelectedCodeIndex"] != null)
            {
                selectedCodeIndex = Request.QueryString["SelectedCodeIndex"];
            }
            if (!selectedCodeIndex.Equals(string.Empty))
            {
                string[] selIndexStr = selectedCodeIndex.Split(new char[] { ',' });
                for (int i = 0; i < selIndexStr.Length; i++)
                {
                    int index = Int32.Parse(selIndexStr[i].Trim());
                    strArrCode.Add(cblCodeOption.Items[index].Value);
                }
            }
            ViewState["FromQueryString"] = false;
        }
        for (int j = 0; j < strArrCode.Count; j++)
        {
            if (j == 0)
                strFilter = strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
            else
                strFilter += " OR " + strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
        }
        
        Session["TypeFilter"]=strFilter;
        
        //Get the Original Table and apply row filter
        DataTable dtRD = dsRD.Tables[0].Copy();
        DataView dvwRD = dtRD.DefaultView;
        dvwRD.RowFilter = strFilter;
        //DataTable dtRDDistinct = dvwRD.ToTable(true, new string[] { "Detail", "DisCode", "DisciplineType" });
        DataTable dtRDDistinct = dvwRD.ToTable(true);

        DataView dvwSortedRDList = new DataView(dtRDDistinct, "", "Detail", DataViewRowState.CurrentRows);
        if (dvwSortedRDList != null)
        {
            CreateRDObjects(dvwSortedRDList.ToTable());
        }


    }

    private void CreateRDObjects(DataTable dtRD)
    {
        if (dtRD.Rows.Count > 0)
        {
            RadDockingZone zone = (RadDockingZone)Page.FindControl(ZONE_RD_LEFT);
            zone.Height = Unit.Pixel(20);

            //string ParentZoneID;
            //string DockName;
            DataRowCollection DataRows = dtRD.Rows;

            #region SetFilter
            string strFilterOriginal = "CodeType=";
            ArrayList strArrCode = new ArrayList();

            CheckBoxList cblCodeOption = (CheckBoxList)(ZoneQL.FindControl("RadDockableObject2").FindControl("ZoneRDQL").FindControl("RadDockableObject1").FindControl("RDQLinks").FindControl("cblCodeOption"));
            foreach (ListItem chkCodeItem in cblCodeOption.Items)
            {
                if (chkCodeItem.Selected)
                {
                    strArrCode.Add(chkCodeItem.Value);
                }
            }
            for (int j = 0; j < strArrCode.Count; j++)
            {
                if (j == 0)
                    strFilter = strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
                else
                    strFilter += " OR " + strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
            }

            Session["TypeFilter"] = strFilter; 
            #endregion

            for (int i = 0; i < DataRows.Count; i++)
            {
                ////First Decide which Zone should host it...
                //if ((i % 2) == 0)
                //{
                //    ParentZoneID = ZONE_RD_LEFT;
                //}
                //else
                //{
                //    ParentZoneID = ZONE_RD_RIGHT;
                //}
                //Now they want everything docked on a single line one after another...
                //Now call the CreateDock Method
                CreateDockObjects(DataRows[i][0].ToString(), ZONE_RD_LEFT, INNER_CONTROL_PATH);
            }
        }
    }

    private void CreateDockObjects(string Name, string ParentZoneId, string InnerControlPath)
    {
        #region Set FIlter
        string strFilterOriginal = "CodeType=";
        ArrayList strArrCode = new ArrayList();

        CheckBoxList cblCodeOption = (CheckBoxList)(ZoneQL.FindControl("RadDockableObject2").FindControl("ZoneRDQL").FindControl("RadDockableObject1").FindControl("RDQLinks").FindControl("cblCodeOption"));
        foreach (ListItem chkCodeItem in cblCodeOption.Items)
        {
            if (chkCodeItem.Selected)
            {
                strArrCode.Add(chkCodeItem.Value);
            }
        }
        for (int j = 0; j < strArrCode.Count; j++)
        {
            if (j == 0)
                strFilter = strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
            else
                strFilter += " OR " + strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
        }

        Session["TypeFilter"] = strFilter; 
        #endregion

        RadDockableObject dock = new RadDockableObject();
        //dock.ID = DateTime.Now.Ticks.ToString();
        dock.Text = Name;     //added this to show the part header... P
        dock.Behavior = RadDockableObjectBehaviorFlags.Collapse;
        dock.Width = Unit.Percentage(100);
        dock.AllowedDockingZoneTypes = RadDockingZoneTypeFlags.Custom;
        dock.AllowedDockingZones = new string[] { ZONE_RD_LEFT, ZONE_RD_MIDDLE, ZONE_RD_RIGHT };
        //dock.Command += new EventHandler(DockableObject_Command);
        dock.DockingMode = RadDockingModeFlags.AlwaysDock;
        //Set the height explicitly so empty white spaces wont show up for smaller disciplines...
        dock.Height = Unit.Pixel(20);
        //Load the ContentTemplate for the RadDock
        ITemplate innerControl = LoadTemplate(InnerControlPath);
        dock.ContentTemplate = innerControl;
        //Add the dock in the Zone
        RadDockingZone zone = (RadDockingZone)Page.FindControl(ParentZoneId);
        zone.Controls.Add(dock);
    }


    private void LoadPreviewMDSObjects()
    {
        patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
        DataTable dtMDSDistinctSvcDate = PatientManager.GetDistinctSpecificServiceDate(patientKey, MDS);

        if (dtMDSDistinctSvcDate != null)
        {
            CreateRDObjects(dtMDSDistinctSvcDate, ZONE_MDS, INNER_MDSCONTROL_PATH);
        }
    }

    private void CreateRDObjects(DataTable dtRD, string zoneName, string innerControlPath)
    {
        if (dtRD.Rows.Count > 0)
        {
            RadDockingZone zone = (RadDockingZone)Page.FindControl(zoneName);
            zone.Height = Unit.Pixel(20);

            DataRowCollection DataRows = dtRD.Rows;
            for (int i = 0; i < DataRows.Count; i++)
            {
                CreateMDSDockObjects(DataRows[i][0].ToString(), zoneName, innerControlPath);
            }
        }
    }

    private void CreateMDSDockObjects(string Name, string ParentZoneId, string InnerControlPath)
    {
        RadDockableObject dock = new RadDockableObject();
        //dock.ID = DateTime.Now.Ticks.ToString();
        dock.Text = Name;     //added this to show the part header... P
        dock.Behavior = RadDockableObjectBehaviorFlags.Collapse;
        dock.Width = Unit.Percentage(100);
        dock.AllowedDockingZoneTypes = RadDockingZoneTypeFlags.Custom;
        dock.AllowedDockingZones = new string[] { ParentZoneId };
        //dock.Command += new EventHandler(DockableObject_Command);
        dock.DockingMode = RadDockingModeFlags.AlwaysDock;
        //Set the height explicitly so empty white spaces wont show up for smaller disciplines...
        dock.Height = Unit.Pixel(20);
        //Load the ContentTemplate for the RadDock
        ITemplate innerControl = LoadTemplate(InnerControlPath);
        dock.ContentTemplate = innerControl;
        //Add the dock in the Zone
        RadDockingZone zone = (RadDockingZone)Page.FindControl(ParentZoneId);
        zone.Controls.Add(dock);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPreviewObjects();

            LoadPreviewMDSObjects();
        }

        
        patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
        
        //First Get the latest dates for different Links... ex. Last MPI Date, Last Accessed Date etc.
        DataSet dsDates = PatientManager.GetLastDates(patientKey);
        if (dsDates != null)
        {
            lastMPIDate = dsDates.Tables[0].Rows[0]["PatientLastExam"].ToString();
            lastAccessDate = dsDates.Tables[0].Rows[0]["PatientLastAccess"].ToString();
            lastPMDate = dsDates.Tables[0].Rows[0]["PatientLastPrescription"].ToString();
        }
        //Now Build the links
        PrepareMiscLinks();

        ////Set the link for the Healthcare Provider Link Button inside ZoneProviderInfo
        //LinkButton lbHPL = this.ZoneProviderInfo.DockableObjects["DockProviderInfo"].FindControl("lbHPL") as LinkButton;
        //if (lbHPL != null)
        //{
        //    lbHPL.Attributes["onclick"] = string.Format("return ShowHPList('{0}','{1}','{2}');", new object[] { id, modifierID, "M" }); // Sending "M" as DispType to get the list of Providers and not the details...
        //}

        // This is for the <TR> </TR> Trial Run... if that doesn't work out, REMOVE THIS...
        LinkButton lbHPL1 = this.ZoneMisc.DockableObjects["DockProvider"].FindControl("lbHPL1") as LinkButton;
        if (lbHPL1 != null)
        {
            lbHPL1.Attributes["onclick"] = string.Format("return ShowHPList('{0}','{1}');", new object[] { patientKey, "M" }); // Sending "M" as DispType to get the list of Providers and not the details...
        }



    }

    private void PrepareMiscLinks()
    {
        //Start with the Master Patient Index
        LinkButton lbMPI = this.ZoneMisc.DockableObjects["DockMisc"].FindControl("lbMPI") as LinkButton;
        if (lbMPI != null)
        {
            lbMPI.Text = "Master Patient Indexes as of Last Exam -- " + lastMPIDate + " EST";
            //lbMPI.Text = "Master Patient Indexes";
            lbMPI.Attributes["onclick"] = string.Format("return ShowMPI('{0}');", new object[] { patientKey });
        }

        //Last Recorded Prescription/Medication Link
        LinkButton lbLRP = this.ZoneMisc.DockableObjects["DockMisc"].FindControl("lbLRP") as LinkButton;
        if (lbLRP != null)
        {
            lbLRP.Text = "Last Ordered Prescription -- " + lastPMDate + " EST";
            //lbLRP.Text = "Last Ordered Prescription";
            lbLRP.Attributes["onclick"] = string.Format("return ShowLRPM('{0}'');", new object[] { patientKey });
        }

        //Last Record Accessed
        LinkButton lbLRA = this.ZoneMisc.DockableObjects["DockMisc"].FindControl("lbLRA") as LinkButton;
        if (lbLRA != null)
        {
            lbLRA.Text = "Last Record Accessed -- " + lastAccessDate + " EST";
            //lbLRA.Text = "Last Record Accessed";
            lbLRA.Attributes["onclick"] = string.Format("return ShowLRA('{0}');", new object[] { patientKey });
        }
    }
}
