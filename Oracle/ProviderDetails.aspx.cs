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


public partial class Oracle_ProviderDetails : System.Web.UI.Page
{
    //private string id, modifierID;

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    //if (!IsPostBack)
    //    //{
    //    //    //Provider Detail is requested in 2 ways ==> For a single Provider or for all the providers that a patient may have
    //    //    //We r tagging the request with "DispType" querystring, for Single provider DispType == "S" else DispType =="M"
    //    //    if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
    //    //    {
    //    //        string dispType = Request.QueryString["DispType"].ToString();
    //    //        if (dispType.Equals("S"))       //This request is coming from other pages (RDDiagDetails,RDMedDetails,MPI,LRPM etc.)Details...
    //    //        {
    //    //            id = Request.QueryString["ID"].ToString();
    //    //            ucProvider.ProviderGrid.DataSource = PatientManager.GetProviderInfo(id);
    //    //        }
    //    //        else                            //This request will generate only from the HPList QuickLink in Result Page...
    //    //        {
    //    //            id = Request.QueryString["ID"].ToString();
    //    //            modifierID = Request.QueryString["ModifierID"].ToString();
    //    //            ucProvider.ProviderGrid.DataSource = PatientManager.GetMultipleProviderInfo(id, modifierID);
    //    //        }
    //    //        ucProvider.ProviderGrid.DataBind();
    //    //    }
    //    //}
    //}
}
