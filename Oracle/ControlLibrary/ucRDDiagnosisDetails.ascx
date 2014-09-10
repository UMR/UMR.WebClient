<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRDDiagnosisDetails.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucRDDiagnosisDetails" %>
<%--<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>

<radG:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" EnableAJAX="True" GridLines="None" EnableAJAXLoadingTemplate="true"
    OnItemCreated="RadGrid1_ItemCreated" PageSize="4" Skin="Office2007" OnNeedDataSource="RadGrid1_NeedDataSource" UseEmbeddedScripts="false">
    <MasterTableView AutoGenerateColumns="false">
        <ExpandCollapseColumn Visible="False">
            <HeaderStyle Width="19px"></HeaderStyle>
        </ExpandCollapseColumn>
        <RowIndicatorColumn Visible="False">
            <HeaderStyle Width="20px"></HeaderStyle>
        </RowIndicatorColumn>
        <Columns>
            <radG:GridBoundColumn DataField="Code" HeaderText="Code" UniqueName="Code" />
            <radG:GridBoundColumn DataField="CodeType" HeaderText="Type" UniqueName="Type" />
            <radG:GridBoundColumn DataField="CodeVersion" HeaderText="Version" UniqueName="Version" />
            <radG:GridBoundColumn DataField="DateOfService" HeaderText="Date of Service" UniqueName="ServiceDate" />
            <radG:GridTemplateColumn Display="true" HeaderText="Provider ID" UniqueName="TemplateEditColumn">
                <ItemTemplate>
                    <asp:HyperLink ID="Provider" runat="server"></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </radG:GridTemplateColumn>
            <radG:GridBoundColumn DataField="InstitutionCode" HeaderText="Institution" UniqueName="Institution" />
        </Columns>
    </MasterTableView>
    <AJAXLoadingTemplate>
        <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
    </AJAXLoadingTemplate>
</radG:RadGrid>
--%>
<asp:GridView ID="grdDiagnosisDetails" runat="server" AutoGenerateColumns="False"
    AllowSorting="true" GridLines="Horizontal" Width="100%" OnRowCreated="grdDiagnosisDetails_RowCreated"
    OnSorting="grdDiagnosisDetails_Sorting">
    <Columns>
        <asp:BoundField DataField="Code" SortExpression="Code" HeaderText="Code" />
        <asp:TemplateField HeaderText="Modifier">
            <ItemTemplate>
                <asp:Literal ID="ltCodeModifier" runat="server" Text="">
                </asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CodeType" SortExpression="CodeType" HeaderText="Type" />
        <asp:BoundField DataField="CodeVersion" SortExpression="CodeVersion" HeaderText="Version" />
        <%--<asp:BoundField DataField="CodeDescription" HeaderText="Description" />--%>
        <asp:BoundField DataField="DateOfService" SortExpression="DateOfService" HeaderText="Date of Service" />
        <asp:TemplateField ShowHeader="true" SortExpression="ProviderID" HeaderText="Provider ID">
            <ItemTemplate>
                <asp:HyperLink ID="Provider" runat="server"></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ShowHeader="true" SortExpression="InstitutionCode" HeaderText="Healthcare Facility ID">
            <ItemTemplate>
                <asp:HyperLink ID="Institution" runat="server"></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" ForeColor="black"
        HorizontalAlign="Left" />
    <RowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" />
</asp:GridView>
