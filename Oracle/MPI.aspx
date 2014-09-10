<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MPI.aspx.cs" Inherits="Oracle_MPI" %>

<%@ Register Src="ControlLibrary/ucLegendCompactCheckBox.ascx" TagName="ucLegendCompact"
    TagPrefix="uc1" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />

    <script src="../jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="ControlLibrary/jQueryTooltip/jquery.tooltip.js"></script>

    <script type="text/javascript" src="ControlLibrary/jQueryTooltip/jquery.dimensions.js"></script>

    <script type="text/javascript">
        $(function() {
            $('.tooltip').tooltip({
	            track: true,
	            delay: 0,
	            showURL: false,
	            showBody: " - ",
	            fade: 250
            });
        });
        
        function clientRequestEnd()
        {
        $('.tooltip').tooltip({
	            track: true,
	            delay: 0,
	            showURL: false,
	            showBody: " - ",
	            fade: 250
            });
        }
    </script>

    <title>Master Patient Code Indexes</title>
    <style type="text/css">
        .tooltip
        {
            /*color:blue;
            text-decoration:none;
            cursor:hand;*/
        }
        #tooltip{
            font-family: Tahoma, Arial, Helvetica, sans-serif;
            font-size:8pt;
	        position:absolute;
	        border:1px solid #333;
	        background:#f7f5d1;
	        padding:2px 5px;
	        color:#333;
	        display:none;
	        }	
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td>
                    <asp:Panel ID="pnl" runat="server" GroupingText="Code Filter" Width="348px"
                        Font-Names="Tahoma,Arial,Verdana" Font-Size="8pt">
                        <asp:CheckBoxList ID="cblCodeOption" runat="server" Font-Names="Tahoma,Arial,Verdana"
                            Font-Size="8pt" RepeatColumns="6" RepeatDirection="Horizontal" RepeatLayout="flow"
                            ForeColor="Blue">
                                <asp:ListItem Text="CPT" Value="CPT-4" Selected="True" />
                                <asp:ListItem Text="HCPCS" Value="HCPCS" Selected="True" />
                                <asp:ListItem Text="ICD" Value="ICD-9-CM" Selected="True" />
                                <asp:ListItem Text="MDS" Value="MDS" Selected="False" />
                                <asp:ListItem Text="NDC" Value="NDC" Selected="True" />
                                <asp:ListItem Text="OASIS" Value="OASIS" Selected="False" />
                        </asp:CheckBoxList>
                        <asp:Button ID="btnApplyCodeFilter" runat="server" Text="Apply" Font-Names="Tahoma"
                            Font-Size="8pt" ForeColor="Blue" OnClick="btnApplyCodeFilter_Click" />
                    </asp:Panel>
                </td>
                <td>
                    <uc1:ucLegendCompact ID="UcLegendCompact1" runat="server" />
                </td>
                <td align="right" class="noprint">
                    <img alt="Fax" src="../Oracle/images/fax_button.jpg" onclick="return faxButton_onclick()"
                        style="cursor: pointer;" />
                    <img alt="Email" src="../Oracle/images/email_button.jpg" onclick="return emailButton_onclick()"
                        style="cursor: pointer;" />
                    <img alt="Print" src="../Oracle/images/print_button.jpg" onclick="return printRangeButton_onclick()"
                        style="cursor: pointer;" />
                    <%-- <img alt="Email Button" src="images/email_all_button.png" onclick="return emailButton_onclick()"
                        style="cursor: hand" />
                    &nbsp;
                    <img alt="Print Button" src="images/printall.jpg" onclick="return printButton_onclick()"
                        style="cursor: hand" />
                    <img alt="Print Range Button" src="images/printrange.jpg" onclick="return printRangeButton_onclick()"
                        style="cursor: hand" />--%>
                </td>
            </tr>
        </table>
        <div>
            <radG:RadGrid ID="RadGridMPI" runat="server" AllowPaging="True" EnableAJAX="True"
                UseEmbeddedScripts="false" AllowSorting="true" EnableAJAXLoadingTemplate="true"
                GridLines="None" OnItemCreated="RadGridMPI_ItemCreated" PageSize="40" OnSortCommand="RadGridMPI_SortCommand"
                OnNeedDataSource="RadGridMPI_NeedDataSource">
                <ClientSettings>
                    <ClientEvents OnRequestEnd="clientRequestEnd" />
                </ClientSettings>
                <MasterTableView AutoGenerateColumns="False">
                    <%--<ExpandCollapseColumn Visible="False">
                        <HeaderStyle Width="19px" />
                    </ExpandCollapseColumn>
                    <RowIndicatorColumn Visible="False">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>--%>
                    <Columns>
                        <radG:GridBoundColumn DataField="Code" HeaderText="Code" UniqueName="Code" />
                        <radG:GridTemplateColumn HeaderText="Modifier">
                            <ItemTemplate>
                                <asp:Literal ID="ltCodeModifier" runat="server" Text="">
                                </asp:Literal>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="False" />
                        </radG:GridTemplateColumn>
                        <radG:GridBoundColumn DataField="Type" HeaderText="Type" UniqueName="Type" />
                        <radG:GridBoundColumn DataField="Version" HeaderText="Version" UniqueName="Version" />
                        <radG:GridTemplateColumn SortExpression="MedicalContentIndex" Display="true" HeaderText="Medical Content Index"
                            UniqueName="MCI">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkMedicalContentIndex" runat="server" ForeColor="#355E3B"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </radG:GridTemplateColumn>
                        <%--  <radG:GridBoundColumn DataField="MedicalContentIndex" HeaderText="Medical Content Index"
                            UniqueName="MCI" />--%>
                        <radG:GridBoundColumn DataField="DateOfService" HeaderText="Service Date" UniqueName="SDate" />
                        <radG:GridBoundColumn DataField="ServiceTime" HeaderText="Time (EST)" UniqueName="STime" />
                        <radG:GridTemplateColumn SortExpression="ProviderID" HeaderImageUrl="~/Oracle/images/Unsortedlist.png"
                            UniqueName="ProviderID">
                            <ItemTemplate>
                                <asp:HyperLink ID="Provider" runat="server" ForeColor="Blue"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderTemplate>
                                <asp:LinkButton ID="btnProviderID" Text="Provider ID/Name" runat="server" OnClick="btnProviderID_Click"></asp:LinkButton>
                            </HeaderTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn SortExpression="DoctorsFirstName" HeaderImageUrl="~/Oracle/images/Unsortedlist.png"
                            UniqueName="ProviderName">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlProviderName" runat="server" ForeColor="Blue"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderTemplate>
                                <asp:LinkButton ID="btnProviderName" Text="Provider ID/Name" runat="server" OnClick="btnProviderName_Click"></asp:LinkButton>
                            </HeaderTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn SortExpression="InstituteCode" HeaderImageUrl="~/Oracle/images/Unsortedlist.png"
                            UniqueName="IC">
                            <ItemTemplate>
                                <asp:HyperLink ID="Institution" runat="server" ForeColor="Blue"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderTemplate>
                                <asp:LinkButton ID="btnInstitutionID" Text="Healthcare Facility ID/Name" runat="server"
                                    OnClick="btnInstitutionID_Click"></asp:LinkButton>
                            </HeaderTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn SortExpression="InstitutionName" HeaderImageUrl="~/Oracle/images/Unsortedlist.png"
                            UniqueName="InstitutionName">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlHealthcare" runat="server" ForeColor="Blue"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderTemplate>
                                <asp:LinkButton ID="btnInstitutionName" Text="Healthcare Facility ID/Name" runat="server" OnClick="btnInstitutionName_Click"></asp:LinkButton>
                            </HeaderTemplate>
                        </radG:GridTemplateColumn>
                        <%-- <radG:GridBoundColumn DataField="InstituteCode" HeaderText="Healthcare Facility ID"
                            UniqueName="IC" />--%>
                    </Columns>
                    <NoRecordsTemplate>
                        <asp:Literal ID="NoText" runat="server" Text="<b>No Record to Display</b>"></asp:Literal>
                    </NoRecordsTemplate>
                    <PagerStyle Mode="NextPrevAndNumeric" />
                    <HeaderStyle Font-Names="Tahoma" Font-Size="8pt" Font-Underline="true" HorizontalAlign="Left" />
                    <ItemStyle Font-Names="Tahoma, Verdana, Arial" Font-Size="8pt" />
                    <AlternatingItemStyle Font-Names="Tahoma" Font-Size="8pt" />
                </MasterTableView>
                <AJAXLoadingTemplate>
                    <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
                </AJAXLoadingTemplate>
            </radG:RadGrid>
            <%--<asp:GridView ID="grdMPI" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                GridLines="None" Width="100%" 
                PageSize="14" OnRowCreated="grdMPI_RowCreated" >
                <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField DataField="Code" HeaderText="Code" />
                    <asp:BoundField DataField="Type" HeaderText="Type" />
                    <asp:BoundField DataField="Version" HeaderText="Version" />
                    <asp:BoundField DataField="MedicalContentIndex" HeaderText="Medical Content Index" />
                    <asp:BoundField DataField="DateOfService" HeaderText="Date of Service" />
                    <asp:TemplateField ShowHeader="true" HeaderText="Provider ID">
                        <ItemTemplate>
                            <asp:HyperLink ID="Provider" runat="server"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="InstituteCode" HeaderText="Institution" />
                </Columns>
                <RowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" />
            </asp:GridView>--%>
        </div>
    </form>
    <radW:RadWindowManager ID="RADWMMPI" UseEmbeddedScripts="false" runat="server" InitialBehavior="maximize"
        Behavior="Close" Skin="Office2007" />
    &nbsp;
</body>

<script type="text/javascript">
            function GetRadWindow()
		    {
			        var oWindow = null;
			        if (window.radWindow) oWindow = window.radWindow;
			        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
			        return oWindow;
		    } 
            GetRadWindow().Maximize();//.SetWidth(810);
            function ShowProviderDetails(id,dispType,hasCodeStr)
            {
                window.radopen("ProviderDetails.aspx?ID="+id+"&DispType="+dispType+"&"+hasCodeStr, null);
                return false;
            }
            function ShowInstitutionDetails(code,patientKey,hasCodeStr)
            {
                window.radopen("InstitutionDetails.aspx?code="+code+"&PatientKey="+patientKey+"&"+hasCodeStr, null);
                return false;
            }
            function printButton_onclick() 
            {
                    var queryString="<%= GetQueryString() %>";
                    window.open("MPIPrint.aspx?"+queryString,'printWindow', 'resizable=no,width=5,height=5,status=no scrollbars=no,top=100,left=100');
            }
            function printRangeButton_onclick()
            {
                    var queryString="<%= GetQueryStringWithPageCount() %>";
                    window.open("MPIPrintRange.aspx?"+queryString,'printWindow', 'resizable=no,width=370,height=150,status=no scrollbars=no,top=300,left=200');
              
            }
            function emailButton_onclick()
            {
                var queryString="<%= GetQueryStringWithPageCount() %>";
                window.radopen("MPIEmail.aspx?"+queryString);
            }
            function faxButton_onclick()
            {
                var queryString="<%= GetQueryStringWithPageCount() %>";
                window.radopen("MPIFax.aspx?"+queryString);
            }
</script>
</html>
