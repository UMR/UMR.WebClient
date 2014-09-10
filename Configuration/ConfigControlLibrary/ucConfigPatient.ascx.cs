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

public partial class Configuration_ConfigControlLibrary_ucConfigPatient : System.Web.UI.UserControl
{

    private object _dataItem = null;
    public object DataItem
    {
        get { return _dataItem; }
        set { _dataItem = value; }
    }

    static long patientKey;

    public long PatientKey
    {
        get { return patientKey; }
        set { patientKey = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
       
    }
    protected void pgGeneral_Load(object sender, EventArgs e)
    {
        
        DataTable dt = PatientManager.GetPatientDemographicGeneralInfo(patientKey);
        if (dt.Rows.Count > 0)
        {
            this.txtFName.Text = dt.Rows[0]["FirstName"].ToString();
            this.txtLName.Text = dt.Rows[0]["LastName"].ToString(); 
        }
    }
    protected void pgPhone_Load(object sender, EventArgs e)
    {

    }

}
