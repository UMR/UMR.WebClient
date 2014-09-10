<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Copy of Default.aspx.cs"
    Inherits="SecuredPages_Default" %>

<%@ Register Src="~/Oracle/ControlLibrary/ucSearchGrid.ascx" TagName="crtlSearchGrid"
    TagPrefix="uc1" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="RadDock.Net2" Namespace="Telerik.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="RadDock.Net2" Namespace="Telerik.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadPanelbar.Net2" Namespace="Telerik.WebControls" TagPrefix="radP" %>
<%@ Register Assembly="RadSplitter.Net2" Namespace="Telerik.WebControls" TagPrefix="radspl" %>
<%@ Register Assembly="RadTabStrip.Net2" Namespace="Telerik.WebControls" TagPrefix="radTS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Collaspable Search And Result</title>
    <link href="../App_Themes/Default/Client.css" rel="stylesheet" type="text/css" />
</head>
<body >
    <form id="form1" runat="server" >
        <radA:RadAjaxManager ID="RadAjaxManagerDefault" runat="server" UseEmbeddedScripts="false">
        </radA:RadAjaxManager>
        <cc1:RadDockingManager ID="RadDockingManager1" runat="server" Skin="Web20" UseEmbeddedScripts="false" />
        <radspl:RadSplitter ID="RadSplitter1" runat="server" Orientation="Horizontal"
                FullScreenMode="true" PanesBorderSize="0" BorderStyle="None" UseEmbeddedScripts="false">   
            <radspl:RadPane ID="s" runat="server" Height="15px" Scrolling="None" UseEmbeddedScripts="false">                         
                <a href="javascript:ToggleCollapsePane('Radpane12')" style="float: right; vertical-align: top;
                            font: Tahoma; font-size: 10px; font-weight: bold; color: Black;">Click to Show/Hide Search Screen</a>
                <script type="text/javascript">    
    function ToggleCollapsePane(paneID)
                {
                    var splitter = <%= RadSplitter1.ClientID%>;
                    var pane = splitter.GetPaneById(paneID);

                    if (!pane) return;

                    if (pane.IsCollapsed()) 
                    {
                        pane.Expand();
                    } 
                    else
                    {
                        pane.Collapse();
                    }
                }       
    </script>
            </radspl:RadPane>
            <radspl:RadPane ID="Radpane12" runat="server" Scrolling="none" Height="80px" UseEmbeddedScripts="false">
                <div>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%" align="center">
                            <uc1:crtlSearchGrid ID="CrtlSearchGrid2" runat="server" />                                
                            </td>                           
                        </tr>
                    </table>
                </div>
            </radspl:RadPane>
            <radspl:RadPane ID="ContentPane" runat="server" Scrolling="X" ContentUrl="about:Blank" UseEmbeddedScripts="false">
            </radspl:RadPane>
        </radspl:RadSplitter>       
    
    </form>
</body>
</html>
