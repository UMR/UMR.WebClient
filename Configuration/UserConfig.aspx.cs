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

public partial class UserConfig : System.Web.UI.Page
{
    DataTable dtUsers = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.Name.ToLower().Trim().Equals("test")==false)
        {
            Response.Redirect("../PatientSearchDefault.aspx");
        }
        string clientExecute = string.Format("document.getElementById('{0}').innerHTML = ''; document.getElementById('{1}').innerHTML = '';", this.Label1.ClientID, Label2.ClientID);
        grdUser.ClientSettings.ClientEvents.OnGridCreated = clientExecute;
    }
    protected void grdUser_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        grdUser.DataSource = PatientManager.GetAllUsers();
    }
    protected void grdUser_InsertCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;
        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
        //Update new values
        Hashtable newValues = new Hashtable();
        //Load Up the parameter values for Update
        newValues["UserID"] = (userControl.FindControl("txtID") as TextBox).Text;
        newValues["Password"] = (userControl.FindControl("txtPassword") as TextBox).Text;
        newValues["FirstName"] = (userControl.FindControl("txtFName") as TextBox).Text;
        newValues["LastName"] = (userControl.FindControl("txtLName") as TextBox).Text;
        newValues["Phone"] = (userControl.FindControl("txtPhone") as TextBox).Text;
        newValues["Email"] = (userControl.FindControl("txtEmail") as TextBox).Text;
        newValues["Industry"] = (userControl.FindControl("txtIndustry") as TextBox).Text;
        newValues["Visibility"] = (userControl.FindControl("txtVisibility") as TextBox).Text;
        newValues["Group"] = (userControl.FindControl("txtGroup") as TextBox).Text;
        newValues["Fax"] = (userControl.FindControl("txtFax") as TextBox).Text;
        newValues["Active"] = (userControl.FindControl("chkActive") as CheckBox).Checked;
        
        //Now Call The Update Method in the BL

        int retVal = 0;
        string errMsg = "";
        try
        {
            retVal = PatientManager.InsertUser(newValues, out errMsg);
        }
        catch (Exception ex)
        {
            grdUser.Controls.Add(Utility.GetLabel(true, "An Exception Occurred! User cannot be created."));
            e.Canceled = true;
        }
        if (retVal == 1)
        {
            grdUser.Controls.Add(Utility.GetLabel(false, "Success! User has been created."));
            e.Canceled = false;
        }
        else if (retVal == -1) // These exceptions were caught by DAL and prepared...
        {
            grdUser.Controls.Add(Utility.GetLabel(true, errMsg));
        }
    }
    protected void grdUser_UpdateCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;
        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
        //Get the Primary Key       
        string userID = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["UserID"].ToString();
        //Update new values
        Hashtable newValues = new Hashtable();
        //Load Up the parameter values for Update
        newValues["FirstName"] = (userControl.FindControl("txtFName") as TextBox).Text;
        newValues["LastName"] = (userControl.FindControl("txtLName") as TextBox).Text;
        newValues["Phone"] = (userControl.FindControl("txtPhone") as TextBox).Text;
        newValues["Email"] = (userControl.FindControl("txtEmail") as TextBox).Text;
        newValues["Industry"] = (userControl.FindControl("txtIndustry") as TextBox).Text;
        newValues["Visibility"] = (userControl.FindControl("txtVisibility") as TextBox).Text;
        newValues["UserGroup"] = (userControl.FindControl("txtGroup") as TextBox).Text;
        newValues["Fax"] = (userControl.FindControl("txtFax") as TextBox).Text;
        newValues["Active"] = (userControl.FindControl("chkActive") as CheckBox).Checked;
        //Now Call The Update Method in the BL
        int retVal = 0;
        string errMsg = "";
        try
        {
            retVal = PatientManager.UpdateUser(newValues, userID, out errMsg);
        }
        catch (Exception)
        {
            grdUser.Controls.Add(Utility.GetLabel(true, "An Exception Occurred! User cannot be edited."));
            e.Canceled = true;
        }
        
        if (retVal == 1)
        {
            grdUser.Controls.Add(Utility.GetLabel(false, "Success! User information has been updated."));
            e.Canceled = false;
        }
        if (retVal == -1)
        {
            grdUser.Controls.Add(Utility.GetLabel(true,errMsg));
        }
    }
    protected void grdUser_DeleteCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;
        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
        //Get the Primary Key       
        string userID = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["UserID"].ToString();
        //Now Call Delete in the BL
        int retVal = 0;
        string errMsg = "";
        try
        {
            retVal = PatientManager.DeleteUser(userID, out errMsg);
        }
        catch (Exception ex)
        {
            grdUser.Controls.Add(Utility.GetLabel(true, "An Exception Occurred! User cannot be deleted."));
        }
        if (retVal == 1)
        {
            grdUser.Controls.Add(Utility.GetLabel(false, "Success! User has been deleted."));
        }
        else if (retVal == -1)
        {
            grdUser.Controls.Add(Utility.GetLabel(true, errMsg));
        }
    }
    private void DisplayMessage(bool isError, string text)
    {
        //In AJAX mode this will update the corresponding label text, client-side:
        string labelID = (isError) ? this.Label1.ClientID : this.Label2.ClientID;
        string clientExecute = string.Format("document.getElementById('{0}').innerHTML = '{1}';",
            labelID, Server.HtmlEncode(text).Replace("'", "&#39;"));

        this.grdUser.ClientSettings.ClientEvents.OnGridCreated = clientExecute;
    }

    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }

}
