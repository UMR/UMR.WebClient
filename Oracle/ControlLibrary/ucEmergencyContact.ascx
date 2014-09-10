<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucEmergencyContact.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucEmergencyContact" %>
<asp:DetailsView ID="dtvEmergencyContact" runat="server" AutoGenerateRows="False"
    GridLines="None" CellSpacing="2">
    <Fields>
        <%--<asp:BoundField DataField="ContactName" HeaderText="Name:" ReadOnly="True" />--%>
        <asp:BoundField DataField="ContactFirstName" HeaderText="First Name:" />
        <asp:BoundField DataField="ContactLastName" HeaderText="Last Name:" />
        <asp:BoundField DataField="ContactRelation" HeaderText="Relation:" />
        <%--<asp:BoundField DataField="HomePhone" HeaderText="Home Phone:" />--%>
        <asp:TemplateField HeaderText="Home Phone:" ShowHeader="true">
            <ItemTemplate>
                <%#(Eval("ContactHomePhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactHomePhone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:BoundField DataField="BusinessPhone" HeaderText="Business Phone:" />--%>
        <asp:TemplateField HeaderText="Business Phone:" ShowHeader="true">
            <ItemTemplate>
                <%#(Eval("ContactBusinessPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactBusinessPhone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Mobile Phone:" ShowHeader="true">
            <ItemTemplate>
                <%#(Eval("ContactCellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactCellPhone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:BoundField DataField="PagerNo" HeaderText="Pager No:" />--%>
        <asp:TemplateField HeaderText="Pager Number:" ShowHeader="true">
            <ItemTemplate>
                <%#(Eval("ContactPagerNo") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactPagerNo")))%>
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
    <RowStyle Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt" HorizontalAlign="Left"
        VerticalAlign="Top" />
</asp:DetailsView>
<asp:GridView ID="grdEmergencyContact" runat="server" AutoGenerateColumns="false"
    Width="100%" GridLines="None">
    <Columns>
        <asp:BoundField DataField="ContactFirstName" HeaderText="First Name" />
        <asp:BoundField DataField="ContactLastName" HeaderText="Last Name" />
        <asp:BoundField DataField="ContactRelation" HeaderText="Relation" />
        <asp:TemplateField HeaderText="Home Phone" ShowHeader="true">
            <ItemTemplate>
                <%#(Eval("ContactHomePhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactHomePhone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Business Phone" ShowHeader="true">
            <ItemTemplate>
                <%#(Eval("ContactBusinessPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactBusinessPhone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Mobile Phone" ShowHeader="true">
            <ItemTemplate>
                <%#(Eval("ContactCellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactCellPhone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pager Number" ShowHeader="true">
            <ItemTemplate>
                <%#(Eval("ContactPagerNo") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("ContactPagerNo")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="ContactEmail" HeaderText="Email" />
        <asp:BoundField DataField="ContactStreetAddress" HeaderText="Address" />
        <asp:BoundField DataField="ContactCityTown" HeaderText="City/Town" />
        <asp:BoundField DataField="ContactCounty" HeaderText="County" />
        <asp:BoundField DataField="ContactState" HeaderText="State" />
        <asp:BoundField DataField="ContactZip" HeaderText="Zip Code" />
        <asp:BoundField DataField="ContactCountry" HeaderText="Country" />
    </Columns>
    <RowStyle Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt" HorizontalAlign="Left"
        VerticalAlign="Top" />
    <HeaderStyle Font-Names="Tahoma, Arial" Font-Size="8pt" Font-Bold="false" HorizontalAlign="Left"
        Font-Underline="True" />
</asp:GridView>
