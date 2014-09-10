using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;

public class EmailManager
{
    #region Variable(s)
    private string _toAddress;
    private string _subject;
    private bool _isBodyHtml;
    private string _body;
    #endregion

    #region Property(s)
    private string SmtpServer
    {
        get
        {
            string str = string.Empty;
            if (ConfigurationManager.AppSettings["SmtpServer"] != null)
                str = ConfigurationManager.AppSettings["SmtpServer"].Trim();
            else
                throw new Exception("Web config is not configure properly. Smtp Server is missing.");
            return str;
        }
    }
    private int SmtpPort
    {
        get
        {
            int str = 0;
            if (ConfigurationManager.AppSettings["SmtpPort"] != null)
                str = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"].Trim());
            else
                throw new Exception("Web config is not configure properly. Smtp port is missing.");
            return str;
        }
    }
    private string SenderUserID
    {
        get
        {
            string str = string.Empty;
            if (ConfigurationManager.AppSettings["SenderUserID"] != null)
                str = ConfigurationManager.AppSettings["SenderUserID"].Trim();
            else
                throw new Exception("Web config is not configure properly. Sender email is missing.");
            return str;
        }
    }
    private string DisplayName
    {
        get
        {
            string str = null;
            if (ConfigurationManager.AppSettings["DisplayName"] != null)
                str = ConfigurationManager.AppSettings["DisplayName"].Trim();
            return str;
        }
    }
    private string SenderPassword
    {
        get
        {
            string str = string.Empty;
            if (ConfigurationManager.AppSettings["SenderPassword"] != null)
                str = ConfigurationManager.AppSettings["SenderPassword"].Trim();
            else
                throw new Exception("Web config is not configure properly. Sender password is missing.");
            return str;
        }
    }

    public string ToAddress
    {
        get { return _toAddress; }
        set { _toAddress = value; }
    }
    public string Subject
    {
        get { return _subject; }
        set { _subject = value; }
    }
    public bool IsBodyHtml
    {
        get { return _isBodyHtml; }
        set { _isBodyHtml = value; }
    }
    public string Body
    {
        get { return _body; }
        set { _body = value; }
    }
    #endregion

    #region Constructor
    public EmailManager()
    {
        this.Subject = "Last Access Records";
    }
    public EmailManager(string toAddress, string subject, bool isBodyHtml, string body)
    {
        this.ToAddress = toAddress;
        this.Subject = subject;
        this.IsBodyHtml = isBodyHtml;
        this.Body = body;
    }
    #endregion

    #region Method(s)
    public void Send()
    {
        SmtpClient client = new SmtpClient(this.SmtpServer, this.SmtpPort);
        client.Credentials = new NetworkCredential(this.SenderUserID, this.SenderPassword);
        client.EnableSsl = true;

        MailMessage message = new MailMessage();
        message.IsBodyHtml = this.IsBodyHtml;
        message.Subject = this.Subject;
        message.Body = this.Body;
        message.From = new MailAddress(this.SenderUserID, this.DisplayName == string.Empty ? null : this.DisplayName);
        message.To.Add(this.ToAddress);

        try
        {
            client.Send(message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}
