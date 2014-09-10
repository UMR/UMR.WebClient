<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRDMedicationMain.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucRDMedicationMain" %>
<%--<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>

<radG:RadGrid ID="RadGridRDMedicationMain" runat="server" AllowPaging="false" EnableAJAX="false"
    GridLines="None" OnItemCreated="RadGridRDMedicationMain_ItemCreated"
    Skin="Office2007" OnNeedDataSource="RadGridRDMedicationMain_NeedDataSource" EnableAJAXLoadingTemplate="true" UseEmbeddedScripts="false">
    <MasterTableView AutoGenerateColumns="false">
        <ExpandCollapseColumn Visible="False">
            <HeaderStyle Width="19px" />
        </ExpandCollapseColumn>
        <RowIndicatorColumn Visible="False">
            <HeaderStyle Width="20px" />
        </RowIndicatorColumn>
        <Columns>
            <radG:GridTemplateColumn Display="true" HeaderText="Brand Name" UniqueName="TemplateEditColumn">
                <ItemTemplate>
                    <asp:HyperLink ID="Brand" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridBoundColumn DataField="Strength" HeaderText="Strength" UniqueName="Strength" />
            <radG:GridBoundColumn DataField="Route" HeaderText="Route" UniqueName="Route" />
            <radG:GridBoundColumn DataField="FIRSTDATE" HeaderText="First Date" UniqueName="FDate" />
            <radG:GridBoundColumn DataField="LASTDATE" HeaderText="Last Date" UniqueName="LDate" />
            <radG:GridBoundColumn DataField="F" HeaderText="F" UniqueName="Found" />
        </Columns>
        <NoRecordsTemplate>
            <asp:Literal ID="NoTextMedMain" runat="server" Text="<b>No Record to Display</b>"></asp:Literal>
        </NoRecordsTemplate>
    </MasterTableView>
    <AJAXLoadingTemplate>
        <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
    </AJAXLoadingTemplate>
</radG:RadGrid>--%>
<%--<asp:GridView ID="RadGridRDMedicationMain" runat="server" 
    AutoGenerateColumns="False" 
    Width="100%" 
    OnRowCreated="RadGridRDMedicationMain_RowCreated"
    GridLines="Both">
    <Columns>
        <asp:TemplateField HeaderText="Brand Name">
            <ItemTemplate>
                <asp:Label ID="lblExclamation" runat="server" Font-Bold="true" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Red" />
                <asp:HyperLink ID="Brand" runat="server"/>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" Width="50%" />
        </asp:TemplateField>
        <asp:BoundField DataField="Strength" HeaderText="Strength" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="Route" HeaderText="Route" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="FirstDate" HeaderText="First Date" >
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="LastDate" HeaderText="Last Date" >
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="F" HeaderText="F" >
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    </Columns>
    <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Center" />
    <RowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" />
</asp:GridView>--%>
<asp:Panel ID="pnlHidden" runat="server" Visible="false">
<asp:CheckBoxList ID="cblCodeOption" runat="server" Font-Names="Tahoma,Arial,Verdana"
    Font-Size="8pt" RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="flow"
    ForeColor="Blue">
    <asp:ListItem Text="NDC" Value="NDC" Selected="True" />
    <asp:ListItem Text="HCPCS" Value="HCPCS" Selected="False" />
</asp:CheckBoxList>
<asp:Button ID="btnApplyCodeFilter" runat="server" Text="Apply" Font-Names="Tahoma"
    Font-Size="8pt" ForeColor="Blue" OnClick="btnApplyCodeFilter_Click" />
</asp:Panel>
<asp:GridView ID="RadGridRDMedicationMain" runat="server" AutoGenerateColumns="False"
    AllowSorting="true" Width="100%" OnRowCreated="RadGridRDMedicationMain_RowCreated"
    GridLines="Both" OnSorting="RadGridRDMedicationMain_Sorting">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblExclamation" runat="server" ForeColor="Red" Font-Names="Tahoma"
                    Font-Size="9pt" Font-Bold="true" />
            </ItemTemplate>
            <HeaderStyle Font-Underline="false" />
            <ItemStyle HorizontalAlign="center" BackColor="white" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Prescription" SortExpression="BrandName">
            <ItemTemplate>
                <asp:HyperLink ID="Brand" runat="server" />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" Width="50%" />
        </asp:TemplateField>
        <asp:BoundField DataField="Strength" SortExpression="Strength" HeaderText="Strength">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="Route" SortExpression="Route" HeaderText="Route">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Latest No. of Pills">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Latest Refills">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="FirstDate" SortExpression="FirstDate" HeaderText="First Date">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="LastDate" SortExpression="LastDate" HeaderText="Last Date">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="F" SortExpression="F" HeaderText="F">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    </Columns>
    <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" ForeColor="black" HorizontalAlign="Center" />
    <RowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" />
</asp:GridView>
<%--<asp:Button ID="Button1" runat="server" Text="Add this Discipline to my Home Page" />--%>

<script type="text/javascript">
            function GetRadWindow()
		    {
			        var oWindow = null;
			        if (window.radWindow) oWindow = window.radWindow;
			        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
			        return oWindow;
		    } 
            //GetRadWindow().SetWidth(730);
            function ShowRDMedicationDrillDown(patientKey,disType,name,ndcode)
            {window.radopen("RDDetails.aspx?PatientKey="+patientKey+"&disType="+disType+"&name="+name+"&NDCCode="+ndcode, null);return false;}
</script>

