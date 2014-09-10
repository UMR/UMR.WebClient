<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DockedRD.aspx.cs" Inherits="Oracle_DockedRD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadDock.Net2" Namespace="Telerik.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Reference Control="~/Oracle/ControlLibrary/ucRD2l.ascx" %>
<%@ Register Src="ControlLibrary/ucLegendCompactCheckBox.ascx" TagName="ucLegendCompact"
    TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Related Disciplines</title>
    <link rel="stylesheet" href="ControlLibrary/jQueryTooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../jquery.js"></script>

    <script type="text/javascript" src="ControlLibrary/jQueryTooltip/jquery.tooltip.js"></script>

    <script type="text/javascript" src="ControlLibrary/jQueryTooltip/jquery.dimensions.js"></script>

    <script type="text/javascript" src="ControlLibrary/clueTip/jquery.hoverIntent.js"></script>

    <script type="text/javascript" src="ControlLibrary/clueTip/jquery.cluetip.js"></script>

    <link href="ControlLibrary/clueTip/jquery.cluetip.css" rel="stylesheet" type="text/css" />
    <style type="text/css" media="screen">
    #btnClose:hover
    {
        cursor: pointer;
    }
    #imgBack:hover
    {
        cursor: pointer;
    } 
    #imgForward:hover
    {
        cursor: pointer;
    }
    .RadDockableObjectFixed
    {
        width: 100%  !important;
        height: 10%  !important;
    }
    .disableRD
    {
        color: black;
        cursor: default;
    }
    .rdcluetip
     {
            cursor: hand;
	        color:blue;
            text-decoration:underline;
     }
    </style>
    <script type="text/javascript">
        $(function() {
            CallFnOnParent(0);
            ImplementTooltip();
            BackForwardControler();
        });
        function GetRadWindow()
        {      
            var oWindow = null;      
            if (window.radWindow)
                oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog      
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;//IE (and Moz as well)      
            return oWindow;                
        }
        
        function ShowRemarkableDiscipline(url,text)
        {
	        window.radopen(url, null); 
	        return false;
        }
        function ImplementTooltip()
        {
            //            $('.tooltip').tooltip({
            //	            track: true,
            //	            delay: 0,
            //	            showURL: false,
            //	            showBody: " - ",
            //	            fade: 250
            //            });
             $('.rdcluetip').each(function(i){
                    var id = $(this).attr("id");
                    $('#'+id).cluetip({
                        cluetipClass: 'jtip',
                        arrows: true,
                        dropShadow: false,
                        hoverIntent: false,
                        sticky: true,
                        mouseOutClose: false,
                        closePosition: 'title',
                        closeText: '<img src="ControlLibrary/clueTip/cross.png" alt="close" />'
                    });
            });
        }
        function onResponseEnd(sender, arguments)
        {
             ImplementTooltip();
             BackForwardControler();
        };
        function printButton_onclick() 
        {
            window.print();
        }
        function emailButton_onclick()
        {
            CallFnOnParent(1);
            
            var emailText= document.getElementById("displayDiv").innerHTML;   
            document.getElementById("emailHidden").value =emailText;
            document.gotoEmailForm.submit();
        }
        function faxButton_onclick() 
        {
            CallFnOnParent(1);
            
            var faxText= document.getElementById("displayDiv").innerHTML;   
            document.getElementById("faxHidden").value =faxText;
            document.gotoFaxForm.submit();
        }
        function CallFnOnParent(option)
        {
           GetRadWindow().BrowserWindow.CalledFn_CloseButtonVisibility(option); //CalledFn_CloseButtonVisibility() must exist in parent
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="margin: 0px; padding: 0px">
        <table style="width: 100%; height: 100%; padding-top: 0px; margin-top: 0px;" cellpadding="0px;"
            cellspacing="0px">
            <tr>
                <td style="width: 30%">
                    <asp:Label ID="lblRD" runat="server" Font-Bold="true" Font-Size="9pt" Font-Names="Tahoma, Arial"
                        ForeColor="navy" />
                </td>
                <td style="width: 40%; text-align: center;">
                    <asp:Panel ID="panLegend" runat="server">
                        <uc1:ucLegendCompact ID="UcLegendCompact2" runat="server" />
                    </asp:Panel>
                </td>
                <td align="right" style="width: 15%;">
                    <div class="noprint">
                        <img alt="Fax" src="../Oracle/images/fax_button.jpg" onclick="return faxButton_onclick()"
                            style="cursor: pointer;" />
                        <img alt="Email" src="../Oracle/images/email_button.jpg" onclick="return emailButton_onclick()"
                            style="cursor: pointer;" />
                        <img alt="Print" src="../Oracle/images/print_button.jpg" onclick="return printButton_onclick()"
                            style="cursor: pointer;" />
                    </div>
                </td>
                <td style="width: 15%; text-align: right; vertical-align: middle;">
                    <table>
                        <tr>
                            <td>
                                <img id="imgBack" src="images/Backward22.png" alt="Back" title="Go back one page"
                                    onclick="GoBackward();" />
                            </td>
                            <td>
                                <img id="imgForward" src="images/Forward22.png" alt="Forward" title="Go forward one page"
                                    onclick="GoForward();" />
                            </td>
                            <td>
                                <img id="btnClose" src="images/CloseButton.png" style="height: 14px;" onclick="Close();"
                                    alt="Close" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div id="displayDiv">
                        <cc1:RadDockingManager ID="RadDockingManager1" runat="server" SkinsPath="../RadControls/Dock/Skins/"
                            Skin="Office2007" />
                        <table style="width: 100%; height: 100%; border: solid 1px black;">
                            <tr>
                                <td style="width: 50%; vertical-align: top;">
                                    <cc1:RadDockingZone ID="RadDockingZone1" runat="server" Width="100%" Height="500px"
                                        BorderWidth="0px" Type="Top" FixedSizeMode="ByHeight" MinimumHeight="500px">
                                    </cc1:RadDockingZone>
                                </td>
                                <td style="width: 50%; vertical-align: top;">
                                    <cc1:RadDockingZone ID="RadDockingZone2" runat="server" Width="100%" Height="500px"
                                        BorderWidth="0px" Type="Top" FixedSizeMode="ByHeight" MinimumHeight="500px">
                                    </cc1:RadDockingZone>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfLocationPointer" runat="server" />
        <asp:HiddenField ID="hLastClicked" runat="server" />
        <radA:AjaxLoadingPanel ID="AjaxLoadingPanel1" runat="server" Height="75px" Transparency="10"
            Width="75px">
            <asp:Image ID="Image1" runat="server" AlternateText="Loading..." BackColor="Transparent"
                ImageUrl="~/RadControls/Ajax/Skins/Default/loading.gif" />
        </radA:AjaxLoadingPanel>

        <script type="text/javascript">
            function Close()      
            {     
                var ajaxManager = <%= RadAjaxManagerDefault.ClientID %> 
                ajaxManager.AjaxRequest('CloseWindow');  
                GetRadWindow().Close();
            }  
            function LoadRelatedDiscipline(patientKey, disCodes, clickedRD)
            {
                var lastClicked = document.getElementById('hLastClicked');    
                if( lastClicked.value != clickedRD )
                {
                    lastClicked.value = clickedRD;
                    var ajaxManager = <%= RadAjaxManagerDefault.ClientID %> 
                    ajaxManager.AjaxRequest(patientKey + '^' + disCodes + '^' + clickedRD); 
                }
            }
            
            function BackForwardControler()
            {
                var imgBack = document.getElementById('imgBack'); 
                var imgForward = document.getElementById('imgForward'); 
                
                var willDisable = DisableBack();
                if( willDisable) //Backword Button
                { 
                    imgBack.src = 'images/Backward22_disabled.png';
                    imgBack.title = 'Back';
                    imgBack.style.cursor='default';
                }
                else
                {
                    imgBack.src = 'images/Backward22.png';
                    imgBack.title = 'Back';
                    imgBack.style.cursor='pointer';
                }
                
                willDisable = DisableForward();
                if( willDisable) //Forward Button
                {
                    imgForward.src = 'images/Forward22_disabled.png';
                    imgForward.title = 'Forward';
                    imgForward.style.cursor='default';
                }
                else
                {
                    imgForward.src = 'images/Forward22.png';
                    imgForward.title = 'Forward';
                    imgForward.style.cursor='pointer';
                }
            }
            
            function DisableBack()
            {
                var option = document.getElementById('hfLocationPointer'); //Position|Count
                var parts = option.value.split('|');
                var part1 = parseInt(parts[0]);
                var part2 = parseInt(parts[1]);
                
                var willDisable = true;
                
                if( part1 == 0 && part2 == 1) //Initial stage
                    willDisable = true;
                else if( part1 == 0 && part2 > 1)
                    willDisable = true;
                else
                    willDisable = false;
                    
                return willDisable;
            }
            
            function GoBackward()
            {
                var willDisable = DisableBack();
                
                if( !willDisable )
                {
                    var ajaxManager = <%= RadAjaxManagerDefault.ClientID %> 
                    ajaxManager.AjaxRequest('GoBack'); 
                }
                else
                {
                    //alert('Back disable');
                }
            }
            
            function DisableForward()
            {
                var option = document.getElementById('hfLocationPointer'); //Position|Count
                var parts = option.value.split('|');
                var part1 = parseInt(parts[0]);
                var part2 = parseInt(parts[1]);
                
                var willDisable = true;
                
                if( part1 == 0 && part2 == 1) //Initial stage
                    willDisable = true;
                else if( part1 + 1 == part2 && part2 > 1)
                    willDisable = true;
                else
                    willDisable = false;
                    
                return willDisable;
            }
            function GoForward()
            {
                var willDisable = DisableForward();
            
                if( !willDisable)
                {
                    var ajaxManager = <%= RadAjaxManagerDefault.ClientID %> 
                    ajaxManager.AjaxRequest('GoForward'); 
                }
                else
                {
                    //alert('Forward disable');
                }
            }
        </script>
 
        <radW:RadWindowManager ID="RadWindowManager2" runat="server" Skin="Office2007" Behavior="Close,Move" Top="10px" Left="10px"  OnClientClose="Close()" ReloadOnShow ="true"
            Height="400px" Width="800px" VisibleStatusbar="false" UseEmbeddedScripts="false" Modal="true">
        </radW:RadWindowManager>
        <radA:RadAjaxManager ID="RadAjaxManagerDefault" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1"
            OnAjaxRequest="RadAjaxManagerDefault_AjaxRequest" ClientEvents-OnResponseEnd="onResponseEnd"
            UseEmbeddedScripts="true">
            <AjaxSettings>
                <radA:AjaxSetting AjaxControlID="RadAjaxManagerDefault">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="RadDockingZone1" />
                        <radA:AjaxUpdatedControl ControlID="RadDockingZone2" />
                        <radA:AjaxUpdatedControl ControlID="lblRD" />
                        <radA:AjaxUpdatedControl ControlID="btnBack" />
                        <radA:AjaxUpdatedControl ControlID="hLastClicked" />
                        <radA:AjaxUpdatedControl ControlID="hfLocationPointer" />
                    </UpdatedControls>
                </radA:AjaxSetting>
                <radA:AjaxSetting AjaxControlID="panLegend">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="RadDockingZone1" />
                        <radA:AjaxUpdatedControl ControlID="RadDockingZone2" />
                    </UpdatedControls>
                </radA:AjaxSetting>
            </AjaxSettings>
        </radA:RadAjaxManager>
    </form>
    <form name="gotoEmailForm" action="DockedRDEmail.aspx" method="post">
        <input type="hidden" name="emailHidden" id="emailHidden" />
        <input type="hidden" name="subject" id="subject" value="RD" />
    </form>
    <form name="gotoFaxForm" action="DockedRDFax.aspx" method="post">
        <input type="hidden" name="faxHidden" id="faxHidden" />
        <input type="hidden" name="subjectFax" id="subjectFax" value="RD" />
    </form>
</body>
</html>
