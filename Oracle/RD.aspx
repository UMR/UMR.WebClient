<%@ Page AutoEventWireup="true" CodeFile="RD.aspx.cs" Inherits="Oracle_RD" Language="C#" %>

<%@ Register Src="ControlLibrary/ucLegendCompactCheckBox.ascx" TagName="ucLegendCompact"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Reference Control="~/Oracle/ControlLibrary/ucRDDiagnosisMain.ascx" %>
<%@ Reference Control="~/Oracle/ControlLibrary/ucRDMedicationMain.ascx" %>
<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Remarkable Discipline</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:PlaceHolder ID="phControl" runat="server" />
    </form>
    <radW:RadWindowManager ID="RadWindowManager2" runat="server" Skin="Office2007"
        InitialBehavior="maximize" VisibleStatusbar="false" UseEmbeddedScripts="false">
    </radW:RadWindowManager>
    <script type="text/javascript">
        function ShowRemarkableDiscipline(url){window.radopen(url, null); return false;}
//        function ShowRemarkableDisciplineDrillDown(id,modifierID,medCode,disType)
//                {window.radopen("RDDetails.aspx?ID="+id+"&ModifierID="+modifierID+"&medCode="+medCode+"&disType="+disType, null);return false;}
        function HideStatus(){var statusMsg ="Click to see the details..."; window.status = statusMsg; return true; } 
    </script>
    
</body>
</html>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title></title>
    <style type="text/css" media="print">
        .noprint 
        {
            display:none 
        }
         #otherRemarkableDisipline
         {
            display:none;
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
                <td>
                    <uc1:ucLegendCompact ID="UcLegendCompact1" runat="server" />
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
        <%--<asp:Label ID="lblName" runat="server"/> <br />--%>
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
    <radW:RadWindowManager ID="RadWindowManager2" runat="server" Skin="Office2007" Behavior="Close"
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
    </script>

    <script type="text/javascript">
    function SizeToFit()
	   {
	    window.setTimeout(
			function()
			{
				var oWnd = GetRadWindow();
//				alert('Height: ' + oWnd.GetHeight() + ' Width: ' + oWnd.GetWidth() 
//				    + ' Content Height: ' + document.body.scrollHeight 
//				    + ' Content Width: ' + document.body.scrollWidth);
		
				//if(document.body.scrollWidth < oWnd.GetWidth())
				//    oWnd.SetWidth(document.body.scrollWidth + 4);
				//if(document.body.scrollHeight < oWnd.GetHeight())
				//    oWnd.SetHeight(document.body.scrollHeight + 70);
                //oWnd.Center();				
				//oWnd.SetHeight(document.body.scrollHeight + 70);
			}, 1);
	   }
		function GetRadWindow()
		{
			var oWindow = null;
			if (window.radWindow) oWindow = window.radWindow;
			else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
			return oWindow;
		} 
        function ShowRemarkableDiscipline(url,text){window.radopen(url, null); RemoveDiscipline(text);return false;}
//        function ShowRemarkableDisciplineDrillDown(id,modifierID,medCode,disType)
//                {window.radopen("RDDetails.aspx?ID="+id+"&ModifierID="+modifierID+"&medCode="+medCode+"&disType="+disType, null);return false;}
        function HideStatus(){var statusMsg ="Click to see the details..."; window.status = statusMsg; return true; } 
        function SetTitle(disName)
        {
            var oWnd = GetRadWindow(); if(oWnd)oWnd.SetTitle(disName);
        }
       
       
        var unvistedDiscipline = [];
        var visitedDiscipline = [];
        
              
        function AddDiscipline(name)
	    {
	        //alert('Adding '+name);
            var alreadyExist = false;
            for (var i = 0; i < unvistedDiscipline.length; i++) 
		    {
                if (unvistedDiscipline[i] == name) 
			    {
                    alreadyExist = true;
                    break;
                }
            }
            if (alreadyExist == false) 
		    {
                unvistedDiscipline[unvistedDiscipline.length] = name;
            }
            
        }
        function AddDisciplinetoVisitedList(name)
	    {
	        //alert('Adding '+name);
            var alreadyExist = false;
            for (var i = 0; i < visitedDiscipline.length; i++) 
		    {
                if (visitedDiscipline[i] == name) 
			    {
                    alreadyExist = true;
                    break;
                }
            }
            if (alreadyExist == false) 
		    {
                visitedDiscipline[visitedDiscipline.length] = name;
            }
            
        }
        function RemoveDiscipline(name)
	    {   
	        //alert('Removing '+name);
            for (var i = 0; i < unvistedDiscipline.length; i++) 
		    {
        	    if(unvistedDiscipline[i]==name)
			    {
				    unvistedDiscipline.splice(i,1);
				    break;
			    }
            }
            AddDisciplinetoVisitedList(name);
        }
        function MargeDiscipline(dispArr)
	    {
		    for(var i=0;i<dispArr.length;i++)
		    {
			    AddDiscipline(dispArr[i]);
		    }
	    }
	    function MargeVisitedDiscipline(dispArr)
	    {
		    for(var i=0;i<dispArr.length;i++)
		    {
			    AddDisciplinetoVisitedList(dispArr[i]);
		    }
	    }
	    
	    function GetChildUnvisitedDiscList()
	    {
	        var allWindows = GetRadWindowManager().GetWindowObjects();
	        if(allWindows.length>0)
	        { 
	            var win = allWindows[0];
	            if(win.IsClosed()==false)
	            {
	                 var contWnd=win.GetContentFrame().contentWindow;
	                 if(contWnd.location.href.indexOf("RD.aspx")>=0 && contWnd.location.href.indexOf("TiledRD.aspx")<0)
	                 {
                        var arr= win.GetContentFrame().contentWindow.GetChildUnvisitedDiscList();
                        if(arr)
                        {  
                            MargeDiscipline(arr);
                        }
                     }
                  }
	           
	        }
	        
	        return unvistedDiscipline;
	       
	    }
	    function GetChildVisitedDiscList()
	    {
	        var allWindows = GetRadWindowManager().GetWindowObjects();
	        if(allWindows.length>0)
	        {
	            var win = allWindows[0];
	            if(win.IsClosed()==false)
	            {
                    var contWnd=win.GetContentFrame().contentWindow;
                    if(contWnd.location.href.indexOf("RD.aspx")>=0 && contWnd.location.href.indexOf("TiledRD.aspx")<0)
                    {
                        var arr= win.GetContentFrame().contentWindow.GetChildVisitedDiscList();
                        if(arr)
                        {
                            MargeVisitedDiscipline(arr);
                        }
                    }
	            }
	        }
	        
	        return visitedDiscipline;
	    }
	    
	    var wnd=GetRadWindow();
	    wnd.OnClientClose = function(wnd)
        {
            //alert('Closing');
            //checking for child window
            GetChildUnvisitedDiscList();
            GetChildVisitedDiscList();
            
            //PrintDiscipline(unvistedDiscipline);
            //PrintDiscipline(visitedDiscipline);
             // check parent window
             var urlStr= GetRadWindow().BrowserWindow.location.href;
             if(urlStr.indexOf("RD.aspx")>=0)
             {
                 wnd.BrowserWindow.MargeDiscipline(unvistedDiscipline);
                 wnd.BrowserWindow.MargeVisitedDiscipline(visitedDiscipline);
             }
             else
             {
                 wnd.BrowserWindow.BlinkUnvisitedDiscipline(unvistedDiscipline);
                 wnd.BrowserWindow.StopBlinkingVisitedDiscipline(visitedDiscipline);
             }
        }
       
        function IsVisited(discName)
        {    
            var rdWnd=GetRadWindow();
            var urlStr= rdWnd.BrowserWindow.location.href;
            if(urlStr.indexOf("RD.aspx")>=0)
            {
                return rdWnd.BrowserWindow.IsVisited(discName);
            }
            else
            {
                return rdWnd.BrowserWindow.GetVisistedStatus(discName);
            }
        }
        function Decorate(text)
	    {  
	        if(IsVisited(text)=='True')
	        {
		        var rdLink = document.getElementById('rd_' + text);
		        rdLink.style.color='purple';
		    }
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
