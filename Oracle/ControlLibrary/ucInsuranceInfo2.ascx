<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucInsuranceInfo2.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucInsuranceInfo2" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<%--<radG:RadGrid ID="RadGridInsuranceInfo" runat="server" AllowPaging="false" EnableAJAX="false"
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
            <radG:GridBoundColumn DataField="ExpirationDate" HeaderText="Expiration Date" UniqueName="ExpDate" />
            <radG:GridBoundColumn DataField="Type" HeaderText="Type" UniqueName="Type" />
        </Columns>
        <NoRecordsTemplate>
            <asp:Label ID="lblNoText" runat="server" Text="N/A" Font-Size="8pt" Font-Names="Tahoma,Arial"></asp:Label>
        </NoRecordsTemplate>
        <HeaderStyle Font-Names="Tahoma, Arial" Font-Size="8pt" Font-Bold="false" HorizontalAlign="Left"
            Font-Underline="True" />
        <ItemStyle Font-Names="Tahoman, Arial, Verdana" Font-Size="8pt" />
        <AlternatingItemStyle Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt" />
    </MasterTableView>
    <AJAXLoadingTemplate>
        <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
    </AJAXLoadingTemplate>
</radG:RadGrid>--%>
<table>
    <tr>
        <td valign="top" align="left">
            <asp:GridView ID="grdInsurenceInfo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                AllowSorting="true" OnSorting="grdInsurenceInfo_Sorting" OnRowCommand="grdInsurenceInfo_RowCommand"
                ForeColor="#333333" GridLines="None" DataKeyNames="NameOfInsCompany" EmptyDataText="No Insurance Info Found"
                OnRowDataBound="grdInsurenceInfo_RowDataBound">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="" HeaderText=" " ShowHeader="False" />
                    <asp:BoundField DataField="NameOfInsCompany" SortExpression="NameOfInsCompany" HeaderText="Name" />
                    <asp:BoundField DataField="PolicyNo" SortExpression="PolicyNo" HeaderText="Policy No" />
                    <asp:BoundField DataField="Type" SortExpression="Type" HeaderText="Type" />
                     <asp:BoundField DataField="CommencementDate" SortExpression="CommencementDate" HeaderText="Commencement Date" />
                    <asp:BoundField DataField="ExpirationDate" SortExpression="ExpirationDate" HeaderText="Expiration Date" />
                    <%--<asp:TemplateField HeaderText="Phone" ShowHeader="true">
                        <ItemTemplate>
                            <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fax" ShowHeader="true">
                        <ItemTemplate>
                            <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email" ShowHeader="true">
                        <ItemTemplate>
                            <%#(Eval("Email") == DBNull.Value) ? "N/A" : Eval("Email")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Website" ShowHeader="true">
                        <ItemTemplate>
                            <%#(Eval("Website") == DBNull.Value) ? "N/A" : String.Format("<a href=\"{0}\" target=\"_new\">{1}</a>", Eval("Website").ToString(), Eval("Website").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Left"
                    BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle Font-Names="Tahoma,Arial" Font-Size="11px" BackColor="#F7F6F3" ForeColor="#333333" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EmptyDataRowStyle Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="SlateGray" />
            </asp:GridView>
        </td>
        <td valign="top" align="left">
            <asp:DetailsView ID="dvwInsurenceInfo" runat="server" AutoGenerateRows="False" GridLines="None"
                CellPadding="4" ForeColor="#333333">
                <Fields>
                    <%--   <asp:BoundField DataField="NameOfInsCompany" HeaderText="Name:" />
                    <asp:BoundField DataField="PolicyNo" HeaderText="Policy No:" />
                    <asp:BoundField DataField="Type" HeaderText="Type:" />
                    <asp:BoundField DataField="ExpirationDate" HeaderText="Expiration Date:" />--%>
                    <asp:TemplateField HeaderText="Phone No:" ShowHeader="true">
                        <ItemTemplate>
                            <%# String.IsNullOrEmpty(Eval("Phone").ToString()) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fax:" ShowHeader="true">
                        <ItemTemplate>
                            <%#String.IsNullOrEmpty(Eval("Fax").ToString()) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email:" ShowHeader="true">
                        <ItemTemplate>
                            <%#String.IsNullOrEmpty(Eval("Email").ToString()) ? "N/A" : Eval("Email")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Website:" ShowHeader="true">
                        <ItemTemplate>
                            <%#String.IsNullOrEmpty(Eval("Website").ToString()) ? "N/A" : String.Format("<a href=\"{0}\" target=\"_new\">{1}</a>", Eval("Website").ToString(), Eval("Website").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Fields>
                <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Center"
                    BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle Font-Names="Tahoma,Arial" Font-Size="11px" BackColor="#F7F6F3" ForeColor="#333333" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:DetailsView>
        </td>
    </tr>
</table>
