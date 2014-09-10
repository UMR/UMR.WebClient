<%@ Application Language="C#" %>
<%@ Import Namespace="Microsoft.Practices.EnterpriseLibrary.Logging" %>
<%@ Import Namespace="Microsoft.Practices.EnterpriseLibrary.ExceptionHandling" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        Exception ex = Server.GetLastError().GetBaseException();
        if (ex.GetType() == typeof(HttpException))
        {
            Response.Redirect("~/NoPageFound.aspx?message= " + ex.Message);
        }
        else
        {
            ExceptionPolicy.HandleException(ex, "Exception Log Policy");
            Server.Transfer("~/GenericErrorPage.aspx");
        }
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
    }

    void Session_End(object sender, EventArgs e)
    {
        System.Data.DataTable dtLastAccessInfo = Session["LAST_ACCESS_INFO"] as System.Data.DataTable;
        if (dtLastAccessInfo != null)
        {
            if (dtLastAccessInfo.Rows.Count > 0)
            {
                for (int i = 0; i < dtLastAccessInfo.Rows.Count; i++)
                {
                    DateTime lastAccessTime = DateTime.Parse(dtLastAccessInfo.Rows[i]["LastAccessTime"].ToString());
                    long patientKey = Int64.Parse(dtLastAccessInfo.Rows[i]["PatientKey"].ToString().Trim());
                    string userid = dtLastAccessInfo.Rows[i]["UserAccessed"].ToString().Trim();

                    //if (userid.ToLower().Contains("trialuser1") || userid.ToLower().Contains("trialuser2") || userid.ToLower().Contains("trialuser3"))
                    //{
                    System.Data.DataTable dtUserInfo = PatientManager.GetUserInfo(userid);
                    if (dtUserInfo.Rows[0]["Industry"].ToString().Trim() == "Healthcare")
                    {
                        PatientManager.UpdatePatientAccessRecordUpdateTime(patientKey, userid, lastAccessTime);
                    }
                    //}
                }
            }
        }
    }
    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        // if (Request.HttpMethod == "GET")
        //  {
        if (Request.AppRelativeCurrentExecutionFilePath.EndsWith(".aspx"))
        {
            //Response.Filter = new ScriptDeferFilter(Response);
        }
        //}
    }
       
</script>

