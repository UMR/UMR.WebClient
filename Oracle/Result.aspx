<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Result.aspx.cs" Inherits="SecuredPages_Result" %>

<%@ Register Assembly="RadPanelbar.Net2" Namespace="Telerik.WebControls" TagPrefix="radP" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<%@ Register Src="../ControlLibrary/crtlRemarkableDiscipline.ascx" TagName="crtlRemarkableDiscipline" TagPrefix="uc5" %>--%>
<%--<%@ Register Src="~/Oracle/ControlLibrary/ucDemographics.ascx" TagName="Demogrphics" TagPrefix="ucDemo" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucEmergencyContact.ascx" TagName="EmergencyContactInfo" TagPrefix="ucEmergency" %>--%>
<%@ Register Src="~/Oracle/ControlLibrary/ucRDQuickLinks.ascx" TagName="RDQuickLinks"
    TagPrefix="ucRDQL" %>
<%--<%@ Register Src="~/Oracle/ControlLibrary/ucMainProviderDetails.ascx" TagName="MainProviderInfo" TagPrefix="ucMPI" %>--%>
<%@ Register Src="~/Oracle/ControlLibrary/ucQuickLinks.ascx" TagName="QuickLinks"
    TagPrefix="ucQL" %>
<%--<%@ Register Src="~/Oracle/ControlLibrary/ucInsuranceInfo.ascx" TagName="InsInfo" TagPrefix="ucII" %>--%>
<%--<%@ Register Src="~/Oracle/ControlLibrary/ucRDShell.ascx" TagName="RDShell" TagPrefix="ucRDS" %>--%>
<%--<%@ Register Src="~/Oracle/ControlLibrary/ucLegend.ascx" TagName="Legend" TagPrefix="uc" %>--%>
<%@ Register Src="~/Oracle/ControlLibrary/ucCommentsTop.ascx" TagName="CommentsAtTop"
    TagPrefix="uc" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucDisclaimer.ascx" TagName="Disclaimer"
    TagPrefix="uc" %>
<%--<%@ Register Src="~/Oracle/ControlLibrary/ucUnRD.ascx" TagName="UnRemarkableDisciplines" TagPrefix="uc" %>--%>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="RadDock.Net2" Namespace="Telerik.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadSplitter.Net2" Namespace="Telerik.WebControls" TagPrefix="radspl" %>
<%--<%@ Register Assembly="RadTabStrip.Net2" Namespace="Telerik.WebControls" TagPrefix="radTS" %>--%>
<%@ Register Src="~/Oracle/ControlLibrary/ucRDQuickLinksButtons.ascx" TagName="RDQLButtons"
    TagPrefix="uc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Patient Details Page</title>
    <%--<link href="../RadControls/Dock/Skins/Web20/RadDockableObject.css" rel="Stylesheet" type="text/css"/>
    <link href="../RadControls/Window/Skins/Office2007/Window.css" rel="Stylesheet"  type="text/css"/>
    <link href="../RadControls/Panelbar/Skins/Office2007/styles.css" rel="Stylesheet" type="text/css" />--%>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../ColorButton/ButtonStyle.css" rel="stylesheet" type="text/css" />
    <%--<style type="text/css">
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
    </style>--%>
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
        <cc1:RadDockingManager ID="RadDockingManager1" runat="server" Skin="Web20" UseEmbeddedScripts="false" />
        <%--<div>--%>
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr valign="top" style="width: 100%">
                <td colspan="3">
                    <table width="100%">
                        <tr>
                            <td>
                                <uc:CommentsAtTop ID="ucComments" runat="server" />
                            </td>
                            <td align="right">
                                 <asp:Button ID="btnDoctorAlertNetwork" runat="server" 
                                     Text="Doctor Alert Network" Font-Names="Tahoma"
                                    Font-Size="8pt" Width="190px" 
                                     onclick="btnDoctorAlertNetwork_Click"/>
                                 <%--<asp:Button ID="btnIMergencyTransferFormRequest" runat="server" 
                                     Text="Emergency Transfer Form Request" Font-Names="Tahoma"
                                    Font-Size="8pt" Width="190px" 
                                     onclick="btnIMergencyTransferFormRequest_Click"/>--%>
                                <asp:Button ID="btnSignOut" runat="server" Text="Signout" Font-Names="Tahoma"
                                    Font-Size="8pt" Width="64px" OnClick="btnSignOut_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr valign="top" style="height: auto; width: 100%">
                <td id="offsetElement" valign="top">
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
                                        AllowedDockingZoneTypes="Custom" Behavior="Pin" DockingMode="AlwaysDock" Height="100%"
                                        Text="Remarkable Disciplines" Width="100%">
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
            <%--<tr>
                <td>
                    <cc1:RadDockingZone ID="ZoneRDQL" runat="server" BackColor="Transparent" Type="Horizontal" Width="100%">
                        <cc1:RadDockableObject ID="RadDockableObject1" runat="server" AllowedDockingZones="ZoneRDQL"
                            AllowedDockingZoneTypes="Custom" Behavior="Pin" DockingMode="AlwaysDock" Height="100%"
                            Text="Remarkable Discipline(s)" Width="100%">
                            <ContentTemplate>
                                <ucRDQL:RDQuickLinks ID="RDQLinks" runat="server" />
                            </ContentTemplate>
                        </cc1:RadDockableObject>
                    </cc1:RadDockingZone>
                </td>
            </tr>--%>
            <tr valign="top">
                <%--<td style="width: 20%">
                    <cc1:RadDockingZone ID="RadDockingZoneLeft" runat="server" BackColor="Transparent"
                        Width="100%">
                        <cc1:RadDockableObject ID="RadDockPatientDemographics" runat="server" Text="Patient Demographic Information"
                            Width="100%" Behavior="Pin">
                            <ContentTemplate>
                                <ucDemo:Demogrphics ID="ucDemo" runat="server" />
                            </ContentTemplate>
                        </cc1:RadDockableObject>
                        <cc1:RadDockableObject ID="RadDockPrincipalProviderInfo" runat="server" Text="Healthcare Provider Information"
                            Width="100%" Height="20px">
                            <ContentTemplate>
                                <ucMPI:MainProviderInfo ID="MainProviderDetail" runat="server" />
                            </ContentTemplate>
                        </cc1:RadDockableObject>
                        <cc1:RadDockableObject ID="RadDockEmergencyContact" runat="server" Expanded="true"
                            Text="Emergency Contact Information" Width="100%" Height="20px">
                            <ContentTemplate>
                                <ucEmergency:EmergencyContactInfo ID="ucEmergencyContact" runat="server" />
                            </ContentTemplate>
                        </cc1:RadDockableObject>
                        <cc1:RadDockableObject ID="RadDockInsuranceInfo" runat="server" Text="Insurance Information"
                            Width="100%" Height="20px">
                            <ContentTemplate>
                                <ucII:InsInfo ID="ucInsInfo" runat="server" />
                            </ContentTemplate>
                        </cc1:RadDockableObject>
                    </cc1:RadDockingZone>
                </td>--%>
                <%--<td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr valign="top">
                            <td style="width: 50%">
                                <cc1:RadDockingZone ID="ZoneRightBottomLeft" runat="server" BackColor="Transparent"
                                    Width="100%" >
                                    <cc1:RadDockableObject ID="UnRD" runat="server" Width="100%" Text="Unremarkable Discipline(s)"
                                         DockingMode="alwaysdock" AllowedDockingZoneTypes="custom" AllowedDockingZones="ZoneRightBottomLeft"
                                         Behavior="Pin" Height="100%">
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
                </td>--%>
            </tr>
            <tr align="center">
                <td>
                    <asp:Button ID="btnPreview" runat="server" Font-Names="Tahoma" Font-Size="8pt" Font-Bold="true"
                        ForeColor="Black" Text="Preview" Height="30px" Width="60px" />
                    <%--<asp:Button ID="btnPrint" runat="server" Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black"
                        OnClientClick="return btnClick();" Text="Print" Height="30px" Width="50px"/>--%>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <%--<cc1:RadDockingZone ID="ZoneLegend" runat="server" Width="100%">
                        <cc1:RadDockableObject ID="dockLegend" runat="server" 
                             Text="Legend" Behavior="pin" Width="100%"
                             AllowedDockingZoneTypes="custom"
                             AllowedDockingZones="ZoneLegend"
                             DockingMode="AlwaysDock"
                             TitleBarStyle-Font-Bold="true" TitleBarStyle-HorizontalAlign="Center"
                             >
                            <ContentTemplate>
                                <uc:Legend ID="Legend" runat="server" />
                            </ContentTemplate>
                        </cc1:RadDockableObject>
                    </cc1:RadDockingZone>--%>
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
//                
//				function ReceiveServerData(arg)
//				{   if (currentLoadingPanel != null)currentLoadingPanel.Hide(currentUpdatedControl);
//                    currentUpdatedControl = null; currentLoadingPanel = null; alert(arg);}
                function HideStatus(){window.status="Click to see the details...";return true;}
                function ShowRemarkableDisciplineDrillDown(id,modifierID,medCode,disType){window.radopen("RDDetails.aspx?ID="+id+"&ModifierID="+modifierID+"&medCode="+medCode+"&disType="+disType, null);return false;}
                function ShowRemarkableDiscipline(url){window.radopen(url, null);return false;}
                function btnClick(){ return false;}
                function btnPreviewClick(url)
                {
                    var oManager = GetRadWindowManager();
                    var oWnd = oManager.GetWindowByName("wndPreview");
                    if(oWnd)
                    {
                        oWnd.SetUrl(url);
                        oWnd.Show();
                        oWnd.Maximize();
                    }
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
                
                var unvistedDiscipline = [];
                var visitedDiscipline = [];
        
                function SetVisited(discName)
                {
                    /////////////////////////////////////////////////////////////////
                    var alreadyExist = false;
                    for (var i = 0; i < visitedDiscipline.length; i++) 
		            {
                        if (visitedDiscipline[i] == discName) 
			            {
                            alreadyExist = true;
                            break;
                        }
                    }
                    if (alreadyExist == false) 
		            {
                        visitedDiscipline[visitedDiscipline.length] = discName;
                    }
                    ////////////////////////////////////////////////////////////////
                    for(var j=0;j<buttonIds.length;j++)
			        { 
			  	        if(buttonIds[j]==discName+'Button')
				        {  
				           animateStats[j]='False';
					       break;
				        }
			         }
                }
                function BlinkUnvisitedDiscipline(discArr)
                {
                    ///////////////////////////////////////////////////////////////////////
                     for(var i=0;i<discArr.length;i++)
                     {  
                        var alreadyExist = false;
                        for (var j = 0; j < unvistedDiscipline.length; j++) 
		                {
                            if (unvistedDiscipline[j] ==discArr[i]) 
			                {
                                alreadyExist = true;
                                break;
                            }
                        }
                        if (alreadyExist == false) 
		                {
		                    //check in visited discipline
		                    var alreadyVisisted = false;
                            for (var j = 0; j < visitedDiscipline.length; j++) 
	                        {
                                if (visitedDiscipline[j] == discArr[i]) 
		                        {
                                    alreadyVisisted = true;
                                    break;
                                }
                            }
                            if(alreadyVisisted==false)
                            {
                                unvistedDiscipline[unvistedDiscipline.length]=discArr[i];
                            }
                        }
                     }
                     /////////////////////////////////////////////////////////////////////
                      for(var i=0;i<unvistedDiscipline.length;i++)
                      {
			              for(var j=0;j<buttonIds.length;j++)
			              { 
			  	            if(buttonIds[j]==unvistedDiscipline[i]+'Button')
				            {  
				                animateStats[j]='True';
				                FireMouserOutEvent(j);
					            break;
				            }
			              }
                      }
                }
                function StopBlinkingVisitedDiscipline(discArr)
                {
                    ///////////////////////////////////////////////////////////////////////
                     for(var i=0;i<discArr.length;i++)
                     {
                        var alreadyExist = false;
                        for (var j = 0; j < visitedDiscipline.length; j++) 
		                {
                            if (visitedDiscipline[j] == discArr[i]) 
			                {
                                alreadyExist = true;
                                break;
                            }
                        }
                        if (alreadyExist == false) 
		                {
		                   visitedDiscipline[visitedDiscipline.length] =discArr[i];
                        }
                     }
                     /////////////////////////////////////////////////////////////////////
                    for(var i=0;i<visitedDiscipline.length;i++)
                    {
			              for(var j=0;j<buttonIds.length;j++)
			              { 
			  	            if(buttonIds[j]==visitedDiscipline[i]+'Button')
				            {  
				                animateStats[j]='False';
				                FireMouserOutEvent(j);
					            break;
				            }
			              }
                   }
                }
                function GetVisistedStatus(discName)
                {
                    for (var i = 0; i < visitedDiscipline.length; i++) 
		            {
                        if (visitedDiscipline[i] == discName) 
			            {
			                return 'True';
                        }
                    }
                    return 'False';
                }

                function openEmergencyTransferFormRequest(patientKey) {
                    //var wnd = window.radopen("Oracle/EmergencyTransferFormRequest.aspx?PatientKey=" + patientKey, null); 
                    // wnd.Maximize();
                    window.open("EmergencyTransferFormRequest.aspx?PatientKey=" + patientKey);
                }

                function openDoctorAlertNetwork(patientKey) {
                    //var wnd = window.radopen("Oracle/EmergencyTransferFormRequest.aspx?PatientKey=" + patientKey, null); 
                    // wnd.Maximize();
                    //window.open("DoctorAlertNetwork.aspx?PatientKey=" + patientKey);

                    window.open("NurseNotes.aspx?PatientKey=" + patientKey);
                }
        </script>

        <radA:AjaxLoadingPanel ID="AjaxLoadingPanel" runat="server" Width="100%" Transparency="25"
            MinDisplayTime="1000">
            <asp:Image ID="imgLoadPanel" runat="server" AlternateText="Saving..." ImageUrl="~/RadControls/Ajax/Skins/Default/LoadingProgressBar.gif" />
            <asp:Label ID="msgPref" runat="server" Font-Names="Verdana, Arial, Tahoma" Font-Bold="true"
                Font-Size="10pt" ForeColor="red">Saving...</asp:Label>
        </radA:AjaxLoadingPanel>
        <radW:RadWindowManager ID="RadWindowManager1" runat="server" Height="400px" Width="940px"
            Skin="Office2007" ShowContentDuringLoad="true" Modal="True" UseEmbeddedScripts="false" ReloadOnShow="true"
            Top="60" Left="20">
            <Windows>
                <radW:RadWindow ID="wndPreview" runat="server" Modal="true" Behavior="Close" VisibleStatusbar="false"
                    UseEmbeddedScripts="false" />
            </Windows>
        </radW:RadWindowManager>
    </form>
    <%--<asp:Label ID="InjectScriptLabelSaved" runat="server"></asp:Label>--%>
</body>
</html>
