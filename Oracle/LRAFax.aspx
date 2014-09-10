<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LRAFax.aspx.cs" Inherits="Oracle_LRAFax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Fax Last Access Records</title>
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
        <div>
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
                <asp:TextBox ID="txtRecipientName" runat="server"></asp:TextBox>
                <asp:Button ID="btnSendFax" runat="server" Text="Send" OnClick="btnSendFax_Click" />
                <asp:Label ID="lblMessage" runat="server"></asp:Label><br />
                &nbsp;<br />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </div>
            <div id="printDiv">
                <asp:GridView ID="gridViewLra" runat="server" GridLines="None" AutoGenerateColumns="False"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="AccessTime" HeaderText="Access Time (EST)">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Record Update Time (EST)">
                        <ItemTemplate>
                            <itemtemplate>
                                <%# (Eval("RecordUpdateTime") == DBNull.Value) ? "N/A" : String.Format("{0:MM-dd-yyyy hh:mm:ss tt}", DateTime.Parse(Eval("RecordUpdateTime").ToString()))%>                              
                            </itemtemplate>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:BoundField DataField="UserName" HeaderText="Accessor">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Industry" HeaderText="Industry">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Phone">
                            <ItemTemplate>
                                <itemtemplate>
                                <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
                            </itemtemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fax">
                            <ItemTemplate>
                                <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle Font-Names="Tahoma" Font-Size="8pt" Font-Underline="True" HorizontalAlign="Left" />
                    <RowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="8pt" />
                    <AlternatingRowStyle Font-Names="Tahoma" Font-Size="8pt" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
