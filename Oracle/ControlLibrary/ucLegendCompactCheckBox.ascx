<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLegendCompactCheckBox.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucLegendCompactCheckBox" %>
<asp:Panel ID="pnlLegend" runat="server" GroupingText="Date Filter" Font-Names="Tahoma,Arial,Verdana"
    Font-Size="8pt" Width="634px">
    <table>
        <tr valign="middle">
            <td>
                <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" />
            </td>
            <td>
                <asp:Panel ID="Panel1" runat="server" Height="15px" Width="15px" BackColor="#ff8282">
                </asp:Panel>
            </td>
            <td>
                <asp:Label ID="lblOption1" runat="server"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="CheckBox2" runat="server" Checked="True" />
            </td>
            <td>
                <asp:Panel ID="Panel2" runat="server" Height="15px" Width="15px" BackColor="#F4A460">
                </asp:Panel>
            </td>
            <td>
                <asp:Label ID="lblOption2" runat="server"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="CheckBox3" runat="server" Checked="True" />
            </td>
            <td>
                <asp:Panel ID="Panel3" runat="server" Height="15px" Width="15px" BackColor="#82ff82">
                </asp:Panel>
            </td>
            <td>
                <asp:Label ID="lblOption3" runat="server"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="CheckBox4" runat="server" Checked="True" />
            </td>
            <td>
                <asp:Panel ID="Panel4" runat="server" Height="15px" Width="15px" BackColor="#8cdaff">
                </asp:Panel>
            </td>
            <td>
                Next
            </td>
            <td>
                <asp:Button ID="btnApply" runat="server" Text="Apply" OnClick="btnApply_Click" Font-Names="Tahoma"
                    Font-Size="8pt" ForeColor="Blue" />
            </td>
        </tr>
    </table>
</asp:Panel>
