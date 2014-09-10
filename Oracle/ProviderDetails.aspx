<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProviderDetails.aspx.cs"
    Inherits="Oracle_ProviderDetails" %>

<%@ Register Src="ControlLibrary/ucLegendCompact.ascx" TagName="ucLegendCompact"
    TagPrefix="uc1" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Oracle/ControlLibrary/ucProviderDetails.ascx" TagName="ProviderInfo"
    TagPrefix="uc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Provider Locator</title>
    <style type="text/css" media="print">
        .noprint 
        {
            display:none 
        }
    </style>
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

    <script src="../jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="ControlLibrary/jQueryTooltip/jquery.tooltip.js"></script>

    <script type="text/javascript" src="ControlLibrary/jQueryTooltip/jquery.dimensions.js"></script>

    <script type="text/javascript">
        $(function() {
            $('.tooltip').tooltip({
	            track: true,
	            delay: 0,
	            showURL: false,
	            showBody: " - ",
	            fade: 250
            });
        });
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
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
            <uc:ProviderInfo ID="ucProvider" runat="server" />
        </div>
    </form>
    <form name="gotoEmailForm" action="Email.aspx" method="post">
        <input type="hidden" name="emailHidden" id="emailHidden" />
        <input type="hidden" name="subject" id="subject" value="Provider Details" />
    </form>
    <form name="gotoFaxForm" action="Fax.aspx" method="post">
        <input type="hidden" name="faxHidden" id="faxHidden" />
        <input type="hidden" name="subjectFax" id="subjectFax" value="Provider Details" />
    </form>
    <radW:RadWindowManager ID="RADWMPD" UseEmbeddedScripts="false" runat="server" InitialBehavior="maximize"
        Behavior="Close" Skin="Office2007" />

    <script type="text/javascript">
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
        function ShowInstitutionDetails(code)
        {
            window.radopen("InstitutionDetails.aspx?code="+code, null);
            return false;
        }
    </script>

</body>
</html>
