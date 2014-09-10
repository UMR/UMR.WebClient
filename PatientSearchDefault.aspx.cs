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
using System.Xml;
using System.Collections.Generic;
using DockPreferenceSerializationUtiliy;

public partial class PatientSearchDefault : System.Web.UI.Page
{
    const string INNER_CONTROL_PATH = "~/Oracle/ControlLibrary/ucRDShell.ascx";
    const string ZONE_LEFT = "ZoneRightBottomLeft";
    const string ZONE_RIGHT = "ZoneRightBottomRight";
    string userName;

    protected override void OnPreInit(EventArgs e)
    {
        userName = User.Identity.Name;
        //**********************************************************************************************
        //** 01/03/08 ==>   Suppressing this feature for now... 
        //                  erroneously popping up even when options r there...
        //**********************************************************************************************
        //if (!IsPostBack)
        //{
        //    DataTable dtSettings;

        //if (PatientManager.IsFirstVisit(userName, out dtSettings) == true)
        //{
        //    //Show the Preferece settings window
        //    //Get the choices from the user and persist
        //    RadWindow rw = GetRadWindow();
        //    WMDefault.Windows.Add(rw);
        //}
        //else
        //{
        //    // This means, prefereces have been saved for this user already...
        //    // Do Nothing The Result Page will do the processing with the existing Settings...
        //}
        //}
    }

    protected override void OnLoad(EventArgs e)
    {
        if (!IsPostBack)
        {
            //Load the Modifier Combo
            DataTable dtModifier = PatientManager.GetAllPatientModifiers();
            cmbModifier.DataValueField = "ModifierCode";
            cmbModifier.DataTextField = "RelationShip";
            cmbModifier.DataSource = dtModifier;
            cmbModifier.DataBind();

            //cmbModifier.SelectedIndex = -1;
        }
        //base.OnLoad(e);
    }

    private RadWindow GetRadWindow()
    {
        RadWindow rw = new RadWindow();
        //rw.Behavior = RadWindowBehaviorFlags.Pin | RadWindowBehaviorFlags.Minimize | RadWindowBehaviorFlags.Maximize;
        //rw.VisibleTitlebar = false;
        rw.Modal = true;
        rw.ShowContentDuringLoad = true;
        rw.NavigateUrl = "~/RDOptionPage.aspx";
        rw.VisibleOnPageLoad = true;
        return rw;
    }

    protected void RadAjaxManagerDefault_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        //This is where we will receive the input from the RDOptionAll Window
        // We will parse the Argument and get the name of the RDs that the user prefers
        // We will persist the Names in the depository (XML File for now || DB later)...

        //Get the values passed in the Arg
        string[] opName = e.Argument.Split(new char[] { ';' });
        if (opName[0] == "DisciplineList")
        {
            DockPreferenceSerializationManager mgrDPS = new DockPreferenceSerializationManager();
            userName = User.Identity.Name;
            if (mgrDPS.SaveUserDockStatePreference(userName, opName))
            {
                RadAjaxManagerDefault.ResponseScripts.Add(string.Format("alert('Your Preference Has Been Saved Successfully.');"));
            }
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            this.ucSearchGrid.PatientID = txtID.Text;
            this.ucSearchGrid.Modifier = cmbModifier.SelectedValue;
            this.ucSearchGrid.FirstName = txtFirstName.Text;
            this.ucSearchGrid.LastName = txtLastName.Text;
            this.ucSearchGrid.DOB = txtDOB.Text.Trim().Length == 0 ? DateTime.MinValue : Convert.ToDateTime(txtDOB.Text.Trim());

            if (txtDOB.Text.Trim().Length > 0)
            {
                DateTime dt = DateTime.Parse(txtDOB.Text.Trim());
                lblInvalidDate.Visible = false;
            }
        }
        catch
        {
            lblInvalidDate.Visible = true;
            return;
        }

        if (txtDOB.Text.Trim() == String.Empty && txtFirstName.Text.Trim() == String.Empty &&
                txtID.Text.Trim() == String.Empty && txtLastName.Text.Trim() == string.Empty &&
                cmbModifier.Text.Trim() == String.Empty)
        {
            ucSearchGrid.Grid.DataSource=String.Empty;
            ucSearchGrid.Grid.MasterTableView.VirtualItemCount = 0;
        }
        this.ucSearchGrid.Grid.Rebind();
    }

    protected void cmbModifier_DataBound(object sender, EventArgs e)
    {
        cmbModifier.Items.Insert(0, new ListItem("", ""));
    }

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session.Abandon();
        FormsAuthentication.RedirectToLoginPage();
    }
    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        System.Data.DataTable dtLastAccessInfo = Session["LAST_ACCESS_INFO"] as System.Data.DataTable;
        if (dtLastAccessInfo != null)
        {
            if (dtLastAccessInfo.Rows.Count > 0)
            {
                for (int i = 0; i < dtLastAccessInfo.Rows.Count; i++)
                {
                    // update the RecordUpdateField of  UMR_PATIENT_ACCESS  table
                    DateTime lastAccessTime = DateTime.Parse(dtLastAccessInfo.Rows[i]["LastAccessTime"].ToString());
                    long patientKey = Int64.Parse(dtLastAccessInfo.Rows[i]["PatientKey"].ToString().Trim());
                    string userid = dtLastAccessInfo.Rows[i]["UserAccessed"].ToString().Trim();
                    
                    System.Data.DataTable dtUserInfo = PatientManager.GetUserInfo(userid);
                    if (dtUserInfo.Rows[0]["Industry"].ToString().Trim() == "Healthcare")
                    {
                        PatientManager.UpdatePatientAccessRecordUpdateTime(patientKey, userid, lastAccessTime);
                    } 
                }
            }
        }
    }
}
