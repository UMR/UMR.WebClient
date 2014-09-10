<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSearchGrid.ascx.cs"
    Inherits="ControlLibrary_crtlSearchGrid" %>
<%--<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>--%>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>

<radG:RadGrid ID="RadGrid1" runat="server" AllowPaging="true" AllowSorting="True" AllowCustomPaging="True"
    EnableAJAX="true" GridLines="None" OnItemCreated="RadGrid1_ItemCreated" PageSize="25"
    Skin="Office2007" UseEmbeddedScripts="false" EnableAJAXLoadingTemplate="true" OnNeedDataSource="RadGrid1_NeedDataSource">
    <MasterTableView AutoGenerateColumns="False">
        <ExpandCollapseColumn Visible="False">
            <HeaderStyle Width="19px"></HeaderStyle>
        </ExpandCollapseColumn>
        <RowIndicatorColumn Visible="False">
            <HeaderStyle Width="20px"></HeaderStyle>
        </RowIndicatorColumn>
        <Columns>
            <radG:GridBoundColumn DataField="ModifierID" UniqueName="ModifierID" Visible="false" />
            <radG:GridHyperLinkColumn DataNavigateUrlFields="PatientKey,Patient_ID,ModifierID" DataNavigateUrlFormatString="~/Oracle/Result.aspx?PatientKey={0}"
                DataTextField="Patient_ID" HeaderText="ID" Target="_blank" UniqueName="PatientID" SortExpression="Patient_ID">
            </radG:GridHyperLinkColumn>
           <%-- <radG:GridTemplateColumn HeaderText="SSN">
                <ItemTemplate>
                    <%#  String.Format("{0:###-##-####}", Double.Parse(Eval("ID").ToString())) %>
                </ItemTemplate>
            </radG:GridTemplateColumn>--%>
            <radG:GridBoundColumn DataField="DOB" HeaderText="Date of Birth" UniqueName="DateOfBirth" />
            <radG:GridBoundColumn DataField="First_Name" HeaderText="First Name" UniqueName="FirstName" />
            <radG:GridBoundColumn DataField="Last_Name" HeaderText="Last Name" UniqueName="LastName" />
            <radG:GridTemplateColumn UniqueName="DANAlert">
                <ItemTemplate>
                <table style="border-spacing:0px;">
                    <%--<tr>
                        <td>
                            <button style="width:220px" onClick="openEmergencyTransferFormRequest(<%# Eval("PatientKey") %>);">
                                Emergency Transfer Form Request</button>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <button style="width:220px" onClick="openDoctorAlertNetwork(<%# Eval("PatientKey") %>);">
                                <asp:Label ID="lblBut" runat="server" Text="Doctor Alert Network"></asp:Label>
                            </button>
                        </td>
                    </tr>

                </table>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="230px" />
            </radG:GridTemplateColumn>
        </Columns>
        <NoRecordsTemplate>
            <asp:Literal ID="NoText" runat="server" Text="<b>No Record to Display</b>" />
        </NoRecordsTemplate>
        <PagerStyle Mode="NextPrevAndNumeric" />
    </MasterTableView>
    <AJAXLoadingTemplate>
        <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
    </AJAXLoadingTemplate>
</radG:RadGrid>

<script type="text/javascript">
            function ShowPatientDrillDown(patientKey)
            {window.open("../Oracle/Result.aspx?PatientKey="+patientKey, null);return false;}
            function ChangeTitle(title)
            {
                document.title=title;
            }
</script>

