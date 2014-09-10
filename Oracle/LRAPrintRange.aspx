<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LRAPrintRange.aspx.cs" Inherits="Oracle_LRAPrintRange" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Last Record Access Details</title>
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
                <asp:RadioButtonList ID="rdbListPrintChoice" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="True" OnSelectedIndexChanged="rdbListPrintChoice_SelectedIndexChanged">
                    <asp:ListItem Selected="True">Print all </asp:ListItem>
                    <asp:ListItem>Print range</asp:ListItem>
                </asp:RadioButtonList>
                <div id="rangeConfig" runat="server" visible="false">
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
                                <asp:TextBox ID="txtPageTo" ForeColor="#336699" Width="50px" runat="server"></asp:TextBox><asp:RequiredFieldValidator
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
            </div>
            <div id="printingDiv" visible="false" runat="server">
                Printing...
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
        </div>
        <div id="printDiv">
            <asp:GridView ID="gridViewLra" runat="server" GridLines="None" AutoGenerateColumns="False"
                Width="100%">
                <Columns>
                    <asp:BoundField DataField="AccessTime" HeaderText="Access Time (EST)" />
                    <asp:TemplateField HeaderText="Record Update Time (EST)">
                        <ItemTemplate>
                            <itemtemplate>
                                <%# (Eval("RecordUpdateTime") == DBNull.Value) ? "N/A" : String.Format("{0:MM-dd-yyyy hh:mm:ss tt}", DateTime.Parse(Eval("RecordUpdateTime").ToString()))%>                              
                            </itemtemplate>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="UserName" HeaderText="Accessor" />
                    <asp:BoundField DataField="Industry" HeaderText="Industry" />
                    <asp:TemplateField HeaderText="Phone">
                        <ItemTemplate>
                            <itemtemplate>
                                <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
                            </itemtemplate>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fax">
                        <ItemTemplate>
                            <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle Font-Names="Tahoma" Font-Size="8pt" Font-Underline="True" HorizontalAlign="Left" />
                <RowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="8pt" />
                <AlternatingRowStyle Font-Names="Tahoma" Font-Size="8pt" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
