<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRDQuickLinksPreview.ascx.cs" Inherits="Oracle_ControlLibrary_PrintPreview_ucRDQuickLinksPreview" %>

<table border="0" cellpadding="5" cellspacing="0">
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
         </td>
    </tr>
    
    
</table>