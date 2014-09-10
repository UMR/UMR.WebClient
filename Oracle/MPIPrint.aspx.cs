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

public partial class Oracle_MPIPrint : System.Web.UI.Page
{
    private long patientKey;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
        {
            patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
        }
        RadGridMPI.DataBind();
        //grdMPI.DataSource = PatientManager.GetMPIInfo(id, modifierID);
        //grdMPI.DataBind();
    }

    protected void RadGridMPI_ItemCreated(object sender, GridItemEventArgs e)
    {
        //if (e.Item is GridDataItem)
        //{
        //    HyperLink editLink = e.Item.FindControl("Provider") as HyperLink;
        //    if (editLink != null)
        //    {
        //        editLink.Attributes["href"] = "#";
        //    }
        //    DataRowView drv;
        //    if (e.Item.DataItem != null)
        //    {
        //        drv = e.Item.DataItem as DataRowView;
        //        if (drv != null)
        //        {
        //            editLink.Text = drv["ProviderID"].ToString();   // e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["DETAIL"].ToString();
        //        }
        //        editLink.Attributes["onclick"] = string.Format("return ShowProviderDetails('{0}','{1}');",
        //                                                                    new object[] { drv["ProviderID"], "S" });
        //    }
        //}
    }
    protected void RadGridMPI_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        DataSet dsMPIInfo = PatientManager.GetMPIInfo(patientKey);


        string strFilter = "";

        if (Request.QueryString["filterText"] != null)
        {
            string[] filterArray = Request.QueryString["filterText"].Trim().Split(',');
            for (int i = 0; i < filterArray.Length; i++)
            {
                if (filterArray[i].Trim().Equals("")) continue;
                if (i < filterArray.Length - 1)
                {
                    strFilter += "Type='" + filterArray[i] + "' OR ";
                }
                else
                {
                    strFilter += "Type='" + filterArray[i] + "'";
                }
            }

        }

        DataTable dtMPIInfo = dsMPIInfo.Tables[0].Copy();
        DataView dvwMPIInfo = dtMPIInfo.DefaultView;
        dvwMPIInfo.RowFilter = strFilter;
        DataTable dtMPIInfoDistinct = dvwMPIInfo.ToTable();

        RadGridMPI.MasterTableView.DataSource = dtMPIInfoDistinct;
    }

    //protected void grdMPI_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    int i = e.Row.DataItemIndex;
    //    if (i > -1)
    //    {
    //        HyperLink editLink = e.Row.FindControl("Provider") as HyperLink;
    //        if (editLink != null)
    //        {
    //            editLink.Attributes["href"] = "#";
    //            DataRowView rowView = e.Row.DataItem as DataRowView;
    //            if (rowView != null)
    //            {
    //                editLink.Text = rowView.Row["ProviderID"].ToString();
    //                editLink.Attributes["onclick"] = string.Format("return ShowProviderDetails('{0}','{1}');",
    //                                                                        new object[] { rowView.Row["ProviderID"], "S" });
    //            }
    //            editLink.Attributes["onMouseOver"] = string.Format("return HideStatus()");
    //        }
    //    }
    //}
}
