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

public partial class Oracle_ControlLibrary_ucDisclaimer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblCurrentYear.Text=DateTime.Now.Year.ToString();
    }
}
