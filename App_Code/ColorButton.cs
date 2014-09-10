using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

/// <summary>
/// Summary description for ColorButton
/// </summary>
public class ColorButton
{
    private string id;

    public string ID
    {
        get { return id; }
        set { id = value; }
    }
    private string text;

    public string Text
    {
        get { return text; }
        set { text = value; }
    }
    private Color buttonColor;

    public Color ButtonColor
    {
        get { return buttonColor; }
        set { buttonColor = value; }
    }
    private bool animate;

    public bool Animate
    {
        get { return animate; }
        set { animate = value; }
    }

    public int ButtonWidth
    {
        get
        {
            return this.text.Length * 10 + 8;
        }
    }
    private string clientScript;

    public string ClientScript
    {
        get { return clientScript; }
        set { clientScript = value; }
    }

    public ColorButton()
    {
        this.id = "0";
        this.text = "Untitled";
        this.buttonColor = Color.Green;
        this.animate = false;
    }
}
