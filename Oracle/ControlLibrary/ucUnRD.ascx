<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUnRD.ascx.cs" Inherits="Oracle_ControlLibrary_ucUnRD" %>


<asp:DataList ID="dlNRD" runat="server" CellPadding="2" CellSpacing="2" RepeatColumns="6"
    RepeatDirection="Horizontal" RepeatLayout="Table" Width="100%">
    <ItemTemplate>
        <%#(Eval("DETAIL"))%>
    </ItemTemplate>
    <ItemStyle Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt" ForeColor="Blue" HorizontalAlign="Left"
        VerticalAlign="Top" />
</asp:DataList>