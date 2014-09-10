<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucInsuranceInfo.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucInsuranceInfo" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<radG:RadGrid ID="RadGridInsuranceInfo" runat="server" AllowPaging="false" EnableAJAX="false"
    EnableAJAXLoadingTemplate="true" GridLines="None" Skin="None" UseEmbeddedScripts="false"
    OnNeedDataSource="RadGridInsuranceInfo_NeedDataSource">
    <MasterTableView AutoGenerateColumns="false">
        <ExpandCollapseColumn Visible="False">
            <HeaderStyle Width="19px" />
        </ExpandCollapseColumn>
        <RowIndicatorColumn Visible="False">
            <HeaderStyle Width="20px" />
        </RowIndicatorColumn>
        <Columns>
            <radG:GridBoundColumn DataField="NameOfInsCompany" HeaderText="Name" UniqueName="Name" />
            <radG:GridBoundColumn DataField="PolicyNo" HeaderText="Policy No" UniqueName="Policy" />
             <radG:GridBoundColumn DataField="CommencementDate" HeaderText="Commencement Date" UniqueName="CommDate" />
            <radG:GridBoundColumn DataField="ExpirationDate" HeaderText="Expiration Date" UniqueName="ExpDate" />
            <radG:GridBoundColumn DataField="Type" HeaderText="Type" UniqueName="Type" />
            <radG:GridTemplateColumn HeaderText="Phone No">
                <ItemTemplate>
                    <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridTemplateColumn HeaderText="Fax:">
                <ItemTemplate>
                    <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridTemplateColumn HeaderText="Email">
                <ItemTemplate>
                    <%#(Eval("Email") == DBNull.Value) ? "N/A" : Eval("Email")%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridTemplateColumn HeaderText="Website">
                <ItemTemplate>
                    <%#(Eval("Website") == DBNull.Value) ? "N/A" : String.Format("<a href=\"{0}\" target=\"_new\">{1}</a>", Eval("Website").ToString(), Eval("Website").ToString())%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
        </Columns>
        <NoRecordsTemplate>
            <asp:Label ID="lblNoText" runat="server" Text="N/A" Font-Size="8pt" Font-Names="Tahoma,Arial"></asp:Label>
        </NoRecordsTemplate>
        <HeaderStyle Font-Names="Tahoma, Arial" Font-Size="8pt" Font-Bold="True" HorizontalAlign="Left"
            Font-Underline="True" />
        <ItemStyle Font-Names="Tahoman, Arial, Verdana" Font-Size="8pt" />
        <AlternatingItemStyle Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt" />
    </MasterTableView>
    <AJAXLoadingTemplate>
        <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
    </AJAXLoadingTemplate>
</radG:RadGrid>
