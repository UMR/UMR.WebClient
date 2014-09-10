<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Copy of ucProviderDetails.ascx.cs" Inherits="Oracle_ControlLibrary_ucProviderDetails" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>


<radg:radgrid id="RadGrid1" runat="server" allowpaging="True" enableajax="True" gridlines="None" EnableAJAXLoadingTemplate="true"
    pagesize="8" skin="Office2007" UseEmbeddedScripts="false" OnNeedDataSource="RadGrid1_NeedDataSource">
    <MasterTableView AutoGenerateColumns="false">
        <ExpandCollapseColumn Visible="False">
            <HeaderStyle Width="19px" />
        </ExpandCollapseColumn>
        <RowIndicatorColumn Visible="False">
            <HeaderStyle Width="20px" />
        </RowIndicatorColumn>
        <Columns>
            <radG:GridBoundColumn DataField="LastName" HeaderText="Last Name" UniqueName="LName" />
            <radG:GridBoundColumn DataField="FirstName" HeaderText="First Name" UniqueName="FName" />
            <%--<radG:GridBoundColumn DataField="Phone" HeaderText="Phone" UniqueName="Phone" />--%>
            <radg:GridTemplateColumn HeaderText="Phone" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###)###-####}", Double.Parse((string)Eval("Phone")))%>
                </ItemTemplate>
            </radg:GridTemplateColumn>
            <%--<radG:GridBoundColumn DataField="Fax" HeaderText="Fax" UniqueName="Fax" />--%>
            <radG:GridTemplateColumn HeaderText="Fax" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###)###-####}", Double.Parse((string)Eval("Fax")))%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <%--<radG:GridBoundColumn DataField="CellPhone" HeaderText="CellPhone" UniqueName="CellPhone" />--%>
            <radG:GridTemplateColumn HeaderText="CellPhone" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("CellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###)###-####}", Double.Parse((string)Eval("CellPhone")))%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <%--<radG:GridBoundColumn DataField="Pager" HeaderText="Pager" UniqueName="Pager" />--%>
            <radG:GridTemplateColumn HeaderText="Pager" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("Pager") == DBNull.Value) ? "N/A" : String.Format("{0:(###)###-####}", Double.Parse((string)Eval("Pager")))%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridBoundColumn DataField="InstitutionID" HeaderText="Institution" UniqueName="Institution" />
            <radG:GridBoundColumn DataField="Speciality" HeaderText="Speciality" UniqueName="Speciality" />
            <radG:GridBoundColumn DataField="WebSite" HeaderText="WebSite" UniqueName="WebSite" />
            <radG:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="Email" />
        </Columns>
    </MasterTableView>
    <AJAXLoadingTemplate>
        <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
    </AJAXLoadingTemplate>
</radg:radgrid>
