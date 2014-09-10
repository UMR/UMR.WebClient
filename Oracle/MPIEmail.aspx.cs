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
using System.Text;
using System.IO;
using System.Net.Mail;

public partial class Oracle_MPIEmail : System.Web.UI.Page
{
    private long patientKey;
    private string pageCount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString.ToString()))
        {
            patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
            pageCount = Request.QueryString["PageCount"];
        }
        lblTotalPages.Text = pageCount;
        txtPageTo.Focus();
        //BounGridView();
        //RadGridMPI.DataBind();
    }

    private void BounGridView()
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
        grdMPI.DataSource = dtMPIInfoDistinct;
        grdMPI.DataBind();

    }
    //protected void RadGridMPI_ItemCreated(object sender, GridItemEventArgs e)
    //{
    //    if (e.Item is GridDataItem)
    //    {
    //        HyperLink editLink = e.Item.FindControl("Provider") as HyperLink;
    //        if (editLink != null)
    //        {
    //            editLink.Attributes["href"] = "#";
    //        }
    //        DataRowView drv;
    //        if (e.Item.DataItem != null)
    //        {
    //            drv = e.Item.DataItem as DataRowView;
    //            if (drv != null)
    //            {
    //                editLink.Text = drv["ProviderID"].ToString();   // e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["DETAIL"].ToString();
    //            }
    //            //editLink.Attributes["onclick"] = string.Format("return ShowProviderDetails('{0}','{1}');",
    //            //                                                            new object[] { drv["ProviderID"], "S" });
    //        }
    //    }
    //}
    //protected void RadGridMPI_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    //{
    //    DataSet dsMPIInfo = PatientManager.GetMPIInfo(id, modifierID);


    //    string strFilter = "";

    //    if (Request.QueryString["filterText"] != null)
    //    {
    //        string[] filterArray = Request.QueryString["filterText"].Trim().Split(',');
    //        for (int i = 0; i < filterArray.Length; i++)
    //        {
    //            if (filterArray[i].Trim().Equals("")) continue;
    //            if (i < filterArray.Length - 1)
    //            {
    //                strFilter += "Type='" + filterArray[i] + "' OR ";
    //            }
    //            else
    //            {
    //                strFilter += "Type='" + filterArray[i] + "'";
    //            }
    //        }

    //    }

    //    DataTable dtMPIInfo = dsMPIInfo.Tables[0].Copy();
    //    DataView dvwMPIInfo = dtMPIInfo.DefaultView;
    //    dvwMPIInfo.RowFilter = strFilter;
    //    DataTable dtMPIInfoDistinct = dvwMPIInfo.ToTable();

    //    RadGridMPI.MasterTableView.DataSource = dtMPIInfoDistinct;
    //}
    protected void btnSendEmail_Click(object sender, EventArgs e)
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

        int rowCount = dtMPIInfoDistinct.Rows.Count;

        int totalPage = 1;
        if (Request.QueryString["PageCount"] != null)
        {
            totalPage = Int32.Parse(Request.QueryString["PageCount"]);
        }
        int numRowsPerPage = 40;

        int pageFrom = 1;
        int pageTo = 1;

        if (rdbListPrintChoice.SelectedIndex == 1)
        {
            try
            {
                pageFrom = Convert.ToInt32(txtPageFrom.Text);
                pageTo = Convert.ToInt32(txtPageTo.Text);
            }
            catch { }
        }
        else
        {
            pageFrom = 1;
            pageTo = totalPage;
        }
        if (pageTo > totalPage || pageFrom > totalPage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "invalidtoscript", "alert('Invalid Page Range');window.onfocus = function() { window.close(); }", true);
            return;
        }
        DataTable dt = new DataTable();

        dt.Columns.Add("Code", typeof(string));
        dt.Columns.Add("Type", typeof(string));
        dt.Columns.Add("Version", typeof(string));
        dt.Columns.Add("MedicalContentIndex", typeof(string));
        dt.Columns.Add("DateOfService", typeof(string));
        dt.Columns.Add("ServiceTime", typeof(string));
        dt.Columns.Add("ProviderID", typeof(string));
        dt.Columns.Add("InstituteCode", typeof(string));

        for (int i = ((pageFrom - 1) * numRowsPerPage); i < pageTo * numRowsPerPage; i++)
        {
            if (i >= rowCount) break;
            DataRow dr = dt.NewRow();
            dr["Code"] = dtMPIInfoDistinct.Rows[i]["Code"].ToString();
            dr["Type"] = dtMPIInfoDistinct.Rows[i]["Type"].ToString();
            dr["Version"] = dtMPIInfoDistinct.Rows[i]["Version"].ToString();
            dr["MedicalContentIndex"] = dtMPIInfoDistinct.Rows[i]["MedicalContentIndex"].ToString();
            dr["DateOfService"] = dtMPIInfoDistinct.Rows[i]["DateOfService"].ToString();
            dr["ServiceTime"] = dtMPIInfoDistinct.Rows[i]["ServiceTime"].ToString();
            dr["ProviderID"] = dtMPIInfoDistinct.Rows[i]["ProviderID"].ToString();
            dr["InstituteCode"] = dtMPIInfoDistinct.Rows[i]["InstituteCode"].ToString();
            dt.Rows.Add(dr);
        }
        grdMPI.DataSource = dt;
        grdMPI.DataBind();



        lblMessage.Text = "";
        Panel myPanel = new Panel();
        myPanel.Controls.Add(grdMPI);
        StringBuilder sb = new StringBuilder();
        using (StringWriter sw = new StringWriter(sb))
        {
            using (HtmlTextWriter textWriter = new HtmlTextWriter(sw))
            {
                myPanel.RenderControl(textWriter);
            }
        }
        string html = sb.ToString();

        try
        {
            EmailManager client = new EmailManager(txtEmailAddress.Text.Trim(), "Master Patient Code Indexes", true, html);
            client.Send();
            ClientScript.RegisterStartupScript(this.GetType(), "successscript", "alert('Mail Successfully Sent');", true);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "successscript", "alert('" + ex.Message + "');", true);
        }

    }
    protected void grdMPI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = e.Row.DataItem as DataRowView;
            string dateOfService = rowView.Row["DateOfService"].ToString();
            e.Row.BackColor = PatientManager.GetRowColorBasedOnDate(dateOfService);

            Literal ltCodeModifier = e.Row.FindControl("ltCodeModifier") as Literal;
            if (ltCodeModifier != null)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv != null)
                {
                    string codeType = drv["Type"].ToString().Trim();
                    string medCode = drv["Code"].ToString().Trim();
                    int codeVersion = Convert.ToInt32(drv["Version"].ToString().Trim());

                    DataSet ds = PatientManager.GetCodeModifiersByMedcode(codeType, medCode, codeVersion);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            sb.Append("<a class=\"tooltip\" title=\"" + ds.Tables[0].Rows[i]["LongDescription"].ToString() + "\" style=\"color:#355E3B;\">" + ds.Tables[0].Rows[i]["ModifierCode"].ToString() + "</a>");
                            if (i < ds.Tables[0].Rows.Count - 1)
                            {
                                sb.Append(", ");
                            }
                        }
                        ltCodeModifier.Text = sb.ToString();
                    }
                    else
                    {
                        ltCodeModifier.Text = "N/A";
                    }
                }
            }
        }
    }
    protected void rdbListPrintChoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbListPrintChoice.SelectedIndex == 0)
        {
            rangeConfig.Visible = false;
        }
        else
        {
            rangeConfig.Visible = true;
        }
    }
}
