<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRDMedicationDetails.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucRDMedicationDetails" %>
<%--<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>

<radG:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" EnableAJAX="True" GridLines="None"
    OnItemCreated="RadGrid1_ItemCreated" PageSize="4" Skin="Office2007" EnableAJAXLoadingTemplate="true" OnNeedDataSource="RadGrid1_NeedDataSource" UseEmbeddedScripts="false">
    <MasterTableView AutoGenerateColumns="false">
        <ExpandCollapseColumn Visible="False">
            <HeaderStyle Width="19px" />
        </ExpandCollapseColumn>
        <RowIndicatorColumn Visible="False">
            <HeaderStyle Width="20px" />
        </RowIndicatorColumn>
        <Columns>
            <radG:GridBoundColumn DataField="NDCCode" HeaderText="Code" UniqueName="Code" />
            <radG:GridBoundColumn DataField="CodeVersion" HeaderText="Version" UniqueName="Version" />
            <radG:GridBoundColumn DataField="DateOfService" HeaderText="Date of Service" UniqueName="ServiceDate" />
            <radG:GridBoundColumn DataField="SIG" HeaderText="S.I.G" UniqueName="Desc" />
            <radG:GridTemplateColumn Display="true" HeaderText="Provider ID" UniqueName="TemplateEditColumn">
                <ItemTemplate>
                    <asp:HyperLink ID="Provider" runat="server"></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </radG:GridTemplateColumn>
            <radG:GridBoundColumn DataField="InstitutionCode" HeaderText="Institution" UniqueName="Institution">
            </radG:GridBoundColumn>
        </Columns>
    </MasterTableView>
    <AJAXLoadingTemplate>
        <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
    </AJAXLoadingTemplate>
</radG:RadGrid>
--%>
<asp:GridView ID="grdMedicationDetails" runat="server" AutoGenerateColumns="False"
    AllowSorting="true" Width="100%" OnRowCreated="grdMedicationDetails_RowCreated"
    GridLines="Horizontal" OnSorting="grdMedicationDetails_Sorting">
    <Columns>
        <asp:BoundField DataField="NDCCode" SortExpression="NDCCode" HeaderText="Code" />
        <asp:BoundField DataField="CodeVersion" SortExpression="CodeVersion" HeaderText="Version" />
        <asp:BoundField DataField="DateOfService" SortExpression="DateOfService" HeaderText="Date of Service" />
        <asp:BoundField DataField="SIG" SortExpression="SIG" HeaderText="S.I.G" />
        <asp:BoundField HeaderText="No. of Pills">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Refills">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:TemplateField SortExpression="ProviderID" HeaderText="Provider ID">
            <ItemTemplate>
                <asp:HyperLink ID="Provider" runat="server"></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField SortExpression="InstitutionCode" HeaderText="Healthcare Facility ID">
            <ItemTemplate>
                <asp:HyperLink ID="Institution" runat="server"></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" ForeColor="black"
        HorizontalAlign="Left" />
    <RowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" HorizontalAlign="Left" />
    <AlternatingRowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" HorizontalAlign="Left" />
</asp:GridView>
