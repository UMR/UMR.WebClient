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

public partial class SecuredPages_Default : System.Web.UI.Page
{
    //const string INNER_CONTROL_PATH = "~/Oracle/ControlLibrary/ucRDShell.ascx";
    //const string ZONE_LEFT = "ZoneRightBottomLeft";
    //const string ZONE_RIGHT = "ZoneRightBottomRight";
    //string userName;

    //protected override void OnPreInit(EventArgs e)
    //{
    //    userName = User.Identity.Name;
    //    if (!IsPostBack)
    //    {
    //        DataTable dtSettings;
    //        if (PatientManager.IsFirstVisit(userName, out dtSettings) == true)
    //        {
    //            //Show the Preferece settings window
    //            //Get the choices from the user and persist
    //            RadWindow rw = GetRadWindow();
    //            WMDefault.Windows.Add(rw);
    //        }
    //        else
    //        {
    //            // This means, prefereces have been saved for this user already...
    //            // Do Nothing The Result Page will do the processing with the existing Settings...
    //        }
    //    }
    //}

    //protected override void OnLoad(EventArgs e)
    //{
    //    if (!IsPostBack)
    //    {
    //        //Load the Modifier Combo
    //        DataTable dtModifier = PatientManager.GetAllPatientModifiers();
    //        cmbModifier.DataValueField = "ModifierCode";
    //        cmbModifier.DataTextField = "RelationShip";
    //        cmbModifier.DataSource = dtModifier;
    //        cmbModifier.DataBind();

    //        //cmbModifier.SelectedIndex = -1;
    //    }
    //    //base.OnLoad(e);
    //}

    //private RadWindow GetRadWindow()
    //{
    //    RadWindow rw = new RadWindow();
    //    rw.Behavior = RadWindowBehaviorFlags.Pin | RadWindowBehaviorFlags.Minimize | RadWindowBehaviorFlags.Maximize;
    //    //rw.VisibleTitlebar = false;
    //    rw.Modal = true;
    //    rw.ShowContentDuringLoad = true;
    //    rw.NavigateUrl = "~/RDOptionPage.aspx";
    //    rw.VisibleOnPageLoad = true;
    //    return rw;
    //}

    //protected void RadAjaxManagerDefault_AjaxRequest(object sender, AjaxRequestEventArgs e)
    //{
    //    //This is where we will receive the input from the RDOptionAll Window
    //    // We will parse the Argument and get the name of the RDs that the user prefers
    //    // We will persist the Names in the depository (XML File for now || DB later)...

    //    //Get the values passed in the Arg
    //    string[] opName = e.Argument.Split(new char[] { ';' });
    //    if (opName[0] == "DisciplineList")
    //    {
    //        DockPreferenceSerializationManager mgrDPS = new DockPreferenceSerializationManager();
    //        userName = User.Identity.Name;
    //        if (mgrDPS.SaveUserDockStatePreference(userName, opName))
    //        {
    //            RadAjaxManagerDefault.ResponseScripts.Add(string.Format("alert('Your Preference Has Been Saved Successfully.');"));
    //        }
    //    }

    //}

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    this.ucSearchGrid.Grid.DataBind();
    //}

    //protected void cmbModifier_DataBound(object sender, EventArgs e)
    //{
    //    cmbModifier.Items.Insert(0, new ListItem("", ""));
    //}

    //protected void btnLogOut_Click(object sender, EventArgs e)
    //{
    //    FormsAuthentication.SignOut();
    //    FormsAuthentication.RedirectToLoginPage();
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.Name.ToLower().Trim().Equals("test"))
        {
            linkConfig.Visible = true;
        }
        else
        {
            linkConfig.Visible = false;
        }
        if (User.Identity.Name.ToLower().Trim().Equals("test") || User.Identity.Name.ToLower().Trim().Equals("scohn"))
        {
            linkUsages.Visible = true;
        }
        else
        {
            linkUsages.Visible = false;
        }

        if (linkConfig.Visible == false && linkUsages.Visible == false)
        {
            Response.Redirect(linkSearch.NavigateUrl);
        }
    }
}

