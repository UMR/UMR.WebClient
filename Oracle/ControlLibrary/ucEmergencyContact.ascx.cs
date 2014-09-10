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

public partial class Oracle_ControlLibrary_ucEmergencyContact : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string user = HttpContext.Current.User.Identity.Name;
            long patientKey = long.Parse(Request.QueryString["PatientKey"].ToString());

            //dtvEmergencyContact.DataSource = PatientManager.GetEmergencyContact(id, modifierId,user);
            //dtvEmergencyContact.DataBind();

            grdEmergencyContact.DataSource = PatientManager.GetEmergencyContact(patientKey, user);
            grdEmergencyContact.DataBind();
        }
    }
}
