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

public partial class SecuredPages_Result : System.Web.UI.Page
{
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
    }

    protected override void OnInit(EventArgs e)
    {
        //ITemplate innerControl = LoadTemplate(state.InnerControlPath);
        //dock.ContentTemplate = innerControl;

        //RadDockableObject dockObj = this.RadDockingZoneRightBottomLeft.FindControl("Uro
        /*id = Request.QueryString["ID"].ToString();
        modifierID = Request.QueryString["ModifierID"].ToString();
        codeType = Request.QueryString["CodeType"].ToString();
        medCode = Request.QueryString["MedCode"].ToString();
        codeVersion = Request.QueryString["CodeVersion"].ToString();
        disCode = Request.QueryString["DisCode"].ToString();
        
        RD.aspx?ID=" + id +"&ModifierID=" + modifierID
                                    + "&CodeType=" + codeType + "&MedCode=" + medCode
                                    + "&CodeVersion=" + codeVersion + "&DisCode=" + disCode
                                    + "&DisType=" + disType
         
         */
        //string id = "1", modifierID = "1", codeType = "CPT-4", medCode = "00103", codeVersion = "2006", disCode = "3";
        //string strControlPath = "~/Oracle/ControlLibrary/ucRDDiagnosis.ascx?ID=" + id + "&ModifierID=" + modifierID
        //                            + "&CodeType=" + codeType + "&MedCode=" + medCode
        //                            + "&CodeVersion=" + codeVersion + "&DisCode=" + disCode;
        //ITemplate innerControl = LoadTemplate(strControlPath);
        //this.RadDockableObject4.ContentTemplate = innerControl;
        
        
        
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
}
