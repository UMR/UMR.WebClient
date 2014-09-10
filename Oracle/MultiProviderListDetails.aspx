<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultiProviderListDetails.aspx.cs"
    Inherits="Oracle_MultiProviderListDetails" %>

<%@ Register Src="ControlLibrary/ucLegendCompactCheckBox.ascx" TagName="ucLegendCompact"
    TagPrefix="uc1" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Oracle/ControlLibrary/ucMultiProviderListDetails.ascx" TagName="MultiProviderList"
    TagPrefix="uc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Health Care Provider List</title>
    <style type="text/css" media="print">
        .noprint 
        {
            display:none 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" class="noprint">
            <tr>
                <td align="right">
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
            <div class="noprint">
                <uc1:ucLegendCompact ID="UcLegendCompact1" runat="server" />
            </div>
            <uc:MultiProviderList ID="multiProviderList" runat="server" />
        </div>
        <radW:RadWindowManager ID="RADWMMPI" runat="server" InitialBehavior="maximize" Skin="Office2007" ReloadOnShow="true"
            UseEmbeddedScripts="false" Behavior="Close" />
    </form>
    <form name="gotoEmailForm" action="Email.aspx" method="post">
        <input type="hidden" name="emailHidden" id="emailHidden" />
        <input type="hidden" name="subject" id="subject" value="Health Care Provider List" />
    </form>
    <form name="gotoFaxForm" action="Fax.aspx" method="post">
        <input type="hidden" name="faxHidden" id="faxHidden" />
        <input type="hidden" name="subjectFax" id="subjectFax" value="Health Care Provider List" />
    </form>

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
        function GetRadWindow()
        {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        } 
        function SetNewWidth(width)
        {
            GetRadWindow().SetWidth(width);
        }
        
        function ShowInstitutionDetails(Institutioncode,hasCodeStr)
        {
            window.radopen("InstitutionDetails.aspx?code="+Institutioncode+"&"+hasCodeStr, null);
            return false;
        }
    </script>

</body>
</html>
