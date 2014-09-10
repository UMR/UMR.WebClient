using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.RadDockUtils;
using Telerik.WebControls;
using System.Collections;
using System.Collections.Generic;

public partial class Oracle_DockedRD : System.Web.UI.Page
{

    const string CODETYPEFILTER = "CodeType";

    #region Property
    public long PatientKey
    {
        get
        {
            long id = 0;
            if (ViewState["PatientKey"] != null)
                id = Convert.ToInt64(ViewState["PatientKey"]);
            return id;
        }
        set
        {
            ViewState["PatientKey"] = value;
        }
    }
    public string DisCodes
    {
        get
        {
            string str = string.Empty;
            if (ViewState["DisCodes"] != null)
                str = ViewState["DisCodes"].ToString();
            return str;
        }
        set
        {
            ViewState["DisCodes"] = value;
        }
    }
    private string ClickedRD
    {
        get
        {
            string str = string.Empty;
            if (ViewState["ClickedRD"] != null)
                str = ViewState["ClickedRD"].ToString();
            return str;
        }
        set
        {
            ViewState["ClickedRD"] = value;
            hLastClicked.Value = value;
        }
    }
    private string SelectedColor
    {
        get
        {
            string str = string.Empty;
            if (ViewState["SelectedColor"] != null)
                str = ViewState["SelectedColor"].ToString();
            return str;
        }
        set
        {
            ViewState["SelectedColor"] = value;
        }
    }
    #endregion

    #region Page Event(s)
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["CommunicationPageLoaded"] == null)
            {
                Session["VisitedRD"] = null;
                Session["Trace"] = null;
                Session["CommunicationPageLoaded"] = null;
                Session["hfLocationPointer"] = null;

                this.PatientKey = Convert.ToInt64(Request.QueryString["PatientKey"]);
                this.DisCodes = Convert.ToString(Request.QueryString["discodes"]);
                this.ClickedRD = Request.QueryString["ClickedRD"] == null ? string.Empty : Convert.ToString(Request.QueryString["ClickedRD"]);

                this.TraceHandler();
            }
            else
            {
                Session["CommunicationPageLoaded"] = null;

                Stack<string> stack = (Stack<string>)Session["Trace"];
                hfLocationPointer.Value = string.Format("{0}|{1}", Session["hfLocationPointer"], stack.Count);

                if (stack != null && stack.Count != 0)
                {
                    string val = stack.Peek();

                    this.PatientKey = Convert.ToInt64(val.Split('^')[0]);
                    this.DisCodes = Convert.ToString(val.Split('^')[1]);
                    this.ClickedRD = Convert.ToString(val.Split('^')[2]);
                    this.PrepareDockObjects();
                }
                else
                {
                    this.ClosePage();
                }
            }
            this.PrepareDockObjects();
        }
        else
        {
            string ctrlname = Page.Request.Params.Get("__EVENTTARGET");
            if (ctrlname.ToUpper() != "RadAjaxManagerDefault".ToUpper() && ctrlname.ToUpper() != "UcLegendCompact2:btnApply".ToUpper())
            {
                this.PrepareDockObjects();
            }
        }
        UcLegendCompact2.FilterApplied += new LegendCompactEventHandler(RefreshRD);
    }
    #endregion

    #region Method(s)
    protected void RefreshRD(object sender, string selectedColor)
    {
        SelectedColor = selectedColor;
        this.PrepareDockObjects();
    }
    private void PrepareDockObjects()
    {
        DataTable dt = PatientManager.GetSortedRDs(DisCodes);
        if (dt == null) return;

        if (ClickedRD.Trim() != string.Empty)
            lblRD.Text = ClickedRD;
        else
            lblRD.Text = string.Empty;

        RadDockingZone1.Controls.Clear();
        RadDockingZone2.Controls.Clear();

        int counter = 0;
        
        foreach (DataRow oRow in dt.Rows)
        {
            RadDockableObject obj = new RadDockableObject();
            obj.ID = string.Format("ID{0}", oRow["ID_NUM"]);
            obj.Text = oRow["DETAIL"].ToString();
            obj.DockingMode = RadDockingModeFlags.AlwaysDock;

            Oracle_ControlLibrary_ucRD2l customControl = (Oracle_ControlLibrary_ucRD2l)LoadControl("~/Oracle/ControlLibrary/ucRD2l.ascx");

            if (Session[CODETYPEFILTER] != null)
            {
                customControl.CodeType = Session[CODETYPEFILTER].ToString();
            }

            customControl.PatientKey = PatientKey;
            customControl.DisCode = oRow["ID_NUM"].ToString();
            customControl.Clicked = ClickedRD;

            obj.Width = Unit.Percentage(100);
            obj.Height = Unit.Percentage(10);

            if (this.SelectedColor != string.Empty)
            {
                customControl.RefreshPage(this.SelectedColor);
            }
            obj.Container.Controls.Add(customControl);
            if (counter % 2 == 0)
                RadDockingZone1.Controls.Add(obj);
            else
                RadDockingZone2.Controls.Add(obj);

            counter++;
        }
    }
    private void TraceHandler()
    {
        string val = string.Format("{0}^{1}^{2}", PatientKey, DisCodes, ClickedRD);
        if (Session["Trace"] != null)
        {
            Stack<string> stack = (Stack<string>)Session["Trace"];

            //POP & Push
            int location = Convert.ToInt32(Session["hfLocationPointer"]);
            if (location + 1 != stack.Count)
            {
                int temp = stack.Count;
                for (int i = location; i < temp - 1; i++)
                {
                    stack.Pop();
                }
            }

            stack.Push(val);
            Session["Trace"] = stack;
            Session["hfLocationPointer"] = location + 1;
            hfLocationPointer.Value = string.Format("{0}|{1}", Session["hfLocationPointer"], stack.Count);
        }
        else
        {
            Stack<string> stack = new Stack<string>();
            stack.Push(val);

            Session["Trace"] = stack;
            Session["hfLocationPointer"] = 0;
            hfLocationPointer.Value = string.Format("{0}|{1}", Session["hfLocationPointer"], stack.Count);
        }
    }
    private void ClosePage()
    {
        Session["Trace"] = null;
        RadAjaxManagerDefault.ResponseScripts.Add("Close();");
    }
    #endregion

    #region AJAX Event(s)
    protected void RadAjaxManagerDefault_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        if (e.Argument.Contains("GoBack"))
        {
            Stack<string> stack = (Stack<string>)Session["Trace"];
            if (stack != null && stack.Count != 0)
            {
                //stack.Pop();

                if (stack.Count == 0)
                    this.ClosePage();
                else
                {
                    int location = Convert.ToInt32(Session["hfLocationPointer"]);
                    //string val = stack.Peek();
                    string[] arr = stack.ToArray();
                    Array.Reverse(arr);
                    string val = arr[--location];
                    Session["hfLocationPointer"] = location;

                    hfLocationPointer.Value = string.Format("{0}|{1}", Session["hfLocationPointer"], stack.Count);

                    this.PatientKey = Convert.ToInt64(val.Split('^')[0]);
                    this.DisCodes = Convert.ToString(val.Split('^')[1]);
                    this.ClickedRD = Convert.ToString(val.Split('^')[2]);
                    this.PrepareDockObjects();
                }
            }
            else
                this.ClosePage();
        }
        else if (e.Argument.Contains("GoForward"))
        {
            Stack<string> stack = (Stack<string>)Session["Trace"];
            if (stack != null && stack.Count != 0)
            {
                //stack.Pop();
                if (stack.Count == 0)
                    this.ClosePage();
                else
                {
                    //string val = stack.Peek();
                    int location = Convert.ToInt32(Session["hfLocationPointer"]);
                    string[] arr = stack.ToArray();
                    Array.Reverse(arr);
                    string val = arr[++location];

                    Session["hfLocationPointer"] = location;
                    hfLocationPointer.Value = string.Format("{0}|{1}", Session["hfLocationPointer"], stack.Count);

                    this.PatientKey = Convert.ToInt64(val.Split('^')[0]);
                    this.DisCodes = Convert.ToString(val.Split('^')[1]);
                    this.ClickedRD = Convert.ToString(val.Split('^')[2]);
                    this.PrepareDockObjects();
                }
            }
            else
                this.ClosePage();
        }
        else if (e.Argument.Contains("^"))
        {
            string crd = Convert.ToString(e.Argument.Split('^')[2]);
            if (crd != this.ClickedRD)
            {
                this.PatientKey = Convert.ToInt64(e.Argument.Split('^')[0]);
                this.DisCodes = Convert.ToString(e.Argument.Split('^')[1]);
                this.ClickedRD = crd;

                TraceHandler();
            }
            this.PrepareDockObjects();
        }
        else if (e.Argument.Contains("CloseWindow"))
        {
            Session["VisitedRD"] = null;
            Session["Trace"] = null;
            Session["CommunicationPageLoaded"] = null;
            Session["hfLocationPointer"] = null;
        }
    }
    #endregion
}
