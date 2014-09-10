<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDNew.aspx.cs" Inherits="Oracle_RDNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="ControlLibrary/ucLegendCompactCheckBox.ascx" TagName="ucLegendCompact"
    TagPrefix="uc1" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Reference Control="~/Oracle/ControlLibrary/ucRD2l.ascx" %>
<%@ Reference Control="~/Oracle/ControlLibrary/ucRDMedicationMain.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title></title>
     <link rel="stylesheet" href="ControlLibrary/jQueryTooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../jquery.js"></script>

    <script type="text/javascript" src="ControlLibrary/jQueryTooltip/jquery.tooltip.js"></script>

    <script type="text/javascript" src="ControlLibrary/jQueryTooltip/jquery.dimensions.js"></script>

    <script type="text/javascript" src="ControlLibrary/clueTip/jquery.hoverIntent.js"></script>

    <script type="text/javascript" src="ControlLibrary/clueTip/jquery.cluetip.js"></script>

    <link href="ControlLibrary/clueTip/jquery.cluetip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function() {
            //            $('.tooltip').tooltip({
            //              track: true,
            //              delay: 0,
            //              showURL: false,
            //              showBody: " - ",
            //              fade: 250
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
        });
    </script>

    <style type="text/css" media="print">
        .noprint 
        {
            display:none 
        }
         #otherRemarkableDisipline
         {
            display:none;
         }
         .disLabel
         {
            font-family:Tahoma; 
            font-size: 8pt;
         }
    </style>
    <style type="text/css" media="screen">
        #otherRemarkableDisipline
        {
            height:15px;
        }
        #otherRemarkableDisipline ul
        {
            margin-left:5px;
        }
        #otherRemarkableDisipline li
        {
            list-style: none;
            display: inline;
            font-family: 'Tahoma';
        }
        #otherRemarkableDisipline li a
        {
            text-decoration: none;
            font-size: 8pt;
            font-weight:bold;
            color: midnightblue;
            padding-right:20px;
        }
        #otherRemarkableDisipline li a:link, #otherRemarkableDisipline ul li a:visited, #otherRemarkableDisipline ul lia:active
        {
            color: midnightblue;
        }
        #otherRemarkableDisipline li a:hover
        {
            color: cornflowerblue;
        }
        .rdcluetip
        {
            cursor: hand;
	        color:blue;
            text-decoration:underline;
        }
    </style>
</head>
<body onload="SizeToFit()">
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td align="left" class="noprint" visible="false" id="backButtonTd" runat="server">
                    <img alt="BackButton" src="../Oracle/images/back.png" width="39" height="39" onclick="return backButton_onclick()"
                        style="cursor: hand;" />
                </td>
                <td align="left" class="noprint">
                    <uc1:ucLegendCompact ID="UcLegendCompact2" runat="server" Visible="false" />
                </td>
                <td align="right" class="noprint">
                    <img alt="Fax" src="../Oracle/images/fax_button.jpg" onclick="return faxButton_onclick()"
                        style="cursor: pointer;" />
                    <img alt="Email" src="../Oracle/images/email_button.jpg" onclick="return emailButton_onclick()"
                        style="cursor: pointer;" />
                    <img alt="Print" src="../Oracle/images/print_button.jpg" onclick="return printButton_onclick()"
                        style="cursor: pointer;" />
                </td>
            </tr>
        </table>
        <div id="displayDiv">
            <asp:PlaceHolder ID="phControl" runat="server" />
        </div>
    </form>
    <form name="gotoEmailForm" action="Email.aspx" method="post">
        <input type="hidden" name="emailHidden" id="emailHidden" />
        <input type="hidden" name="subject" id="subject" value="RD" />
    </form>
    <form name="gotoFaxForm" action="Fax.aspx" method="post">
        <input type="hidden" name="faxHidden" id="faxHidden" />
        <input type="hidden" name="subjectFax" id="subjectFax" value="RD" />
    </form>
    <radW:RadWindowManager ID="RadWindowManager2" runat="server" Skin="Office2007" Behavior="Close" ReloadOnShow="true"
        InitialBehavior="maximize" VisibleStatusbar="false" UseEmbeddedScripts="false">
    </radW:RadWindowManager>

    <script type="text/javascript">
        function backButton_onclick() 
        {
            history.back();
        }
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
        function ShowRemarkableDiscipline(url,text)
        {
	        window.radopen(url, null); 
	        return false;
        }
        function CalledFn_CloseButtonVisibility( option )
        {
            if( option)
                $('.RadWWrapperHeaderCenter').filter('[title=Close]').css('display', 'block'); //Disable Close Button
            else
                $('.RadWWrapperHeaderCenter').filter('[title=Close]').css('display', 'none'); //Disable Close Button
        }
    </script>

    <script type="text/javascript">
    var wnd=GetRadWindow();
    wnd.OnClientClose = function(wnd)
    {
        
    }
    function LoadRelatedDisciplineTest(patientKey, disCodes, clickedRD)
    {
        var manager = GetRadWindowManager();
        var win = GetRadWindowManager().GetWindowByName("winDockedRD");  
        win.Url = win.Url + '?PatientKey='+patientKey+'&discodes='+disCodes+'&ClickedRD='+clickedRD;

        var urlStr= GetRadWindow().BrowserWindow.location.href;
	    if(urlStr.indexOf("Result.aspx")<0)
	    {
		    GetRadWindow().BrowserWindow.MaximizeParentWindow();
	    }
	    var oWnd = win;
	    oWnd.Maximize();
	    
	    var allDisWindow= window.radopen(win.Url,null);
    }
    function LoadRelatedDiscipline(patientKey, disCodes, clickedRD)
    {
	    var url = "DockedRD.aspx?PatientKey="+patientKey+"&discodes="+disCodes+"&ClickedRD="+clickedRD;
	    var urlStr= GetRadWindow().BrowserWindow.location.href;
	    if(urlStr.indexOf("Result.aspx")<0)
	    {
		    GetRadWindow().BrowserWindow.MaximizeParentWindow();
	    }

	    var oWnd = GetRadWindow();
	    oWnd.Maximize();
	    
	    var allDisWindow= window.radopen(url,null);
	    allDisWindow.OnClientClose = function()
	    {
		    if(urlStr.indexOf("Result.aspx")<0)
		    {
			    GetRadWindow().BrowserWindow.RestoreParentWindow();
		    }
		    else
		    {
    			
			    if(oWnd.GetUrl().indexOf("PrintPreview.aspx")<0)
			    {
				    oWnd.Restore();
			    }
			    else
			    {
				    oWnd.Maximize();
			    }
		    }
	    }
	    
        //$('.RadWWrapperHeaderCenter').filter('[title=Close]').css('display', 'none'); //Disable Close Button
    }
    function SizeToFit()
    {
        window.setTimeout(function(){
		        var oWnd = GetRadWindow();
	        }, 
	    1);
    }
    function GetRadWindow()
    {
	    var oWindow = null;
	    if (window.radWindow) oWindow = window.radWindow;
	    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
	    return oWindow;
    } 
    function HideStatus(){var statusMsg ="Click to see the details..."; window.status = statusMsg; return true; } 
    function SetTitle(disName)
    {
	    var oWnd = GetRadWindow(); if(oWnd)oWnd.SetTitle(disName);
    }
    function PrintDiscipline(disList)
    {
	    var str='';
	    for(var i=0;i<disList.length;i++)
	    {
		    str+=disList[i]+' ';
	    }
	    alert(str);
    }
    </script>

    <asp:Label ID="InjectScriptLabelWindowTitle" runat="server" />
</body>
</html>
