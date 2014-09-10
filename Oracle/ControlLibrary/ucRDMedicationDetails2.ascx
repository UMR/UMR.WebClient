<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRDMedicationDetails2.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucRDMedicationDetails2" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<radG:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" EnableAJAX="True" UseEmbeddedScripts="false"
    AllowSorting="true" EnableAJAXLoadingTemplate="true" GridLines="None" OnItemCreated="RadGrid1_ItemCreated"
    PageSize="10" OnSortCommand="RadGrid1_SortCommand" OnNeedDataSource="RadGrid1_NeedDataSource">
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
            <radG:GridTemplateColumn SortExpression="ProviderID" HeaderImageUrl="../../Oracle/images/Unsortedlist.png" ItemStyle-Width="110px"
                UniqueName="ProviderID">
                <ItemTemplate>
                    <asp:HyperLink ID="Provider" runat="server" ForeColor="Blue"></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
                <HeaderTemplate>
                    <asp:LinkButton ID="btnProviderID" Text="Provider ID/Name" runat="server" OnClick="btnProviderID_Click"></asp:LinkButton>
                </HeaderTemplate>
            </radG:GridTemplateColumn>
            <radG:GridTemplateColumn SortExpression="FirstName" HeaderImageUrl="../../Oracle/images/Unsortedlist.png" ItemStyle-Width="110px"
                UniqueName="ProviderName">
                <ItemTemplate>
                    <asp:HyperLink ID="hlProviderName" runat="server" ForeColor="Blue"></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
                <HeaderTemplate>
                    <asp:LinkButton ID="btnProviderName" Text="Provider ID/Name" runat="server" OnClick="btnProviderName_Click"></asp:LinkButton>
                </HeaderTemplate>
            </radG:GridTemplateColumn>
            <radG:GridTemplateColumn SortExpression="InstitutionCode" HeaderImageUrl="../../Oracle/images/Unsortedlist.png" ItemStyle-Width="155px"
                UniqueName="IC">
                <ItemTemplate>
                    <asp:HyperLink ID="Institution" runat="server" ForeColor="Blue"></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
                <HeaderTemplate>
                    <asp:LinkButton ID="btnInstitutionID" Text="Healthcare Facility ID/Name" runat="server"
                        OnClick="btnInstitutionID_Click"></asp:LinkButton>
                </HeaderTemplate>
            </radG:GridTemplateColumn>
            <radG:GridTemplateColumn SortExpression="InstitutionName" HeaderImageUrl="../../Oracle/images/Unsortedlist.png" ItemStyle-Width="155px"
                UniqueName="InstitutionName">
                <ItemTemplate>
                    <asp:HyperLink ID="hlHealthcare" runat="server" ForeColor="Blue"></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
                <HeaderTemplate>
                    <asp:LinkButton ID="btnInstitutionName" Text="Healthcare Facility ID/Name" runat="server"
                        OnClick="btnInstitutionName_Click"></asp:LinkButton>
                </HeaderTemplate>
            </radG:GridTemplateColumn>
        </Columns>
        <NoRecordsTemplate>
            <asp:Literal ID="NoText" runat="server" Text="<b>No Record to Display</b>"></asp:Literal>
        </NoRecordsTemplate>
        <PagerStyle Mode="NextPrevAndNumeric" />
        <HeaderStyle Font-Names="Tahoma" Font-Size="8pt" Font-Underline="true" HorizontalAlign="Left" />
        <ItemStyle Font-Names="Tahoma, Verdana, Arial" Font-Size="8pt" />
        <AlternatingItemStyle Font-Names="Tahoma" Font-Size="8pt" />
    </MasterTableView>
    <AJAXLoadingTemplate>
        <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/loading.gif" />
    </AJAXLoadingTemplate>
</radG:RadGrid>
