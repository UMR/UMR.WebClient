<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRD2l.ascx.cs" Inherits="Oracle_ControlLibrary_ucRD2l" %>

<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<div>
    <asp:GridView ID="RadGridRDDiagnosisMain" runat="server" AutoGenerateColumns="False"
        OnRowCreated="RadGridRDDiagnosisMain_RowCreated" Width="100%" GridLines="Both">
        <EmptyDataTemplate>
        <span style="color:Red;font-weight:bold;font-family:Tahoma;font-size:8pt;">No Known Medication Allergy</span>
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblExclamation" runat="server" ForeColor="Red" Font-Names="Tahoma"
                        Font-Size="9pt" Font-Bold="true" />
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
            <asp:BoundField DataField="FirstDate" HeaderText="First Date" ControlStyle-Width="25px">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="LastDate" HeaderText="Last Date" ControlStyle-Width="25px">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Found" HeaderText="F">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle BackColor="PowderBlue" />
            </asp:BoundField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnDetail" runat="server" ImageUrl="~/Oracle/images/DownArrow.png" />
                </ItemTemplate>
                <HeaderStyle Font-Underline="false" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Center" />
        <RowStyle Font-Names="Tahoma,Arial" Font-Size="11px" />
        <SelectedRowStyle ForeColor="Yellow" />
        <AlternatingRowStyle Font-Names="Tahoma, Arial" Font-Size="11px" />
    </asp:GridView>
</div>
