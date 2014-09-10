<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AMD.aspx.cs" Inherits="Oracle_AMD" %>

<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucAMD.ascx" TagName="AMD" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Advanced Medical Directives</title>
    <style type="text/css" media="print">
        .noprint 
        {
            display:none 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr>
                    <td align="right" class="noprint">
                        <img alt="Fax" src="../Oracle/images/fax_button.jpg" style="cursor: pointer;" onclick="return faxButton_onclick()" />
                        <img alt="Email" src="../Oracle/images/email_button.jpg" onclick="return emailButton_onclick()"
                            style="cursor: pointer;" />
                        <img alt="Print" src="../Oracle/images/print_button.jpg" onclick="return printButton_onclick()"
                            style="cursor: pointer;" />
                    </td>
                </tr>
            </table>
            <div id="displayDiv">
                <uc:AMD ID="AMD" runat="server" />
            </div>
            <br />
            <%-- <table style="font-family: Tahoma; font-size: 8pt; width: 100%">
                <tr>
                    <td>
                        <span style="text-decoration: underline">Power of Attorney </span></td>
                    <td>
                        <span style="text-decoration: underline">Durable Power of Attorney</span></td>
                </tr>
            </table>--%>
            <%--<radG:RadGrid ID="RadGridAMD" runat="server" AllowPaging="false" EnableAJAX="false"
                EnableAJAXLoadingTemplate="true" GridLines="None" UseEmbeddedScripts="false">
                <MasterTableView AutoGenerateColumns="false">
                    <ExpandCollapseColumn Visible="False">
                        <HeaderStyle Width="19px" />
                    </ExpandCollapseColumn>
                    <RowIndicatorColumn Visible="False">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <Columns>
                        <radG:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description" />
                    </Columns>
                    <NoRecordsTemplate>
                        <asp:Literal ID="NoTextDiagMain" runat="server" Text="<b>No Record to Display</b>"></asp:Literal>
                    </NoRecordsTemplate>
                    <HeaderStyle Font-Names="Tahoma" Font-Size="8pt" Font-Underline="true" HorizontalAlign="Left" />
                    <ItemStyle Font-Names="Tahoma, Verdana, Arial" Font-Size="8pt" />
                    <AlternatingItemStyle Font-Names="Tahoma" Font-Size="8pt" />
                </MasterTableView>
                <AJAXLoadingTemplate>
                    <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
                </AJAXLoadingTemplate>
            </radG:RadGrid>--%>
        </div>
    </form>
    <form name="gotoEmailForm" action="Email.aspx" method="post">
        <input type="hidden" name="emailHidden" id="emailHidden" />
        <input type="hidden" name="subject" id="subject" value="Advanced Medical Directives" />
    </form>
    <form name="gotoFaxForm" action="Fax.aspx" method="post">
        <input type="hidden" name="faxHidden" id="faxHidden" />
        <input type="hidden" name="subjectFax" id="subjectFax" value="Advanced Medical Directives" />
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
    </script>

</body>
</html>
