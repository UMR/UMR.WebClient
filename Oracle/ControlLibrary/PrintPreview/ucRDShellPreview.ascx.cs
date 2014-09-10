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

public partial class Oracle_ControlLibrary_PrintPreview_ucRDShellPreview : System.Web.UI.UserControl
{
    long patientKey;
    string disCode, disType; //codeType, medCode, codeVersion, 
    RadDockableObject rdoParent;
    DataSet dsRDList;

    protected void Page_Load(object sender, EventArgs e)
    {
        patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());

        //  Get the List First...
        dsRDList = PatientManager.GetRemarkableDisciplineList(patientKey);
        // Now Get the Parent Dock
        rdoParent = GetParentDockableObject(this);
        if (rdoParent != null)
        {
            // Now do a match based on name from the table and load data if available
            LoadDataForSpecifiedRDName(rdoParent.Text);
        }
    }
    private void LoadDataForSpecifiedRDName(string RDName)
    {
        // Fun Stuff...
        //  1. Get the Name of the Dock Container
        //  2. Match it with the RDList for this client
        //  3. If a match is Found
        //      3.1 Read the Param Values
        //      3.2 Load and Display Data Based on the Param Values
        //  4. Else
        //      4.1 No RD for this specific Criteria
        //      4.2 Show "No Record!!!"
        //  5. 
        DataView dvwRDList = new DataView(dsRDList.Tables[0], "", "Detail", DataViewRowState.CurrentRows);
        int rowIndex = dvwRDList.Find(RDName);
        if (rowIndex > -1)
        {
            // Match Found... Get the values...
            //codeType = dvwRDList[rowIndex]["CodeType"].ToString();
            //medCode = dvwRDList[rowIndex]["MedCode"].ToString();
            //codeVersion = dvwRDList[rowIndex]["CodeVersion"].ToString();
            disCode = dvwRDList[rowIndex]["DisCode"].ToString();
            disType = dvwRDList[rowIndex]["DisciplineType"].ToString();
            //Get the Type of RD tag
            if (disType.Equals("D"))                //typeof(RD) == Diagnostics
            {
                string path = ("~/Oracle/ControlLibrary/PrintPreview/ucRDDiagnosisMainPreview.ascx");
                Oracle_ControlLibrary_PrintPreview_ucRDDiagnosisMainPreview ucDiagMain = (Oracle_ControlLibrary_PrintPreview_ucRDDiagnosisMainPreview)LoadControl(path);
                //Set the required Parameter Values in the User Control
                ucDiagMain.PatientKey = patientKey;
                if (Session["TypeFilter"] != null)
                {
                    ucDiagMain.CodeType = Session["TypeFilter"].ToString().Trim();

                    //Session["TypeFilter"] = null;
                }
                //ucDiagMain.CodeType = codeType;
                //ucDiagMain.MedCode = medCode;
                //ucDiagMain.CodeVersion = codeVersion;
                ucDiagMain.DisCode = disCode;
                //Now Add the control
                phRD.Controls.Add(ucDiagMain);
            }
            else                                //typeOf(RD) == Medication
            {
                string path = ("~/Oracle/ControlLibrary/PrintPreview/ucRDMedicationMainPreview.ascx");
                Oracle_ControlLibrary_PrintPreview_ucRDMedicationMainPreview ucMedMain = (Oracle_ControlLibrary_PrintPreview_ucRDMedicationMainPreview)LoadControl(path);
                ucMedMain.PatientKey=patientKey;
                //Add the control
                phRD.Controls.Add(ucMedMain);
            }
        }
        else
        {
            // No Match -- Show No Record...
            Label lbl = new Label();
            lbl.ForeColor = System.Drawing.Color.Green;
            lbl.Font.Names = new string[] { "Tahoma", "Arial", "Verdana" };
            lbl.Text = "N/A";
            phRD.Controls.Add(lbl);
        }
    }

    private RadDockableObject GetParentDockableObject(Control control)
    {
        if (control.Parent == null) return null;
        if (control.Parent is RadDockableObject) return (RadDockableObject)control.Parent;
        else return GetParentDockableObject(control.Parent);
    }
}
