<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLegend.ascx.cs" Inherits="Oracle_ControlLibrary_ucLegend" %>
<%@ OutputCache Duration="20" VaryByParam="none" %>

<table cellpadding="2" cellspacing="2" class="tableLegend" width="100%">
    <tr>
        <td style="width:60%" class="tdLegend">
            <table class="tableLegend" width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td style="text-align:left; font-weight:bold; width:25%" class="tdLegend">
                        Symbol</td>
                    <td style="text-align: center; font-weight: bold; width:75%" class="tdLegend">
                        Definition</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        Code</td>
                    <td class="tdLegend">
                        Medical classification system, such as: CPT, HCPCS, ICD and NDC</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        Type</td>
                    <td class="tdLegend">
                        Type of code</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        Version</td>
                    <td class="tdLegend">
                        Medical code latest implementation year</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        First Date</td>
                    <td class="tdLegend">
                        Initial date of diagnosis, procedure and/or treatment</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        Last Date</td>
                    <td class="tdLegend">
                        Last date of diagnosis, procedure and/or treatment</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        Medical Content Index</td>
                    <td class="tdLegend">
                        Medical code summation</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        Reference</td>
                    <td class="tdLegend">
                        Related medical disciplines</td>
                </tr>
                <tr class="trLegend">
                    <td class="tdLegend"> 
                        F</td>
                    <td class="tdLegend">
                        Medical code frequency usage</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        Remarkable</td>
                    <td class="tdLegend">
                        Medical codes has been utilized with positive findings</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        Unremarkable</td>
                    <td class="tdLegend">
                        Medical codes not applicable, no positive findings or not available</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        Brand Name</td>
                    <td class="tdLegend">
                        Prescription and medication name</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        Strength</td>
                    <td class="tdLegend">
                        Prescription and medication dosage</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        Route</td>
                    <td class="tdLegend">
                        Prescription and medication administration method</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        S.I.G.</td>
                    <td class="tdLegend">
                        Medication dispensing instructions</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        <b style="color: red">!</b></td>
                    <td class="tdLegend">
                        Medical codes where last update occurred</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        N/A</td>
                    <td class="tdLegend">
                        Not Available</td>
                </tr>
                <tr>
                    <td class="tdLegend" style="background-color: #ff8282"/>
                    <td class="tdLegend">
                        Data sensitive to color legend of the time range that includes
                        <asp:Label ID="lblOption1" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="tdLegend" style="background-color:#F4A460"/>
                    <td class="tdLegend">
                        Data sensitive to color legend of the time range
                        <asp:Label ID="lblOption2" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="tdLegend" style="background-color: #82ff82" />
                    <td class="tdLegend">
                        Data sensitive to color legend of the time range
                        <asp:Label ID="lblOption3" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="tdLegend" style="background-color: #8cdaff"/>
                    <td class="tdLegend">
                        Data sensitive to color legend of the time range
                        next</td>
                </tr>
            </table>
        </td>
       <%-- <td class="tdLegend" style="width: 40%" valign="top">
            <table class="tableLegend" cellspacing="0" cellpadding="2">
                <tr>
                    <td class="tdLegend" style="width: 50%; text-align: center; font-weight: bold;">
                        Definition</td>
                    <td class="tdLegend" style="width: 50%; text-align: center; font-weight: bold;">
                        Color Code</td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        Remarkable Discpline(s) 0 - 1 Years</td>
                    <td class="tdLegend" style="background-color: #ff8282">
                    </td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        Remarkable Discpline(s) 1 - 2 Years</td>
                    <td class="tdLegend" style="background-color: #ffff99">
                    </td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        Remarkable Discpline(s) 2 - 4 Years</td>
                    <td class="tdLegend" style="background-color: #82ff82">
                    </td>
                </tr>
                <tr>
                    <td class="tdLegend">
                        Remarkable Discpline(s) over 4 Years</td>
                    <td class="tdLegend" style="background-color: #8cdaff">
                    </td>
                </tr>
            </table>
        </td>--%>
    </tr>
</table>