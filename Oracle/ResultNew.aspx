<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResultNew.aspx.cs" Inherits="Oracle_ResultNew" %>


<%@ Register Assembly="RadPanelbar.Net2" Namespace="Telerik.WebControls" TagPrefix="radP" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<%@ Register Src="../ControlLibrary/crtlRemarkableDiscipline.ascx" TagName="crtlRemarkableDiscipline" TagPrefix="uc5" %>--%>
<%@ Register Src="~/Oracle/ControlLibrary/ucDemographics.ascx" TagName="Demogrphics"
    TagPrefix="ucDemo" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucEmergencyContact.ascx" TagName="EmergencyContactInfo"
    TagPrefix="ucEmergency" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucRDQuickLinks.ascx" TagName="RDQuickLinks"
    TagPrefix="ucRDQL" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucMainProviderDetails.ascx" TagName="MainProviderInfo"
    TagPrefix="ucMPI" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucQuickLinks.ascx" TagName="QuickLinks"
    TagPrefix="ucQL" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucInsuranceInfo.ascx" TagName="InsInfo"
    TagPrefix="ucII" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucRDShell.ascx" TagName="RDShell" TagPrefix="ucRDS" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucLegend.ascx" TagName="Legend" TagPrefix="uc" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucCommentsTop.ascx" TagName="CommentsAtTop"
    TagPrefix="uc" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucDisclaimer.ascx" TagName="Disclaimer"
    TagPrefix="uc" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucUnRD.ascx" TagName="UnRemarkableDisciplines"
    TagPrefix="uc" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="RadDock.Net2" Namespace="Telerik.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadSplitter.Net2" Namespace="Telerik.WebControls" TagPrefix="radspl" %>
<%@ Register Assembly="RadTabStrip.Net2" Namespace="Telerik.WebControls" TagPrefix="radTS" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Patient Details Page</title>
    <%--<link href="../RadControls/Dock/Skins/Web20/RadDockableObject.css" rel="Stylesheet" type="text/css"/>
    <link href="../RadControls/Window/Skins/Office2007/Window.css" rel="Stylesheet"  type="text/css"/>
    <link href="../RadControls/Panelbar/Skins/Office2007/styles.css" rel="Stylesheet" type="text/css" />--%>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        A:link
        {
	        color: Blue;
	        text-decoration: underline;
        }
        A:hover
        {
	        color: Red;
	        background-color: HighlightTxt;
	        text-decoration: underline;
        }
        A:visited
        {
	        color: Purple;
	        text-decoration: none;
        }
    </style>
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
        <cc1:RadDockingManager ID="RadDockingManager1" runat="server" Skin="Web20" UseEmbeddedScripts="false" />
        <%--<div>--%>
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr valign="top">
                <td colspan="3">
                    <uc:CommentsAtTop ID="ucComments" runat="server" />
                </td>
            </tr>
            <tr valign="top">
                <td id="offsetElement" style="height: 100%; width: 25%" valign="top">
                    <%-- <table width="100%">
                        <tr valign="top" >
                            <td style="height:100%" colspan="2">--%>
                    <cc1:RadDockingZone ID="ZoneQL" runat="server" BackColor="Transparent" Height="100%"
                        Type="Horizontal" Width="100%">
                        <cc1:RadDockableObject ID="RadDockableObject2" runat="server" AllowedDockingZones="ZoneQL"
                            AllowedDockingZoneTypes="Custom" Behavior="Pin" DockingMode="AlwaysDock" Text="Quick Links"
                            Width="100%">
                            <ContentTemplate>
                                <ucQL:QuickLinks ID="ucQuickLinks" runat="server" />
                            </ContentTemplate>
                        </cc1:RadDockableObject>
                    </cc1:RadDockingZone>
                    <%--</td>--%>
                </td>
                <td style="height: 100%; width: 75%">
                    <cc1:RadDockingZone ID="ZoneRDQL" runat="server" BackColor="Transparent" Height="100%"
                        Type="Horizontal" Width="100%">
                        <cc1:RadDockableObject ID="RadDockableObject1" runat="server" AllowedDockingZones="ZoneRDQL"
                            AllowedDockingZoneTypes="Custom" Behavior="Pin" DockingMode="AlwaysDock" Text="Remarkable Discipline(s)"
                            Width="100%">
                            <ContentTemplate>
                                <ucRDQL:RDQuickLinks ID="RDQLinks" runat="server" />
                            </ContentTemplate>
                        </cc1:RadDockableObject>
                    </cc1:RadDockingZone>
                </td>
            </tr>
            <tr valign="top">
                <td style="width: 20%">
                    <cc1:RadDockingZone ID="RadDockingZoneLeft" runat="server" BackColor="Transparent"
                        Width="100%">
                        <cc1:RadDockableObject ID="RadDockPatientDemographics" runat="server" Behavior="Pin"
                            Text="Patient Demographic Information" Width="100%">
                            <ContentTemplate>
                                <ucDemo:Demogrphics ID="ucDemo" runat="server" />
                            </ContentTemplate>
                        </cc1:RadDockableObject>
                        <cc1:RadDockableObject ID="RadDockPrincipalProviderInfo" runat="server" Height="20px"
                            Text="Healthcare Provider Information" Width="100%">
                            <ContentTemplate>
                                <ucMPI:MainProviderInfo ID="MainProviderDetail" runat="server" />
                            </ContentTemplate>
                        </cc1:RadDockableObject>
                        <cc1:RadDockableObject ID="RadDockEmergencyContact" runat="server" Expanded="true"
                            Height="20px" Text="Emergency Contact Information" Width="100%">
                            <ContentTemplate>
                                <ucEmergency:EmergencyContactInfo ID="ucEmergencyContact" runat="server" />
                            </ContentTemplate>
                        </cc1:RadDockableObject>
                        <cc1:RadDockableObject ID="RadDockInsuranceInfo" runat="server" Height="20px" Text="Insurance Information"
                            Width="100%">
                            <ContentTemplate>
                                <ucII:InsInfo ID="ucInsInfo" runat="server" />
                            </ContentTemplate>
                        </cc1:RadDockableObject>
                    </cc1:RadDockingZone>
                </td>
                <td style="width: 80%">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr valign="top">
                            <td style="width: 50%">
                                <cc1:RadDockingZone ID="ZoneRightBottomLeft" runat="server" BackColor="Transparent"
                                    Width="100%">
                                    <cc1:RadDockableObject ID="UnRD" runat="server" AllowedDockingZones="ZoneRightBottomLeft"
                                        AllowedDockingZoneTypes="custom" Behavior="Pin" DockingMode="alwaysdock" Text="Unremarkable Discipline(s)"
                                        Width="100%">
                                        <ContentTemplate>
                                            <uc:UnRemarkableDisciplines ID="UnRD" runat="server" />
                                        </ContentTemplate>
                                    </cc1:RadDockableObject>
                                </cc1:RadDockingZone>
                            </td>
                            <td style="width: 50%">
                                <cc1:RadDockingZone ID="ZoneRightBottomRight" runat="server" BackColor="Transparent"
                                    Width="100%">
                                </cc1:RadDockingZone>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <cc1:RadDockingZone ID="ZoneLegend" runat="server" Width="100%">
                        <cc1:RadDockableObject ID="dockLegend" runat="server" AllowedDockingZones="ZoneLegend"
                            AllowedDockingZoneTypes="custom" Behavior="pin" DockingMode="AlwaysDock" Text="Legend"
                            TitleBarStyle-Font-Bold="true" TitleBarStyle-HorizontalAlign="Center" Width="100%">
                            <ContentTemplate>
                                <uc:Legend ID="Legend" runat="server" />
                            </ContentTemplate>
                        </cc1:RadDockableObject>
                    </cc1:RadDockingZone>
                    <uc:Disclaimer ID="Disclaimer" runat="server" />
                </td>
            </tr>
        </table>
        <%--<radA:RadAjaxManager ID="RadAjaxManagerResult" runat="server" UseEmbeddedScripts="false"
            OnAjaxRequest="RadAjaxManagerResult_AjaxRequest">
        </radA:RadAjaxManager>--%>

        <script type="text/javascript">
                var currentLoadingPanel = null;
                var currentUpdatedControl = null;
                function CallBackFunction(OptionList)
                {   currentLoadingPanel = RadAjaxNamespace.LoadingPanels["<%= AjaxLoadingPanel.ClientID %>"];
                    currentUpdatedControl = "<%=ZoneRDQL.ClientID %>"; currentLoadingPanel.Show(currentUpdatedControl);
                    CallServerToSaveUserPreferences(OptionList);}
				function ReceiveServerData(arg)
				{   if (currentLoadingPanel != null)currentLoadingPanel.Hide(currentUpdatedControl);
                    currentUpdatedControl = null; currentLoadingPanel = null; alert(arg);}
                function HideStatus(){window.status="Click to see the details...";return true;}
                function ShowRemarkableDisciplineDrillDown(id,modifierID,medCode,disType){window.radopen("RDDetails.aspx?ID="+id+"&ModifierID="+modifierID+"&medCode="+medCode+"&disType="+disType, null);return false;}
                function ShowRemarkableDiscipline(url){window.radopen(url, null);return false;}
        </script>

        <radA:AjaxLoadingPanel ID="AjaxLoadingPanel" runat="server" MinDisplayTime="1000"
            Transparency="25" Width="100%">
            <asp:Image ID="imgLoadPanel" runat="server" AlternateText="Saving..." ImageUrl="~/RadControls/Ajax/Skins/Default/LoadingProgressBar.gif" />
            <asp:Label ID="msgPref" runat="server" Font-Bold="true" Font-Names="Verdana, Arial, Tahoma"
                Font-Size="10pt" ForeColor="red">Saving...</asp:Label>
        </radA:AjaxLoadingPanel>
        <radW:RadWindowManager ID="RadWindowManager1" runat="server" Height="325px" Left="30"
            Modal="True" ShowContentDuringLoad="true" Skin="Office2007" Top="20" UseEmbeddedScripts="false"
            Width="650px">
        </radW:RadWindowManager>
    </form>
    <%--<asp:Label ID="InjectScriptLabelSaved" runat="server"></asp:Label>--%>
</body>
</html>
