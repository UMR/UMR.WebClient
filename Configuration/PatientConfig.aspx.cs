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

public partial class Configuration_PatientConfig : System.Web.UI.Page
{
    private string PatientId, ModifierId;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grdPatient_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        grdPatient.DataSource = PatientManager.GetPatientList(String.Empty, String.Empty, String.Empty, String.Empty,String.Empty);
    }
    protected void grdPatient_InsertCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
    {

    }
    protected void grdPatient_UpdateCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
    {
        
    }
    protected void grdPatient_DeleteCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
    {

    }

    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }
    protected void grdPatient_EditCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
    {

    }
    protected void grdPatient_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
    {
        if ((e.CommandName == RadGrid.EditCommandName) || (e.CommandName == RadGrid.DeleteCommandName))
        {
            //Get the Primary keys
            PatientId = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
            ModifierId = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModifierID"].ToString();

            //UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //Configuration_ConfigControlLibrary_ucConfigPatient ucConfigPatient = (Configuration_ConfigControlLibrary_ucConfigPatient)userControl;
            ////string path = "~/Configuration/ConfigControlLibrary/ucConfigPatient.ascx";
            ////Configuration_ConfigControlLibrary_ucConfigPatient ucConfigPatient = (Configuration_ConfigControlLibrary_ucConfigPatient)LoadControl(path);
            
            //ucConfigPatient.PatientId = this.PatientId;
            //ucConfigPatient.ModifierID = this.ModifierId;
        }
        
    }
}
