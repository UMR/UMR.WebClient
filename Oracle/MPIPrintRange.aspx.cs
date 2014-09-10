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

public partial class Oracle_MPIPrintRange : System.Web.UI.Page
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
    }

    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
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
        printingDiv.Visible = true;
        ClientScript.RegisterStartupScript(this.GetType(), "printrangescript", "window.print(); window.onfocus = function() { window.close(); }", true);
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
    protected void grdMPI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = e.Row.DataItem as DataRowView;

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
}
