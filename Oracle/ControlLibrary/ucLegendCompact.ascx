<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLegendCompact.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucLegendCompact" %>
<asp:Panel ID="pnlLegend" runat="server" GroupingText="Date Filter" Font-Names="Tahoma,Arial,Verdana"
    Font-Size="8pt" Width="400px">
    <table>
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" Height="15px" Width="15px" BackColor="#ff8282">
                </asp:Panel>
            </td>
            <td>
                <asp:Label ID="lblOption1" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="Panel2" runat="server" Height="15px" Width="15px" BackColor="#F4A460">
                </asp:Panel>
            </td>
            <td>
                Next
                <asp:Label ID="lblOption2" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="Panel3" runat="server" Height="15px" Width="15px" BackColor="#82ff82">
                </asp:Panel>
            </td>
            <td>
                Next
                <asp:Label ID="lblOption3" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="Panel4" runat="server" Height="15px" Width="15px" BackColor="#8cdaff">
                </asp:Panel>
            </td>
            <td>
                Next
            </td>
        </tr>
    </table>
</asp:Panel>
