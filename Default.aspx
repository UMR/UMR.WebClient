<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SecuredPages_Default" %>

<%@ Register Src="~/Oracle/ControlLibrary/ucSearchGrid.ascx" TagName="crtlSearchGrid"
    TagPrefix="uc1" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="RadDock.Net2" Namespace="Telerik.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="RadDock.Net2" Namespace="Telerik.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadPanelbar.Net2" Namespace="Telerik.WebControls" TagPrefix="radP" %>
<%@ Register Assembly="RadSplitter.Net2" Namespace="Telerik.WebControls" TagPrefix="radspl" %>
<%@ Register Assembly="RadTabStrip.Net2" Namespace="Telerik.WebControls" TagPrefix="radTS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/tr/xhtml11/dtd/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" style="height: 100%; margion: 0px">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Temporary Navigation Link Page...</title>
    <%--<link href="../App_Themes/Default/Client.css" rel="stylesheet" type="text/css" />
    <link href="RadControls/Ajax/Scripts/1_6_2/RadAjaxNamespace.js" type="text/javascript" />
    <link href="RadControls/Ajax/Scripts/1_6_2/RadAjax.js" type="text/javascript" />
    <link href="RadControls/Grid/Scripts/4_5_2/RadAjaxNamespace.js" type="text/javascript" />
    <link href="RadControls/Grid/Scripts/4_5_2/RadGrid.js" type="text/javascript" />--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HyperLink ID="linkSearch" runat="server" NavigateUrl="~/PatientSearchDefault.aspx"
                Text="Patient Search Page" Font-Names="Tahoma" Font-Size="10pt">
            </asp:HyperLink>
            <br /><br />
            <%--<asp:HyperLink ID="linkPatientConfig" runat="server" NavigateUrl="~/Configuration/PatientConfig.aspx"
                Text="Patient Configuration Page" Font-Names="Tahoma" Font-Size="10pt">
            </asp:HyperLink>
            <br /><br />--%>
            <asp:HyperLink ID="linkConfig" runat="server" NavigateUrl="~/Configuration/UserConfig.aspx"
                Text="User Configuration Page" Font-Names="Tahoma" Font-Size="10pt">
            </asp:HyperLink>
             <br /><br />
           
            <asp:HyperLink ID="linkUsages" runat="server" NavigateUrl="~/Oracle/Usages.aspx"
                Text="Usage  History" Font-Names="Tahoma" Font-Size="10pt">
            </asp:HyperLink>
        </div>
    </form>
</body>
</html>
