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

public partial class SecuredPages_Default : System.Web.UI.Page
{

    protected override void OnPreInit(EventArgs e)
    {
        string userName = User.Identity.Name;
        if (!IsPostBack)
            //Check to see if this is 1st time visit or not...
            if (IsUserHereForFirstTime(userName))
            {
                //Get the choices from him and persist
            }
            else
            {
                //Fetch his saved choices and save it in the Session so that Result Page can use that...

            }
        base.OnPreInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private bool IsUserHereForFirstTime(string UserName)
    {
        //Check to see if this is his 1st visit or not and return T/F
        return true;
    }

    
}
