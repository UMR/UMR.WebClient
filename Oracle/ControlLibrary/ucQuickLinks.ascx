<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucQuickLinks.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucQuickLinks" %>
<%--<table>
    <tr>
        <td>
            <asp:Image ID="Image4" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/flag4_(add)_16x16.gif" />
            <asp:LinkButton ID="lbMPI" runat="server" Font-Names="Arial, Verdana" Font-Size="8pt" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Image ID="Image3" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/notepad_16x16.gif" />
            <asp:LinkButton ID="lbLRP" runat="server" Font-Size="8pt" Font-Names="Tahoma, Arial, Verdana"/>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Image ID="Image2" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/record_16x16.gif" />
            <asp:LinkButton ID="lbLRA" runat="server" Font-Names="Arial, Verdana" Font-Size="8pt" Style="width: 80px; padding: 0px;" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Image ID="Image5" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/ordered_list_16x16.gif" />
            <asp:LinkButton ID="lbAMD" runat="server" Font-Names="Arial, Verdana" Font-Size="8pt" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Image ID="Image1" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/report3_(add)_16x16.gif" />
            <asp:LinkButton ID="lbHPL" runat="server" Font-Names="Arial, Verdana" Font-Size="8pt" Style="width: 80px; padding: 0px;" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Image ID="img_lbOption" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/table_auto_format_16x16.gif" />
            <asp:LinkButton ID="lbOption" runat="server" Font-Names="Arial, Verdana" Font-Size="8pt" Text="Set My Discipline View Preference" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Image ID="Image6" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/table_auto_format_16x16.gif" />
            <asp:LinkButton ID="LinkButton1" runat="server" Font-Names="Arial, Verdana" Font-Size="8pt" Font-Underline="false" Enabled="false" Text="Institution(s) and/or Healthcare Facility(ies)" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Image ID="Image7" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/table_auto_format_16x16.gif" />
            <asp:LinkButton ID="lbPrimaryProvider" runat="server" Font-Names="Arial, Verdana"
                Font-Size="8pt" Text="Primary Provider Information">
            </asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Image ID="Image8" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/table_auto_format_16x16.gif" />
            <asp:LinkButton ID="lbInsurance" runat="server" Font-Names="Arial, Verdana" Font-Size="8pt"
                Text="Insurance Information">
            </asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Image ID="Image9" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/table_auto_format_16x16.gif" />
            <asp:LinkButton ID="lbEmergency" runat="server" Font-Names="Arial, Verdana" Font-Size="8pt"
                Text="Emergency Information">
            </asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Image ID="Image10" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/table_auto_format_16x16.gif" />
            <asp:LinkButton ID="lbDemographic" runat="server" Font-Names="Arial, Verdana" Font-Size="8pt"
                Text="Patient Demographic">
            </asp:LinkButton>
        </td>
    </tr>
</table>--%>
<table width="100%">
    <tr>
        <td>
            <asp:Image ID="Image4" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/flag4_(add)_16x16.gif" />
        </td>
        <td>
            <asp:LinkButton ID="lbMPI" runat="server" Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt"
                ForeColor="Black" Text="Master Patient Code Indexes">
            </asp:LinkButton>
        </td>
        <td runat="server" visible="false">
            <asp:Image ID="Image3" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/notepad_16x16.gif" />
        </td>
        <td runat="server" visible="false">
            <asp:LinkButton ID="lbLRP" runat="server" Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt"
                ForeColor="Black" Text="Last Ordered Prescription">
            </asp:LinkButton>
        </td>
        <td>
            <asp:Image ID="Image2" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/record_16x16.gif" />
        </td>
        <td>
            <asp:LinkButton ID="lbLRA" runat="server" Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt"
                ForeColor="Black" Text="Last Record Accessed">
            </asp:LinkButton>
        </td>
        <td>
            <asp:Image ID="Image5" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/ordered_list_16x16.gif" />
        </td>
        <td>
            <asp:LinkButton ID="lbAMD" runat="server" Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt"
                ForeColor="Black" Text="Advanced Medical Directives">
            </asp:LinkButton>
        </td>
        <td>
            <asp:Image ID="Image1" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/report3_(add)_16x16.gif" />
        </td>
        <td>
            <asp:LinkButton ID="lbHPL" runat="server" Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt"
                ForeColor="Black" Text="Healthcare Providers">
            </asp:LinkButton>
        </td>
        <td>
            <asp:Image ID="Image6" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/table_auto_format_16x16.gif" />
        </td>
        <td colspan="3">
            <asp:LinkButton ID="lbHF" runat="server" Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt"
                ForeColor="Black" Text="Healthcare Facilities">
            </asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Image ID="Image10" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/table_auto_format_16x16.gif" />
        </td>
        <td>
            <asp:LinkButton ID="lbDemographic" runat="server" Font-Names="Tahoma, Arial, Verdana"
                Font-Size="8pt" ForeColor="Black" Text="Patient Demographics">
            </asp:LinkButton>
        </td>
        <td>
            <asp:Image ID="Image11" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/record_16x16.gif" />
        </td>
        <td>
            <asp:LinkButton ID="lbUnRD" runat="server" Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt"
                ForeColor="Black" Text="Unremarkable Disciplines">
            </asp:LinkButton>
        </td>
        <td runat="server" visible="false">
            <asp:Image ID="Image7" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/report3_(add)_16x16.gif" />
        </td>
        <td runat="server" visible="false">
            <asp:LinkButton ID="lbPrimaryProvider" runat="server" Font-Names="Tahoma, Arial, Verdana"
                Font-Size="8pt" ForeColor="Black" Text="Primary Provider Information">
            </asp:LinkButton>
        </td>
        <td>
            <asp:Image ID="Image9" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/notepad_16x16.gif" />
        </td>
        <td>
            <asp:LinkButton ID="lbEmergency" runat="server" Font-Names="Tahoma, Arial, Verdana"
                Font-Size="8pt" ForeColor="Black" Text="Emergency Information">
            </asp:LinkButton>
        </td>
        <td>
            <asp:Image ID="Image8" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/record_16x16.gif" />
        </td>
        <td>
            <asp:LinkButton ID="lbInsurance" runat="server" Font-Names="Tahoma, Arial, Verdana"
                Font-Size="8pt" ForeColor="Black" Text="Insurance Information">
            </asp:LinkButton>
        </td>
        <td>
            <asp:Image ID="Image12" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/report3_(add)_16x16.gif" />
        </td>
        <td colspan="1">
            <asp:LinkButton ID="lbLegend" runat="server" Font-Names="Tahoma, Arial, Verdana"
                Font-Size="8pt" ForeColor="Black" Text="Legend">
            </asp:LinkButton>
        </td>
        <td colspan="1">
            <asp:Image ID="Image13" runat="server" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/report3_(add)_16x16.gif" />
            <asp:LinkButton ID="lbDisclaimer" runat="server" Font-Names="Tahoma, Arial, Verdana"
                Font-Size="8pt" ForeColor="Black" Text="Disclaimer">
            </asp:LinkButton>
        </td>
    </tr>
</table>

<script type="text/javascript">
            function ShowHPList(patientKey,dispType)
            {
                window.radopen("MultiProviderListDetails.aspx?PatientKey="+patientKey+"&DispType="+dispType, null);
                return false;
            }
            function ShowMPI(patientKey)
            {
                window.radopen("MPI.aspx?PatientKey="+patientKey, null);
                return false;
            }
            function ShowAMD(patientKey)
            {
                window.radopen("AMD.aspx?PatientKey="+patientKey, null);
                return false;
            }
            function ShowLRPM(patientKey)
            {
                window.radopen("LRPM.aspx?PatientKey="+patientKey, null);
                return false;
            }
            function ShowLRA(patientKey)
            {
                window.radopen("LRA.aspx?PatientKey="+patientKey, null);
                return false;
            }
            function SetOption(patientKey)
            {
                window.radopen("../RDOptionPage.aspx?PatientKey="+patientKey, null);
                return false;
            }
            function ShowPrimaryProvider(patientKey)
            {
                window.radopen("PrimaryProvider.aspx?PatientKey="+patientKey, null);
                return false;
            }
            function ShowInsurance(patientKey)
            {
                window.radopen("Insurance.aspx?PatientKey="+patientKey, null);
                return false;
            }
            function ShowEmergency(patientKey)
            {
                window.radopen("Emergency.aspx?PatientKey="+patientKey, null);
                return false;
            }
            function ShowDemographic(patientKey)
            {
                window.radopen("Demographic.aspx?PatientKey="+patientKey, null);
                return false;
            }
            function ShowUnremarkable(patientKey)
            {
                window.radopen("UnRD.aspx?PatientKey="+patientKey, null);
                return false;
            }
            function ShowLegend(patientKey)
            {
                window.radopen("Legend.aspx?PatientKey="+patientKey, null);
                return false;
            }
            function ShowDisclaimer()
            {
                window.radopen("Disclaimer.aspx", null);
                return false;
            }
            function ShowHealthcareFacilities(patientKey)
            {
                window.radopen("HealthcareFacilities.aspx?PatientKey="+patientKey, null);
                return false;
            }
            
</script>

