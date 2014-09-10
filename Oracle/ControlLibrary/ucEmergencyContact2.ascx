<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucEmergencyContact2.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucEmergencyContact2" %>
<table>
    <tr>
        <td align="left" valign="top">
            <asp:GridView ID="grdViewEmergencyContact" AutoGenerateColumns="False" CellPadding="4"
                AllowSorting="true" DataKeyNames="ContactFirstName,ContactLastName,ContactRelation"
                ForeColor="#333333" GridLines="None" runat="server" OnRowCommand="grdViewEmergencyContact_RowCommand"
                OnRowDataBound="grdViewEmergencyContact_RowDataBound" OnSorting="grdViewEmergencyContact_Sorting">
                <Columns>
                    <asp:CommandField ShowSelectButton="true" SelectText="" ShowHeader="false" HeaderText=" " />
                    <%--<asp:BoundField DataField="ContactName" HeaderText="Name:" ReadOnly="True" />--%>
                    <asp:BoundField DataField="ContactFirstName" SortExpression="ContactFirstName" HeaderText="First Name" />
                    <asp:BoundField DataField="ContactLastName" SortExpression="ContactLastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="ContactRelation" SortExpression="ContactRelation" HeaderText="Relation" />
                    <%--<asp:BoundField DataField="HomePhone" HeaderText="Home Phone:" />--%>
                    <%--<asp:TemplateField HeaderText="Home Phone" ShowHeader="true">
                        <ItemTemplate>
                            <%#(Eval("ContactHomePhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactHomePhone")))%>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--<asp:BoundField DataField="BusinessPhone" HeaderText="Business Phone:" />--%>
                    <%-- <asp:TemplateField HeaderText="Business Phone" ShowHeader="true">
                        <ItemTemplate>
                            <%#(Eval("ContactBusinessPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactBusinessPhone")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile Phone" ShowHeader="true">
                        <ItemTemplate>
                            <%#(Eval("ContactCellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactCellPhone")))%>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--<asp:BoundField DataField="PagerNo" HeaderText="Pager No:" />--%>
                    <%-- <asp:TemplateField HeaderText="Pager Number" ShowHeader="true">
                        <ItemTemplate>
                            <%#(Eval("ContactPagerNo") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactPagerNo")))%>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Left"
                    BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle Font-Names="Tahoma,Arial" Font-Size="11px" BackColor="#F7F6F3" ForeColor="#333333" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="False" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
        <td valign="top" align="left">
            <asp:DetailsView ID="dvwEmergencyContact" runat="server" AutoGenerateRows="False"
                GridLines="None" CellPadding="4" ForeColor="#333333">
                <Fields>
                    <asp:BoundField DataField="ContactFirstName" HeaderText="First Name:" />
                    <asp:BoundField DataField="ContactLastName" HeaderText="Last Name:" />
                    <asp:BoundField DataField="ContactRelation" HeaderText="Relation:" />
                    <asp:TemplateField HeaderText="Home Phone:" ShowHeader="true">
                        <ItemTemplate>
                            <%# String.IsNullOrEmpty(Eval("ContactHomePhone").ToString()) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactHomePhone")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Business Phone:" ShowHeader="true">
                        <ItemTemplate>
                            <%# String.IsNullOrEmpty(Eval("ContactBusinessPhone").ToString()) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactBusinessPhone")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile Phone:" ShowHeader="true">
                        <ItemTemplate>
                            <%# String.IsNullOrEmpty(Eval("ContactCellPhone").ToString()) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactCellPhone")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pager Number:" ShowHeader="true">
                        <ItemTemplate>
                            <%# String.IsNullOrEmpty(Eval("ContactPagerNo").ToString()) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactPagerNo")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ContactEmail" HeaderText="Email:" />
                    <asp:BoundField DataField="ContactStreetAddress" HeaderText="Address:" />
                    <asp:BoundField DataField="ContactCityTown" HeaderText="City/Town:" />
                    <asp:BoundField DataField="ContactCounty" HeaderText="County:" />
                    <asp:BoundField DataField="ContactState" HeaderText="State:" />
                    <asp:BoundField DataField="ContactZip" HeaderText="Zip Code:" />
                    <asp:BoundField DataField="ContactCountry" HeaderText="Country:" />
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
