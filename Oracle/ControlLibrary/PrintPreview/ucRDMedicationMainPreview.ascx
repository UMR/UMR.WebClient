<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRDMedicationMainPreview.ascx.cs"
    Inherits="Oracle_ControlLibrary_PrintPreview_ucRDMedicationMainPreview" %>
<asp:GridView ID="RadGridRDMedicationMain" runat="server" AutoGenerateColumns="False"
    Width="100%" OnRowCreated="RadGridRDMedicationMain_RowCreated" GridLines="Both">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblExclamation" runat="server" ForeColor="Red" Font-Names="Tahoma"
                    Font-Size="9pt" Font-Bold="true" />
            </ItemTemplate>
            <HeaderStyle Font-Underline="false" />
            <ItemStyle HorizontalAlign="center" BackColor="white" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Prescription">
            <ItemTemplate>
                <asp:HyperLink ID="Brand" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Strength" HeaderText="Strength" />
        <asp:BoundField DataField="Route" HeaderText="Route" />
        <asp:BoundField HeaderText="No. of Pills">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Refills">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="FirstDate" HeaderText="First Date" />
        <asp:BoundField DataField="LastDate" HeaderText="Last Date" />
        <asp:BoundField DataField="F" HeaderText="F">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>
    </Columns>
    <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Left" />
    <RowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" HorizontalAlign="Left" />
    <AlternatingRowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" HorizontalAlign="Left" />
</asp:GridView>
