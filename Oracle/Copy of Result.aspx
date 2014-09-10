<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Copy of Result.aspx.cs" Inherits="SecuredPages_Result" %>

<%@ Register Assembly="RadPanelbar.Net2" Namespace="Telerik.WebControls" TagPrefix="radP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%--<%@ Register Src="../ControlLibrary/crtlRemarkableDiscipline.ascx" TagName="crtlRemarkableDiscipline" TagPrefix="uc5" %>--%>
<%@ Register Src="~/Oracle/ControlLibrary/ucDemographics.ascx" TagName="Demogrphics" TagPrefix="ucDemo" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucEmergencyContact.ascx" TagName="EmergencyContactInfo" TagPrefix="ucEmergency" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucRDQuickLinks.ascx" TagName="RDQuickLinks" TagPrefix="ucRDQL" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucMainProviderDetails.ascx" TagName="MainProviderInfo" TagPrefix="ucMPI" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucQuickLinks.ascx" TagName="QuickLinks" TagPrefix="ucQL" %> 
<%@ Register Src="~/Oracle/ControlLibrary/ucRDShell.ascx" TagName="RDShell" TagPrefix="ucRDS" %>   
    
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="RadDock.Net2" Namespace="Telerik.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadSplitter.Net2" Namespace="Telerik.WebControls" TagPrefix="radspl" %>
<%@ Register Assembly="RadTabStrip.Net2" Namespace="Telerik.WebControls" TagPrefix="radTS" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Patient Details Page</title>
    <%--<link href="../RadControls/Dock/Skins/Web20/RadDockableObject.css" rel="Stylesheet" type="text/css"/>
    <link href="../RadControls/Window/Skins/Office2007/Window.css" rel="Stylesheet"  type="text/css"/>
    <link href="../RadControls/Panelbar/Skins/Office2007/styles.css" rel="Stylesheet" type="text/css" />--%>
</head>
<body>
    <form id="form1" runat="server">
        <radA:RadAjaxManager ID="RadAjaxManager1" runat="server" UseEmbeddedScripts="false">
        </radA:RadAjaxManager>
        <cc1:RadDockingManager ID="RadDockingManager1" runat="server" Skin="Web20" UseEmbeddedScripts="false" />
        <div style="width: 100%">
            <table width="100%">
                <%--<tr style="width: 100%">
                    <td>
                        <radTS:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2007" MultiPageID="RadMultiPage1"
                            SelectedIndex="0" BackColor="Transparent" OnClientTabSelected="OnSelected">
                            <Tabs>
                                <radTS:Tab ID="Tab1" runat="server" Text="*- First Page -*" PageViewID="PageView1">
                                </radTS:Tab>
                                <radTS:Tab ID="tpAddNew" runat="server" Text="Add New Page" Value="99">
                                </radTS:Tab>
                            </Tabs>
                        </radTS:RadTabStrip>

                        <script type="text/javascript">
                            function OnSelected(sender, eventArgs)
                            {
                                if(eventArgs.Tab.Value != null && eventArgs.Tab.Value==99)
                                {
                                    var tabStrip = <%= RadTabStrip1.ClientID %>;
                                    //var tbsFirstPage = tabStrip.FindTabByText("*- My First Page -*");
                                    var tbsFirstPage = tabStrip.FindTabById("RadTabStrip1_Tab1");
                                    //tabNewYork.Disable();
                                    //tabNewYork.Enable();
                                    if(tbsFirstPage)
                                    {
                                        tbsFirstPage.Select();
                                    }
                                    var oManager = GetRadWindowManager();
                                    if(oManager)
                                    {   
                                        var oWnd = oManager.GetWindowByName("DialogWindow");
                                        if(oWnd)
                                        {
                                            oWnd.SetSize(400,400);
                                            oWnd.SetUrl("AddNewContent.aspx"); 
                                            oWnd.Show();
                                        }
                                    }
                                    return false;
                                }
                            }
                        </script>

                    </td>
                </tr>--%>
                <tr valign="top">
                    <td style="padding-right: 5px; padding-left: 5px; vertical-align: top">
                        <radTS:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Width="100%" UseEmbeddedScripts="false">
                            <radTS:PageView ID="PageView1" runat="server">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 20%; vertical-align:top">
                                            <%--Lets try out with PanelBar--%>
                                            <radP:RadPanelbar ID="RadPanelbar1" runat="server" BorderColor="#7a96df" BorderStyle="Solid"
                                                BorderWidth="1" ExpandMode="SingleExpandedItem" Font-Size="Small" Height="100%"
                                                Skin="Web20" UseEmbeddedScripts="false">
                                                <Items>
                                                    <radP:RadPanelItem Text="Patient Demographics" runat="server" Expanded="true">
                                                        <Items>
                                                            <radP:RadPanelItem runat="server">
                                                                <ItemTemplate>
                                                                    <ucDemo:Demogrphics ID="ucDemo" runat="server" />
                                                                </ItemTemplate>
                                                            </radP:RadPanelItem>
                                                        </Items>
                                                    </radP:RadPanelItem>
                                                    <radP:RadPanelItem Text="Provider Information" runat="server">
                                                        <Items>
                                                            <radP:RadPanelItem runat="server">
                                                                <ItemTemplate>
                                                                    <ucMPI:MainProviderInfo ID="MainProviderDetail" runat="server" />
                                                                </ItemTemplate>
                                                            </radP:RadPanelItem>
                                                        </Items>
                                                    </radP:RadPanelItem>
                                                    <radP:RadPanelItem Text="Emergency Contact" runat="server">
                                                        <Items>
                                                            <radP:RadPanelItem runat="server">
                                                                <ItemTemplate>
                                                                    <ucEmergency:EmergencyContactInfo ID="ucEmergencyContact" runat="server" />
                                                                </ItemTemplate>
                                                            </radP:RadPanelItem>
                                                        </Items>
                                                    </radP:RadPanelItem>
                                                </Items>
                                                <%--<CollapseAnimation Type="InBack" Duration="20" />
                                                <ExpandAnimation Type="OutBack" Duration="10" />--%>
                                            </radP:RadPanelbar>
                                            <%--End of PanelBar Code--%>
                                            <%--Previous Code--%>
                                            <%--<cc1:RadDockingZone ID="RadDockingZoneLeft" runat="server" Width="100%" BackColor="Transparent">
                                                <cc1:RadDockableObject ID="RadDockPatientDemographics" runat="server" Text="Patient Demographic Information" Width="100%">
                                                    <ContentTemplate>
                                                        <ucDemo:Demogrphics ID="ucDemographics" runat="server" />
                                                    </ContentTemplate>
                                                </cc1:RadDockableObject>
                                                <cc1:RadDockableObject ID="RadDockPrincipalProviderInfo" runat="server" Text="Healthcare Provider Information" Width="100%">
                                                    <ContentTemplate>
                                                        <ucMPI:MainProviderInfo ID="MainProviderDetail" runat="server" />
                                                    </ContentTemplate>
                                                </cc1:RadDockableObject>
                                                <cc1:RadDockableObject ID="RadDockEmergencyContact" runat="server" Text="Emergency Contact Information"
                                                    Width="100%" Expanded="false">
                                                    <ContentTemplate>
                                                        <ucEmergency:EmergencyContactInfo ID="ucEmergencyContact" runat="server" />
                                                    </ContentTemplate>
                                                </cc1:RadDockableObject>
                                                <%--<cc1:RadDockableObject ID="RadDockQuickLinks" runat="server" Text="Quick Links" Width="100%">
                                                    <ContentTemplate>
                                                        <ucQL:QuickLinks ID="ucQuickLinks" runat="server" />
                                                    </ContentTemplate>
                                                </cc1:RadDockableObject>
                                            </cc1:RadDockingZone>--%>
                                        </td>
                                        <td style="width: 80%; vertical-align:top">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr valign="top">
                                                    <td>
                                                        <cc1:RadDockingZone ID="RadDockingZoneRightTop" runat="server" Width="100%" Height="100%" BackColor="Transparent" Type="Horizontal">
                                                            <table width="100%">
                                                                <tr style="vertical-align: top">
                                                                    <td style="width:70%">
                                                                        <cc1:RadDockableObject ID="RadDockableObject3" runat="server" AllowedDockingZoneTypes="Horizontal"
                                                                            DockingMode="AlwaysDock" Text="Remarkable Discipline(s) for This Patient" Width="100%">
                                                    
                                                                            <ContentTemplate>
                                                                                <ucRDQL:RDQuickLinks ID="RDQLinks" runat="server" />
                                                                            </ContentTemplate>
                                                                            <%--<Commands>
                                                                                <cc1:RadDockableObjectCommand Enabled="False" Name="Expand" ToolTip="Expand" />
                                                                                <cc1:RadDockableObjectCommand Name="Collapse" ToolTip="Collapse" />
                                                                            </Commands>--%>
                                                                        </cc1:RadDockableObject>
                                                                    </td>
                                                                    <td style="width:30%">
                                                                        <cc1:RadDockableObject ID="RadDockQuickLinks" runat="server" Text="Quick Links" DockingMode="AlwaysDock" 
                                                                                                  AllowedDockingZoneTypes="Horizontal" Width="100%">
                                                                            <ContentTemplate>
                                                                                <ucQL:QuickLinks ID="ucQuickLinks" runat="server" />
                                                                            </ContentTemplate>
                                                                            <%--<Commands>
                                                                                <cc1:RadDockableObjectCommand Enabled="False" Name="Expand" ToolTip="Expand" />
                                                                                <cc1:RadDockableObjectCommand Name="Collapse" ToolTip="Collapse" />
                                                                            </Commands>--%>
                                                                        </cc1:RadDockableObject> 
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </cc1:RadDockingZone>
                                                    </td>
                                                </tr>
                                                <tr valign="top">
                                                    <td>
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr valign="top">
                                                                <td style="width: 50%">
                                                                    <cc1:RadDockingZone ID="RadDockingZoneRightBottomLeft" runat="server" Width="100%" BackColor="Transparent">
                                                                        <cc1:RadDockableObject ID="RadDockableObject4" runat="server" Text="General Medicine(GM)" Width="100%">
                                                                            <ContentTemplate>
                                                                                <ucRDS:RDShell ID="RDShell1" runat="server" />
                                                                                <%--<uc5:crtlRemarkableDiscipline ID="CrtlRemarkableDiscipline1" runat="server" />--%>
                                                                            </ContentTemplate>
                                                                            <%--<Commands>
                                                                                <cc1:RadDockableObjectCommand Enabled="False" Name="Expand" ToolTip="Expand" />
                                                                                <cc1:RadDockableObjectCommand Name="Collapse" ToolTip="Collapse" />
                                                                            </Commands>--%>
                                                                        </cc1:RadDockableObject>
                                                                        <cc1:RadDockableObject ID="RadDockableObject6" runat="server" Text="Allergy of Medication(AM)" Width="100%" >
                                                                            <ContentTemplate>
                                                                                <ucRDS:RDShell ID="RDShell2" runat="server" />
                                                                                <%--<uc5:crtlRemarkableDiscipline ID="CrtlRemarkableDiscipline3" runat="server" />--%>
                                                                            </ContentTemplate>
                                                                            <%--<Commands>
                                                                                <cc1:RadDockableObjectCommand Enabled="False" Name="Expand" ToolTip="Expand" />
                                                                                <cc1:RadDockableObjectCommand Name="Collapse" ToolTip="Collapse" />
                                                                            </Commands>--%>
                                                                        </cc1:RadDockableObject>
                                                                        <cc1:RadDockableObject ID="RadDockableObject9" runat="server" Text="Gastroenterology(GI)"
                                                                            Width="100%">
                                                                            <ContentTemplate>
                                                                                <ucRDS:RDShell ID="RDShell3" runat="server" />
                                                                                <%--<uc5:crtlRemarkableDiscipline ID="CrtlRemarkableDiscipline5" runat="server" />--%>
                                                                            </ContentTemplate>
                                                                            <%--<Commands>
                                                                                <cc1:RadDockableObjectCommand Enabled="False" Name="Expand" ToolTip="Expand" />
                                                                                <cc1:RadDockableObjectCommand Name="Collapse" ToolTip="Collapse" />
                                                                            </Commands>--%>
                                                                        </cc1:RadDockableObject>
                                                                    </cc1:RadDockingZone>
                                                                </td>
                                                                <td style="width: 50%">
                                                                    <cc1:RadDockingZone ID="RadDockingZoneRightBottomRight" runat="server" BackColor="Transparent"
                                                                        Width="100%">
                                                                        <cc1:RadDockableObject ID="RadDockableObject5" runat="server" Text="Neurology(NU)" Width="100%">
                                                                            <ContentTemplate>
                                                                                <ucRDS:RDShell ID="RDShell4" runat="server" />
                                                                            </ContentTemplate>
                                                                            <%--<Commands>
                                                                                <cc1:RadDockableObjectCommand Enabled="False" Name="Expand" ToolTip="Expand" />
                                                                                <cc1:RadDockableObjectCommand Name="Collapse" ToolTip="Collapse" />
                                                                            </Commands>--%>
                                                                        </cc1:RadDockableObject>
                                                                        <cc1:RadDockableObject ID="RadDockableObject10" runat="server" Text="Immunology and Allergy(IA)"
                                                                            Width="100%">
                                                                            <ContentTemplate>
                                                                                <ucRDS:RDShell ID="RDShell5" runat="server" />
                                                                            </ContentTemplate>
                                                                            <%--<Commands>
                                                                                <cc1:RadDockableObjectCommand Enabled="False" Name="Expand" ToolTip="Expand" />
                                                                                <cc1:RadDockableObjectCommand Name="Collapse" ToolTip="Collapse" />
                                                                            </Commands>--%>
                                                                        </cc1:RadDockableObject>
                                                                        <cc1:RadDockableObject ID="RadDockableObject7" runat="server" Text="Prescription And Medication(PM)"
                                                                            Width="100%">
                                                                            <ContentTemplate>
                                                                                <ucRDS:RDShell ID="RDShell6" runat="server" />
                                                                            </ContentTemplate>
                                                                            <%--<Commands>
                                                                                <cc1:RadDockableObjectCommand Enabled="False" Name="Expand" ToolTip="Expand" />
                                                                                <cc1:RadDockableObjectCommand Name="Collapse" ToolTip="Collapse" />
                                                                            </Commands>--%>
                                                                        </cc1:RadDockableObject>
                                                                        
                                                                    </cc1:RadDockingZone>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </radTS:PageView>
                        </radTS:RadMultiPage>
                    </td>
                </tr>
            </table>
        </div>
        <radW:RadWindowManager ID="RadWindowManager1" runat="server" Height="500px" Width="600px"
            Skin="Office2007" ShowContentDuringLoad="true">
            <Windows>
                <radW:RadWindow ID="DialogWindow" runat="server" Modal="True" Skin="Office2007" />
            </Windows>
        </radW:RadWindowManager>
    </form>
</body>
</html>
