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


public partial class Oracle_LRAPrintRange : System.Web.UI.Page
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
        DataSet ds = PatientManager.GetLRAInfo(patientKey);
        int rowCount = ds.Tables[0].Rows.Count;

        int totalPage = 1;
        if (Request.QueryString["PageCount"] != null)
        {
            totalPage = Int32.Parse(Request.QueryString["PageCount"]);
        }
        int numRowsPerPage = 20;

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
            ClientScript.RegisterStartupScript(this.GetType(), "invalidpagerangescript", "alert('Invalid Page Range');window.onfocus = function() { window.close(); }", true);
            return;
        }

        DataView dtView = ds.Tables[0].Copy().DefaultView;
        if (Session["SORTEXPRESSON"] != null)
        {
            dtView.Sort = Session["SORTEXPRESSON"].ToString();
            Session["SORTEXPRESSON"] = null;
        }
        DataTable dtSorted = dtView.ToTable();

        DataTable dt = new DataTable();

        dt.Columns.Add("AccessTime", typeof(string));
        dt.Columns.Add("RecordUpdateTime", typeof(string));        
        dt.Columns.Add("Phone", typeof(string));
        dt.Columns.Add("Fax", typeof(string));
        dt.Columns.Add("UserName", typeof(string));
        dt.Columns.Add("Industry", typeof(string));

        for (int i = ((pageFrom - 1) * numRowsPerPage); i < pageTo * numRowsPerPage; i++)
        {
            if (i >= rowCount) break;
            DataRow dr = dt.NewRow();
            dr["AccessTime"] = dtSorted.Rows[i]["AccessTime"].ToString();
            if (dtSorted.Rows[i]["RecordUpdateTime"] == DBNull.Value)
            {
                dr["RecordUpdateTime"] = DBNull.Value;
            }
            else
            {
                dr["RecordUpdateTime"] = dtSorted.Rows[i]["RecordUpdateTime"].ToString();
            }
            if (dtSorted.Rows[i]["Phone"] == DBNull.Value)
            {
                dr["Phone"] = DBNull.Value;
            }
            else
            {
                dr["Phone"] = dtSorted.Rows[i]["Phone"].ToString();
            }
            if (dtSorted.Rows[i]["Fax"] == DBNull.Value)
            {
                dr["Fax"] = DBNull.Value;
            }
            else
            {
                dr["Fax"] = dtSorted.Rows[i]["Fax"].ToString();
            }

            if (dtSorted.Rows[i]["Industry"] == DBNull.Value)
            {
                dr["Industry"] = DBNull.Value;
            }
            else
            {
                dr["Industry"] = dtSorted.Rows[i]["Industry"].ToString();
            }

            dr["UserName"] = dtSorted.Rows[i]["UserName"].ToString();

            dt.Rows.Add(dr);
        }

        gridViewLra.DataSource = dt;
        gridViewLra.DataBind();
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
}
