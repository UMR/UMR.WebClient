<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRDQuickLinksButtonsPreview.ascx.cs"
    Inherits="Oracle_ControlLibrary_PrintPreview_ucRDQuickLinksButtonsPreview" %>
<%--<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:DataList ID="dlRDQuickLinks" runat="server" OnItemCreated="dlRDQuickLinks_ItemCreated"
                RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" CellSpacing="1"
                CellPadding="1" Width="100%">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" 
                                    Font-Underline="false" Height="35px" 
                                    Font-Names="Tahoma, Arial" ForeColor="Black"
                                    Font-Bold="true"
                                    CssClass="button">
                    </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt" HorizontalAlign="Left"
                    VerticalAlign="Top" />
            </asp:DataList>
         </td>
    </tr>
</table>--%>
<table border="0" cellpadding="5" cellspacing="0" width="100%">
    <tr style="width: 100%">
        <td style="width: 100%">
            <table>
                <tr>
                    <td>
                        <asp:Panel ID="pnl" runat="server" GroupingText="Code Filter" Width="348px"
                            Font-Names="Tahoma,Arial,Verdana" Font-Size="8pt">
                            <asp:CheckBoxList ID="cblCodeOption" runat="server" Font-Names="Tahoma,Arial,Verdana"
                                Font-Size="8pt" RepeatColumns="6" RepeatDirection="Horizontal" RepeatLayout="flow"
                                ForeColor="Blue">
                                <asp:ListItem Text="CPT" Value="CPT-4" Selected="True" />
                                <asp:ListItem Text="HCPCS" Value="HCPCS" Selected="True" />
                                <asp:ListItem Text="ICD" Value="ICD-9-CM" Selected="True" />
                                <asp:ListItem Text="MDS" Value="MDS" Selected="False" />
                                <asp:ListItem Text="NDC" Value="NDC" Selected="True" />
                                <asp:ListItem Text="OASIS" Value="OASIS" Selected="False" />
                            </asp:CheckBoxList>
                            <asp:Button ID="btnApplyCodeFilter" runat="server" Text="Apply" Font-Names="Tahoma"
                                Font-Size="8pt" ForeColor="Blue" OnClick="btnApplyCodeFilter_Click" />
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="pnlLegend" runat="server" GroupingText="Date Filter" Font-Names="Tahoma,Arial,Verdana"
                            Font-Size="8pt">
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
                                        <asp:Label ID="lblOption2" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel3" runat="server" Height="15px" Width="15px" BackColor="#82ff82">
                                        </asp:Panel>
                                    </td>
                                    <td>
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
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr style="width: 100%">
        <td style="width: 100%">
            <asp:PlaceHolder ID="phRD" runat="server" />
        </td>
    </tr>
</table>
