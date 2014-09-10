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

public partial class ProviderConfig : System.Web.UI.Page
{
    DataTable dtProviders = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        string clientExecute = string.Format("document.getElementById('{0}').innerHTML = ''; document.getElementById('{1}').innerHTML = '';", this.Label1.ClientID, Label2.ClientID);
        grdProvider.ClientSettings.ClientEvents.OnGridCreated = clientExecute;

    }
    protected void grdProvider_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        grdProvider.DataSource = PatientManager.GetAllProviders();
    }
    protected void grdProvider_InsertCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;
        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
        //Update new values
        Hashtable newValues = new Hashtable();
        //Load Up the parameter values for Update
        newValues["ProviderID"] = (userControl.FindControl("txtID") as TextBox).Text;
        newValues["FirstName"] = (userControl.FindControl("txtFName") as TextBox).Text;
        newValues["LastName"] = (userControl.FindControl("txtLName") as TextBox).Text;
        newValues["Phone"] = (userControl.FindControl("txtPhone") as TextBox).Text;
        newValues["Fax"] = (userControl.FindControl("txtFax") as TextBox).Text;
        newValues["CellPhone"] = (userControl.FindControl("txtCell") as TextBox).Text;
        newValues["InstitutionID"] = (userControl.FindControl("txtInst") as TextBox).Text;
        newValues["Discipline"] = (userControl.FindControl("ddlDiscipline") as DropDownList).SelectedValue;
        newValues["WebSite"] = (userControl.FindControl("txtWeb") as TextBox).Text;
        newValues["Email"] = (userControl.FindControl("txtEmail") as TextBox).Text;

        //Now Call The Update Method in the BL

        int retVal = -1;
        try
        {
            retVal = PatientManager.InsertProvider(newValues);
        } 
        catch
        {

            DisplayMessage(true, "An Exception Occurred! Employee cannot be inserted. Please contact your System Administrator. ");
            //e.Canceled = true;
        }
        if (retVal == 1)
        {
            DisplayMessage(false, "Provider inserted");

        }
    }
    protected void grdProvider_UpdateCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;
        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
        //Get the Primary Key       
        string providerID = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ProviderID"].ToString();
        //Update new values
        Hashtable newValues = new Hashtable();
        //Load Up the parameter values for Update
        newValues["FirstName"] = (userControl.FindControl("txtFName") as TextBox).Text;
        newValues["LastName"] = (userControl.FindControl("txtLName") as TextBox).Text;
        newValues["Phone"] = (userControl.FindControl("txtPhone") as TextBox).Text;
        newValues["Fax"] = (userControl.FindControl("txtFax") as TextBox).Text;
        newValues["CellPhone"] = (userControl.FindControl("txtCell") as TextBox).Text;
        newValues["InstitutionID"] = (userControl.FindControl("txtInst") as TextBox).Text;
        newValues["Discipline"] = (userControl.FindControl("ddlDiscipline") as DropDownList).SelectedValue;
        newValues["WebSite"] = (userControl.FindControl("txtWeb") as TextBox).Text;
        newValues["Email"] = (userControl.FindControl("txtEmail") as TextBox).Text;

        //Now Call The Update Method in the BL
        int retVal = PatientManager.UpdateProvider(newValues, providerID);
        if (retVal == 0)
        {

            //grdProvider.Controls.Add(new LiteralControl("Unable to locate the Employee for updating."));
            e.Canceled = true;
            return;
        }
        else if (retVal == -1)
        {
            grdProvider.Controls.Add(new LiteralControl("Unable to update Providers."));
            e.Canceled = true;
        }
    }
    protected void grdProvider_DeleteCommand(object source, GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;
        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
        //Get the Primary Key       
        string providerID = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ProviderID"].ToString();
        //Now Call Delete in the BL
        int retVal = PatientManager.DeleteProvider(providerID);
        if (retVal == 0)
        {
            grdProvider.Controls.Add(new LiteralControl("Unable to locate the Employee for updating."));
            e.Canceled = true;
            return;
        }
        else if (retVal == -1)
        {
            grdProvider.Controls.Add(new LiteralControl("Unable to update Providers."));
            e.Canceled = true;
        }
    }
    private void DisplayMessage(bool isError, string text)
    {
        //In AJAX mode this will update the corresponding label text, client-side:
        string labelID = (isError) ? this.Label1.ClientID : this.Label2.ClientID;
        string clientExecute = string.Format("document.getElementById('{0}').innerHTML = '{1}';",
            labelID, Server.HtmlEncode(text).Replace("'", "&#39;"));

        this.grdProvider.ClientSettings.ClientEvents.OnGridCreated = clientExecute;
    }


    
}
