<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPreview.aspx.cs" Inherits="Oracle_PrintPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadDock.Net2" Namespace="Telerik.WebControls" TagPrefix="cc1" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucQuickLinks.ascx" TagName="QuickLinks"
    TagPrefix="ucQL" %>
<%@ Register Src="~/Oracle/ControlLibrary/PrintPreview/ucRDQuickLinksPreview.ascx"
    TagName="RDQuickLinks" TagPrefix="ucRDQL" %>
<%@ Register Src="~/Oracle/ControlLibrary/PrintPreview/ucPatientDemographicPreview.ascx"
    TagName="Demographic" TagPrefix="uc" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucCommentsTop.ascx" TagName="CommentsAtTop"
    TagPrefix="uc" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucDisclaimer.ascx" TagName="Disclaimer"
    TagPrefix="uc" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucLegend.ascx" TagName="Legend" TagPrefix="uc" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucInsuranceInfo.ascx" TagName="InsuranceInfo"
    TagPrefix="uc" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucEmergencyContact.ascx" TagName="EmergencyContactInfo"
    TagPrefix="uc" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucMainProviderDetails.ascx" TagName="MainProviderInfo"
    TagPrefix="uc" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucAMD.ascx" TagName="AMD" TagPrefix="uc" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucUnRD.ascx" TagName="UnRD" TagPrefix="uc" %>
<%@ Register Src="~/Oracle/ControlLibrary/PrintPreview/ucRDQuickLinksButtonsPreview.ascx"
    TagName="RDQLButtons" TagPrefix="uc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css" media="print">
        .noprint 
        {
            display:none 
        }
    </style>
    <link href="../ColorButton/ButtonStyle.css" rel="stylesheet" type="text/css" />
    <title>Preview Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <cc1:RadDockingManager ID="RadDockMgr" runat="Server" Skin="Web20" UseEmbeddedScripts="false" />
        <table width="100%">
            <tr>
                <td align="right" class="noprint">
                </td> 
                <td align="left" class="noprint" style="width:50px;">
                    <img alt="Fax" src="../Oracle/images/fax_button.jpg" style="cursor: pointer;" />
                </td> 
                <td align="left" class="noprint" style="width:50px;">
                    <img alt="Email" src="../Oracle/images/email_button.jpg" onclick="return emailButton_onclick()" style="cursor: pointer;" />
                </td>    
                <td align="left" class="noprint" style="width:50px;">
                    <img src="images/printbutton.png" alt="Print" onclick="return printButton_onclick()" style="cursor: hand" />
                </td>
            </tr>
        </table>
        <div id="displayDiv">
            <%--<h4>This page is still under construction...</h4>--%>
            <table style="width: 100%; vertical-align: top">
                <tr valign="top">
                    <td colspan="3">
                        <uc:CommentsAtTop ID="ucComments" runat="server" />
                    </td>
                </tr>
                <tr valign="top" style="height: auto; width: 100%">
                    <td id="offsetElement" valign="top" style="background-image: '../RadControls/Dock/Skins/Web20/Img/titleBarBg.gif'">
                        <cc1:RadDockingZone ID="ZoneQL" runat="server" BackColor="Transparent" Type="Horizontal"
                            Width="100%">
                            <cc1:RadDockableObject ID="RadDockableObject2" runat="server" AllowedDockingZoneTypes="Custom"
                                AllowedDockingZones="ZoneQL" Behavior="Pin" DockingMode="AlwaysDock" Text="Table of Contents"
                                Font-Bold="true" Font-Size="12pt" Width="100%" Height="100%">
                                <ContentTemplate>
                                    <ucQL:QuickLinks ID="ucQuickLinks" runat="server" />
                                    <cc1:RadDockingZone ID="ZoneRDQL" runat="server" BackColor="Transparent" Type="Horizontal"
                                        Width="100%">
                                        <cc1:RadDockableObject ID="RadDockableObject1" runat="server" AllowedDockingZones="ZoneRDQL"
                                            AllowedDockingZoneTypes="Custom" Behavior="Pin" DockingMode="AlwaysDock" Text="Remarkable Disciplines"
                                            Width="100%" Height="100%">
                                            <ContentTemplate>
                                                <%--<ucRDQL:RDQuickLinks ID="RDQLinks" runat="server" />--%>
                                                <uc:RDQLButtons ID="RDQLinks" runat="server" />
                                            </ContentTemplate>
                                            <TitleBarStyle Font-Size="7pt" Font-Names="Tahoma, Arial, Verdana" />
                                        </cc1:RadDockableObject>
                                    </cc1:RadDockingZone>
                                </ContentTemplate>
                                <TitleBarStyle Font-Size="11pt" Font-Names="Tahoma, Arial" />
                            </cc1:RadDockableObject>
                        </cc1:RadDockingZone>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; vertical-align: top">
                <%--<tr>
                    <td valign="top">
                        <cc1:RadDockingZone ID="ZoneProviderInfo" runat="server" Width="100%">
                            <cc1:RadDockableObject ID="DockProviderInfo" runat="server" Text="Primary Provider Information"
                                Width="100%" Height="100%" Behavior="Pin" DockingMode="AlwaysDock">
                                <ContentTemplate>
                                    <br />
                                    <asp:LinkButton ID="lbHPL" runat="server" Text="Healthcare Provider(s) Information"
                                        Font-Names="Arial, Verdana" Font-Size="8pt" Style="width: 80px; padding: 0px;" />
                                    <br />
                                    <uc:MainProviderInfo ID="MainProviderInfo" runat="server" />
                                </ContentTemplate>
                            </cc1:RadDockableObject>
                        </cc1:RadDockingZone>
                    </td>
                </tr>--%>
                <%--Next <TR> </TR> => Trial run to fit more stuff inside provider info Dock--%>
                <%-- <TR> </TR> => Trial run ends here...--%>
                <%--<tr>
                    <td valign="top">
                        <cc1:RadDockingZone ID="ZoneAMD" runat="server" Width="100%">
                            <cc1:RadDockableObject ID="DockAMD" runat="server" Text="Advanced Medical Directive(s)"
                                Width="100%" Height="100%" Behavior="Pin" DockingMode="AlwaysDock">
                                <ContentTemplate>
                                    <uc:AMD ID="AMD" runat="server" />
                                </ContentTemplate>
                            </cc1:RadDockableObject>
                        </cc1:RadDockingZone>
                    </td>
                </tr>--%>
                <%--<tr>
                    <td valign="top">
                        <cc1:RadDockingZone ID="ZoneMisc" runat="server" Width="100%">
                            <cc1:RadDockableObject ID="DockMisc" runat="server" Width="100%" Height="100%" Behavior="Pin" DockingMode="AlwaysDock">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lbMPI" runat="server" Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lbLRP" runat="server" Font-Size="8pt" Font-Names="Tahoma, Arial, Verdana" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lbLRA" runat="server" Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </cc1:RadDockableObject>
                        </cc1:RadDockingZone>
                    </td>
                </tr>--%>
            </table>
            <%--This is where the Remarkable Disciplines will go...--%>
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td valign="top">
                        <cc1:RadDockingZone ID="ZoneRDLeft" runat="server" Width="100%">
                        </cc1:RadDockingZone>
                    </td>
                    <%--<td valign="top">
                        <cc1:RadDockingZone ID="ZoneMiddle" runat="server" Width="100%">
                        </cc1:RadDockingZone>
                    </td>
                    <td valign="top">
                        <cc1:RadDockingZone ID="ZoneRDRight" runat="server" Width="100%">
                        </cc1:RadDockingZone>
                    </td>--%>
                </tr>
                <%-- Unremarkable Disciplines go here --%>
                <tr style="width: 100%">
                    <td valign="top" colspan="2">
                        <cc1:RadDockingZone ID="ZoneUnremarkable" runat="server" Width="100%">
                            <cc1:RadDockableObject ID="DockUnRD" runat="server" Text="The following Disciplines were found UNREMARKABLE"
                                Width="100%" Height="100%" Behavior="Pin" DockingMode="AlwaysDock">
                                <ContentTemplate>
                                    <uc:UnRD ID="UnRD" runat="server" />
                                </ContentTemplate>
                                <TitleBarStyle Font-Underline="True" />
                            </cc1:RadDockableObject>
                        </cc1:RadDockingZone>
                    </td>
                </tr>
                
                <%-- MDS go here --%>
                <tr style="width: 100%">
                    <td valign="top">
                        <%--<cc1:RadDockingZone ID="ZoneMDSHeader" runat="server" Width="100%">
                            <cc1:RadDockableObject ID="MDSHeader" runat="server" Text="The following MDS were found"
                                Width="100%" Height="100%" Behavior="Pin" DockingMode="AlwaysDock">
                                <ContentTemplate>
                                    <uc:UnRD ID="UnRD" runat="server" />
                                </ContentTemplate>
                                <TitleBarStyle Font-Underline="True" />
                            </cc1:RadDockableObject>
                        </cc1:RadDockingZone>--%>
                        <cc1:RadDockingZone ID="ZoneMDS" runat="server" Width="100%">

                        </cc1:RadDockingZone>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td valign="top" colspan="3">
                        <cc1:RadDockingZone ID="ZoneDemographics" runat="server" Width="100%">
                            <cc1:RadDockableObject ID="dockDemogrphic" runat="server" Text="Demographics" Width="100%"
                                Height="100%" Behavior="Pin" DockingMode="AlwaysDock">
                                <ContentTemplate>
                                    <uc:Demographic ID="ucDemographics" runat="server" />
                                </ContentTemplate>
                            </cc1:RadDockableObject>
                        </cc1:RadDockingZone>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td valign="top">
                        <cc1:RadDockingZone ID="ZoneInsuracne" runat="server" Width="100%">
                            <cc1:RadDockableObject ID="DockIns" runat="server" Text="Insurance Company Information"
                                Width="100%" Height="100%" Behavior="Pin" DockingMode="AlwaysDock">
                                <ContentTemplate>
                                    <uc:InsuranceInfo ID="InsInfo" runat="server" />
                                </ContentTemplate>
                            </cc1:RadDockableObject>
                        </cc1:RadDockingZone>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td valign="top">
                        <cc1:RadDockingZone ID="ZoneEmergency" runat="server" Width="100%">
                            <cc1:RadDockableObject ID="DockEmergency" runat="server" Text="Emergency Contact Information"
                                Width="100%" Height="100%" Behavior="Pin" DockingMode="AlwaysDock">
                                <ContentTemplate>
                                    <uc:EmergencyContactInfo ID="Emergency" runat="server" />
                                </ContentTemplate>
                            </cc1:RadDockableObject>
                        </cc1:RadDockingZone>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <cc1:RadDockingZone ID="ZoneMisc" runat="server" Width="100%">
                            <table>
                                <tr style="width: 100%">
                                    <td valign="top" style="width: 40%">
                                        <cc1:RadDockableObject ID="DockProvider" runat="server" Text="Primary Provider Information"
                                            Width="100%" Height="100%" Behavior="Pin" DockingMode="AlwaysDock">
                                            <ContentTemplate>
                                                <br />
                                                <asp:LinkButton ID="lbHPL1" runat="server" Text="Healthcare Providers' Information"
                                                    Font-Names="Arial, Verdana" Font-Size="8pt" Style="width: 80px; padding: 0px;" />
                                                <br />
                                                <uc:MainProviderInfo ID="MainProviderInfo" runat="server" />
                                            </ContentTemplate>
                                        </cc1:RadDockableObject>
                                    </td>
                                    <td valign="top" style="width: 20%">
                                        <cc1:RadDockableObject ID="RadDockableObject3" runat="server" Text="Advanced Medical Directives"
                                            Width="100%" Height="100%" Behavior="Pin" DockingMode="AlwaysDock">
                                            <ContentTemplate>
                                                <uc:AMD ID="AMD" runat="server" />
                                            </ContentTemplate>
                                        </cc1:RadDockableObject>
                                    </td>
                                    <td valign="top" style="width: 40%">
                                        <cc1:RadDockableObject ID="DockMisc" runat="server" Width="100%" Height="100%" Behavior="Pin"
                                            DockingMode="AlwaysDock">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="lbMPI" runat="server" Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server" visible="false">
                                                            <asp:LinkButton ID="lbLRP" runat="server" Font-Size="8pt" Font-Names="Tahoma, Arial, Verdana" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="lbLRA" runat="server" Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc1:RadDockableObject>
                                    </td>
                                </tr>
                            </table>
                        </cc1:RadDockingZone>
                    </td>
                    <%--<td valign="top">
                        <cc1:RadDockingZone ID="RadDockingZone1" runat="server" Width="100%">
                            
                        </cc1:RadDockingZone>
                    </td>--%>
                    <%--<td valign="top">
                        <cc1:RadDockingZone ID="ZoneMisc" runat="server" Width="100%">
                            
                        </cc1:RadDockingZone>
                    </td>--%>
                </tr>
                <%-- Legends and Disclaimer goes here  --%>
                <tr>
                    <td>
                        <cc1:RadDockingZone ID="ZoneLegend" runat="server" Width="100%">
                            <cc1:RadDockableObject ID="dockLegend" runat="server" Text="Legend" Behavior="pin"
                                Width="100%" Height="100%" AllowedDockingZoneTypes="custom" AllowedDockingZones="ZoneLegend"
                                DockingMode="AlwaysDock" TitleBarStyle-Font-Bold="true" TitleBarStyle-HorizontalAlign="Center">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 20%">
                                            </td>
                                            <td style="width: 60%">
                                                <uc:Legend ID="Legend" runat="server" />
                                            </td>
                                            <td style="width: 20%">
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </cc1:RadDockableObject>
                        </cc1:RadDockingZone>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc:Disclaimer ID="Disclaimer1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
                var currentLoadingPanel = null;
                var currentUpdatedControl = null;
//                
//				function ReceiveServerData(arg)
//				{   if (currentLoadingPanel != null)currentLoadingPanel.Hide(currentUpdatedControl);
//                    currentUpdatedControl = null; currentLoadingPanel = null; alert(arg);}
                function HideStatus(){window.status="Click to see the details...";return true;}
                //function ShowRemarkableDisciplineDrillDown(id,modifierID,medCode,disType){window.radopen("RDDetails.aspx?ID="+id+"&ModifierID="+modifierID+"&medCode="+medCode+"&disType="+disType, null);return false;}
                //function ShowRemarkableDiscipline(url){window.radopen(url, null);return false;}
                function ShowMPI(patientKey)
                {
                    window.radopen("MPI.aspx?PatientKey="+patientKey, null);
                    return false;
                }
                function ShowLRPM(patientKey)
                {
                    window.radopen("LRPM.aspx?PatientKey="+patientKey, null);
                    return false;
                }
                function ShowLRA(patientKey)
                {
                    window.radopen("LRA.aspx?PatientKey="+patientKey, null);
                    return false;
                }
                function ShowHPList(patientKey,dispType)
                {
                    //alert('inside ShowHPList');
                    window.radopen("MultiProviderListDetails.aspx?PatientKey="+patientKey+"&DispType="+dispType, null);
                    return false;
                }
                function ShowColor(id,colorCode)
                {
                    //alert(id);
                    var link = document.getElementById(id);
                    if(link)
                    {
                        //alert(link.firstChild.nodeValue);
                        //alert(colorCode);
                        //alert(link.style.backgroundColor);
                        link.style.color = 'red'; //colorCode;
                        //document.body.bgColor = 'red';
                    }

                }
                function emailButton_onclick() {
                    var emailText = document.getElementById("displayDiv").innerHTML;
                    document.getElementById("emailHidden").value = emailText;
                    document.gotoEmailForm.submit();
                }
                function printButton_onclick() {
                    window.status="";
                    print();
                }
                function SetVisited(discName)
                {
                    for(var j=0;j<buttonIds.length;j++)
			        { 
			  	        if(buttonIds[j]==discName+'Button')
				        {  
				           animateStats[j]='False';
					       break;
				        }
			         }
                }
                
                function GetRadWindow()
		        {
			        var oWindow = null;
			        if (window.radWindow) oWindow = window.radWindow;
			        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
			        return oWindow;
		        } 
		        var wnd=GetRadWindow();
                wnd.OnClientClose = function(wnd)
                {
                   //do nothing
                }
        </script>

        <radW:RadWindowManager ID="RadWindowManagerPreview" runat="server" Height="375px"
            Width="800px" Skin="Office2007" ShowContentDuringLoad="true" Modal="True" UseEmbeddedScripts="false"
            Top="20" Left="30">
        </radW:RadWindowManager>
    </form>

    <form name="gotoEmailForm" action="Email.aspx" method="post">
        <input type="hidden" name="emailHidden" id="emailHidden" />
        <input type="hidden" name="subject" id="subject" value="Advanced Medical Directives" />
    </form>
</body>
</html>
