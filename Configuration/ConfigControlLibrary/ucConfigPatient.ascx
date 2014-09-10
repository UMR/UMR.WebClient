<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucConfigPatient.ascx.cs"
    Inherits="Configuration_ConfigControlLibrary_ucConfigPatient" %>
<%@ Register Assembly="RadTabStrip.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>

<%--<%@ Register Src="~/Configuration/ConfigControlLibrary/PatientDemographic/ucPatientDemographic.ascx" TagName="DemogGeneral" TagPrefix="ucPG" %>
<%@ Register Src="~/Configuration/ConfigControlLibrary/PatientDemographic/ucPatientPhones.ascx" TagName="DemogPhone" TagPrefix="ucPP" %>
--%>

<table id="Table2" border="1" cellpadding="1" cellspacing="2" rules="none" style="border-collapse: collapse"
    width="100%">
    <tr>
        <rad:RadTabStrip ID="tabPatient" runat="server" MultiPageID="multiMain" 
            SelectedIndex="0" Skin="Office2007">
            <Tabs>
                <rad:Tab ID="tabGeneral" runat="server" Text="General Information" />
                <rad:Tab ID="tabPhone" runat="server" Text="Phone(s) Information" />
            </Tabs>
        </rad:RadTabStrip>
        <rad:RadMultiPage ID="multiMain" runat="server">
            <rad:PageView ID="pgGeneral" runat="server" OnLoad="pgGeneral_Load">
                <asp:Table ID="tblGeneral" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            First Name:
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtFName" runat="server"/>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Last Name: 
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtLName" runat="server"/>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </rad:PageView>
            <rad:PageView ID="pgPhone" runat="server" OnLoad="pgPhone_Load">
                <%--<ucPP:DemogPhone ID="ucPhone" runat="server" />--%>
            </rad:PageView>
        </rad:RadMultiPage>
    </tr>
</table>
