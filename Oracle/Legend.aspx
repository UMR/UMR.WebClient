<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Legend.aspx.cs" Inherits="Oracle_Legend" %>

<%@ Register Src="~/Oracle/ControlLibrary/ucLegend.ascx" TagName="Legend" TagPrefix="uc" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Legend</title>
    <link type="text/css" rel="Stylesheet" href="../StyleSheet.css" />
    <style type="text/css" media="print">
        .noprint 
        {
            display:none 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td align="right" class="noprint">
                    <img alt="Edit Legend Button" src="images/edit_legend_button.png" onclick="return editLegend_onClick()"
                        style="cursor: hand" />
                    <img alt="Fax" src="../Oracle/images/fax_button.jpg" onclick="return faxButton_onclick()"
                        style="cursor: pointer;"  />
                    <img alt="Email" src="../Oracle/images/email_button.jpg" onclick="return emailButton_onclick()"
                        style="cursor: hand" />
                    <img alt="Print" src="../Oracle/images/print_button.jpg" onclick="return printButton_onclick()"
                        style="cursor: hand" />
                </td>
            </tr>
        </table>
        <div id="displayDiv">
            <uc:Legend ID="Legend" runat="server" />
        </div>
        <%--<div style="text-align: right; color: Blue; font-family: Tahoma; font-size: 8pt;
            font-weight: bold; padding-right: 5px; cursor: hand;">
            <a onclick="editLegend_onClick()">Edit Legend</a>
        </div>--%>
    </form>
    <form name="gotoEmailForm" action="Email.aspx" method="post">
        <input type="hidden" name="emailHidden" id="emailHidden" />
        <input type="hidden" name="subject" id="subject" value="Legend" />
    </form>
    <form name="gotoFaxForm" action="Fax.aspx" method="post">
        <input type="hidden" name="faxHidden" id="faxHidden" />
        <input type="hidden" name="subjectFax" id="subjectFax" value="Legend" />
    </form>
    <radW:RadWindowManager ID="RadWindowManager2" runat="server" Skin="Office2007" Behavior="Close"
        InitialBehavior="maximize" VisibleStatusbar="false" UseEmbeddedScripts="false">
    </radW:RadWindowManager>

    <script type="text/javascript">
        function GetRadWindow()
	    {
		        var oWindow = null;
		        if (window.radWindow) oWindow = window.radWindow;
		        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
		        return oWindow;
	    } 
        //GetRadWindow().SetWidth(850);
        function printButton_onclick() 
        {
            window.print();
        }
        function emailButton_onclick()
        {
             var emailText= document.getElementById("displayDiv").innerHTML;   
             document.getElementById("emailHidden").value =emailText;
             document.gotoEmailForm.submit();
        }
        function faxButton_onclick() 
        {
            var faxText= document.getElementById("displayDiv").innerHTML;   
            document.getElementById("faxHidden").value =faxText;
            document.gotoFaxForm.submit();
        }
        function editLegend_onClick()
        {
            var queryString="<%= GetQueryString() %>";
            window.radopen("LegendEdit.aspx?"+queryString);
        }
        function GetParentWindow()
        {
            return GetRadWindow().BrowserWindow;
        }
        var wnd=GetRadWindow();
	    wnd.OnClientClose = function(wnd)
        {
            var parent= GetParentWindow();
            var allWindows = GetRadWindowManager().GetWindowObjects();
	        if(allWindows.length>0)
	        { 
	            var win = allWindows[0];
	            var isSaved=win.GetContentFrame().contentWindow.IsSaved();
	            if(isSaved)
	            {
	                parent.location.href= parent.location.href;
	            }
	        }
        }
    </script>

</body>
</html>
