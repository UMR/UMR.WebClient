<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMultiProviderListDetails.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucMultiProviderListDetails" %>
<table width="100%">
    <tr style="width: 100%">
        <td valign="top" align="left">
            <div style="font-weight: bold; font-size: 10pt; font-family: 'Tahoma';">
                <span>Primary Care Provider: </span>
                <asp:Label ID="lblPrimaryProviderName" runat="server" Text="Not Provided" ForeColor="MidnightBlue"></asp:Label>
            </div>
            <br />
            <asp:GridView ID="grdProvider" runat="server" AutoGenerateColumns="False" DataKeyNames="DoctorID"
                AllowSorting="true" OnRowCommand="grdProvider_RowCommand" CellPadding="4" ForeColor="#333333"
                GridLines="None" OnRowCreated="grdProvider_RowCreated" OnSorting="grdProvider_Sorting"
                OnRowDataBound="grdProvider_RowDataBound" AllowPaging="true"  OnPageIndexChanging="grdProvider_PageIndexChanging">
                <Columns>
                    <asp:ButtonField CommandName="Details" HeaderText=" ">
                        <ItemStyle HorizontalAlign="Center" Width="0px" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="DoctorID" SortExpression="DoctorID" HeaderText="ID" />
                    <asp:BoundField DataField="FirstName" SortExpression="FirstName" HeaderText="First Name">
                    </asp:BoundField>
                    <asp:BoundField DataField="LastName" SortExpression="LastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="Speciality" SortExpression="Speciality" HeaderText="Specialty" />
                    <asp:TemplateField HeaderText="Degree" SortExpression="Degree">
                        <ItemTemplate>
                            <%#(Eval("Degree") == DBNull.Value) ? "N/A" : Eval("Degree")%>
                        </ItemTemplate>
                        <%--<HeaderTemplate>
                            <asp:Literal ID="hdrDeg" runat="server" Text="Degree"></asp:Literal>
                        </HeaderTemplate>--%>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="LastCodeDate" HeaderText="Service Date" />--%>
                    <asp:TemplateField HeaderText="First Date" SortExpression="FirstCodeDate">
                        <ItemTemplate>
                            <asp:Label ID="lblFirstDate" Text='<%#  DateTime.Parse(Eval("FirstCodeDate").ToString()).ToString("MM/dd/yyyy") %>'
                                runat="server">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Date" SortExpression="LastCodeDate">
                        <ItemTemplate>
                            <asp:Label ID="lblServiceDate" Text='<%#  DateTime.Parse(Eval("LastCodeDate").ToString()).ToString("MM/dd/yyyy") %>'
                                runat="server">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="F" SortExpression="CodeFreq">
                        <ItemTemplate>
                            <asp:Label ID="lblFreq" runat="server" Text='<%# GetFreqLabelText(Eval("CodeFreq").ToString(),Eval("DoctorID").ToString()) %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Left"
                    BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle Font-Names="Tahoma,Arial" Font-Size="11px" BackColor="#F7F6F3" ForeColor="#333333" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
        <td valign="top" align="left" colspan="">
            <asp:DetailsView ID="dvwProvider" runat="server" AutoGenerateRows="False" DataKeyNames="DoctorID"
                GridLines="None" CellPadding="4" ForeColor="#333333" Width="340px">
                <Fields>
                    <asp:BoundField DataField="DoctorID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name:" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name:" />
                    <asp:TemplateField HeaderText="Business Phone:">
                        <ItemTemplate>
                            <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile Phone:">
                        <ItemTemplate>
                            <%#(Eval("CellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("CellPhone")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pager Number:">
                        <ItemTemplate>
                            <%#(Eval("Pager") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Pager")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fax Number:">
                        <ItemTemplate>
                            <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--  <asp:BoundField HeaderText="Healthcare Facility ID:" />--%>
                    <asp:TemplateField HeaderText="Healthcare Facility ID:">
                        <ItemTemplate>
                            <asp:GridView ID="grdHFs" ShowHeader="false" BackColor="#F7F6F3" BorderColor="#F7F6F3"
                                OnRowDataBound="grdHFs_RowDataBound" BorderWidth="1px" CellPadding="4" runat="server"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkHF" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="InstitutionName" />
                                </Columns>
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#003399" />
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

<script type="text/javascript">
 function GetRadWindow()
{
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
} 
GetRadWindow().SetWidth(930);
function ShowDetails(text)
{
}
function ShowAllDate(doctorID,patientKey)
{
    //alert(doctorID+' '+patientID+' '+patientModifiedId);
    window.radopen('ProviderCodeDateHistory.aspx?PatientKey='+patientKey+'&DoctorID='+doctorID,null);
}
</script>

