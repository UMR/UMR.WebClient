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
using System.Text;

public partial class Oracle_RDTooltip : System.Web.UI.Page
{


    public int Discode
    {
        get
        {
            int discode = 0;
            if (!String.IsNullOrEmpty(Request.QueryString["discode"]))
            {
                discode = Convert.ToInt32(Request.QueryString["discode"]);
            }
            return discode;
        }
    }
    public string Codetype
    {
        get
        {
            if (!String.IsNullOrEmpty(Request.QueryString["codetype"]))
            {
                return Request.QueryString["codetype"];
            }
            return "";
        }
    }
    public string Medcode
    {
        get
        {
            if (!String.IsNullOrEmpty(Request.QueryString["medcode"]))
            {
                return Request.QueryString["medcode"];
            }
            return "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PopulateControls();
        }
    }

    private void PopulateControls()
    {
        DataSet dsRMDisc = PatientManager.GetRemarkableDisciplineListForMedCode(Medcode, Discode.ToString());
        string relatedDisciplineNames = RelatedDisciplineNames(dsRMDisc.Tables.Count == 0 ? new DataTable() : dsRMDisc.Tables[0]);
        string codedescriptionHistory = GetCodeDescriptinChangeHistoryString(Codetype, Medcode);
        string text = string.Format("<div style=\"color:blue;\">{0}</div>{1}", relatedDisciplineNames, codedescriptionHistory);
        Literal1.Text = text;
    }

    private string RelatedDisciplineNames(DataTable dt)
    {
        if (dt.Rows.Count == 0) return string.Format("<b>Related Discipline(s):</b> N/A");
        StringBuilder str = new StringBuilder();

        foreach (DataRow oRow in dt.Select(string.Empty, "DETAIL ASC"))
        {
            str.Append(str.Length == 0 ? oRow["DETAIL"].ToString() : string.Format(", {0}", oRow["DETAIL"]));
        }
        return string.Format("<b>Related Discipline(s):</b> {0}", str.ToString());
    }

    private string GetCodeDescriptinChangeHistoryString(string codeType, string medcode)
    {
        //DataTable dt = PatientManager.GetCodeDescriptinChangeHistory(codeType, medcode);
        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<br/><div>");
        //    string strDesc = dt.Rows[0]["DETAIL"].ToString().Replace("__", ", ").Replace('\0', ' ').Trim();
        //    strDesc = strDesc.Replace("'", "\\'");

        //    sb.Append("<table>");
        //    sb.Append("<tr><td valign='top'><span style=\"color:green;\">" + dt.Rows[0]["CODE_VERSION"] + "&nbsp;-&nbsp;</span></td><td valign='top'>" + strDesc + "<br/></td></tr>");
        //    //for (int i = 1; i < dt.Rows.Count; i++)
        //    //{
        //    //    string title = dt.Rows[i]["DETAIL"].ToString().Replace("__", ", ").Replace('\0', ' ').Trim();
        //    //    title = title.Replace("'", "\\'");
        //    //    if (i < (dt.Rows.Count - 1))
        //    //    {
        //    //        sb.Append("<tr style=\"color:green;cursor:pointer;\" title=\"" + title + "\"><td valign='top'><span>" + dt.Rows[i]["CODE_VERSION"].ToString() + "&nbsp;-&nbsp;</span></td><td valign='top'><span>Modified</span></td></tr>");
        //    //    }
        //    //    else
        //    //    {
        //    //        //sb.Append("<tr style=\"color:green;cursor:pointer;\" title=\"" + title + "\"><td valign='top'><span>" + dt.Rows[i]["CODE_VERSION"].ToString() + "&nbsp;-&nbsp;</span></td><td valign='top'><span>Originated</span></td></tr>");
        //    //    }
        //    //}
        //    sb.Append("</table>");
        //    if (dt.Rows.Count > 2)
        //    {
        //        sb.Append("<br/>");
        //        string title = "";
        //        for (int i = 1; i < dt.Rows.Count - 1; i++)
        //        {
        //            string desc = dt.Rows[i]["DETAIL"].ToString().Replace("__", ", ").Replace('\0', ' ').Trim();
        //            desc = desc.Replace("'", "\\'");
        //            title += dt.Rows[i]["CODE_VERSION"].ToString() + " - " + desc + "\n";
        //        }
        //        sb.Append("<a style=\"padding-left:5px;color:green;cursor:pointer;\" title=\"" + title + "\">Modified</a>");
        //    }
        //    sb.Append("</div>");
        //    return sb.ToString();
        //}

        DataTable dt = PatientManager.GetCodeHistory(codeType, medcode);
        if (dt != null && dt.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<br/><div>");
            string strDesc = dt.Rows[0]["DETAIL"].ToString().Replace("__", ", ").Replace('\0', ' ').Trim();
            strDesc = strDesc.Replace("'", "\\'");

            sb.Append("<table>");
            sb.Append("<tr><td valign='top'><span style=\"color:green;\">Active&nbsp;-&nbsp;</span></td><td valign='top'>" + strDesc + "<br/></td></tr>");
            sb.Append("</table>");

            sb.Append("<div style=\"padding-top:5px;\"><a style=\"padding-left:3px;color:green;\">Originated - N/A</a></div>");


            string strNew = "";
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Status"].ToString() == "N")
                {
                    strNew += dt.Rows[i]["CODE_VERSION"].ToString() + ", ";
                }
            }
            if (strNew.Length > 0)
            {
                strNew = strNew.Substring(0, strNew.Length - 2);
                sb.Append("<div style=\"padding-top:5px;\"><a style=\"padding-left:3px;color:green;\">New - " + strNew + "</a></div>");
            }

            string strModified = "";
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Status"].ToString() == "M")
                {
                    string desc = dt.Rows[i]["DETAIL"].ToString().Replace("__", ", ").Replace('\0', ' ').Trim();
                    desc = desc.Replace("'", "\\'");
                    strModified += dt.Rows[i]["CODE_VERSION"].ToString() + " - " + desc + "\n";
                }
            }
            if (strModified.Length > 0)
            {
                sb.Append("<div style=\"padding-top:5px;\"><a style=\"padding-left:3px;color:green;cursor:pointer;\" title=\"" + strModified + "\">Modified</a></div>");
            }

            string strReinstatement = "";
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Status"].ToString() == "R")
                {
                    strReinstatement += dt.Rows[i]["CODE_VERSION"].ToString() + ", ";
                }
            }
            if (strReinstatement.Length > 0)
            {
                strReinstatement = strReinstatement.Substring(0, strReinstatement.Length - 2);
                sb.Append("<div style=\"padding-top:5px;\"><a style=\"padding-left:3px;color:green;\">Reinstatement - " + strReinstatement + "</a></div>");
            }

            string strRclassification = "";
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Status"].ToString() == "C")
                {
                    strRclassification += dt.Rows[i]["CODE_VERSION"].ToString() + ", ";
                }
            }
            if (strRclassification.Length > 0)
            {
                strRclassification = strRclassification.Substring(0, strRclassification.Length - 2);
                sb.Append("<div style=\"padding-top:5px;\"><a style=\"padding-left:3px;color:green;\">Reclassification - " + strRclassification + "</a></div>");
            }

            string strDelete = "";
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Status"].ToString() == "D")
                {
                    strDelete += dt.Rows[i]["CODE_VERSION"].ToString() + ", ";
                }
            }
            if (strDelete.Length > 0)
            {
                strDelete = strDelete.Substring(0, strDelete.Length - 2);
                sb.Append("<div style=\"padding-top:5px;\"><a style=\"padding-left:3px;color:green;\">Deleted - " + strDelete + "</a></div>");
            }

           
            //if (dt.Rows.Count > 2)
            //{
            //    sb.Append("<br/>");
            //    string title = "";
            //    for (int i = 1; i < dt.Rows.Count - 1; i++)
            //    {
            //        string desc = dt.Rows[i]["DETAIL"].ToString().Replace("__", ", ").Replace('\0', ' ').Trim();
            //        desc = desc.Replace("'", "\\'");
            //        title += dt.Rows[i]["CODE_VERSION"].ToString() + " - " + desc + "\n";
            //    }
            //    sb.Append("<a style=\"padding-left:5px;color:green;cursor:pointer;\" title=\"" + title + "\">Modified</a>");
            //}
            sb.Append("</div>");
            return sb.ToString();
        }
        return "";
    }
}
