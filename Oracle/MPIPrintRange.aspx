<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MPIPrintRange.aspx.cs" Inherits="Oracle_MPIPrintRange" %>

<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Master Patient Code Indexes</title>
    <style type="text/css" media="screen">
        #printDiv
        {
            display:none;
        }
        #displayDiv
        {
         font-family: 'trebuchet MS'; 
         font-size: large; 
         font-weight: bold; 
         font-style: normal; 
         color: #336699   
        }
    </style>
    <style type="text/css" media="print">
        #displayDiv
        {
            display:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="displayDiv">
            <div style="font-size: small;">
                Total Pages:
                <asp:Label ID="lblTotalPages" runat="server" />
                <br />
                <br />
                <asp:RadioButtonList ID="rdbListPrintChoice" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="True" OnSelectedIndexChanged="rdbListPrintChoice_SelectedIndexChanged">
                    <asp:ListItem Selected="True">Print all </asp:ListItem>
                    <asp:ListItem>Print range</asp:ListItem>
                </asp:RadioButtonList><div id="rangeConfig" runat="server" visible="false">
                    <table>
                        <tr>
                            <td>
                                Print Pages From:</td>
                            <td>
                                <asp:TextBox ID="txtPageFrom" ForeColor="#336699" Width="50px" runat="server">1</asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFrom" runat="server" ControlToValidate="txtPageFrom"
                                    ErrorMessage="Pages From  Required">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvFrom" runat="server" ControlToValidate="txtPageFrom"
                                    ErrorMessage="Invalid From" Operator="GreaterThan" Type="Integer" ValueToCompare="0">*</asp:CompareValidator></td>
                            <td>
                                To:</td>
                            <td>
                                <asp:TextBox ID="txtPageTo" Width="50px" ForeColor="#336699" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="rfvTo" runat="server" ControlToValidate="txtPageTo" ErrorMessage="Pages To Required">*</asp:RequiredFieldValidator><asp:CompareValidator
                                        ID="cvTo" runat="server" ControlToCompare="txtPageFrom" ControlToValidate="txtPageTo"
                                        ErrorMessage="Invalid To" Operator="GreaterThanEqual" Type="Integer">*</asp:CompareValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:ImageButton ID="btnPrint" ImageUrl="~/Oracle/images/printbutton.png" runat="server"
                    OnClick="btnPrint_Click" /><br />
                <%--<img alt="Print Range Button" src="images/printbutton.png" style="cursor: hand" id="IMG1"
                    onclick="return printButton_Click()" />--%>
                <br />
            </div>
            <div id="printingDiv" visible="false" runat="server">
                Printing...
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
        </div>
        <div id="printDiv">
            <asp:GridView ID="grdMPI" runat="server" AutoGenerateColumns="False" GridLines="None"
                Width="100%" OnRowDataBound="grdMPI_RowDataBound">
                <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField DataField="Code" HeaderText="Code">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Modifier">
                        <ItemTemplate>
                            <asp:Literal ID="ltCodeModifier" runat="server" Text="">
                            </asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Type" HeaderText="Type">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Version" HeaderText="Version">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MedicalContentIndex" HeaderText="Medical Content Index">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DateOfService" HeaderText="Service Date">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ServiceTime" HeaderText="Time (EST)">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ProviderID" HeaderText="Provider ID">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="InstituteCode" HeaderText="Healthcare Facility ID">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
                <RowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" />
            </asp:GridView>
        </div>
    </form>
    <radW:RadWindowManager ID="RADWMMPI" runat="server" InitialBehavior="maximize" Skin="Office2007" />
</body>

<script type="text/javascript">
    function ShowProviderDetails(id,dispType)
    {
        window.radopen("ProviderDetails.aspx?ID="+id+"&DispType="+dispType, null);
        return false;
    }
           
</script>

</html>
