<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRDDiagnosisMainPreview.ascx.cs"
    Inherits="Oracle_ControlLibrary_PrintPreview_ucRDDiagnosisMainPreview" %>
<asp:GridView ID="RadGridRDDiagnosisMain" runat="server" AutoGenerateColumns="False"
    OnRowCreated="RadGridRDDiagnosisMain_RowCreated" Width="100%" GridLines="Both">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblExclamation" runat="server" ForeColor="Red" Font-Names="Tahoma"
                    Font-Size="8pt" Font-Bold="true" />
            </ItemTemplate>
            <HeaderStyle Font-Underline="false" />
            <ItemStyle HorizontalAlign="center" BackColor="white" />
        </asp:TemplateField>
        <asp:BoundField DataField="CodeType" SortExpression="CodeType" HeaderText="Code Type">
            <HeaderStyle HorizontalAlign ="Left" Width="100px" />
            <ItemStyle HorizontalAlign="Left" Width="100px"/>
        </asp:BoundField>
        <asp:TemplateField HeaderText="Medical Content Index">
            <ItemTemplate>
                <asp:HyperLink ID="Details" runat="server" />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" Width="60%" />
        </asp:TemplateField>
        <asp:BoundField DataField="FirstDate" HeaderText="First Date">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="LastDate" HeaderText="Last Date">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Found" HeaderText="F">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Right" />
            <FooterStyle BackColor="PowderBlue" />
        </asp:BoundField>
    </Columns>
    <HeaderStyle Font-Names="Tahoma" Font-Size="8pt" Font-Underline="True" HorizontalAlign="Center" />
    <RowStyle Font-Names="Tahoma,Arial" Font-Size="8pt" />
    <SelectedRowStyle ForeColor="Yellow" />
    <AlternatingRowStyle Font-Names="Tahoma, Arial" Font-Size="8pt" />
</asp:GridView>
