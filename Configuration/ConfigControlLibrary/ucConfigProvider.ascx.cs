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


public partial class Oracle_ControlLibrary_ConfigControls_ucConfigProvider : System.Web.UI.UserControl
{

    private object _dataItem = null;

    public object DataItem
    {
        get { return _dataItem; }
        set { _dataItem = value; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.DataBinding += new EventHandler(Oracle_ControlLibrary_ConfigControls_ucConfigProvider_DataBinding);
    }

    void Oracle_ControlLibrary_ConfigControls_ucConfigProvider_DataBinding(object sender, EventArgs e)
    {
        DataTable dtDisc = PatientManager.GetDisciplines();
        ddlDiscipline.DataSource = dtDisc;
        ddlDiscipline.DataTextField = "Discipline";
        ddlDiscipline.DataValueField = "DisciplineCode";
        
        ddlDiscipline.DataBind();
       
        object tocValue = DataBinder.Eval(DataItem, "Discipline");

        if (tocValue == DBNull.Value)
        {
            ddlDiscipline.SelectedIndex = 0;
        }
        else
        {
            ddlDiscipline.SelectedItem.Text = tocValue.ToString();
        }
        ddlDiscipline.DataSource = null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void ddlDiscipline_DataBinding(object sender, EventArgs e)
    {
        //ddlDiscipline.DataSource = PatientManager.GetDisciplines();
        //ddlDiscipline.DataBind();
    }
}
