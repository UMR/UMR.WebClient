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

public partial class Oracle_ControlLibrary_ucRemarkableDisciplineQuickLinks : System.Web.UI.UserControl
{
    private static long patientKey;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());

            dlRDQuickLinks.DataSource = PatientManager.GetRemarkableDisciplineList(patientKey);
            dlRDQuickLinks.DataBind();
        }
    }

    protected void dlRDQuickLinks_ItemCreated(object sender, DataListItemEventArgs e)
    {
        if (e.Item is DataListItem)
        {
            LinkButton editLink = e.Item.FindControl("LinkButton1") as LinkButton;
            if (editLink != null)
            {
                editLink.Attributes["href"] = "#";
            }
            DataRowView drv;
            if (e.Item.DataItem != null)
            {
                drv = (DataRowView)e.Item.DataItem;
                if (drv != null && drv["Detail"] != DBNull.Value || drv["Detail"].ToString() != string.Empty)
                {
                    editLink.Text = drv["Detail"].ToString();
                }
                else
                    editLink.Text = "N/A";
                editLink.Attributes["onclick"] = string.Format("return ShowRemarkableDiscipline('{0}','{1}','{2}','{3}','{4}');",
                                                                            new object[] { patientKey, drv["CodeType"],drv["MedCode"],drv["CodeVersion"], drv["DisCode"] });
            }
        }
    }
}
