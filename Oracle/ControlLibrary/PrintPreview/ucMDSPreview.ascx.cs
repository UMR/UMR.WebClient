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
using System.Globalization;
using Telerik.WebControls;

public partial class Oracle_ControlLibrary_PrintPreview_ucMDSPreview : System.Web.UI.UserControl
{
    private long patientKey;
    private string codeType;
    private DateTime serviceDate;
    RadDockableObject rdoParent;
    DataSet dsRDList;

    const string MDS = "MDS";

    #region Public Properties


    public DateTime ServiceDate
    {
        get { return serviceDate; }
        set { serviceDate = value; }
    }

    public string CodeType
    {
        get { return codeType; }
        set { codeType = value; }
    }

    public long PatientKey
    {
        get { return patientKey; }
        set { patientKey = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());

        rdoParent = GetParentDockableObject(this);
        if (rdoParent != null)
        {
            // Now do a match based on name from the table and load data if available
            if (DateTime.TryParse(rdoParent.Text, out serviceDate))
            {
                serviceDate = Convert.ToDateTime(rdoParent.Text);
                //Now get the Data
                DataTable dtRD = PatientManager.GetSpecificServiceDetails(patientKey, MDS, serviceDate);

                RadGridMDSMain.DataSource = dtRD;
                RadGridMDSMain.DataBind();

                rdoParent.Text = "MDS found on: " + serviceDate.ToString("MM-dd-yyyy");
            }
        }
        //}
    }

    private RadDockableObject GetParentDockableObject(Control control)
    {
        if (control.Parent == null) return null;
        if (control.Parent is RadDockableObject) return (RadDockableObject)control.Parent;
        else return GetParentDockableObject(control.Parent);
    }

}