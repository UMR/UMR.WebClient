<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPatientDemographicPreview2.ascx.cs"
    Inherits="Oracle_ControlLibrary_PrintPreview_ucPatientDemographicPreview2" %>
<style type="text/css">
    .educationItem
    {
        margin-top:5px;
        margin-bottom:5px;
    }
    .headerCollapsedStyle
    {
        background-color:#edf0f5;
        background-image:url('images/arrow_state_blue_collapsed.png');
        background-repeat:no-repeat;
        background-position:right center;
        padding:5px;
        cursor:pointer;
    }
    .headerExpandeddStyle
    {
        background-image:url('images/arrow_state_blue_expanded.png');
        background-repeat:no-repeat;
        background-position:right center;
    }
    .educationContentStyle
    {
        padding:5px;
        border:solid 1px #edf0f5;
    }
    
    .currentEmployerItem
    {
        margin-top:5px;
        margin-bottom:5px;
        width:500px;
    }
    .currentEmployerHeader
    {
        background-color:#edf0f5;
        padding:5px;
        cursor:pointer;
        background-image:url('images/arrow_state_blue_expanded.png');
        background-repeat:no-repeat;
        background-position:right center;
    }
    .currentEmployerHeaderExpandeddStyle
    {
        background-image:url('images/arrow_state_blue_collapsed.png');
        background-repeat:no-repeat;
        background-position:right center;
    }
    .currentEmployerHeader span
    {
       color:black;
       font-weight:bold;
    }
    .currentEmployerContent
    {
        padding:5px;
        border:solid 1px #edf0f5;
    }

    .formerEmployerItem
    {
        margin-top:5px;
        margin-bottom:5px;
        width:500px;
    }
    .formerEmployerHeader
    {
        background-color:#edf0f5;
        padding:5px;
        cursor:pointer;
        background-image:url('images/arrow_state_blue_collapsed.png');
        background-repeat:no-repeat;
        background-position:right center;
    }
    .formerEmployerHeaderExpandeddStyle
    {
        background-image:url('images/arrow_state_blue_expanded.png');
        background-repeat:no-repeat;
        background-position:right center;
    }
    .formerEmployerHeader span
    {
       color:black;
       font-weight:bold;
    }
    .formerEmployerContent
    {
        padding:5px;
        border:solid 1px #edf0f5;
    }
    
    .employmentItem
    {
        margin-top:5px;
        margin-bottom:5px;
        width:500px;
    }
    #dvCurrentEmployementHeader
    {
        background-color:#edf0f5;
        padding:5px;
        cursor:pointer;
        background-image:url('images/arrow_state_blue_expanded.png');
        background-repeat:no-repeat;
        background-position:right center;
    }
    .currentEmployementHeaderExpandeddStyle
    {
        background-image:url('images/arrow_state_blue_collapsed.png') !important;
        background-repeat:no-repeat !important;
        background-position:right center !important;
    }
    #dvCurrentEmployementHeader span
    {
       color:black;
       font-weight:bold;
    }
    .employmentContentStyle
    {
        padding:5px;
        border:solid 1px #edf0f5;
    }
    
     #dvFormerEmployementHeader
    {
        background-color:#edf0f5;
        padding:5px;
        cursor:pointer;
        background-image:url('images/arrow_state_blue_collapsed.png');
        background-repeat:no-repeat;
        background-position:right center;
    }
    #dvFormerEmployementHeader span
    {
       color:black;
       font-weight:bold;
    }
    .formerEmployementHeaderExpandeddStyle
    {
        background-image:url('images/arrow_state_blue_expanded.png') !important;
        background-repeat:no-repeat !important;
        background-position:right center !important;
    }
</style>

<script type="text/javascript">
    $(document).ready(function(){
        $("#dvGrammarSchoolContent").hide();
        $("#dvJrHighSchoolContent").hide();
        $("#dvHighSchoolContent").hide();
        $("#dvCollegeContent").hide();
        $("#dvGraduateSchoolContent").hide();
        $("#dvTradeContent").hide();
         
        $("#dvGrammarSchoolHeader").click(function(){
            $("#dvGrammarSchoolContent").toggle();
            $("#dvGrammarSchoolHeader").toggleClass("headerExpandeddStyle");
        });
        $("#dvJrHighSchoolHeader").click(function(){
            $("#dvJrHighSchoolContent").toggle();
            $("#dvJrHighSchoolHeader").toggleClass("headerExpandeddStyle");
        });
        $("#dvHighSchoolHeader").click(function(){
            $("#dvHighSchoolContent").toggle();
            $("#dvHighSchoolHeader").toggleClass("headerExpandeddStyle");
        });
        $("#dvCollegeHeader").click(function(){
            $("#dvCollegeContent").toggle();
            $("#dvCollegeHeader").toggleClass("headerExpandeddStyle");
        });
        $("#dvGraduateSchoolHeader").click(function(){
            $("#dvGraduateSchoolContent").toggle();
            $("#dvGraduateSchoolHeader").toggleClass("headerExpandeddStyle");
        });
        $("#dvTradeHeader").click(function(){
            $("#dvTradeContent").toggle();
            $("#dvTradeHeader").toggleClass("headerExpandeddStyle");
        });
        
        $(".currentEmployerHeader").click(function(){
            $(this).siblings().toggle();
            $(this).toggleClass("currentEmployerHeaderExpandeddStyle");
        });
        
        $(".formerEmployerContent").hide();        
        $(".formerEmployerHeader").click(function(){
            $(this).siblings().toggle();
            $(this).toggleClass("formerEmployerHeaderExpandeddStyle");
        });
        
         $("#dvCurrentEmployementHeader").click(function(){
            $("#dvCurrentEmployementContent").toggle();
            $("#dvCurrentEmployementHeader").toggleClass("currentEmployementHeaderExpandeddStyle");
        });
        
        $("#dvFormerEmployementContent").hide();
        $("#dvFormerEmployementHeader").click(function(){
            $("#dvFormerEmployementContent").toggle();
            $("#dvFormerEmployementHeader").toggleClass("formerEmployementHeaderExpandeddStyle");
        });
    });
</script>

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
<div style="font-size: 10pt; color: #000; font-weight: bold; border-right: gainsboro 1px solid;
    padding-right: 5px; padding-left: 5px; border-top: gainsboro 1px solid; padding-bottom: 5px;
    border-left: gainsboro 1px solid; padding-top: 5px; border-bottom: gainsboro 1px solid;
    background-color: #F5F5F5; width: auto;">
    Password: &nbsp;<asp:Label ID="lblPassword" runat="server" ForeColor="DarkBlue"></asp:Label>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Hint: &nbsp;
    <asp:Label ID="lblPasswordHint" runat="server" ForeColor="DarkBlue"></asp:Label>
</div>
<br />
<%-- 
<table cellspacing="0" cellpadding="4" style="color: #333333;">
    <tr>
        <td style="background-color: #e9ecf1; font-weight: bold;">
            <asp:Label ID="label0" runat="server" Text="Patient ID:" /></td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblID" runat="server"></asp:Label></td>
        <td style="background-color: #e9ecf1; font-weight: bold;">
            <asp:Label ID="Label27" runat="server" Text="Pager Number:" /></td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblPager" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
            <asp:Label ID="Label32" runat="server" Text="SSN:" Visible="false" /></td>
        <td style="color: #284775">
            <asp:Label ID="lblSSN" runat="server" Visible="false"></asp:Label></td>
        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
            <asp:Label ID="label23" runat="server" Text="Eye Color:" /></td>
        <td style="color: #284775">
            <asp:Label ID="lblEye" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="background-color: #e9ecf1; font-weight: bold;">
            <asp:Label ID="Label1" runat="server" Text="Prefix:" /></td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblPrefix" runat="server"></asp:Label></td>
        <td style="background-color: #e9ecf1; font-weight: bold;">
            <asp:Label ID="label3" runat="server" Text="Physical Marks:" /></td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblPMarks" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
            <asp:Label ID="Label2" runat="server" Text="First Name:" /></td>
        <td style="color: #284775">
            <asp:Label ID="lblFName" runat="server"></asp:Label></td>
        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
            <asp:Label ID="label5" runat="server" Text="Age:" /></td>
        <td style="color: #284775">
            <asp:Label ID="lblAge" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="background-color: #e9ecf1; font-weight: bold;">
            <asp:Label ID="Label4" runat="server" Text="Middle Name:" /></td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblMName" runat="server"></asp:Label></td>
        <td style="background-color: #e9ecf1; font-weight: bold;">
            <asp:Label ID="label7" runat="server" Text="Date of Birth:" /></td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblDOB" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
            <asp:Label ID="Label6" runat="server">Last Name:</asp:Label></td>
        <td style="color: #284775">
            <asp:Label ID="lblLName" runat="server"></asp:Label></td>
        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
            <asp:Label ID="label9" runat="server" Text="Birth Place:" /></td>
        <td style="color: #284775">
            <asp:Label ID="lblBPlace" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="background-color: #e9ecf1; font-weight: bold;">
            <asp:Label ID="Label10" runat="server" Text="Suffix:" /></td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblSuffix" runat="server"></asp:Label></td>
        <td style="background-color: #e9ecf1; font-weight: bold;">
            <asp:Label ID="label11" runat="server" Text="Sex:" /></td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblSex" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
            <asp:Label ID="Label12" runat="server" Text="Street Address:" /></td>
        <td style="color: #284775">
            <asp:Label ID="lblAddress" runat="server" C></asp:Label></td>
        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
            <asp:Label ID="label13" runat="server" Text="Marital Status:" /></td>
        <td style="color: #284775">
            <asp:Label ID="lblMaritalStatus" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="background-color: #e9ecf1; font-weight: bold;">
            <asp:Label ID="Label14" runat="server" Text="City/Town:" /></td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblCity" runat="server"></asp:Label></td>
        <td style="background-color: #e9ecf1; font-weight: bold;">
            <asp:Label ID="label15" runat="server" Text="Languages Spoken:" /></td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblLanguage" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
            <asp:Label ID="Label28" runat="server" Text="County:" />&nbsp;</td>
        <td style="color: #284775">
            <asp:Label ID="lblCounty" runat="server"></asp:Label></td>
        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
            <asp:Label ID="label17" runat="server" Text="Religion:" /></td>
        <td style="color: #284775">
            <asp:Label ID="lblReligion" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="background-color: #e9ecf1; font-weight: bold;">
            <asp:Label ID="Label16" runat="server" Text="State:" />&nbsp;</td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblState" runat="server"></asp:Label></td>
        <td style="background-color: #e9ecf1; font-weight: bold;">
            <asp:Label ID="label19" runat="server" Text="Blood Type:" /></td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblBlood" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
            <asp:Label ID="Label8" runat="server" Text="Zip Code:" /></td>
        <td style="color: #284775">
            <asp:Label ID="lblZip" runat="server"></asp:Label></td>
        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
            <asp:Label ID="label21" runat="server" Text="Occupation:" /></td>
        <td style="color: #284775">
            <asp:Label ID="lblOccupation" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="background-color: #e9ecf1; font-weight: bold;">
            <asp:Label ID="Label24" runat="server" Text="Country:" /></td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblCountry" runat="server"></asp:Label></td>
        <td style="background-color: #e9ecf1; font-weight: bold;">
            <asp:Label ID="Label29" runat="server" Text="Employer:" /></td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblEmployer" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
            <asp:Label ID="Label31" runat="server" Text="Email:" /></td>
        <td style="color: #284775">
            <asp:Label ID="lblEmail" runat="server"></asp:Label></td>
        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
            <asp:Label ID="Label30" runat="server" Text="Education:" /></td>
        <td style="color: #284775">
            <asp:Label ID="lblEducation" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="font-weight: bold; background-color: #e9ecf1">
            <asp:Label ID="Label18" runat="server" Text="Home Phone:" /></td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblPhone" runat="server"></asp:Label></td>
        <td style="font-weight: bold; background-color: #e9ecf1">
            <asp:Label ID="Label20" runat="server" Text="Hair Color:" /></td>
        <td style="background-color: #f7f6f3;">
            <asp:Label ID="lblHair" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="font-weight: bold; color: #284775; background-color: #e9ecf1">
            <asp:Label ID="Label25" runat="server" Text="Business Phone:" /></td>
        <td style="color: #284775">
            <asp:Label ID="lblBPhone" runat="server"></asp:Label></td>
        <td style="font-weight: bold; color: #284775; background-color: #e9ecf1">
            <asp:Label ID="Label22" runat="server" Text="Race:" /></td>
        <td style="color: #284775">
            <asp:Label ID="lblRace" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="font-weight: bold; background-color: #e9ecf1">
            <asp:Label ID="Label26" runat="server" Text="Mobile Phone:" /></td>
        <td style="background-color: #f7f6f3">
            <asp:Label ID="lblCellPhone" runat="server"></asp:Label></td>
        <td style="font-weight: bold; color: #284775; background-color: #e9ecf1">
        </td>
        <td style="background-color: #f7f6f3">
        </td>
    </tr>
</table>
--%>
<table style="width: 100%;">
    <tr>
        <td align="left" valign="top">
            <table cellspacing="0" cellpadding="4" style="color: #333333;">
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; width: 100px;">
                        <asp:Label ID="Label1" runat="server" Text="Prefix:" /></td>
                    <td style="background-color: #f7f6f3; width: 150px;">
                        <asp:Label ID="lblPrefix" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                        <asp:Label ID="Label2" runat="server" Text="First Name:" /></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblFName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold;">
                        <asp:Label ID="Label4" runat="server" Text="Middle Name:" /></td>
                    <td style="background-color: #f7f6f3">
                        <asp:Label ID="lblMName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                        <asp:Label ID="Label6" runat="server">Last Name:</asp:Label></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblLName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold;">
                        <asp:Label ID="Label10" runat="server" Text="Suffix:" /></td>
                    <td style="background-color: #f7f6f3">
                        <asp:Label ID="lblSuffix" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                        <asp:Label ID="Label12" runat="server" Text="Street Address:" /></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblAddress" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold;">
                        <asp:Label ID="Label14" runat="server" Text="City/Town:" /></td>
                    <td style="background-color: #f7f6f3">
                        <asp:Label ID="lblCity" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                        <asp:Label ID="Label28" runat="server" Text="County:" />&nbsp;</td>
                    <td style="color: #284775">
                        <asp:Label ID="lblCounty" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold;">
                        <asp:Label ID="Label16" runat="server" Text="State:" />&nbsp;</td>
                    <td style="background-color: #f7f6f3">
                        <asp:Label ID="lblState" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                        <asp:Label ID="Label8" runat="server" Text="Zip Code:" /></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblZip" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold;">
                        <asp:Label ID="Label24" runat="server" Text="Country:" /></td>
                    <td style="background-color: #f7f6f3">
                        <asp:Label ID="lblCountry" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                        <asp:Label ID="Label31" runat="server" Text="Email:" /></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblEmail" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold; background-color: #e9ecf1">
                        <asp:Label ID="Label18" runat="server" Text="Home Phone:" /></td>
                    <td style="background-color: #f7f6f3">
                        <asp:Label ID="lblPhone" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold; color: #284775; background-color: #e9ecf1">
                        <asp:Label ID="Label25" runat="server" Text="Business Phone:" /></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblBPhone" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold; background-color: #e9ecf1">
                        <asp:Label ID="Label26" runat="server" Text="Mobile Phone:" /></td>
                    <td style="background-color: #f7f6f3">
                        <asp:Label ID="lblCellPhone" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold;">
                        <asp:Label ID="label0" runat="server" Text="Patient ID:" Visible="false" /></td>
                    <td style="background-color: #f7f6f3">
                        <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                        <asp:Label ID="Label32" runat="server" Text="SSN:" Visible="false" /></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblSSN" runat="server" Visible="false"></asp:Label></td>
                </tr>
            </table>
        </td>
        <td align="left" valign="top">
            <table cellspacing="0" cellpadding="4" style="color: #333333;">
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; width: 120px;">
                        <asp:Label ID="Label27" runat="server" Text="Pager Number:" /></td>
                    <td style="background-color: #f7f6f3; width: 500px;">
                        <asp:Label ID="lblPager" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                        <asp:Label ID="label23" runat="server" Text="Eye Color:" /></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblEye" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold;">
                        <asp:Label ID="Label20" runat="server" Text="Hair Color:" /></td>
                    <td style="background-color: #f7f6f3">
                        <asp:Label ID="lblHair" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                        <asp:Label ID="label3" runat="server" Text="Physical Marks:" /></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblPMarks" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold;">
                        <asp:Label ID="label40" runat="server" Text="Height:" /></td>
                    <td style="background-color: #f7f6f3">
                        <asp:Label ID="lblHeight" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                        <asp:Label ID="label42" runat="server" Text="Weight:" /></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblWeight" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold;">
                        <asp:Label ID="label5" runat="server" Text="Age:" /></td>
                    <td style="background-color: #f7f6f3">
                        <asp:Label ID="lblAge" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                        <asp:Label ID="label7" runat="server" Text="Date of Birth:" /></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblDOB" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold;">
                        <asp:Label ID="label9" runat="server" Text="Birth Place:" /></td>
                    <td style="background-color: #f7f6f3">
                        <asp:Label ID="lblBPlace" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                        <asp:Label ID="label11" runat="server" Text="Sex:" /></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblSex" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold;">
                        <asp:Label ID="label13" runat="server" Text="Marital Status:" /></td>
                    <td style="background-color: #f7f6f3">
                        <asp:Label ID="lblMaritalStatus" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                        <asp:Label ID="label15" runat="server" Text="Languages Spoken:" /></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblLanguage" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold;">
                        <asp:Label ID="label17" runat="server" Text="Religion:" /></td>
                    <td style="background-color: #f7f6f3">
                        <asp:Label ID="lblReligion" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                        <asp:Label ID="label19" runat="server" Text="Blood Type:" /></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblBlood" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold;">
                        <asp:Label ID="Label22" runat="server" Text="Race:" /></td>
                    <td style="background-color: #f7f6f3">
                        <asp:Label ID="lblRace" runat="server"></asp:Label>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                        <asp:Label ID="label21" runat="server" Text="Occupation:" /></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblOccupation" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold; background-color: #e9ecf1" valign="top">
                        <asp:Label ID="Label30" runat="server" Text="Education:" /></td>
                    <td style="background-color: #ffffff;">
                        <asp:Label ID="lblNoEducation" runat="server" Visible="false"></asp:Label>
                        <div class="educationItem">
                            <div id="dvGrammarSchoolHeader" class="headerCollapsedStyle">
                                <asp:Label ID="lbl1" runat="server" Text="Grammar School" Font-Bold="true">
                                </asp:Label>
                            </div>
                            <div id="dvGrammarSchoolContent" class="educationContentStyle">
                                <asp:DataList ID="dataListGrammarSchool" runat="server">
                                    <ItemTemplate>
                                        <table style="background-color: White; margin: 5px; border-collapse: collapse; padding: 5px;"
                                            cellpadding="5">
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; width: 140px;">
                                                    Name:
                                                </td>
                                                <td style="background-color: #f7f6f3; width: 390px;">
                                                    <%# Eval("NAME")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Address:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("ADDRESS")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    City/Town:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("CITY_TOWN")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    State
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("State")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Zip:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("ZIP")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Start Date:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# String.Format("{0:MM-dd-yyyy}", Eval("START_DATE"))%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Completion Date:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%#String.Format("{0:MM-dd-yyyy}", Eval("COMPLETE_DATE"))%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                        <div class="educationItem">
                            <div id="dvJrHighSchoolHeader" class="headerCollapsedStyle">
                                <asp:Label ID="Label33" runat="server" Text="Jr. High School " Font-Bold="true">
                                </asp:Label>
                            </div>
                            <div id="dvJrHighSchoolContent" class="educationContentStyle">
                                <asp:DataList ID="dataListJrHighSchool" runat="server">
                                    <ItemTemplate>
                                        <table style="background-color: White; margin: 5px; border-collapse: collapse; padding: 5px;"
                                            cellpadding="5">
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; width: 140px;">
                                                    Name:
                                                </td>
                                                <td style="background-color: #f7f6f3; width: 390px;">
                                                    <%# Eval("NAME")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Address:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("ADDRESS")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    City/Town:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("CITY_TOWN")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    State
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("State")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Zip:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("ZIP")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Start Date:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# String.Format("{0:MM-dd-yyyy}", Eval("START_DATE"))%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Completion Date:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%#String.Format("{0:MM-dd-yyyy}", Eval("COMPLETE_DATE"))%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                        <div class="educationItem">
                            <div id="dvHighSchoolHeader" class="headerCollapsedStyle">
                                <asp:Label ID="Label34" runat="server" Text="High School" Font-Bold="true">
                                </asp:Label>
                            </div>
                            <div id="dvHighSchoolContent" class="educationContentStyle">
                                <asp:DataList ID="dataListHighSchool" runat="server">
                                    <ItemTemplate>
                                        <table style="background-color: White; margin: 5px; border-collapse: collapse; padding: 5px;"
                                            cellpadding="5">
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; width: 140px;">
                                                    Name:
                                                </td>
                                                <td style="background-color: #f7f6f3; width: 390px;">
                                                    <%# Eval("NAME")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Address:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("ADDRESS")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    City/Town:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("CITY_TOWN")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    State
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("State")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Zip:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("ZIP")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Start Date:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# String.Format("{0:MM-dd-yyyy}", Eval("START_DATE"))%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Completion Date:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%#String.Format("{0:MM-dd-yyyy}", Eval("COMPLETE_DATE"))%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Diploma:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("Diploma")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                        <div class="educationItem">
                            <div id="dvCollegeHeader" class="headerCollapsedStyle">
                                <asp:Label ID="Label35" runat="server" Text="College" Font-Bold="true">
                                </asp:Label>
                            </div>
                            <div id="dvCollegeContent" class="educationContentStyle">
                                <asp:DataList ID="dataListCollege" runat="server">
                                    <ItemTemplate>
                                        <table style="background-color: White; margin: 5px; border-collapse: collapse; padding: 5px;"
                                            cellpadding="5">
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; width: 140px;">
                                                    Name:
                                                </td>
                                                <td style="background-color: #f7f6f3; width: 390px;">
                                                    <%# Eval("NAME")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Address:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("ADDRESS")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    City/Town:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("CITY_TOWN")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    State
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("State")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Zip:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("ZIP")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Start Date:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# String.Format("{0:MM-dd-yyyy}", Eval("START_DATE"))%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Completion Date:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%#String.Format("{0:MM-dd-yyyy}", Eval("COMPLETE_DATE"))%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Degree:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("Degree")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                        <div class="educationItem">
                            <div id="dvGraduateSchoolHeader" class="headerCollapsedStyle">
                                <asp:Label ID="Label36" runat="server" Text="Graduate School" Font-Bold="true">
                                </asp:Label>
                            </div>
                            <div id="dvGraduateSchoolContent" class="educationContentStyle">
                                <asp:DataList ID="dataListGraduateSchool" runat="server">
                                    <ItemTemplate>
                                        <table style="background-color: White; margin: 5px; border-collapse: collapse; padding: 5px;"
                                            cellpadding="5">
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; width: 140px;">
                                                    Name:
                                                </td>
                                                <td style="background-color: #f7f6f3; width: 390px;">
                                                    <%# Eval("NAME")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Address:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("ADDRESS")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    City/Town:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("CITY_TOWN")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    State
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("State")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Zip:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("ZIP")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Start Date:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# String.Format("{0:MM-dd-yyyy}", Eval("START_DATE"))%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Completion Date:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%#String.Format("{0:MM-dd-yyyy}", Eval("COMPLETE_DATE"))%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Degree:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("Degree")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                        <div class="educationItem">
                            <div id="dvTradeHeader" class="headerCollapsedStyle">
                                <asp:Label ID="Label37" runat="server" Text="Trade, Business or Correspondence School"
                                    Font-Bold="true">
                                </asp:Label>
                            </div>
                            <div id="dvTradeContent" class="educationContentStyle">
                                <asp:DataList ID="dataListTrade" runat="server">
                                    <ItemTemplate>
                                        <table style="background-color: White; margin: 5px; border-collapse: collapse; padding: 5px;"
                                            cellpadding="5">
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; width: 140px;">
                                                    Name:
                                                </td>
                                                <td style="background-color: #f7f6f3; width: 390px;">
                                                    <%# Eval("NAME")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Address:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("ADDRESS")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    City/Town:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("CITY_TOWN")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    State
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("State")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Zip:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("ZIP")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Start Date:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# String.Format("{0:MM-dd-yyyy}", Eval("START_DATE"))%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Completion Date:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%#String.Format("{0:MM-dd-yyyy}", Eval("COMPLETE_DATE"))%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Degree:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("Degree")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold; color: #284775; background-color: #e9ecf1" align="left"
                        valign="top">
                        <asp:Label ID="Label29" runat="server" Text="Employment History:" /></td>
                    <td style="color: #284775">
                        <asp:Label ID="lblNoEmplyer" runat="server" Visible="false"></asp:Label>
                        <div class="employmentItem">
                            <div id="dvCurrentEmployementHeader">
                                <asp:Label ID="Label38" runat="server" Text="Current Employment" Font-Bold="true">
                                </asp:Label>
                            </div>
                            <div id="dvCurrentEmployementContent" class="employmentContentStyle">
                                <asp:DataList ID="dataListEmployerCurrent" runat="server">
                                    <ItemTemplate>
                                        <table style="background-color: White; margin: 5px; border-collapse: collapse; padding: 5px;"
                                            cellpadding="5">
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Company Name:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("COMPANY_NAME") %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Job Title:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("JOBTITLE") %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Address:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("ADDRESS")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    City/Town:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("CITY_TOWN")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    State:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("State")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Zip:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("ZIP")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Phone:
                                                </td>
                                                <td style="color: #284775">
                                                    <%#(Eval("EMPLOYERS_PHONE") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("EMPLOYERS_PHONE")))%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Employed From:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# ((DateTime)Eval("EMPLOYED_FROM")).ToString("MM-dd-yyyy")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Employed To:
                                                </td>
                                                <td style="color: #284775">
                                                    <span>--</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Number of year(s):
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# ((int)(DateTime.Now-((DateTime)Eval("EMPLOYED_FROM"))).TotalDays/365).ToString()%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                        <div class="employmentItem">
                            <div id="dvFormerEmployementHeader">
                                <asp:Label ID="Label39" runat="server" Text="Former Employment" Font-Bold="true">
                                </asp:Label>
                            </div>
                            <div id="dvFormerEmployementContent" class="employmentContentStyle">
                                <asp:DataList ID="dataListEmployerFormer" runat="server">
                                    <ItemTemplate>
                                        <table style="background-color: White; margin: 5px; border-collapse: collapse; padding: 5px;"
                                            cellpadding="5">
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Company Name:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("COMPANY_NAME") %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Job Title:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("JOBTITLE") %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Address:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("ADDRESS")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    City/Town:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("CITY_TOWN")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    State:
                                                </td>
                                                <td style="color: #284775">
                                                    <%# Eval("State")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Zip:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# Eval("ZIP")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Phone:
                                                </td>
                                                <td style="color: #284775">
                                                    <%#(Eval("EMPLOYERS_PHONE") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("EMPLOYERS_PHONE")))%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Employed From:
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# ((DateTime)Eval("EMPLOYED_FROM")).ToString("MM-dd-yyyy")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold;">
                                                    Employed To:
                                                </td>
                                                <td style="color: #284775">
                                                    <span>--</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                                    Number of year(s):
                                                </td>
                                                <td style="background-color: #f7f6f3">
                                                    <%# ((int)(DateTime.Now-((DateTime)Eval("EMPLOYED_FROM"))).TotalDays/365).ToString()%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                        <asp:DataList ID="dataListEmployer" runat="server">
                            <ItemTemplate>
                                <table style="width: 550px; background-color: White; margin: 5px; border-collapse: collapse;
                                    padding: 5px;" cellpadding="5">
                                    <tr>
                                        <td style="background-color: #e9ecf1; font-weight: bold; width: 100px;">
                                            Company Name:
                                        </td>
                                        <td style="background-color: #f7f6f3">
                                            <%# Eval("COMPANY_NAME") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                            Job Title:
                                        </td>
                                        <td style="color: #284775">
                                            <%# Eval("JOBTITLE") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #e9ecf1; font-weight: bold;">
                                            Phone:
                                        </td>
                                        <td style="background-color: #f7f6f3">
                                            <%#(Eval("EMPLOYERS_PHONE") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("EMPLOYERS_PHONE")))%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                            Address:
                                        </td>
                                        <td style="color: #284775">
                                            <%# Eval("ADDRESS")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #e9ecf1; font-weight: bold;">
                                            City/Town:
                                        </td>
                                        <td style="background-color: #f7f6f3">
                                            <%# Eval("CITY_TOWN")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                            State:
                                        </td>
                                        <td style="color: #284775">
                                            <%# Eval("State")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #e9ecf1; font-weight: bold;">
                                            Zip:
                                        </td>
                                        <td style="background-color: #f7f6f3">
                                            <%# Eval("ZIP")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #e9ecf1; font-weight: bold; color: #284775;">
                                            Employed From:
                                        </td>
                                        <td style="color: #284775">
                                            <%# ((DateTime)Eval("EMPLOYED_FROM")).ToString("MM-dd-yyyy")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #e9ecf1; font-weight: bold;">
                                            Employed To:
                                        </td>
                                        <td style="background-color: #f7f6f3">
                                            <%#(Eval("EMPLOYED_TO") == DBNull.Value) ? "Continuing" : String.Format("{0:MM-dd-yyyy}",Eval("EMPLOYED_TO"))%>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList></td>
                </tr>
                <tr>
                    <td style="font-weight: bold; color: #284775; background-color: #e9ecf1">
                    </td>
                    <td style="background-color: #f7f6f3">
                        &nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
</table>
&nbsp; 