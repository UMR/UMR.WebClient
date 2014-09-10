<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDDetails.aspx.cs" Inherits="Oracle_RDDetails" %>

<%@ Register Src="ControlLibrary/ucLegendCompactCheckBox.ascx" TagName="ucLegendCompact"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Reference Control="~/Oracle/ControlLibrary/ucRDDiagnosisDetails2.ascx" %>
<%@ Reference Control="~/Oracle/ControlLibrary/ucRDMedicationDetails2.ascx" %>
<%--<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:PlaceHolder ID="phControl" runat="server"></asp:PlaceHolder>
    </div>
    </form>
    <radW:RadWindowManager ID="RadWindowManager3" runat="server" Skin="Office2007"
        InitialBehavior="maximize" UseEmbeddedScripts="false">
    </radW:RadWindowManager>
</body>

<script type="text/javascript">
            function ShowProviderInfo(id,dispType)
            {
                window.radopen("ProviderDetails.aspx?ID="+id+"&DispType="+dispType, null);
                return false;
            }
            function HideStatus(){var statusMsg ="Click to see the details..."; window.status = statusMsg; return true; } 
</script>

</html>
--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title></title>
    <style type="text/css" media="print">
        .noprint 
        {
            display:none;
        }
    </style>
    <style type="text/css" media="screen">
        .noprint 
        {
            display:block;
        }
    </style>

    <script src="../jquery.js" type="text/javascript"></script>

    <script src="../tooltip.js" type="text/javascript"></script>

    <style type="text/css">
        .tooltip
        {
            /*color:blue;
            text-decoration:none;
            cursor:hand;*/
        }
        #tooltip{
            font-family: Tahoma, Arial, Helvetica, sans-serif;
            font-size:8pt;
	        position:absolute;
	        border:1px solid #333;
	        background:#f7f5d1;
	        padding:2px 5px;
	        color:#333;
	        display:none;
	        }	
	
    </style>
</head>
<body onload="SizeToFit()">

    <script type="text/javascript">
    
        // For Now we will only center it... will take care about size later...
	   function SizeToFit()
	   {
	    window.setTimeout(
			function()
			{
				var oWnd = GetRadWindow();
				//oWnd.SetWidth(document.body.scrollWidth + 4);
				//oWnd.SetHeight(document.body.scrollHeight + 70);
				
			}, 400);
	   }
	   
		function GetRadWindow()
		{
			var oWindow = null;
			if (window.radWindow) oWindow = window.radWindow;
			else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
			return oWindow;
		} 
    </script>

    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td align="left" class="noprint" id="backButtonTd" runat="server">
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
        <div id="displayDiv">
            <asp:Label ID="lblName" runat="server" Font-Bold="true" Font-Size="8pt" Font-Names="Tahoma, Arial" />
            <br />
            <asp:PlaceHolder ID="phControl" runat="server"></asp:PlaceHolder>
        </div>
    </form>
    <form name="gotoEmailForm" action="Email.aspx" method="post">
        <input type="hidden" name="emailHidden" id="Hidden1" />
        <input type="hidden" name="subject" id="Hidden2" value="RD Details" />
    </form>
    <form name="gotoFaxForm" action="Fax.aspx" method="post">
        <input type="hidden" name="faxHidden" id="faxHidden" />
        <input type="hidden" name="subjectFax" id="subjectFax" value="RD Details" />
    </form>
    <radW:RadWindowManager ID="RadWindowManager3" runat="server" Skin="Office2007" InitialBehavior="maximize"
        Behavior="Close" UseEmbeddedScripts="false">
    </radW:RadWindowManager>
</body>

<script type="text/javascript">
            function ShowProviderInfo(id,dispType,showCodeStr)
            {
                window.radopen("ProviderDetails.aspx?ID="+id+"&DispType="+dispType+"&"+showCodeStr, null);
                return false;
            }
            function ShowInstitutionDetails(code,patientKey,hasCodeStr)
            {
                window.radopen("InstitutionDetails.aspx?code="+code+"&PatientKey="+patientKey+"&"+hasCodeStr, null);
                return false;
            }
            function HideStatus(){var statusMsg ="Click to see the details..."; window.status = statusMsg; return true; } 
            
            function backButton_onclick() {
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
            var wnd=GetRadWindow();
            wnd.OnClientClose = function(wnd)
            {
               //do nothing
            }
   
</script>

</html>
