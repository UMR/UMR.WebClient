using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FaxSenderLib;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;

/// <summary>
/// Summary description for Utility
/// </summary>
public class Utility
{
    public Utility()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static Label GetLabel(bool Error, string Message)
    {
        Label lblMsg = new Label();
        lblMsg.Text = Message;
        lblMsg.Font.Name = "Tahoma";
        lblMsg.Font.Size = FontUnit.Point(8);
        lblMsg.BackColor = System.Drawing.Color.Yellow;
        switch (Error)
        {
            case false:
                lblMsg.ForeColor = System.Drawing.Color.Navy;
                break;
            default:
                lblMsg.ForeColor = System.Drawing.Color.Red;
                break;
        }
        return lblMsg;
    }
    private static FaxSender faxSender = null;
    public static FaxSender FaxSender
    {
        get
        {
            if (faxSender == null)
            {
                // using TCP protocol
                // running both client and server on same machines
                TcpChannel chan = new TcpChannel();
                ChannelServices.RegisterChannel(chan, false);

                // Create an instance of the remote object
               faxSender = (FaxSender)Activator.GetObject(typeof(FaxSender), "tcp://localhost:8089/FaxSender");
            }
            return faxSender;
        }
    }
}
