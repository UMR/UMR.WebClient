<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MPIFax.aspx.cs" Inherits="Oracle_MPIFax" %>

<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Email Master Patient Code Indexes</title>
    <style type="text/css" media="screen">
        #printDiv
        {
          display:none;
        }
        #displayDiv
        {
         font-family: 'Tahoma'; 
         font-size:8pt;
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
            Total Pages:
            <asp:Label ID="lblTotalPages" runat="server"></asp:Label>
            <br />
            <br />
            <asp:RadioButtonList ID="rdbListPrintChoice" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdbListPrintChoice_SelectedIndexChanged"
                RepeatDirection="Horizontal">
                <asp:ListItem Selected="True">Fax all </asp:ListItem>
                <asp:ListItem>Fax range</asp:ListItem>
            </asp:RadioButtonList><div id="rangeConfig" runat="server" visible="false">
                <table>
                    <tr>
                        <td>
                            Fax Pages From:</td>
                        <td>
                            <asp:TextBox ID="txtPageFrom" runat="server" ForeColor="#336699" Width="50px">1</asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFrom" runat="server" ControlToValidate="txtPageFrom"
                                ErrorMessage="Pages From  Required">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvFrom" runat="server" ControlToValidate="txtPageFrom"
                                ErrorMessage="Invalid From" Operator="GreaterThan" Type="Integer" ValueToCompare="0">*</asp:CompareValidator></td>
                        <td>
                            To:</td>
                        <td>
                            <asp:TextBox ID="txtPageTo" runat="server" ForeColor="#336699" Width="50px"></asp:TextBox><asp:RequiredFieldValidator
                                ID="rfvTo" runat="server" ControlToValidate="txtPageTo" ErrorMessage="Pages To Required">*</asp:RequiredFieldValidator><asp:CompareValidator
                                    ID="cvTo" runat="server" ControlToCompare="txtPageFrom" ControlToValidate="txtPageTo"
                                    ErrorMessage="Invalid To" Operator="GreaterThanEqual" Type="Integer">*</asp:CompareValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            Fax Number:
            <asp:TextBox ID="txtFaxNo" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFaxNo"
                ErrorMessage="Fax Number Required">*</asp:RequiredFieldValidator>
            Recipient Name:<asp:TextBox ID="txtRecipient" runat="server"></asp:TextBox>
            <asp:Button ID="btnSendFax" runat="server" Text="Send" OnClick="btnSendFax_Click" />
            <asp:Label ID="lblMessage" runat="server"></asp:Label><br />
            &nbsp;<br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        </div>
        <div id="printDiv">
            <asp:GridView ID="grdMPI" runat="server" AutoGenerateColumns="False" GridLines="None"
                Width="100%" OnRowDataBound="grdMPI_RowDataBound">
                <HeaderStyle Font-Names="Tahoma" Font-Size="8pt" Font-Underline="True" HorizontalAlign="Center" />
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
                <RowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="8pt" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
