<%@ WebHandler Language="C#" Class="AutoCompleteHandler" %>

using System;
using System.Web;

public class AutoCompleteHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string queryType = context.Request.QueryString["type"];
        if (!String.IsNullOrEmpty(queryType))
        {
            if (queryType == "count")
            {
                //return count
                string query = context.Request.QueryString["query"];
                int option = Convert.ToInt32(context.Request.QueryString["option"]);
                
                if (query != null && query.Trim().Length > 0)
                {
                    int pageSize = 20;
                    if (!String.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                    {
                        pageSize = Int32.Parse(context.Request.QueryString["pagesize"]);
                    }

                    int count = PatientManager.GetPatientSearchPageCount(query, pageSize, option);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(count.ToString());
                }
            }
            else if (queryType == "recordcount")
            {
                string query = context.Request.QueryString["query"];
                int option = Convert.ToInt32(context.Request.QueryString["option"]);

                if (query != null && query.Trim().Length > 0)
                {
                    int count = PatientManager.GetPatientSearchRecordCount(query, option);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(count.ToString());
                }
            }
            else
            {
                //return data
                string query = context.Request.QueryString["query"];
               
                int option = Convert.ToInt32(context.Request.QueryString["option"]);
                
                if (query != null && query.Trim().Length > 0)
                {
                    int pageSize = 20;
                    if (!String.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                    {
                        pageSize = Int32.Parse(context.Request.QueryString["pagesize"]);
                    }

                    int pageNo = 1;
                    if (!String.IsNullOrEmpty(context.Request.QueryString["pageno"]))
                    {
                        pageNo = Int32.Parse(context.Request.QueryString["pageno"]);
                    }


                    System.Data.DataTable dt = PatientManager.GetPatientSearchPageData(query, pageNo, pageSize, option);

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append(dt.Rows[i]["PATIENT_KEY"].ToString()+";"+dt.Rows[i]["First_Name"].ToString() + ";" + dt.Rows[i]["Last_Name"].ToString());
                        if (i < dt.Rows.Count - 1)
                        {
                            sb.Append("|");
                        }
                    }
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(sb.ToString());
                }
            }
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}