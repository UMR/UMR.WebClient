<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRDQuickLinks.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucRDQuickLinks" %>


<table border="0" cellpadding="5" cellspacing="0">
    <tr>
        <td>
            <asp:Panel ID="pnl" runat="server" GroupingText="Code Filter" Width="348px" Font-Names="Tahoma,Arial,Verdana" Font-Size="8pt" >
                <asp:CheckBoxList ID="cblCodeOption" runat="server" Font-Names="Tahoma,Arial,Verdana" Font-Size="8pt"
                    RepeatColumns="6" RepeatDirection="Horizontal" RepeatLayout="flow" ForeColor="Blue">
                                <asp:ListItem Text="CPT" Value="CPT-4" Selected="True" />
                                <asp:ListItem Text="HCPCS" Value="HCPCS" Selected="True" />
                                <asp:ListItem Text="ICD" Value="ICD-9-CM" Selected="True" />
                                <asp:ListItem Text="MDS" Value="MDS" Selected="False" />
                                <asp:ListItem Text="NDC" Value="NDC" Selected="True" />
                                <asp:ListItem Text="OASIS" Value="OASIS" Selected="False" />
                </asp:CheckBoxList>
                <asp:Button ID="btnApplyCodeFilter" runat="server" Text="Apply" Font-Names="Tahoma" Font-Size="8pt" ForeColor="Blue" OnClick="btnApplyCodeFilter_Click" />
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataList ID="dlRDQuickLinks" runat="server" OnItemCreated="dlRDQuickLinks_ItemCreated"
                RepeatColumns="8" RepeatDirection="Horizontal" RepeatLayout="Table" CellSpacing="1"
                CellPadding="1" Width="100%">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" Font-Underline="true" runat="server" Font-Names="Tahoma, Arial">test</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt" HorizontalAlign="Left"
                    VerticalAlign="Top" />
            </asp:DataList>
          <%--<asp:LinkButton ID="lbNRD" runat="server" Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt"
                Style="width: 80px; padding: 0px;" Text="Unremarkable Discipline(s)">
                //Following style was used to make the linkButton look like regular button...
                style="border:1px solid gray;padding:3px;background-color:#95ACD5;color:Black;background-image:url(/UMR.WebClient/RadControls/Dock/Skins/Office2007/img/titleBarBg.gif)" 
            </asp:LinkButton>--%>

        </td>
    </tr>
    
    
</table>
