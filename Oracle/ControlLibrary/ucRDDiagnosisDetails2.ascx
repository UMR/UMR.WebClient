<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRDDiagnosisDetails2.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucRDDiagnosisDetails2" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>

<radG:RadGrid ID="RadGridMPI" runat="server" AllowPaging="True" EnableAJAX="True"
    UseEmbeddedScripts="false" AllowSorting="true" EnableAJAXLoadingTemplate="true"
    GridLines="None" OnItemCreated="RadGridMPI_ItemCreated" PageSize="10" OnSortCommand="RadGridMPI_SortCommand"
    OnNeedDataSource="RadGridMPI_NeedDataSource">
    <MasterTableView AutoGenerateColumns="False">
        <Columns>
            <radG:GridBoundColumn DataField="Code" HeaderText="Code" UniqueName="Code" />
            <radG:GridTemplateColumn HeaderText="Modifier">
                <ItemTemplate>
                    <asp:Literal ID="ltCodeModifier" runat="server" Text="">
                    </asp:Literal>
                </ItemTemplate>
                <HeaderStyle Font-Bold="False" />
            </radG:GridTemplateColumn>
            <radG:GridBoundColumn DataField="CodeType" HeaderText="Type" UniqueName="Type" />
            <radG:GridBoundColumn DataField="CodeVersion" HeaderText="Version" UniqueName="Version" />
            <radG:GridBoundColumn DataField="DateOfService" HeaderText="Service Date" UniqueName="SDate" />
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
