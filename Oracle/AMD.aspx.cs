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

public partial class Oracle_AMD : System.Web.UI.Page
{

    //string id, modifierID;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
        //{
        //    id = Request.QueryString["ID"].ToString();
        //    modifierID = Request.QueryString["ModifierID"].ToString();
        //}
        //DataTable dtAMD = PatientManager.GetAMDForPatient(id, modifierID);
        //RadGridAMD.DataSource = dtAMD;
        //RadGridAMD.DataBind();
    }
}
