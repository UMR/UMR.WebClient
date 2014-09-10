<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDemographics.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucDemographics" %>

<asp:DetailsView ID="dtvDemographics" runat="server" AutoGenerateRows="False" GridLines="None" CellSpacing="2" 
                   OnDataBound="dtvDemographics_DataBound">
    <Fields>
        <asp:BoundField DataField="PatientID" HeaderText="ID:" ReadOnly="True" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Maroon" />
        <asp:BoundField DataField="Prefix" HeaderText="Prefix:" />
        <asp:BoundField DataField="FirstName" HeaderText="First Name:" ItemStyle-Font-Bold="True" ItemStyle-ForeColor="Maroon" />
        <asp:BoundField DataField="MiddleName" HeaderText="Middle Name:" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name:"  ItemStyle-ForeColor="Maroon" ItemStyle-Font-Bold="True"/>
        <asp:BoundField DataField="Suffix" HeaderText="Suffix:" />
        <asp:BoundField DataField="StreetAddress" HeaderText="Address:" />
        <asp:BoundField DataField="CityTown" HeaderText="City/Town:" />
        <asp:BoundField DataField="StateAndZip" HeaderText="State and Zip:" />
        <asp:BoundField DataField="Country" HeaderText="Country:" />
        <asp:TemplateField HeaderText="Home Phone:" ShowHeader="true">
            <ItemTemplate>
                <%#(Eval("HomePhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}",Double.Parse((string)Eval("HomePhone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Age:" ShowHeader="true">
            <ItemTemplate>
                <asp:Label ID ="lblAge" runat="server"/>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="PhysicalMarks" HeaderText="Physical Mark(s):" />
        <asp:BoundField DataField="DateOfBirth" HeaderText="Date of Birth:" />
        <asp:BoundField DataField="BirthPlace" HeaderText="Birth Place:" />
        <asp:BoundField DataField="Sex" HeaderText="Sex:" />
        <asp:BoundField DataField="MaritalStatus" HeaderText="Marital Status:" />
        <asp:BoundField DataField="LanguagesSpoken" HeaderText="Language(s) Spoken:" />
        <asp:BoundField DataField="Religion" HeaderText="Religion:" />
        <asp:BoundField DataField="BloodType" HeaderText="BloodType:" />
        <asp:BoundField DataField="Occupation" HeaderText="Occupation:" />
        <asp:BoundField DataField="HairColor" HeaderText="Hair Color:" />
        <asp:BoundField DataField="EyeColor" HeaderText="Eye Color:" />
        <asp:BoundField DataField="Race" HeaderText="Race:" />
        <asp:BoundField DataField="Weight" HeaderText="Weight:" />
        <asp:BoundField DataField="Height" HeaderText="Height:" />
    </Fields>
    <RowStyle Font-Names="Tahoma, Arial, Verdana" HorizontalAlign="Left" VerticalAlign="Top" Font-Size="8pt" />
    <AlternatingRowStyle Font-Names="Tahoma, Arial"  />
</asp:DetailsView>

<script type="text/javascript">
    function SetTitle(fName, lName){if(window.parent){var title = "Health History for Patient :: " + fName + " " + lName;window.parent.document.title = title;}}
</script>

<asp:Label ID="InjectScriptLabel" runat="server"></asp:Label>