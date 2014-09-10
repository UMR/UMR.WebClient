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

public partial class Oracle_ControlLibrary_ConfigControls_ucConfigUser : System.Web.UI.UserControl
{

    private object _dataItem = null;

    public object DataItem
    {
        get { return _dataItem; }
        set { _dataItem = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
