<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMainProviderDetails.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucMainProviderDetails" %>
<asp:DetailsView ID="dtvMainProviderDetails" runat="server" AutoGenerateRows="False"
    CellSpacing="2" GridLines="None">
    <Fields>
        <asp:BoundField DataField="FirstName" HeaderText="First Name:" ReadOnly="True" ItemStyle-Font-Bold="false"
            ItemStyle-ForeColor="Maroon" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name:" ItemStyle-Font-Bold="false"
            ItemStyle-ForeColor="Maroon" />
        <asp:TemplateField HeaderText="Business Phone:" ShowHeader="true">
            <ItemTemplate>
                <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Mobile Phone:" ShowHeader="true">
            <ItemTemplate>
                <%#(Eval("CellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("CellPhone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:BoundField DataField="ProviderFax" HeaderText="Fax No:" />--%>
        <asp:TemplateField HeaderText="Fax Number:" ShowHeader="true">
            <ItemTemplate>
                <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <%-- <asp:BoundField DataField="ProviderPager" HeaderText="Pager No:" />--%>
        <asp:TemplateField HeaderText="Pager Number:" ShowHeader="true">
            <ItemTemplate>
                <%#(Eval("Pager") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Pager")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:BoundField DataField="ProviderCellPhone" HeaderText="Cellular No:" />--%>
        <asp:BoundField DataField="Speciality" HeaderText="Specialty:" />
        <%--<asp:BoundField HeaderText="Healthcare Facility ID:" />--%>
        <asp:TemplateField HeaderText="Healthcare Facilities:">
            <ItemTemplate>
                <asp:GridView ID="grdHFs" ShowHeader="false" AutoGenerateColumns="false" BackColor="#FFFFFF"
                    BorderColor="#FFFFFF" BorderWidth="1px" CellPadding="4" runat="server">
                    <Columns>
                        <asp:BoundField DataField="Institution_ID" />
                        <asp:BoundField DataField="InstitutionName" />
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                </asp:GridView>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="WebSite" HeaderText="Website:" />
        <asp:BoundField DataField="Email" HeaderText="Email:" />
        <asp:BoundField DataField="Address" HeaderText="Address:" />
        <asp:BoundField DataField="CityTown" HeaderText="City/Town:" />
        <asp:BoundField DataField="County" HeaderText="County:" />
        <asp:BoundField DataField="State" HeaderText="State:" />
        <asp:BoundField DataField="Zip" HeaderText="Zip Code:" />
        <asp:BoundField DataField="Country" HeaderText="Country:" />
    </Fields>
    <RowStyle Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt" HorizontalAlign="Left"
        VerticalAlign="Top" />
</asp:DetailsView>
