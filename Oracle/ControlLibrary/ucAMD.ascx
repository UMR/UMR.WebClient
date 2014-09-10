<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAMD.ascx.cs" Inherits="Oracle_ControlLibrary_ucAMD" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<radG:RadGrid ID="RadGridAMD" runat="server" AllowPaging="false" EnableAJAX="false"
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
            <asp:Literal ID="NoTextDiagMain" runat="server" Text="<b>N/A</b>"></asp:Literal>
        </NoRecordsTemplate>
        <PagerStyle Mode="NextPrevAndNumeric" />
        <HeaderStyle Font-Names="Tahoma" Font-Size="8pt" Font-Underline="true" HorizontalAlign="Left" />
        <ItemStyle Font-Names="Tahoma, Verdana, Arial" Font-Size="8pt" />
        <AlternatingItemStyle Font-Names="Tahoma" Font-Size="8pt" />
    </MasterTableView>
    <AJAXLoadingTemplate>
        <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
    </AJAXLoadingTemplate>
</radG:RadGrid>

<br />
<table style="font-family: Tahoma; font-size: 8pt; width: 100%">
    <tr>
        <td>
            <span style="text-decoration: underline">Power of Attorney </span>
        </td>
        <td>
            <span style="text-decoration: underline">Durable Power of Attorney</span></td>
    </tr>
</table>
