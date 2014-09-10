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
using System.Text;
using DockPreferenceSerializationUtiliy;

public partial class SecuredPages_Result : System.Web.UI.Page, ICallbackEventHandler
{

    private string strCallBackReturnMsg = "";

    private DockPreferenceSerializationManager DPSMgr;

    #region Test

    private ArrayList ObjectsState
    {
        get
        {
            ArrayList state = (ArrayList)Session["ObjectsState"];
            if (state == null)
            {
                Session["ObjectsState"] = state = new ArrayList();
            }
            return state;
        }
        set
        {
            Session["ObjectsState"] = value;
        }
    }

    //private string GetStateXmlPath()
    //{
    //    return Server.MapPath("~/Oracle/DockState.xml");
    //}

    private void CreateObjects()
    {
        foreach (DockState state in ObjectsState)
        {
            RadDockableObject dock = CreateDockableObject(state);
        }
    }

    private RadDockableObject CreateDockableObject(DockState state)
    {
        RadDockableObject dock = new RadDockableObject();
        dock.ID = state.Id;
        dock.Text = state.Text;     //added this to show the part header... P
        dock.Behavior = RadDockableObjectBehaviorFlags.Close | RadDockableObjectBehaviorFlags.Collapse;
        dock.Width = Unit.Percentage(100);
        dock.AllowedDockingZoneTypes = RadDockingZoneTypeFlags.Custom;
        dock.AllowedDockingZones = new string[] { DockPreferenceSerializationManager.ZONE_LEFT_NAME, DockPreferenceSerializationManager.ZONE_RIGHT_NAME };
        dock.Command += new EventHandler(DockableObject_Command);
        dock.DockingMode = RadDockingModeFlags.AlwaysDock;
        //***
        dock.Height = Unit.Pixel(20);
        //***

        ITemplate innerControl = LoadTemplate(state.InnerControlPath);
        dock.ContentTemplate = innerControl;

        RadDockingZone zone = (RadDockingZone)Page.FindControl(state.ParentZoneId);
        zone.Controls.Add(dock);

        return dock;
    }

    protected void DockableObject_Command(object sender, EventArgs e)
    {
        RadDockableObject dock = (RadDockableObject)sender;
        DockState state = FindState(dock.ID);

        RadDockableObjectCommandEventArgs args = (RadDockableObjectCommandEventArgs)e;
        switch (args.Command.Name)
        {
            case "Close":
                dock.Closed = true;
                state.Closed = true;
                UpdateState();
                DockPreferenceSerializationManager dsMgr = new DockPreferenceSerializationManager();
                dsMgr.SerializeState(User.Identity.Name);
                break;
        }
    }

    private ArrayList GetZoneState(RadDockingZone zone)
    {
        ArrayList zoneState = new ArrayList();
        foreach (RadDockableObject dock in zone.DockableObjects)
        {
            DockState state = FindState(dock.ID);
            state.ParentZoneId = zone.ID;
            zoneState.Add(state);
        }
        return zoneState;
    }

    private void UpdateState()
    {
        //ArrayList state = new ArrayList();
        //state.AddRange(GetZoneState(this.ZoneRightBottomLeft));
        //state.AddRange(GetZoneState(this.ZoneRightBottomRight));
        //ObjectsState = state;
    }

    private DockState FindState(string id)
    {
        foreach (DockState state in ObjectsState)
        {
            if (state.Id == id)
            {
                return state;
            }
        }
        return null;
    }

    private RadDockableObject FindDockableObject(string id)
    {
        RadDockableObject obj = new RadDockableObject();
        //obj = (RadDockableObject)ZoneRightBottomLeft.FindControl(id);
        //if (obj != null)
        //{
        //    return obj;
        //}
        //obj = (RadDockableObject)ZoneRightBottomRight.FindControl(id);
        //if (obj != null)
        //{
        //    return obj;
        //}
        return obj;
    }

    private void ConfigureObjects()
    {
        foreach (DockState state in ObjectsState)
        {
            RadDockableObject obj = FindDockableObject(state.Id);
            if (obj != null)
            {
                obj.Text = state.Text;
                obj.Closed = state.Closed;
            }
        }
    }

    #endregion

    protected override void OnPreInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            //string userName = HttpContext.Current.User.Identity.Name;
            //DPSMgr = new DockPreferenceSerializationManager();
            //DPSMgr.DeserializeState(userName);
        }

    }

    protected override void OnInit(EventArgs e)
    {
        //CreateObjects();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsCallback)
        //{
        //    string cbRef = Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData", "context");
        //    string callBackScript = "function CallServerToSaveUserPreferences(arg, context){" + cbRef + "};";
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallServerToSaveUserPreferences", callBackScript, true);
        //}
        //else
        //{
        //    strCallBackReturnMsg = "";
        //}

        //ConfigureObjects();

        //Set the Preview Button's OnClientClick handler... This is to enable the PrintPreview button to pop up the PrintPreview.aspx page
        string patientKey = Request.QueryString["PatientKey"].ToString();
        CheckBoxList codeTypeList = (CheckBoxList)(ZoneQL.FindControl("RadDockableObject2").FindControl("ZoneRDQL").FindControl("RadDockableObject1").FindControl("RDQLinks").FindControl("cblCodeOption"));
        string selectedCodeIndex = "";
        if (codeTypeList != null)
        {
            for (int i = 0; i < codeTypeList.Items.Count; i++)
            {
                if (codeTypeList.Items[i].Selected)
                {
                    selectedCodeIndex += i + ",";
                }
            }
        }
        if (selectedCodeIndex.Length > 0)
        {
            selectedCodeIndex = selectedCodeIndex.Substring(0, selectedCodeIndex.Length - 1);
        }
        
        string strUrl = "PrintPreview.aspx?PatientKey=" + patientKey.Trim() + "&SelectedCodeIndex=" + selectedCodeIndex;
        this.btnPreview.OnClientClick = "return btnPreviewClick('" + strUrl + "')";

        try
        {
            long patientdblKey = Convert.ToInt64(patientKey);
            int alertCount = PatientManager.GetPatientGetActiveAlertCount(patientdblKey, (byte)EnumAlertStatus.NurseDone);

            if (alertCount <= 0)
            {
                int docAlertCount = PatientManager.GetPatientGetActiveDOCAlertCount(patientdblKey, (byte)EnumAlertStatus.DocDone);
                if (docAlertCount <= 0)
                {
                    btnDoctorAlertNetwork.Text = "Nurse Alert Network";
                }
                else
                {
                    btnDoctorAlertNetwork.Text = "P.E.T.N.";
                }
            }
            else
            {
                btnDoctorAlertNetwork.Text = "Provider Alert Network";
            }
            DataSet dsPatientDemographic = PatientManager.GetPatientDemographics(long.Parse(patientKey), HttpContext.Current.User.Identity.Name.ToUpper());
            string name = dsPatientDemographic.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsPatientDemographic.Tables[0].Rows[0]["LastName"].ToString();
            Page.Title = string.Format("Medical History Index for: " + name);
        }
        catch { }
    }

    protected override void OnPreRender(EventArgs e)
    {
        //foreach (RadDockableObject dock in RadDockingManager1.DockableObjects)
        //{
        //    RadDockableObjectCommand close = dock.Commands.GetByName("Close");
        //    if (close != null)
        //    {
        //        close.AutoPostBack = true;
        //    }
        //}

        base.OnPreRender(e);
    }

    //protected void RadAjaxManagerResult_AjaxRequest(object sender, AjaxRequestEventArgs e)
    //{
    //    //Get the values passed in the Arg
    //    string[] opName = e.Argument.Split(new char[] { ';' });
    //    if (opName[0] == "DisciplineList")
    //    {
    //        DPSMgr = new DockPreferenceSerializationManager();
    //        string userName = User.Identity.Name;
    //        if (DPSMgr.SaveUserDockStatePreference(userName, opName))
    //        {

    //            string id = Request.QueryString["ID"].ToString();
    //            string modifierID = Request.QueryString["ModifierID"].ToString();
    //            RadAjaxManagerResult.ResponseScripts.Add(string.Format("alert('Your Preference Has Been Saved Successfully. Please select the patient from the grid again to reflect your changes in the result Page.');"));

    //            //Simply Redirect so the user can see the changes right away -- Erroneous!!!
    //            //Response.Redirect("Result.aspx?ID=" + id + "&ModifierID=" + modifierID);
    //        }
    //    }
    //}

    #region ICallbackEventHandler Members

    public string GetCallbackResult()
    {
        return (string.IsNullOrEmpty(strCallBackReturnMsg) ? "" : strCallBackReturnMsg);
    }

    public void RaiseCallbackEvent(string eventArgument)
    {
        AjaxLoadingPanel.Visible = true;
        //Get the values passed in the Arg
        string[] opName = eventArgument.Split(new char[] { ';' });
        if (opName[0] == "DisciplineList")
        {
            DPSMgr = new DockPreferenceSerializationManager();
            string userName = User.Identity.Name;
            //System.Threading.Thread.Sleep(5000);
            if (DPSMgr.SaveUserDockStatePreference(userName, opName))
            {
                strCallBackReturnMsg = "Your Preference Has Been Saved Successfully. \n Please select the patient from the grid again to reflect your changes in the result Page.";
                //string id = Request.QueryString["ID"].ToString();
                //string modifierID = Request.QueryString["ModifierID"].ToString();
                //RadAjaxManagerResult.ResponseScripts.Add(string.Format("alert('Your Preference Has Been Saved Successfully. Please select the patient from the grid again to reflect your changes in the result Page.');"));

                //Simply Redirect so the user can see the changes right away -- Erroneous!!!
                //Response.Redirect("Result.aspx?ID=" + id + "&ModifierID=" + modifierID);
            }
            else
            {
                strCallBackReturnMsg = "An Unexpected Error occurred during the save operation. \n Please contact your System Administrator regarding this error.";
            }
        }
        AjaxLoadingPanel.Visible = false;
    }

    #endregion

    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        long patientKey = Convert.ToInt64(Request.QueryString["PatientKey"]);
        string loginUser = HttpContext.Current == null ? "test" : HttpContext.Current.User.Identity.Name;
        DataTable dtLastAccessInfo = Session["LAST_ACCESS_INFO"] as System.Data.DataTable;

        if (dtLastAccessInfo != null)
        {
            DataRow[] dtRows = dtLastAccessInfo.Select(string.Format("PatientKey='{0}' AND UserAccessed = '{1}'", patientKey, loginUser));
            if (dtRows.Length != 0)
            {
                DateTime lastAccessTime = DateTime.Parse(dtRows[0]["LastAccessTime"].ToString());
                System.Data.DataTable dtUserInfo = PatientManager.GetUserInfo(loginUser);

                if (dtUserInfo.Rows[0]["Industry"].ToString().Trim() == "Healthcare")
                {
                    PatientManager.UpdatePatientAccessRecordUpdateTime(patientKey, loginUser, lastAccessTime);
                    dtLastAccessInfo.Rows.Remove(dtRows[0]);
                    Session["LAST_ACCESS_INFO"] = dtLastAccessInfo;
                }
            }
        }
        Page.RegisterStartupScript("clientScript", "<script>window.close()</script>");
    }
    protected void btnIMergencyTransferFormRequest_Click(object sender, EventArgs e)
    {
        string patientKey = Request.QueryString["PatientKey"].ToString();
        ClientScript.RegisterStartupScript(this.GetType(), "openEmergency", "openEmergencyTransferFormRequest('" + patientKey + "');", true);
    }

    protected void btnDoctorAlertNetwork_Click(object sender, EventArgs e)
    {
        string patientKey = Request.QueryString["PatientKey"].ToString();
        ClientScript.RegisterStartupScript(this.GetType(), "openDoctor", "openDoctorAlertNetwork('" + patientKey + "');", true);
    }
}


