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

public partial class Configuration_ConfigControlLibrary_ucPatientDemographic : System.Web.UI.UserControl
{
    private string id, modifierID;

    public string ModifierID
    {
        get { return modifierID; }
        set { modifierID = value; }
    }

    public string PatientId
    {
        get { return id; }
        set { id = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //DataTable dt = PatientManager.GetPatientDemographicGeneralInfo(this.id, this.modifierID);
    }
}
