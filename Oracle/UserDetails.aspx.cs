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

public partial class Oracle_UserDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["UserName"] != null)
            {
                string UserName = Request.QueryString["UserName"];

                detailsUserDetails.DataSource = PatientManager.GetUserInfo(UserName);
                detailsUserDetails.DataBind();
            }

        }
    }
}
