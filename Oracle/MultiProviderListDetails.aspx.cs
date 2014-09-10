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

public partial class Oracle_MultiProviderListDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UcLegendCompact1.FilterApplied += new LegendCompactEventHandler(RefreshPage);
    }
    protected void RefreshPage(object sender, string selectedColor)
    {
       multiProviderList.RefreshProviderGrid(selectedColor);
    }
}
