using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Summary description for ColorButtonContainer
/// </summary>
public class ColorButtonContainer
{
    private List<ColorButton> buttons;

    private int maxColumn;

    public int MaxColumn
    {
        get { return maxColumn; }
        set { maxColumn = value; }
    }

    public ColorButtonContainer()
    {
        buttons = new List<ColorButton>();
    }

    public void AddButton(ColorButton btn)
    {
        this.buttons.Add(btn);
    }
    public string GetHTMLWithScript()
    {
        StringBuilder sbTotal = new StringBuilder();
        sbTotal.Append("<div>");
        string html = GetHTML();
        string script = GetScript();
        sbTotal.Append(html);
        sbTotal.Append(script);
        sbTotal.Append("</div>");
        return sbTotal.ToString();
    }

    private string GetScript()
    {
        StringBuilder sbScript = new StringBuilder();
        sbScript.Append("<script type=\"text/javascript\">");

        // global variables
        sbScript.Append("var buttonIds=[];");
        sbScript.Append("var buttonColors=[];");
        sbScript.Append("var animateStats=[];");
        for (int i = 0; i < buttons.Count; i++)
        {
            sbScript.Append("buttonIds[" + i + "]= '" + buttons[i].ID + "';");
            sbScript.Append("buttonColors[" + i + "]= '" + GetHexCode(buttons[i].ButtonColor) + "';");
            sbScript.Append("animateStats[" + i + "]= '" + buttons[i].Animate + "';");
        }

        // color tint script
        sbScript.Append(" function getTintedColor(color, v){");
        sbScript.Append("if(color!=null){");
        sbScript.Append("if (color.length > 6) {");
        sbScript.Append("color = color.substring(1, color.length)");
        sbScript.Append("}");
        sbScript.Append("var rgb = parseInt(color, 16);");
        sbScript.Append("var r = Math.abs(((rgb >> 16) & 0xFF) + v);");
        sbScript.Append("if (r > 255) ");
        sbScript.Append("r = r - (r - 255);");
        sbScript.Append("var g = Math.abs(((rgb >> 8) & 0xFF) + v);");
        sbScript.Append("if (g > 255) ");
        sbScript.Append("g = g - (g - 255);");
        sbScript.Append("var b = Math.abs((rgb & 0xFF) + v);");
        sbScript.Append("if (b > 255) ");
        sbScript.Append("b = b - (b - 255);");
        sbScript.Append("r = Number(r < 0 || isNaN(r)) ? 0 : ((r > 255) ? 255 : r).toString(16);");
        sbScript.Append("if (r.length == 1) ");
        sbScript.Append("r = '0' + r;");
        sbScript.Append(" g = Number(g < 0 || isNaN(g)) ? 0 : ((g > 255) ? 255 : g).toString(16);");
        sbScript.Append("if (g.length == 1) ");
        sbScript.Append("g = '0' + g;");
        sbScript.Append("b = Number(b < 0 || isNaN(b)) ? 0 : ((b > 255) ? 255 : b).toString(16);");
        sbScript.Append("if (b.length == 1) ");
        sbScript.Append("b = '0' + b;");
        sbScript.Append("return \"#\" + r + g + b;");
        sbScript.Append("} ");
        sbScript.Append("} ");

        // initialize buttons
        sbScript.Append("function InitButtons()");
        sbScript.Append("{");
        sbScript.Append("for (var i = 0; i < buttonIds.length; i++)");
        sbScript.Append("{");
        sbScript.Append("var color = buttonColors[i];");
        //sbScript.Append("var darkColor = getTintedColor(color, -40);");
        //sbScript.Append("document.getElementById(buttonIds[i]).style.backgroundColor = darkColor;");
        sbScript.Append("if(color!=null) {");
        sbScript.Append("var moreDarkColor = getTintedColor(color, -130);");
        sbScript.Append("document.getElementById(buttonIds[i]).style.color = moreDarkColor;");
        sbScript.Append("}");
        sbScript.Append("}");
        sbScript.Append("} ");
        sbScript.Append("InitButtons(); ");

        // event handler
        sbScript.Append(" function FireMouserOverEvent(index)");
        sbScript.Append("{");
        sbScript.Append("var color = buttonColors[index];");
        sbScript.Append("if(color!=null) {");
        sbScript.Append("var lightColor = getTintedColor(color, 10);");
        //sbScript.Append("document.getElementById(buttonIds[index]).style.backgroundColor = lightColor;");
        // sbScript.Append("document.getElementById(buttonIds[index]).style.color = '#FFFFFF';");
        sbScript.Append("}");
        sbScript.Append("} ");

        sbScript.Append("function FireMouserOutEvent(index)");
        sbScript.Append("{");
        sbScript.Append("var color = buttonColors[index];");
        sbScript.Append("if(color!=null) {");
        //sbScript.Append("var darkColor = getTintedColor(color,-40 );");
        //sbScript.Append("document.getElementById(buttonIds[index]).style.backgroundColor = darkColor;");
        sbScript.Append("var moreDarkColor = getTintedColor(color, -130);");
        sbScript.Append("document.getElementById(buttonIds[index]).style.color = moreDarkColor;");
        sbScript.Append("}");
        sbScript.Append("} ");

        sbScript.Append("function FireMouserDownEvent(index)");
        sbScript.Append("{");
        sbScript.Append("var color = buttonColors[index];");
        sbScript.Append("if(color!=null) {");
        sbScript.Append("var darkColor = getTintedColor(color,-30 );");
        sbScript.Append("document.getElementById(buttonIds[index]).style.backgroundColor = darkColor;");
        sbScript.Append("}");
        sbScript.Append("} ");

        sbScript.Append("function FireMouserUpEvent(index)");
        sbScript.Append("{");
        sbScript.Append("var color = buttonColors[index];");
        sbScript.Append("if(color!=null) {");
        sbScript.Append("var lightColor = getTintedColor(color, 10);");
        sbScript.Append(" document.getElementById(buttonIds[index]).style.backgroundColor = lightColor;");
        sbScript.Append("}");
        sbScript.Append(" } ");

        // Animation Script
        sbScript.Append(" var timer;");
        sbScript.Append(" var op='+';");
        sbScript.Append(" var colorOffsetVal=0;");
        sbScript.Append(" function Animate()");
        sbScript.Append(" {");

        sbScript.Append(" for (var i = 0; i < buttonIds.length; i++) ");
        sbScript.Append(" {");
        sbScript.Append(" if (animateStats[i] == 'True') ");
        sbScript.Append(" {");
        sbScript.Append("	var color = buttonColors[i];");
        sbScript.Append(" var newColor = getTintedColor(color, colorOffsetVal);");
        sbScript.Append(" document.getElementById(buttonIds[i]).style.backgroundColor = newColor;");
        sbScript.Append(" }");
        sbScript.Append(" }");

        sbScript.Append(" if(op=='+')");
        sbScript.Append(" {");
        sbScript.Append("	colorOffsetVal=colorOffsetVal+5;");
        sbScript.Append(" }");
        sbScript.Append(" if(op=='-')");
        sbScript.Append(" {");
        sbScript.Append(" colorOffsetVal=colorOffsetVal-5;");
        sbScript.Append(" }");
        sbScript.Append(" if(colorOffsetVal>=60)");
        sbScript.Append(" {");
        sbScript.Append("	op = '-';");
        sbScript.Append(" }");
        sbScript.Append(" if(colorOffsetVal<=-60)");
        sbScript.Append(" {");
        sbScript.Append("	op = '+';");
        sbScript.Append(" }");
        sbScript.Append(" timer = setTimeout(Animate, 10);");
        sbScript.Append(" }");
        sbScript.Append(" Animate(); ");

        sbScript.Append("</script>");
        return sbScript.ToString();
    }

    private string GetHTML()
    {
        StringBuilder sbHtml = new StringBuilder();

        sbHtml.Append("<table style=\"width:100%;\">");

        int columnNo = 1;
        for (int i = 0; i < buttons.Count; i++)
        {
            if (columnNo == 1)
            {
                sbHtml.Append("<tr>");
            }
            sbHtml.Append("<td style=\"width:" + (100 / maxColumn) + "%;\">");
            string buttonHtml = "<div id=\"" + buttons[i].ID + "\" onmouseover=\"FireMouserOverEvent(" + i + ")\" onclick=\"" + buttons[i].ClientScript + "\" onmouseout=\"FireMouserOutEvent(" + i + ")\"" +
                            " onmousedown=\"FireMouserDownEvent(" + i + ")\" onmouseup=\"FireMouserUpEvent(" + i + ")\"" +
                            " class=\"colorbutton\" style=\"width: auto; background-color: " + GetHexCode(buttons[i].ButtonColor) + ";\">" +
                            "<span><em>" + buttons[i].Text + "</em> </span></div>";
            sbHtml.Append(buttonHtml);
            sbHtml.Append("</td>");

            columnNo++;
            if (columnNo > maxColumn)
            {
                sbHtml.Append("</tr>");
                columnNo = 1;
            }
        }
        if (columnNo <= maxColumn)
        {
            for (int j = columnNo; j <= maxColumn; j++)
            {
                sbHtml.Append("<td style=\"width:" + (100 / maxColumn) + "%;\">");
                sbHtml.Append("</td>");
            }
            sbHtml.Append("</tr>");
        }
        sbHtml.Append("</table>");
        return sbHtml.ToString();
    }

    private string GetHexCode(System.Drawing.Color color)
    {
        if (System.Drawing.Color.Transparent == color)
            return "Transparent";
        return string.Concat("#", (color.ToArgb() & 0x00FFFFFF).ToString("X6"));
    }
}
