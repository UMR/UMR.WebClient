<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPatientDemographicPreview.ascx.cs"
    Inherits="Oracle_ControlLibrary_PrintPreview_ucPatientDemographicPreview" %>
<%--Original--%>
<%--<table >
    <tr>
        <td style="width: 96px" >
            <asp:Label ID="label8" runat="server" CssClass="lblDemographic" Text="Patient ID:" />
        </td>
        <td style="width: 211px" >
            <asp:Label ID="Label25" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
        <td style="width: 131px">
            <asp:Label ID="label26" runat="server" CssClass="lblDemographic" Text="Physical Mark(s):" />
        </td>
        <td >
            <asp:Label ID="Label27" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 96px">
            <asp:Label ID="Label28" runat="server" CssClass="lblDemographic" Text="Prefix:" />
        </td>
        <td style="width: 211px" >
            <asp:Label ID="Label29" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
        <td style="width: 131px">
            <asp:Label ID="label30" runat="server" CssClass="lblDemographic" Text="Age:" />
        </td>
        <td >
            <asp:Label ID="Label31" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 96px">
            <asp:Label ID="Label32" runat="server" CssClass="lblDemographic" Text="First Name:" />
        </td>
        <td style="width: 211px" >
            <asp:Label ID="Label33" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
        <td style="width: 131px">
            <asp:Label ID="label34" runat="server" CssClass="lblDemographic" Text="Date of Birth:" />
        </td>
        <td >
            <asp:Label ID="Label35" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 96px">
            <asp:Label ID="Label36" runat="server" CssClass="lblDemographic" Text="Middle Name:" />
        </td>
        <td style="width: 211px" >
            <asp:Label ID="Label37" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
        <td style="width: 131px">
            <asp:Label ID="label38" runat="server" CssClass="lblDemographic" Text="Birth Place:" />
        </td>
        <td>
            <asp:Label ID="Label39" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 96px; height: 21px;">
            <asp:Label ID="Label40" runat="server" CssClass="lblDemographic" Text="Last Name:" />
        </td>
        <td style="width: 211px; height: 21px" >
            <asp:Label ID="Label41" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
        <td style="width: 131px; height: 21px">
            <asp:Label ID="label42" runat="server" CssClass="lblDemographic" Text="Sex:" />
        </td>
        <td style="height: 21px" >
            <asp:Label ID="Label43" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 96px">
            <asp:Label ID="Label44" runat="server" CssClass="lblDemographic" Text="Suffix:" />
        </td>
        <td style="width: 211px" >
            <asp:Label ID="Label45" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
        <td style="width: 131px">
            <asp:Label ID="label46" runat="server" CssClass="lblDemographic" Text="Marital Status:" />
        </td>
        <td>
            <asp:Label ID="Label47" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 96px">
            <asp:Label ID="Label48" runat="server" CssClass="lblDemographic" Text="Street Address:" />
        </td>
        <td style="width: 211px" >
            <asp:Label ID="Label49" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
        <td style="width: 131px">
            <asp:Label ID="label50" runat="server" CssClass="lblDemographic" Text="Language(s) Spoken:" />
        </td>
        <td >
            <asp:Label ID="Label51" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 96px">
            <asp:Label ID="Label52" runat="server" CssClass="lblDemographic" Text="City/Town:" />
        </td>
        <td style="width: 211px">
            <asp:Label ID="Label53" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
        <td style="width: 131px">
            <asp:Label ID="label54" runat="server" CssClass="lblDemographic" Text="Religion:" />
        </td>
        <td >
            <asp:Label ID="Label55" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 96px">
            <asp:Label ID="Label56" runat="server" CssClass="lblDemographic" Text="State:" />
        </td>
        <td style="width: 211px">
            <asp:Label ID="Label57" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
        <td style="width: 131px">
            <asp:Label ID="label58" runat="server" CssClass="lblDemographic" Text="Blood Type:" />
        </td>
        <td>
            <asp:Label ID="Label59" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 96px">
            <asp:Label ID="Label60" runat="server" CssClass="lblDemographic" Text="Country:" />
        </td>
        <td style="width: 211px">
            <asp:Label ID="Label61" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
        <td style="width: 131px">
            <asp:Label ID="label62" runat="server" CssClass="lblDemographic" Text="Occupation:" />
        </td>
        <td>
            <asp:Label ID="Label63" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 96px">
            <asp:Label ID="Label64" runat="server" CssClass="lblDemographic" Text="Home Phone:" />
        </td>
        <td style="width: 211px">
            <asp:Label ID="Label65" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
        <td style="width: 131px">
            <asp:Label ID="Label66" runat="server" CssClass="lblDemographic" Text="Hair Color:" />
        </td>
        <td>
            <asp:Label ID="Label67" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 96px">
            <asp:Label ID="label68" runat="server" CssClass="lblDemographic" Text="Eye Color:" />
        </td>
        <td style="width: 211px">
            <asp:Label ID="Label69" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
        <td style="width: 131px">
            <asp:Label ID="Label70" runat="server" CssClass="lblDemographic" Text="Race:" />
        </td>
        <td>
            <asp:Label ID="Label71" runat="server" CssClass="lblDemographic"></asp:Label>
        </td>
    </tr>
</table>--%>
<table id="new" cellpadding="2px" cellspacing="8px">
    <tr>
        <td>
            <asp:Label ID="label0" runat="server" CssClass="lblDemographic" Text="Patient ID:" />
        </td>
        <td>
            <asp:Label ID="lblID" runat="server" CssClass="lblDemographicContent"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label28" runat="server" CssClass="lblDemographic" Text="County:" /></td>
        <td>
            <asp:Label ID="lblCounty" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="label23" runat="server" CssClass="lblDemographic" Text="Eye Color:" /></td>
        <td>
            <asp:Label ID="lblEye" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="label19" runat="server" CssClass="lblDemographic" Text="Blood Type:" /></td>
        <td>
            <asp:Label ID="lblBlood" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
    </tr>
    <tr>
        <td><asp:Label ID="Label32" runat="server" CssClass="lblDemographic" Text="SSN:" Visible="false" /></td>
        <td>
            <asp:Label ID="lblSSN" runat="server" CssClass="lblDemographicContent" Visible="false"></asp:Label></td>
        <td>
            <asp:Label ID="Label16" runat="server" CssClass="lblDemographic" Text="State:" /></td>
        <td>
            <asp:Label ID="lblState" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="label3" runat="server" CssClass="lblDemographic" Text="Physical Marks:" /></td>
        <td>
            <asp:Label ID="lblPMarks" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="label21" runat="server" CssClass="lblDemographic" Text="Occupation:" /></td>
        <td>
            <asp:Label ID="lblOccupation" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" CssClass="lblDemographic" Text="Prefix:" /></td>
        <td>
            <asp:Label ID="lblPrefix" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="Label8" runat="server" CssClass="lblDemographic" Text="Zip:" /></td>
        <td>
            <asp:Label ID="lblZip" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="label5" runat="server" CssClass="lblDemographic" Text="Age:" /></td>
        <td>
            <asp:Label ID="lblAge" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="Label30" runat="server" CssClass="lblDemographic" Text="Employer:" /></td>
        <td>
            <asp:Label ID="lblEmployer" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
    </tr>
    <tr>
         <td>
            <asp:Label ID="Label2" runat="server" CssClass="lblDemographic" Text="First Name:" /></td>
        <td>
            <asp:Label ID="lblFName" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="Label24" runat="server" CssClass="lblDemographic" Text="Country:" /></td>
        <td>
            <asp:Label ID="lblCountry" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="label7" runat="server" CssClass="lblDemographic" Text="Date of Birth:" /></td>
        <td>
            <asp:Label ID="lblDOB" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="Label29" runat="server" CssClass="lblDemographic" Text="Education:" /></td>
        <td>
            <asp:Label ID="lblEducation" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
    </tr>
    <tr>
    <td>
            <asp:Label ID="Label4" runat="server" CssClass="lblDemographic" Text="Middle Name:" /></td>
        <td>
            <asp:Label ID="lblMName" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="Label31" runat="server" CssClass="lblDemographic" Text="Email:" /></td>
        <td>
            <asp:Label ID="lblEmail" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="label9" runat="server" CssClass="lblDemographic" Text="Birth Place:" /></td>
        <td>
            <asp:Label ID="lblBPlace" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="Label20" runat="server" CssClass="lblDemographic" Text="Hair Color:" /></td>
        <td>
            <asp:Label ID="lblHair" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label6" runat="server" CssClass="lblDemographic" Text="Last Name:" /></td>
        <td>
            <asp:Label ID="lblLName" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="Label18" runat="server" CssClass="lblDemographic" Text="Home Phone:" /></td>
        <td>
            <asp:Label ID="lblPhone" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="label11" runat="server" CssClass="lblDemographic" Text="Sex:" /></td>
        <td>
            <asp:Label ID="lblSex" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="Label22" runat="server" CssClass="lblDemographic" Text="Race:" /></td>
        <td>
            <asp:Label ID="lblRace" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label10" runat="server" CssClass="lblDemographic" Text="Suffix:" /></td>
        <td>
            <asp:Label ID="lblSuffix" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="Label25" runat="server" CssClass="lblDemographic" Text="Business Phone:" /></td>
        <td>
            <asp:Label ID="lblBPhone" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="label13" runat="server" CssClass="lblDemographic" Text="Marital Status:" /></td>
        <td>
            <asp:Label ID="lblMaritalStatus" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="label33" runat="server" CssClass="lblDemographic" Text="Height:" /></td>
        <td>
            <asp:Label ID="lblHeight" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label12" runat="server" CssClass="lblDemographic" Text="Street Address:" /></td>
        <td>
            <asp:Label ID="lblAddress" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="Label26" runat="server" CssClass="lblDemographic" Text="Mobile Phone:" /></td>
        <td>
            <asp:Label ID="lblCellPhone" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="label15" runat="server" CssClass="lblDemographic" Text="Languages Spoken:" /></td>
        <td>
            <asp:Label ID="lblLanguage" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="label34" runat="server" CssClass="lblDemographic" Text="Weight:" /></td>
        <td>
            <asp:Label ID="lblWeight" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label14" runat="server" CssClass="lblDemographic" Text="City/Town:" /></td>
        <td>
            <asp:Label ID="lblCity" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="Label27" runat="server" CssClass="lblDemographic" Text="Pager Number:" /></td>
        <td>
            <asp:Label ID="lblPager" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
            <asp:Label ID="label17" runat="server" CssClass="lblDemographic" Text="Religion:" /></td>
        <td>
            <asp:Label ID="lblReligion" runat="server" CssClass="lblDemographicContent"></asp:Label></td>
        <td>
        </td>
        <td>
        </td>
    </tr>
</table>
&nbsp;


