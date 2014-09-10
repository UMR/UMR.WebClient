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

public partial class Oracle_DockRD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadAllRelatedDiscipline();
    }

    private void LoadAllRelatedDiscipline()
    {
        string[] disCodes = DisCodes.Split(',');
        int colCount = 2;
        if (disCodes.Length > 2)
        {
            colCount = 3;
        }

        RadDockingZone[] zones = new RadDockingZone[colCount];
        for (int i = 0; i < colCount; i++)
        {
            zones[i] = new RadDockingZone();
            zones[i].Width = Unit.Percentage(100);
            zones[i].Height = Unit.Percentage(100);
            zones[i].ID = "zone" + i;
        }


        Table table = new Table();
        table.Width = Unit.Percentage(100);
        table.Height = Unit.Percentage(100);

        TableRow tr = new TableRow();
        for (int i = 0; i < colCount; i++)
        {
            TableCell tc = new TableCell();
            tc.Height = Unit.Percentage(100);
            tc.Width = Unit.Percentage(100/colCount);
            tc.VerticalAlign = VerticalAlign.Top;
            tc.HorizontalAlign = HorizontalAlign.Left;
            tc.Controls.Add(zones[i]);
            tr.Cells.Add(tc);
        }
        table.Rows.Add(tr);
        PlaceHolder1.Controls.Add(table);

        for (int i = 0; i < disCodes.Length; i++)
        {
            RadDockableObject dockObj = new RadDockableObject();
            dockObj.Width = Unit.Percentage(100);
            dockObj.ID = _ID + "_" + ModifierID + "_" + disCodes[i].Trim();
            dockObj.Behavior = RadDockableObjectBehaviorFlags.None;
            dockObj.DockingMode = RadDockingModeFlags.AlwaysDock;
            ITemplate innerControl = LoadTemplate("~/Oracle/ControlLibrary/ucRD2.ascx");
            dockObj.ContentTemplate = innerControl;
            zones[i % zones.Length].Controls.Add(dockObj);
        }
    }
    public string DisCodes
    {
        get
        {
            return Request.QueryString["discodes"].Trim();
        }
    }
    public string _ID
    {
        get
        {
            return Request.QueryString["ID"].Trim();
        }
    }
    public string ModifierID
    {
        get
        {
            return Request.QueryString["ModifierID"].Trim();
        }
    }
}
