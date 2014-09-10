using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Oracle_PatientImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int vitalSignID = 0;

            if (Request.QueryString.GetValues("VitalSignID") != null)
            {
                vitalSignID = Convert.ToInt32(Request.QueryString["VitalSignID"]);

                DataSet dsVitalSigns = PatientManager.GetPatientVitalSignsByID(vitalSignID);
                Response.ContentType = "image/jpg";
                byte[] bytData = (byte[])dsVitalSigns.Tables[0].Rows[0]["PATIENT_IMAGE"];

                Response.BinaryWrite(bytData);
            }
        }
        catch (Exception)
        {
        }
    }
}